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
    public class TuitionPaymentController : ControllerBase
    {
        private DataContext _context;
        private AuthContext _authContext;
        public TuitionPaymentController(DataContext context, AuthContext authContext)
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
                return Ok(_context.tuition_payments.ToArray());
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Create([FromBody] TuitionPayments entity, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                try
                {
                    _context.tuition_payments.Add(entity);
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
        public IActionResult Edit(string? session, [FromBody] TuitionPayments newEntity)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                TuitionPayments entity = _context.tuition_payments.FirstOrDefault(e => e.payment_id == newEntity.payment_id);
                entity.payment_date = newEntity.payment_date;
                entity.description = newEntity.description;
                entity.student_id = newEntity.student_id;
                entity.amount_paid = newEntity.amount_paid;
                entity.attachment_id = newEntity.attachment_id;


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
                    TuitionPayments entity = _context.tuition_payments.FirstOrDefault(e => e.payment_id == id);
                    if (entity != null)
                    {
                        _context.tuition_payments.Remove(entity);
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
                TuitionPayments entity = _context.tuition_payments.FirstOrDefault(e => e.payment_id == id);
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

    }
}
