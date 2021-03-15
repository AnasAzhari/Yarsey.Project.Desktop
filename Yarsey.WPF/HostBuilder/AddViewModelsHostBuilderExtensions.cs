using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarsey.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yarsey.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {

                //services.AddTransient(CreateHomeViewModel);
                //services.AddTransient<PortfolioViewModel>();
                //services.AddTransient<BuyViewModel>();
                //services.AddTransient<SellViewModel>();
                //services.AddTransient<AssetSummaryViewModel>();
                //services.AddTransient<MainViewModel>();

                //services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                //services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services => () => services.GetRequiredService<PortfolioViewModel>());
                //services.AddSingleton<CreateViewModel<BuyViewModel>>(services => () => services.GetRequiredService<BuyViewModel>());
                //services.AddSingleton<CreateViewModel<SellViewModel>>(services => () => services.GetRequiredService<SellViewModel>());
                //services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                //services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

                //services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();

                //services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                //services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                //services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
            });

            return host;
        }
    }
}
