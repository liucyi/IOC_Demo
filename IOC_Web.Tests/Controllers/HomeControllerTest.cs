﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IOC_Web;
using IOC_Web.Common;
using IOC_Web.Controllers;
using IOC_Web.Models;

namespace IOC_Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {  
        [TestMethod]
        public void Index()
        {
          StudentService studentService=new StudentService( new StudentRepository());
            AutoMapperProfileRegister.Register();
            //   var data = studentService.Get(1).MapTo<ViewStudents>()  ;
            //  var data = studentService.GetAll(1).MapToList<ViewStudents>() ;
            var data = studentService.GetAll(1).MapToList<ViewStudents>().ToJson();
            var d = DESEncrypt.Encrypt(data);
            var n = DESEncrypt.Decrypt(d);
        }

        private void reg()
        {
            var builder = new ContainerBuilder();

            SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();

        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
        public enum AreaMode
        {
            NONE,
            CITY,
            TOWN,
            ROAD,
            CITYTOWN,
            TOWNROAD,
            CITYROAD,
            ALL
        }

        [TestClass()]
        public class EnumHelperTests
        {
            [TestMethod()]
            public void CheckedContainEnumNameTest()
            {
                Assert.IsTrue(EnumsHelper.CheckedContainEnumName<AreaMode>("CITY"));
            }

            [TestMethod()]
            public void GetDescriptionTest()
            {
            // Assert.AreEqual("NONE", AreaMode.NONE.GetDescription());
            }

            [TestMethod()]
            public void ParseEnumDescriptionTest()
            {
                Assert.AreEqual(AreaMode.NONE, EnumsHelper.ParseEnumDescription<AreaMode>("NONE", AreaMode.CITYTOWN));
            }

            [TestMethod()]
            public void ParseEnumNameTest()
            {
                Assert.AreEqual(AreaMode.ALL, EnumsHelper.ParseEnumName<AreaMode>("ALL"));
            }
        }
        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }

    

}
