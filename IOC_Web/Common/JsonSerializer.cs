using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC_Web.Common
{
    /// <summary>
    /// json转换类
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// 转化为json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反json为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeSerializeJson<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 转化为json单个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToFirstJosn(this object obj)
        {
            return Newtonsoft.Json.Linq.JArray.FromObject(obj)[0].ToString();
        }
    }
}