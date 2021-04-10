using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarsey.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.HostBuilder
{
   public static class AddStoresHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {

                services.AddSingleton<NavigationStore>();
                services.AddSingleton<ErrorMessageViewModel>();
                services.AddSingleton<SuccessMessageViewModel>();
                services.AddSingleton<ModalNavigationStore>(s=>new ModalNavigationStore(s.GetRequiredService<ErrorMessageViewModel>(),s.GetRequiredService<SuccessMessageViewModel>()));

            });

            return host;
        }
    }
}
