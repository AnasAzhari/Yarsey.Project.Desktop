﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.WPF.ViewModels
{
    public class LayoutViewModel:ViewModelBase
    {
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ViewModelBase ContentViewModel { get; }
        public LayoutViewModel(NavigationBarViewModel navigationBarViewModel,ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            contentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}
