using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Services;

namespace Yarsey.Desktop.WPF.Commands
{
    public class NavigationDrawerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigationService _navigationService;

        public NavigationDrawerCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
