using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.WPF.Stores;

namespace Yarsey.WPF.Services
{
    public class GeneralModalNavigationService : INavigationService
    {
        private ModalNavigationStore _modaNavigationStore;


        public GeneralModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            _modaNavigationStore = modalNavigationStore;
        }


        public void Navigate()
        {
           
        }
    }
}
