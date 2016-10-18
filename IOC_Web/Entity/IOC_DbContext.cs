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
                    new Student {Id = 1, Name = "张三", Major = "软件工程", Graduation = "2013年", School = "西安工业大学"},
                    new Student {Id = 2, Name = "李四", Major = "计算机科学与技术", Graduation = "2013年", School = "西安工业大学"},
                    new Student {Id = 3, Name = "王五", Major = "自动化", Graduation = "2013年", School = "西安工业大学"}
                };

                list.ForEach(e => context.Students.Add(e));
                context.SaveChanges();
            }

        }
    }
}