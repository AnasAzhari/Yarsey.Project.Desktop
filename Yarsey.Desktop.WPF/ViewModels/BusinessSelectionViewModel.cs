using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class BusinessSelectionViewModel:ViewModelBase
    {




        private ObservableCollection<PageModel> _pages;

        public ObservableCollection<PageModel> Pages { get { return _pages; } set { SetProperty(ref _pages, value); } }


        private readonly IBusinessService _businessDataService;
        private readonly IAccountService _accountService;
        private BusinessSelectionPageModel _businessSelectionPageVM;

        public BusinessSelectionPageModel BizSelectionPage
        {
            get { return _businessSelectionPageVM; }
            set { SetProperty(ref _businessSelectionPageVM, value); }
        }

        private PageModel _businessPage;

        public PageModel BusinessPage
        {
            get { return _businessPage; }
            set { SetProperty(ref _businessPage, value); }
        }

        public BusinessSelectionViewModel(IBusinessService businessDataService,IAccountService accountService)
        {
            this._businessDataService = businessDataService;
            this._accountService = accountService;
            _businessSelectionPageVM = new BusinessSelectionPageModel(_businessDataService);
            _businessPage = new CreateBusinessPageModel(_businessDataService, _accountService) { Title = "Konfigurasi Bisnes", Content = "Konfigurasi Bisnes" };
        }

    }
}
