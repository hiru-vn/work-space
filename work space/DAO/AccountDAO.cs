using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using work_space.DTO;

namespace work_space.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }

        public List<AccountCategory> GetListAccountCategory()
        {
            List<AccountCategory> list = new List<AccountCategory>();
            string query = string.Format("select id,content from dbo.accountcategory where content != 'none'");
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new AccountCategory(row)); }
            return list;
        }
        public List<AccountCategory> SearchListAccountCategory(string s)
        {
            List<AccountCategory> list = new List<AccountCategory>();
            string query = string.Format("select id,content from dbo.accountcategory where content != 'none' and content like N'%{0}%'",s);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new AccountCategory(row)); }
            return list;
        }
        public List<Account> GetListAccountByCategoryID(int id)
        {
            List<Account> list = new List<Account>();
            string query = string.Format("select * from dbo.account where idcategory={0}", id);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new Account(row)); }
            return list;
        }
        public List<Account> SearchListAccountByCategoryID(int id,string s)
        {
            List<Account> list = new List<Account>();
            string query = string.Format("select * from dbo.account where idcategory={0} and title like N'%{1}%'", id,s);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new Account(row)); }
            return list;
        }
        public DataTable GetDataTableAccountByCategoryID(int id)
        {
            string query = string.Format("select * from dbo.account where idcategory={0}", id);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            return data;
        }
        public List<Account> GetAccount()
        {
            List<Account> list = new List<Account>();
            string query = string.Format("select * from dbo.account");
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new Account(row)); }
            return list;
        }
        public List<Account> SearchAccount(string s)
        {
            List<Account> list = new List<Account>();
            string query = string.Format("select * from dbo.account where title like N'%{0}%' ",s);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new Account(row)); }
            return list;
        }
        public Account GetAccountByID(int id)
        {
            string query = string.Format("select top 1 * from dbo.account where id={0}", id);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            Account acc;
            if (data.Rows.Count > 0)
            {
                acc = new Account(data.Rows[0]);
                return acc;
            }
            else
            {
                return null;
            }
        }
        public int DeleteAccountByID(int id)
        {
            string query = string.Format("delete from dbo.customfield where idaccount = {0} ", id);
            DataProvider.Instance.ExcuteNonQuery(query);
            query = string.Format("delete from dbo.account where id = {0} ", id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int DeleteAccountCategoryByID(int id)
        {
            if (id != 1)
            {
                string query = string.Format("select id from dbo.account where idcategory = {0} ", id);
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                foreach(DataRow row in data.Rows)
                {
                    DeleteAccountByID(int.Parse(row["id"].ToString()));
                }
                query = string.Format("delete from dbo.accountcategory where id = {0} ", id);
                return DataProvider.Instance.ExcuteNonQuery(query);
            }
            return 0;
        }
        public int InsertAccountCategory(string content)
        {
            string query = string.Format("insert into dbo.accountcategory(content) values (N'{0}') ", content);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int InsertAccount(Account acc)
        {
            string query = string.Format("insert into dbo.account(title,username,Apassword,website,idcategory) values (N'{0}',N'{1}',N'{2}','{3}',{4})", acc.Title, acc.Username, acc.Apassword, acc.Website, acc.Idcategory);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int UpdateAccount(Account acc)
        {
            string query = string.Format("update dbo.account set title = N'{0}', username = N'{1}', Apassword = N'{2}', website = '{3}' where id = {4}", acc.Title, acc.Username, acc.Apassword, acc.Website, acc.Id);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public List<AccountCustomField> GetCustomFieldByAccountID(int id)
        {
            List<AccountCustomField> list = new List<AccountCustomField>();
            string query = string.Format("select * from dbo.customfield where idaccount = {0}",id);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            if (data.Rows.Count > 0)
                foreach (DataRow row in data.Rows) { list.Add(new AccountCustomField(row)); }
            return list;
        }
        public int InsertCustomField(AccountCustomField customField)
        {
            string query = string.Format("insert into dbo.customfield(title,content,idaccount) values (N'{0}',N'{1}',{2}) ", customField.Title, customField.Content,customField.IdAccount);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int UpdateCustomField(AccountCustomField customField)
        {
            string query = string.Format("update dbo.customfield set content = N'{0}' where idaccount = {1} ", customField.Content, customField.IdAccount);
            return DataProvider.Instance.ExcuteNonQuery(query);
        }
        public int GetMaxAccountID()
        {
            return (int) DataProvider.Instance.ExcuteScarar("select max(id) from account");
        }
    }
}
