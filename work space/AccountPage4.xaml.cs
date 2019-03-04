using MaterialDesignThemes.Wpf;
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
using work_space.DAO;
using work_space.DTO;

namespace work_space
{
    /// <summary>
    /// Interaction logic for AccountPage4.xaml
    /// </summary>
    public partial class AccountPage4 : Page
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
        public AccountPage4()
        {
            InitializeComponent();
        }
        
        private void Button_AddField(object sender, RoutedEventArgs e)
        {
            this.ButtonSaveField.Visibility = Visibility.Visible;
            (sender as Button).Visibility = Visibility.Collapsed;
            StkPanelCustomField.Visibility = Visibility.Visible;

            this.Account_Page4_txbFieldContent.Text = "";
            this.Account_Page4_txbFieldName.Text = "";
        }

        private void Button_SaveField(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Account_Page4_txbFieldName.Text) && !string.IsNullOrWhiteSpace(this.Account_Page4_txbFieldName.Text))
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
                textBox.Text = this.Account_Page4_txbFieldContent.Text;
                HintAssist.SetHint(textBox, this.Account_Page4_txbFieldName.Text);
                HintAssist.SetIsFloating(textBox, true);

                AccountCustomField customField = new AccountCustomField();
                customField.Content = this.Account_Page4_txbFieldContent.Text;
                customField.Title = this.Account_Page4_txbFieldName.Text;
                textBox.Tag = customField;

                dockPanel.Children.Add(packIcon);
                dockPanel.Children.Add(textBox);

                this.StkPanelAccountInfo.Children.Add(dockPanel);

                this.ButtonAddField.Visibility = Visibility.Visible;
                (sender as Button).Visibility = Visibility.Collapsed;
                StkPanelCustomField.Visibility = Visibility.Collapsed;

                // save change to database
            }
            //else
            //{
            //    TextBlock textBlock = new TextBlock();
            //    textBlock.Text = "Field title can not be empty";
            //    textBlock.Foreground = Brushes.MediumVioletRed;
            //    this.StkPanelAccountInfo.Children.Add(textBlock);
            //}
        }

        private void Account_textboxpassword_textChanged(object sender, TextChangedEventArgs e)
        {
            int value = PasswordCheck.GetPasswordStrength((sender as TextBox).Text) * 20;
            this.Account_BarCheckPasswordStrength.Value = value;
        }
        public void SaveRecord(int idCategory)
        {
            if (string.IsNullOrEmpty(this.AccountPage4_txbtitle.Text) || string.IsNullOrWhiteSpace(this.AccountPage4_txbtitle.Text)) return;
            Account acc = new Account();
            acc.Title = this.AccountPage4_txbtitle.Text;
            acc.Username = this.AccountPage4_txbuser.Text;
            acc.Apassword = this.AccountPage4_txbpassword.Text;
            acc.Website = this.AccountPage4_txbwebadress.Text;
            acc.Idcategory = idCategory;
            AccountDAO.Instance.InsertAccount(acc);

            foreach (TextBox tb in FindVisualChildren<TextBox>(this))
            {
                if (tb.Tag is AccountCustomField)
                {
                    AccountCustomField customField = tb.Tag as AccountCustomField;
                    customField.IdAccount = AccountDAO.Instance.GetMaxAccountID();
                    AccountDAO.Instance.InsertCustomField(customField);
                }
            }
        }
    }
}
