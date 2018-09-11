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
    public partial class ADMQuestion : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        public static int rptCountQuestion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillQuestion();
            }
        }
        private void fillQuestion()
        {
            rptCountQuestion = DALQUESTION.GetCount();
            rptrQuestion.Bind(DALQUESTION.GetAll().OrderBy(p => p.SORT));
            lblCountImages.Text = "(" + rptCountQuestion.ToString() + ")";
        }

        protected void btnRefreshList_Click(object sender, EventArgs e)
        {
            fillQuestion();
        }

        protected void rptrQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    {
                        QUESTION self = DALQUESTION.Get(Int32.Parse(e.CommandArgument.ToString()));
                        QUESTION upper = DALQUESTION.GetBySort(self.SORT + 1);
                        self.SORT++;
                        upper.SORT--;
                        DALQUESTION.Update(self);
                        DALQUESTION.Update(upper);
                        fillQuestion();
                        break;
                    }
                case "Up":
                    {
                        QUESTION self = DALQUESTION.Get(Int32.Parse(e.CommandArgument.ToString()));
                        QUESTION upper = DALQUESTION.GetBySort(self.SORT - 1);
                        self.SORT--;
                        upper.SORT++;
                        DALQUESTION.Update(self);
                        DALQUESTION.Update(upper);
                        fillQuestion();
                        break;
                    }
            }
        }

        protected void rptrQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    (e.Item.FindControl("btnUp") as LinkButton).Visible = false;
                    if (rptCountQuestion == 1)
                    {
                        (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                    }
                }
                else if (e.Item.ItemIndex == rptCountQuestion - 1)
                {
                    (e.Item.FindControl("btnDown") as LinkButton).Visible = false;
                }
            }
        }
    }
}