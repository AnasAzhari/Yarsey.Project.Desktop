using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.Services
{
    interface IDialogService
    {
        public void ShowErrorMessage(string error);

        public void ShowMessage(string message);
    }
}
