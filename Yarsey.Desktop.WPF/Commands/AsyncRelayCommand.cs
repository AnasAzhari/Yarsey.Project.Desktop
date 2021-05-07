using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.Commands
{
    public class AsyncRelayCommand:AsyncCommandBase
    {
        // task to be executed
        private readonly Func<Task<bool>> _callbackOnValidation;

        private readonly Func<Task> _callbackOnSuccess;

        public AsyncRelayCommand(Func<Task<bool>> callback, Func<Task> callbackOnSuccess = null, Action<Exception> onException = null) : base(onException)
        {
            _callbackOnValidation = callback;
            _callbackOnSuccess = callbackOnSuccess;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            //await _callbackOnValidation().ContinueWith((j)=> {

            //    if (j.IsCompletedSuccessfully )
            //    {
            //         _callbackOnSuccess?.Invoke();

            //    }

            //});

            var res = await _callbackOnValidation();

            if (res == true)
            {
                _callbackOnSuccess?.Invoke();
            }
        }
    }
}
