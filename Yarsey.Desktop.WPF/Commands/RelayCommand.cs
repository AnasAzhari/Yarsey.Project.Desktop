using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yarsey.Desktop.WPF.Commands
{
    public sealed class RelayBasicCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayBasicCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();

        }
    }
}
