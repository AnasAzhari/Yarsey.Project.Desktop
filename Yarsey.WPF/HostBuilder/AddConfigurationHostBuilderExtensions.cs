using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yarsey.WPF.HostBuilder
{
    public static class AddConfigurationHostBuilderExtensions
    {

        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("AppSettings.json");
                c.AddEnvironmentVariables();
            });

            return host;
        }

    }
}
