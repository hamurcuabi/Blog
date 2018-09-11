using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
   public class DALCategories
    {
        private static readonly string TableName = "CATEGORIES";
        public static CATEGORIES Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<CATEGORIES>(Functions.GetByPropertyValue(TableName, where));
        }

        public static CATEGORIES GetTitle(string URLSEO)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("URLSEO", "=", URLSEO, null));
            return Functions.ConvertToEntity<CATEGORIES>(Functions.GetByPropertyValue(TableName, where));
        }

        public static List<CATEGORIES> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<CATEGORIES>();
        }

        public static int getCount()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "CATEGORIES";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }

        public static void Insert(CATEGORIES item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(CATEGORIES item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(SORT),0) FROM " + Functions.TABLEPREFIX + "CATEGORIES";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));

            return retval;
        }
      
        public static short getCount(int ID)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "CATEGORIES";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }
        public static CATEGORIES GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<CATEGORIES>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }

        public static bool HasTitle(string TITLE)
        {

            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from CATEGORIES where TITLE = @TITLE) if @hasMember > 1 select 1 as deger else select 0 as deger";
            Hashtable param = new Hashtable();
            param.Add("TITLE", TITLE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }
    }
}