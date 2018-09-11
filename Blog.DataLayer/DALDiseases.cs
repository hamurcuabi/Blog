using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALDiseases
    {
        private static readonly string TableName = "DISEASES";
        public static DISEASES Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<DISEASES>(Functions.GetByPropertyValue(TableName, where));
        }
        public static DISEASES GetTitle(string URLSEO)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("URLSEO", "=", URLSEO, null));
            return Functions.ConvertToEntity<DISEASES>(Functions.GetByPropertyValue(TableName, where));
        }
        public static List<DISEASES> GetTitleList(string URLSEO)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("URLSEO", "=", URLSEO, null));
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<DISEASES>();
        }

        public static List<DISEASES> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<DISEASES>();
        }
        public static List<DISEASES> Get8()
        {
            string sql = string.Empty;
            sql = @"select TOP 8 * from DISEASES order by SORT";
            Hashtable param = new Hashtable();
            DataSet retval = new DataSet();
            retval = SQLMs.RunDataset(sql, param);
            return retval.ConvertToList<DISEASES>();
        }

        public static void Insert(DISEASES item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(DISEASES item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(SORT),0) FROM " + Functions.TABLEPREFIX + "DISEASES";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));

            return retval;
        }
        public static bool HasImage(string IMAGE)
        {
            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from DISEASES where IMAGE = @IMAGE) if @hasMember > 0 select 0 as deger else select 1 as deger";
            Hashtable param = new Hashtable();
            param.Add("IMAGE", IMAGE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }

        public static bool HasTitle(string TITLE)
        {
            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from DISEASES where TITLE = @TITLE) if @hasMember > 1 select 1 as deger else select 0 as deger";
            Hashtable param = new Hashtable();
            param.Add("TITLE", TITLE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }
        public static short getCount()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "DISEASES";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }
        public static DISEASES GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<DISEASES>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }

    }
}