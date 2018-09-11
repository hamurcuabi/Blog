using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Net.Mail;
using System.Net;

namespace Blog.DataLayer
{
    public static class Functions
    {
        #region Encryptor
        private static byte[] Key = { 91, 93, 19, 39, 110, 195, 123, 98, 101, 213, 5, 50, 52, 92, 193, 133, 193, 111, 221, 164, 58, 128, 89, 48, 97, 154, 83, 187, 111, 164, 171, 74 };
        private static byte[] IV = { 10, 61, 235, 120, 122, 121, 82, 248, 15, 121, 196, 212, 176, 46, 54, 85 };

        private static byte[] Encrypt(byte[] clearData)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }
        private static byte[] Decrypt(byte[] cipherData)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        public static string EncryptString(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            byte[] encryptedData = Encrypt(clearBytes);
            return Convert.ToBase64String(encryptedData);
        }
        public static string DecryptString(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] decryptedData = Decrypt(cipherBytes);
            return Encoding.Unicode.GetString(decryptedData);
        }
        public static string MD5(string text)
        {
            byte[] result = new byte[text.Length];
            MD5 md = new MD5CryptoServiceProvider();
            UTF8Encoding encode = new UTF8Encoding();
            result = md.ComputeHash(encode.GetBytes(text));

            return BitConverter.ToString(result).Replace("-", "");

        }
        #endregion
        public enum SqlType
        {
            MSSql = 1,
            MySql = 2
        }

        public static void Clear(Control parent)
        {
            foreach (Control cntrl in parent.Controls)
            {
                if ((cntrl.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(cntrl)).Text = string.Empty;
                }
                if ((cntrl.GetType() == typeof(DropDownList)))
                {
                    ((DropDownList)(cntrl)).SelectedValue = "0";
                }
                if (cntrl.HasControls())
                {
                    Clear(cntrl);
                }
            }
        }
        public static void Bind(this DropDownList item, object data, string id, string text, bool addSelect)
        {
            item.Items.Clear();
            item.DataValueField = id;
            item.DataTextField = text;
            item.DataSource = data;
            item.DataBind();
            if (addSelect)
            {
                item.Items.Insert(0, new ListItem("Seçiniz...", "0"));
            }
        }
        public static void Bind(this DropDownList item, object data, string id, string text, bool addSelect, string name)
        {
            item.Items.Clear();
            item.DataValueField = id;
            item.DataTextField = text;
            item.DataSource = data;
            item.DataBind();
            if (!string.IsNullOrEmpty(name))
            {
                if (addSelect)
                {
                    item.Items.Insert(0, new ListItem(name, "0"));
                }
            }
            else
            {
                if (addSelect)
                {
                    item.Items.Insert(0, new ListItem("Seçiniz...", "0"));
                }
            }

        }
        public static void Bind(this Repeater item, object data)
        {
            item.DataSource = data;
            item.DataBind();

        }

        public static void Bind(this ListBox item, object data, string dataValue, string textValue)
        {
            item.Items.Clear();
            item.SelectedIndex = -1;
            item.DataSource = data;
            item.DataValueField = dataValue;
            item.DataTextField = textValue;
            item.DataBind();

        }

        public static bool IsSelected(this DropDownList item)
        {
            return item.SelectedValue != "0" && item.SelectedValue != "";
        }
        public static bool IsSelected(this ListBox item)
        {
            return item.SelectedValue != "0" && item.SelectedValue != "";
        }
        public static Uri SITEURL
        {
            get { return new Uri(DecryptString(ConfigurationSettings.AppSettings["SystemCode"]).Split('~')[0]); }
        }
        public static string THEME
        {
            get
            {
                if (HttpContext.Current.Application["THEME"] == null)
                {
                    HttpContext.Current.Application["THEME"] = ConfigurationSettings.AppSettings["THEME"];
                }
                return HttpContext.Current.Application["THEME"].ToString();
            }
        }
        public static SqlType DBTYPE
        {
            get
            {
                if (ConfigurationSettings.AppSettings["DBType"] == "MSSql")
                {
                    return SqlType.MSSql;
                }
                else
                {
                    return SqlType.MySql;
                }
            }
        }
        public static string TABLEPREFIX
        {
            get { return ConfigurationSettings.AppSettings["TablePrefix"]; }
        }
        public static DateTime GetDate()
        { // server yurt disinda oldugundan tarih ayari yapalim diye
            return TimeZoneInfo.ConvertTime(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("GTB Standard Time"));
        }
        public static string FormatPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                if (phone.IndexOf("-") > -1)
                {
                    phone = "+90 (" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + " " + phone.Substring(6, 2) + " " + phone.Substring(8, 2) + " - " + phone.Substring(phone.IndexOf("-") + 1);
                }
                else if (!string.IsNullOrEmpty(phone))
                {
                    phone = "+90 (" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + " " + phone.Substring(6, 2) + " " + phone.Substring(8, 2);
                }
            }
            return phone;
        }


        public static void SetLoginUser(ONLINEUSER l, MEMBER m)
        {
            l.ID = m.ID;
            l.NAME = m.NAME;
            l.SURNAME = m.SURNAME;
            l.EMAIL = m.EMAIL;
            l.ISADMIN = m.ISADMIN;
            l.ISACTIVE = m.ISACTIVE;
            l.ISLOGIN = m.ISLOGIN;
            l.FULLNAME = m.FULLNAME;
            l.REALMEMBERID = m.ID;
        }


        #region DB Operation
        public static T ConvertToEntity<T>(DataSet dataset) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();
            DataRow tableRow = null;

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                tableRow = dataset.Tables[0].Rows[0];
            }

            if (tableRow != null)
            {
                foreach (DataColumn col in tableRow.Table.Columns)
                {
                    string colName = col.ColumnName;

                    // Look for the object's property with the columns name, ignore case
                    PropertyInfo pInfo = t.GetProperty(colName.ToLower().Replace("ı", "i"),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // did we find the property ?
                    if (pInfo != null)
                    {
                        object val = tableRow[colName];

                        // is this a Nullable<> type
                        bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                        if (IsNullable)
                        {
                            if (val is System.DBNull)
                            {
                                val = null;
                            }
                            else
                            {
                                // Convert the db type into the T we have in our Nullable<T> type
                                val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                            }
                        }
                        else
                        {
                            // Convert the db type into the type of the property in our entity
                            val = Convert.ChangeType(val, pInfo.PropertyType);
                        }
                        // Set the value of the property with the value from the db
                        pInfo.SetValue(returnObject, val, null);
                    }
                }
            }
            else
            {
                returnObject = default(T);
            }
            // return the entity object with values
            return returnObject;
        }
        public static T ConvertToEntity<T>(DataRow tableRow) where T : new()
        {
            // Create a new type of the entity I want
            Type t = typeof(T);
            T returnObject = new T();

            if (tableRow != null)
            {
                foreach (DataColumn col in tableRow.Table.Columns)
                {
                    string colName = col.ColumnName;

                    // Look for the object's property with the columns name, ignore case
                    PropertyInfo pInfo = t.GetProperty(colName.ToLower().Replace("ı", "i"),
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // did we find the property ?
                    if (pInfo != null)
                    {
                        object val = tableRow[colName];

                        // is this a Nullable<> type
                        bool IsNullable = (Nullable.GetUnderlyingType(pInfo.PropertyType) != null);
                        if (IsNullable)
                        {
                            if (val is System.DBNull)
                            {
                                val = null;
                            }
                            else
                            {
                                // Convert the db type into the T we have in our Nullable<T> type
                                val = Convert.ChangeType(val, Nullable.GetUnderlyingType(pInfo.PropertyType));
                            }
                        }
                        else
                        {
                            // Convert the db type into the type of the property in our entity
                            val = Convert.ChangeType(val, pInfo.PropertyType);
                        }
                        // Set the value of the property with the value from the db
                        pInfo.SetValue(returnObject, val, null);
                    }
                }
            }
            else
            {
                returnObject = default(T);
            }
            // return the entity object with values
            return returnObject;
        }
        public static List<T> ConvertToList<T>(this DataSet dataSet) where T : new()
        {
            return dataSet.Tables[0].ConvertToList<T>();
        }
        public static T ConvertToEnt<T>(this DataSet dataSet) where T : new()
        {
            T retval = default(T);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                retval = ConvertToEntity<T>(dataSet.Tables[0].Rows[0]);
            }
            return retval;
        }
        public static List<T> ConvertToList<T>(this DataTable table) where T : new()
        {
            Type t = typeof(T);

            // Create a list of the entities we want to return
            List<T> returnObject = new List<T>();

            // Iterate through the DataTable's rows
            foreach (DataRow dr in table.Rows)
            {
                // Convert each row into an entity object and add to the list
                T newRow = ConvertToEntity<T>(dr);
                returnObject.Add(newRow);
            }

            // Return the finished list
            return returnObject;
        }

        public static DataSet GetByPropertyValueDistinct(string tableName, List<WHERE> where, string colon)
        {
            DataSet retval = new DataSet();
            Hashtable param = new Hashtable();
            string sql = "SELECT DISTINCT " + colon + " FROM " + TABLEPREFIX + tableName + " WHERE 1=1";
            foreach (WHERE w in where)
            {
                switch (w.OPERATOR)
                {
                    case "BETWEEN":
                        {
                            sql += " AND " + w.COLUMNNAME + " BETWEEN @" + w.COLUMNNAME + "Left AND @" + w.COLUMNNAME + "Right";
                            param.Add(w.COLUMNNAME + "Left", w.VALUE);
                            param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                            break;
                        }
                    case "NOT BETWEEN":
                        {
                            sql += " AND " + w.COLUMNNAME + " NOT BETWEEN @" + w.COLUMNNAME + "Left AND @" + w.COLUMNNAME + "Right";
                            param.Add(w.COLUMNNAME + "Left", w.VALUE);
                            param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                            break;
                        }
                    case "ISNOTNULL":
                        {
                            sql += " AND " + w.COLUMNNAME + " IS NOT NULL";
                            break;
                        }
                    case "ISNULL":
                        {
                            sql += " AND " + w.COLUMNNAME + " IS NULL";
                            break;
                        }
                    case "OR":
                        {
                            if (string.IsNullOrEmpty(w.VALUE2.ToString()))
                            {
                                if (!string.IsNullOrEmpty(w.OPERATOR2.ToString()))
                                {
                                    sql += " AND (" + w.COLUMNNAME + " " + w.OPERATOR2 + " @" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + " IS NULL )";
                                    param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                    break;
                                }
                                else
                                {
                                    sql += " AND (" + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + " IS NULL )";
                                    param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                    break;
                                }

                            }
                            else
                            {
                                sql += " AND (" + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Right )";
                                param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                                break;
                            }

                        }
                    default:
                        {
                            if (!param.ContainsKey(w.COLUMNNAME))
                            {
                                sql += " AND " + w.COLUMNNAME + " " + w.OPERATOR + " @" + w.COLUMNNAME;
                                param.Add(w.COLUMNNAME, w.VALUE);
                            }
                            else
                            {// bir kolon ikinci defa gelmisse
                                string paramName = string.Empty;
                                for (int i = 0; i < 100; i++)
                                {
                                    if (!param.ContainsKey(w.COLUMNNAME + i.ToString()))
                                    {
                                        paramName = w.COLUMNNAME + i.ToString();
                                        break;
                                    }
                                }
                                sql += " AND " + w.COLUMNNAME + " " + w.OPERATOR + " @" + paramName;
                                param.Add(paramName, w.VALUE);
                            }
                            break;
                        }
                }
            }

            retval = SQLMs.RunDataset(sql, param);


            return retval;
        }
        public static DataSet GetByPropertyValue(string tableName, List<WHERE> where)
        {
            DataSet retval = new DataSet();
            Hashtable param = new Hashtable();
            string sql = "SELECT * FROM " + TABLEPREFIX + tableName + " WHERE 1=1";

            foreach (WHERE w in where)
            {
                switch (w.OPERATOR)
                {
                    case "TOP":
                        {
                            sql = "SELECT TOP " + w.VALUE + " * FROM " + TABLEPREFIX + tableName + " WHERE 1=1";
                            break;
                        }
                    case "BETWEEN":
                        {
                            sql += " AND " + w.COLUMNNAME + " BETWEEN @" + w.COLUMNNAME + "Left AND @" + w.COLUMNNAME + "Right";
                            param.Add(w.COLUMNNAME + "Left", w.VALUE);
                            param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                            break;
                        }
                    case "NOT BETWEEN":
                        {
                            sql += " AND " + w.COLUMNNAME + " NOT BETWEEN @" + w.COLUMNNAME + "Left AND @" + w.COLUMNNAME + "Right";
                            param.Add(w.COLUMNNAME + "Left", w.VALUE);
                            param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                            break;
                        }
                    case "ISNOTNULL":
                        {
                            sql += " AND " + w.COLUMNNAME + " IS NOT NULL";
                            break;
                        }
                    case "ISNULL":
                        {
                            sql += " AND " + w.COLUMNNAME + " IS NULL";
                            break;
                        }
                    case "OR":
                        {
                            if (string.IsNullOrEmpty(w.VALUE2.ToString()))
                            {
                                if (!string.IsNullOrEmpty(w.OPERATOR2.ToString()))
                                {
                                    sql += " AND (" + w.COLUMNNAME + " " + w.OPERATOR2 + " @" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + " IS NULL )";
                                    param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                    break;
                                }
                                else
                                {
                                    sql += " AND (" + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + " IS NULL )";
                                    param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                    break;
                                }

                            }
                            else
                            {
                                sql += " AND (" + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Left " + w.OPERATOR + " " + w.COLUMNNAME + "=@" + w.COLUMNNAME + "Right )";
                                param.Add(w.COLUMNNAME + "Left", w.VALUE);
                                param.Add(w.COLUMNNAME + "Right", w.VALUE2);
                                break;
                            }

                        }
                    default:
                        {
                            if (!param.ContainsKey(w.COLUMNNAME))
                            {
                                sql += " AND " + w.COLUMNNAME + " " + w.OPERATOR + " @" + w.COLUMNNAME;
                                param.Add(w.COLUMNNAME, w.VALUE);
                            }
                            else
                            {// bir kolon ikinci defa gelmisse
                                string paramName = string.Empty;
                                for (int i = 0; i < 100; i++)
                                {
                                    if (!param.ContainsKey(w.COLUMNNAME + i.ToString()))
                                    {
                                        paramName = w.COLUMNNAME + i.ToString();
                                        break;
                                    }
                                }
                                sql += " AND " + w.COLUMNNAME + " " + w.OPERATOR + " @" + paramName;
                                param.Add(paramName, w.VALUE);
                            }
                            break;
                        }
                }
            }

            retval = SQLMs.RunDataset(sql, param);


            return retval;
        }
        public static void UpdateEntity<T>(string tableName, T entity)
        {
            string sql = string.Empty;
            bool isUpdate = true;
            Type item = typeof(T);
            Hashtable param = new Hashtable();
            Facet computed = null;
            //var storeItem = DALBase.ctx.MetadataWorkspace.GetItems<EntityType>(DataSpace.OSpace).Where(et => et.FullName == entity.ToString()).FirstOrDefault();

            EntityConnection con = new EntityConnection("name=BlogEntities");

            MetadataWorkspace vStorageWorkspace = con.GetMetadataWorkspace();
            EdmType vStorageEdmType = vStorageWorkspace.GetType(tableName, "BlogModel.Store", DataSpace.SSpace);
            EntityType storeItem = vStorageEdmType as EntityType;

            // once kayit varmi bakalim, update mi insert mi
            sql = "SELECT COUNT(*) FROM " + TABLEPREFIX + tableName + " WHERE ";
            foreach (EdmMember member in storeItem.KeyMembers)
            {
                PropertyInfo info = item.GetProperty(member.Name);
                if (info.CanRead)
                {
                    sql += " " + member.Name + " = @" + member.Name + " #";
                    param.Add(member.Name, info.GetValue(entity, null));
                }
            }
            sql = sql.TrimEnd('#').Replace("#", " AND ");

            isUpdate = Convert.ToBoolean(Convert.ToInt32(SQLMs.RunScalar(sql, param)));

            param = new Hashtable();
            if (isUpdate)
            {
                #region update
                sql = "UPDATE " + TABLEPREFIX + tableName + " SET";
                // propertileri ayarliyalim
                foreach (EdmMember member in storeItem.Members)
                {
                    Facet identity = member.TypeUsage.Facets.Where(p => p.Name == "StoreGeneratedPattern").FirstOrDefault();

                    if (member.TypeUsage.EdmType.BaseType != null && !member.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out computed))
                    {// gercek alanlari yapsin, kendi koydugu navigation propertyleri yapmasin diye
                        if (storeItem.KeyMembers.Where(p => p.Name == member.Name).Count() == 0)
                        {//preimary key olmayanlari setleyecez.
                            if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Computed)
                            {// bir sey yapmasin

                            }
                            else
                            {
                                PropertyInfo info = item.GetProperty(member.Name);
                                if (info.CanRead)
                                {
                                    sql += " [" + member.Name + "] = @" + member.Name + ",";
                                    param.Add(member.Name, info.GetValue(entity, null));
                                }
                            }
                        }
                    }
                }
                sql = sql.TrimEnd(',');
                sql += " WHERE ";
                //simdi keye gore where yapalim
                foreach (EdmMember member in storeItem.KeyMembers)
                {
                    if (member.TypeUsage.EdmType.BaseType != null)
                    {// gercek alanlari yapsin, kendi koydugu navigation propertyleri yapmasin diye
                        PropertyInfo info = item.GetProperty(member.Name);
                        if (info.CanRead)
                        {
                            sql += " " + member.Name + " = @" + member.Name + " #";
                            param.Add(member.Name, info.GetValue(entity, null));
                        }
                    }
                }
                sql = sql.TrimEnd('#').Replace("#", " AND ");

                SQLMs.RunSQL(sql, param);

                #endregion
            }
            else
            {
                sql = "INSERT INTO " + TABLEPREFIX + tableName + "(";
                string values = " VALUES(";
                bool hasIdentity = false;
                string identityColName = string.Empty;
                foreach (EdmMember member in storeItem.Members)
                {
                    if (member.TypeUsage.EdmType.BaseType != null)
                    {// gercek alanlari yapsin, kendi koydugu navigation propertyleri yapmasin diye
                        PropertyInfo info = item.GetProperty(member.Name);
                        Facet identity = member.TypeUsage.Facets.Where(p => p.Name == "StoreGeneratedPattern").FirstOrDefault();
                        if (storeItem.KeyMembers.Where(p => p.Name == member.Name).Count() > 0)
                        {// key var


                            if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Identity)
                            {
                                hasIdentity = true;
                                identityColName = member.Name;
                            }
                            else
                            {
                                // identity degilse genede ekleyecegiz,ornek related product tablosu icin
                                sql += "[" + member.Name + "],";
                                values += "@" + member.Name + ",";
                                param.Add(member.Name, info.GetValue(entity, null));
                            }
                        }
                        else
                        {
                            if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Computed)
                            {// bir sey yapmasin

                            }
                            else if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Identity)
                            {// key kolon haricinde Identity var
                                hasIdentity = true;
                                identityColName = member.Name;
                            }
                            else
                            {
                                sql += "[" + member.Name + "],";
                                values += "@" + member.Name + ",";
                                param.Add(member.Name, info.GetValue(entity, null));
                            }
                        }
                    }
                }

                sql = sql.TrimEnd(',') + ") " + values.TrimEnd(',') + ")";
                if (hasIdentity)
                {//varsa alip ID ye setlemek lazim, ornek, orderi ekleyince sonrasinda order detail icin kullanilacak
                    if (DBTYPE == SqlType.MSSql)
                    {
                        sql += Environment.NewLine + " SELECT SCOPE_IDENTITY()";
                    }
                    else if (DBTYPE == SqlType.MySql)
                    {
                        sql += ";" + Environment.NewLine + " SELECT LAST_INSERT_ID(); ";
                    }
                    object id = null;

                    id = SQLMs.RunScalar(sql, param);

                    PropertyInfo info = item.GetProperty(identityColName);
                    id = Convert.ChangeType(id, info.PropertyType);
                    info.SetValue(entity, id, null);
                }
                else
                {
                    SQLMs.RunSQL(sql, param);
                }
            }

        }
        public static void InsertEntity<T>(string tableName, T entity)
        {
            string sql = string.Empty;
            Type item = typeof(T);
            Hashtable param = new Hashtable();
            //var storeItem = DALBase.ctx.MetadataWorkspace.GetItems<EntityType>(DataSpace.OSpace).Where(et => et.FullName == entity.ToString()).FirstOrDefault();
            EntityConnection con = new EntityConnection("name=BlogEntities");

            MetadataWorkspace vStorageWorkspace = con.GetMetadataWorkspace();
            EdmType vStorageEdmType = vStorageWorkspace.GetType(tableName, "BlogModel.Store", DataSpace.SSpace);
            EntityType storeItem = vStorageEdmType as EntityType;


            sql = "INSERT INTO " + TABLEPREFIX + tableName + "(";
            string values = " VALUES(";
            bool hasIdentity = false;
            string identityColName = string.Empty;
            foreach (EdmMember member in storeItem.Members)
            {
                if (member.TypeUsage.EdmType.BaseType != null)
                {// gercek alanlari yapsin, kendi koydugu navigation propertyleri yapmasin diye
                    PropertyInfo info = item.GetProperty(member.Name);
                    Facet identity = member.TypeUsage.Facets.Where(p => p.Name == "StoreGeneratedPattern").FirstOrDefault();
                    if (storeItem.KeyMembers.Where(p => p.Name == member.Name).Count() > 0)
                    {// key var


                        if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Identity)
                        {
                            hasIdentity = true;
                            identityColName = member.Name;
                        }
                        else
                        {
                            // identity degilse genede ekleyecegiz,ornek related product tablosu icin
                            sql += "[" + member.Name + "],";
                            values += "@" + member.Name + ",";
                            param.Add(member.Name, info.GetValue(entity, null));
                        }
                    }
                    else
                    {
                        if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Computed)
                        {// bir sey yapmasin

                        }
                        else if (identity != null && (StoreGeneratedPattern)identity.Value == StoreGeneratedPattern.Identity)
                        {// key kolon haricinde Identity var
                            hasIdentity = true;
                            identityColName = member.Name;
                        }
                        else
                        {
                            sql += "[" + member.Name + "],";
                            values += "@" + member.Name + ",";
                            param.Add(member.Name, info.GetValue(entity, null));
                        }
                    }
                }
            }

            sql = sql.TrimEnd(',') + ") " + values.TrimEnd(',') + ")";
            if (hasIdentity)
            {//varsa alip ID ye setlemek lazim, ornek, orderi ekleyince sonrasinda order detail icin kullanilacak
                if (DBTYPE == SqlType.MSSql)
                {
                    sql += Environment.NewLine + " SELECT SCOPE_IDENTITY()";
                }
                else if (DBTYPE == SqlType.MySql)
                {
                    sql += ";" + Environment.NewLine + " SELECT LAST_INSERT_ID(); ";
                }
                object id = null;

                id = SQLMs.RunScalar(sql, param);

                PropertyInfo info = item.GetProperty(identityColName);
                id = Convert.ChangeType(id, info.PropertyType);
                info.SetValue(entity, id, null);
            }
            else
            {
                SQLMs.RunSQL(sql, param);
            }

        }

        public static void DeleteEnt(string tableName, object ID)
        {
            List<WHERE> where = new List<WHERE>();
            where.Add(new WHERE("ID", "=", ID));
            DeleteItem(tableName, where);
        }

        public static void DeleteItem(string tableName, List<WHERE> where)
        {
            string sql = "DELETE FROM " + TABLEPREFIX + tableName + " WHERE 1=1";
            Hashtable param = new Hashtable();
            foreach (WHERE w in where)
            {
                sql += " AND " + w.COLUMNNAME + w.OPERATOR + "@" + w.COLUMNNAME;
                param.Add(w.COLUMNNAME, w.VALUE);
            }
            SQLMs.RunSQL(sql, param);
        }
        public static bool IsValidTCKN(string tcKimlikNo)
        {
            bool returnvalue = false;
            if (tcKimlikNo.Length == 11)
            {
                Int64 ATCNO, BTCNO, TcNo;
                long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

                TcNo = Int64.Parse(tcKimlikNo);

                ATCNO = TcNo / 100;
                BTCNO = TcNo / 100;

                C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);

                returnvalue = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
            }
            return returnvalue;
        }
        public static String DateTimeChange(String date)
        {
            DateTime dt = DateTime.Parse(date);
            return String.Format("{0:ddd, MMM d, yyyy}", dt);

        }
        public static String GetSubString(String text, int count)
        {

            if (text.Length > count) return text.Substring(0, count - 1);
            else return text;

        }
     
    }
    public class WHERE
    {
        public WHERE(string columnName, string op)
        {
            COLUMNNAME = columnName;
            OPERATOR = op;
        }
        public WHERE(string columnName, string op, object value)
        {
            COLUMNNAME = columnName;
            OPERATOR = op;
            VALUE = value;
        }
        public WHERE(string columnName, string op, object value, object value2)
        {
            COLUMNNAME = columnName;
            OPERATOR = op;
            VALUE = value;
            VALUE2 = value2;
            OPERATOR2 = string.Empty;
        }
        public WHERE(string columnName, string op, object value, object value2, object op2)
        {
            COLUMNNAME = columnName;
            OPERATOR = op;
            VALUE = value;
            VALUE2 = value2;
            OPERATOR2 = op2;
        }
        public string COLUMNNAME { get; set; }
        public string OPERATOR { get; set; }
        public object VALUE { get; set; }
        public object VALUE2 { get; set; }
        public object OPERATOR2 { get; set; }
    }
}
#endregion