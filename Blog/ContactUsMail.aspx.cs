﻿using Blog.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog
{
    public partial class ContactUsMail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AsyncMode = true;
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