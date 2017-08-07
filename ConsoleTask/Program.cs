using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTask
{
	class Program
	{
		static void Main(string[] args)
		{
			var task4 = new Task(() =>
			{
				Console.WriteLine("Task 4 Begin");
				System.Threading.Thread.Sleep(2000);
				Console.WriteLine("Task 4 Finish");
			});
			var task5 = new Task(() =>
			{
				Console.WriteLine("Task 5 Begin");
				System.Threading.Thread.Sleep(3000);
				Console.WriteLine("Task 5 Finish");
			});

			task4.Start();
			task5.Start();
			var result = task4.ContinueWith<string>(task =>
			  {
				  Console.WriteLine("task1 finished");
				  return "This is task result!";
			  });
			Console.WriteLine(result.Result.ToString());
			//Task.WaitAll(task4, task5);
			//Console.WriteLine("All task finished!");
			//var task1 = new Task(() =>
			//  {
			//	  Console.WriteLine("Hello,task");
			//  });
			//task1.Start();
			//var task2 = Task.Factory.StartNew(() =>
			//  {
			//	  Console.WriteLine("Hello,task started by task factory");
			//  });
			//var task3 = new Task(() =>
			//  {
			//	  Console.WriteLine("Begin");
			//	  System.Threading.Thread.Sleep(2000);
			//	  Console.WriteLine("Finish");
			//  }
			//);
			//Console.WriteLine("Before start:" + task3.Status);
			//task3.Start();
			//Console.WriteLine("After start:" + task3.Status);
			//task3.Wait();
			//Console.WriteLine("After Finish:" + task3.Status);
			Console.Read();
		}
	}
}
