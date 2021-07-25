using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Settings;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Services
{
    public class SettingsConfiguration
    {

        public static string baseLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string settingsJson = "Settings.json";
        public static string Folder = System.IO.Path.Combine(baseLocation, "Yarsey");
        public static string FullSettingPath { get { return System.IO.Path.Combine(Folder, settingsJson); } }
        public static string DbConnection = System.IO.Path.Combine(Folder, "Yarsey.db");

        public static string backupLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Yarsey", "YarseyBackup");

        public static string FileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Yarsey");



        public YarseySettings CurrentSettings { get; set; }


        public Action CurrentSettingsChanged;

        public SettingsConfiguration()
        {
            ReadSettingsFromJson();
        }

        public void ReadSettingsFromConfigurationManager()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Console.WriteLine($"App Data Folder Path : {path}");
        }

        public void ReadSettingsFromJson()
        {
            var data = System.IO.File.ReadAllText(FullSettingPath);
            var setting = JsonSerializer.Deserialize<YarseySettings>(data);
            CurrentSettings = setting;
            //CurrentSettingsChanged();
        }

        public void WriteSettingsToJson()
        {
            var settingsText = JsonSerializer.Serialize<YarseySettings>(CurrentSettings);
            File.WriteAllText(SettingsConfiguration.FullSettingPath, settingsText);
        }

        public static string  GetInvoiceFolder(Business business){

            if (!Directory.Exists(FileFolder))
            {
                Directory.CreateDirectory(FileFolder);
            }

            string invoiceFolder = Path.Combine(FileFolder, business.BusinessName, "Invoice");

            if (!Directory.Exists(invoiceFolder)){
                Directory.CreateDirectory(invoiceFolder);
            }
            return invoiceFolder;

         }



    }
}
