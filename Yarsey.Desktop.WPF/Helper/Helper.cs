using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using System.IO;

namespace Yarsey.Desktop.WPF.Helper
{
    public static class Helper
    {


        public static BitmapSource ImageResizer(string loc,int maxheight)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(loc));
            var ar = (float)bitmapImage.Width / (float)bitmapImage.Height;

            //maximise height

            int width;

            if (bitmapImage.Width > bitmapImage.Height)
            {
                 width = (int)(ar * maxheight);

            }else if (bitmapImage.Width < bitmapImage.Height)
            {
                 width= (int)(ar * maxheight);

            }
            else 
            {
                width = maxheight;
            }
            

           var targetBitmap = new TransformedBitmap();
            targetBitmap.BeginInit();
            targetBitmap.Source = bitmapImage;
            targetBitmap.Transform = new ScaleTransform((double)((double)width /(double)bitmapImage.Width), (double)((double)maxheight / (double)bitmapImage.Height));
            targetBitmap.EndInit();
           
            return targetBitmap;

        }
        //public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        imageIn.Save(ms, imageIn.RawFormat);
        //        return  ms.ToArray();
        //    }
        //}
        public static byte[] ImageToByte(BitmapImage imageSource)
        {
            Stream stream = imageSource.StreamSource;
            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }

    }
}
