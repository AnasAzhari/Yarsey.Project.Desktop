﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Services;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }



    //public class NavigateCommand<TViewModel> : CommandBase where TViewModel:ViewModelBase
    //{
    //    private readonly NavigationStore _navigationStore;
    //    private readonly Func<TViewModel> _createViewModel;

    //    public NavigateCommand(NavigationStore navigationStore,Func<TViewModel> createViewModel) 
    //    {
    //        _navigationStore = navigationStore;
    //        _createViewModel = createViewModel;
    //    }

    //    public override void Execute(object parameter)
    //    {
    //        _navigationStore.CurrentViewModel = _createViewModel();
    //    }
    //}


}
