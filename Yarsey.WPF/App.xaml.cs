﻿using System;
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
using Yarsey.WPF.ViewModels;
using Yarsey.WPF.Stores;
using Yarsey.WPF.Services;
using Yarsey.WPF.Views;
using Syncfusion.SfSkinManager;

namespace Yarsey.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public IHost CurrentHost { get { return _host; } }

        public App()
        {
            _host = CreateHostBuilder().Build();

        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                .AddStores()
                .AddServices()
                //.AddViewModels()
                .AddViewModelsV2()
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

            //MainWindow mainWindow= _host.Services.GetRequiredService<MainWindow>();
            //NavigationStore navStore = _host.Services.GetRequiredService<NavigationStore>();
            //navStore.CurrentViewModel = _host.Services.GetRequiredService<CustomerViewModel>();
            //mainWindow.Show();

            //HomeViewModel hvm = _host.Services.GetRequiredService<HomeViewModel>();
            //NavigationDrawerStore navigationDrawerStore = _host.Services.GetRequiredService<NavigationDrawerStore>();
            //navigationDrawerStore.CurrentContentViewModel = hvm;

            INavigationService initialNavigationService = _host.Services.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

           // ViewModelBase customerviewmodel = _host.Services.GetRequiredService<CustomerViewModel>();
            //var customerview = _host.Services.GetRequiredService<CustomerView>();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();


            base.OnStartup(e);
        }


    }
}
