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

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ICommand NavigateCustomerCommand { get; }
        public ICommand NavigateHomeCommand { get; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            NavigateCustomerCommand = new NavigateCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
