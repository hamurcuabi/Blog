using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Blog.DataLayer
{
    public class DALMainPageSlider
    {
        private static readonly string TableName = "MAINPAGESLIDER";
        public static MAINPAGESLIDER Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<MAINPAGESLIDER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static List<MAINPAGESLIDER> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<MAINPAGESLIDER>();
        }

        public static void Insert(MAINPAGESLIDER item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(MAINPAGESLIDER item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        public static short GetLastSort()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(MAX(SORT),0) FROM " + Functions.TABLEPREFIX + "MAINPAGESLIDER";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));

            return retval;
        }
        public static bool HasImage(string IMAGE)
        {
            string sql = string.Empty;
            bool retval;
            sql = "declare @hasMember int = (select COUNT(*) as hasMember from MAINPAGESLIDER where IMAGE = @IMAGE) if @hasMember > 0 select 0 as deger else select 1 as deger";
            Hashtable param = new Hashtable();
            param.Add("IMAGE", IMAGE);
            retval = Convert.ToBoolean(SQLMs.RunScalar(sql, param));
            return retval;
        }

        public static short getCount()
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "MAINPAGESLIDER";
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return retval;
        }
        public static MAINPAGESLIDER GetBySort(int sort)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("SORT", "=", sort, null));
            return Functions.ConvertToEntity<MAINPAGESLIDER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static void Delete(int ID)
        {
            Functions.DeleteEnt(TableName, ID);
        }

    }
}