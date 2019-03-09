using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using work_space.DTO;

namespace work_space.DAO
{
    public class DiaryDAO
    {
        private static DiaryDAO instance;

        public static DiaryDAO Instance
        {
            get { if (instance == null) instance = new DiaryDAO(); return instance; }
            private set { instance = value; }
        }
        private DiaryDAO() { }

        #region Get Diary
        public Diary GetDiaryByDateAndTitle(DateTime? date,string title)
        {
            Diary diary = null;
            if (date != null)
            {
                string query = string.Format("select * from dbo.diary where storydate = '{0}' and title = N'{1}'", date, title);
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                if (data.Rows.Count > 0)
                    diary = new Diary(data.Rows[0]);
            }
            return diary;
        }
        public List<string> GetListDiaryTitleByDate(DateTime? date)
        {
            List<string> list = new List<string>();
            if (date != null)
            {
                string query = string.Format("select title from dbo.diary where storydate = '{0}' ", date);
                DataTable data = DataProvider.Instance.ExcuteQuery(query);
                if (data.Rows.Count > 0)
                    foreach (DataRow row in data.Rows) { list.Add(row["title"].ToString()); }
            }
            return list;
        }
        #endregion
        #region Insert Diary
        public int InsertDiary(DateTime? date, string title)
        {
            if (date != null) {
                string query = string.Format("insert into dbo.diary(storydate,title) values ('{0}',N'{1}')", date, title);
                DataProvider.Instance.ExcuteNonQuery(query);
                return 1;
            }
            return 0;
        }
        public int InsertDiary(DateTime? date, string title, string story)
        {
            if (date != null)
            {
                string query = string.Format("insert into dbo.diary(storydate,title,story) values ('{0}',N'{1}',N'{2}')", date, title,story);
                DataProvider.Instance.ExcuteNonQuery(query);
                return 1;
            }
            return 0;
        }
        public int InsertDiary(Diary diary)
        {
            if (diary.Storydate != null)
            {
                string query = string.Format("insert into dbo.diary(storydate,title,story,fontfamily,fontsize,fontcolor) values ('{0}',N'{1}',N'{2}','{3}',{4},'{5}')", diary.Storydate, diary.Title, diary.Story, diary.Fontfamily, diary.Fontsize,diary.Fontcolor);
                DataProvider.Instance.ExcuteNonQuery(query);
                return 1;
            }
            return 0;
        }
        #endregion
        #region Update Diary
        public int UpdateDiaryStory(DateTime? date, string title, string story)
        {
            if (date!=null)
            {
                string query = string.Format("update dbo.diary set story = N'{0}' where storydate = '{1}' and title = N'{2}' ",story,date,title);
                DataProvider.Instance.ExcuteNonQuery(query);
                return 1;
            }
            return 0;
        }
        public int UpdateDiaryFontFamily(DateTime? date, string title, string ff)
        {
            if (!string.IsNullOrEmpty(ff))
            {
                if (date != null)
                {
                    string query = string.Format("update dbo.diary set fontfamily = '{0}' where storydate = '{1}' and title = N'{2}' ", ff, date, title);
                    DataProvider.Instance.ExcuteNonQuery(query);
                    return 1;
                }
            }
            return 0;
        }
        public int UpdateDiaryFontColor(DateTime? date, string title, string ff)
        {
            if (!string.IsNullOrEmpty(ff))
            {
                if (date != null)
                {
                    string query = string.Format("update dbo.diary set fontcolor = '{0}' where storydate = '{1}' and title = N'{2}' ", ff, date, title);
                    DataProvider.Instance.ExcuteNonQuery(query);
                    return 1;
                }
            }
            return 0;
        }
        public int UpdateDiaryFontSize(DateTime? date, string title, int size)
        {
                if (date != null)
                {
                    string query = string.Format("update dbo.diary set fontsize = {0} where storydate = '{1}' and title = N'{2}' ", size, date, title);
                    DataProvider.Instance.ExcuteNonQuery(query);
                    return 1;
                }
            return 0;
        }
        public int UpdateDiaryFontStyle(DateTime? date, string title, string fs)
        {
            if (!string.IsNullOrEmpty(fs))
            {
                if (date != null)
                {
                    string query = string.Format("update dbo.diary set fontstyle = '{0}' where storydate = '{1}' and title = N'{2}' ", fs, date, title);
                    DataProvider.Instance.ExcuteNonQuery(query);
                    return 1;
                }
            }
            return 0;
        }
        #endregion
        #region Delete Diary
        public int DeleteDiary(DateTime? date, string title)
        {
            if (date != null)
            {
                string query = string.Format("delete from dbo.diary where storydate = '{0}' and title = N'{1}' ", date, title);
                DataProvider.Instance.ExcuteNonQuery(query);
                return 1;
            }
            return 0;
        }
        #endregion
    }
}
