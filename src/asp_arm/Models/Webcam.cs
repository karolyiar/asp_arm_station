using WebcamWrapper;

namespace asp_arm.Models
{
    public class WebcamModel
    {
        private static volatile WebcamModel webcamModel = null;
        private Webcam webcam;
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
        public string GetSavedImage()
        {
            return webcam.SavePhoto();
        }
        public byte[] GetImage()
        {
            return webcam.CapturePhoto();
        }

    }
}
