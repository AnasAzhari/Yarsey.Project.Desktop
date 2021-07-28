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


        private BusinessSelectionPageModel _businessSelectionPageVM;

        public BusinessSelectionPageModel BusinessSelectionPage
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

        public BusinessSelectionViewModel(IBusinessService businessDataService)
        {
            this._businessDataService = businessDataService;

            _businessSelectionPageVM = new BusinessSelectionPageModel(_businessDataService);
            _businessPage = new CreateBusinessPageModel(businessDataService) { Title = "Konfigurasi Bisnes", Content = "Konfigurasi Bisnes" };
        }

    }
}
