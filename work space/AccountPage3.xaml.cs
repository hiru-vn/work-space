using System;
using System.Collections.Generic;
using System.Data;
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
using work_space.DAO;
using work_space.DTO;

namespace work_space
{
    /// <summary>
    /// Interaction logic for AccountPage3.xaml
    /// </summary>
    public partial class AccountPage3 : Page
    {
        public static T FindChild<T>(DependencyObject parent, string childName)
        where T : DependencyObject
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

        public AccountPage3()
        {
            InitializeComponent();
        }

        private void AccountPage3_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TreeView tree = FindChild<TreeView>(Application.Current.MainWindow, "Account_treeview");
            Object obj = tree.SelectedItem;
            if (obj is TreeViewItem)
            {
                this.Tag = (obj as TreeViewItem).Tag;
            }
            if (this.Tag is AccountCategory)
            {
                AccountCategory accountCategory = this.Tag as AccountCategory;
                this.AccountPage3_txbfoldertitle.Text = accountCategory.Content;
                DataTable list = AccountDAO.Instance.GetDataTableAccountByCategoryID(accountCategory.Id);
                this.Account_listviewfolder.ItemsSource = list.DefaultView;
            }
        }
    }
}
