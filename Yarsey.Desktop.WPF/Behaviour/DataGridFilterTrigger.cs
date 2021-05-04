using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Desktop.WPF.ViewModels;


namespace Yarsey.Desktop.WPF.Behaviour
{
    public class DataGridFilterTrigger : TargetedTriggerAction<SfDataGrid>
    {
        protected override void Invoke(object parameter)
        {
            var viewModel = this.Target.DataContext as CustomerViewModel;
            viewModel.filterChanged += OnFilterChanged;
        }

        /// <summary>
        /// occurs when filter changed.
        /// </summary>
        private void OnFilterChanged()
        {
            var viewModel = this.Target.DataContext as CustomerViewModel;
            if (this.Target.View != null)
            {
                this.Target.View.Filter = viewModel.FilerRecords;
                this.Target.View.RefreshFilter();
            }
        }
    }
}
