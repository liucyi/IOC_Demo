using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC_Web.Models.ViewModel
{
    public class ViewStudents
    {

        public int id { get; set; }
        public string Name { get; set; }
        public string Graduation { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string Text { get; set; }
        public int age { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public IEnumerable<Course> CourseInfo { get; set; }
    }


}