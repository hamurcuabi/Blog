using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Blog.DataLayer
{
    public class SQLMs
    {
        public static DataSet RunDataset(string sql)
        {
            return RunDataset(sql, null);
        }
        public static DataSet RunDataset(string sql, Hashtable param)
        {
            DataSet retval = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (param != null)
            {
                foreach (DictionaryEntry p in param)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + p.Key.ToString(), p.Value == null ? DBNull.Value : p.Value));
                }
            }

            try
            {
                (new SqlDataAdapter(cmd)).Fill(retval);
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Broken)
                {
                    cmd.Connection.Close();
                }
                throw ex;
            }
            return retval;
        }
        public static object RunScalar(string sql)
        {
            return RunScalar(sql, null);
        }
        public static object RunScalar(string sql, Hashtable param)
        {
            object retval = null;
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (param != null)
            {
                if (param != null)
                {
                    foreach (DictionaryEntry p in param)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + p.Key.ToString(), p.Value == null ? DBNull.Value : p.Value));
                    }
                }
            }
            try
            {
                cmd.Connection.Open();
                retval = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Broken)
                {
                    cmd.Connection.Close();
                }
                throw ex;
            }
            return retval;
        }
        public static void RunSQL(string sql)
        {
            RunSQL(sql, null);
        }
        public static void RunSQL(string sql, Hashtable param)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (param != null)
            {
                if (param != null)
                {
                    foreach (DictionaryEntry p in param)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + p.Key.ToString(), p.Value == null ? DBNull.Value : p.Value));
                    }
                }
            }
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open || cmd.Connection.State == ConnectionState.Broken)
                {
                    cmd.Connection.Close();
                }
                throw ex;
            }
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationSettings.AppSettings["connectionOfDatabaseEmre"]);
        }
    }
}
