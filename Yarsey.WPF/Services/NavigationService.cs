﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;

namespace Yarsey.WPF.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore,Func<TViewModel> createviewmodel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createviewmodel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentLayoutViewModel = _createViewModel();
        }
    }
}
