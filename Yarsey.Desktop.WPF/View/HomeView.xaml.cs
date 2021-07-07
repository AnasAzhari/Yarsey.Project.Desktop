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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yarsey.Desktop.WPF.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl,IDisposable
    {
        public HomeView()
        {
            this.Unloaded += HomeView_Unloaded;
            InitializeComponent();
        }

        private void HomeView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Dispose();
        }

        public void Dispose()
        {
            
        }
    }
}
