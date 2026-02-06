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
    public class BudgetTransactionController : ControllerBase
    {
        private DataContext _context;
        private AuthContext _authContext;
        public BudgetTransactionController(DataContext context, AuthContext authContext)
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
                return Ok(_context.budget_transactions.ToArray());
            }
            else
            {
                return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
            }
        }

        [HttpPost("Add")]
        public IActionResult Create([FromBody] BudgetTransaction entity, string? session)
        {
            if (session == null)
            {
                return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
            }

            if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
            {
                try
                {
                    Budget change_budget_amount = _context.budgets.FirstOrDefault(e => e.id == entity.budget_id);
                    if (entity.is_deposit == true)
                    {
                        change_budget_amount.budget_amount += entity.transaction_amount;
                    }
                    else
                    {
                        change_budget_amount.budget_amount -= entity.transaction_amount;

                    }
                    entity.date = DateOnly.FromDateTime(DateTime.Now);
                    _context.budget_transactions.Add(entity);
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

        //[HttpPut("Update")]
        //public IActionResult Edit(string? session, [FromBody] Class newEntity)
        //{
        //    if (session == null)
        //    {
        //        return BadRequest("{\"error\":\"لطفا ایدی سشن را ارسال کنید\"}");
        //    }

        //    if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
        //    {
        //        Class entity = _context.classes.FirstOrDefault(e => e.id == newEntity.id);
        //        entity.class_name = newEntity.class_name;
        //        entity.grade_id = newEntity.grade_id;


        //        _context.SaveChanges();
        //        return Ok(entity);
        //    }
        //    else
        //    {
        //        return BadRequest("{\"error\":\"کاربری با این مشخصات وجود ندارد\"}");
        //    }

        //}

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
                    BudgetTransaction entity = _context.budget_transactions.FirstOrDefault(e => e.id == id);
                    Budget Budgetentity = _context.budgets.FirstOrDefault(e => e.id == entity.budget_id);

                    if (entity != null && Budgetentity != null)
                    {
                        if (entity.is_deposit == true)
                        {
                            Budgetentity.budget_amount -= entity.transaction_amount;
                        }
                        else
                        {
                            Budgetentity.budget_amount += entity.transaction_amount;

                        }
                        _context.budget_transactions.Remove(entity);
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
                BudgetTransaction entity = _context.budget_transactions.FirstOrDefault(e => e.id == id);
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

        [HttpGet("GetByBudgetId/{id}")]
        public IActionResult GetByBudgetId(string? session, int id)
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
                //BudgetTransaction entity = _context.budget_transactions.Where(e => e.budget_id == id);
                if (_authContext.sessions.FirstOrDefault(e => e.SessionId == session) != null)
                {
                    if (_context.budget_transactions.Where(e => e.budget_id == id) != null)
                    {
                        return Ok(_context.budget_transactions.Where(e => e.budget_id == id));
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
