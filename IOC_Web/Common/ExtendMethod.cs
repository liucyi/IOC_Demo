using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IOC_Web.Common
{
    public static class ExtendMethod
    {
        #region 转换整数、小数

        /// <summary>
        /// 将双精度的数据转化为小数点后几位的小数
        /// </summary>
        /// <remarks>dotCount小于等于0时，不做任何处理</remarks>
        /// <param name="num">双精度的数</param>
        /// <param name="dotCount">小数点后的位数</param>
        /// <returns>固定小数长度的双精数据</returns>
        public static double ToFixedDouble(this double num, int dotCount)
        {
            if (dotCount <= 0)
            {
                dotCount = 0;
            }

            return Math.Round(num, dotCount, MidpointRounding.ToEven);//采用银行家舍人方法
        }

        public static decimal ToFixedDecimal(this decimal num, int dotCount)
        {
            if (dotCount <= 0)
            {
                dotCount = 0;
            }

            return Math.Round(num, dotCount, MidpointRounding.ToEven);//采用银行家舍人方法
        }

        public static double ToFixedDouble(this double? num, int dotCount)
        {
            if (dotCount <= 0)
            {
                dotCount = 0;
            }

            if (num.HasValue)
            {
                return Math.Round(num.Value, dotCount, MidpointRounding.ToEven);//采用银行家舍人方法
            }

            return 0;
        }

        public static decimal ToFixedDecimal(this decimal? num, int dotCount)
        {
            if (dotCount <= 0)
            {
                dotCount = 0;
            }

            if (num.HasValue)
            {
                return Math.Round(num.Value, dotCount, MidpointRounding.ToEven);//采用银行家舍人方法
            }

            return 0;
        }

        public static decimal ToDecimal(this object obj)
        {
            if (obj == null)
            {
                return decimal.MinValue;
            }
            decimal result;
            if (decimal.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return 0;
        }

        public static double ToDouble(this object obj)
        {
            if (obj == null)
            {
                return double.MinValue;
            }
            double result;
            if (double.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return 0;
        }

        public static int ToInt32(this string strValue)
        {
            if (string.IsNullOrWhiteSpace(strValue))
            {
                return int.MinValue;
            }
            strValue = strValue.Trim();
            int result = int.MinValue;

            if (int.TryParse(strValue, out result))
            {
                return result;
            }
            return 0;
        }

        #endregion

        #region 字符串
        /// <summary>
        /// 截取字符串最后几个
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToEndSubstring(this string strValue, int length)
        {
            if (length == 0)
            {
                return "";
            }
            var start = strValue.Length - length;
            if (start < 0)
            {
                return "";
            }
            return strValue.Substring(start, length);
        }
        #endregion

        #region 系统中的单位转换
        /// <summary>
        /// 体积立方厘米转换到立方米
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToVolumeM(this decimal num)
        {
            if (num <= 0)
            {
                return 0;
            }
            return num / 1000000;
        }
        /// <summary>
        /// 体积立方厘米转换到立方米
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToVolumeM(this decimal? num)
        {
            if (!num.HasValue || num <= 0)
            {
                return 0;
            }
            return num.Value / 1000000;
        }
        /// <summary>
        /// 重量g转换到kg
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToWeightKG(this decimal num)
        {
            if (num <= 0)
            {
                return 0;
            }
            return num / 1000;
        }
        /// <summary>
        /// 重量g转换到kg
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToWeightKG(this decimal? num)
        {
            if (!num.HasValue || num <= 0)
            {
                return 0;
            }
            return num.Value / 1000;
        }
        /// <summary>
        /// 体积立方米转换到立方厘米
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToVolumeCM(this decimal num)
        {
            if (num <= 0)
            {
                return 0;
            }
            return num * 1000000;
        }
        /// <summary>
        /// 体积立方米转换到立方厘米
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToVolumeCM(this decimal? num)
        {
            if (!num.HasValue || num <= 0)
            {
                return 0;
            }
            return num.Value * 1000000;
        }
        /// <summary>
        /// 重量kg转换到g
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToWeightG(this decimal num)
        {
            if (num <= 0)
            {
                return 0;
            }
            return num * 1000;
        }
        /// <summary>
        /// 重量kg转换到g
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToWeightG(this decimal? num)
        {
            if (!num.HasValue || num <= 0)
            {
                return 0;
            }
            return num.Value * 1000;
        }
        #endregion

        #region 日期时间
        public static string Format = "yyyy-MM-dd";//默认格式
        public static string LongFormat = "yyyy-MM-dd HH:mm:ss";//默认格式

        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime date)
        {
            try
            {
                return date.ToString(Format);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime? date)
        {
            try
            {
                if (date.HasValue)
                {
                    var _d = Convert.ToDateTime(date);
                    return _d.ToString(Format);
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 日期+时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToLongDate(this DateTime date)
        {
            try
            {
                return date.ToString(LongFormat);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 日期+时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToLongDate(this DateTime? date)
        {
            try
            {
                if (date.HasValue)
                {
                    var _d = Convert.ToDateTime(date);
                    return _d.ToString(LongFormat);
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// UNIX时间戳转日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 日期转UNIX时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToUnixTime(this DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        #endregion

        #region DataTable转换
        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            var list = new List<T>();
            Type type = typeof(T);
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[pi.Name];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// DataTable转Model 只取第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return default(T);
            }
            var model = new T();
            Type type = typeof(T);
            DataRow dr = dt.Rows[0];
            PropertyInfo[] propertys = model.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (dt.Columns.Contains(pi.Name))
                {
                    if (!pi.CanWrite) continue;
                    object value = dr[pi.Name];
                    if (value != DBNull.Value)
                        pi.SetValue(model, value, null);
                }
            }
            return model;
        }

        /// <summary>
        /// 集合转换成DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this List<T> list) where T : new()
        {
            if (list == null || list.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }

            Type type = list[0].GetType();
            PropertyInfo[] propertys = type.GetProperties();

            var dt = CreateTable(list[0]);
            //将所有entity添加到DataTable中
            foreach (object entity in list)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != type)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[propertys.Length];
                for (int i = 0; i < propertys.Length; i++)
                {
                    entityValues[i] = propertys[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        /// 实体转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable ModelToDataTable<T>(this T entity) where T : new()
        {
            if (entity == null)
            {
                throw new Exception("需转换的对象为空");
            }

            Type type = entity.GetType();
            PropertyInfo[] propertys = type.GetProperties();

            var dt = CreateTable(entity);

            //检查所有的的实体都为同一类型
            if (entity.GetType() != type)
            {
                throw new Exception("要转换的对象类型不一致");
            }
            object[] entityValues = new object[propertys.Length];
            for (int i = 0; i < propertys.Length; i++)
            {
                entityValues[i] = propertys[i].GetValue(entity, null);
            }
            dt.Rows.Add(entityValues);
            return dt;
        }

        private static DataTable CreateTable<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] propertys = type.GetProperties();

            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < propertys.Length; i++)
            {
                //dt.Columns.Add(propertys[i].Name, propertys[i].PropertyType);
                dt.Columns.Add(propertys[i].Name);
            }
            return dt;
        }
        #endregion
    }
}
