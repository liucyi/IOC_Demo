using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using IOC_Web.Models;
  
using IOC_Web.Common;
using IOC_Web.Repository;
using IOC_Web.Service;

namespace IOC_Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            #region MyRegion1单个注册

            //SetupResolveRules(builder);
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
            #region MyRegion2全部注册
            //// Register your MVC controllers. (MvcApplication is the name of
            //// the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(
               t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

             //builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(
             //  t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            builder.RegisterType<StudentService>();


            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            //automapper注册
            //   RegisterAutomapper.Excute();
            // builder.RegisterModule(new Autofac.Configuration.Core.ConfigurationRegistrar("autofac"));



            LogHelper.Log("启动Web");
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            LogHelper.Fatal("\r\n客户机IP：" + Request.UserHostAddress + "\r\n 错误地址：" + Request.Url+ Server.GetLastError());
        }
        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
          
        }
    }
}
