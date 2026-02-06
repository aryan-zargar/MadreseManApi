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
    public class ReportCardEntryController : ControllerBase
    {
        private DataContext _context;
        private AuthContext _authContext;
        public ReportCardEntryController(DataContext context, AuthContext authContext)
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
                return Ok(_context.report_card_entries.ToArray());
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Create([FromBody] ReportCardEntry entity, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                try
                {
                    _context.report_card_entries.Add(entity);
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
        public IActionResult Edit(string? session, [FromBody] ReportCardEntry newEntity)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                ReportCardEntry entity = _context.report_card_entries.FirstOrDefault(e => e.id == newEntity.id);
                entity.report_card_id = newEntity.report_card_id;
                entity.subject_id = newEntity.subject_id;
                entity.score = newEntity.score;
                entity.student_id = newEntity.student_id;


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
                    ReportCardEntry entity = _context.report_card_entries.FirstOrDefault(e => e.id == id);
                    if (entity != null)
                    {
                        _context.report_card_entries.Remove(entity);
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
                ReportCardEntry entity = _context.report_card_entries.FirstOrDefault(e => e.id == id);
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
