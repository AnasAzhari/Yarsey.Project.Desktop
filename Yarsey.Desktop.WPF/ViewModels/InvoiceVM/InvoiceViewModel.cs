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
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class InvoiceViewModel:ViewModelBase
    {
        private readonly INavigationService _newInvoicenavService;
        private readonly BusinessStore _businessStore;
        private readonly BusinessDataService _businessDataService;
        private readonly GeneralModalNavigationService _generalModalNavigationService;



        public ICommand NavigateNewInvoice{ get; }

        public InvoiceViewModel(INavigationService newInvoicenavService, BusinessStore businessStore, BusinessDataService businessDataService, GeneralModalNavigationService generalModalNavigationService)
        {
            this._newInvoicenavService = newInvoicenavService;
            this._businessStore = businessStore;
            this._businessDataService = businessDataService;
            this._generalModalNavigationService = generalModalNavigationService;

            this.NavigateNewInvoice = new NavigationDrawerCommand(newInvoicenavService);
        }

    }
}
