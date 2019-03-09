using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_space.DAO
{
    public class RateDAO
    {
        private static RateDAO instance;

        public static RateDAO Instance
        {
            get { if (instance == null) instance = new RateDAO(); return instance; }
            private set { instance = value; }
        }
        private RateDAO() { }
        public int InsertRate(int ratepoint)
        {
            string query = string.Format("insert into dbo.rate(ratepoint) values ({0})", ratepoint);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int GetLatestRatePoint()
        {
            string query = string.Format("select ratepoint from dbo.rate where ratetime = (select max(ratetime) from dbo.rate) ");
            int ratepoint = int.Parse(DataProvider.Instance.ExcuteScarar(query).ToString());
            return ratepoint;
        }
    }
}
