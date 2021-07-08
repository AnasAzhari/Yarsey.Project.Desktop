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

        private ObservableCollection<Customer> _businessCustomers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> BusinessCustomers { get { return _businessCustomers; } set { SetProperty(ref _businessCustomers, value); } }

        public ObservableCollection<ProductSelection> _productSelections=new ObservableCollection<ProductSelection>();

        public ObservableCollection<ProductSelection> ProductSelections { get { return _productSelections; } set { SetProperty(ref _productSelections, value); } }

        private ObservableCollection<Product> _productList= new ObservableCollection<Product>();
        public ObservableCollection<Product> ProductList { get { return _productList; } set { SetProperty(ref _productList, value); } }

        public ICommand NavigateInvoiceCommand { get; set; }

        public ICommand AddProductSelectionCommand { get; set; }

        public RelayBasicCommand SelectedProductChanged { get; set; }

        public NewInvoiceViewModel(INavigationService invoiceNavService, BusinessStore businessStore, BusinessDataService businessDataService)
        {
            this._productSelections = new ObservableCollection<ProductSelection>();
            this._productNavService = invoiceNavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;

            this.NavigateInvoiceCommand = new NavigationDrawerCommand(invoiceNavService);
            this.AddProductSelectionCommand = new AsyncRelayCommand(DeleteValidationAsync, AddProduct);
            this.SelectedProductChanged = new RelayBasicCommand(ProductChanged);
            if (_businessStore.CurrentBusiness != null)
            {
                InitCollections();


            }

        }

        public void InitCollections()
        {
            var c = this._businessStore.CurrentBusiness?.Customers.ToList();
            if (c != null)
            {
                BusinessCustomers = new ObservableCollection<Customer>(c);
            }
            var p = this._businessStore.CurrentBusiness?.Products.ToList();
            _productList = new ObservableCollection<Product>(p);
            
        }

        

        public async Task<bool> DeleteValidationAsync()
        {
            return true;
        }
        public async Task AddProduct()
        {
            var ps = new ProductSelection() {PricePerItem=900 };
            ProductSelections.Add(ps);
       
        }
        private void ProductChanged()
        {
           

        }

    }
}
