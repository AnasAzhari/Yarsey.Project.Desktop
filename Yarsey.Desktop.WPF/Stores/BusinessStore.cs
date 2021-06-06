using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Stores
{
    public class BusinessStore
    {
        public event Action CurrentBusinessChanged;

        private Business _currentBusiness;

        public Business CurrentBusiness
        {
            get => _currentBusiness;
            set
            {
                _currentBusiness = value;
                OnCurrentBusinessChanged();
            }
        }

        private void OnCurrentBusinessChanged()
        {
            CurrentBusinessChanged?.Invoke();
        }
    }
}
