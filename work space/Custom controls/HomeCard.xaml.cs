using Dragablz;
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

namespace work_space.Custom_controls
{
    /// <summary>
    /// Interaction logic for HomeCard.xaml
    /// </summary>
    public partial class HomeCard : UserControl
    {
        private int _tabItemIndex=-1;
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
            public HomeCard()
        {
            InitializeComponent();
        }
        public HomeCard SetSource(string s)
        {
            this.contentImage.Source = new BitmapImage(new Uri(s,UriKind.RelativeOrAbsolute));
            return this;
        }
        public HomeCard SetTitle(string s)
        {
            this.textboxtitle.Text = s;
            return this;
        }
        public HomeCard SetContent(string s)
        {
            this.textboxcontent.Text = s;
            return this;
        }
        public HomeCard SetTabIndex(int index)
        {
            this._tabItemIndex = index;
            return this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabablzControl tab = FindChild<TabablzControl>(Application.Current.MainWindow, "MainTabControl");
            if (this._tabItemIndex >= 0 && this._tabItemIndex <= 5) tab.SelectedIndex = this._tabItemIndex;
        }
    }
}
