using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using work_space.Custom_controls;
using work_space.DAO;
using work_space.DTO;

namespace work_space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUp();
        }
        public void SetUp()
        {
            HomeDefault();
            ScheduleDefault();
            TaskDefault();
            DiaryDefault();
            AccountsDefault();
            DocumentDefault();
        }

        #region Task
        private void TaskDefault()
        {
            this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByDate(DateTime.Now).DefaultView;
        }
        private void TextBoxTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string content = this.TextBoxTask.Text;
                if (content != "" && content != null)
                    TaskDAO.Instance.InsertTask(content);
                TaskDefault();
            }
        }
        private void Task_comboboxday_DropDownClosed(object sender, EventArgs e)
        {
            string filter = Task_comboboxsort.SelectedValue.ToString();
            if (filter == "Priority") filter = "IDPriority";
            string filter2 = Task_comboboxday.SelectedValue.ToString();
            if (filter2 == "Today")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByDate(DateTime.Now, filter).DefaultView;
            else if (filter2 == "Tomorrow")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByDate(DateTime.Now.AddDays(1), filter).DefaultView;
            else if (filter2 == "This week")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByWeek(DateTime.Now, filter).DefaultView;
            else if (filter2 == "This month")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByMonth(DateTime.Now, filter).DefaultView;
            else if (filter2 == "Anytime")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTask(filter).DefaultView;
        }

        private void Task_comboboxsort_SelectionChanged(object sender, EventArgs e)
        {
            string filter = Task_comboboxsort.SelectedValue.ToString();
            if (filter == "Priority") filter = "IDPriority";
            string filter2 = Task_comboboxday.SelectedValue.ToString();
            if (filter2 == "Today")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByDate(DateTime.Now, filter).DefaultView;
            else if (filter2 == "Tomorrow")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByDate(DateTime.Now.AddDays(1), filter).DefaultView;
            else if (filter2 == "This week")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByWeek(DateTime.Now, filter).DefaultView;
            else if (filter2 == "This month")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTaskByMonth(DateTime.Now, filter).DefaultView;
            else if (filter2 == "Anytime")
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTask(filter).DefaultView;
        }
        private void Task_comboboxpriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            int priority = cb.SelectedIndex;
            int id = -1;
            if (cb.Tag != null) id = int.Parse(cb.Tag.ToString());
            if (id >= 0) TaskDAO.Instance.UpdateTaskPriorityByID(id, priority);
        }
        private void Task_checkboxchecked_Clicked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int id = -1;
            if (cb.Tag != null) id = int.Parse(cb.Tag.ToString());
            int isChecked = -1;
            if (cb.IsChecked == true) isChecked = 1;
            if (cb.IsChecked == false) isChecked = 0;
            if (id >= 0 && isChecked >= 0) TaskDAO.Instance.UpdateTaskCheckedByID(id, isChecked);
        }
        private void Task_buttondelete_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = -1;
            if (btn.Tag != null) id = int.Parse(btn.Tag.ToString());
            if (id >= 0 && MyMessageBox.setContent("Delete this task?").ShowDialog() == true)
            {
                TaskDAO.Instance.DeleteTaskByID(id);
                this.listviewtask.ItemsSource = TaskDAO.Instance.GetListTask().DefaultView;
            }
        }
        private void Task_calendardate_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            StackPanel stackPanel = datePicker.Parent as StackPanel;
            int id = -1;
            if (stackPanel.Tag != null) id = int.Parse(stackPanel.Tag.ToString());
            foreach (object child in stackPanel.Children)
            {
                TimePicker timePicker;
                if (child is TimePicker)
                {
                    timePicker = child as TimePicker;
                    if (timePicker.SelectedTime != null)
                    {
                        DateTime dtd = datePicker.SelectedDate ?? DateTime.Now;
                        DateTime dtt = timePicker.SelectedTime ?? DateTime.Now;
                        DateTime? dateTime = dtd.Add(dtt.TimeOfDay);
                        TaskDAO.Instance.UpdateTaskDeadlineByID(id, dateTime);
                    }
                }
            }
        }
        private void Task_calendartime_SelectionChange(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            TimePicker timePicker = sender as TimePicker;
            StackPanel stackPanel = timePicker.Parent as StackPanel;
            int id = -1;
            if (stackPanel.Tag != null) id = int.Parse(stackPanel.Tag.ToString());
            foreach (object child in stackPanel.Children)
            {
                DatePicker datePicker;
                if (child is DatePicker)
                {
                    datePicker = child as DatePicker;
                    if (datePicker.SelectedDate != null)
                    {
                        DateTime dtd = datePicker.SelectedDate ?? DateTime.Now;
                        DateTime dtt = timePicker.SelectedTime ?? DateTime.Now;
                        DateTime? dateTime = dtd.Add(dtt.TimeOfDay);
                        TaskDAO.Instance.UpdateTaskDeadlineByID(id, dateTime);
                    }
                }
            }
        }
        #endregion

        #region Diary
        public void DiaryDefault()
        {
            this.Diary_calendar.SelectedDate = DateTime.Now;
            this.Story_textbox.Foreground = Brushes.Black;
        }
        public void GetListDiaryTitle()
        {
            List<string> diarytitle = DiaryDAO.Instance.GetListDiaryTitleByDate(this.Diary_calendar.SelectedDate);
            this.Story_titlelistbox.Items.Clear();
            foreach (string s in diarytitle)
            {
                this.Story_titlelistbox.Items.Add(s);
            }
            if (diarytitle.Count > 0)
                Story_titlelistbox.SelectedIndex = 0;
        }
        private void Story_textboxfastadd_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!string.IsNullOrEmpty(tb.Text))
            {
                this.Diary_ButtonClearTextbox.Visibility = Visibility.Visible;
            }
            else
            {
                this.Diary_ButtonClearTextbox.Visibility = Visibility.Collapsed;
            }
        }
        private void Diary_ButtonClearTextbox_Clicked(object sender, RoutedEventArgs e)
        {
            this.Story_textboxfastadd.Clear();
            (sender as Button).Visibility = Visibility.Collapsed;
        }
        private void Story_listboxfontstyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ListBoxItem> mySelectedItems = new List<ListBoxItem>();
            ListBox lb = sender as ListBox;
            bool flagBold = false, flagItalic = false, flagUnderline = false;
            foreach (ListBoxItem item in lb.SelectedItems)
            {
                if (item.Tag != null)
                {
                    if (item.Tag.ToString() == "Bold") { this.Story_textbox.FontWeight = FontWeights.Bold; flagBold = true; }
                    else if (item.Tag.ToString() == "Italic") { this.Story_textbox.FontStyle = FontStyles.Italic; flagItalic = true; }
                    else if (item.Tag.ToString() == "Underline") { this.Story_textbox.TextDecorations = TextDecorations.Underline; flagUnderline = true; }
                }
            }
            if (flagBold == false) this.Story_textbox.FontWeight = FontWeights.Normal;
            if (flagItalic == false) this.Story_textbox.FontStyle = FontStyles.Normal;
            if (flagUnderline == false) this.Story_textbox.TextDecorations = null;
        }
        private void Diary_calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListDiaryTitle();
        }
        private void Diary_dialoghost_Closing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            MaterialDesignThemes.Wpf.DialogHost dialog = sender as MaterialDesignThemes.Wpf.DialogHost;
            if (!Equals(eventArgs.Parameter, true)) return;
            if (!string.IsNullOrWhiteSpace(Story_textboxadd.Text))
            {
                string title = Story_textboxadd.Text.Trim();
                if (!Story_titlelistbox.Items.Contains(title))
                {
                    this.Story_titlelistbox.Items.Add(title);

                    DiaryDAO.Instance.InsertDiary(this.Diary_calendar.SelectedDate, title);
                }
            }
            if (Story_titlelistbox.Items.Count > 0)
                Story_titlelistbox.SelectedIndex = 0;
        }
        private void Diary_titlelistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Story_textbox.Text = "";
            this.Story_textbox.FontWeight = FontWeights.Normal;
            this.Story_textbox.FontStyle = FontStyles.Normal;
            this.Story_textbox.TextDecorations = null;
            if (this.Story_titlelistbox.Items.Count > 0)
            {
                Diary diary = DiaryDAO.Instance.GetDiaryByDateAndTitle(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedValue.ToString());
                if (!string.IsNullOrWhiteSpace(diary.Story))
                {
                    this.Story_textbox.Text = diary.Story;
                    this.Story_textbox.FontFamily = new FontFamily(diary.Fontfamily);
                    this.Story_textbox.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString(diary.Fontcolor));
                    this.Story_textbox.FontSize = diary.Fontsize;
                    if (diary.Fontstyle == "Italic") { this.Story_textbox.FontStyle = FontStyles.Italic; }
                    else if (diary.Fontstyle == "Bold") { this.Story_textbox.FontWeight = FontWeights.Bold; }
                    else if (diary.Fontstyle == "Underline") { this.Story_textbox.TextDecorations = TextDecorations.Underline; }
                }
            }
        }
        private void Story_buttonsave_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.Story_titlelistbox.Items.Count > 0)
                DiaryDAO.Instance.UpdateDiaryStory(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedItem.ToString(), this.Story_textbox.Text);
            else
            {
                string content = this.Story_textbox.Text;
                if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content))
                    DiaryDAO.Instance.InsertDiary(this.Diary_calendar.SelectedDate, DateTime.Now.ToShortTimeString(), content);
            }
            GetListDiaryTitle();
        }

        private void Story_comboboxfontfamily_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            this.Story_textbox.FontFamily = new FontFamily(this.Story_comboboxfontfamily.SelectedValue.ToString());
            if (this.Story_titlelistbox.Items.Count > 0)
            {
                DiaryDAO.Instance.UpdateDiaryFontFamily(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedItem.ToString(), (sender as ComboBox).SelectedValue.ToString());
            }
        }

        private void Story_comboboxfontcolor_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            this.Story_textbox.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString((sender as ComboBox).SelectedValue.ToString()));
            if (this.Story_titlelistbox.Items.Count > 0)
            {
                DiaryDAO.Instance.UpdateDiaryFontColor(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedItem.ToString(), (sender as ComboBox).SelectedValue.ToString());
            }
        }

        private void Story_comboboxfontsize_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            this.Story_textbox.FontSize = int.Parse((sender as ComboBox).SelectedValue.ToString());
            if (this.Story_titlelistbox.Items.Count > 0)
            {
                DiaryDAO.Instance.UpdateDiaryFontSize(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedItem.ToString(), int.Parse((sender as ComboBox).SelectedValue.ToString()));
            }
        }
        private void Story_textboxfastadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string content = this.Story_textboxfastadd.Text;
                if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrEmpty(content))
                    DiaryDAO.Instance.InsertDiary(DateTime.Now, DateTime.Now.ToShortTimeString(), content);
            }
            GetListDiaryTitle();
        }

        private void Story_buttondelete_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.Story_titlelistbox.Items.Count > 0)
            {
                if (MyMessageBox.setContent("Bạn thực sự muốn xóa?").ShowDialog() == true)
                {
                    DiaryDAO.Instance.DeleteDiary(this.Diary_calendar.SelectedDate, this.Story_titlelistbox.SelectedItem.ToString());
                    GetListDiaryTitle();
                }
            }
        }
        #endregion

        #region Home
        public void HomeDefault()
        {
            this.Home_cardSchedule.SetSource("pack://siteoforigin:,,,/Resources/schedule2.jpg").SetTitle("Schedule").SetContent("Planning your week").SetTabIndex(1);
            this.Home_cardTask.SetSource("pack://siteoforigin:,,,/Resources/task.png").SetTitle("Task").SetContent("Never miss a deadline again").SetTabIndex(2);
            this.Home_cardDiary.SetSource("pack://siteoforigin:,,,/Resources/diary.png").SetTitle("Diary").SetContent("Release inner thoughts").SetTabIndex(3);
            this.Home_cardAccount.SetSource("pack://siteoforigin:,,,/Resources/account.png").SetTitle("Account").SetContent("Never forget your account").SetTabIndex(4);
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        #endregion

        #region Schedule
        public void ScheduleDefault()
        {
            GetListEvent();
            List<ScheduleItem> listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(2, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_2.Children.Add(card);
            }
            listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(3, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_3.Children.Add(card);
            }
            listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(4, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_4.Children.Add(card);
            }
            listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(5, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_5.Children.Add(card);
            }
            listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(6, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_6.Children.Add(card);
            }
            listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(7, GetWeekType());
            foreach (ScheduleItem item in listmon)
            {
                ScheduleCard card = new ScheduleCard(item);
                this.Schedule_stackpanel_7.Children.Add(card);
            }
        }
        public void clearSchedule()
        {
            this.Schedule_stackpanel_2.Children.Clear();
            this.Schedule_stackpanel_3.Children.Clear();
            this.Schedule_stackpanel_4.Children.Clear();
            this.Schedule_stackpanel_5.Children.Clear();
            this.Schedule_stackpanel_6.Children.Clear();
            this.Schedule_stackpanel_7.Children.Clear();
        }
        private int GetWeekType()
        {
            if (this.Schedule_buttonweek1.Visibility == Visibility.Collapsed) return 0;
            else
            {
                if (this.Schedule_buttonweek1.IsEnabled) return 1;
                return 0;
            }
        }
        private void GetListEvent()
        {
            this.Schedule_eventlist.Items.Clear();
            List<Event> list = EventDAO.Instance.GetListEvent();
            foreach (Event @event in list)
            {
                string content = @event.Name + " " + (@event.Date ?? DateTime.MinValue).ToString("d/M", CultureInfo.InvariantCulture);
                if (@event.Time != null) content = content + " " + (@event.Time ?? TimeSpan.MinValue).ToString(@"h\:mm");
                DockPanel dockPanel = new DockPanel();
                PackIcon icon = new PackIcon();
                icon.Kind = PackIconKind.MicrosoftXamarin;
                Button button = new Button();
                button.Content = icon;
                button.SetResourceReference(Control.StyleProperty, "MaterialDesignToolButton");
                button.Visibility = Visibility.Collapsed;
                button.Click += Schedule_deleteevent;
                button.Tag = @event.Id;
                button.Foreground = (Brush)(new BrushConverter().ConvertFrom("#FF673AB7"));
                button.VerticalAlignment = VerticalAlignment.Center;
                TextBlock textBlock = new TextBlock();
                textBlock.Foreground = (Brush)(new BrushConverter().ConvertFrom("#FF673AB7"));
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Text = content;
                textBlock.TextWrapping = TextWrapping.Wrap;
                dockPanel.Children.Add(button);
                dockPanel.Children.Add(textBlock);

                this.Schedule_eventlist.Items.Add(dockPanel);
            }
        }
        private void Schedule_deleteevent(object sender, RoutedEventArgs e)
        {
            if (MyMessageBox.setContent("Delete this event?").ShowDialog() == true)
            {
                if ((sender as Button).Parent is DockPanel)
                {
                    this.Schedule_eventlist.Items.Remove((sender as Button).Parent as DockPanel);
                    EventDAO.Instance.DeleteEvent(int.Parse((sender as Button).Tag.ToString()));
                }
            }
        }
        private void Schedule_EventSelected(object sender, SelectionChangedEventArgs e)
        {
            DockPanel dock = (sender as ListBox).SelectedItem as DockPanel;
            foreach (object obj in (sender as ListBox).Items)
            {
                if ((obj as DockPanel) == dock)
                {
                    foreach (object obj2 in (obj as DockPanel).Children)
                    {
                        if (obj2 is Button)
                            (obj2 as Button).Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    foreach (object obj2 in (obj as DockPanel).Children)
                    {
                        if (obj2 is Button)
                            (obj2 as Button).Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void SwitchRoutine(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked == true)
            {
                this.Schedule_buttonweek1.Visibility = Visibility.Visible;
                this.Schedule_buttonweek2.Visibility = Visibility.Visible;
                //this.Schedule_buttonweek2.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
                //this.Schedule_buttonweek2.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
                //this.Schedule_buttonweek1.Background = (Brush)new BrushConverter().ConvertFrom("#FFD1C4E9");
                //this.Schedule_buttonweek1.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFD1C4E9");
                this.Schedule_stackpanelroutine.Orientation = Orientation.Vertical;
            }
            else
            {
                this.Schedule_buttonweek1.Visibility = Visibility.Collapsed;
                this.Schedule_buttonweek2.Visibility = Visibility.Collapsed;
                this.Schedule_stackpanelroutine.Orientation = Orientation.Horizontal;
            }
            this.Schedule_buttonweek1.IsEnabled = false;
            this.Schedule_buttonweek2.IsEnabled = true;
            clearSchedule();
            ScheduleDefault();
        }

        private void SwitchRoutineButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Schedule_buttonweek1.IsEnabled == false)
            {
                this.Schedule_buttonweek1.IsEnabled = true;
                this.Schedule_buttonweek2.IsEnabled = false;
                //week1
                clearSchedule();
                ScheduleDefault();
            }
            else
            {
                this.Schedule_buttonweek2.IsEnabled = true;
                this.Schedule_buttonweek1.IsEnabled = false;
                //week2
                clearSchedule();
                ScheduleDefault();
            }
        }

        private void Schedule_newwork(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Parent is StackPanel)
            {
                StackPanel panel = button.Parent as StackPanel;
                foreach (object obj in panel.Children)
                {
                    if (obj is StackPanel)
                    {
                        StackPanel stk = obj as StackPanel;

                        WindowAddScheduleItem window = new WindowAddScheduleItem(GetWeekType(), int.Parse(stk.Tag.ToString()));
                        if (window.ShowDialog() == true)
                        {
                            List<ScheduleItem> listmon = ScheduleDAO.Instance.GetScheduleItemByDayofWeek(int.Parse(stk.Tag.ToString()), GetWeekType());
                            stk.Children.Clear();
                            foreach (ScheduleItem item in listmon)
                            {
                                stk.Children.Add(new ScheduleCard(item));
                            }
                        }
                    }
                }
            }

        }

        private void Schedule_newevent(object sender, RoutedEventArgs e)
        {
            WindowEvent window = new WindowEvent();
            if (window.ShowDialog() == true)
            {
                GetListEvent();
            }
        }
        #endregion

        #region Accounts
        public void AccountsDefault()
        {
            if (this.Account_comboboxShowMode.SelectedIndex == 1)
            {
                FileMode();
            }
            else
            {
                FolderMode();
            }
        }
        public void FileMode()
        {
            this.Account_treeview.Items.Clear();
            List<Account> list = new List<Account>();
            list = AccountDAO.Instance.SearchAccount(this.Account_textboxsearch.Text);
            foreach (Account acc in list)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                PackIcon icon = new PackIcon();
                icon.Kind = PackIconKind.FileAccount;
                icon.Margin = new Thickness(-5, 0, 5, 0);
                TextBlock textBlock = new TextBlock();
                textBlock.Text = acc.Title;
                stackPanel.Children.Add(icon);
                stackPanel.Children.Add(textBlock);
                TreeViewItem item = new TreeViewItem();
                item.Header = stackPanel;
                item.Tag = acc;

                this.Account_treeview.Items.Add(item);
            }

            this.AccountFrame.Content = new AccountPage1();
        }
        public void FolderMode()
        {
            this.Account_treeview.Items.Clear();
            List<AccountCategory> list = new List<AccountCategory>();
            list = AccountDAO.Instance.SearchListAccountCategory(this.Account_textboxsearch.Text);
            foreach (AccountCategory accountCategory in list)
            {
                TreeViewItem treeViewItem = new TreeViewItem();
                List<Account> listacc = AccountDAO.Instance.GetListAccountByCategoryID(accountCategory.Id);
                foreach (Account acc in listacc)
                {
                    TreeViewItem treeViewItem2 = new TreeViewItem();
                    treeViewItem2.Tag = acc;

                    //recordheader
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    PackIcon icon = new PackIcon();
                    icon.Kind = PackIconKind.FileAccount;
                    icon.Margin = new Thickness(-5, 0, 5, 0);
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = acc.Title;
                    stackPanel.Children.Add(icon);
                    stackPanel.Children.Add(textBlock);
                    treeViewItem2.Header = stackPanel;

                    treeViewItem.Items.Add(treeViewItem2);
                }
                StackPanel stackPanel2 = new StackPanel();
                stackPanel2.Orientation = Orientation.Horizontal;
                PackIcon icon2 = new PackIcon();
                icon2.Kind = PackIconKind.FolderOpen;
                icon2.Margin = new Thickness(-5, 0, 5, 0);
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = accountCategory.Content;
                stackPanel2.Children.Add(icon2);
                stackPanel2.Children.Add(textBlock2);
                treeViewItem.Header = stackPanel2;

                treeViewItem.Tag = accountCategory;
                this.Account_treeview.Items.Add(treeViewItem);
            }
            List<Account> listaccnocat = AccountDAO.Instance.SearchListAccountByCategoryID(1, this.Account_textboxsearch.Text);
            foreach (Account account in listaccnocat)
            {
                if (!string.IsNullOrEmpty(account.Title) && !string.IsNullOrWhiteSpace(account.Title))
                {
                    TreeViewItem treeViewItem = new TreeViewItem();
                    treeViewItem.Tag = account;
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    PackIcon icon = new PackIcon();
                    icon.Kind = PackIconKind.FileAccount;
                    icon.Margin = new Thickness(-5, 0, 5, 0);
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = account.Title;
                    stackPanel.Children.Add(icon);
                    stackPanel.Children.Add(textBlock);
                    treeViewItem.Header = stackPanel;
                    this.Account_treeview.Items.Add(treeViewItem);
                }
            }
            this.AccountFrame.Content = new AccountPage1();
        }
        private void Account_comboboxShowMode_selectionChange(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 1)
            {
                FileMode();
            }
            else
            {
                FolderMode();
            }
        }
        private void Account_textboxsearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!string.IsNullOrEmpty(tb.Text))
            {
                this.Account_buttonclearsearchtext.Visibility = Visibility.Visible;
            }
            else
            {
                this.Account_buttonclearsearchtext.Visibility = Visibility.Collapsed;
            }
            if (this.Account_comboboxShowMode.SelectedIndex == 1) FileMode();
            else FolderMode();
        }
        private void Account_buttonclearsearchtext_Clicked(object sender, RoutedEventArgs e)
        {
            this.Account_textboxsearch.Clear();
            (sender as Button).Visibility = Visibility.Collapsed;
        }
        private void Account_buttonaddcategory_Clicked(object sender, RoutedEventArgs e)
        {
            windowAddCategory window = new windowAddCategory();
            window.ShowDialog();
            AccountsDefault();
        }
        private void Account_buttondelete(object sender, RoutedEventArgs e)
        {
            Object obj = this.Account_treeview.SelectedItem;
            if (obj is TreeViewItem)
            {
                Object obj2 = (obj as TreeViewItem).Tag;
                if (obj2 is AccountCategory)
                {
                    if (MyMessageBox.setContent("bạn muốn xóa chủ đề " + (obj2 as AccountCategory).Content + " ? ").ShowDialog() == true)
                    {
                        AccountDAO.Instance.DeleteAccountCategoryByID((obj2 as AccountCategory).Id);
                        AccountsDefault();
                    }
                }
                if (obj2 is Account)
                {
                    if (MyMessageBox.setContent("bạn muốn xóa tài khoản " + (obj2 as Account).Title + " ?").ShowDialog() == true)
                    {
                        AccountDAO.Instance.DeleteAccountByID((obj2 as Account).Id);
                        AccountsDefault();
                    }
                }
            }
        }

        private void Account_treeview_selecteditemChange(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Object obj = this.Account_treeview.SelectedItem;
            if (obj is TreeViewItem)
            {
                Object obj2 = (obj as TreeViewItem).Tag;
                if (obj2 is AccountCategory)
                {
                    this.AccountFrame.Content = new AccountPage3();
                }
                else if (obj2 is Account)
                {
                    this.AccountFrame.Content = new AccountPage2();

                    this.Account_PageButtonFix.Visibility = Visibility.Visible;
                    this.Account_PageButtonSave.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.AccountFrame.Content = new AccountPage1();
                }
            }
        }

        private void Account_PageButtonFix_clicked(object sender, RoutedEventArgs e)
        {
            if (this.AccountFrame.Content is AccountPage2)
            {
                AccountPage2 account = this.AccountFrame.Content as AccountPage2;
                account.EnableTextbox();

                this.Account_PageButtonFix.Visibility = Visibility.Collapsed;
                this.Account_PageButtonSave.Visibility = Visibility.Visible;
            }
        }

        private void Account_PageButtonSave_clicked(object sender, RoutedEventArgs e)
        {
            if (this.AccountFrame.Content is AccountPage2)
            {
                AccountPage2 account = this.AccountFrame.Content as AccountPage2;
                account.DisableTextbox();

                this.Account_PageButtonFix.Visibility = Visibility.Visible;
                this.Account_PageButtonSave.Visibility = Visibility.Collapsed;
                //save account
                account.SaveAccount();

            }
        }

        private void Account_PageButtonOkSave_clicked(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Collapsed;
            this.Account_PageButtonDelete.Visibility = Visibility.Visible;
            this.Account_PageButtonFix.Visibility = Visibility.Visible;
            int Idcategory = 1;
            //get current folder
            Object obj = this.Account_treeview.SelectedItem;
            if (obj is TreeViewItem)
            {
                Object obj2 = (obj as TreeViewItem).Tag;
                if (obj2 is AccountCategory)
                {
                    Idcategory = (obj2 as AccountCategory).Id;
                }
                if (obj2 is Account)
                {
                    Idcategory = (obj2 as Account).Idcategory;
                }
            }
            if (this.AccountFrame.Content is AccountPage4)
            {
                AccountPage4 account = this.AccountFrame.Content as AccountPage4;
                account.SaveRecord(Idcategory);

                AccountsDefault();
            }
        }

        private void Account_buttonnewrecord_Clicked(object sender, RoutedEventArgs e)
        {
            this.Account_PageButtonDelete.Visibility = Visibility.Collapsed;
            this.Account_PageButtonFix.Visibility = Visibility.Collapsed;
            this.Account_PageButtonSave.Visibility = Visibility.Collapsed;
            this.Account_PageButtonOkSave.Visibility = Visibility.Visible;

            this.AccountFrame.Content = new AccountPage4();
        }
        #endregion

        #region Document
        public void DocumentDefault() { }





        #endregion

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
    
        }

        private void Window_sizechange(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 1000)
                this.Home_cardAccount.Visibility = Visibility.Collapsed;
            else
                this.Home_cardAccount.Visibility = Visibility.Visible;
        }
    }
}
