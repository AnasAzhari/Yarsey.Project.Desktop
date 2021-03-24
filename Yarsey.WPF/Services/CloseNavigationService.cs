using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;

namespace Yarsey.WPF.Services
{
    public class CloseNavigationService : INavigationService
    {
        private readonly ModalNavigationStore _navigationStore;

        public CloseNavigationService(ModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.Close();
        }
    }
}
