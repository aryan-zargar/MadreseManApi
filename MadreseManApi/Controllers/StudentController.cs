using MadreseManCore;
using MadreseManModels.Auth;
using MadreseManModels.BaseInfo;
using MadreseManModels.complications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadreseManApi.Controllers
{
    [Route("api/v1/[Controller]/")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private DataContext _context;
        private AuthContext _authContext;
        public StudentController(DataContext context, AuthContext authContext)
        {
            _context = context;
            _authContext = authContext;
        }

        [HttpGet("GetAll")]
        public IActionResult Get(string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                return Ok(_context.students.ToArray());
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Create([FromBody] Student entity, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                try
                {
                    _context.students.Add(entity);
                    _context.SaveChanges();
                    return Ok(entity);
                }   
                catch (Exception e)
                {
                    return StatusCode(500, e);
                }
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPut("Update")]
        public IActionResult Edit(string? session, [FromBody] Student newEntity)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                Student entity = _context.students.FirstOrDefault(e => e.id == newEntity.id);
                entity.name = newEntity.name;
                entity.lastname = newEntity.lastname;
                entity.national_id = newEntity.national_id;
                entity.birth_date = newEntity.birth_date;
                entity.grade_id = newEntity.grade_id;
                entity.class_id = newEntity.class_id;



                _context.SaveChanges();
                return Ok(entity);
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }

        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int? id, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }
            else if (id == null)
            {
                return BadRequest("{\"error\":'لطفا ایدی سال را ارسال کنید'}");
            }
            else
            {
                if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
                {
                    Student entity = _context.students.FirstOrDefault(e => e.id == id);
                    if (entity != null)
                    {
                        _context.students.Remove(entity);
                        _context.SaveChanges();
                        return Ok("Success !");
                    }
                    else
                    {
                        return NotFound("{ \"error\":\"انباری با این ایدی وجود ندارد\" }");
                    }
                }
                else
                {
                    return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
                }
            }

        }
        [HttpPost("Delete")]
        public IActionResult DeleteWithList( string? session ,[FromBody] List<int> idlist )
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }
            else if (idlist == null)
            {
                return BadRequest("{\"error\":'لطفا ایدی سال را ارسال کنید'}");
            }
            else
            {
                if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
                {
                    foreach (var item in idlist)
                    {
                        Student entity = _context.students.FirstOrDefault(e => e.id == item);
                        if (entity != null)
                        {
                            _context.students.Remove(entity);
                            
                            
                        }
                    }
                    _context.SaveChanges();
                    return Ok("Success !");

                }
                else
                {
                    return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
                }
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetById(string? session, int id)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }
            else if (id == null)
            {
                return BadRequest("{\"error\":'لطفا ایدی را ارسال کنید'}");
            }
            else
            {
                Student entity = _context.students.FirstOrDefault(e => e.id == id);
                if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
                {
                    if (entity != null)
                    {
                        return Ok(entity);
                    }
                    else
                    {
                        return NotFound("{ \"error\":\" این ایدی وجود ندارد\" }");
                    }
                }
                else
                {
                    return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
                }

            }
        }
        [HttpPost("/login")]
        public IActionResult Login(string? username, string? password) {
            try
            {
                Student entity = _context.students.FirstOrDefault((e) => e.national_id == username && e.national_id == password);
                if (entity != null)
                {
                    //return Ok(entity);
                }
                else
                {
                    return NotFound("{ \"error\":\"نام کاربری یا رمز عبور اشتباه می باشد\" }");
                }
                Session newSession = new Session() { UserId = entity.id, SessionId = Guid.NewGuid().ToString() };
                Session lastSession = _authContext.sessions.FirstOrDefault(e => e.UserId == entity.id);
                if (lastSession != null)
                {
                    lastSession.SessionId = newSession.SessionId;
                }
                else
                {
                    _authContext.sessions.Add(newSession);
                }
                _authContext.SaveChanges();
                return Ok(newSession.SessionId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
}
