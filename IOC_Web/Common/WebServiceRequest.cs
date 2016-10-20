using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace IOC_Web.Common
{
    /// <summary>
    /// http请求类
    /// </summary>
    public static class WebServiceRequest
    {
        /// <summary>
        /// http get
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpWebRequestGet(string url, int timeOut = 20)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = timeOut * 1000;
            httpWebRequest.Proxy = null;

            //HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpWebResponse = (HttpWebResponse)ex.Response;
            }
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();

            return responseContent;
        }

        /// <summary>
        /// http get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string HttpWebRequestGet(string url, HttpContext httpContext, int timeOut = 20)
        {
            string queryString = "?";
            foreach (string key in httpContext.Request.QueryString.AllKeys)
            {
                queryString += key + "=" + httpContext.Request.QueryString[key] + "&";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + queryString);

            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = timeOut * 1000;
            httpWebRequest.Proxy = null;

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();

            return responseContent;
        }

        /// <summary>
        /// http post
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="body">body是要传递的参数,格式id=1&name=张三</param>
        /// <param name="contentType">application/x-www-form-urlencoded soap填写text/xml; charset=utf-8</param>
        /// <returns></returns>
        public static string HttpWebRequestPost(string url, string body, string contentType = "application/x-www-form-urlencoded", int timeOut = 20)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = timeOut * 1000;
            httpWebRequest.Proxy = null;

            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }
    }
}
