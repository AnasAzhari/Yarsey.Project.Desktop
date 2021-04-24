using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarsey.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using Yarsey.WPF.Stores;
using Yarsey.WPF.Services;
using Yarsey.WPF.ViewModels;
using Yarsey.EntityFramework.Services;
using Yarsey.WPF.Views;
using Yarsey.WPF.ViewModels.Modal;
namespace Yarsey.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensionsV2
    {

        public static IHostBuilder AddViewModelsV2(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                // services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
                services.AddSingleton<GeneralModalNavigationService>(s => new GeneralModalNavigationService(s.GetRequiredService<ModalNavigationStore>(), s.GetRequiredService<ErrorMessageViewModel>(), s.GetRequiredService<SuccessMessageViewModel>()));
                services.AddSingleton<CloseModalNavigationService>();
             



                services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
                services.AddSingleton<CustomerView>();

            });

            return host;
        }

        //public static INavigationService CreateNavigationDrawerLayoutService(IServiceProvider serviceProvider)
        //{

        //}

      
    }
}
