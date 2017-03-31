using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebcamWrapper;

namespace asp_arm.Models
{
    public class WebcamModel
    {
        static WebcamModel webcamModel = null;
        Webcam webcam;
        private WebcamModel()
        {
            initVideo();
        }
        public static WebcamModel GetWebcam()
        {
            if (webcamModel == null)
            {
                webcamModel = new WebcamModel();
            }
            return webcamModel;
        }
        private void initVideo()
        {
            webcam = new Webcam();
            webcam.Init();
        }

        public string GetImage()
        {
            return webcam.CapturePhoto().Result;
        }

    }
}
