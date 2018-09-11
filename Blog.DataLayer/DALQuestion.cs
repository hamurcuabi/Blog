using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALQUESTION
    {
        private static readonly string TableName = "QUESTION";
        public static QUESTION Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<QUESTION>(Functions.GetByPropertyValue(TableName, where));
        }

        public static List<QUESTION> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
          
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<QUESTION>();

        }
        public static List<QUESTION> GetAll2()
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ISACTIVE", "=", 1, null));
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<QUESTION>();

        }
        public static List<QUESTION> Get8()
        {
            string sql = string.Empty;
            sql = @"select TOP 8 * from QUESTION order by SORT";
            Hashtable param = new Hashtable();
            DataSet retval = new DataSet();
            retval = SQLMs.RunDataset(sql, param);
            return retval.ConvertToList<QUESTION>();
        }
        public static List<QUESTION> GetLastCount(int count)
        {
            string sql = string.Empty;
            if (GetCount() >= count)
            {
                sql = @"select TOP " + count + " * from QUESTION order by DATE DESC";

            }
            else if (GetCount() > 0)
            {
                sql = @"select TOP " + 1 + " * from QUESTION order by DATE DESC";

            }
            else
            {
                sql = @"select TOP " + 0 + " * from QUESTION order by DATE DESC";

            }
            Hashtable param = new Hashtable();
            DataSet retval = new DataSet();
            retval = SQLMs.RunDataset(sql, param);
            return retval.ConvertToList<QUESTION>();
        }
        public static List<QUESTION> GetTop(int topCount)
        {
            string sql = string.Empty;
            sql = @"SELECT TOP " + topCount + " * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY SORT ) AS RowNum, * FROM QUESTION ) AS RowConstrainedResult ORDER BY RowNum";
            DataSet retval = new DataSet();
            retval = SQLMs.RunDataset(sql);
            return retval.ConvertToList<QUESTION>();
        }
        public static List<QUESTION> GetbyPaging(int start, int end)
        {
            string sql = string.Empty;
            sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY SORT ) AS RowNum, * FROM QUESTION ) AS RowConstrainedResult WHERE RowNum >= " + start + " AND RowNum <= " + end + "  ORDER BY RowNum";
            DataSet retval = new DataSet();
            retval = SQLMs.RunDataset(sql);
            return retval.ConvertToList<QUESTION>();
        }
        public static QUESTION GetTitle(string URLSEO)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("URLSEO", "=", URLSEO, null));
            return Functions.ConvertToEntity<QUESTION>(Functions.GetByPropertyValue(TableName, where));
        }
        public static List<QUESTION> GetTitleList(string URLSEO)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("URLSEO", "=", URLSEO, null));
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<QUESTION>();
        }
        public static void Insert(QUESTION item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(QUESTION item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(ID),0) FROM " + Functions.TABLEPREFIX + "QUESTION";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }

     
        public static int GetCount()
        {
            string sql = string.Empty;
            int retval = 0;
            sql = "select COUNT(*) from QUESTION";
            retval = Convert.ToInt32(SQLMs.RunScalar(sql));
            return retval;
        }
        public static bool HasImage(string IMAGE)
        {
            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from BLOG where IMAGE = @IMAGE) if @hasMember > 0 select 0 as deger else select 1 as deger";
            Hashtable param = new Hashtable();
            param.Add("IMAGE", IMAGE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }

     
        public static QUESTION GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<QUESTION>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }
     

    }
}