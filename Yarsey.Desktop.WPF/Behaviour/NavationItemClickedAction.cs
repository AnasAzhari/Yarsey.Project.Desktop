using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Yarsey.Desktop.WPF.View;


namespace Yarsey.Desktop.WPF.Behaviour
{
    public class NavationItemClickedAction : TargetedTriggerAction<Grid>
    {

        private readonly UserControl _homeView;

        private readonly UserControl _customerView;

        UserControl userControl = null;

        //public NavationItemClickedAction()
        //{

        //}
        //public NavationItemClickedAction(List<UserControl> userControls)
        //{
        //    _homeView = userControls.Where(s => s.GetType() == typeof(HomeView)).FirstOrDefault();
        //    _customerView=userControls.Where(s => s.GetType() == typeof(CustomerView)).FirstOrDefault();
        //}
        protected override void Invoke(object parameter)
        {
            var args = parameter as NavigationItemClickedEventArgs;
            if (args == null)
                return;

            if (args.Item.ItemType != ItemType.Tab)
                return;

            var pagename = args.Item.Header.ToString();


            if (pagename == "Home")
            {
                userControl = new HomeView();
            }
            else if (pagename == "Customer")
            {
                userControl = new CustomerView();
            }
            ((this.Target as Grid).Children[1] as UserControl).Content = userControl;
        }
    }
}
