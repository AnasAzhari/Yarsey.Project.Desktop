using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private ViewModelBase _previousVM;

        private ViewModelBase _errorMessageVM;

        private ViewModelBase _successMessageVM;

        public ModalNavigationStore(ErrorMessageViewModel errorMessageViewModel,SuccessMessageViewModel successMessageViewModel)
        {
            _errorMessageVM=errorMessageViewModel;
            _successMessageVM = successMessageViewModel;

        }

        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;

        public void ShowErrorMessage(string error)
        {
            _previousVM = CurrentViewModel;
            CurrentViewModel = _errorMessageVM;
        }

        public void ShowSuccessMessage(string success)
        {
            _previousVM = CurrentViewModel;
            CurrentViewModel = _successMessageVM;
        }

        public void Close()
        {
            CurrentViewModel = null;
            _previousVM = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
