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
    public class InvoiceViewModel:ViewModelBase
    {
        private readonly INavigationService _newInvoicenavService;
        private readonly BusinessStore _businessStore;
        private readonly IBusinessService _businessDataService;
        private readonly GeneralModalNavigationService _generalModalNavigationService;



        private ObservableCollection<Invoice> _invCollection;

        public ObservableCollection<Invoice> InvoiceCollection
        {
            get { return _invCollection; }
            set { SetProperty(ref _invCollection, value); }
        }

        private Invoice _selectedInvoice;

        public Invoice SelectedInvoice
        {
            get { return _selectedInvoice; }
            set { SetProperty(ref _selectedInvoice, value); }
        }

        public ICommand NavigateNewInvoice{ get; }

        public InvoiceViewModel(INavigationService newInvoicenavService, BusinessStore businessStore, IBusinessService businessDataService, GeneralModalNavigationService generalModalNavigationService)
        {
            this._newInvoicenavService = newInvoicenavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;
            this._generalModalNavigationService = generalModalNavigationService;

            this.NavigateNewInvoice = new NavigationDrawerCommand(newInvoicenavService);

            OnBusinessChanged();
        }

        public async void OnBusinessChanged()
        {
            var res = await GetInvoiceCollection();
            this.InvoiceCollection = res;
        }

        private async Task<ObservableCollection<Invoice>> GetInvoiceCollection()
        {
            if (_businessStore.CurrentBusiness != null)
            {
                IEnumerable<Invoice> iList = _businessStore.CurrentBusiness.Invoices?.OrderByDescending(x=>x.ref_no).ToList();
                // IEnumerable<Customer> customerlist = await _customerDataService.GetCustomersByBusineness(_businessStore.CurrentBusiness.Id);
                ObservableCollection<Invoice> invCollection;
                if (iList != null)
                {
                    invCollection = new ObservableCollection<Invoice>(iList);
                }
                else
                {
                    invCollection = null;

                }

                return invCollection;

            }
            else
            {
                return null;
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
            var item = o as Invoice;
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
                        if (item.ref_no.ToLower().Contains(FilterText.ToLower()))
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
