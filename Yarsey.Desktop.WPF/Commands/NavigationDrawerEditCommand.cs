using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Commands
{
    public class NavigationDrawerEditCommand : ICommand
    {
        private readonly INavigationService _navigationService;
        private readonly object obj;

        public NavigationDrawerEditCommand(INavigationService navigationService,object obj)
        {
            this._navigationService = navigationService;
            this.obj = obj;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.GetType() == typeof(Customer))
            {
                _navigationService.NavigateEditCustomer((Customer)parameter);
            }
           
        }


    }
}
