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
using work_space.DTO;

namespace work_space
{
    /// <summary>
    /// Interaction logic for WindowEvent.xaml
    /// </summary>
    public partial class WindowEvent : Window
    {
        public WindowEvent()
        {
            InitializeComponent();
        }
        private void accept(object sender, RoutedEventArgs e)
        {
            //save data or insert data
            if ((!string.IsNullOrEmpty(this.TxbName.Text) || !string.IsNullOrWhiteSpace(this.TxbName.Text)) && this.datepicker != null)
            {
                Event @event = new Event();
                @event.Name = this.TxbName.Text;
                @event.Time = (this.timepicker.SelectedTime ?? DateTime.MinValue).TimeOfDay;
                @event.Date = this.datepicker.SelectedDate ?? DateTime.MinValue;
                EventDAO.Instance.InsertEvent(@event);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
