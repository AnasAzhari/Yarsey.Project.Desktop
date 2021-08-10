using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF.Services
{
   public class GeneralModalNavigationService : IModalNavigationService
    {
        private ModalNavigationStore _modaNavigationStore;
        private ErrorMessageViewModel _errorMessageViewModel;
        private SuccessMessageViewModel _successMessageViewModel;
        private readonly ConfirmMessageViewModel _confirmMessageViewModel;

        public GeneralModalNavigationService(ModalNavigationStore modalNavigationStore, ErrorMessageViewModel errorMessageViewModel, SuccessMessageViewModel successMessageViewModel,ConfirmMessageViewModel confirmMessageViewModel)
        {
            this._modaNavigationStore = modalNavigationStore;
            this._errorMessageViewModel = errorMessageViewModel;
            this._successMessageViewModel = successMessageViewModel;
            this._confirmMessageViewModel = confirmMessageViewModel;
        }

        public void Navigate()
        {
          
        }
        public void NavigateOnEception(string message)
        {
            _modaNavigationStore.CurrentViewModel = _errorMessageViewModel;
            _errorMessageViewModel.ErrorMessage = message;
        }

        public void NavigationOnSuccess(string message)
        {
            _modaNavigationStore.CurrentViewModel = _successMessageViewModel;
            _successMessageViewModel.SuccessMessage = message;
        }
        public async void NavigationOnConfirmDelete(string message,Func<Task> task)
        {
            _modaNavigationStore.CurrentViewModel = _confirmMessageViewModel;
            _confirmMessageViewModel.ConfirmMessage = message;
            _confirmMessageViewModel.ConfirmTask = task;
            
        }

        public async void NavigationOnConfirmBackup(string message, Func<Task> task)
        {
            _modaNavigationStore.CurrentViewModel = _confirmMessageViewModel;
            _confirmMessageViewModel.ConfirmMessage = message;
            _confirmMessageViewModel.ConfirmTask = task;

        }


    }
}
