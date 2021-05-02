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

                services.AddSingleton<HomeViewModel>(s => new HomeViewModel(s.GetRequiredService<NavigationStore>()));
                services.AddSingleton<CustomerViewModelV2>(s => new CustomerViewModelV2(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<ModalNavigationStore>(), s.GetRequiredService<CustomerDataService>()));
           
                services.AddTransient<INavigationService>(s => CreateHomeNavigationDrawerLayoutService(s)); // transient because wanna switch type of layout
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>(s => new MainWindow() { DataContext = s.GetRequiredService<MainViewModel>() });
                services.AddSingleton<CustomerView>();

            });

            return host;
        }

        public static INavigationService CreateHomeNavigationDrawerLayoutService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationDrawerService<HomeViewModel>(

                serviceProvider.GetRequiredService<NavigationStore>(),

                ()=>serviceProvider.GetRequiredService<HomeViewModel>()

            //new List<ViewModelBase>()
            //{
            //    serviceProvider.GetRequiredService<HomeViewModel>(),
            //    serviceProvider.GetRequiredService<CustomerViewModelV2>()

            //}


            );

        }
        public static INavigationService CreateCustomerNavigationDrawerLayoutService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationDrawerService<CustomerViewModelV2>(

                serviceProvider.GetRequiredService<NavigationStore>(),

                () => serviceProvider.GetRequiredService<CustomerViewModelV2>()

            //new List<ViewModelBase>()
            //{
            //    serviceProvider.GetRequiredService<HomeViewModel>(),
            //    serviceProvider.GetRequiredService<CustomerViewModelV2>()

            //}


            );

        }





    }
}
