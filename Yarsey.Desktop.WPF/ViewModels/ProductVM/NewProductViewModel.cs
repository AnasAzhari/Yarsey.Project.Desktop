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
using Yarsey.Domain.Services;
using Yarsey.EntityFramework.Services;
using System.Windows.Data;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewProductViewModel:ViewModelBase, INotifyDataErrorInfo
    {
        private readonly INavigationService _productNavService;
        private readonly BusinessStore _businessStore;
        private readonly IBusinessService _businessDataService;

        public string ProductName { get { return _productName; } set { SetProperty(ref _productName, value); } }
        public decimal ProductCost { get { return _productDecimal; } set { SetProperty(ref _productDecimal, value); } }
        public ProductUom ProductUOM { get { return _productUom; } set { SetProperty(ref _productUom, value); } }
        public string Notes { get { return _notes; } set { SetProperty(ref _notes, value); } }

        public List<ProductUom> ProductUoms { get; set; } = Enum.GetValues(typeof(ProductUom)).Cast<ProductUom>().ToList();

     
        ProductUom _productUom;
        string _productName;
        decimal _productDecimal;
        string _notes;

        bool _isSales;
        public bool IsSales { get { return _isSales; } 
            set { SetProperty(ref _isSales, value);  } }

        bool _isPurchase;
        public bool IsPurchase { get { return _isPurchase; } set { SetProperty(ref _isPurchase, value); ; } }


        //sell 
        public decimal SalesPrice { get; set; }
        public Account SelectedSalesAccount { get; set; }
        public ListCollectionView SalesAccount { get; set; }
        public string SalesDescription { get; set; }

        //
        //purchase

        public decimal PurchasePrice { get; set; }
        public Account SelectedPurchaseAccount { get; set; }

        public ListCollectionView PurchaseAccount { get; set; }
        public string PurchaseDescription { get; set; }
        //
        public ICommand NavigateProductCommand { get; set; }

        public ICommand CreateProductCommand { get; set; }

        public NewProductViewModel(INavigationService productNavService, BusinessStore businessStore, IBusinessService businessDataService)
        {
            this._productNavService = productNavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;

            NavigateProductCommand = new NavigationDrawerCommand(productNavService);
            CreateProductCommand = new AsyncRelayCommand(ValidateAsync, Success);
            SalesAccount = new ListCollectionView(this._businessStore.CurrentBusiness.Accounts.Where(x=>x.AccountType==AccountType.Income).ToList());
            SalesAccount.GroupDescriptions.Add(new PropertyGroupDescription("AccountType"));
            if(this._businessStore.CurrentBusiness.Accounts.ToList().Any(x=>x.Name=="Inventory Sales"))
            {
                foreach (var item in SalesAccount)
                {
                    var curr = (Account)item;
                    if(curr.Name== "Inventory Sales")
                    {
                        SelectedSalesAccount = curr;
                        break;
                    }
                }
            }

            PurchaseAccount = new ListCollectionView(this._businessStore.CurrentBusiness.Accounts.Where(x => x.AccountType == AccountType.Expenses).ToList());
            PurchaseAccount.GroupDescriptions.Add(new PropertyGroupDescription("AccountType"));
            if (this._businessStore.CurrentBusiness.Accounts.ToList().Any(x=>x.Name== "General Expense")){
                foreach (var item in PurchaseAccount)
                {
                    var curr = (Account)item;
                    if(curr.Name=="General Expenses")
                    {
                        SelectedPurchaseAccount = curr;
                        break;

                    }
                }
            }

        }
        private bool canValidateForErrors;

        private async Task Success()
        {
            //Customer customer = new Customer() { Name = Name, Adress = Adress, Email = Email, PhoneNo = PhoneNo, Created_at = DateTime.Now };

            //await _businessDataService.AddCustomer(_businessStore.CurrentBusiness.Id, customer).ContinueWith((customer) => { _customerVMNavigationService.Navigate(); });

            //_businessStore.RefreshBusiness();
            Product product = new Product() { ProductName = ProductName, ProductUOM = ProductUOM, Notes = Notes };

            if(IsSales)
            {
                var salesDetail = new ProductSalesDetail()
                {
                    Product = product,
                    SalesDescription = SalesDescription,
                    SalesPrice = SalesPrice
                };
                product.ProductSalesDetail = salesDetail;
            }

            if (IsPurchase)
            {
                var purchaseDetail = new ProductPurchaseDetail()
                {
                    Product = product,
                    PurchaseDescription = PurchaseDescription,
                    PurchasePrice = PurchasePrice,

                };
                product.ProductPurchaseDetail = purchaseDetail;
            }
           

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

                if (IsSales)
                {
                    this.RaiseErrorsChanged("SalesPrice");
                    this.RaiseErrorsChanged("SelectedSalesAccount");
             
                }


                if (IsPurchase)
                {
                    this.RaiseErrorsChanged("PurchasePrice");
                    this.RaiseErrorsChanged("SelectedPurchaseAccount");
             
                }


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

            if (IsSales)
            {
                if (string.IsNullOrEmpty(columnName) || columnName == "SalesPrice")
                {
                    //if (string.IsNullOrEmpty(ProductName))
                    //    result.Add("Sila masukkan nama");
                    if (SalesPrice <= 0)
                    {
                        result.Add("Price must be greater than 0");
                    }
                }

                if (string.IsNullOrEmpty(columnName) || columnName == "SelectedSalesAccount")
                {
                    //if (string.IsNullOrEmpty(ProductName))
                    //    result.Add("Sila masukkan nama");
                    if (SelectedSalesAccount == null)
                    {
                        result.Add("Please select account");
                    }
                }
            }

            if (IsPurchase)
            {
                if (string.IsNullOrEmpty(columnName) || columnName == "SelectedPurchaseAccount")
                {
                    //if (string.IsNullOrEmpty(ProductName))
                    //    result.Add("Sila masukkan nama");
                    if (SelectedPurchaseAccount == null)
                    {
                        result.Add("Please select account");
                    }
                }

                if (string.IsNullOrEmpty(columnName) || columnName == "PurchasePrice")
                {
                    //if (string.IsNullOrEmpty(ProductName))
                    //    result.Add("Sila masukkan nama");
                    if (PurchasePrice <= 0)
                    {
                        result.Add("Price must be higher than 0");
                    }
                }

         
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
