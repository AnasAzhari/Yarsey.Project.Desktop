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

namespace Yarsey.WPF.CustomControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Yarsey.WPF.CustomControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Yarsey.WPF.CustomControl;assembly=Yarsey.WPF.CustomControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ModalControl/>
    ///
    /// </summary>
    public class ModalControl : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty =
              DependencyProperty.Register("IsOpen", typeof(bool), typeof(ModalControl),
                  new PropertyMetadata(false));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        static ModalControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalControl), new FrameworkPropertyMetadata(typeof(ModalControl)));
            BackgroundProperty.OverrideMetadata(typeof(ModalControl), new FrameworkPropertyMetadata(CreateDefaultBackground()));
        }

        private static object CreateDefaultBackground()
        {
            return new SolidColorBrush(Colors.Black)
            {
                Opacity = 0.3
            };
        }
    }
}
