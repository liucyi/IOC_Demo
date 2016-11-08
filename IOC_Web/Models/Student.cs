using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC_Web.Models
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

       /// <summary>
       /// 毕业年份
       /// </summary>
        public string Graduation { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 主修
        /// </summary>
        public string Major { get; set; }


 
    }
     
}