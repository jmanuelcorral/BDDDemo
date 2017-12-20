using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BDDDemo.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        // GET api/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {

            return new string[] { "Comprar el Pan", "value2" };
        }

      
    }
}
