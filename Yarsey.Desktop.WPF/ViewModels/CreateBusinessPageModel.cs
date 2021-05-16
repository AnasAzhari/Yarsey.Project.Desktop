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
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class CreateBusinessPageModel : PageModel, INotifyDataErrorInfo
    {
        #region Properties
        string _name;
        string _email;
        string _phoneNo;
        string _adress;
        string _regNo;
        byte[] _img;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string PhoneNo
        {
            get { return _phoneNo; }
            set { SetProperty(ref _phoneNo, value); }

        }

        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }

        public string RegistrationNo
        {
            get { return _regNo; }
            set { SetProperty(ref _regNo, value); }
        }


        private string _fileLocation;

        public string FileLocation
        {
            get { return _fileLocation; }
            set { SetProperty(ref _fileLocation, value); }
        }

        public byte[] Image
        {
            get { return _img; }
            set { SetProperty(ref _img, value); }
        }
        #endregion

        public ICommand CreateBusinessCommand { get; set; }

        BusinessDataService _businessDataService;

        public CreateBusinessPageModel(BusinessDataService businessDataService)
        {
            _businessDataService = businessDataService;
            CreateBusinessCommand = new AsyncRelayCommand(ValidateAsync, Success);
        }


        private async Task Success()
        {
            Business business = new Business() { BusinessName = Name,RegistrationNo=RegistrationNo ,Adresss = Adress, Email = Email, PhoneNo = PhoneNo,Image=Image };

            await _businessDataService.Create(business).ContinueWith((business) => {});



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
                this.RaiseErrorsChanged("RegistrationNo");
                this.RaiseErrorsChanged("FileLocation");

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


        #region Validation

        private bool canValidateForErrors;
        private readonly string mailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                    [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors
        {
            get
            {
                return OnValidate(string.Empty).Count > 0;
            }
        }
        System.Collections.IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return OnValidate(propertyName);
        }
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



        #endregion
    }
}
