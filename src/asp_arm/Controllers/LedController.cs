﻿using System;
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
        LedModel led = LedModel.Instance;

        [HttpGet]
        public string Get()
        {  
            return led.State;
        }
        [HttpGet("{state:int}")]
        public string Get(int state)
        {
            led.State = (state == 1 ? "on" : "off");
            return led.State;
        }
    }
}
