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
        public LayoutNavigationDrawerService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
           
        }
    }
}
