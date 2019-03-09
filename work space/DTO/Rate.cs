using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DTO
{
    public class Rate
    {
        int _id;
        int _ratepoint;
        DateTime? _ratetime;

        public int Id { get => _id; set => _id = value; }
        public int Ratepoint { get => _ratepoint; set => _ratepoint = value; }
        public DateTime? Ratetime { get => _ratetime; set => _ratetime = value; }

        public Rate() { }
        public Rate(DataRow row)
        {
            this.Id = int.Parse(row["id"].ToString());
            this.Ratepoint = int.Parse(row["ratepoint"].ToString());
            this.Ratetime = (DateTime?)row["ratetime"];
        }
    }
}
