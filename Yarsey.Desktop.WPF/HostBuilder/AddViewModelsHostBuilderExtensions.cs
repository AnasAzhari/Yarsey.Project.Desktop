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
using Yarsey.Desktop.WPF.Services;
using Yarsey.EntityFramework.Services;
using AutoMapper;
using Serilog;
using Microsoft.Extensions.Logging;
using Yarsey.Domain.Services;

namespace Yarsey.Desktop.WPF.HostBuilder
{
    public static class AddViewModelsHostBuilderExtensions
    {

        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((context,services) =>
            {

                services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>(), s.GetRequiredService<SettingsConfiguration>()));
                services.AddSingleton<HomeView>();

                #region Customer

                //services.AddTransient<CustomerView>(s => new CustomerView() { DataContext = s.GetRequiredService<CustomerViewModel>() });

                services.AddTransient<CustomerViewModel>(s=>new CustomerViewModel(
                                                             CreateNewCustomerNavigationDraweService(s), 
                                                             EditCustomerNavigationDrawerService(s),
                                                             s.GetRequiredService<CustomerDataService>(),
                                                             s.GetRequiredService<BusinessStore>(),
                                                             s.GetRequiredService<IBusinessService>(),
                                                             s.GetRequiredService<GeneralModalNavigationService>()

                    ));
                services.AddTransient<NewCustomerViewModel>(s => new NewCustomerViewModel(CreateCustomerNavigationDrawerService(s), 
                                                                    s.GetRequiredService<CustomerDataService>(),
                                                                    s.GetRequiredService<BusinessStore>(), 
                                                                    s.GetRequiredService<IBusinessService>(), 
                                                                    s.GetRequiredService<GeneralModalNavigationService>(),
                                                                    s.GetRequiredService<IMapper>()
                                                                    
                                                                    
                                                                    ));
                services.AddTransient<EditCustomerViewModel>(s => new EditCustomerViewModel(CreateCustomerNavigationDrawerService(s),
                                                                    s.GetRequiredService<BusinessStore>(),
                                                                    s.GetRequiredService<IBusinessService>(),
                                                                    s.GetRequiredService<GeneralModalNavigationService>()));


                #endregion

                #region Product
                services.AddTransient<ProductViewModel>(s => new ProductViewModel(CreateNewProductNavigationDraweService(s),
                                                                                s.GetRequiredService<BusinessStore>(),
                                                                                s.GetRequiredService<IBusinessService>(),
                                                                                s.GetRequiredService<GeneralModalNavigationService>()
                                                        ));

                //services.AddSingleton<ProductView>(services => new ProductView() { DataContext = services.GetRequiredService<ProductViewModel>() });


                services.AddTransient<NewProductViewModel>(s => new NewProductViewModel(CreateProductNavigationDrawerService(s), 
                                                                    s.GetRequiredService<BusinessStore>(), 
                                                                    s.GetRequiredService<IBusinessService>()));

                #endregion

                #region Settings

                services.AddSingleton<SettingsViewModel>(s => new SettingsViewModel(s.GetRequiredService<MainWindow>(),s.GetRequiredService<SettingsConfiguration>(),s.GetRequiredService<GeneralModalNavigationService>()));
                services.AddSingleton<SettingsView>(s => new SettingsView() { DataContext = s.GetRequiredService<SettingsViewModel>() });

                #endregion

                #region Invoice 

                services.AddTransient<InvoiceViewModel>(s => new InvoiceViewModel(CreateNewInvoiceNavigationDraweService(s),
                                                                      s.GetRequiredService<BusinessStore>(),
                                                                      s.GetRequiredService<IBusinessService>(),
                                                                      s.GetRequiredService<GeneralModalNavigationService>()
                                                        ));
                services.AddTransient<NewInvoiceViewModel>(s => new NewInvoiceViewModel(
                                                                      s.GetRequiredService<ILogger<NewInvoiceViewModel>>(),
                                                                        CreateInvoiceNavigationDrawerService(s),
                                                                      s.GetRequiredService<BusinessStore>(),
                                                                      s.GetRequiredService<IBusinessService>(),
                                                                      s.GetRequiredService<InvoiceDataService>(),
                                                                      s.GetRequiredService<GeneralModalNavigationService>(),
                                                                      s.GetRequiredService<PdfService>()
                                                        ));

                #endregion

                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<MainViewModel>(s => new MainViewModel(
                    
                    s.GetRequiredService<NavigationDrawerStore>(),

                    new List<ViewModelBase>()
                    {
                        s.GetRequiredService<HomeViewModel>(),
                        s.GetRequiredService<CustomerViewModel>(),
                        s.GetRequiredService<ProductViewModel>()
                  
                    },
                    CreateHomeNavigationDrawerService(s),
                    CreateCustomerNavigationDrawerService(s),
                    CreateProductNavigationDrawerService(s),
                    CreateInvoiceNavigationDrawerService(s),
                    CreateSettingsNavigationDrawerService(s),
                    s.GetRequiredService<BusinessStore>(),
                    s.GetRequiredService<ModalNavigationStore>()

                ));
                services.AddSingleton<MainWindowSetupViewModel>(s=>new MainWindowSetupViewModel(s.GetRequiredService<IBusinessService>()));
                services.AddSingleton<WindowSetupCompany>(s => new WindowSetupCompany(s.GetRequiredService<SettingsConfiguration>()) { DataContext=s.GetRequiredService<MainWindowSetupViewModel>()});

                services.AddSingleton<BusinessSelectionViewModel>();
                services.AddSingleton<BusinessSelectionPage>(s=>new BusinessSelectionPage(s.GetRequiredService<SettingsConfiguration>()) {DataContext = s.GetRequiredService<BusinessSelectionViewModel>() });

                //services.AddSingleton<MainViewModel>(s => new MainViewModel());

                #region General Modal Navigation Service
                services.AddSingleton<ErrorMessageViewModel>();
                services.AddSingleton<SuccessMessageViewModel>();
                services.AddSingleton<ConfirmMessageViewModel>();
                services.AddSingleton<GeneralModalNavigationService>(s => new GeneralModalNavigationService(s.GetRequiredService<ModalNavigationStore>(), s.GetRequiredService<ErrorMessageViewModel>(), s.GetRequiredService<SuccessMessageViewModel>(),s.GetRequiredService<ConfirmMessageViewModel>()));
                #endregion

            });

            return host;
        }

        #region Services

        public static INavigationService CreateHomeNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<HomeViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<HomeViewModel>()

            );
        }

        public static INavigationService CreateCustomerNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<CustomerViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<CustomerViewModel>()

            );

        }
     
        public static INavigationService CreateProductNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<ProductViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<ProductViewModel>()

            );

        }
        public static INavigationService CreateInvoiceNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<InvoiceViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<InvoiceViewModel>()

            );

        }
        public static INavigationService CreateNewCustomerNavigationDraweService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<NewCustomerViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<NewCustomerViewModel>()

            );

        }
        public static INavigationService EditCustomerNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<EditCustomerViewModel>(
                    serviceProvider.GetRequiredService<NavigationDrawerStore>(),
                    ()=>serviceProvider.GetRequiredService<EditCustomerViewModel>()

                );
        }
      
        public static INavigationService CreateNewProductNavigationDraweService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<NewProductViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<NewProductViewModel>()

            );

        }
        public static INavigationService CreateNewInvoiceNavigationDraweService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<NewInvoiceViewModel>(

                serviceProvider.GetRequiredService<NavigationDrawerStore>(),

                () => serviceProvider.GetRequiredService<NewInvoiceViewModel>()

            );

        }

        public static INavigationService CreateSettingsNavigationDrawerService(IServiceProvider serviceProvider)
        {
            return new NavigationDrawerService<SettingsViewModel>(
                    serviceProvider.GetRequiredService<NavigationDrawerStore>(),
                    () => serviceProvider.GetRequiredService<SettingsViewModel>()

                );
        }


        #endregion
    }
}
