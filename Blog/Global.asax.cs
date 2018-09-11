using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Blog.DataLayer;

namespace Blog
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add("AdminAboutUsList", new Route("AdminAboutUsList", new PageRouteHandler("~/Admin/ADMQuestion.aspx")));
            RouteTable.Routes.Add("AdminBlogList", new Route("AdminBlogList", new PageRouteHandler("~/Admin/ADMBlogList.aspx")));
            RouteTable.Routes.Add("AdminMainPageList", new Route("AdminMainPageList", new PageRouteHandler("~/Admin/ADMBlogList.aspx")));
            RouteTable.Routes.Add("Login", new Route("Login", new PageRouteHandler("~/Admin/NewLogin.aspx")));
            RouteTable.Routes.Add("AdminMainPage", new Route("AdminMainPage", new PageRouteHandler("~/Admin/ADMBlogList.aspx")));
            RouteTable.Routes.Add("AdminPasswordReset", new Route("AdminPasswordReset", new PageRouteHandler("~/Admin/PasswordReset.aspx")));
            RouteTable.Routes.Add("NewLogin", new Route("NewLogin", new PageRouteHandler("~/Admin/NewLogin.aspx")));
           
            RouteTable.Routes.Add("Logout", new Route("Logout", new PageRouteHandler("~/Admin/Logout.aspx")));
            //Benim ekledikleirm
            RouteTable.Routes.Add("AdminCategoriesList", new Route("AdminCategoriesList", new PageRouteHandler("~/Admin/ADMCategoriesList.aspx")));
            RouteTable.Routes.Add("kategoriler-detay", new Route("kategoriler/{title}", new PageRouteHandler("~/Categories.aspx")));
            RouteTable.Routes.Add("Iletisim-detay", new Route("Iletisim/{title}", new PageRouteHandler("~/ContactUs.aspx")));

            RouteTable.Routes.Add("", new Route("", new PageRouteHandler("~/Index.aspx")));
            RouteTable.Routes.Add("Anasayfa", new Route("Anasayfa", new PageRouteHandler("~/Index.aspx")));
            RouteTable.Routes.Add("Hakkımda", new Route("Hakkımda", new PageRouteHandler("~/AboutMe.aspx")));
            RouteTable.Routes.Add("Iletisim", new Route("Iletisim", new PageRouteHandler("~/ContactUs.aspx")));
           
            RouteTable.Routes.Add("Mail", new Route("Mail", new PageRouteHandler("~/ContactUsMail.aspx")));

            RouteTable.Routes.Add("Blog", new Route("Blog", new PageRouteHandler("~/BlogList.aspx")));
            RouteTable.Routes.Add("blog-detay", new Route("blog/{title}", new PageRouteHandler("~/Blog.aspx")));
            //Bitti
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
