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
using work_space.DAO;

namespace work_space
{
    /// <summary>
    /// Interaction logic for windowAddCategory.xaml
    /// </summary>
    public partial class windowAddCategory : Window
    {
        public windowAddCategory()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbtitle.Text) && ! string.IsNullOrWhiteSpace(txbtitle.Text))
            {
                AccountDAO.Instance.InsertAccountCategory(txbtitle.Text);
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
