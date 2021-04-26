using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.WPF.Stores
{
    public class NavigationStore 
    {
        public event Action CurrentViewModelChanged;

        private ViewModelBase _currentLayoutViewModel { get; set; } 

        // this are all Layouts only
        public ViewModelBase CurrentLayoutViewModel {

            get => _currentLayoutViewModel;
            set
            {
                _currentLayoutViewModel = value;
                OnCurrentViewModelChanged();
            }
        
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
