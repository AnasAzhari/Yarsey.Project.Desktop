﻿using System;
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

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        public bool IsOpen => _modalNavigationStore.IsOpen;

        public ICommand NavigateCustomerCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        public MainViewModel(NavigationStore navigationStore,ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;


            //NavigateCustomerCommand = new NavigateCommand<CustomerViewModel>(navigationStore,()=> customerViewModel);
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => homeViewModel);
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

    }
}
