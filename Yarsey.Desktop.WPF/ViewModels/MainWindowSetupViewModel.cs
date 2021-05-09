using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class MainWindowSetupViewModel:ViewModelBase
    {
        private ObservableCollection<PageModel> _pages;

        public ObservableCollection<PageModel> Pages{ get { return _pages; } set { SetProperty(ref _pages, value); } }

        public MainWindowSetupViewModel()
        {
            //PopulatePages();
        }

        public void PopulatePages()  
        {
            _pages = new ObservableCollection<PageModel>();
            _pages.Add(new PageModel() { Title = "Welcome",Content="Selamat Datang, Terima Kasih kerana memilih Yarsey Desktop Akaun" });
            _pages.Add(new CreateBusinessPageModel() { Title = "Konfigurasi Bisnes", Content = "Konfigurasi Bisnes" });
        }

    }

    public class PageModel:ViewModelBase
    {
        private string _title;
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

    }
    
    public class CreateBusinessPageModel : PageModel
    {

        public CreateBusinessPageModel()
        {

        }
    }

}
