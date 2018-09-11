using System;
using System.IO;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Blog.DataLayer;

namespace Blog.Admin
{
    public partial class ADMAboutUsImageUpdate : BasePage
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
                ABOUTUS item = DALAboutUs.Get(RECid);
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
                    txtTitle.Text = item.TITLE;
                    chkActive.Checked = item.ISACTIVE;
                    hdnDescription.Value = Server.HtmlDecode(item.MAINTEXT);
                    btnSave.Text = "Güncelle";
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateItem())
            {
                ABOUTUS rec = new ABOUTUS();
                if (RECid > 0)
                {
                    rec = DALAboutUs.Get(RECid);
                    if (!string.IsNullOrEmpty(IMAGE))
                    {
                        rec.IMAGE = IMAGE;
                    }
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtTitle.Text;
                    DALAboutUs.Update(rec);
                }
                else
                {
                    rec.IMAGE = IMAGE;
                    rec.ALT = txtAlt.Text;
                    rec.ISACTIVE = chkActive.Checked;
                    rec.MAINTEXT = Server.HtmlEncode(hdnDescription.Value);
                    rec.TITLE = txtTitle.Text;
                    DALAboutUs.Insert(rec);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ClosePage", "CloseModal(true);", true);
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
            if (string.IsNullOrEmpty(hdnDescription.Value))
            {
                NotificationAdd(NotificationType.error, "Ana Yazı Alanını Giriniz");
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
                if (DALAboutUs.HasImage(IMAGE))
                {
                    // once orjinal dosyayi bir kaydedelim
                    orgImagePath = path + IMAGE + extension;
                    fuImage.SaveAs(orgImagePath);
                    // simdi sizelara gore kaydedelim
                    SaveImage(orgImagePath, Convert.ToInt32(GetPrameterValue("ABOUTUSWIDTH")), Convert.ToInt32(GetPrameterValue("ABOUTUSHEIGHT")), "_l");
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