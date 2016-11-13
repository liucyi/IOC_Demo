using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IOC_Web.Common;
using IOC_Web.Entity;
using IOC_Web.Models;

namespace IOC_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

         
 
            //CookieHelper.WriteCookie("userDate","admin1");
            //wcookie();
           CookieHelper.WriteAuthCookie("adminData", "Login");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var s = CookieHelper.GetCookie("Login");
            var ticket2 = CookieHelper.GetAuthCookie("ioc.authcookie");

            //string[] strInfo = ticket2.UserData.Split(new char[] {'|'});
            //string id1 = strInfo[0].ToString();
            //初始化登陆后的数据


            Student user = null;

            if (HttpContext.User != null
                && HttpContext.User.Identity.IsAuthenticated
                && HttpContext.User.Identity.Name != string.Empty
                && HttpContext.User.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity id = HttpContext.User.Identity as FormsIdentity;

                if (id != null)
                {
                    FormsAuthenticationTicket ticket = id.Ticket;

                   // user = this.DeserializeUserInfo(ticket.UserData);

                }

               
            } return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       


        private void wcookie()
        {
            string userInfo = "admin" + "|" + "192.168.1.1";
            FormsAuthenticationTicket ticket =
                      new FormsAuthenticationTicket(1, "userInfo", DateTime.Now, DateTime.Now.AddDays(1), false, userInfo);
            String cookieStr = FormsAuthentication.Encrypt(ticket);
            // Send the cookie to the client 
            Response.Cookies["userInfo"].Value = cookieStr;
            Response.Cookies["userInfo"].Path = "/";
        }

    }
}