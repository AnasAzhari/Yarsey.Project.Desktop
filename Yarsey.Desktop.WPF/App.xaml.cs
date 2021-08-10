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
using Yarsey.EntityFramework.Seed;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;
using Yarsey.Domain.Services;

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
               .AddLoggingConfiguration()
               .AddDbContext()
               .AddServices()
               .AddStores()
               .AddViewModels();
               //.AddNavigationClickedAction();
            

        }
        protected override async void OnStartup(StartupEventArgs e)
        {

            _host.Start();
            Business business;
            YarseyDbContextFactory contextFactory = _host.Services.GetRequiredService<YarseyDbContextFactory>();
            using (YarseyDbContext context = contextFactory.CreateDbContext())
            {
                await context.Database.MigrateAsync();
                await Seed.SeedReference(context, new List<string>() { Helper.Helper.InvoiceModule, Helper.Helper.ReceiptModule });
                var biz=context.Businesses.FirstOrDefault();

                business = biz;
               
            }

            //BusinessSelection();
            if (business != null)
            {
                MainWindow = _host.Services.GetRequiredService<MainWindow>();
                MainWindow = _host.Services.GetRequiredService<BusinessSelectionPage>();
            }
            else
            {
                MainWindow = _host.Services.GetRequiredService<WindowSetupCompany>();
            }

            ConfigureMainWindow();
            ConfigureSetupWindow();
            ConfigureBusinessSelectionWindow();

            MainWindow.Show();
          
           

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            DeConfigureSetupWindow();
            DeConfigureBusinessSelectionWindow();
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

        private void ConfigureBusinessSelectionWindow()
        {
            var businessSelectionPage = _host.Services.GetRequiredService<BusinessSelectionViewModel>().BizSelectionPage;
            var createBusinessPage = (CreateBusinessPageModel)_host.Services.GetRequiredService<BusinessSelectionViewModel>().BusinessPage;

            businessSelectionPage.ChangeMainWindow += ChangeBusinessSelectionWindowtoMainwindow;
            createBusinessPage.ChangeMainWindow += ChangeBusinessSelectionWindowtoMainwindow;
        }

        private void DeConfigureBusinessSelectionWindow()
        {
            var businessSelectionPage = _host.Services.GetRequiredService<BusinessSelectionViewModel>().BizSelectionPage;
            var createBusinessPage = (CreateBusinessPageModel)_host.Services.GetRequiredService<BusinessSelectionViewModel>().BusinessPage;
            businessSelectionPage.ChangeMainWindow -= ChangeBusinessSelectionWindowtoMainwindow;
            createBusinessPage.ChangeMainWindow -= ChangeBusinessSelectionWindowtoMainwindow;
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

        public async void ChangeBusinessSelectionWindowtoMainwindow(Business business)
        {
            //var mainwindowsetup = _host.Services.GetRequiredService<MainViewModel>();
            //mainwindowsetup.Business = business;
            var id = business.Id;
            business = await _host.Services.GetRequiredService<IBusinessService>().Get(id);

            _host.Services.GetRequiredService<BusinessSelectionPage>().Hide();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            BusinessStore bizStore = _host.Services.GetRequiredService<BusinessStore>();
            bizStore.CurrentBusiness = business;

        }



        //default business selection. Assuming only 1 business. To be changed later
        private async void BusinessSelection()
        {
            Business biz = await _host.Services.GetRequiredService<IBusinessService>().GetDefault();
            BusinessStore bizStore = _host.Services.GetRequiredService<BusinessStore>();
            bizStore.CurrentBusiness = biz;
            
        }



    }
}
