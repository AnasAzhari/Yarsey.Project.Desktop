using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yarsey.WPF.Commands;
using Yarsey.WPF.Services;
using Yarsey.WPF.Stores;

namespace Yarsey.WPF.ViewModels
{
    public class LayoutNavigationDrawerViewModel:ViewModelBase
    {
        private List<ViewModelBase> _contentViewModel;

        private ViewModelBase _currentContentViewModel;
        public ViewModelBase CurrentContentViewModel
        {
            get { return _currentContentViewModel; }
            set { SetProperty(ref _currentContentViewModel, value); }
        }


        public ICommand HomeNavigateCommand { get;}
        public ICommand CustomerNavigateCommand { get;}

        public ICommand ModalCommand { get; }
        public string Tester { get; } = "Testing content";
        

        public LayoutNavigationDrawerViewModel(List<ViewModelBase> contentViewModel,NavigationDrawerStore navigationDrawerStore)
        {

            _contentViewModel = contentViewModel;

            var homeVM = _contentViewModel.Where(s => s.GetType() == typeof(HomeViewModel)).FirstOrDefault();
            var customerVM = _contentViewModel.Where(s => s.GetType() == typeof(CustomerViewModelV2)).FirstOrDefault();

            if (homeVM != null)
            {
                HomeNavigateCommand = new NavigateDrawerCommand(this, homeVM, navigationDrawerStore);
            }
            if (customerVM != null)
            {
                CustomerNavigateCommand = new NavigateDrawerCommand(this, customerVM, navigationDrawerStore);
            }
            //ModalCommand = new NavigateCommand(customerModalNavigationService);
        }

        public override void Dispose()
        {
            _contentViewModel.ForEach(s=> { s.Dispose(); });
        }

    }
}
