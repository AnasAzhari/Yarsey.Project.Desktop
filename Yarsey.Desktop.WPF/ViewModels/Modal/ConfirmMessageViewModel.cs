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
    public class ConfirmMessageViewModel : ViewModelBase,IDisposable
    {

        private string _confirmMessage;
        private readonly ModalNavigationStore _modalNavigationStore;

        public ICommand ConfirmCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Func<Task> ConfirmTask { get; set; }

        public string ConfirmMessage
        {
            get { return _confirmMessage; }
            set { SetProperty(ref _confirmMessage, value); }
        }

        public ConfirmMessageViewModel(ModalNavigationStore modalNavigationStore)
        {
            this._modalNavigationStore = modalNavigationStore;
            this.CancelCommand = new AsyncRelayCommand(CanClose, Close);
            this.ConfirmCommand = new AsyncRelayCommand(CanClose, ConfirmationTask);
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
        private async Task ConfirmationTask()
        {
            await ConfirmTask();
            _modalNavigationStore.Close();

        }

        public override void Dispose()
        {
            ConfirmTask = null;
        }

    }
}
