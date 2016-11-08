using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IOC_Web.Common;
using IOC_Web.Models;
using IOC_Web.Models.ViewModel;
using IOC_Web.Repository;

namespace IOC_Web.Service
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
            return iStudentRepository.GetEntity(c=>c.Id==id);
        }

        public IEnumerable<Student> GetAll(int id)
        {
            return iStudentRepository.GetList(c=>c.Id>0);
         
        }
        public List<ViewStudents> GetViewStudentsAll(int id)
        {
            string sql = "select*from  Student s left join Student_Course sc on s.id=sc.studentid";
               return iStudentRepository.SqlQuery(sql).MapToList<ViewStudents>();
        


        }
        public string GetName(int ID)
        {
            return iStudentRepository.GetEntity(c => c.Id == ID).Name;
        }


    }
}