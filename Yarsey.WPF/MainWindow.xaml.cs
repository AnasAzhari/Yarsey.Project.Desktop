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
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.FluentDark.WPF;
using Syncfusion.Themes.FluentDark;
using Syncfusion.Themes;
using Syncfusion.Themes.MaterialDark;


namespace Yarsey.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //string style = "FluentDarkTurq";
            //SkinHelper styleInstance = null;
            //var skinHelpterStr = "Syncfusion.Themes." + style + ".WPF." + style + "SkinHelper, Syncfusion.Themes." + style + ".WPF";
            //Type skinHelpterType = Type.GetType(skinHelpterStr);
            //if (skinHelpterType != null)
            //    styleInstance = Activator.CreateInstance(skinHelpterType) as SkinHelper;
            //if (styleInstance != null)
            //{
            //    SfSkinManager.RegisterTheme(style, styleInstance);
            //}
            //SfSkinManager.SetTheme(this, new Theme("FluentDarkTurq;FluentDark"));
            ////SfSkinManager.SetTheme(this, new FluentTheme());
            ////SfSkinManager.SetTheme(this, new FluentTheme("FluentDarkTurq"));
            //SfSkinManager.ApplyStylesOnApplication = true;
            InitializeComponent();

   
        }
    }
}
