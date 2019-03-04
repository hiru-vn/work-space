using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class ScheduleItem
    {
        int _id;
        string _title;
        string _place;
        int _dayinweek;
        TimeSpan? _starttime;
        TimeSpan? _endtime;
        string _hexcolor = "#FFFFFFFF";
        int weektype;

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public int Dayinweek { get => _dayinweek; set => _dayinweek = value; }
        public TimeSpan? Starttime { get => _starttime; set => _starttime = value; }
        public TimeSpan? Endtime { get => _endtime; set => _endtime = value; }
        public string Hexcolor { get => _hexcolor; set => _hexcolor = value; }
        public string Place { get => _place; set => _place = value; }
        public int Weektype { get => weektype; set => weektype = value; }

        public ScheduleItem() { }
        public ScheduleItem(DataRow data)
        {
            this.Id = int.Parse(data["id"].ToString());
            this.Title = data["title"].ToString();
            this.Place = data["place"].ToString();
            this.Starttime = (TimeSpan)data["starttime"];
            this.Endtime = (TimeSpan)data["endtime"];
            this.Hexcolor = data["hexcolor"].ToString();
            this.weektype = int.Parse(data["weektype"].ToString());
        }
    }
}
