using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Blog.DataLayer;
using System.Data.SqlClient;

namespace Blog.Admin
{
    public partial class ADMQuestionUpdate : BasePage
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
                QUESTION item = DALQUESTION.Get(RECid);
                if (item != null)
                {


                    hdnDescription.Value = Server.HtmlDecode(item.ANSWER);
                    txtQuestion.Text = item.TITLE;
                    chkActive.Checked = item.ISACTIVE;

                    btnDelete.Visible = true;
                    btnSave.Text = "Güncelle";
                }
            }

        }




        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                QUESTION rec = DALQUESTION.Get(Int32.Parse(Request.QueryString["ID"]));
                if (rec != null)
                {
                    DALQUESTION.Delete(rec.ID);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
                NotificationAdd(NotificationType.success, "Kayıt Silindi.");
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    if ((ex as SqlException).Number == 547)
                    {
                        NotificationAdd(NotificationType.error, "Kayıt başka yerlerde kullanıdı.");
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateItem())
            {
                QUESTION rec = new QUESTION();
                if (RECid > 0)
                {
                    rec = DALQUESTION.Get(RECid);
                    rec.ISACTIVE = chkActive.Checked;
                    rec.ANSWER = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtQuestion.Text;
                    DALQUESTION.Update(rec);
                }
                else
                {

                    rec.SORT = Convert.ToInt16(DALQUESTION.GetLastSort() + 1);
                    rec.ISACTIVE = chkActive.Checked;
                    rec.ANSWER = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtQuestion.Text;
                    DALQUESTION.Insert(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
                btnDelete.Visible = false;
                btnCancel.Visible = false;
            }
        }

        protected bool ValidateItem()
        {
            bool retval = true;

            if (string.IsNullOrEmpty(txtQuestion.Text))
            {
                NotificationAdd(NotificationType.error, "Alt Alanını Giriniz");
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