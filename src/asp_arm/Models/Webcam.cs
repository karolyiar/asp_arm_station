using WebcamWrapper;

namespace asp_arm.Models
{
    public class WebcamModel
    {
        private static volatile WebcamModel webcamModel = null;
        Webcam webcam;
        private WebcamModel()
        {
            webcam = new Webcam();
            webcam.Init();
        }
        public static WebcamModel Instance
        {
            get
            {
                if (webcamModel == null)
                {
                    webcamModel = new WebcamModel();
                }
                return webcamModel;
            }
        }
        public string GetImage()
        {
            return webcam.CapturePhoto();
        }

    }
}
