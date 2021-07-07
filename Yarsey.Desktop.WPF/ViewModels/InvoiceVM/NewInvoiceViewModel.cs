using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;
namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewInvoiceViewModel:ViewModelBase
    {
        private readonly INavigationService _productNavService;
        private readonly BusinessStore _businessStore;
        private readonly BusinessDataService _businessDataService;

        private ObservableCollection<Customer> _businessCustomers;
        public ObservableCollection<Customer> BusinessCustomers { get { return _businessCustomers; } set { SetProperty(ref _businessCustomers, value); } }

        public ObservableCollection<ProductSelection> _productSelections;

        public ObservableCollection<ProductSelection> ProductSelections { get { return _productSelections; } set { } }
        public ICommand NavigateInvoiceCommand { get; set; }

        public NewInvoiceViewModel(INavigationService invoiceNavService, BusinessStore businessStore, BusinessDataService businessDataService)
        {
            this._productNavService = invoiceNavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;

            this.NavigateInvoiceCommand = new NavigationDrawerCommand(invoiceNavService);
            if (_businessStore.CurrentBusiness != null)
            {
                InitCustomerCollection();
            }

        }

        public void InitCustomerCollection()
        {
            var c = this._businessStore.CurrentBusiness?.Customers.ToList();
            if (c != null)
            {
                BusinessCustomers = new ObservableCollection<Customer>(c);
            }
            
        }

    }
}
