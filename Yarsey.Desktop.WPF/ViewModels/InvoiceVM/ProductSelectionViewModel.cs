using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class ProductSelectionViewModel:ViewModelBase
    {
        Product _selectedProduct;

        int _quantity;
        decimal _pricePerItem;
        decimal _tax;
        decimal _discount;
        decimal _amount;
        string _word;

        public Product SelectedProduct { get { return _selectedProduct; } set { SetProperty(ref _selectedProduct, value); } }
        public int Quantity { get { return _quantity; } set { SetProperty(ref _quantity, value); } }
        public decimal PricePerItem { get { return _pricePerItem; } set { SetProperty(ref _pricePerItem, value); } }
        public decimal Tax { get { return _tax; } set { SetProperty(ref _tax, value); } }
        public decimal Discount { get { return _discount; } set { SetProperty(ref _discount, value); } }
        public decimal Amount { get { return _amount; } set { SetProperty(ref _amount, value); } }
        public string Word { get { return _word; } set { SetProperty(ref _word, value); } }
    }
}
