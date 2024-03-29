﻿using System;
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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl,IDisposable
    {

        public ProductView()
        {
            this.Unloaded += ProductView_Unloaded;
            InitializeComponent();
           
        }

        private void ProductView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.sfDataPager.Dispose();
            this.sfDataGrid.Dispose();
        }
    }
}
