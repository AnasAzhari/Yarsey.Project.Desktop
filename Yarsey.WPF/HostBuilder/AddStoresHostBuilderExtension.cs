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
          
                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<ErrorMessageViewModel>(s => new ErrorMessageViewModel(s.GetRequiredService<ModalNavigationStore>()));
                services.AddSingleton<SuccessMessageViewModel>(s => new SuccessMessageViewModel(s.GetRequiredService<ModalNavigationStore>()));

              
            });

            return host;
        }
    }
}
