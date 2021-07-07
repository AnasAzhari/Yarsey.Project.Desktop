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
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;
using Yarsey.Desktop.WPF.View;

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
               .AddServices()
               .AddStores()
               .AddViewModels()
               .AddNavigationClickedAction();
            

        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            Business business;
            YarseyDbContextFactory contextFactory = _host.Services.GetRequiredService<YarseyDbContextFactory>();
            using (YarseyDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
                var biz=context.Businesses.FirstOrDefault();

                business = biz;
               
            }

            BusinessSelection();
            if (business != null)
            {
                MainWindow = _host.Services.GetRequiredService<MainWindow>();
            }
            else
            {
                MainWindow = _host.Services.GetRequiredService<WindowSetupCompany>();
            }


            
            ConfigureMainWindow();
            ConfigureSetupWindow();
          

            MainWindow.Show();
          
           

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            DeConfigureSetupWindow();
            _host.Services.GetRequiredService<BusinessStore>().ClearEvents();

            base.OnExit(e);
        }

        private void ConfigureMainWindow()
        {
            var homeVM = _host.Services.GetRequiredService<HomeViewModel>();
            var navDrawerStore = _host.Services.GetRequiredService<NavigationDrawerStore>();
            navDrawerStore.CurrentContentViewModel = homeVM;

            //var viewCust = _host.Services.GetRequiredService<CustomerView>();
            //var viewProduct = _host.Services.GetRequiredService<ProductView>();
        }

        private void ConfigureSetupWindow()
        {
            var createBusinessPage = (CreateBusinessPageModel)_host.Services.GetRequiredService<MainWindowSetupViewModel>().BusinessPage;
            createBusinessPage.ChangeMainWindow += ChangeSetupWindowtoMainwindow;
        }

        private void DeConfigureSetupWindow()
        {
            var createBusinessPage = (CreateBusinessPageModel)_host.Services.GetRequiredService<MainWindowSetupViewModel>().BusinessPage;
            createBusinessPage.ChangeMainWindow -= ChangeSetupWindowtoMainwindow;
        }

        public void ChangeSetupWindowtoMainwindow(Business business)
        {
            //var mainwindowsetup = _host.Services.GetRequiredService<MainViewModel>();
            //mainwindowsetup.Business = business;
            _host.Services.GetRequiredService<WindowSetupCompany>().Hide();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            BusinessStore bizStore = _host.Services.GetRequiredService<BusinessStore>();
            bizStore.CurrentBusiness = business;

        }

        //default business selection. Assuming only 1 business. To be changed later
        private async void BusinessSelection()
        {
            Business biz = await _host.Services.GetRequiredService<BusinessDataService>().GetDefault();
            BusinessStore bizStore = _host.Services.GetRequiredService<BusinessStore>();
            bizStore.CurrentBusiness = biz;
            
        }



    }
}
