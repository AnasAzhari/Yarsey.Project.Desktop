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
using Yarsey.Domain.Services;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{

    public class CustomerViewModel:ViewModelBase
    {
  
        private ObservableCollection<Customer> _customerCollection=null;

        public ObservableCollection<Customer> CustomerCollection
        {
            get { return _customerCollection; }
            set {
                SetProperty(ref _customerCollection, value); 
            }
        }

        public Customer _selectedCustomer;
        public Customer SelectedCustomer {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value); } 
        }

        private readonly CustomerDataService _customerDataService;
        private readonly BusinessStore _businessStore;
        private readonly IBusinessService _businessDataService;
        private readonly GeneralModalNavigationService _generalModalNavigationService;

        public ICommand NavigateNewCustomer { get; }
        public ICommand DeleteCustomerCommand { get; set; }

        public ICommand EditCustomerCommand { get; set; }
        public ICommand TestModal { get; set; }

        public CustomerViewModel(INavigationService newCustNavService,INavigationService editCustNavService, CustomerDataService customerDataService,BusinessStore businessStore, IBusinessService businessDataService, GeneralModalNavigationService generalModalNavigationService)
        {
            _customerDataService = customerDataService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;
            this._generalModalNavigationService = generalModalNavigationService;
            NavigateNewCustomer = new NavigationDrawerCommand(newCustNavService);
            EditCustomerCommand = new NavigationDrawerEditCommand(editCustNavService, SelectedCustomer);
           //this._businessStore.CurrentBusinessChanged += OnBusinessChanged;

            this.DeleteCustomerCommand = new AsyncRelayCommand(DeleteValidationAsync, ConfirmDelete);
            OnBusinessChanged();

        }

        public async void OnBusinessChanged()
        {
            
            var res = await GetCustomerCollectionX();
            this.CustomerCollection = res;
        }
        private async Task<ObservableCollection<Customer>> GetCustomerCollectionX()
        {
            if (_businessStore.CurrentBusiness != null)
            {
                IEnumerable<Customer> cList = _businessStore.CurrentBusiness.Customers?.ToList();
                // IEnumerable<Customer> customerlist = await _customerDataService.GetCustomersByBusineness(_businessStore.CurrentBusiness.Id);
                ObservableCollection<Customer> custCollection;
                if (cList != null)
                {
                    custCollection = new ObservableCollection<Customer>(cList);
                }
                else
                {
                    custCollection = null;

                }

                return custCollection;
            }
            else
            {
                return null;
            }
        }

        private  ObservableCollection<Customer> GetCustomerCollection()
        {
           // IEnumerable<Customer> cList = _businessStore.CurrentBusiness.Customers.ToList();
            IEnumerable<Customer> customerlist = _customerDataService.GetAll().Result;
            ObservableCollection<Customer> custCollection = new ObservableCollection<Customer>(customerlist);
            
            return custCollection;
        }

        private async Task<ObservableCollection<Customer>> GetCollectionOri()
        {
            IEnumerable<Customer> customerlist = await _customerDataService.GetAll();
            ObservableCollection<Customer> custCollection = new ObservableCollection<Customer>(customerlist);

            return custCollection;
        }

        #region ----------------------Delete Customer Section----------------------------------


        public async Task ConfirmDelete()
        {
            _generalModalNavigationService.NavigationOnConfirmDelete("Are you sure you want to delete this customer ?", DeleteSelectedCustomer);
        }
        public async Task DeleteSelectedCustomer()
        {
            var adatak = _businessStore.CurrentBusiness.Customers.Contains(SelectedCustomer);

            
            await _businessDataService.DeleteCustomer(SelectedCustomer);

            _businessStore.RefreshBusiness();
        }

        public async Task<bool> DeleteValidationAsync()
        {
            if (SelectedCustomer != null)
            {
                return true;
            }
            else
            {
                _generalModalNavigationService.NavigateOnEception("Cannot delete the product");
                return false;
            }
        }



        #endregion


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
                        if (item.Name.ToLower().Contains(FilterText.ToLower()))
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

        #region Dispose

        public override void Dispose()
        {
            this._businessStore.CurrentBusinessChanged -= OnBusinessChanged;
            base.Dispose();
        }


        #endregion
    }
}
