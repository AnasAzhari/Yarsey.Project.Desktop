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

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationDrawerStore _navigationDrawerStore;

        public ViewModelBase CurrentNavigationDrawerContentViewModel => _navigationDrawerStore.CurrentContentViewModel;

        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateCustomerCommand { get; set; }

        public NavationItemClickedAction navationItemClickedAction { get; set; }


        public MainViewModel(

            NavigationDrawerStore navigationDrawerStore, 
            List<ViewModelBase> vMList,
            INavigationService homeNavigationService,
            INavigationService custNavService
            
            )
        {

            _navigationDrawerStore = navigationDrawerStore;

            _navigationDrawerStore.CurrentContentViewModelChanged += OnCurrentContentViewModel;
 
            NavigateHomeCommand = new NavigationDrawerCommand(homeNavigationService);
            NavigateCustomerCommand = new NavigationDrawerCommand(custNavService);

        }
        private void OnCurrentContentViewModel()
        {
            OnPropertyChanged(nameof(CurrentNavigationDrawerContentViewModel));
        }





    }
}
