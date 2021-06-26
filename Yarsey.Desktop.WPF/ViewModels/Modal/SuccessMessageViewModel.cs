using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Stores;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class SuccessMessageViewModel:ViewModelBase,IDisposable
    {

        private string _successMessage;

        public string SuccessMessage
        {
            get { return _successMessage; }
            set { SetProperty(ref _successMessage, value); }
        }

        public ICommand ReturnCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private ModalNavigationStore _modalNavigationStore;

        public SuccessMessageViewModel(ModalNavigationStore store)
        {
            _modalNavigationStore = store;
            CloseCommand = new AsyncRelayCommand(CanClose, Close);
            ReturnCommand = new AsyncRelayCommand(CanReturn, Return);
        }
        private async Task<bool> CanClose()
        {
            return true;
        }

        private async Task<bool> CanReturn()
        {
            return true;
        }
        private async Task Close()
        {

            await Task.Run(() => { _modalNavigationStore.Close(); });
        }

        private async Task Return()
        {
            await Task.Run(() => { _modalNavigationStore.CurrentViewModel = _modalNavigationStore.PreviousVm; });
        }

    }
}
