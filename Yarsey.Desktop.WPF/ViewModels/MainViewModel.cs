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
        public ICommand NavigateSettingsCommand { get; set; }

        public ICommand NavigateInvoiceCommand { get; set; }

        private bool _isBusy=false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { 
                SetProperty(ref _isBusy, value); }
        }
        public bool IsOpen => _modalNavigationStore.IsOpen;
        //public NavationItemClickedAction navationItemClickedAction { get; set; }

        public ViewModelBase CurrentLayout { get; set; }


        public MainViewModel(

            NavigationDrawerStore navigationDrawerStore, 
            List<ViewModelBase> vMList,
            INavigationService homeNavigationService,
            INavigationService custNavService,
      
            INavigationService productNavigationServvice,
            INavigationService invoiceNavigationService,
            INavigationService settingsNavigationService,
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
            NavigateSettingsCommand = new NavigationDrawerCommand(settingsNavigationService);
            NavigateInvoiceCommand = new NavigationDrawerCommand(invoiceNavigationService);
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }
        private async void OnCurrentContentViewModel()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(CurrentNavigationDrawerContentViewModel));
            GC.Collect();
           
            IsBusy = false;
        }

        private void OnBusinessChanged()
        {
            OnPropertyChanged(nameof(Business));
            GC.Collect();
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
            GC.Collect();
            
        }





    }
}
