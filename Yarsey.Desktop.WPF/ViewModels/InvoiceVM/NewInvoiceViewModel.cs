﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;
namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewInvoiceViewModel:ViewModelBase,IDisposable,INotifyDataErrorInfo
    {
        #region Properties

        public string CurrentRunningNo { get; set; }

        private DateTime _invoiceDate=DateTime.Today;
        public DateTime InvoiceDate { get { return _invoiceDate; } set { SetProperty(ref _invoiceDate, value); } }

        private DateTime _due=DateTime.Today;
        public DateTime Due { get { return _due; } set { SetProperty(ref _due, value); } }

        public ObservableCollection<ProductSelectionViewModel> _productSelections=new ObservableCollection<ProductSelectionViewModel>();

        public ObservableCollection<ProductSelectionViewModel> ProductSelections { get { return _productSelections; } set { SetProperty(ref _productSelections, value); } }

        private ObservableCollection<Product> _productList= new ObservableCollection<Product>();
        public ObservableCollection<Product> ProductList { get { return _productList; } set { SetProperty(ref _productList, value); OnProductSelectionChanged(); } }

        decimal _total;
        public decimal Total { get { return _total; } set { SetProperty(ref _total, value); } }

        #endregion

        private readonly INavigationService _productNavService;
        private readonly BusinessStore _businessStore;
        private readonly InvoiceDataService _invoiceDataService;
        private readonly GeneralModalNavigationService _generalModalNavigationService;
        private ObservableCollection<Customer> _businessCustomers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> BusinessCustomers { get { return _businessCustomers; } set { SetProperty(ref _businessCustomers, value); } }
        public ICommand NavigateInvoiceCommand { get; set; }
        public ICommand AddProductSelectionCommand { get; set; }
        public DelegateCommand<object> SelectedProductChanged { get; set; }
        public DelegateCommand<object> DeleteProductSelection { get; set; }

        public Action cbCalculateTotal;
        public NewInvoiceViewModel(INavigationService invoiceNavService, BusinessStore businessStore, InvoiceDataService invoiceDataService,GeneralModalNavigationService generalModalNavigationService)
        {
            this._productSelections = new ObservableCollection<ProductSelectionViewModel>();
            this._productNavService = invoiceNavService;
            this._businessStore = businessStore;
            this._invoiceDataService = invoiceDataService;
            this._generalModalNavigationService = generalModalNavigationService;
            this.cbCalculateTotal += OnProductSelectionChanged;
            this.NavigateInvoiceCommand = new NavigationDrawerCommand(invoiceNavService);
            this.AddProductSelectionCommand = new AsyncRelayCommand(AddValidationAsync, AddProduct);
            this.SelectedProductChanged = new DelegateCommand<object>(ProductChanged);
            this.DeleteProductSelection = new DelegateCommand<object>(DeleteProductSelectionFunction);
            if (_businessStore.CurrentBusiness != null)
            {
                InitCollections();
                InitRunningNo();
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
        public void InitRunningNo()
        {
            CurrentRunningNo = _invoiceDataService.GetNextRunningNo(Helper.Helper.InvoiceModule);
        }

        public async Task<bool> AddValidationAsync()
        {
            //return true;

            if (ProductList == null)
            {
                _generalModalNavigationService.NavigateOnEception("Please create product in product page");
                return false;
            }
            else
            {
                if (ProductList.Count <= 0)
                {
                    _generalModalNavigationService.NavigateOnEception("Please create product in product page");
                    return false;
                }
                else
                {
                   
                    return true;
                }

            }
        }
        public async Task AddProduct()
        {
            var ps = new ProductSelectionViewModel(this);
            ProductSelections.Add(ps);
       
        }
        private void ProductChanged(object param)
        {
            var obj = param.GetType();
            ProductSelectionViewModel ps = (ProductSelectionViewModel)param;
            ps.Word = ps.SelectedProduct.Notes;
            ps.PricePerItem = ps.SelectedProduct.ProductCost;
            
            

        }
        private void DeleteProductSelectionFunction(object param)
        {
            var obj = param.GetType();
            ProductSelectionViewModel ps = (ProductSelectionViewModel)param;
            ProductSelections.Remove(ps);
            this.cbCalculateTotal();
        }

        private void OnProductSelectionChanged()
        {
            decimal sum=0;
            foreach (var item in ProductSelections)
            {
                sum += item.Amount;
            }
            Total = sum;
        }



        #region Validation

        private bool canValidateForErrors;

        private async Task Success()
        {
       

        }


        private async Task<bool> ValidateAsync()
        {

            canValidateForErrors = true;
            if (this.ErrorsChanged != null)
            {
                this.RaiseErrorsChanged("Name");
                this.RaiseErrorsChanged("PhoneNo");
                this.RaiseErrorsChanged("Adress");
                this.RaiseErrorsChanged("Email");


                if (!this.HasErrors)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!this.HasErrors)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasErrors
        {
            get
            {
                return OnValidate(string.Empty).Count > 0;
            }
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private List<string> OnValidate(string columnName)
        {
            List<string> result = new List<string>();

            if (!canValidateForErrors)
                return result;

     
            return result;
        }

        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return OnValidate(propertyName);
        }

        #endregion

        #region Dispose
        public override void Dispose()
        {
            this.cbCalculateTotal -= OnProductSelectionChanged;
            foreach (var item in ProductSelections)
            {
                item.Dispose();
            }
            base.Dispose();

        }
        #endregion
    }
}
