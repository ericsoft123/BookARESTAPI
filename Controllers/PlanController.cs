using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MywebApi.Data;

namespace MywebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly BookDbContext _db;

        public PlanController(BookDbContext db)
        {
            _db = db;
        }
        [HttpGet("view-all")]
        public IActionResult Getall()
        {
            var plan = _db.plan.ToList();
            return Ok(new { data = plan });
        }

    }
}