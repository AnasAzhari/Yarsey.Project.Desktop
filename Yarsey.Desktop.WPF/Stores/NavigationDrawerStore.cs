using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;

namespace Yarsey.Desktop.WPF.Stores
{
    public class NavigationDrawerStore
    {
        public event Action CurrentContentViewModelChanged;

        private ViewModelBase _currentContentViewModel { get; set; }

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
