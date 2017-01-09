using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IOC_Web.Common;
using IOC_Web.Entity;
using IOC_Web.Models;
using IOC_Web.Service;

namespace IOC_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

         LogHelper.Log("pppp");
            LogHelper.Debug("DDD");
            LogHelper.Fatal("ffff");
            LogHelper.Warn("wwww");
            studentService.Delete(c => c.Id == 6);
            //CookieHelper.WriteCookie("userDate","admin1");
            //wcookie();
            CookieHelper.WriteAuthCookie("adminData", "Login");
            return View();
        }
        StudentService studentService;
        public HomeController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        public HomeController()
        {
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


            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExpExcel()
        {
            var path = Server.MapPath("/Upload/1.xlsx");
            var table = NPOIHelper.ExcelToDataTable(path);
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            int n = 0;
            #region MyRegion
            //var task = Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < table.Rows.Count / 4; i++)
            //    {
            //        Student student = new Student();
            //        student.Id = i;
            //        student.Graduation = table.Rows[i]["Graduation"].ToString();
            //        student.Major = table.Rows[i]["Major"].ToString();
            //        student.Name = table.Rows[i]["Name"].ToString();
            //        student.School = table.Rows[i]["School"].ToString();

            //        using (IOC_DbContext db = new IOC_DbContext())
            //        {
            //            db.Students.Add(student);
            //            db.SaveChanges();
            //            LogHelper.Log("线程1："+i + "添加成功--"+n++);
            //        }


            //    }
            //});
            //var task1 = Task.Factory.StartNew(() =>
            //{
            //    for (int i = table.Rows.Count / 4; i < table.Rows.Count / 4 * 2; i++)
            //    {
            //        Student student = new Student();
            //        student.Id = i;
            //        student.Graduation = table.Rows[i]["Graduation"].ToString();
            //        student.Major = table.Rows[i]["Major"].ToString();
            //        student.Name = table.Rows[i]["Name"].ToString();
            //        student.School = table.Rows[i]["School"].ToString();

            //        using (IOC_DbContext db = new IOC_DbContext())
            //        {
            //            db.Students.Add(student);
            //            db.SaveChanges();
            //            LogHelper.Log("线程2：" + i + "添加成功--" + n++);
            //        }

            //    }
            //});
            //var task2 = Task.Factory.StartNew(() =>
            //{
            //    for (int i = table.Rows.Count / 4 * 2; i < table.Rows.Count / 4 * 3; i++)
            //    {
            //        Student student = new Student();
            //        student.Id = i;
            //        student.Graduation = table.Rows[i]["Graduation"].ToString();
            //        student.Major = table.Rows[i]["Major"].ToString();
            //        student.Name = table.Rows[i]["Name"].ToString();
            //        student.School = table.Rows[i]["School"].ToString();

            //        using (IOC_DbContext db = new IOC_DbContext())
            //        {
            //            db.Students.Add(student);
            //            db.SaveChanges();
            //            LogHelper.Log("线程3：" + i + "添加成功--" + n++);
            //        }

            //    }
            //});
            //var task3 = Task.Factory.StartNew(() =>
            // {
            //     for (int i = table.Rows.Count / 4 * 3; i < table.Rows.Count; i++)
            //     {
            //         Student student = new Student();
            //         student.Id = i;
            //         student.Graduation = table.Rows[i]["Graduation"].ToString();
            //         student.Major = table.Rows[i]["Major"].ToString();
            //         student.Name = table.Rows[i]["Name"].ToString();
            //         student.School = table.Rows[i]["School"].ToString();

            //         using (IOC_DbContext db = new IOC_DbContext())
            //         {
            //             db.Students.Add(student);
            //             db.SaveChanges();
            //             LogHelper.Log("线程4：" + i + "添加成功--" + n++);
            //         }

            //     }
            // });

            #endregion
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
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



        public ActionResult Select2()
        {
            return View();
        }

    }
}