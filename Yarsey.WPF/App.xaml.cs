using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Yarsey.WPF.HostBuilder;
using Yarsey.EntityFramework;


namespace Yarsey.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddDbContext(); 
                
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            YarseyDbContextFactory contextFactory = _host.Services.GetRequiredService<YarseyDbContextFactory>();
            using (YarseyDbContext context=contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }


            base.OnStartup(e);
        }


    }
}
