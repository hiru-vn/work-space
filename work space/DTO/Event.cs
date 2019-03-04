using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class Event
    {
        int id;
        string name;
        DateTime? date;
        TimeSpan? time;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public DateTime? Date { get => date; set => date = value; }
        public TimeSpan? Time { get => time; set => time = value; }

        public Event() { }
        public Event(DataRow row)
        {
            this.id = int.Parse(row["id"].ToString());
            this.name = row["name"].ToString();
            this.date = (DateTime?)row["date"];
            this.time = (TimeSpan?)row["time"];
        }
    }
}
