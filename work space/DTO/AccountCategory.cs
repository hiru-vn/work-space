using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class AccountCategory
    {
        int id;
        string content;

        public int Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }

        public AccountCategory() { }
        public AccountCategory(DataRow dataRow)
        {
            this.id = int.Parse(dataRow["id"].ToString());
            this.content = dataRow["content"].ToString();
        }
    }
}
