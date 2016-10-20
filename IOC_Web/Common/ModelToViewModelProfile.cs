using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using IOC_Web.Models;

namespace IOC_Web.Common
{
    public class ModelToViewModelProfile:Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Student, ViewStudents>()
                .ForMember(des => des.Text, opt => opt.MapFrom(src => src.School + src.Name))
                 .ForMember(des => des.id, opt => opt.MapFrom(src => src.Id));
        }
    }
}