using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.DataLayer;

namespace Blog.Controler
{
    public partial class SliderBlog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Request.RequestContext.RouteData.Values["TITLE"] != null)
            {
                string title = HttpContext.Current.Request.RequestContext.RouteData.Values["TITLE"].ToString();
                lblTitle.Text =title;
            }
            else lblTitle.Text = "";
        }
    }
}