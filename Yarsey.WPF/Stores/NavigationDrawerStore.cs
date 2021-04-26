using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.ViewModels;

namespace Yarsey.WPF.Stores
{
    public class NavigationDrawerStore
    {

        public event Action CurrentContentViewModelChanged;

        private ViewModelBase _currentContentViewModel { get; set; }

        // this are all contents only
        public ViewModelBase CurrentContentViewModel
        {

            get => _currentContentViewModel;
            set
            {
                _currentContentViewModel = value;
                OnCurrentViewModelChanged();
            }

        }

        private void OnCurrentViewModelChanged()
        {
            CurrentContentViewModelChanged?.Invoke();
        }
    }
}
