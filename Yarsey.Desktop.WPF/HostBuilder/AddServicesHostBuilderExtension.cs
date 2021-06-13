﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CustomerDataService>(s => new CustomerDataService(s.GetRequiredService<YarseyDbContextFactory>()));
                services.AddTransient<BusinessDataService>(s => new BusinessDataService(s.GetRequiredService<YarseyDbContextFactory>()));
                services.AddTransient<SaleDataService>(s => new SaleDataService(s.GetRequiredService<YarseyDbContextFactory>()));
            });

            return host;
        }
    }
}
