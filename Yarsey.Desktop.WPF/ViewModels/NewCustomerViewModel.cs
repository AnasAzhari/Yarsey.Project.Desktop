using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewCustomerViewModel:ViewModelBase
    {
        public ICommand NavigateCustomerCommand { get; set; }
        public NewCustomerViewModel(INavigationService navigationService)
        {
            NavigateCustomerCommand = new NavigationDrawerCommand(navigationService);
        }
    }
}
