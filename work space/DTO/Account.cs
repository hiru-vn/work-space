using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class Account
    {
        int id;
        DateTime? createdate;
        DateTime? lastupdate;
        string title;
        string username;
        string apassword;
        string website;
        int idcategory;

        public Account() { }
        public Account(DataRow data)
        {
            Id = int.Parse(data["id"].ToString());
            Createdate = (DateTime?)data["createdate"];
            lastupdate = (DateTime?)data["lastupdate"];
            title = data["title"].ToString();
            username = data["username"].ToString();
            Apassword = data["Apassword"].ToString();
            if (data["website"]!=null) website = data["website"].ToString();
            Idcategory = int.Parse(data["idcategory"].ToString());
        }

        public int Id { get => id; set => id = value; }
        public DateTime? Createdate { get => createdate; set => createdate = value; }
        public string Title { get => title; set => title = value; }
        public string Username { get => username; set => username = value; }
        public string Apassword { get => apassword; set => apassword = value; }
        public string Website { get => website; set => website = value; }
        public int Idcategory { get => idcategory; set => idcategory = value; }
    }
}
