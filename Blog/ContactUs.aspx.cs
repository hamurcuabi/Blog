using Blog.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog
{
    public partial class ContactUs : BasePage
    {
        string title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (RouteData.Values["TITLE"] != null)
            {
                title = RouteData.Values["TITLE"].ToString();
                isQuestion();
            }
                rptrCategories.Bind(DALCategories.GetAll());
            rptLastPost.Bind(DALBlog.GetLastCount(3));



        }
        public String NewDate(String date)
        {

            return Functions.DateTimeChange(date);

        }
        public bool isQuestion() {
            if (string.IsNullOrEmpty(title)) return false;
            else return true;

        }
        public String GetCountBlog(String id)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "BLOG where CATEGORIESID=" + id;
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return "" + retval;


        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            
  
            Response.Redirect("/Mail");
            
               
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSendQuestion_Click(object sender, EventArgs e)
        {
            String name = txtAskName.Text;
            String quest = txtAskQuestion.Text;
            String name2 = txtName2.Text;
            String quest2 = txtQuestion2.Text;

            QUESTION q = new QUESTION();
            q.ANSWER = name+name2 + " Kişisine Henüz Cevap Verilmedi";
            q.ISACTIVE = false;
            q.TITLE = quest+quest2;
            q.SORT = Convert.ToInt16(DALQUESTION.GetLastSort() + 1);
            DALQUESTION.Insert(q);
            Response.Redirect("/Mail");
        }

        protected void btnClearQuestion_Click(object sender, EventArgs e)
        {
           

        }
    }
}