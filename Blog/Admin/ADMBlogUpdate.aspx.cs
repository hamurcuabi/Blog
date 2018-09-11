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
    public partial class ADMBlogUpdate : BasePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            LOGINREQUIRED = true;
            base.OnPreInit(e);
        }
        private string IMAGE
        {
            get
            {
                if (ViewState["#IMAGE#"] == null)
                {
                    ViewState["#IMAGE#"] = string.Empty;
                }
                return ViewState["#IMAGE#"].ToString();
            }
            set { ViewState["#IMAGE#"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                drpFill();
                InitializePage();
               
            }
        }

        private void drpFill()
        {
            drpCatergori.DataSource = DALCategories.GetAll();
            drpCatergori.DataTextField = "TITLE";
            drpCatergori.DataValueField = "ID";
            drpCatergori.DataBind();
        }

        private void InitializePage()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                RECid = Convert.ToInt16(Request.QueryString["ID"]);
                BLOG item = DALBlog.Get(RECid);
                if (item != null)
                {
                    if (string.IsNullOrEmpty(item.IMAGE))
                    {
                        tdImage.Visible = true;
                    }
                    else
                    {
                        tdImage.Visible = true;
                        tdImagePreview.Visible = true;
                    }
                    
                    imgImage.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + item.IMAGE + "_s.jpg";
                    imgImage.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                    imgImage.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));
                    if (drpCatergori.Items.Count >= 1) { drpCatergori.SelectedValue= item.CATEGORIESID.ToString(); }
                    txtVideo.Text = item.VIDEOCODE;
                    txtAlt.Text = item.ALT;
                    hdnDescription.Value = Server.HtmlDecode(item.MAINTEXT);
                    txtTitle.Text = item.TITLE;
                    chkActive.Checked = item.ISACTIVE;
                    btnDelete.Visible = true;
                    if (!string.IsNullOrEmpty(item.VIDEOCODE))
                    {
                        item.ISIMAGE = false;
                        item.ISVIDEO = true;
                        item.ISARTICLE = false;
                    }
                    else if (!string.IsNullOrEmpty(item.IMAGE))
                    {
                        item.ISIMAGE = true;
                        item.ISVIDEO = false;
                        item.ISARTICLE = false;
                    }
                    else
                    {
                        item.ISIMAGE = false;
                        item.ISVIDEO = false;
                        item.ISARTICLE = true;
                    }
                    btnSave.Text = "Güncelle";
                }
            }
            else
            {
                tdImage.Visible = true;
            }
        }
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fuImage.HasFile)
            {
                string path = Server.MapPath("~") + "Admin\\assets\\images\\";
                string extension = Path.GetExtension(fuImage.FileName);
                string orgImagePath = string.Empty;
                string[] name = fuImage.FileName.Split(new string[] { extension }, StringSplitOptions.None);
                IMAGE = name[0];
                if (DALBlog.HasImage(IMAGE))
                {
                    // once orjinal dosyayi bir kaydedelim
                    orgImagePath = path + IMAGE + extension;
                    fuImage.SaveAs(orgImagePath);
                    // simdi sizelara gore kaydedelim
                    SaveImage(orgImagePath, Convert.ToInt32(GetPrameterValue("DISEASESWIDTH")), Convert.ToInt32(GetPrameterValue("DISEASESHEIGHT")), "_l");
                    SaveImage(orgImagePath, Convert.ToInt32(GetPrameterValue("SLIDERADMINWIDTH")), Convert.ToInt32(GetPrameterValue("SLIDERADMINHEIGHT")), "_s");
                    tdImage.Visible = false;
                    tdImagePreview.Visible = true;
                    imgImage.ImageUrl = Functions.SITEURL.PathAndQuery + "Admin/assets/images/" + IMAGE + "_s" + extension;
                    imgImage.Width = new Unit(GetPrameterValue("SLIDERADMINWIDTH"));
                    imgImage.Height = new Unit(GetPrameterValue("SLIDERADMINHEIGHT"));
                }
                else
                {
                    NotificationAdd(NotificationType.error, "Lütfen Resmin Adını Değiştiriniz");
                }

            }
        }
        private void SaveImage(string path, int width, int height, string prefix)
        {
            Bitmap imgOrj = (System.Drawing.Image.FromFile(path) as Bitmap);
            // orani bulalim once
            decimal division = Convert.ToDecimal(imgOrj.Width) / Convert.ToDecimal(width) > Convert.ToDecimal(imgOrj.Height) / Convert.ToDecimal(height) ? Convert.ToDecimal(imgOrj.Width) / Convert.ToDecimal(width) : Convert.ToDecimal(imgOrj.Height) / Convert.ToDecimal(height);
            // resimi orantili olarak kucultelim
            Size szProccess = new Size(Convert.ToInt32(imgOrj.Width / division), Convert.ToInt32(imgOrj.Height / division));
            Bitmap imgProccessed = new Bitmap(imgOrj, szProccess);
            // arka planini hazirlayalim
            Bitmap imgBackground = new Bitmap(width, height);
            //// arka plani boyuyalim
            string bg = GetPrameterValue("PRODUCTIMAGEBG").Replace("#", string.Empty);
            Color bgColor = Color.FromArgb(255, Int32.Parse(bg.Substring(0, 2), NumberStyles.HexNumber), Int32.Parse(bg.Substring(2, 2), NumberStyles.HexNumber), Int32.Parse(bg.Substring(4, 2), NumberStyles.HexNumber));

            for (int i = 0; i < width; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    imgBackground.SetPixel(i, y, bgColor);
                }
            }

            // arka planin uzerine yeni resimi koyalim
            Graphics bgImage = Graphics.FromImage(imgBackground);
            int positionX = (width - imgProccessed.Width) / 2;
            int positionY = (height - imgProccessed.Height) / 2;
            bgImage.DrawImage(imgProccessed, positionX, positionY);
            imgBackground.Save(path.Substring(0, path.LastIndexOf("\\") + 1) + Path.GetFileNameWithoutExtension(path) + prefix + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            imgOrj.Dispose();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BLOG rec = DALBlog.Get(Int32.Parse(Request.QueryString["ID"]));
                if (rec != null)
                {
                    DALBlog.Delete(rec.ID);
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
                BLOG rec = new BLOG();
                if (RECid > 0)
                {
                    rec = DALBlog.Get(RECid);
                    if (!string.IsNullOrEmpty(IMAGE))
                    {
                        rec.IMAGE = IMAGE;

                    }
                    if (!string.IsNullOrEmpty(txtVideo.Text))
                    {
                        rec.ISIMAGE = false;
                        rec.ISVIDEO = true;
                        rec.ISARTICLE = false;
                    }
                    else if (!string.IsNullOrEmpty(rec.IMAGE))
                    {
                        rec.ISIMAGE = true;
                        rec.ISVIDEO = false;
                        rec.ISARTICLE = false;
                    }
                    else
                    {
                        rec.ISIMAGE = false;
                        rec.ISVIDEO = false;
                        rec.ISARTICLE = true;
                    }
                    if (drpCatergori.Items.Count >= 1) rec.CATEGORIESID = Int32.Parse(drpCatergori.SelectedItem.Value);
                    else rec.CATEGORIESID = -1;
                    rec.EDITOR = ONLINEUSER.NAME;
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtTitle.Text;
                    rec.URLSEO = Code.Helper.SEOUrl(txtTitle.Text);
                    rec.VIDEOCODE = txtVideo.Text;
                    DALBlog.Update(rec);
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtVideo.Text))
                    {
                        rec.ISIMAGE = false;
                        rec.ISVIDEO = true;
                        rec.ISARTICLE = false;
                    }
                    else if (!string.IsNullOrEmpty(IMAGE))
                    {
                        rec.ISIMAGE = true;
                        rec.ISVIDEO = false;
                        rec.ISARTICLE = false;
                    }
                    else
                    {
                        rec.ISIMAGE = false;
                        rec.ISVIDEO = false;
                        rec.ISARTICLE = true;
                    }
                    if (drpCatergori.Items.Count >= 1) rec.CATEGORIESID = Int32.Parse(drpCatergori.SelectedItem.Value);
                    else rec.CATEGORIESID = -1;
                    rec.VIDEOCODE = txtVideo.Text;
                    rec.EDITOR = ONLINEUSER.NAME;
                    rec.IMAGE = IMAGE;
                    rec.SORT = Convert.ToInt16(DALBlog.GetLastSort() + 1);
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtTitle.Text;
                    rec.URLSEO = Code.Helper.SEOUrl(txtTitle.Text);
                    rec.DATE = DateTime.Now;
                    DALBlog.Insert(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
                btnDelete.Visible = false;
                btnCancel.Visible = false;
            }
        }
        protected bool ValidateItem()
        {
            bool retval = true;

            if (string.IsNullOrEmpty(txtAlt.Text))
            {
                NotificationAdd(NotificationType.error, "Alt Alanını Giriniz");
                retval = false;
            }
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