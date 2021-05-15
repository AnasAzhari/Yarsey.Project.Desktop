using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Yarsey.Desktop.WPF.ViewModels
{
    public class MainWindowSetupViewModel:ViewModelBase
    {
        private ObservableCollection<PageModel> _pages;

        public ObservableCollection<PageModel> Pages{ get { return _pages; } set { SetProperty(ref _pages, value); } }


        private PageModel _businessPage;

        public PageModel BusinessPage
        {
            get { return _businessPage; }
            set { SetProperty(ref _businessPage, value); }
        }



        private PageModel _welcomePage;
        public PageModel WelcomePage { get { return _welcomePage; } set { SetProperty(ref _welcomePage, value); } } 

        public MainWindowSetupViewModel()
        {
            PopulatePages();
        }

        public void PopulatePages()  
        {       
         
           _welcomePage=  new PageModel() { Title = "Selamat Datang, Terima Kasih kerana memilih Yarsey Desktop Akaun", 
                                            Content="Sila Tekan Next untuk konfigurasi bisnes anda" };
           _businessPage=new CreateBusinessPageModel() { Title = "Konfigurasi Bisnes", Content = "Konfigurasi Bisnes" };

 
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

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _phoneNo;

        public string PhoneNo
        {
            get { return _phoneNo; }
            set { SetProperty(ref _phoneNo, value); }

        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }

        private string _regNo;

        public string RegistrationNo
        {
            get { return _regNo; }
            set { SetProperty(ref _regNo, value); }
        }

        private byte[] _img;

        public byte[] Image
        {
            get { return _img; }
            set { SetProperty(ref _img, value); }
        }

        public ICommand CreateBusinessCommand { get; set; }

        public CreateBusinessPageModel()
        {
            
        }
    }

}
