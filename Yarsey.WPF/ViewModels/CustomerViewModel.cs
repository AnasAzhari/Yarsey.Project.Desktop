using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Stores;
using Yarsey.WPF.Commands;
using Yarsey.Domain.Models;
using System.Collections.ObjectModel;
using Yarsey.EntityFramework.Services;

namespace Yarsey.WPF.ViewModels
{
    public class CustomerViewModel:ViewModelBase
    {

        private ObservableCollection<Customer> _customerCollection;

        public ObservableCollection<Customer> CustomerCollection
        {
            get { return _customerCollection; }
            set { SetProperty(ref _customerCollection, value); }

        }

        private readonly CustomerDataService _customerDataService;

        //public ICommand NavigateHomeCommand { get; }
        public  CustomerViewModel(NavigationStore navigationStore,CustomerDataService customerDataService)
        {
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            this._customerDataService = customerDataService;

            this.CustomerCollection = GetCustomerCollection().Result;
            Console.WriteLine("Testing");
        }

        public  async Task<ObservableCollection<Customer>> GetCustomerCollection()
        {
            //string dbLocation = ConfigurationManager.AppSettings.Get("dbLocation");

            //IDataService<Customer> custService = new GenericDataService<Customer>(new YarseyDBContextFactory(dbLocation));
            //var customers = custService.GetAll().Result.ToList();


            //ObservableCollection<Customer> custCollection = new ObservableCollection<Customer>();
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
                        //if (FilterCondition == null || FilterCondition.Equals("Contains") || FilterCondition.Equals("StartsWith") || FilterCondition.Equals("EndsWith"))
                        //    FilterCondition = "Equals";
                        //bool result = MakeNumericFilter(item, FilterOption, FilterCondition);
                        //return result;
                    }
                    else if (FilterOption.Equals("All Columns"))
                    {
                        if (item.Name.ToLower().Contains(FilterText.ToLower()) ||
                            item.PhoneNo.ToLower().Contains(FilterText.ToLower()) ||
                            item.Email.ToLower().Contains(FilterText.ToLower()) ||
                            //item.Salary.ToString().ToLower().Contains(FilterText.ToLower()) || item.EmployeeID.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Adress.ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        //if (FilterCondition == null || FilterCondition.Equals("Equals") || FilterCondition.Equals("LessThan") || FilterCondition.Equals("GreaterThan") || FilterCondition.Equals("NotEquals"))
                        //    FilterCondition = "Contains";
                        //bool result = MakeStringFilter(item, FilterOption, FilterCondition);
                        //return result;
                    }
                }
            }
            return false;
        }


        #endregion

    }
}
