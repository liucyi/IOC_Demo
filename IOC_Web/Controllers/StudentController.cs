using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOC_Web.Models;

namespace IOC_Web.Controllers
{
    public class StudentController : Controller
    {
         // IStudentRepository repository;
          IStudentService studentService;
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

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        public ActionResult Index()
        {
          //  var data = repository.GetAll();
            var data = studentService.GetName(1);
            return View(data);
        }
    }
}