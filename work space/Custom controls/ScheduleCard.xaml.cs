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

namespace work_space.Custom_controls
{
    /// <summary>
    /// Interaction logic for ScheduleCard.xaml
    /// </summary>
    public partial class ScheduleCard : UserControl
    {
        ScheduleItem scheduleItem;
        public ScheduleCard()
        {
            InitializeComponent();
        }
        public ScheduleCard(ScheduleItem item)
        {
            InitializeComponent();
            this.scheduleItem = item;
            this.Content.Text = item.Title;
            this.Time.Text = (item.Starttime ?? TimeSpan.MinValue).ToString(@"h\:mm") + "-" + (item.Endtime ?? TimeSpan.MinValue) .ToString(@"h\:mm");
            this.Place.Text = item.Place;
            if (string.IsNullOrEmpty(this.Place.Text)) this.Place.Visibility = Visibility.Collapsed;
            if (!string.IsNullOrEmpty(item.Hexcolor)) this.frontButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(item.Hexcolor));
        }
        public void setContent(string s)
        {
            this.Content.Text = s;
        }
        public void setTime(TimeSpan? time1,TimeSpan? time2)
        {
            this.Time.Text = (time1 ?? TimeSpan.MinValue).ToString(@"h\:mm") + "-" + (time2 ?? TimeSpan.MinValue).ToString(@"h\:mm");
        }
        public void place(string place)
        {
            this.Place.Text = place;
        }

        private void Flipper_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show((sender as MaterialDesignThemes.Wpf.Flipper).Width.ToString());
        }

        private void ControlSC_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("UC "+(sender as ScheduleCard).Width.ToString());
        }
        private void dd(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("STK "+(sender as StackPanel).Width.ToString());
        }

        private void DeleteScheduleItem(object sender, RoutedEventArgs e)
        {
            (this.Parent as Panel).Children.Remove(this);
            ScheduleDAO.Instance.DeleteScheduleByID(scheduleItem.Id);
        }

        private void EditScheduleItem(object sender, RoutedEventArgs e)
        {
            WindowAddScheduleItem window = new WindowAddScheduleItem(scheduleItem);
            if (window.ShowDialog()==true)
            {
                ScheduleItem itemChange = window.GetChange();
                this.Content.Text = itemChange.Title;
                this.Time.Text = (itemChange.Starttime ?? TimeSpan.MinValue).ToString(@"h\:mm") + "-" + (itemChange.Endtime ?? TimeSpan.MinValue).ToString(@"h\:mm");
                this.Place.Text = itemChange.Place;
                if (string.IsNullOrEmpty(this.Place.Text)) this.Place.Visibility = Visibility.Collapsed;
                if (!string.IsNullOrEmpty(itemChange.Hexcolor)) this.frontButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(itemChange.Hexcolor));
            }
        }
    }
}
