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

        private ViewModelBase _currentNavigationVM;
        public ViewModelBase CurrentNavigationDrawerContentViewModel { get { return _currentNavigationVM; } set { SetProperty(ref _currentNavigationVM, value); } }

        public ICommand NavigateHomeCommand { get; set; }
        public ICommand NavigateCustomerCommand { get; set; }

        public NavationItemClickedAction navationItemClickedAction { get; set; }


        public MainViewModel()
        {

            //_navigationDrawerStore = navigationDrawerStore;
            // _navigationDrawerStore.CurrentContentViewModelChanged += OnCurrentNavigationContentViewModelChanged;

            //var homeVM = vMList.Where(s => s.GetType() == typeof(HomeViewModel)).FirstOrDefault();
            //var custVm = vMList.Where(s => s.GetType() == typeof(CustomerViewModel)).FirstOrDefault();
            navationItemClickedAction = new NavationItemClickedAction();
            var homeVM = new HomeViewModel();
            var custVm = new CustomerViewModel();

            if (homeVM != null)
            {
                // NavigateHomeCommand = new NavigateCommand(new NavigationDrawerService<HomeViewModel>(_navigationDrawerStore, () => (HomeViewModel)homeVM));
                NavigateHomeCommand = new NavigateDrawerCommand(this, homeVM);
            }
            if (custVm != null)
            {
                NavigateCustomerCommand = new NavigateDrawerCommand(this, custVm);
               // NavigateCustomerCommand = new NavigateCommand(new NavigationDrawerService<CustomerViewModel>(_navigationDrawerStore, () => (CustomerViewModel)homeVM));
            }


        }
 
   



    }
}
