using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.Settings
{
    public class YarseySettings
    {

        public string DBConnection { get; set; }
        public bool LightTheme { get; set; }
        public string CurrentLocation { get; set; }
        public string[] DbLocations { get; set; }
        public string BackupLocation { get; set; }



    }
}
