using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{

    public class CustomerViewModel:ViewModelBase
    {
        private string _vmstring= "Doodle";
        public string VMString { get { return _vmstring; } set { SetProperty(ref _vmstring, value); } }

        private ObservableCollection<Customer> _customerCollection;

        public ObservableCollection<Customer> CustomerCollection
        {
            get { return _customerCollection; }
            set { SetProperty(ref _customerCollection, value); }

        }

        private readonly CustomerDataService _customerDataService;

        public ICommand NavigateNewCustomer { get; }

        public CustomerViewModel(INavigationService newCustNavService, CustomerDataService customerDataService)
        {
            _customerDataService = customerDataService;
            _vmstring = "Google";
            NavigateNewCustomer = new NavigationDrawerCommand(newCustNavService);
            this.CustomerCollection = GetCustomerCollection().Result;
            
        }

        private async Task<ObservableCollection<Customer>> GetCustomerCollection()
        {

            IEnumerable<Customer> customerlist = await _customerDataService.GetAll();
            ObservableCollection<Customer> custCollection = new ObservableCollection<Customer>(customerlist);

            return custCollection;
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
            var item = o as Customer;
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
                        if (item.Name.ToLower().Contains(FilterText.ToLower())){
                            return true;
                          //  item.PhoneNo.ToLower().Contains(FilterText.ToLower()) ||
                          //item.Email.ToLower().Contains(FilterText.ToLower()) ||

                          //item.Adress.ToLower().Contains(FilterText.ToLower()))
                        }else if(string.IsNullOrEmpty(item.PhoneNo) && item.PhoneNo.ToLower().Contains(FilterText.ToLower())){
                            return true;
                        }else if(string.IsNullOrEmpty(item.Email)&& item.Email.ToLower().Contains(FilterText.ToLower()))
                        {
                            return true;
                        }else if(string.IsNullOrEmpty(item.Adress)&& item.Adress.ToLower().Contains(FilterText.ToLower())){
                            return true;
                        }
                        else
                        {
                            return false;
                        }                          
                            
                        
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
