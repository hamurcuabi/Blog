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
    public partial class ADMMainPageSliderUpdate : BasePage
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
                InitializePage();
            }
        }
        private void InitializePage()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                RECid = Convert.ToInt16(Request.QueryString["ID"]);
                MAINPAGESLIDER item = DALMainPageSlider.Get(RECid);
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
                    txtAlt.Text = item.ALT;
                    txtMottto.Text = item.MAINTEXT;
                    txtTitle.Text = item.TITLE;
                    chkActive.Checked = item.ISACTIVE;
                    drpDirection.SelectedValue = item.DIRECTION;
                    btnDelete.Visible = true;
                    btnSave.Text = "Güncelle";
                }
            }
            else
            {
                tdImage.Visible = true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MAINPAGESLIDER rec = DALMainPageSlider.Get(Int32.Parse(Request.QueryString["ID"]));
                if (rec != null)
                {
                    DALMainPageSlider.Delete(rec.ID);
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
                MAINPAGESLIDER rec = new MAINPAGESLIDER();
                if (RECid > 0)
                {
                    rec = DALMainPageSlider.Get(RECid);
                    if (!string.IsNullOrEmpty(IMAGE))
                    {
                        rec.IMAGE = IMAGE;
                    }
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = txtMottto.Text;
                    rec.TITLE = txtTitle.Text;
                    rec.DIRECTION = drpDirection.SelectedValue;
                    DALMainPageSlider.Update(rec);
                }
                else
                {
                    rec.IMAGE = IMAGE;
                    rec.SORT = Convert.ToInt16(DALMainPageSlider.GetLastSort() + 1);
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = txtMottto.Text;
                    rec.TITLE = txtTitle.Text;
                    rec.DIRECTION = drpDirection.SelectedValue;
                    DALMainPageSlider.Insert(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
                btnDelete.Visible = false;
                btnCancel.Visible = false;
            }
        }
        protected bool ValidateItem()
        {
            bool retval = true;
            if (RECid == 0 && string.IsNullOrEmpty(IMAGE))
            {
                NotificationAdd(NotificationType.error, "Resim yükleyiniz");
                retval = false;
            }
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
            if (string.IsNullOrEmpty(txtMottto.Text))
            {
                NotificationAdd(NotificationType.error, "Motto Alanını Giriniz");
                retval = false;
            }
            return retval;
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
                if (DALMainPageSlider.HasImage(IMAGE))
                {
                    // once orjinal dosyayi bir kaydedelim
                    orgImagePath = path + IMAGE + extension;
                    fuImage.SaveAs(orgImagePath);
                    // simdi sizelara gore kaydedelim
                    SaveImage(orgImagePath, Convert.ToInt32(GetPrameterValue("SLIDERWIDTH")), Convert.ToInt32(GetPrameterValue("SLIDERHEIGHT")), "_l");
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

    }
}