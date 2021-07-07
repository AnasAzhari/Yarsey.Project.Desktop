using System;
using System.Collections;
using System.Collections.Generic;
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
    public class NewProductViewModel:ViewModelBase, INotifyDataErrorInfo
    {
        private readonly INavigationService _productNavService;
        private readonly BusinessStore _businessStore;
        private readonly BusinessDataService _businessDataService;

        public string ProductName { get { return _productName; } set { SetProperty(ref _productName, value); } }
        public decimal ProductCost { get { return _productDecimal; } set { SetProperty(ref _productDecimal, value); } }
        public ProductUom ProductUOM { get { return _productUom; } set { SetProperty(ref _productUom, value); } }
        public string Notes { get { return _notes; } set { SetProperty(ref _notes, value); } }

        public List<ProductUom> ProductUoms { get; set; } = Enum.GetValues(typeof(ProductUom)).Cast<ProductUom>().ToList();

        ProductUom _productUom;
        string _productName;
        decimal _productDecimal;
        string _notes;
        public ICommand NavigateProductCommand { get; set; }

        public ICommand CreateProductCommand { get; set; }

        public NewProductViewModel(INavigationService productNavService, BusinessStore businessStore, BusinessDataService businessDataService)
        {
            this._productNavService = productNavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;

            NavigateProductCommand = new NavigationDrawerCommand(productNavService);
            CreateProductCommand = new AsyncRelayCommand(ValidateAsync, Success);
        }
        private bool canValidateForErrors;

        private async Task Success()
        {
            //Customer customer = new Customer() { Name = Name, Adress = Adress, Email = Email, PhoneNo = PhoneNo, Created_at = DateTime.Now };

            //await _businessDataService.AddCustomer(_businessStore.CurrentBusiness.Id, customer).ContinueWith((customer) => { _customerVMNavigationService.Navigate(); });

            //_businessStore.RefreshBusiness();
            Product product = new Product() { ProductName = ProductName, ProductUOM = ProductUOM, ProductCost = ProductCost, Notes = Notes };
            await _businessDataService.AddProduct(_businessStore.CurrentBusiness.Id, product).ContinueWith((product) => { _productNavService.Navigate(); });
            _businessStore.RefreshBusiness();
        }
     

        private async Task<bool> ValidateAsync()
        {

            canValidateForErrors = true;
            if (this.ErrorsChanged != null)
            {
                this.RaiseErrorsChanged("ProductName");
                //this.RaiseErrorsChanged("PhoneNo");
                //this.RaiseErrorsChanged("Adress");
                //this.RaiseErrorsChanged("Email");


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

            if (string.IsNullOrEmpty(columnName) || columnName == "ProductName")
            {
                if (string.IsNullOrEmpty(ProductName))
                    result.Add("Sila masukkan nama");
            }

            return result;
        }

        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return OnValidate(propertyName);
        }


        #region Dispose
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
