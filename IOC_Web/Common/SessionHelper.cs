 
using System;
using System.Web;

namespace IOC_Web.Common
{
    /// <summary>  
    /// Session 操作类  
    /// 1、GetSession(string name)根据session名获取session对象  
    /// 2、SetSession(string name, object val)设置session  
    /// </summary>  
    public class SessionHelper
    {
        private static readonly string SessionUser = "SESSION_USER";
        public static void AddSessionUser<T>(T user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[SessionUser] = user;
        }
        public static T GetSessionUser<T>()
        {
            try 
            { 
                HttpContext rq = HttpContext.Current;
                return (T)rq.Session[SessionUser];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Clear()
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[SessionUser] = null;
        }
        /// <summary>  
        /// 根据session名获取session对象  
        /// </summary>  
        /// <param name="name"></param>  
        /// <returns></returns>  
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }
        /// <summary>  
        /// 设置session  
        /// </summary>  
        /// <param name="name">session 名</param>  
        /// <param name="val">session 值</param>  
        public static void SetSession(string name, object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>  
        /// 清空所有的Session  
        /// </summary>  
        /// <returns></returns>  
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>  
        /// 删除一个指定的ession  
        /// </summary>  
        /// <param name="name">Session名称</param>  
        /// <returns></returns>  
        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>  
        /// 删除所有的ession  
        /// </summary>  
        /// <returns></returns>  
        public static void RemoveAllSession(string name)
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}
