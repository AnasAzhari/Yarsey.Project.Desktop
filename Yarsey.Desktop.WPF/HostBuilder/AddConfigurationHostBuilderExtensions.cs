using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Yarsey.Desktop.WPF.Settings;
using System.Text.Json;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Desktop.WPF.Helper;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                
               


                if (!System.IO.Directory.Exists(SettingsConfiguration.Folder))
                {
                    System.IO.Directory.CreateDirectory(SettingsConfiguration.Folder);
                }


                if (!File.Exists(SettingsConfiguration.FullSettingPath))
                {

                    YarseySettings yarseySettings = new YarseySettings()
                    {
                        DBConnection = $"Data Source={SettingsConfiguration.DbConnection}",
                        BackupLocation = SettingsConfiguration.backupLocation,
                        LightTheme = true,

                    };
                    var settingsText = JsonSerializer.Serialize<YarseySettings>(yarseySettings);
                    File.WriteAllText(SettingsConfiguration.FullSettingPath, settingsText);

                }
                

                c.AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);
                c.AddJsonFile(SettingsConfiguration.FullSettingPath, optional: false, reloadOnChange: true);
                c.AddEnvironmentVariables();
                
                
            });

            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<SettingsConfiguration>();
                services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            });



            return host;
        }

    }
}
