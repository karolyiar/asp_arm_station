using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;
using System.IO;

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

        public IActionResult WebcamSaved()
        {
            WebcamModel webcam = WebcamModel.Instance;
            string image = webcam.GetSavedImage();
            FileStream fs = new FileStream(image,
                                   FileMode.Open,
                                   FileAccess.Read);
            return base.File(fs, "image/jpeg");
        }
        public IActionResult Webcam()
        {
            WebcamModel webcam = WebcamModel.Instance;
            var image = webcam.GetImage();
            return base.File(image, "image/jpeg");
        }
    }
}
