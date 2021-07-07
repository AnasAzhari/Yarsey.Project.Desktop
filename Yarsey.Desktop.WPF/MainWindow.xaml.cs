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

namespace Yarsey.Desktop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :ChromelessWindow
    {
        private readonly IConfiguration _configuration;

        public MainWindow(ViewModelBase viewModelBase,IConfiguration configuration)
        {
            DataContext = viewModelBase;
            this._configuration = configuration;
            SfSkinManager.ApplyStylesOnApplication = true;
            InitializeComponent();

            InitConfigure();
        }

        public void SetLightMode()
        {
            SfSkinManager.SetTheme(this, new FluentTheme() { ThemeName = "FluentLight" });
          
        }
        public void SetDarkMode()
        {
            SfSkinManager.SetTheme(this, new FluentTheme() { ThemeName = "FluentDarkTurq" });
            
        }
        public void InitConfigure()
        {
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
