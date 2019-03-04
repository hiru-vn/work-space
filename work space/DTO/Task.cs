using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace work_space.DTO
{
    public class Task
    {
        private int _id;
        private string _content;
        private int _priority;
        private DateTime? _createtime;
        private DateTime? _deadline;
        private bool _checked;
        private static readonly KeyValuePair<int, Rectangle>[] priorityColor = {
        new KeyValuePair<int, Rectangle>(0, new Rectangle()),
        new KeyValuePair<int, Rectangle>(1, new Rectangle()),
        new KeyValuePair<int, Rectangle>(2, new Rectangle()),
        };

        public int Id { get => _id;}
        public string Content { get => _content; set => _content = value; }
        public DateTime? Createtime { get => _createtime;}
        public DateTime? Deadline { get => _deadline; set => _deadline = value; }
        public bool Checked { get => _checked; set => _checked = value; }
        public int Priority { get => _priority; set => _priority = value; }
        public KeyValuePair<int, Rectangle>[] PriorityColor
        {
            get
            {
                return priorityColor;
            }
        }
        public Task(DataRow data)
        {
            this.Content = data["content"].ToString();
            string priority = data["Tpriority"].ToString();
            if (priority == "high") this.Priority = 0;
            else if (priority == "medium") this.Priority = 1;
            else if (priority == "low") this.Priority = 2;
            if (data["deadline"].ToString() != "")
                this.Deadline = (DateTime?)data["deadline"];
            if (byte.Parse(data["checked"].ToString()) == 0)
                this.Checked = false;
            else this.Checked = true;
        }
    }
}
