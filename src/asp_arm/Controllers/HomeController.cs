using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace asp_arm.Controllers
{
    public class HomeController : Controller
    {
        private static int imageId = 0;
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Image()
        {
            imageId = (imageId + 1) % 4;
            var path = "images\\dog" + imageId + ".jpg";
            return base.File(path, "image/jpeg");
        }

        public IActionResult Webcam()
        {
            WebcamModel webcam = WebcamModel.GetWebcam();
            string image = webcam.GetImage();
            return base.File(image, "image/jpeg");
        }
    }
}
