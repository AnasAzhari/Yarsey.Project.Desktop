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
    public class CustomerViewModel:ViewModelBase
    {
      
        //public ICommand NavigateHomeCommand { get; }
        public CustomerViewModel(NavigationStore navigationStore)
        {
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }
    }
}
