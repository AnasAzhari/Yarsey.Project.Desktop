using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class EditCustomerViewModel:ViewModelBase, INotifyDataErrorInfo
    {
        private readonly BusinessStore _businessStore;
        private readonly BusinessDataService _businessDataService;
        private readonly GeneralModalNavigationService _generalModalNavigationService;




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

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                if(value!=null && value.GetType() == typeof(Customer))
                {
                    _selectedCustomer = value;
                    Id = _selectedCustomer.Id;
                    Name = _selectedCustomer.Name;
                    Adress = _selectedCustomer.Adress;
                    Email = _selectedCustomer.Email;
                    PhoneNo = _selectedCustomer.PhoneNo;
                    NameCompany = _selectedCustomer.CompanyName;
                    Notes = _selectedCustomer.Note;
                }
            }
        }


        public ICommand NavigateCustomerCommand { get; set; }

        public EditCustomerViewModel(INavigationService navigationService, BusinessStore businessStore, BusinessDataService businessDataService, GeneralModalNavigationService generalModalNavigationService)
        {
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;
            this._generalModalNavigationService = generalModalNavigationService;

            NavigateCustomerCommand = new NavigationDrawerCommand(navigationService);

        }

        public bool HasErrors => throw new NotImplementedException();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
