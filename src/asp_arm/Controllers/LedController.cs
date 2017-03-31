using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace asp_arm.Controllers
{
    [Route("led")]
    public class LedController : Controller
    {
        // GET: /led/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{state:int}")]
        public string Get(int state)
        {
            LedModel led = LedModel.Instance;
            Console.WriteLine("a");
            led.State = (state==1?"on":"off");
            Console.WriteLine("b");
            return led.State;
        }
        [HttpGet]
        public string Get()
        {
            LedModel led = LedModel.Instance;
            return led.State;
        }
    }
}
