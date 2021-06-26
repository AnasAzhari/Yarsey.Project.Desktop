using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _previousVM?.Dispose();
                _previousVM = _currentViewModel;
                //_currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private ViewModelBase _previousVM;

        public ViewModelBase PreviousVm { get { return _previousVM; } }

        private ErrorMessageViewModel _errorMessageVM;

        private SuccessMessageViewModel _successMessageVM;



        public ModalNavigationStore()
        {


        }

        public bool IsOpen => CurrentViewModel != null;

        public event Action CurrentViewModelChanged;

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
