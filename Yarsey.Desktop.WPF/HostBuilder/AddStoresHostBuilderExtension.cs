using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Stores;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddStoresHostBuilderExtension
    {

        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationDrawerStore>();



            });
            return host;
        }
    }
}
