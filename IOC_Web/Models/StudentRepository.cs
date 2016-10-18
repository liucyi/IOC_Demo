using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IOC_Web.Entity;

namespace IOC_Web.Models
{
    public class StudentRepository : BaseRepository<Student>,IStudentRepository
    {
        
    }
}