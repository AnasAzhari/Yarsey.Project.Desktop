using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Converter
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            if (value == null)
                return null;
            if ((InvoiceStatus)value == InvoiceStatus.Paid)
                return new SolidColorBrush(Colors.Green);
            else if ((InvoiceStatus)value == InvoiceStatus.Unpaid)
                return new SolidColorBrush(Colors.Red);

            return new SolidColorBrush(Colors.Yellow);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
