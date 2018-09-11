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
    public partial class ADMBlogList : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        public static int rptCountBlog;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillBlog();
            }
        }
        private void fillBlog()
        {
            rptCountBlog = DALBlog.getCount();
            rptrBlog.Bind(DALBlog.GetAll().OrderBy(p => p.SORT));
            lblCountImages.Text = "(" + rptCountBlog.ToString() + ")";

            foreach (RepeaterItem item in rptrBlog.Items)
            {
                HiddenField hdn = item.FindControl("hdnImage") as HiddenField;
                Image img = item.FindControl("imgImage") as Image;
                if (string.IsNullOrEmpty(hdn.Value)) img.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/noimage.png";
                else img.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + hdn.Value + "_s.jpg";
                img.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                img.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));

            }
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillBlog();
        }

        protected void rptrBlog_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    {
                        BLOG self = DALBlog.Get(Int32.Parse(e.CommandArgument.ToString()));
                        BLOG upper = DALBlog.GetBySort(self.SORT + 1);
                        self.SORT++;
                        upper.SORT--;
                        DALBlog.Update(self);
                        DALBlog.Update(upper);
                        fillBlog();
                        break;
                    }
                case "Up":
                    {
                        BLOG self = DALBlog.Get(Int32.Parse(e.CommandArgument.ToString()));
                        BLOG upper = DALBlog.GetBySort(self.SORT - 1);
                        self.SORT--;
                        upper.SORT++;
                        DALBlog.Update(self);
                        DALBlog.Update(upper);
                        fillBlog();
                        break;
                    }
            }
        }

        protected void rptrBlog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    (e.Item.FindControl("btnUp") as LinkButton).Visible = false;
                    if (rptCountBlog == 1)
                    {
                        (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                    }
                }
                else if (e.Item.ItemIndex == rptCountBlog - 1)
                {
                    (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                }
            }
        }
        public String GetCategorieName(int id)
        {

            CATEGORIES c = DALCategories.Get(id);
            if (c != null) return c.TITLE;
            else return "Boş";
        }
    }
}