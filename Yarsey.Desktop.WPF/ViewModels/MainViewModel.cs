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

        public ViewModelBase CurrentNavigationDrawerContentViewModel => _navigationDrawerStore.CurrentContentViewModel;

        public Business Business { get { return _businessStore.CurrentBusiness; } set { _businessStore.CurrentBusiness = value; } }
        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateCustomerCommand { get; set; }
        public ICommand NavigateSalesCommand { get; set; }

        public NavationItemClickedAction navationItemClickedAction { get; set; }

        public ViewModelBase CurrentLayout { get; set; }


        public MainViewModel(

            NavigationDrawerStore navigationDrawerStore, 
            List<ViewModelBase> vMList,
            INavigationService homeNavigationService,
            INavigationService custNavService,
            INavigationService salesNavigationServvice,
            BusinessStore businessStore
            
            )
        {

            _navigationDrawerStore = navigationDrawerStore;
            this.salesNavigationServvice = salesNavigationServvice;
            this._businessStore = businessStore;
            this._businessStore.CurrentBusinessChanged += OnBusinessChanged;
            _navigationDrawerStore.CurrentContentViewModelChanged += OnCurrentContentViewModel;
 
            NavigateHomeCommand = new NavigationDrawerCommand(homeNavigationService);
            NavigateCustomerCommand = new NavigationDrawerCommand(custNavService);
            NavigateSalesCommand = new NavigationDrawerCommand(salesNavigationServvice);
        }
        private void OnCurrentContentViewModel()
        {
            OnPropertyChanged(nameof(CurrentNavigationDrawerContentViewModel));
        }

        private void OnBusinessChanged()
        {
            OnPropertyChanged(nameof(Business));
        }





    }
}
