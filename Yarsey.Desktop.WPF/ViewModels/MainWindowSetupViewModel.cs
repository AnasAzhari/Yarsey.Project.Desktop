using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Domain.Models;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class MainWindowSetupViewModel:ViewModelBase
    {

        private ObservableCollection<PageModel> _pages;

        public ObservableCollection<PageModel> Pages{ get { return _pages; } set { SetProperty(ref _pages, value); } }

        public ICommand FinishCommand { get; set; }

        private PageModel _businessPage;

        public PageModel BusinessPage
        {
            get { return _businessPage; }
            set { SetProperty(ref _businessPage, value); }
        }

        private PageModel _welcomePage;

        public PageModel WelcomePage { get { return _welcomePage; } set { SetProperty(ref _welcomePage, value); } }

     
        public MainWindowSetupViewModel(BusinessDataService businessDataService)
        {
            PopulatePages(businessDataService);
            FinishCommand = new DelegateCommand<object>(Finish);
           
        }

        public void Finish(object param)
        {
            Console.WriteLine(" Finish Command");
        }

        public void PopulatePages(BusinessDataService businessDataService)  
        {       
         
           _welcomePage=  new PageModel() { Title = "Selamat Datang, Terima Kasih kerana memilih Yarsey Desktop Akaun", 
                                            Content="Sila Tekan Next untuk konfigurasi bisnes anda" };
           _businessPage=new CreateBusinessPageModel(businessDataService) { Title = "Konfigurasi Bisnes", Content = "Konfigurasi Bisnes" };

 
        }

    }



  

}
