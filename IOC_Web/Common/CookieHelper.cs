

using System;
using System.Web;
using System.Web.Security;

namespace IOC_Web.Common
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public static class CookieHelper
    {
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        /// <summary>  
        /// 清除指定Cookie  
        /// </summary>  
        /// <param name="cookiename">cookiename</param>  
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        //利用asp.net中的form验证加密数据，写入Cookie
        public static HttpCookie WriteAuthCookie(string userData, string userName)
        {

        
            //登录票证
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1,
               userName,
               DateTime.Now,
               DateTime.Now.AddDays(1),
               false,
               userData,
               FormsAuthentication.FormsCookiePath  //可在webconfig中设置 默认为/
            );

            string encTicket = FormsAuthentication.Encrypt(ticket);

            if ((encTicket == null) || (encTicket.Length < 1))
            {
                throw new HttpException("Unable_to_encrypt_cookie_ticket");
            }

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Path = "/";
            cookie.HttpOnly = true;  //是否可通过脚本访问 设置为true 则不可通过脚本访问
            cookie.Domain = FormsAuthentication.CookieDomain;  //webconfig中设置的domain
                                                               //cookie.Secure = FormsAuthentication.RequireSSL;  //当此属性为 true 时，该 Cookie 只能通过 https:// 请求来发送

            //   if (ticket.IsPersistent)     //票证是否持久存储
            //  {
            cookie.Expires = ticket.Expiration;
            //  }
          
            HttpContext.Current.Response.AppendCookie(cookie);
            return cookie;


        }
        /// <summary>
        /// //获取asp.net中的form验证加密数据
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static FormsAuthenticationTicket GetAuthCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies[strName]!=null)
            {
                 FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[strName].Value);
            return ticket;
            }
            return null;
        }
    }
}
