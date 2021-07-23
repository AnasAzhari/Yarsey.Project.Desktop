using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.EntityFramework;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {

                var yarseyDbConnection = context.Configuration.GetSection("DBConnection");
 
                string connectionString = context.Configuration.GetConnectionString("sqlite");
             
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(yarseyDbConnection.Value);
                services.AddDbContext<YarseyDbContext>(configureDbContext);
                services.AddSingleton<YarseyDbContextFactory>(new YarseyDbContextFactory(configureDbContext));
            });

            return host;
        }
    }
}
