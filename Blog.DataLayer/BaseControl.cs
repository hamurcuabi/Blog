using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class BaseControl : System.Web.UI.UserControl
    {
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
    }
}