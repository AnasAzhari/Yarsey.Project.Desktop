using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Desktop.WPF.Stores
{
    public class MainWindowStore
    {
        public event Action CurrentMainWindowChanged;

        private MainWindow _currentMainWindow;

        public MainWindow CurrentMainWindow
        {
            get { return _currentMainWindow; }
            set
            {
                _currentMainWindow = value;

            }
        }

        private void OnCurrentMainWindowChanged()
        {
            CurrentMainWindowChanged?.Invoke();
        }


    }
}
