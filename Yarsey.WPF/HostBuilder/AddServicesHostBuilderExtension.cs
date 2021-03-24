using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarsey.EntityFramework;
using Yarsey.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Yarsey.WPF.Stores;
using Yarsey.Domain.Services;
using Yarsey.WPF.Services;

namespace Yarsey.WPF.HostBuilder
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<CustomerDataService>(s => new CustomerDataService(s.GetRequiredService<YarseyDbContextFactory>()));


            });

            return host;
        }

    
    }
}
