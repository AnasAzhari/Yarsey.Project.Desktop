using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Yarsey.Desktop.WPF.HostBuilder;
using Yarsey.Desktop.WPF.Services;
using Yarsey.EntityFramework;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly IHost _host;

        public IHost CurrentHost { get { return _host; } }

        public App()
        {
            _host = CreateHostBuilder().Build();

        }
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
               .AddConfiguration()
               .AddDbContext()
               .AddStores()
               .AddViewModels()
               .AddNavigationClickedAction();
            

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            
            YarseyDbContextFactory contextFactory = _host.Services.GetRequiredService<YarseyDbContextFactory>();
            using (YarseyDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }



            //INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            //initialNavigationService.Navigate();

           
            //var homeVM = _host.Services.GetRequiredService<HomeViewModel>();
            //mainWindowVM.CurrentNavigationDrawerContentViewModel = homeVM;
            //var custvm = _host.Services.GetRequiredService<CustomerViewModel>();
            //custvm.VMString = "GOOGLE";
        
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            //MainWindow.BeginInit();
            MainWindow.Show();


            base.OnStartup(e);
        }


    }
}
