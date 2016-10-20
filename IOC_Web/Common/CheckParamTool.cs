using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IOC_Web.Common
{
    /// <summary>
    /// 参数校验
    /// </summary>
    public class CheckParamTool
    {
        /// <summary>
        /// 参数校验
        /// </summary>
        /// <returns></returns>
        public static ResultEntity ParameterValidation(List<ParamRequire> list)
        {
            var result = new ResultEntity();

            try
            {
                foreach (ParamRequire item in list)
                {
                    //不允许为空 但为空
                    if (!item.IsNull && string.IsNullOrWhiteSpace(item.Value))
                    {
                        result.Code = ResultCode.Error;
                        result.Message = string.Format("{0} 未填写", item.Key);
                        return result;
                    }

                    //允许为空 值为空 则直接通过
                    if (item.IsNull && string.IsNullOrWhiteSpace(item.Value))
                    {
                        continue;
                    }

                    //判断最大长度
                    if (item.MaxLength > 0 && item.Value.Length > item.MaxLength)
                    {
                        result.Code = ResultCode.Error;
                        result.Message = string.Format("{0}最多可填{1}个字符", item.Key, item.MaxLength);
                        return result;
                    }

                    //正则校验
                    if (!string.IsNullOrWhiteSpace(item.Regular))
                    {
                        Regex regex = new Regex(item.Regular);
                        if (!regex.IsMatch(item.Value))
                        {
                            result.Code = ResultCode.Error;
                            result.Message = string.Format("{0}格式错误", item.Key);
                            return result;
                        }
                    }

                    //数据类型
                    if (item.Type != null)
                    {
                        try
                        {
                            System.ComponentModel.TypeDescriptor.GetConverter(item.Type).ConvertFrom(item.Value);
                        }
                        catch (Exception)
                        {
                            result.Code = ResultCode.Error;
                            result.Message = string.Format("{0}格式和类型错误", item.Key);
                            return result;
                        }
                    }
                }

                result.Code = ResultCode.Success;
                result.Message = "";
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }

    public class ParamRequire
    {
        public ParamRequire(string key, string value, Type type)
        {
            this.Key = key;
            this.Value = value;
            this.Type = type;
            this.IsNull = false;
            this.MaxLength = 0;
            this.Regular = string.Empty;
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 是否可为空
        /// <para>默认不可为空</para>
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 自定义正则
        /// </summary>
        public string Regular { get; set; }

    }
}
