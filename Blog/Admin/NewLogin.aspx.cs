using Blog.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.Admin
{
    public partial class NewLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtPassword.Attributes.Add("onkeypress", "button_click(this,'" + this.btnLogin.ClientID + "')");
                if (ONLINEUSER.ID != Guid.Empty)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        Response.Redirect(Request.QueryString["ReturnUrl"]);
                    }
                    else
                    {
                        Response.Redirect("/AdminMainPage");
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            txtLoginName.Text = EmailReplace(txtLoginName.Text);
            if (!string.IsNullOrEmpty(txtLoginName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                MEMBER m = DALMember.GetByEmailAndPassword(txtLoginName.Text, Functions.MD5(txtPassword.Text));
                if (m != null)
                {
                    if (m.ISACTIVE)
                    {
                        Functions.SetLoginUser(ONLINEUSER, m);

                        
                        if (chkRemember.Checked)
                        {
                            RememberMember(m.ID);
                        }
                        
                        if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        {
                            Response.Redirect(Request.QueryString["ReturnUrl"]);
                        }
                        else
                        {
                            Response.Redirect("/AdminMainPage");
                        }
                    }
                    else
                    {
                        NotificationAdd(NotificationType.error, "Kullanıcınız pasif durumdadır. Sistem yöneticisi ile iletişime geciniz.");
                    }
                }
                else
                {
                    NotificationAdd(NotificationType.error, "Geçersiz e-posta adı veya şifre!");
                }
            }
            else
            {
                NotificationAdd(NotificationType.error, "Kullanıcı Adı ya da Şifrenizi Giriniz");
            }
        }
        private void RememberMember(Guid memberID)
        {
            Response.Cookies["MEMBER"].Value = Functions.EncryptString(memberID.ToString());
            Response.Cookies["MEMBER"].Expires = DateTime.MaxValue;
        }

    }
}