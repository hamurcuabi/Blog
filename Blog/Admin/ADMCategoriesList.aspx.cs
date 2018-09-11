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
    public partial class ADMCategoriesList : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        public static int rptCountCategories;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillCategories();
            }
        }
        private void fillCategories()
        {
            rptCountCategories = DALCategories.getCount();
            rptCategories.Bind(DALCategories.GetAll().OrderBy(p => p.SORT));
            lblCountCategories.Text = "(" + rptCountCategories.ToString() + ")";
        }

        protected void rptCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    {
                        CATEGORIES self = DALCategories.Get(Int32.Parse(e.CommandArgument.ToString()));
                        CATEGORIES upper = DALCategories.GetBySort(self.SORT + 1);
                        self.SORT++;
                        upper.SORT--;
                        DALCategories.Update(self);
                        DALCategories.Update(upper);
                        fillCategories();
                        break;
                    }
                case "Up":
                    {
                        CATEGORIES self = DALCategories.Get(Int32.Parse(e.CommandArgument.ToString()));
                        CATEGORIES upper = DALCategories.GetBySort(self.SORT - 1);
                        self.SORT--;
                        upper.SORT++;
                        DALCategories.Update(self);
                        DALCategories.Update(upper);
                        fillCategories();
                        break;
                    }
            }
        }

        protected void rptCategories_ItemBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    (e.Item.FindControl("btnUp") as LinkButton).Visible = false;
                    if (rptCountCategories == 1)
                    {
                        (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                    }
                }
                else if (e.Item.ItemIndex == rptCountCategories - 1)
                {
                    (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                }
            }
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillCategories();
        }
    }
}