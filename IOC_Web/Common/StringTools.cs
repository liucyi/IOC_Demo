using System;
using System.Security.Cryptography;
using System.Text;

namespace IOC_Web.Common
{
    public class StringTools
    {
        private static object obj = new object();

        #region 加密类
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetMD5(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return "";
            }
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(val, "MD5");
        }

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSha1(string str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[] 
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }
        #endregion

        #region 订单类
        public static string GetOrderNumber()
        {
            string number = "", value = "";
            Random random = new Random(Guid.NewGuid().GetHashCode());
            lock (obj)
            {
                number = DateTime.Now.ToString("yyMMddHHmmss");
                value = random.Next(1000, 9999).ToString();
            }
            if (value.Length != 4)
            {
                value.PadRight(4, '0');
            }
            return number + value;
        }
        #endregion

        #region 货币类
        /// <summary>
        /// 货币格式化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currencyType"></param>
        /// <returns></returns>
        //public static string CurrencyFormat(decimal value, bool isShowMark = true, CurrencyType currencyType = CurrencyType.CNY)
        //{
        //    string str = "";
        //    switch (currencyType)
        //    {
        //        case CurrencyType.CNY:
        //            value = Math.Round(value, 2, MidpointRounding.ToEven);
        //            if (isShowMark)
        //            {
        //                str = string.Format("￥{0}", value.ToString("0.00"));
        //            }
        //            else
        //            {
        //                str = string.Format("{0}", value.ToString("0.00"));
        //            }
        //            break;
        //        case CurrencyType.USD:
        //            if (isShowMark)
        //            {
        //                str = string.Format("${0}", value.ToString("0.00"));
        //            }
        //            else
        //            {
        //                str = string.Format("{0}", value.ToString("0.00"));
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //    return str;
        //}
        #endregion

        #region 编码
        /// <summary>
        /// 唯一编码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string UniqueCode(int length = 6)
        {
            string randomResult = string.Empty;
            string readyStr = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZ";
            char[] rtn = new char[length];
            Guid gid = Guid.NewGuid();
            var ba = gid.ToByteArray();
            for (var i = 0; i < length; i++)
            {
                rtn[i] = readyStr[((ba[i] + ba[length + i]) % 33)];
            }
            foreach (char r in rtn)
            {
                randomResult += r;
            }
            return randomResult;
        }

        /// <summary>
        /// 产品唯一码
        /// </summary>
        /// <returns></returns>
        public static string BuildProductUniqueCode()
        {
            var num = 6;
            string randomResult = string.Empty;
            string readyStr = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
            char[] rtn = new char[num];
            Guid gid = Guid.NewGuid();
            var ba = gid.ToByteArray();
            for (var i = 0; i < num; i++)
            {
                rtn[i] = readyStr[((ba[i] + ba[num + i]) % 31)];
            }
            foreach (char r in rtn)
            {
                randomResult += r;
            }
            return randomResult;
        }

        /// <summary>
        /// 生成客户唯一码
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string BuildCustomerUniqueCode()
        {
            var num = 6;
            string randomResult = string.Empty;
            string readyStr = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ";
            char[] rtn = new char[num];
            Guid gid = Guid.NewGuid();
            var ba = gid.ToByteArray();
            for (var i = 0; i < num; i++)
            {
                rtn[i] = readyStr[((ba[i] + ba[num + i]) % 31)];
            }
            foreach (char r in rtn)
            {
                randomResult += r;
            }
            return randomResult;
        }

        /// <summary>
        /// GUID唯一码
        /// </summary>
        /// <returns></returns>
        public static string BuildGuidUniqueCode()
        {
            return Guid.NewGuid().ToString("N").ToUpper();
        }

        #endregion

        /// <summary>
        /// 生成随机验证码
        /// </summary>
        /// <returns></returns>
        public static string GetSMSCode(int len = 6, bool isNumber = false)
        {
            char[] character = null;
            if (isNumber)
            {
                character = new char[] { '2', '3', '4', '5', '6', '7', '8', '9' };
            }
            else
            {
                character = new char[] { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            }
            Random rnd = new Random();
            string code = "";
            for (int i = 0; i < len; i++)
            {
                code += character[rnd.Next(character.Length)];
            }
            return code;
        }

        /// <summary>
        /// 距离格式化(米/千米单位)
        /// </summary>
        /// <param name="value">米</param>
        /// <returns></returns>
        public static string DistanceFormat(double value)
        {
            if (value >= 1000)
            {
                value = value / 1000;
                value = Math.Round(value, 2, MidpointRounding.ToEven);
                return string.Format("{0}千米", value);
            }
            value = Math.Round(value, 2, MidpointRounding.ToEven);
            return string.Format("{0}米", value);
        }

        /// <summary>
        /// 销量格式化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SalesVolumeFormat(long value)
        {
            if (value >= 10000)
            {
                return string.Format("{0}万笔", value / 10000);
            }
            return string.Format("{0}笔", value);
        }

        /// <summary>
        /// 评论格式化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CommentFormat(decimal value)
        {
            if (value >= 10000)
            {
                return string.Format("{0}万", value / 10000);
            }
            return string.Format("{0}", value);
        }

        /// <summary>
        /// 星级格式化
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string LevelFormat(decimal level)
        {
            return string.Format("{0}星", level);
        }

        #region 结算AB两点距离
        /// <summary>
        /// 地球半径米
        /// </summary>
        private const double EARTH_RADIUS = 6378137.0;

        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        /// <summary>
        /// 地图上两点之间的直线距离单位M
        /// </summary>
        /// <param name="lat1">A点经度</param>
        /// <param name="lng1">A点纬度</param>
        /// <param name="lat2">B点经度</param>
        /// <param name="lng2">B点纬度</param>
        /// <returns>两点直线距离</returns>
        /**
         * 1、公式中经纬度均用弧度表示；
         * 2、Lat1 Lng1 分别表示A点经、纬度，Lat2 Lng2 分别表示B点经纬度；
         * 3、a=Lng1 -Lng2 为两点纬度之差 b=Lat1 – Lat2 为两点经度之差；
         * 4、6378.137为地球半径（公里）；
         * 返回单位米
         * */
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLat2 = Rad(lat2);
            double a = radLat1 - radLat2;
            double b = Rad(lng1) - Rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
            Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }
        #endregion
    }
}
