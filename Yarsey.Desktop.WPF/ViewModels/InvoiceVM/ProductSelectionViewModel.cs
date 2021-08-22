using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class ProductSelectionViewModel:ViewModelBase,IDisposable, INotifyDataErrorInfo
    {

        public ProductSelectionViewModel(NewInvoiceViewModel newInvoiceViewModel)
        {
            this._newInvoiceViewModel = newInvoiceViewModel;
        }


        Product _selectedProduct;

        int _quantity=1;
        decimal _pricePerItem;
        decimal _tax;
        decimal _discount;
        decimal _amount;
        string _word;
        private readonly NewInvoiceViewModel _newInvoiceViewModel;

        public Product SelectedProduct { get { return _selectedProduct; } set { SetProperty(ref _selectedProduct, value); SetPricePerItem(); } }
        public int Quantity { get { return _quantity; } set { SetProperty(ref _quantity, value); RecalculateAmount(); } }
        public decimal PricePerItem { get { return _pricePerItem; } set { SetProperty(ref _pricePerItem, value); RecalculateAmount(); } }
        public decimal Tax { get { return _tax; } set { SetProperty(ref _tax, value); RecalculateAmount(); } }
        public decimal Discount { get { return _discount; } set { SetProperty(ref _discount, value); RecalculateAmount(); } }
        public decimal Amount { get { return _amount; } set { SetProperty(ref _amount, value); } }
        public string Word { get { return _word; } set { SetProperty(ref _word, value); } }


        private void SetPricePerItem()
        {
            if (_selectedProduct != null)
            {
                if (_selectedProduct.ProductSalesDetail != null)
                {
                    PricePerItem = _selectedProduct.ProductSalesDetail.SalesPrice;
                }
            }
        }

        private void RecalculateAmount()
        {
            //ProductSalesDetail productSalesDetail;
            //if (_selectedProduct != null)
            //{
            //    if(_selectedProduct.ProductSalesDetail != null)
            //    {
            //        productSalesDetail = SelectedProduct.ProductSalesDetail;
            //        PricePerItem = productSalesDetail.SalesPrice;
            //    }

            //}

            //if (_selectedProduct != null)
            //{
            //    if (_selectedProduct.ProductSalesDetail != null)
            //    {
            //        PricePerItem = _selectedProduct.ProductSalesDetail.SalesPrice;
            //    }
            //}
           

            decimal baseAmount = PricePerItem * Quantity;
            decimal discountedAmount = ((Tax - Discount) / 100)*baseAmount;
            Amount = baseAmount + discountedAmount;
            _newInvoiceViewModel.cbCalculateTotal();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #region Validation

        private bool canValidateForErrors;

        private async Task Success()
        {


        }


        public async Task<bool> ValidateAsync()
        {
            canValidateForErrors = true;
            if (this.ErrorsChanged != null)
            {
                this.RaiseErrorsChanged("SelectedProduct");
             

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

        public void RaiseErrorsChanged(string propertyName)
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
            if (string.IsNullOrEmpty(columnName) || columnName == "SelectedProduct")
            {
                if (SelectedProduct == null)
                    result.Add("Enter Product");
            }
            return result;
        }

        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return OnValidate(propertyName);
        }

        #endregion

    }
}
