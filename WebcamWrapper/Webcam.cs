using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace WebcamWrapper
{
    public class Webcam
    {
        private MediaCapture mediaCapture;
        private MediaFrameReader _mediaFrameReader;
        private StorageFile photoFile;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";

        public async void Init()
        {
            try
            {
                mediaCapture = new MediaCapture();
                await mediaCapture.InitializeAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to initialize camera for audio/video mode: " + ex.Message);
            }
        }

        public async Task<string> CapturePhoto()
        {
            try
            {
                photoFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                    PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName);
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
                await mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile);
                return photoFile.Path;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not capture photo: " + ex.Message);
            }

        }
    }
}
