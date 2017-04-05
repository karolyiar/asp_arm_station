using System;
using System.Threading;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace WebcamWrapper
{
    public class Webcam
    {
        private MediaCapture mediaCapture;
        private StorageFile photoFile;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";


        public void Init()
        {
            try
            {
                mediaCapture = new MediaCapture();
                RunAsyncTask(mediaCapture.InitializeAsync());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to initialize camera for audio/video mode: " + ex.Message);
            }
        }
        public string CapturePhoto()
        {
            try
            {
                photoFile = RunAsyncTask(
                    KnownFolders.PicturesLibrary.CreateFileAsync(
                    PHOTO_FILE_NAME, CreationCollisionOption.GenerateUniqueName)
                    );
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
                RunAsyncTask(mediaCapture.CapturePhotoToStorageFileAsync(imageProperties, photoFile));
                return photoFile.Path;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Can not capture photo: " + ex.Message);
            }

        }

        /*
         * Wrapper for Windows Universal App aync funtions
         */
        private void RunAsyncTask(IAsyncAction task)
        {
            AutoResetEvent e = new AutoResetEvent(false);
            task.Completed += (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus != AsyncStatus.Completed)
                    throw new InvalidOperationException("Async action error: " + asyncStatus);
                e.Set();
            };
            e.WaitOne();
        }
        private T RunAsyncTask<T>(IAsyncOperation<T> task)
        {
            AutoResetEvent e = new AutoResetEvent(false);
            task.Completed += (asyncInfo, asyncStatus) =>
            {
                if (asyncStatus != AsyncStatus.Completed)
                    throw new InvalidOperationException("Async operation error: " + asyncStatus);
                e.Set();
            };
            e.WaitOne();
            return task.GetResults();
        }
    }
}
