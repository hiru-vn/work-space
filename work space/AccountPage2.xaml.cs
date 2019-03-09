using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for AccountPage2.xaml
    /// </summary>
    public partial class AccountPage2 : Page
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
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
        public AccountPage2()
        {
            InitializeComponent();
        }
        public void DisableTextbox()
        {
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                tb.IsEnabled = false;
            }
        }
        public void EnableTextbox()
        {
            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                tb.IsEnabled = true;
            }
        }
        public void SaveAccount()
        {
            if (this.Tag is Account)
            {
                Account acc = this.Tag as Account;
                acc.Title = this.AccountPage2_txbtitle.Text;
                acc.Apassword = this.AccountPage2_txbpassword.Text;
                acc.Username = this.AccountPage2_txbuser.Text;
                acc.Website = this.AccountPage2_txbwebadress.Text;
                AccountDAO.Instance.UpdateAccount(acc);
                foreach (TextBox tb in FindVisualChildren<TextBox>(this))
                {
                    if (tb.Tag is AccountCustomField)
                    {
                        AccountCustomField customField = tb.Tag as AccountCustomField;
                        customField.Content = tb.Text;
                        AccountDAO.Instance.UpdateCustomField(customField);
                    }
                }
            }
        }
        private void Button_AddField(object sender, RoutedEventArgs e)
        {
            this.ButtonSaveField.Visibility = Visibility.Visible;
            (sender as Button).Visibility = Visibility.Collapsed;
            StkPanelCustomField.Visibility = Visibility.Visible;

            this.Account_Page2_txbFieldContent.Text = "";
            this.Account_Page2_txbFieldName.Text = "";
        }

        private void Button_SaveField(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Account_Page2_txbFieldName.Text) && !string.IsNullOrWhiteSpace(this.Account_Page2_txbFieldName.Text))
            {
                //adding a dockpanel contain new field
                DockPanel dockPanel = new DockPanel();
                dockPanel.Margin = new Thickness(20);
                PackIcon packIcon = new PackIcon();
                packIcon.Kind = PackIconKind.Pen;
                packIcon.Height = 25;
                packIcon.Width = Double.NaN;
                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(7, -13, 0, 0);
                textBox.Text = this.Account_Page2_txbFieldContent.Text;
                textBox.IsEnabled = false;
                HintAssist.SetHint(textBox, this.Account_Page2_txbFieldName.Text);
                HintAssist.SetIsFloating(textBox, true);

                dockPanel.Children.Add(packIcon);
                dockPanel.Children.Add(textBox);

                this.StkPanelAccountInfo.Children.Add(dockPanel);

                this.ButtonAddField.Visibility = Visibility.Visible;
                (sender as Button).Visibility = Visibility.Collapsed;
                StkPanelCustomField.Visibility = Visibility.Collapsed;

                // save change to database
                AccountCustomField customField = new AccountCustomField();
                customField.Content = this.Account_Page2_txbFieldName.Text;
                customField.Title = this.Account_Page2_txbFieldContent.Text;
                customField.IdAccount = (this.Tag as Account).Id;
                AccountDAO.Instance.InsertCustomField(customField);
                textBox.Tag = customField;
                //else
                //{
                //    TextBlock textBlock = new TextBlock();
                //    textBlock.Text = "Field title can not be empty";
                //    textBlock.Foreground = Brushes.MediumVioletRed;
                //    this.StkPanelAccountInfo.Children.Add(textBlock);
                //}
            }
        }
        private void Account_textboxpassword_textChanged(object sender, TextChangedEventArgs e)
        {
            int value = PasswordCheck.GetPasswordStrength((sender as TextBox).Text) * 20;
            this.Account_BarCheckPasswordStrength.Value = value;
        }

        private void AccountPage2_visibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            TreeView tree = FindChild<TreeView>(Application.Current.MainWindow, "Account_treeview");
            Object obj = tree.SelectedItem;
            if (obj is TreeViewItem)
            {
                this.Tag = (obj as TreeViewItem).Tag;
            }
            if (this.Tag is Account)
            {
                Account acc = this.Tag as Account;
                this.AccountPage2_txbtitle.Text = acc.Title;
                this.AccountPage2_txbuser.Text = acc.Username;
                this.AccountPage2_txbpassword.Text = acc.Apassword;
                this.AccountPage2_txbwebadress.Text = acc.Website;

                //get custom field
                List<AccountCustomField> list = AccountDAO.Instance.GetCustomFieldByAccountID(acc.Id);
                foreach (AccountCustomField customField in list) {
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.Margin = new Thickness(20);
                    PackIcon packIcon = new PackIcon();
                    packIcon.Kind = PackIconKind.Pen;
                    packIcon.Height = 25;
                    packIcon.Width = Double.NaN;
                    TextBox textBox = new TextBox();
                    textBox.Margin = new Thickness(7, -13, 0, 0);
                    textBox.Text = customField.Content;
                    textBox.Tag = customField;
                    textBox.IsEnabled = false;
                    HintAssist.SetHint(textBox, customField.Title);
                    HintAssist.SetIsFloating(textBox, true);

                    dockPanel.Children.Add(packIcon);
                    dockPanel.Children.Add(textBox);

                    this.StkPanelAccountInfo.Children.Add(dockPanel);
                }
            }
        }

    }
}
