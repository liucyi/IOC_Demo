using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IOC_Web.Common
{
    /// <summary>  
    /// 把DataTable转换为List<Model>形式  
    /// </summary>  
    public static class ModelConvertHelper<T> where T : new() //where T : new()代表作为泛型的类型T，必须是具有公共的无参数构造函数  
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合  
            IList<T> ts = new List<T>();
            // 获得此模型的类型  
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性  
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    // 检查DataTable是否包含此列  
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite)
                            continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            //pi.SetValue(t, value, null);  
                            pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType, CultureInfo.CurrentCulture), null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}