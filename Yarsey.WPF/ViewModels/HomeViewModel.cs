using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Stores;
using Yarsey.WPF.Commands;

namespace Yarsey.WPF.ViewModels
{
    public class HomeViewModel :ViewModelBase
    {

        public ICommand NavigateCustomerCommand { get; }
        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateCustomerCommand = new NavigateCustomerCommand(navigationStore);
        }

    }
}
