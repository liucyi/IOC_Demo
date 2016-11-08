using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using IOC_Web.Models;
using IOC_Web.Models.ViewModel;

namespace IOC_Web.Common
{
    public class ViewModelToModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ViewStudents, Student>();
        }
    }
}