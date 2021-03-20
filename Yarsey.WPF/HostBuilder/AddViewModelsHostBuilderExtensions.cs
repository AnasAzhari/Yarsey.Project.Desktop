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

namespace Yarsey.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<NavigationStore>()));
                services.AddSingleton<CustomerViewModel>(s => new CustomerViewModel(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<CustomerDataService>()));
                services.AddSingleton<MainViewModel>(s => new MainViewModel(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<CustomerViewModel>(), s.GetRequiredService<HomeViewModel>()));
                services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });

               
            });

            return host;
        }
    }
}
