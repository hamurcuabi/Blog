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
    public partial class ADMDiseasesList : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        public static int rptCountDiseases;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillDiseases();
            }
        }
        private void fillDiseases()
        {
            rptCountDiseases = DALDiseases.getCount();
            rptrDiseases.Bind(DALDiseases.GetAll().OrderBy(p => p.SORT));
            lblCountImages.Text = "(" + rptCountDiseases.ToString() + ")";

            foreach (RepeaterItem item in rptrDiseases.Items)
            {
                HiddenField hdn = item.FindControl("hdnImage") as HiddenField;
                Image img = item.FindControl("imgImage") as Image;
                img.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + hdn.Value + "_s.jpg";
                img.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                img.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));
            }
        }

        protected void rptrDiseases_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    {
                        DISEASES self = DALDiseases.Get(Int32.Parse(e.CommandArgument.ToString()));
                        DISEASES upper = DALDiseases.GetBySort(self.SORT + 1);
                        self.SORT++;
                        upper.SORT--;
                        DALDiseases.Update(self);
                        DALDiseases.Update(upper);
                        fillDiseases();
                        break;
                    }
                case "Up":
                    {
                        DISEASES self = DALDiseases.Get(Int32.Parse(e.CommandArgument.ToString()));
                        DISEASES upper = DALDiseases.GetBySort(self.SORT - 1);
                        self.SORT--;
                        upper.SORT++;
                        DALDiseases.Update(self);
                        DALDiseases.Update(upper);
                        fillDiseases();
                        break;
                    }
            }
        }

        protected void rptrDiseases_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    (e.Item.FindControl("btnUp") as LinkButton).Visible = false;
                    if (rptCountDiseases == 1)
                    {
                        (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                    }
                }
                else if (e.Item.ItemIndex == rptCountDiseases - 1)
                {
                    (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                }
            }
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillDiseases();
        }
    }
}