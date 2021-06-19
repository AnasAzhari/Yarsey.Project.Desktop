using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class NewSaleViewModel:ViewModelBase
    {
        private readonly BusinessStore _businessStore;
        private readonly SaleDataService _saleDataService;

        private List<Customer> _customerList;

        public List<Customer> CustomerList
        {
            get { return _customerList; }
            set { _customerList = value; }
        }


        public NewSaleViewModel(BusinessStore businessStore,SaleDataService saleDataService)
        {
            this._businessStore = businessStore;
            this._saleDataService = saleDataService;
            this.CustomerList = businessStore?.CurrentBusiness.Customers.ToList();
        }


    }
}
