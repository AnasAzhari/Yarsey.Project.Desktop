using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Desktop.WPF.ViewModels;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Services
{
    public class NavigationDrawerService<T> : INavigationService where T:ViewModelBase
    {
        private readonly NavigationDrawerStore _navigationDrawerStore;

        private readonly Func<T> _contentVM;
        public NavigationDrawerService(NavigationDrawerStore navigationDrawerStore,Func<T> contentVM)
        {
            _navigationDrawerStore = navigationDrawerStore;
            _contentVM = contentVM;
        }
        public void Navigate()
        {
            _navigationDrawerStore.CurrentContentViewModel = _contentVM();
        }

        public void NavigateEditCustomer(Customer customer)
        {
            _navigationDrawerStore.CurrentContentViewModel = _contentVM();
            if (_navigationDrawerStore.CurrentContentViewModel.GetType() == typeof(EditCustomerViewModel))
            {
                EditCustomerViewModel vm = (EditCustomerViewModel)_navigationDrawerStore.CurrentContentViewModel;
                vm.SelectedCustomer = customer;

            }
        }
    }
}
