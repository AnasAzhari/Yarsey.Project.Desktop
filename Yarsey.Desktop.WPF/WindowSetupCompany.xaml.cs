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
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using Yarsey.Desktop.WPF.Services;

namespace Yarsey.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for WindowSetupCompany.xaml
    /// </summary>
    public partial class WindowSetupCompany : ChromelessWindow
    {
        private readonly SettingsConfiguration _settingsService;
        private FluentTheme _lightFluent;
        private FluentTheme _darkFluent;

        private string _lightModeBackground = "#e6e6e6";
        private string _darkModeBackground = "#262626";

        private Brush _lightBrush;
        private Brush _darkBrush;



        public WindowSetupCompany(SettingsConfiguration settingsService)
        {
            this._settingsService = settingsService;
            _lightBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_lightModeBackground));
            _darkBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_darkModeBackground));

            SfSkinManager.ApplyStylesOnApplication = true;
            InitializeComponent();

            InitConfigure();
           
        }

        public void SetLightMode()
        {

            SfSkinManager.SetTheme(this, _lightFluent);
            MainControl.Background = _lightBrush;
            
        }
        public void SetDarkMode()
        {

            SfSkinManager.SetTheme(this, _darkFluent);
            MainControl.Background = _darkBrush;

        }
        public void InitConfigure()
        {

            _lightFluent = new FluentTheme("FluentLight", null);
            _lightFluent.ShowAcrylicBackground = false;
            _lightFluent.HoverEffectMode = HoverEffect.BackgroundAndBorder;
            

            _darkFluent = new FluentTheme("FluentDark", null);
            _darkFluent.ShowAcrylicBackground = false;
            _darkFluent.HoverEffectMode = HoverEffect.BackgroundAndBorder;

            if (_settingsService.CurrentSettings.LightTheme == true)
            {
                SetLightMode();
            }
            else
            {
                SetDarkMode();
            }

        }


    }
}
