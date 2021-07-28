using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Services;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddLoggingHostBuilderExtension
    {
        public static IHostBuilder AddLoggingConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((host, loggerconfiguration) =>
            {

                loggerconfiguration.WriteTo.File(System.IO.Path.Combine(SettingsConfiguration.Folder,"YarseyLog.txt"), rollingInterval: RollingInterval.Month)
                    .MinimumLevel.Error()
                    .MinimumLevel.Override("Yarsey.Desktop.WPF.ViewModels", Serilog.Events.LogEventLevel.Information);

            });

            return host;

        }
    }
}
