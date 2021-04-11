using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.WPF.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        // task to be executed
        private readonly Func<Task> _callback;

        private readonly Func<Task> _callbackOnSuccess;

        public AsyncRelayCommand(Func<Task> callback,Func<Task> callbackOnSuccess=null,Action<Exception> onException=null): base(onException)
        {
            _callback = callback;
            _callbackOnSuccess = callbackOnSuccess;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
             await _callback().ContinueWith((j)=> {

                 if (j.IsCompletedSuccessfully)
                 {
                      _callbackOnSuccess?.Invoke();

                 }
             
             });
        }
    }
}
