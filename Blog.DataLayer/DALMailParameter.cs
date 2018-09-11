using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALMailParameter
    {
        private static readonly string TableName = "MAILPARAMETER";
        public static MAILPARAMETER Get(Guid ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<MAILPARAMETER>(Functions.GetByPropertyValue(TableName, where));
        }
        public static List<MAILPARAMETER> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<MAILPARAMETER>();
        }
        public static MAILPARAMETER GetByCode(string code)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("CODE", "=", code));
            return Functions.ConvertToEntity<MAILPARAMETER>(Functions.GetByPropertyValue(TableName, where));
        }
    }
}