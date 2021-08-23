
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
using System.Windows.Forms;
using Microsoft.Toolkit.Mvvm.Input;
using AsyncRelayCommand = Yarsey.Desktop.WPF.Commands.AsyncRelayCommand;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using Yarsey.Domain.Services;

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
            set {
                
                    SetProperty(ref _fileLocation, value); 
                    
                    

                }
        }

        public byte[] Image
        {
            get { return _img; }
            set { SetProperty(ref _img, value); }
        }

        private BitmapSource _bmpImage;
        public BitmapSource BmpImage
        {
            get { return _bmpImage; }
            set
            {
                SetProperty(ref _bmpImage, value);
            }
        }


        #endregion

        public ICommand CreateBusinessCommand { get; set; }
        public ICommand BrowseImage { get; set; }

        IBusinessService _businessDataService;
        private readonly IAccountService _accountService;
        public Action<Business> ChangeMainWindow;

        public CreateBusinessPageModel(IBusinessService businessDataService, IAccountService accountService)
        {
        
            _businessDataService = businessDataService;
            this._accountService = accountService;
            CreateBusinessCommand = new AsyncRelayCommand(ValidateAsync, Success);
            BrowseImage = new RelayCommand(SearchImage);
            var imgHome = App.Current.TryFindResource("Home") as BitmapImage;

            BmpImage = imgHome;
        }


        private async Task Success()
        {
            Business business = new Business() { BusinessName = Name,RegistrationNo=RegistrationNo ,Adresss = Adress, Email = Email, PhoneNo = PhoneNo,Image=Image };

            business= await _businessDataService.Create(business) ;
            await this._accountService.GenerateDefaultAccounts(business.Id);
            await this._businessDataService.GenerateDefaultRunningNumbers(business.Id);
            ChangeMainWindow(business);

        }

        private bool ValidateFileLocation()
        {
            canValidateForErrors = true;
            if (this.ErrorsChanged != null)
            {
         
                this.RaiseErrorsChanged("FileLocation");
                
                if (OnValidate("FileLocation").Count==0)
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
                if (OnValidate("FileLocation").Count == 0)
                {
                    return true;


                }
                else
                {
                    return false;
                }

            }
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

        public async void SearchImage()
        {

            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            DialogResult ret = ofd.ShowDialog();

            if (ret == DialogResult.OK)
            {
                string filename = ofd.FileName;
                FileLocation = filename;
                var isValid= ValidateFileLocation();
                if (isValid)
                {
                    string filelocation = (string)FileLocation;
                  
                    BmpImage = Helper.Helper.ImageResizer(filelocation, 120) ; // set image

                    // BitmapImage bmpOri = new BitmapImage(new Uri(filelocation));

                    Image = Helper.Helper.FileToByteArray(filelocation);

                   // Image = Helper.Helper.ImageToByte(bmpOri);
                   
              
                }
            }     
        }

        public async void RedirectToMainWindow(Business business)
        {

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

            if(string.IsNullOrEmpty(columnName)|| columnName == "FileLocation")
            {
                if (string.IsNullOrEmpty(FileLocation))
                {

                }
                else
                {
                    string filelocation = (string)FileLocation;
                    var fileLength = new FileInfo(filelocation).Length;
                    var fileLengthKb = (float)fileLength / (float)1024;
                    var fileLengthMb = (float)fileLengthKb / (float)1024;


                    if (fileLengthMb <= 5)
                    {
                        result.Add("File size is bigger than 5MB");
                    }
                    else
                    {
                        Image img = new Bitmap(filelocation);

                        var ar = (float)img.Width / (float)img.Height;
                        
                        if(ar>=5 || ar <=0.1)
                        {
                            result.Add("Aspect ratio is too big or too low");
                        }

                    }
                }
            }

            return result;
        }



        #endregion

       

   
    }
}
