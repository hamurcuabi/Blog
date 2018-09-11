using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.DataLayer;
using System.Data.SqlClient;
namespace Blog.Admin
{
    public partial class ADMAboutUsList : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillAbout();
            }
        }
        private void fillAbout()
        {
            rptrAbout.Bind(DALAboutUs.GetAll().OrderBy(p => p.ID));
            lblCountAboutUs.Text = "(" + rptrAbout.Items.Count.ToString() + ")";
            foreach (RepeaterItem item in rptrAbout.Items)
            {
                HiddenField hdn = item.FindControl("hdnImage") as HiddenField;
                Image img = item.FindControl("imgImage") as Image;
                img.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + hdn.Value + "_s.jpg";
                img.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                img.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));
            }
            rptrCounter.Bind(DALAboutUsCounter.GetAll().OrderBy(p => p.ID));
            lblCountCounter.Text = "(" + rptrCounter.Items.Count.ToString() + ")";
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillAbout();
        }
    }
}