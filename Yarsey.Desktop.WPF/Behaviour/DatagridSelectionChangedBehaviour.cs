using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Grid;
using Yarsey.Desktop.WPF.View;

namespace Yarsey.Desktop.WPF.Behaviour
{
    public class DatagridSelectionChangedBehaviour:Behavior<CustomerView>
    {

        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += CustomerView_Loaded;
            base.OnAttached();
        }

        private void CustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.sfDataGrid.SelectionChanged += DataGrid_SelectionChanged;
        }

        private void DataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
           
        }
    }
}
