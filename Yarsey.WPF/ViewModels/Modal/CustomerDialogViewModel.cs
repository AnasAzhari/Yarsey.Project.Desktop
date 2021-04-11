using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;
using Yarsey.WPF.Commands;
using Yarsey.WPF.Services;

namespace Yarsey.WPF.ViewModels.Modal
{
    public class CustomerDialogViewModel:ViewModelBase,INotifyDataErrorInfo
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

        private readonly CustomerDataService _customerDataService;

        public CustomerDataService CustomerDataService { get { return _customerDataService; } }

        private readonly CloseModalNavigationService _closeModalNavigation;

        public ICommand CloseModalCommand { get; set; }

        public AsyncRelayCommand CreateCustomerCommand { get; set; }

        private ModalNavigationService<CustomerDialogViewModel> _modalNavigationService;

        private CustomerViewModel _customerViewModel;

        public CustomerDialogViewModel(CustomerDataService customerDataService,CustomerViewModel customerViewModel,ModalNavigationService<CustomerDialogViewModel> modalNavigationService)
        {
            _customerDataService = customerDataService;
            _customerViewModel = customerViewModel;
            _modalNavigationService = modalNavigationService;
            //CloseModalCommand = new NavigateCommand(closeModalNavigationService);
            CloseModalCommand = new AsyncRelayCommand(Close);
            
             CreateCustomerCommand = new AsyncRelayCommand(Create,Success, (e) => { _modalNavigationService.NavigateException(e.Message); });  // exception happens what next ?
        }

      

        private bool canValidateForErrors;


        public void Cancel()
        {
           // CloseWindowCommand();

        }

        private async Task Close()
        {
            
            await Task.Run(() => { _modalNavigationService.NavigateClose(); });
        }
        private async Task Success()
        {
            _modalNavigationService.NavigateSuccess("Successfully created Customer");
            await _customerViewModel.OnObjectCreated();
        }

        private async Task Create()
        {
            try
            {
                canValidateForErrors = true;
                if (this.ErrorsChanged != null)
                {
                    this.RaiseErrorsChanged("Name");
                    this.RaiseErrorsChanged("PhoneNo");
                    this.RaiseErrorsChanged("Adress");
                    this.RaiseErrorsChanged("Email");

                }

                if (!this.HasErrors)
                {
                    //_customerFactory.CreateNewCustomer(Name, Adress, Email, PhoneNo);
                    //AddedCustomer();
                    Customer customer = new Customer() { Name = Name, Adress = Adress, Email = Email, PhoneNo = PhoneNo };

                    await  CustomerDataService.Create(customer);
                  
                }
                

            }
            catch (Exception ex)
            {

                throw;
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

        //private void OnSubmitButtonClicked()
        //{
        //    canValidateForErrors = true;
        //    if (this.ErrorsChanged != null)
        //    {
        //        this.RaiseErrorsChanged("Name");
        //        this.RaiseErrorsChanged("PhoneNo");
        //        this.RaiseErrorsChanged("Adress");
        //        this.RaiseErrorsChanged("Email");

        //    }

        //    if (!this.HasErrors)
        //        MessageBox.Show("Your details has been registered successfully", "Success", MessageBoxButton.OK);
        //}
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
