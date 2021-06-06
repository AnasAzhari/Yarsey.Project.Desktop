using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Yarsey.Desktop.WPF.Validation
{
    public class ImageValidationRule : ValidationRule
    {
        public ImageValidationRule()
        {

        }
        public int MaxSize { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {


            string filelocation = (string)value;
            var fileLength = new FileInfo(filelocation).Length;
            
            //BitmapImage bmp = new BitmapImage();
            //bmp.BeginInit();
            //bmp.UriSource = new Uri(filelocation);
            //bmp.EndInit();


            Bitmap bitmap = new Bitmap(filelocation);
            


            if (!File.Exists(filelocation))
            {
                return new ValidationResult(false, $"File does not exist");
            }

            if (fileLength >= MaxSize)
            {
                return new ValidationResult(false, "File size is bigger than 5MB");
            }


            return new ValidationResult(false, "salah salah salah");

        }
    }
}
