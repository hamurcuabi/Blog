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
    public partial class ADMCategoriesUpdate : BasePage
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
                CATEGORIES item = DALCategories.Get(RECid);
                if (item != null)
                {
                  
      
                    txtTitle.Text = item.TITLE;
                    chkActive.Checked = (bool)item.ISACTIVE;
                    btnDelete.Visible = true;
                    btnSave.Text = "Güncelle";
                }
            }
         
        }
        public bool GetCountBlog(int id)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "BLOG where CATEGORIESID=" + id;
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            if (retval > 0) return true;
            else return false; ;


        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CATEGORIES rec = DALCategories.Get(Int32.Parse(Request.QueryString["ID"]));
                if (rec != null)
                {
                    if(!GetCountBlog(rec.ID))DALCategories.Delete(rec.ID);
                    else NotificationAdd(NotificationType.error, "Kategoriyi Silmek için bağlı olan yazıları silin!");

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
                CATEGORIES rec = new CATEGORIES();
                if (RECid > 0)
                {
                    rec = DALCategories.Get(RECid);
                    if (!string.IsNullOrEmpty(Title))
                    {
                        rec.TITLE = Title;
                    }
                    rec.ISACTIVE = chkActive.Checked;
                    rec.TITLE = txtTitle.Text;
                    rec.URLSEO = Code.Helper.SEOUrl(txtTitle.Text);
                    DALCategories.Update(rec);
                }
                else
                {
                   
                    rec.SORT = Convert.ToInt16(DALCategories.GetLastSort() + 1);
                    rec.ISACTIVE = chkActive.Checked;
                    rec.TITLE = txtTitle.Text;
                    rec.URLSEO = Code.Helper.SEOUrl(txtTitle.Text);
                    DALCategories.Insert(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
                btnDelete.Visible = false;
                btnCancel.Visible = false;
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
          
            if (DALCategories.HasTitle(txtTitle.Text))
            {
                NotificationAdd(NotificationType.error, "Title Alanını Değiştiriniz.");
                retval = false;
            }
            return retval;
        }
    }
}