using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Yarsey.Desktop.WPF.Behaviour;
using Yarsey.Desktop.WPF.View;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddNavigationClickedActionHostBuilderExtension
    {

        public static IHostBuilder AddNavigationClickedAction(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                //services.AddSingleton<NavationItemClickedAction>(s=>new NavationItemClickedAction(new List<UserControl>()
                //{
                //    s.GetRequiredService<HomeView>(),
                //    s.GetRequiredService<CustomerView>()

                //}));
                services.AddSingleton<NavationItemClickedAction>();
            });

            return host;
        }

    }
}
