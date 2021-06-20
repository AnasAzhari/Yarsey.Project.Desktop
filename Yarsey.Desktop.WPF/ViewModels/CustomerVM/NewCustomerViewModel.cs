using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Desktop.WPF.Services;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewCustomerViewModel:ViewModelBase, INotifyDataErrorInfo
    {
        

        private readonly string mailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public int Id { get { return _id; } set { SetProperty(ref _id, value); } }
        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        public string Adress { get { return _adress; } set { SetProperty(ref _adress, value); } }
        public string Email { get { return _email; } set { SetProperty(ref _email, value); } }
        public string PhoneNo { get { return _phoneNo; } set { SetProperty(ref _phoneNo, value); } }
        public string NameCompany { get { return _namecompany; } set { SetProperty(ref _namecompany, value); } }
        public string Notes { get { return _notes; } set { SetProperty(ref _notes, value); } }

        int _id;
        string _name;
        string _adress;
        string _namecompany;
        string _email;
        string _phoneNo;
        string _notes;

        public ICommand NavigateCustomerCommand { get; set; }

        public ICommand CreateCustomerCommand { get; set; }

        

        private CustomerDataService _customerDataService;
        private readonly BusinessStore _businessStore;
        private readonly BusinessDataService _businessDataService;
        private INavigationService _customerVMNavigationService;

        public NewCustomerViewModel(INavigationService navigationService, CustomerDataService customerService,BusinessStore businessStore, BusinessDataService businessDataService)
        {
            _customerVMNavigationService = navigationService;
            _customerDataService = customerService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;
            NavigateCustomerCommand = new NavigationDrawerCommand(navigationService);
            CreateCustomerCommand = new AsyncRelayCommand(ValidateAsync, Success);
        }


        private bool canValidateForErrors;

        private async Task Success()
        {
            Customer customer = new Customer() { Name = Name, Adress = Adress, Email = Email, PhoneNo = PhoneNo,Created_at=DateTime.Now };

            await _businessDataService.AddCustomer(_businessStore.CurrentBusiness.Id, customer).ContinueWith((customer) => { _customerVMNavigationService.Navigate(); });
            
            _businessStore.RefreshBusiness();
        
        }


        private async Task<bool> ValidateAsync()
        {

            canValidateForErrors = true;
            if (this.ErrorsChanged != null)
            {
                this.RaiseErrorsChanged("Name");
                this.RaiseErrorsChanged("PhoneNo");
                this.RaiseErrorsChanged("Adress");
                this.RaiseErrorsChanged("Email");


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

            if (string.IsNullOrEmpty(columnName) || columnName == "Name")
            {
                if (string.IsNullOrEmpty(Name))
                    result.Add("Sila masukkan nama");
            }

            if (string.IsNullOrEmpty(columnName) || columnName == "PhoneNo")
            {
                if (string.IsNullOrEmpty(PhoneNo))
                    result.Add("Enter your phone number");
            }


            if (string.IsNullOrEmpty(columnName) || columnName == "Email")
            {
                if (string.IsNullOrEmpty(Email))
                {

                }
                else
                {
                    if (!Regex.IsMatch(Email, mailPattern))
                        result.Add("Enter a valid email address");
                }
            }

            return result;
        }

        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return OnValidate(propertyName);
        }


    }
}
