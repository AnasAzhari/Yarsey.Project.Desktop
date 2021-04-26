using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using System.Windows.Input;
using Yarsey.WPF.Commands;

namespace Yarsey.WPF.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly NavigationDrawerStore _navigationDrawerStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentLayoutViewModel;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        public ViewModelBase CurrentNavigationDrawerContentViewModel => _navigationDrawerStore.CurrentContentViewModel;

        public bool IsOpen => _modalNavigationStore.IsOpen;

        public ICommand NavigateCustomerCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        public MainViewModel(NavigationStore navigationStore,ModalNavigationStore modalNavigationStore,NavigationDrawerStore navigationDrawerStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _navigationDrawerStore = navigationDrawerStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
            _navigationDrawerStore.CurrentContentViewModelChanged += OnCurrentNavigationContentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private void OnCurrentNavigationContentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentNavigationDrawerContentViewModel));
        }

    }
}
