using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC_Web.Models
{
    public class Student_Course
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public int CourseId { get; set; }
      public string CourseName { get; set; }
    }
}