using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC_Web.Common
{
    public class Response
    {
        public bool Status = true;
        public string Message = "操作成功";
        public dynamic Result;
    }
}
