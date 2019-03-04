using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using work_space.DTO;

namespace work_space.DAO
{
    public class ScheduleDAO
    {
        private static ScheduleDAO instance;

        public static ScheduleDAO Instance
        {
            get { if (instance == null) instance = new ScheduleDAO(); return instance; }
            private set { instance = value; }
        }
        private ScheduleDAO() { }

        public List<ScheduleItem> GetScheduleItemByDayofWeek(int day, int weektype)
        {
            string query = string.Format("select * from dbo.scheduleitem where dayinweek = {0} and weektype = {1} order by starttime ",day,weektype);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            List<ScheduleItem> list = new List<ScheduleItem>();
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new ScheduleItem(row)); }
            return list;
        }
        public int InsertSchedule(ScheduleItem item)
        {
            string query = string.Format("insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime,weektype,hexcolor) values (N'{0}',N'{1}',{2},'{3}','{4}',{5},'{6}')", item.Title, item.Place, item.Dayinweek, (item.Starttime ?? TimeSpan.MinValue).ToString(@"h\:mm"), (item.Endtime ?? TimeSpan.MinValue).ToString(@"h\:mm"), item.Weektype, item.Hexcolor);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int DeleteScheduleByID(int id)
        {
            string query = string.Format("delete from dbo.scheduleitem where id = {0}", id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int UpdateScheduleByID(int id, ScheduleItem item)
        {
            string query = string.Format("update dbo.scheduleitem set title = N'{0}', place = N'{1}', starttime = '{2}', endtime= '{3}', hexcolor = '{4}' where id = {5} ",item.Title, item.Place, (item.Starttime ?? TimeSpan.MinValue).ToString(@"h\:mm"), (item.Endtime ?? TimeSpan.MinValue).ToString(@"h\:mm"), item.Hexcolor, id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
    }
}
