using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IOC_Web.Common
{
    /// <summary>
    /// 读取配置文件信息
    /// </summary>
    public class ConfigurationHelpers
    {
        public static string GetConnectionStrings(string key)
        {
            var connStr = ConfigurationManager.ConnectionStrings[key].ToString();
            return "data source=192.168.16.137;initial catalog=WMSDB;persist security info=True;user id=sa;password=Sa123123;multipleactiveresultsets=True;application name=EntityFramework;"
;//DESEncrypt.Decrypt(connStr);
        }

        /// <summary>
        /// 得到Web.Config中的键值对
        /// </summary>
        /// <returns>值</returns>
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>  
        /// 得到AppSettings中的配置字符串信息  
        /// </summary>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = CacheHelper.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        CacheHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }

        /// <summary>  
        /// 得到AppSettings中的配置Bool信息  
        /// </summary>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.  
                }
            }
            return result;
        }
        /// <summary>  
        /// 得到AppSettings中的配置Decimal信息  
        /// </summary>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.  
                }
            }

            return result;
        }
        /// <summary>  
        /// 得到AppSettings中的配置int信息  
        /// </summary>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.  
                }
            }

            return result;
        }
    }
}
