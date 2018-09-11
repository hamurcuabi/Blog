using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALMember
    {
        private static readonly string TableName = "MEMBER";
        public static MEMBER Get(Guid ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID, null));
            return Functions.ConvertToEntity<MEMBER>(Functions.GetByPropertyValue(TableName, where));
        }
        public static List<MEMBER> GetAll()
        {
            List<WHERE> where = new List<WHERE>();
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<MEMBER>();
        }

        public static void Insert(MEMBER item)
        {
            Functions.InsertEntity(TableName, item);
        }

        public static List<MEMBER> GetSearchList(string name)
        {
            List<WHERE> where = new List<WHERE>();

            if (!string.IsNullOrEmpty(name))
            {
                where.Add(new WHERE("FULLNAME", "LIKE", "%" + name.Trim().Replace(" ", "%") + "%"));
            }
            return Functions.GetByPropertyValue(TableName, where).ConvertToList<MEMBER>();
        }

        public static void Update(MEMBER item)
        {
            Functions.UpdateEntity(TableName, item);
        }
        //public static void Delete(Guid ID)
        //{
        //    Functions.DeleteItem(TableName, ID);
        //}

        public static MEMBER GetByEmailAndPassword(string email, string password)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("EMAIL", "=", email, null));
            where.Add(new WHERE("PASSWORD", "=", password, null));
            return Functions.ConvertToEntity<MEMBER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static MEMBER GetByEmail(string email)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("EMAIL", "=", email, null));
            return Functions.ConvertToEntity<MEMBER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static MEMBER GetByPasswordResetCode(string passwordcode)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("PASSWORDRESETCODE", "=", passwordcode, null));
            return Functions.ConvertToEntity<MEMBER>(Functions.GetByPropertyValue(TableName, where));
        }

        public static MEMBER GetByID(Guid memberid)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", memberid, null));
            return Functions.ConvertToEntity<MEMBER>(Functions.GetByPropertyValue(TableName, where));
        }
    }
}


