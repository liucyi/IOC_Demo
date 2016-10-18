using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOC_Web.Common;
using IOC_Web.Models;

namespace IOC_Web.Controllers
{
    public class StudentController : Controller
    {
         // IStudentRepository repository;
          StudentService studentService;
        //
        #region 构造器注入
        //public StudentController(IStudentRepository repository)
        //{
        //    this.repository = repository;
        //} 
        #endregion

        //
        #region 属性注入
        //public StudentController()
        //{

        //}
        //public StudentController(IStudentRepository repository)
        //{
        //    this.repository = repository;
        //}

        //public IStudentRepository StudentRepository
        //{
        //    get { return this.repository; }
        //    set { this.repository = value; }
        //} 
        #endregion

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }
        public ActionResult Index()
        {
         // var data1 = studentService.GetAll();
            var data = studentService.GetAll(1)  ;
            return  View(data);
        }
    }
}