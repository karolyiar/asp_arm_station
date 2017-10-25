using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace asp_arm.Controllers
{
    [Route("input")]
    public class InputController : Controller
    {
        InputModel input = InputModel.Instance;

        [HttpGet]
        public string Get()
        {
            return input.State;
        }
    }
}
