using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.DataLayer;

namespace Blog.Admin
{
    public partial class PasswordReset : BasePage
    {
        public Guid MEMBERID
        {
            get
            {
                if (ViewState["#MEMBERID#"] == null)
                {
                    ViewState["#MEMBERID#"] = Guid.Empty;
                }
                return Guid.Parse(ViewState["#MEMBERID#"].ToString());
            }
            set { ViewState["#MEMBERID#"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
                {
                    MEMBER mem = DALMember.GetByPasswordResetCode(Request.QueryString["Code"]);
                    if (mem != null)
                    {
                        MEMBERID = mem.ID;
                    }
                    else
                    {
                        Response.Redirect("/Login");
                    }
                }
                else
                {
                    Response.Redirect("/Login");
                }
            }
        }
        private bool ValidateNewPassword()
        {
            bool retval = true;
            if (string.IsNullOrEmpty(txtPasword.Text))
            {
                retval = false;
                NotificationAdd(NotificationType.error, " Şifre alanını giriniz ");

            }
            else if (string.IsNullOrEmpty(txtPasswordAgain.Text))
            {
                retval = false;
                NotificationAdd(NotificationType.error, " Şifre tekrarı alanını giriniz ");
            }
            else if (!txtPasword.Text.Equals(txtPasswordAgain.Text))
            {
                retval = false;
                NotificationAdd(NotificationType.error, " Şifreler aynı değil ");
            }
            else if (txtPasword.Text.Length < 5)
            {
                retval = false;
                NotificationAdd(NotificationType.error, " Şifreniz en az 6 karakter olmalıdır");
            }
            return retval;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateNewPassword())
            {
                MEMBER mem = DALMember.GetByID(MEMBERID);
                if (mem != null)
                {
                    if (mem.ISACTIVE)
                    {
                        mem.PASSWORD = Functions.MD5(txtPasword.Text);
                        mem.PASSWORDRESETCODE = Guid.NewGuid().ToString();
                        DALMember.Update(mem);
                        Functions.SetLoginUser(ONLINEUSER, mem);
                        DALMailQueue.SendPasswordMail(mem.EMAIL, mem.FULLNAME, txtPasword.Text);
                        Response.Redirect("/AdminMainPage?Reset=" + mem.PASSWORDRESETCODE + "");

                    }
                    else
                    {
                        NotificationAdd(NotificationType.error, "Kullanıcınız pasif durumdadır. Sistem yöneticisi ile iletişime geciniz.");
                    }
                }
                else
                {
                    NotificationAdd(NotificationType.error, "Kayıt Bulunamadı");
                }
            }
        }
    }
}