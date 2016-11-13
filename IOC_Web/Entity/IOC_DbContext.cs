using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using IOC_Web.Models;

namespace IOC_Web.Entity
{
    public class IOC_DbContext : DbContext
    {
        static IOC_DbContext()
        {
            Database.SetInitializer<IOC_DbContext>(
        //  new IOC_DbContextInitializer()
        null
              ); //初始化时删除数据库
        }

        public IOC_DbContext() : base("name=ioc_db")
        {

        }

        public DbSet<Student> Students { get; set; } 
        public DbSet<Course> Courses { get; set; }
       public DbSet<Student_Course> Student_Coursses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //防止黑幕交易 要不然每次都要访问 EdmMetadata这个表
            //  modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//去除复数形式的表名
        }
        public class IOC_DbContextInitializer : DropCreateDatabaseAlways<IOC_DbContext>
        {
            protected override void Seed(IOC_DbContext context)
            {
                var list = new List<Student>
                {
                    new Student {Id = 1, Name = "张三", Major = "软件工程",  Graduation = "2013年", School = "西安工业大学"},
                      new Student {Id = 11, Name = "FDS", Major = "软件工程",  Graduation = "2013年", School = "西安大学"},
                        new Student {Id = 12, Name = "df", Major = "软件工程",  Graduation = "2013年", School = "西安工业大学"},
                    new Student {Id = 2, Name = "李四", Major = "计算机科学与技术", Graduation = "2013年", School = "重庆大学"},
                    new Student {Id = 3, Name = "王五", Major = "自动化", Graduation = "2013年", School = "北京大学"}
                };

                list.ForEach(e => context.Students.Add(e));
                context.SaveChanges();



                var list1 = new List<Course>()
                {
                    new Course() { Name = "数据库原理"},
                       new Course() { Name = "计算机原理"},
                          new Course() { Name = "数据库结构"},
                             new Course() { Name = "数据库优化学习"},
                                new Course() { Name = "C++入门"},
                                  new Course() { Name = "Asp.net 2.0"},
                                    new Course() { Name = "VB入门"},
                                   new Course() { Name = "电气自动化"}
                };
                list1.ForEach(e => context.Courses.Add(e));
                context.SaveChanges();
                var list2 = new List<Student_Course>()
                {
                    new Student_Course() { StudentId  = 1,CourseId = 1,CourseName = "数据库原理"},
                       new Student_Course(){ StudentId  = 1,CourseId = 2,CourseName = "计算机原理" },
                          new Student_Course() { StudentId  = 1,CourseId = 4,CourseName = "数据库优化学习"},
                             new Student_Course() { StudentId  = 2,CourseId = 2,CourseName = "计算机原理"},
                                new Student_Course() { StudentId  = 2,CourseId = 1,CourseName = "数据库原理"},
                                   new Student_Course() { StudentId  = 3,CourseId = 1,CourseName = "数据库原理"},
                             new Student_Course() { StudentId  = 3,CourseId = 8,CourseName ="电气自动化" },
                };
                list2.ForEach(e => context.Student_Coursses.Add(e));
                context.SaveChanges();
            }

        }
    }
}