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
    /// Interaction logic for WindowAddScheduleItem.xaml
    /// </summary>
    public partial class WindowAddScheduleItem : Window
    {
        int weektype;
        int dayofweek;
        bool is2weekmode = false;
        ScheduleItem scheduleItem;

        public WindowAddScheduleItem(int weektype,int dayofweek)
        {
            //constructer for insert only
            InitializeComponent();
            this.weektype = weektype;
            this.dayofweek = dayofweek;
        }
        public WindowAddScheduleItem(int weektype, int dayofweek, bool is2weekmode)
        {
            //constructer for insert only
            InitializeComponent();
            this.weektype = weektype;
            this.dayofweek = dayofweek;
            this.is2weekmode = is2weekmode;
            if (is2weekmode) 
                this.Ckboxweekly.Visibility = Visibility.Visible;
        }
        public WindowAddScheduleItem(ScheduleItem item)
        {
            //constructer for update only
            InitializeComponent();
            this.scheduleItem = item;
            SetItemContent();
        }

        private void SetItemContent()
        {
            this.TxbName.Text = scheduleItem.Title;
            this.Location.Text = scheduleItem.Place;
            DateTime date = new DateTime(2019, 1, 1);
            date += (scheduleItem.Starttime ?? TimeSpan.MinValue);
            this.TimeStart.SelectedTime = date;
            date = new DateTime(2019, 1, 1);
            this.TimeFinish.SelectedTime = (date) += (scheduleItem.Endtime ?? TimeSpan.MinValue);
            if (!string.IsNullOrEmpty(scheduleItem.Hexcolor)) this.ColorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(scheduleItem.Hexcolor);
        }
        public ScheduleItem GetChange()
        {
            ScheduleItem item = new ScheduleItem();
            item.Title = this.TxbName.Text;
            item.Place = this.Location.Text;
            item.Dayinweek = this.dayofweek;
            item.Starttime = (TimeSpan?)(this.TimeStart.SelectedTime ?? DateTime.Now).TimeOfDay;
            item.Endtime = (TimeSpan?)(this.TimeFinish.SelectedTime ?? DateTime.Now).TimeOfDay;
            item.Hexcolor = (this.ColorPicker.SelectedColor ?? (Color)ColorConverter.ConvertFromString(item.Hexcolor)).ToString();
            item.Weektype = this.weektype;
            item.Id = scheduleItem.Id;
            return item;
        }

        private void accept(object sender, RoutedEventArgs e)
        {
            //save data or insert data
            if ((!string.IsNullOrEmpty(this.TxbName.Text) || !string.IsNullOrWhiteSpace(this.TxbName.Text))&&this.TimeStart.SelectedTime != null && this.TimeFinish.SelectedTime!=null)
            {
                ScheduleItem item = new ScheduleItem();
                item.Title = this.TxbName.Text;
                item.Place = this.Location.Text;
                item.Dayinweek = this.dayofweek;
                item.Starttime = (TimeSpan?) (this.TimeStart.SelectedTime ?? DateTime.Now).TimeOfDay;
                item.Endtime = (TimeSpan?)(this.TimeFinish.SelectedTime ?? DateTime.Now).TimeOfDay;
                item.Hexcolor = (this.ColorPicker.SelectedColor ?? (Color)ColorConverter.ConvertFromString(item.Hexcolor)).ToString();
                item.Weektype = this.weektype;
                if (this.scheduleItem == null)
                {
                    ScheduleDAO.Instance.InsertSchedule(item);
                    if (this.is2weekmode == true)
                    {
                        if (this.Ckboxweekly.IsChecked==true)
                        {
                            if (item.Weektype == 0) item.Weektype = 1;
                            else item.Weektype = 0;
                            ScheduleDAO.Instance.InsertSchedule(item);
                        }
                    }
                }
                else
                {
                    ScheduleDAO.Instance.UpdateScheduleByID(scheduleItem.Id,item);
                }
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
