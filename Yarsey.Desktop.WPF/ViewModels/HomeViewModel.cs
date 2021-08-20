﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Yarsey.Desktop.WPF.Stores;
using Yarsey.Desktop.WPF.Commands;
using Yarsey.Domain.Models;
using Yarsey.Desktop.WPF.Services;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Yarsey.Desktop.WPF.ViewModels
{
    public class HomeViewModel:ViewModelBase
    {
        private readonly BusinessStore _businessstore;
        private readonly SettingsConfiguration _settingsConfiguration;
       

        private BitmapImage _image;

        public ICommand OpenModal { get; set; }

        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }


        private string _bizname;

        public string BizName
        {
            get { return _bizname; }
            set { SetProperty(ref _bizname, value); }
        }

        private Color ligthtThemeText =Colors.DarkGreen;
        private Color darkThemeText = Colors.Bisque;

        private System.Windows.Media.Brush _colorOnTheme;
        public System.Windows.Media.Brush ColorOnTheme
        {
            get { return _colorOnTheme; }
            set { SetProperty(ref _colorOnTheme, value); }
        }

        public HomeViewModel(BusinessStore businessstore, SettingsConfiguration settingsConfiguration)
        {
            this._settingsConfiguration = settingsConfiguration;
            this._businessstore = businessstore;
            this._businessstore.CurrentBusinessChanged += OnBusinessChanged;
            InitConfigure();

            Console.WriteLine(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol);

        }

        public void InitConfigure()
        {
            if (_settingsConfiguration.CurrentSettings.LightTheme == true)
            {
                SetLightMode();
            }
            else
            {
                SetDarkMode();
            }
        }

        public void SetLightMode()
        {
            ColorOnTheme = new SolidColorBrush(ligthtThemeText);
        }
        public void SetDarkMode()
        {
            ColorOnTheme = new SolidColorBrush(darkThemeText);
        }

        public void OnBusinessChanged()
        {
            BizName = this._businessstore.CurrentBusiness.BusinessName;
            byte[] imgBlob = this._businessstore.CurrentBusiness.Image;
            Image = Helper.Helper.BlobToImage(imgBlob);
            //PersonsList = this._businessstore.CurrentBusiness.Customers.ToList();
            var invoices = this._businessstore.CurrentBusiness.Invoices;
            var totalReceivable = invoices.SelectMany(o => o.ProductsSelected).Sum(x => x.PricePerItem * x.Quantity);
            TotalReceivable = totalReceivable;


            this.DataPoints = new ObservableCollection<LineChartModel>();
            DateTime year = new DateTime(2005, 5, 1);

            DataPoints.Add(new LineChartModel() { Year = year.AddYears(1), Germany = 24, England = 34 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(2), Germany = 14, England = 24 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(3), Germany = 25, England = 35 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(4), Germany = 08, England = 18 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(5), Germany = 27, England = 37 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(6), Germany = 34, England = 44 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(7), Germany = 39, England = 49 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(8), Germany = 17, England = 27 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(9), Germany = 24, England = 34 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(10), Germany = 28, England = 38 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(11), Germany = 34, England = 44 });
            DataPoints.Add(new LineChartModel() { Year = year.AddYears(12), Germany = 39, England = 49 });

        }

        decimal _totalReceivable;
        public decimal TotalReceivable
        {
            get { return _totalReceivable; }
            set { SetProperty(ref _totalReceivable, value); }
        }
        public string CurrencySymbol
        {
            get { return CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol; }
        }

        public ObservableCollection<LineChartModel> DataPoints
        {
            get;
            set;
        }
    }

    public class LineChartModel
    {
        public DateTime Year
        {
            get;
            set;
        }
        public double Germany
        {
            get;
            set;
        }
        public double England
        {
            get;
            set;
        }
    }


}
