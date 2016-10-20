//只能输入数字： "^[0-9]*$" 。

//  只能输入n位的数字："^\d{n}$"。

//  只能输入至少n位的数字："^\d{n,}$"。

//  只能输入m~n位的数字："^\d{m,n}$"。

//  只能输入零和非零开头的数字："^(0|[1-9][0-9]*)$"。

//  只能输入有两位小数的正实数："^[0-9]+(.[0-9]{2})?$"。

//  只能输入有1~3位小数的正实数："^[0-9]+(.[0-9]{1,3})?$"。

//  只能输入非零的正整数："^\+?[1-9][0-9]*$"。

//  只能输入非零的负整数："^\-[1-9][]0-9"*$。

//  只能输入长度为3的字符："^.{3}$"。

//  只能输入由26个英文字母组成的字符串："^[A-Za-z]+$"。

//  只能输入由26个大写英文字母组成的字符串："^[A-Z]+$"。

//  只能输入由26个小写英文字母组成的字符串："^[a-z]+$"。

//  只能输入由数字和26个英文字母组成的字符串："^[A-Za-z0-9]+$"。

//  只能输入由数字、26个英文字母或者下划线组成的字符串："^\w+$"。

//  验证用户密码："^[a-zA-Z]\w{5,17}$"正确格式为：以字母开头，长度在6~18之间，只能包含字符、数字和下划线。

//  验证是否含有^%&’,;=?$\"等字符："[^%&’,;=?$\x22]+"。

//  只能输入汉字："^[\u4e00-\u9fa5]{0,}$"

//  验证Email地址："^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"。

//  验证InternetURL："^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"。

//提取图片地址：/(http(s)?\:\/\/)?(www\.)?(\w+\:\d+)?(\/\w+)+\.(png|gif|jpg|bmp|jpeg)/gi。

//  验证电话号码："^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$"正确格式为："XXX-XXXXXXX"、"XXXX-XXXXXXXX"、"XXX-XXXXXXX"、"XXX-XXXXXXXX"、"XXXXXXX"和"XXXXXXXX"。

//  验证身份证号(15位或18位数字)："^\d{15}|\d{18}$"。

//  验证一年的12个月："^(0?[1-9]|1[0-2])$"正确格式为："01"～"09"和"1"～"12"。

//  验证一个月的31天："^((0?[1-9])|((1|2)[0-9])|30|31)$"正确格式为;"01"～"09"和"1"～"31"。
using System;

namespace IOC_Web.Common
{
    /// <summary>
    /// 正则表达式
    /// </summary>
    public static class RegularExpression
    {
        public const string Account = "^[a-zA-Z0-9-_]{2,50}$";//字母数字下划线
        public const string Password = "^[A-Za-z0-9]{6,20}$";//6-20位数字或英文组合
        //public const string Mobile = "^13[0-9]{9}|14[0-9]{9}|15[0-9]{9}|17[0-9]{9}|18[0-9]{9}$";//手机号码
        //public const string Number = "^[0-9]*$";//大于0的数字
        //public const string Money = "^[0-9]+(.[0-9]{2})?$";//货币格式
        //public const string Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        //public const string Tel = @"\d{3}-\d{8}|\d{4}-\d{7}";
        //public const string Fax = @"\d{3}-\d{8}|\d{4}-\d{7}";
        //public const string QQ = @"[1-9][0-9]{4,}";
        //public const string Url = @"[a-zA-z]+://[^\s]*";
        //public const string Zip = @"[1-9]\d{5}(?!\d)";
        //public const string CardNum = @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
        //public const string IP = @"\d+\.\d+\.\d+\.\d+";

        /// <summary>
        /// 邮箱正则
        /// </summary>
        public const string EmailPattern = "^[\\w-_.]*@[\\w-_.]*$";

        /// <summary>
        /// Url正则
        /// </summary>
        public const string UrlPattern = @"\s*(http[s]{0,1})://[a-zA-Z0-9\.\-]+\.([a-zA-Z]{2,4})(:\d+)?(/[a-zA-Z0-9\.\-~!@#$%^&*+?:_/=<>]*)?\s*";

        /// <summary>
        /// 身份证号码正则
        /// </summary>
        public const string IDCardPattern = @"\d{17}[\d|x]|\d{15}";

        /// <summary>
        /// 手机号码正则
        /// </summary>
        public const string PhonePattern = @"[1-9]{1}\d{10}";

        /// <summary>
        /// 非负数数字(最多两位小数)
        /// <para>金额、重量等</para>
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string AmountPattern = @"^\d+(\.{0,1}\d{1,2}){0,1}$";

        /// <summary>
        /// 大于1的整数
        /// <para>长，宽，高（cm）</para>
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string LWHPattern = @"^[0-9]\d*$";

        /// <summary>
        /// 非负数数字(最多三位小数)
        /// <para>重量等</para>
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string Weight = @"^\d+(\.{0,1}\d{1,3}){0,1}$";

        /// <summary>
        /// 非负数数字(最多六位小数)
        /// <para>体积m³</para>
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string Valume = @"^\d+(\.{0,1}\d{1,6}){0,1}$";

        /// <summary>
        /// 整数正则
        /// </summary>
        public const string IntergerPattern = @"\d*\.{0}$";

        /// <summary>
        /// 非零正整数
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string NotZeroIntergerPattern = @"^[1-9]+[0-9]*$";

        /// <summary>
        /// 正整数（含零）
        /// <para>如果需要支持负数请另外写正则</para>
        /// </summary>
        public const string ZeroIntergerPattern = @"^[0-9]*$";

        /// <summary>
        /// 汉字正则
        /// </summary>
        public const string ChinesePattern = @"^[\u4e00-\u9fa5]{0,}$";

        /// <summary>
        /// 月份正则(验证一年中的12个月)
        /// </summary>
        public const string MonthPattern = @"^(0?[1-9]|1[0-2])$";

        /// <summary>
        /// 天数正则(验证一个月的31天)
        /// </summary>
        public const string DayPattern = @"^((0?[1-9])|((1|2)[0-9])|30|31)$";

        /// <summary>
        /// 域名正则(格式:www.xxx.xxx)
        /// </summary>
        public const string DomainPattern = @"^www.[^.]+.\w+$";

        /// <summary>
        /// 英文与数字混合正则  (英文、数字不能单独存在)
        /// </summary>
        public const string EnglishNumberPattern = @"^(([a-zA-Z]+\d+)|(\d+[a-zA-Z]+))[A-Za-z\d]*$";
    }
}
