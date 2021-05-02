using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF.Commands
{
    public class NavigateDrawerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly MainViewModel _mainViewModel;

        ViewModelBase _vm;

        public NavigateDrawerCommand(MainViewModel mainViewModel,ViewModelBase vm)
        {
            _mainViewModel = mainViewModel;
            _vm = vm;

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mainViewModel.CurrentNavigationDrawerContentViewModel = _vm;
        }
    }
}
