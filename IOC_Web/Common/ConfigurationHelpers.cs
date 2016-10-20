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
    }
}
