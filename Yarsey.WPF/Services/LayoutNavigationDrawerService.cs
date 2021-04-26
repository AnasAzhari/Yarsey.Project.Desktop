using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Services
{
    public class LayoutNavigationDrawerService:INavigationService
    {
        private readonly NavigationStore _navigationStore;

        private readonly NavigationDrawerStore _navigationDrawerStore;

        private readonly List<ViewModelBase> ContentViewModels;

        private readonly INavigationService _navigationService;
        public LayoutNavigationDrawerService(NavigationStore navigationStore,List<ViewModelBase> contentViewModels,NavigationDrawerStore navigationDrawerStore)
        {
            _navigationStore = navigationStore;
            ContentViewModels = contentViewModels;
            _navigationDrawerStore = navigationDrawerStore;
         
        }

        public void Navigate()
        {
            _navigationStore.CurrentLayoutViewModel = new LayoutNavigationDrawerViewModel(ContentViewModels,_navigationDrawerStore);
        }
    }
}
