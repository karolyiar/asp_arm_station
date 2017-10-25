using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;
using System.IO;

namespace asp_arm.Controllers
{
    [Route("waterlevel")]
    public class WaterLevelController : Controller
    {
        WaterLevel waterLevel = WaterLevel.Instance;

        [HttpGet]
        public IActionResult WebcamSaved()
        {
            string bowlLocation = string.Format("bowl/{0}.gif", waterLevel.Level);
            var bowl = new FileStream(bowlLocation,
                                   FileMode.Open,
                                   FileAccess.Read);
            return File(bowl, "image/gif");
        }
    }
}
