using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IOC_Web.Entity;
using IOC_Web.Models;
using IOC_Web.Models.ViewModel;

namespace IOC_Web.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        
    }
}