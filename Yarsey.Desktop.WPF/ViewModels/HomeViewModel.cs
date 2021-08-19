using System;
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
        }
    }


}
