using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;
using System.IO;

namespace asp_arm.Controllers
{
    [Route("image")]
    public class ImageController : Controller
    {
        [HttpGet("saved")]
        public IActionResult WebcamSaved()
        {
            WebcamModel webcam = WebcamModel.Instance;
            string image = webcam.GetSavedImage();
            var fs = new FileStream(image,
                                   FileMode.Open,
                                   FileAccess.Read);
            return File(fs, "image/jpeg");
        }
        [HttpGet]
        public IActionResult Webcam()
        {
            WebcamModel webcam = WebcamModel.Instance;
            var image = webcam.GetImage();
            return File(image, "image/jpeg");
        }
    }
}
