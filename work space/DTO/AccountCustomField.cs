using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class AccountCustomField
    {
        int id;
        int idAccount;
        string content;
        string title;


        public AccountCustomField() { }
        public AccountCustomField(DataRow dataRow)
        {
            this.Id = int.Parse(dataRow["id"].ToString());
            this.title = dataRow["title"].ToString();
            if (dataRow["content"] != null)
                this.Content = dataRow["content"].ToString();
            this.IdAccount = int.Parse(dataRow["idaccount"].ToString());
        }

        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public int Id { get => id; set => id = value; }
    }
}
