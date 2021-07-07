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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl,IDisposable
    {
        bool isBusy = false;
        public CustomerView()
        {
           
         
            this.Unloaded += CustomerView_Unloaded;
            this.Loaded += CustomerView_Loaded;
            InitializeComponent();
            //this.busyIndicator.IsBusy = true;
            

        }

        private void CustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            //this.busyIndicator.IsBusy = false;
        }

        private void CustomerView_Unloaded(object sender, RoutedEventArgs e)
        {
            
            this.Dispose();
        }

        
        public void Dispose()
        {
            sfDataGrid?.Dispose();
            sfDataPagerC?.Dispose();
           
        }
    }
}
