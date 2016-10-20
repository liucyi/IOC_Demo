using System;

namespace IOC_Web.Common
{
    /// <summary>
    /// 操作类异常
    /// </summary>
    public class OperationException : Exception
    {
        public OperationException()
        {
        }

        public OperationException(Exception e)
            : base(e.Message, e)
        {
        }

        public OperationException(string message)
            : base(message)
        {
        }

        public OperationException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class ApiCheckException : Exception
    {
        public ApiCheckException()
        {
        }

        public ApiCheckException(Exception e)
            : base(e.Message, e)
        {
        }

        public ApiCheckException(string message)
            : base(message)
        {
        }

        public ApiCheckException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
