using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.ViewModels
{


    public class CustomerViewModel:ViewModelBase
    {
        private string _vmstring= "Doodle";
        public string VMString { get { return _vmstring; } set { SetProperty(ref _vmstring, value); } }

        public CustomerViewModel()
        {
            _vmstring = "Google";
        }
    }


    
}
