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

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {

        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<CustomerViewModel>();
                services.AddSingleton<HomeViewModel>();



                //services.AddSingleton<MainViewModel>(s => new MainViewModel(s.GetRequiredService<NavigationDrawerStore>(), new List<ViewModelBase>()
                //{
                //    s.GetRequiredService<HomeViewModel>(),
                //    s.GetRequiredService<CustomerViewModel>()

                //}));

                services.AddSingleton<MainViewModel>(s => new MainViewModel());
                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>())); ; ;
                services.AddSingleton<HomeView>();
                services.AddSingleton<CustomerView>(s=>new CustomerView() {DataContext= s.GetRequiredService<CustomerViewModel>() });
            });

            return host;
        }

        // create layout for HomeViewModel
   
      


    }
}
