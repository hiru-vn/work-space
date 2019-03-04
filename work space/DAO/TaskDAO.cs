using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DAO
{
    public class TaskDAO
    {
        private static TaskDAO instance;

        public static TaskDAO Instance
        {
            get { if (instance == null) instance = new TaskDAO(); return instance; }
            private set { instance = value; }
        }
        private TaskDAO() { }

        #region Get Task
        public DataTable GetListTask()
        {
            string query = "select t.id as[IDTask],  Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id ";
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByDate(DateTime date)
        {
            string query = string.Format("select t.id as[IDTask],  Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id " +
                "where day(deadline) = {0} and month(deadline) = {1} and year(deadline) = {2} or deadline is NULL ", date.Day,date.Month,date.Year) ;
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByWeek(DateTime date)
        {
            string query = string.Format("select  t.id as[IDTask], Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id "
                            + "where DATEPART(week, deadline) = {0} and year(deadline) = {1}  or deadline is NULL ", GetIWeekNumberOfYear(date), date.Year);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByMonth(DateTime date)
        {
            string query = string.Format("select t.id as[IDTask], Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id " +
                            "where month(deadline) = {0} and year(deadline) = {1}  or deadline is NULL ", date.Month, date.Year);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTask(string sort)
        {
            string query = "select t.id as[IDTask],  Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id order by "+sort;
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByDate(DateTime date, string sort)
        {
            string query = string.Format("select t.id as[IDTask],  Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id " +
                "where day(deadline) = {0} and month(deadline) = {1} and year(deadline) = {2} or deadline is NULL order by {3} ", date.Day, date.Month, date.Year,sort);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByWeek(DateTime date, string sort)
        {
            string query = string.Format("select  t.id as[IDTask], Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id "
                            + "where DATEPART(week, deadline) = {0} and year(deadline) = {1}  or deadline is NULL order by {2} ", GetIWeekNumberOfYear(date), date.Year,sort);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        public DataTable GetListTaskByMonth(DateTime date,string sort)
        {
            string query = string.Format("select t.id as[IDTask], Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t inner join dbo.Tpriority p on t.Tpriority = p.id " +
                            "where month(deadline) = {0} and year(deadline) = {1}  or deadline is NULL order by {2} ", date.Month, date.Year,sort);
            DataTable dataTable = DataProvider.Instance.ExcuteQuery(query);
            return dataTable;
        }
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIWeekNumberOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        #endregion

        #region Insert Task
        public int InsertTask(string content)
        {
            string query = string.Format("insert into dbo.task(content) values (N'{0}')", content);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        #endregion

        #region Update Task
        public int UpdateTaskPriorityByID(int id, int priority)
        {
            string query = string.Format("update dbo.Task set Tpriority = {0} where id = {1}", priority, id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int UpdateTaskCheckedByID(int id, int isChecked)
        {
            string query = string.Format("update dbo.Task set checked = {0} where id = {1}", isChecked, id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int UpdateTaskDeadlineByID(int id, DateTime? dl)
        {
            string query = string.Format("update dbo.Task set deadline = '{0}' where id = {1}", dl, id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        #endregion

        #region Delete Task
        public int DeleteTaskByID(int id)
        {
            string query = string.Format("delete from dbo.Task where id = {0}", id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        #endregion
    }
}
