using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
 
namespace IOC_Web.Common
{
    public class AutoMapperProfileRegister
    {
        public static void Register()
        {
            Mapper.Configuration.AddProfile(new ModelToViewModelProfile());
            Mapper.Configuration.AddProfile(new ViewModelToModelProfile());
        }
    }
}