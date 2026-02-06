using MadreseManCore;
using MadreseManModels.Auth;
using MadreseManModels.BaseInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadreseManApi.Controllers
{
    [Route("api/v1/AcademicYear/")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private DataContext _context;
        private AuthContext _authContext;
        public AcademicYearController(DataContext context, AuthContext authContext)
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
                return Ok(_context.academic_years.ToArray());
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Create([FromBody] AcademicYear entity, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                try
                {
                    AcademicYear otherentity = _context.academic_years.FirstOrDefault(e => e.active == true);
                    if(otherentity != null) {
                        otherentity.active = false;
                    }

                    entity.active = true;
                    _context.academic_years.Add(entity);
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
        public IActionResult Edit(string? session, [FromBody] AcademicYear newEntity)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                AcademicYear entity = _context.academic_years.FirstOrDefault(e => e.id == newEntity.id);
                entity.title = newEntity.title;

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
                    AcademicYear entity = _context.academic_years.FirstOrDefault(e => e.id == id);
                    if (entity != null)
                    {
                        _context.academic_years.Remove(entity);
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
                return BadRequest("{\"error\":'لطفا ایدی سال را ارسال کنید'}");
            }
            else
            {
                AcademicYear entity = _context.academic_years.FirstOrDefault(e => e.id == id);
                if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
                {
                    if (entity != null)
                    {
                        return Ok(entity);
                    }
                    else
                    {
                        return NotFound("{ \"error\":\"سالی با این ایدی وجود ندارد\" }");
                    }
                }
                else
                {
                    return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
                }

            }
        }

        [HttpGet]
        public IActionResult getActiveAcademicYear()
        {
            return Ok(_context.academic_years.FirstOrDefault(e=>e.active == true).id);
        }

    }
}
