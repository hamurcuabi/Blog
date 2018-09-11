using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.DataLayer;

namespace Blog.Admin.UserControl
{
    public partial class UCHeader : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMemberName.Text = ONLINEUSER.FULLNAME;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Logout");
        }
    }
}