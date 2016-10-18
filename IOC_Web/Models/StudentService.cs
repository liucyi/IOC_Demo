using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace IOC_Web.Models
{
    public class StudentService
    {
        IStudentRepository iStudentRepository;

        public StudentService(IStudentRepository iStudentRepository)
        {
            this.iStudentRepository = iStudentRepository;
        }



        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAll(int id)
        {
            return iStudentRepository.GetList(c=>c.Id>id);
         
        }
       
        public string GetName(int ID)
        {
            return iStudentRepository.GetEntity(c => c.Id == ID).Name;
        }


    }
}