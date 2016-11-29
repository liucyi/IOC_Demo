using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IOC_Web.Common;
using IOC_Web.Entity;
using IOC_Web.Models;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.IO.Path.GetFullPath("1.xlsx");
            var table = NPOIHelper.ExcelToDataTable(path);
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            int num = 0;

            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < table.Rows.Count / 4; i++)
                {
                    Student student = new Student();
                    student.Id = i;
                    student.Graduation = table.Rows[i]["Graduation"].ToString();
                    student.Major = table.Rows[i]["Major"].ToString();
                    student.Name = table.Rows[i]["Name"].ToString();
                    student.School = table.Rows[i]["School"].ToString();

                    using (IOC_DbContext db = new IOC_DbContext())
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        Console.WriteLine("线程1："+i + "添加成功--"+  num++);
                        
                    }


                }
            });
            var task1 = Task.Factory.StartNew(() =>
            {
                for (int i = table.Rows.Count / 4; i < table.Rows.Count / 4 * 2; i++)
                {
                    Student student = new Student();
                    student.Id = i;
                    student.Graduation = table.Rows[i]["Graduation"].ToString();
                    student.Major = table.Rows[i]["Major"].ToString();
                    student.Name = table.Rows[i]["Name"].ToString();
                    student.School = table.Rows[i]["School"].ToString();

                    using (IOC_DbContext db = new IOC_DbContext())
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        Console.WriteLine("线程2：" + i + "添加成功--" + num++);
                    }

                }
            });
            var task2 = Task.Factory.StartNew(() =>
            {
                for (int i = table.Rows.Count / 4 * 2; i < table.Rows.Count / 4 * 3; i++)
                {
                    Student student = new Student();
                    student.Id = i;
                    student.Graduation = table.Rows[i]["Graduation"].ToString();
                    student.Major = table.Rows[i]["Major"].ToString();
                    student.Name = table.Rows[i]["Name"].ToString();
                    student.School = table.Rows[i]["School"].ToString();

                    using (IOC_DbContext db = new IOC_DbContext())
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        Console.WriteLine("线程3：" + i + "添加成功--" + num++);
                    }

                }
            });
            var task3 = Task.Factory.StartNew(() =>
            {
                for (int i = table.Rows.Count / 4 * 3; i < table.Rows.Count; i++)
                {
                    Student student = new Student();
                    student.Id = i;
                    student.Graduation = table.Rows[i]["Graduation"].ToString();
                    student.Major = table.Rows[i]["Major"].ToString();
                    student.Name = table.Rows[i]["Name"].ToString();
                    student.School = table.Rows[i]["School"].ToString();

                    using (IOC_DbContext db = new IOC_DbContext())
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        Console.WriteLine("线程4：" + i + "添加成功--" + num++);
                    }

                }
            });
           
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine(time+"ms");
            Console.ReadLine();
        }
    }
}
