using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Commands
{
    public class NavigateHomeCommand:CommandBase
    {

        private readonly NavigationStore _navigationStore;


        public NavigateHomeCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }


        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new HomeViewModel();
        }
    }
}
