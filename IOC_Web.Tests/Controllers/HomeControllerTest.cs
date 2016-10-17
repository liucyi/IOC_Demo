using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IOC_Web;
using IOC_Web.Controllers;

namespace IOC_Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DBManager>();
            builder.RegisterType<SqlDAL>().As<IDAL>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DBManager>();
             var s=   manager.Add("INSERT INTO Persons VALUES ('Man', '25', 'WangW', 'Shanghai')");
            }
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

    public class SqlDAL : IDAL
    {
        public string Insert(string text)
        {
           return ("使用sqlDAL添加相关信息");
        }
    }
    public class OracleDAL : IDAL
    {
        public string Insert(string commandText)
        {
            return ("使用OracleDAL添加相关信息");
        }
    }
    public  interface IDAL
    {
        string Insert(string commandText);
    }
    public class DBManager
    {

        IDAL _dal;
        public DBManager(IDAL dal)
        {
            _dal = dal;
        }
        public string Add(string commandText)
        {
            return _dal.Insert(commandText);
        }
    }


}
