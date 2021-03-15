using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Commands
{
    public class NavigateCustomerCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateCustomerCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new CustomerViewModel(_navigationStore);
        }
    }
}
