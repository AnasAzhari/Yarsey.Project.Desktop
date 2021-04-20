using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Commands;
using Yarsey.WPF.Stores;

namespace Yarsey.WPF.ViewModels
{
    public class ErrorMessageViewModel:ViewModelBase
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public ICommand ReturnCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private ModalNavigationStore _modalNavigationStore;

        public ErrorMessageViewModel(ModalNavigationStore store)
        {
            _modalNavigationStore = store;
            CloseCommand = new AsyncRelayCommand(CanClose,Close);
            ReturnCommand = new AsyncRelayCommand(CanReturn,Return);
        }


        private async Task Close()
        {

            await Task.Run(() => { _modalNavigationStore.Close(); });
        }

        private async Task<bool> CanClose()
        {
            return true;
        }

        private async Task<bool> CanReturn()
        {
            return true;
        }

        private async Task Return()
        {
            await Task.Run(() => { _modalNavigationStore.CurrentViewModel=_modalNavigationStore.PreviousVm; });
        }


    }
}
