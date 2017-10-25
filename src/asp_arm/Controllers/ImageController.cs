using Microsoft.AspNetCore.Mvc;
using asp_arm.Models;
using System.IO;

namespace asp_arm.Controllers
{
    [Route("image")]
    public class ImageController : Controller
    {
        WebcamModel webcam = WebcamModel.Instance;

        [HttpGet("saved")]
        public IActionResult WebcamSaved()
        {
            string image = webcam.GetSavedImage();
            var fs = new FileStream(image,
                                   FileMode.Open,
                                   FileAccess.Read);
            return File(fs, "image/jpeg");
        }
        [HttpGet]
        public IActionResult Webcam()
        {
            var image = webcam.GetImage();
            return File(image, "image/jpeg");
        }
    }
}
