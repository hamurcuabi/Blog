using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALParameter
    {
        private static readonly string TableName = "PARAMETER";
        public static PARAMETER Get(int ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<PARAMETER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static PARAMETER GetByCode(string code)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("CODE", "=", code, null));
            return Functions.ConvertToEntity<PARAMETER>(Functions.GetByPropertyValue(TableName, where));
        }
        public static List<PARAMETER> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<PARAMETER>();
        }

        public static void Insert(PARAMETER item)
        {
            Functions.InsertEntity(TableName, item);
        }
        public static void Update(PARAMETER item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        //public static void Delete(Guid ID)
        //{
        //    Functions.DeleteItem(TableName, ID);
        //}

        public static string GetValueByCode(string code)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("CODE", "=", code, null));
            return Functions.ConvertToEntity<PARAMETER>(Functions.GetByPropertyValue(TableName, where)).VALUE;
        }
    }
}
