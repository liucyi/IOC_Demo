using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC_Web.Models
{
    public class StudentService : IStudentService
    {
        IStudentRepository iStudentRepository;

        public StudentService(IStudentRepository iStudentRepository)
        {
            this.iStudentRepository = iStudentRepository;
        }

        public string GetName(int ID)
        {
            return iStudentRepository.Get(ID).Name;
        }
    }
}