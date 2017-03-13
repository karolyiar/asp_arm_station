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
            LedModell led = LedModell.getLed();
            Console.WriteLine("a");
            led.SetState(state==1?"on":"off");
            Console.WriteLine("b");
            return led.GetState();
            return "todo";
        }
        [HttpGet]
        public string Get()
        {
            LedModell led = LedModell.getLed();
            return led.GetState();
            return "todo";
        }
    }
}
