using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Commands
{
    public class NavigateDrawerCommand : CommandBase
    {
        private LayoutNavigationDrawerViewModel _layoutNavigationDrawerViewModel;

        private ViewModelBase _contentViewModelBase;

        private NavigationDrawerStore _navigationDrawerStore;

        public NavigateDrawerCommand(LayoutNavigationDrawerViewModel layoutNavigationDrawerViewModel,ViewModelBase contentViewModelBase,NavigationDrawerStore navigationDrawerStore)
        {
            _layoutNavigationDrawerViewModel = layoutNavigationDrawerViewModel;
            _contentViewModelBase = contentViewModelBase;
            _navigationDrawerStore = navigationDrawerStore;
          
        }

        public override void Execute(object parameter)
        {
            //_layoutNavigationDrawerViewModel.CurrentContentViewModel = _contentViewModelBase;
            _navigationDrawerStore.CurrentContentViewModel = _contentViewModelBase;
            
        }
    }
}
