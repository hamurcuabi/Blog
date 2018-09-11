using System;
using System.Web.UI;
using Blog.DataLayer;

namespace Blog.Admin
{
    public partial class ADMAboutUsCounterUpdate : BasePage
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
                InitializePage();
            }
        }
        private void InitializePage()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                RECid = Convert.ToInt16(Request.QueryString["ID"]);
                ABOUTUSCOUNTER item = DALAboutUsCounter.Get(RECid);
                if (item != null)
                {
                    hdnDescription.Value = Server.HtmlDecode(item.MAINTEXT);
                    txtTitle.Text = item.TITLE;
                    btnSave.Text = "Güncelle";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateItem())
            {
                ABOUTUSCOUNTER rec = new ABOUTUSCOUNTER();
                if (RECid > 0)
                {
                    rec = DALAboutUsCounter.Get(RECid);
                    rec.MAINTEXT = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtTitle.Text;
                    DALAboutUsCounter.Update(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
            }
        }
        protected bool ValidateItem()
        {
            bool retval = true;
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                NotificationAdd(NotificationType.error, "Başlık Alanını Giriniz");
                retval = false;
            }
            if (string.IsNullOrEmpty(hdnDescription.Value))
            {
                NotificationAdd(NotificationType.error, "Ana Yazı Alanını Giriniz");
                retval = false;
            }
            return retval;
        }

    }
}