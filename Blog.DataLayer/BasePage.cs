using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Blog.DataLayer
{
    public class BasePage : System.Web.UI.Page
    {
        public static readonly string SPLITSEPERATOR = ",";
        protected bool LOGINREQUIRED { get; set; }
        protected ONLINEUSER ONLINEUSER
        {
            get
            {
                if (Session["ONLINEUSER"] == null)
                {
                    Session["ONLINEUSER"] = new ONLINEUSER();
                }
                return Session["ONLINEUSER"] as ONLINEUSER;
            }
            set { Session["ONLINEUSER"] = value; }
        }

        public Guid RECID
        {
            get
            {
                if (ViewState["RECID"] == null)
                {
                    ViewState["RECID"] = Functions.EncryptString(Guid.Empty.ToString());
                }
                return Guid.Parse(Functions.DecryptString(ViewState["RECID"].ToString()));
            }
            set
            {
                ViewState["RECID"] = Functions.EncryptString(value.ToString());
            }
        }
        public int RECid
        {
            get
            {
                if (ViewState["#RECid#"] == null)
                {
                    ViewState["#RECid#"] = 0;
                }
                return Convert.ToInt32(ViewState["#RECid#"]);
            }
            set { ViewState["#RECid#"] = value; }
        }
        private List<PARAMETER> PARAMETERS
        {
            get
            {
                if (Application["PARAMETERS"] == null)
                {
                    Application["PARAMETERS"] = DALParameter.GetAll();
                }
                return (Application["PARAMETERS"] as List<PARAMETER>);
            }
            set
            {
                Application["PARAMETERS"] = value;
            }
        }
        public enum MemberType
        {
            SystemAdmin = 0,
            Dietician = 1,
            Asistant = 2,
            Patient = 3
        }
        public static string EmailReplace(string email)
        {
            return email.ToLower().Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
        }
        #region Message
        public enum MessageType
        {
            Info = 1,
            Error = 2,
            Success = 3
        }
        public enum NotificationType
        {
            info = 1,
            error = 2,
            success = 3,
            warning = 4,
            dark = 5
        }
        public void NotificationAdd(NotificationType notiType, string msg)
        {
            if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Notification", "Notification('" + notiType + "' , '" + msg + "')", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Notification", "<script>Notification('" + notiType + "' , '" + msg + "')</script>");

            }
        }

        public void MessageAdd(MessageType msgType, string msg)
        {
            MessageAdd(msgType, msg, string.Empty);
        }
        public void MessageAdd(MessageType msgType, string msg, WebControl control)
        {
            MessageAdd(msgType, msg, control.ClientID);
        }
        public void MessageAdd(MessageType msgType, string msg, string focusObject)
        {

            if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "ShowMessage('" + msgType.ToString() + "','" + Server.HtmlEncode(msg) + "','" + focusObject + "'," + (msgType == MessageType.Error ? "0" : "5000") + ")", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "ShowMessage", "<script>ShowMessage('" + msgType.ToString() + "','" + Server.HtmlEncode(msg) + "',false,'" + focusObject + "'," + (msgType == MessageType.Error ? "0" : "5000") + ")</script>");

            }
        }
        #endregion
        protected string GetPrameterValue(string key)
        {
            return PARAMETERS.Where(p => p.CODE == key).First().VALUE;
        }
        public static string ShowDate(object date)
        {
            return ShowDate(date, false);
        }
        public static string ShowDate(object date, bool showTime)
        {
            return date == null || date == DBNull.Value ? string.Empty : Convert.ToDateTime(date).ToString("dd.MM.yyyy" + (showTime ? " HH:mm" : ""));
        }
        public static void FillDrp(DropDownList drp, object data, string id, string value, bool addSelect)
        {
            FillDrp(drp, data, id, value, addSelect, "Seçiniz");
        }
        public static string CreatPassword()
        {
            char[] character = "0123456789abcdefghijklmnoprstuvyz".ToCharArray(); //Şifrenin hangi harf ve sayılardan oluşacağını burada belirliyoruz
            string result = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++) //Şifremiz şuan 6 karakter içericek.
            {
                result += character[random.Next(0, character.Length - 1)].ToString();
            }

            return result;
        }
        public static void FillDrp(DropDownList drp, object data, string id, string value, bool addSelect, string selectText)
        {
            drp.Items.Clear();
            drp.DataSource = data;
            drp.DataTextField = value;
            drp.DataValueField = id;
            drp.DataBind();
            if (addSelect)
            {
                drp.Items.Insert(0, new ListItem(selectText, "0"));
            }
        }
        public static void FillLb(ListBox drp, object data, string id, string value, bool addSelect)
        {
            drp.Items.Clear();
            drp.DataSource = data;
            drp.DataTextField = value;
            drp.DataValueField = id;
            drp.DataBind();
            if (addSelect)
            {
                drp.Items.Insert(0, new ListItem("Hepsi", "0"));
                drp.Items[0].Selected = true;
            }

        }

        public static decimal CDecimal(object val)
        {
            decimal retval = 0;

            if (val is decimal)
            {
                retval = Convert.ToDecimal(val);
            }
            else if (val is string)
            {
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.CurrencyDecimalDigits = 2;
                nfi.CurrencyDecimalSeparator = ",";
                nfi.CurrencyGroupSeparator = ".";
                nfi.CurrencyGroupSizes = new int[] { 3 };
                nfi.NumberDecimalDigits = 2;
                nfi.NumberDecimalSeparator = ",";
                nfi.NumberGroupSeparator = ".";
                nfi.NumberGroupSizes = new int[] { 3 };
                nfi.PercentDecimalDigits = 2;
                nfi.PercentDecimalSeparator = ",";
                nfi.PercentGroupSeparator = ".";
                nfi.PercentGroupSizes = new int[] { 3 };
                retval = Convert.ToDecimal(val, nfi);
            }
            return retval;
        }

        public static DateTime CDate(object val)
        {
            DateTime retval = DateTime.MinValue;
            if (val is string)
            {
                DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                dfi.DateSeparator = ".";
                dfi.FullDateTimePattern = "dd.MM.yyyy HH:mm";
                dfi.LongDatePattern = "dd.MM.yyyy HH:mm";
                dfi.LongTimePattern = "HH:mm";
                dfi.ShortDatePattern = "dd.MM.yyyy";
                dfi.ShortTimePattern = "HH:mm";
                dfi.TimeSeparator = ":";
                retval = Convert.ToDateTime(val, dfi);
            }
            else
            {
                retval = Convert.ToDateTime(val);
            }
            return retval;
        }
        protected override void OnInit(EventArgs e)
        {
            if (LOGINREQUIRED)
            {
                if (ONLINEUSER.ID == Guid.Empty)
                {
                    Response.Redirect("/Login?ReturnUrl=/Login");
                }
            }

            base.OnInit(e);
        }
    }
    public class ONLINEUSER : MEMBER
    {
        public Guid REALMEMBERID { get; set; }
    }
}

