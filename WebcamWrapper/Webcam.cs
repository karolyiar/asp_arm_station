using System;
using System.IO;
using System.Threading;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using System.Threading.Tasks;

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
        public string SavePhoto()
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
        public byte[] CapturePhoto()
        {
            try
            {
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();
                var stream = new InMemoryRandomAccessStream();
                // capture image to stream
                RunAsyncTask(mediaCapture.CapturePhotoToStreamAsync(imageProperties, stream));
                // save stream to byte[]
                var buffer = new Windows.Storage.Streams.Buffer((uint)stream.Size);
                stream.Seek(0);
                var buffer2 = RunAsyncTask(stream.ReadAsync(buffer, (uint)stream.Size, InputStreamOptions.None));
                var result = buffer2.ToArray(0, (int)stream.Size);
                return result;

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
        private TR RunAsyncTask<TR, TP>(IAsyncOperationWithProgress<TR, TP> task)
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
