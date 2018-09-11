using Blog.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog
{
    public partial class Categories : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string title = "";
            if (RouteData.Values["TITLE"] != null)
            {
                title = RouteData.Values["TITLE"].ToString();
                CATEGORIES c = DALCategories.GetTitle(title);
                rptPostTitle.Bind(DALBlog.GetByCategoriesID(c.ID));


            }
         
            rptrCategories.Bind(DALCategories.GetAll());
            rptLastPost.Bind(DALBlog.GetLastCount(3));
            


        }
        public String NewDate(String date)
        {

            return Functions.DateTimeChange(date);

        }
        public String GetCountBlog(String id)
        {
            string sql = string.Empty;
            short retval = 0;
            sql = "SELECT ISNULL(COUNT(ID),0) FROM " + Functions.TABLEPREFIX + "BLOG where CATEGORIESID=" + id;
            retval = Convert.ToInt16(SQLMs.RunScalar(sql));
            return "" + retval;


        }
    }
}