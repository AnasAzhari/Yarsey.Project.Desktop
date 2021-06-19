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
    public class ProductViewModel:ViewModelBase
    {
        private ObservableCollection<Product> _productCollection = null;

        public ObservableCollection<Product> ProductCollection
        {
            get { return _productCollection; }
            set
            {
                SetProperty(ref _productCollection, value);
            }
        }

        public Product SelectedProduct { get; set; }
        public ICommand NavigateNewProduct { get; }
        public BusinessStore BusinessStore { get; }
        public BusinessDataService BusinessDataService { get; }

        public ProductViewModel(INavigationService newProductnavService, BusinessStore businessStore, BusinessDataService businessDataService)
        {
            BusinessStore = businessStore;
            BusinessDataService = businessDataService;
        }

    }
}
