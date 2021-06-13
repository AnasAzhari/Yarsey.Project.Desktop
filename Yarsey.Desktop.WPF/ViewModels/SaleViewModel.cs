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
   public class SaleViewModel:ViewModelBase
    {
        private readonly BusinessStore _businessStore;
        private readonly SaleDataService _saleDataService;
        private ObservableCollection<Sale> _salesCollection;

        public ObservableCollection<Sale> SalesCollection
        {
            get { return _salesCollection; }
            set { SetProperty(ref _salesCollection, value); }
        }
        public Sale SelectedSale{ get; set; }

        public ICommand NavigateSaleCustomer { get; }

        public SaleViewModel(INavigationService navigationService, BusinessStore businessStore,SaleDataService saleDataService)
        {
            NavigateSaleCustomer = new NavigationDrawerCommand(navigationService);
            this._businessStore = businessStore;
            this._saleDataService = saleDataService;
            this._businessStore.CurrentBusinessChanged += OnBusinessChanged;
            
        }

        public void OnBusinessChanged()
        {
            var res = GetSalesCollection().Result;
            this.SalesCollection = res;
        }

        private async Task<ObservableCollection<Sale>> GetSalesCollection()
        {
            IEnumerable<Sale> sList = _businessStore.CurrentBusiness.Sales.ToList();
            ObservableCollection<Sale> saleCollection = new ObservableCollection<Sale>(sList);
            return saleCollection;
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
            var item = o as Sale;
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
                        if (item.Customer.Name.ToLower().Contains(FilterText.ToLower()))
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
