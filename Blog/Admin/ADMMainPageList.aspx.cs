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
    public partial class ADMMainPageList : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        public static int rptCountSlider;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillSlider();
            }
        }
        private void fillSlider()
        {
            rptCountSlider = DALMainPageSlider.getCount();
            rptrSlider.Bind(DALMainPageSlider.GetAll().OrderBy(p => p.SORT));
            lblCountImages.Text = "(" + rptCountSlider.ToString() + ")";
          
            foreach (RepeaterItem item in rptrSlider.Items)
            {
                HiddenField hdn = item.FindControl("hdnImage") as HiddenField;
                Image img = item.FindControl("imgImage") as Image;
                img.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + hdn.Value + "_s.jpg";
                img.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                img.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));
            }
        }


        protected void rptrSlider_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    {
                        MAINPAGESLIDER self = DALMainPageSlider.Get(Int32.Parse(e.CommandArgument.ToString()));
                        MAINPAGESLIDER upper = DALMainPageSlider.GetBySort(self.SORT + 1);
                        self.SORT++;
                        upper.SORT--;
                        DALMainPageSlider.Update(self);
                        DALMainPageSlider.Update(upper);
                        fillSlider();
                        break;
                    }
                case "Up":
                    {
                        MAINPAGESLIDER self = DALMainPageSlider.Get(Int32.Parse(e.CommandArgument.ToString()));
                        MAINPAGESLIDER upper = DALMainPageSlider.GetBySort(self.SORT - 1);
                        self.SORT--;
                        upper.SORT++;
                        DALMainPageSlider.Update(self);
                        DALMainPageSlider.Update(upper);
                        fillSlider();
                        break;
                    }
            }
        }

        protected void rptrSlider_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    (e.Item.FindControl("btnUp") as LinkButton).Visible = false;
                    if (rptCountSlider == 1)
                    {
                        (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                    }
                }
                else if (e.Item.ItemIndex == rptCountSlider - 1)
                {
                    (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                }
            }
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillSlider();
        }
    }
}