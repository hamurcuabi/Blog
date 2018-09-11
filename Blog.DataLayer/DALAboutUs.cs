using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALAboutUs
    {
        private static readonly string TableName = "ABOUTUS";
        public static ABOUTUS Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<ABOUTUS>(Functions.GetByPropertyValue(TableName, where));
        }

        public static List<ABOUTUS> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<ABOUTUS>();
        }

        public static void Insert(ABOUTUS item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(ABOUTUS item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(SORT),0) FROM " + Functions.TABLEPREFIX + "ABOUTUS";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));

            return retval;
        }
        public static bool HasImage(string IMAGE)
        {
            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from ABOUTUS where IMAGE = @IMAGE) if @hasMember > 0 select 0 as deger else select 1 as deger";
            Hashtable param = new Hashtable();
            param.Add("IMAGE", IMAGE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }
        public static short getCount(int ID)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "ABOUTUS";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }
        public static ABOUTUS GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<ABOUTUS>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }

    }
}