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
using Syncfusion.Windows.Shared;
using Syncfusion.Windows;
using Syncfusion.SfSkinManager;
using Yarsey.Desktop.WPF.ViewModels;
using Microsoft.Extensions.Configuration;
using Syncfusion.Themes.FluentDark.WPF;
using Syncfusion.Themes.FluentLight.WPF;

namespace Yarsey.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :ChromelessWindow
    {
        private readonly IConfiguration _configuration;

        private FluentTheme _lightFluent;
        private FluentTheme _darkFluent;


        private string _lightModeBackground = "#e6e6e6";
        private string _darkModeBackground = "#262626";
      

        private Brush _lightBrush;
        private Brush _darkBrush;

        public MainWindow(ViewModelBase viewModelBase,IConfiguration configuration)
        {

            _lightBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_lightModeBackground));
            _darkBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_darkModeBackground));
       
            DataContext = viewModelBase;
            this._configuration = configuration;
            SfSkinManager.ApplyStylesOnApplication = true;


            InitializeComponent();

            InitConfigure();
        }

        public void SetLightMode()
        {

            // SfSkinManager.SetTheme(this, new FluentTheme() { ThemeName = "FluentLight" });
            //SfSkinManager.SetTheme(this, new FluentTheme("FluentLight",null));

            // SfSkinManager.SetTheme(this,new FluentTheme("FluentLight",))
            SfSkinManager.SetTheme(this, _lightFluent);
            MainControl.Background = _lightBrush;
        }
        public void SetDarkMode()
        {
            // SfSkinManager.SetTheme(this, new FluentTheme() { ThemeName = "FluentDarkTurq" });
            //SfSkinManager.SetTheme(this, new FluentTheme("FluentDark", null));

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

            //FluentLightThemeSettings fluentLightThemeSettings = new FluentLightThemeSettings();
            //fluentLightThemeSettings.PrimaryBackground =new SolidColorBrush() { Color=Colors.Red,Opacity=1.0};
            //SfSkinManager.RegisterThemeSettings("FluentLight", fluentLightThemeSettings);





            //FluentLightThemeSettings l = new FluentLightThemeSettings();
            //l.PrimaryBackground.Opacity = 1;

            //FluentDarkThemeSettings d = new FluentDarkThemeSettings();
            //d.PrimaryBackground.Opacity = 1;

            //SfSkinManager.RegisterThemeSettings("FluentLight", l);
            //SfSkinManager.RegisterThemeSettings("FluentDark", d);




            var d = _configuration.GetSection("Theme").GetSection("LightMode").Value;
            bool result;
            if (bool.TryParse(d, out result))
            {
                if (result == true)
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
}
