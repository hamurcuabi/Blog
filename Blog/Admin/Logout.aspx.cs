using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.DataLayer;

namespace Blog.Admin
{
    public partial class Logout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ONLINEUSER"] = string.Empty;
            Session.Abandon();
            Session.Clear();

            if (Response.Cookies["MEMBER"] != null)
            {
                Response.Cookies["MEMBER"].Value = string.Empty;
                Response.Cookies["MEMBER"].Expires = DateTime.Now.AddDays(-2);
            }
            Response.Redirect(ConfigurationManager.AppSettings["LOGINURL"]);
        }
    }
}