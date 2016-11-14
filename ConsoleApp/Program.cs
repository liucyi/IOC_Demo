using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {  private static Stopwatch stopWatch = new Stopwatch();
        public static void Main(string[] args)
        {
            var task1 = new Task(( ) =>
            {  Console.WriteLine("Begin");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Finish"); }
            

                )
            ; Console.WriteLine("Before start:" + task1.Status);
            task1.Start();
            Console.WriteLine("After start:" + task1.Status);
            task1.Wait();
            Console.WriteLine("After Finish:" + task1.Status);
            var pTask = Task.Factory.StartNew(() =>
            {
                var cTask = Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Childen task finished!");
                });
                Console.WriteLine("Parent task finished!");
            });
            pTask.Wait();
            Console.WriteLine("Flag");
            Console.Read();
            Console.ReadLine();
        }
      

        public static void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }
        public static void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        public static void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
            Console.ReadLine();
        }
        public static void ParallelForMethod()
        {
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");
            Console.ReadLine();
        }
        public static void ParallelForMethod2()
        {
            var obj = new Object();
            long num = 0;
            ConcurrentBag<long> bag = new ConcurrentBag<long>();

            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    num++;
                }
            }
            stopWatch.Stop();
            Console.WriteLine(num);
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    //int sum = 0;
                    //sum += item;
                    lock (obj)
                    {
                        num++;
                    }
                }
            });
            stopWatch.Stop();
            Console.WriteLine(num);
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");
            Console.ReadLine();
        }
        public static void ParallelBreak()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            stopWatch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                     state.Break();
                    
                    return;
                }
                bag.Add(i);
            });
            stopWatch.Stop();
            Console.WriteLine("Bag count is " + bag.Count + ", " + stopWatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
        public static void OrderByTest()
        {
            Stopwatch stopWatch = new Stopwatch();
            List<Custom> customs = new List<Custom>();
            for (int i = 0; i < 2000000; i++)
            {
                customs.Add(new Custom() { Name = "Jack", Age = 21, Address = "NewYork" });
                customs.Add(new Custom() { Name = "Jime", Age = 26, Address = "China" });
                customs.Add(new Custom() { Name = "Tina", Age = 29, Address = "ShangHai" });
                customs.Add(new Custom() { Name = "Luo", Age = 30, Address = "Beijing" });
                customs.Add(new Custom() { Name = "Wang", Age = 60, Address = "Guangdong" });
                customs.Add(new Custom() { Name = "Feng", Age = 25, Address = "YunNan" });
            }

            stopWatch.Restart();
            var groupByAge = customs.GroupBy(item => item.Age).ToList();
            foreach (var item in groupByAge)
            {
                Console.WriteLine("Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();

            Console.WriteLine("Linq group by time is: " + stopWatch.ElapsedMilliseconds);


            stopWatch.Restart();
            var lookupList = customs.ToLookup(i => i.Age);
            foreach (var item in lookupList)
            {
                Console.WriteLine("LookUP:Age={0},count = {1}", item.Key, item.Count());
            }
            stopWatch.Stop();
            Console.WriteLine("LookUp group by time is: " + stopWatch.ElapsedMilliseconds);
        }
    }

    public class Custom
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
