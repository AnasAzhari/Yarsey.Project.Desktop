using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Behaviour;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationDrawerStore _navigationDrawerStore;
        private readonly INavigationService salesNavigationServvice;
        private readonly BusinessStore _businessStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentNavigationDrawerContentViewModel => _navigationDrawerStore.CurrentContentViewModel;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        public Business Business { get { return _businessStore.CurrentBusiness; } set { _businessStore.CurrentBusiness = value; } }
        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateCustomerCommand { get; set; }
        public ICommand NavigateSalesCommand { get; set; }
        public ICommand NavigateProductCommand { get; set; }

        public bool IsOpen => _modalNavigationStore.IsOpen;
        public NavationItemClickedAction navationItemClickedAction { get; set; }

        public ViewModelBase CurrentLayout { get; set; }


        public MainViewModel(

            NavigationDrawerStore navigationDrawerStore, 
            List<ViewModelBase> vMList,
            INavigationService homeNavigationService,
            INavigationService custNavService,
      
            INavigationService productNavigationServvice,
            BusinessStore businessStore,
            ModalNavigationStore modalNavigationStore

            )
        {

            _navigationDrawerStore = navigationDrawerStore;
          
            this._businessStore = businessStore;
            this._modalNavigationStore = modalNavigationStore;
            this._businessStore.CurrentBusinessChanged += OnBusinessChanged;
            _navigationDrawerStore.CurrentContentViewModelChanged += OnCurrentContentViewModel;
 
            NavigateHomeCommand = new NavigationDrawerCommand(homeNavigationService);
            NavigateCustomerCommand = new NavigationDrawerCommand(custNavService);
           
            NavigateProductCommand = new NavigationDrawerCommand(productNavigationServvice);
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }
        private void OnCurrentContentViewModel()
        {
            OnPropertyChanged(nameof(CurrentNavigationDrawerContentViewModel));
        }

        private void OnBusinessChanged()
        {
            OnPropertyChanged(nameof(Business));
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }





    }
}
