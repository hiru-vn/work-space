using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using work_space.DTO;

namespace work_space.DAO
{
    public class EventDAO
    {
        private static EventDAO instance;

        public static EventDAO Instance
        {
            get { if (instance == null) instance = new EventDAO(); return instance; }
            private set { instance = value; }
        }
        private EventDAO() { }

        public List<Event> GetListEvent()
        {
            List<Event> list = new List<Event>();
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from dbo.event where date >= getdate() order by date");
            foreach (DataRow row in data.Rows)
            {
                list.Add(new Event(row));
            }
            return list;
        }
        public int InsertEvent(Event @event)
        {
            string query = string.Format("insert into dbo.event(name,date,time) values (N'{0}', '{1}/{2}/{3}', '{4}')",@event.Name,(@event.Date??DateTime.MinValue).Month, (@event.Date ?? DateTime.MinValue).Day, (@event.Date ?? DateTime.MinValue).Year, (@event.Time ?? TimeSpan.MinValue).ToString(@"h\:mm"));
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int DeleteEvent(int id)
        {
            string query = string.Format("delete from dbo.event where id = {0}", id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
    }
}
