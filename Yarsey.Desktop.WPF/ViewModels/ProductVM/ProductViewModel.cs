using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ProductViewModel:ViewModelBase
    {
        private ObservableCollection<Product> _productCollection = null;
        private readonly BusinessStore _businessStore;

        public ObservableCollection<Product> ProductCollection{get { return _productCollection; } set{ SetProperty(ref _productCollection, value);}}

        private Product _selectedProduct;
        public Product SelectedProduct { get { return _selectedProduct; } set { SetProperty(ref _selectedProduct, value); } }
        public ICommand NavigateNewProduct { get; }

        public ICommand DeleteProductCommand { get; set; }
      
        public BusinessDataService BusinessDataService { get; }

        public ProductViewModel(INavigationService newProductnavService, BusinessStore businessStore, BusinessDataService businessDataService)
        {
            this._businessStore = businessStore;
            BusinessDataService = businessDataService;
            NavigateNewProduct = new NavigationDrawerCommand(newProductnavService);
            //this.DeleteProductCommand = new AsyncRelayCommand()
            this._businessStore.CurrentBusinessChanged += OnBusinessChanged;
        }


        public async void OnBusinessChanged()
        {

            var res = await GetProductCollectionX();
            this.ProductCollection = res;
        }
        private async Task<ObservableCollection<Product>> GetProductCollectionX()
        {
            IEnumerable<Product> pList = _businessStore.CurrentBusiness.Products?.ToList();
            // IEnumerable<Customer> customerlist = await _customerDataService.GetCustomersByBusineness(_businessStore.CurrentBusiness.Id);
            ObservableCollection<Product> prodCollection;
            if (pList != null)
            {
                prodCollection = new ObservableCollection<Product>(pList);
            }
            else
            {
                prodCollection = null;

            }


            return prodCollection;
        }

        public async Task<bool> DeleteValidationAsync()
        {
            if (SelectedProduct != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        


        #region -------------FILTERING-------------------

        private string _filterText = string.Empty;

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                if (filterChanged != null)
                    filterChanged();
                SetProperty(ref _filterText, value);
            }
        }

        private string _filterOption = "All Columns";

        public string FilterOption
        {
            get { return _filterOption; }
            set
            {
                _filterOption = value.Replace(" ", "");
                if (filterChanged != null)
                    filterChanged();
                SetProperty(ref _filterOption, value);
            }
        }


        internal delegate void FilterChanged();

        internal FilterChanged filterChanged;

        private string _filterCondition;

        public string FilterCondition
        {
            get { return _filterCondition; }
            set
            {
                _filterCondition = value.Replace(" ", "");
                if (filterChanged != null)
                    filterChanged();
                SetProperty(ref _filterCondition, value);
            }
        }

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as Product;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !FilterOption.Equals("All Columns"))
                    {

                    }
                    else if (FilterOption.Equals("All Columns"))
                    {
                        if (item.ProductName.ToLower().Contains(FilterText.ToLower()))
                            return true;

                        //if(!string.IsNullOrEmpty(item.PhoneNo))
                        //    return item.PhoneNo.ToLower().Contains(FilterText.ToLower());

                        //if(!string.IsNullOrEmpty(item.Email))
                        //    return item.Email.ToLower().Contains(FilterText.ToLower());

                        //if(!string.IsNullOrEmpty(item.Adress))
                        //    item.Adress.ToLower().Contains(FilterText.ToLower());


                        return false;



                    }
                    else
                    {

                    }
                }
            }
            return false;
        }


        #endregion

    }
}
