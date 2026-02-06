using MadreseManCore;
using MadreseManModels.Auth;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System;
using Newtonsoft.Json;
using MadreseManModels.BaseInfo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

public class EmailRequest
{
    public string subject { get; set; }
    public string message { get; set; }

    public List<string> toEmail { get; set; }
}

namespace MadreseManApi.Controllers
{
    [Route("api/v1/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private AuthContext _context;
        private DataContext _DataContext;

        public AuthController(AuthContext context, DataContext dataContext)
        {
            _context = context;
            _DataContext = dataContext;
        }

        [HttpPost("users")]
        public IActionResult Post([FromBody] User entity)
        {
            try
            {
                Random rng = new Random();
                entity.confirmationCode = rng.Next(1000, 9999);
                _context.users.Add(entity);
                _context.SaveChanges();
                return Ok(entity);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("sendEmail")]
        public async Task<IActionResult> SendEmail(string email)
        {
            User entity = _context.users.First(e => e.email == email);
            int confirmation_code = _context.users.First(e => e.email == email).confirmationCode;
            string code_string = confirmation_code.ToString();

            using (var client = new HttpClient())
            {
                EmailRequest request = new EmailRequest();
                request.subject = "کد احراز هویت";
                request.message = "سلام کاربر گرامی\nکد ورود برای ایمیل " + email + ":\n" + code_string;
                request.toEmail = new List<string> { email };

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.0.0.1:8080/mailService/", content);

                // Read response content as string or JSON
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { message = "Email sent successfully", response = responseContent });
                }
                else
                {
                    return StatusCode((int)response.StatusCode, new { error = "Failed to send email", response = responseContent });
                }
            }
        }

        [HttpGet("ConfirmUser")]
        public IActionResult ConfirmUser(string code,string email)
        {
            User entity = _context.users.FirstOrDefault(e => e.email == email);
            string stringConfirmationCode = entity.confirmationCode.ToString();
            if (entity == null) {
                return BadRequest("این ایمیل در سیستم وجود ندارد");
            }
            if (stringConfirmationCode == code) 
            {
                entity.isConfirmed = true;
                _context.SaveChanges();
                Session session = _context.sessions.FirstOrDefault(e =>  e.UserId == entity.id);
                Random rng = new Random();
                entity.confirmationCode = rng.Next(1000, 9999);
                _context.SaveChanges();
                return Ok(session.SessionId);
            }
            else
            {
                return StatusCode(500,"the code is wrong");
            }
        }

        [HttpPost("login")]
        public IActionResult CreateUserSession(string? username, string? password)
        {

            try
            {
                User entity = _context.users.FirstOrDefault((e) => e.username == username && e.password == password);
                if (entity != null)
                {
                    //return Ok(entity);
                }
                else
                {
                    return NotFound("{ \"error\":\"نام کاربری یا رمز عبور اشتباه می باشد\" }");
                }
                Session newSession = new Session() { UserId = entity.id, SessionId = Guid.NewGuid().ToString() };
                Session lastSession = _context.sessions.FirstOrDefault(e => e.UserId == entity.id);
                if (lastSession != null)
                {
                    lastSession.SessionId = newSession.SessionId;
                }
                else
                {
                    _context.sessions.Add(newSession);
                }
                _context.SaveChanges();
                return Ok(entity);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("sessions/GetUserBySessionId/{sessionId}")]

        public IActionResult GetUserBySessionId(string sessionId)
        {
            Session SessionEntity = _context.sessions.FirstOrDefault((e) => e.SessionId == sessionId);
            if (SessionEntity != null)
            {
                User UserEntity = _context.users.FirstOrDefault((e) => e.id == SessionEntity.UserId);
                if (UserEntity != null)
                {
                    UserEntity.password = "hidden";
                    return Ok(UserEntity);
                }
                else
                {
                    return NotFound("The User Entity was Null");
                }
            }
            else
            {
                return NotFound("The Session Entity Was Null");
            }

        }
        [HttpGet("sessions/GetStudentBySessionId/{sessionId}")]

        public IActionResult GetStudentBySessionId(string sessionId)
        {
            Session SessionEntity = _context.sessions.FirstOrDefault((e) => e.SessionId == sessionId);
            if (SessionEntity != null)
            {
                Student UserEntity = _DataContext.students.FirstOrDefault((e) => e.id == SessionEntity.UserId);
                if (UserEntity != null)
                {
                    return Ok(UserEntity);
                }
                else
                {
                    return NotFound("The User Entity was Null");
                }
            }
            else
            {
                return NotFound("The Session Entity Was Null");
            }

        }
    }
}
