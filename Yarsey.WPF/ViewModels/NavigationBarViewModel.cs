using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Commands;
using Yarsey.WPF.Services;

namespace Yarsey.WPF.ViewModels
{
    public class NavigationBarViewModel:ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateCustomerCommand { get; }

        public ICommand NavigateCustomerDialogCommand { get; }

        public NavigationBarViewModel(INavigationService homeNavigationService,INavigationService customerNavigationService,INavigationService customerModalNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateCustomerCommand = new NavigateCommand(customerNavigationService);
            NavigateCustomerDialogCommand = new NavigateCommand(customerModalNavigationService);
        }
    }
}
