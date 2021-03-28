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
    public static class AddViewModelsHostBuilderExtensions
    {
       
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
               // services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));
                services.AddSingleton<CloseNavigationService>();
                services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<NavigationStore>()));
                services.AddSingleton<CustomerViewModel>(s => new CustomerViewModel(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<ModalNavigationStore>(), s.GetRequiredService<CustomerDataService>()));
                services.AddTransient<NavigationBarViewModel>(CreateNavigationViewModel);
                services.AddSingleton<MainViewModel>();


                services.AddTransient<INavigationService>(s => CreateHomeNavigationService(s));

                services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
                services.AddSingleton<CustomerView>();
               
            });

            return host;
        }

        // create layout for HomeViewModel
        public static INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                    serviceProvider.GetRequiredService<NavigationStore>(),
                    () => serviceProvider.GetRequiredService<HomeViewModel>(),
                    () => serviceProvider.GetRequiredService<NavigationBarViewModel>()
                
                );
        }
        public static INavigationService CreateCustomerNavigation(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CustomerViewModel>(
                    serviceProvider.GetRequiredService<NavigationStore>(),
                    () => serviceProvider.GetRequiredService<CustomerViewModel>(),
                    () => serviceProvider.GetRequiredService<NavigationBarViewModel>()
                );
        }

        public static INavigationService CreateNewCustomerNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<CustomerDialogViewModel>(
                        serviceProvider.GetRequiredService<ModalNavigationStore>(),
                        () => serviceProvider.GetRequiredService<CustomerDialogViewModel>()
                );
        }

        public static NavigationBarViewModel CreateNavigationViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                    CreateHomeNavigationService(serviceProvider),
                    CreateCustomerNavigation(serviceProvider),
                    CreateNewCustomerNavigationService(serviceProvider)
                );

        }    


    }
}
