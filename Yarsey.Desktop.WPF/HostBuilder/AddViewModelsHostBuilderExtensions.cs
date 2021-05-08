using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Yarsey.Desktop.WPF.ViewModels;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Desktop.WPF.View;
using Yarsey.Desktop.WPF.Services;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {

        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<CustomerViewModel>(s=>new CustomerViewModel(CreateNewCustomerNavigationDraweService(s),s.GetRequiredService<CustomerDataService>()));
                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<MainViewModel>(s => new MainViewModel(
                    
                    s.GetRequiredService<NavigationDrawerStore>(), new List<ViewModelBase>()
                    {
                    s.GetRequiredService<HomeViewModel>(),
                    s.GetRequiredService<CustomerViewModel>(),
                  
                    },
                    CreateHomeNavigationDrawerService(s),
                    CreateCustomerNavigationDrawerService(s)


                ));

                services.AddSingleton<WindowSetupCompany>(s => new WindowSetupCompany());

                //services.AddSingleton<MainViewModel>(s => new MainViewModel());
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>())); ; 
                services.AddSingleton<HomeView>();
                services.AddSingleton<CustomerView>(s=>new CustomerView() {DataContext= s.GetRequiredService<CustomerViewModel>() });
                services.AddTransient<NewCustomerViewModel>(s => new NewCustomerViewModel(CreateCustomerNavigationDrawerService(s),s.GetRequiredService<CustomerDataService>()));
            });

            return host;
        }

        #region Services

        public static INavigationService CreateHomeNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<HomeViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<HomeViewModel>()

            );
        }

        public static INavigationService CreateCustomerNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<CustomerViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<CustomerViewModel>()

            );

        }
        public static INavigationService CreateNewCustomerNavigationDraweService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<NewCustomerViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<NewCustomerViewModel>()

            );

        }



        #endregion
    }
}
