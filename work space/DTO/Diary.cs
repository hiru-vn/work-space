using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class Diary
    {
        private int _id;
        private DateTime _createdate;
        private DateTime? _storydate;
        private string _title;
        private string _story;
        private string _fontfamily;
        private string _fontcolor;
        private string _fontstyle;
        private int _fontsize;

        public int Id { get => _id;}
        public DateTime Createdate { get => _createdate; set => _createdate = value; }
        public DateTime? Storydate { get => _storydate; set => _storydate = value; }
        public string Title { get => _title; set => _title = value; }
        public string Fontfamily { get => _fontfamily; set => _fontfamily = value; }
        public string Fontcolor { get => _fontcolor; set => _fontcolor = value; }
        public string Fontstyle { get => _fontstyle; set => _fontstyle = value; }
        public int Fontsize { get => _fontsize; set => _fontsize = value; }
        public string Story { get => _story; set => _story = value; }

        public Diary() { }
        public Diary(DataRow data)
        {
            this.Storydate = (DateTime)data["storydate"];
            this.Title = data["title"].ToString();
            this.Story = data["story"].ToString();
            this.Fontfamily = data["fontfamily"].ToString();
            this.Fontsize = int.Parse(data["fontsize"].ToString());
            this.Fontstyle = data["fontstyle"].ToString();
            this.Fontcolor = data["fontcolor"].ToString();
        }
    }
}
