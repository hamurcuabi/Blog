using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALAboutUsCounter
    {
        private static readonly string TableName = "ABOUTUSCOUNTER";
        public static ABOUTUSCOUNTER Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<ABOUTUSCOUNTER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static List<ABOUTUSCOUNTER> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<ABOUTUSCOUNTER>();
        }

        public static void Insert(ABOUTUSCOUNTER item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(ABOUTUSCOUNTER item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(SORT),0) FROM " + Functions.TABLEPREFIX + "ABOUTUSCOUNTER";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));

            return retval;
        }

        public static short getCount(int ID)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "ABOUTUSCOUNTER";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }
        public static ABOUTUSCOUNTER GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<ABOUTUSCOUNTER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }

    }
}