using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF.Services
{
   public class GeneralModalNavigationService : INavigationService
    {
        private ModalNavigationStore _modaNavigationStore;
        private ErrorMessageViewModel _errorMessageViewModel;
        private SuccessMessageViewModel _successMessageViewModel;

        public GeneralModalNavigationService(ModalNavigationStore modalNavigationStore, ErrorMessageViewModel errorMessageViewModel, SuccessMessageViewModel successMessageViewModel)
        {
            _modaNavigationStore = modalNavigationStore;
            _errorMessageViewModel = errorMessageViewModel;
            _successMessageViewModel = successMessageViewModel;
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
    }
}
