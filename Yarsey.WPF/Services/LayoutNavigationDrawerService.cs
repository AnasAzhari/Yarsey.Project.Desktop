using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Services
{
    public class LayoutNavigationDrawerService<T> : INavigationService where T : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly NavigationDrawerStore _navigationDrawerStore;

        private readonly Func<T> _contentViewModel;
        
        public LayoutNavigationDrawerService(NavigationStore navigationStore,Func<T> contentViewModels)
        {
            _navigationStore = navigationStore;
            _contentViewModel = contentViewModels;
            //_navigationDrawerStore = navigationDrawerStore;
         
        }

        public void Navigate()
        {
            _navigationStore.CurrentLayoutViewModel = new LayoutNavigationDrawerViewModel(_contentViewModel(), _navigationDrawerStore);
        }
    }
}
