using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;

namespace ImageLibrary
{
    public class ImageStorage
    {
        public static BitmapImage CreateBitmapImage(Stream bitmapStream)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(bitmapStream);
            bitmapStream.Close();
            return bitmap;
        }
        public static BitmapImage CreateBitmapImage(string fileName)
        {
            return CreateBitmapImage(ReadPhoto(fileName));
        }

        #region Photo storage

        public static void SavePhoto(string fileName, BitmapImage image)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {

                if (myIsolatedStorage.FileExists(fileName))
                {
                    myIsolatedStorage.DeleteFile(fileName);
                }

                IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(fileName);

                WriteableBitmap wb = new WriteableBitmap(image);

                // Encode WriteableBitmap object to a JPEG stream.
                Extensions.SaveJpeg(wb, fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);

                //wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 85);
                fileStream.Close();

            }
        }

        public static void SavePhoto(string fileName, Stream photoStream)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(photoStream);
            SavePhoto(fileName, bitmap);
        }
        public static Stream ReadPhoto(string fileName)
        {
            Stream stream = null;
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {

                if (myIsolatedStorage.FileExists(fileName))
                {
                    stream = myIsolatedStorage.OpenFile(fileName, FileMode.Open);
                }

            }
            return stream;
        }
        #endregion

    }

}
