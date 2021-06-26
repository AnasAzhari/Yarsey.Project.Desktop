using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Yarsey.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        private bool _isOpen = false;
        public bool IsOpen { get { return _isOpen; } set
            {
                if (value == true)
                {
                    this.ShowDialog();
                }
                else
                {
                    this.Close();
                }
            }
        }
        public ModalWindow()
        {
            InitializeComponent();
            //this.Owner = App.Current.MainWindow;

        }
    }
}
