using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Services
{
    public class LayoutNavigationService<T> : INavigationService where T : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;

        private readonly Func<T> _createContentViewModel;

        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;



        public LayoutNavigationService(NavigationStore navigationStore,Func<T> creteContentViewModel,Func<NavigationBarViewModel> createnavigationViewmodel)
        {
            _navigationStore = navigationStore;
            _createContentViewModel = creteContentViewModel;
            _createNavigationBarViewModel = createnavigationViewmodel;
     

        }

        public void Navigate()
        {
            _navigationStore.CurrentLayoutViewModel = new LayoutViewModel(_createNavigationBarViewModel(), _createContentViewModel());
        }
    }
}
