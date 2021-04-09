using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        public LoginCommand(Action<Exception> onException):base(onException)
        {


        }

        protected override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
