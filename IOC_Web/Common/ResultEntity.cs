
namespace IOC_Web.Common
{
    public class ResultEntity
    {
        public ResultEntity()
        {
            this.Code = ResultCode.Success;
            this.Message = "";
        }

        /// <summary>
        /// 返回类型代码
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }

    public class TResultEntity<T>
    {
        public TResultEntity()
        {
            this.Code = ResultCode.Success;
            this.Data = default(T);
            this.Message = "";
        }

        /// <summary>
        /// 返回类型代码
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// 实际返回的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 接口专用返回实体
    /// </summary>
    public class ApiResultEntity
    {
        public ApiResultEntity()
        {
            this.Code = "";
            this.IsSuccess = true;
            this.Message = "";
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回类型代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 接口专用返回实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiTResultEntity<T>
    {
        public ApiTResultEntity()
        {
            this.Code = "";
            this.IsSuccess = true;
            this.Data = default(T);
            this.Message = "";
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回类型代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 实际返回的数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }


    public enum ResultCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 10000,
        /// <summary>
        /// 操作失败，不输出错误信息，操作失败、未知异常
        /// </summary>
        Error = 10001,
        /// <summary>
        /// 输出消息，要提示用户的消息
        /// </summary>
        PointOut = 10002,
        /// <summary>
        /// 无权限
        /// </summary>
        NoAuthority = 10003,
        /// <summary>
        /// 登录超时
        /// </summary>
        LoginTimeout = 10004,
        /// <summary>
        /// 用户被踢下线
        /// </summary>
        DownLine = 10005
    }
}
