using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testfirst.API.Data;

namespace Testfirst.API.Controller
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetValues()
        {
            var result = _context.MyProperty.ToList();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Value(int id)
        {
            var value = _context.MyProperty.FirstOrDefault(x => x.Id == id);
            return Ok(value);
        }

    }
}
