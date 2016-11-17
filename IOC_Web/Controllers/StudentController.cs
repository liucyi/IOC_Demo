using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOC_Web.Common;
using IOC_Web.Models;
using IOC_Web.Models.ViewModel;
using IOC_Web.Service;

namespace IOC_Web.Controllers
{
    public class StudentController : Controller
    {
        // IStudentRepository repository;

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
        StudentService studentService;
        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }
        public ActionResult Index()
        {
            AutoMapperProfileRegister.Register();
            //   var data = studentService.Get(1).MapTo<ViewStudents>()  ;
            //  var data = studentService.GetAll(1).MapToList<ViewStudents>() ;
            var data = studentService.GetViewStudentsAll(1);
            var data1 = studentService.GetAll(1);
            var json = data.ToJson();
            var d = DESEncrypt.Encrypt(json);
            var n = DESEncrypt.Decrypt(d);
            return View(json);
        }
    }
}