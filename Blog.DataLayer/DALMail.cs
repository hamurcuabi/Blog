using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALMail
    {
        private static readonly string TableName = "MAIL";
        public static MAIL GetByCode(string code)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("CODE", "=", code));
            return Functions.ConvertToEntity<MAIL>(Functions.GetByPropertyValue(TableName, where));
        }
    }
}
