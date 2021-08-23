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
using Yarsey.EntityFramework;

namespace Yarsey.Desktop.WPF.Helper
{
    public static class Helper
    {
        #region CONSTANT
        // DONT CHANGE THIS SECTION !!!!!
        public const string InvoiceModule = "Invoice"; // refer businessdataservice GenerateDefaultRunningNumbers(int bizId)
        public const string ReceiptModule = "Receipt";
        #endregion
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
        public static byte[] FileToByteArray(string fileName)
        {
            byte[] fileData = null;
             
            using (FileStream fs = File.OpenRead(fileName))
            {
                var binaryReader = new BinaryReader(fs);
                fileData = binaryReader.ReadBytes((int)fs.Length);
            }
            return fileData;
        }

        public static BitmapImage BlobToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        //public static string RunnningNo(string module)
        //{
        //  //    $newNumber = intval($currentNumber->dat_remark) + 1;
        //  //$updateNumber = MdtDataParameter::where('dat_name', '=',$key)->update([
        //  //      'dat_remark' => str_pad($newNumber, 8, "0", STR_PAD_LEFT)
        //  //]);
        //  //  return $currentNumber->dat_description.date("Y").str_pad($newNumber, 8, "0", STR_PAD_LEFT);

        //}


    }
}
