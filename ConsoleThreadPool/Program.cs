using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThreadPool
{
	class Program
	{
		static void Main(string[] args)
		{
			Foobar1();
			Foobar2();
			Foobar3();
		}
		public static void Foobar1()
		{
			int i = 0;
			int j = 0;
			//前面是辅助（也就是所谓的工作者）线程，后面是I/O线程
			ThreadPool.GetMaxThreads(out i, out j);
			Console.WriteLine(i.ToString() + "  " + j.ToString());//默认都是1000
			//获取空闲线程，由于现在没有使用异步线程，所以为空
			ThreadPool.GetAvailableThreads(out i, out j);
			Console.WriteLine(i.ToString() + "  " + j.ToString());
			Console.ReadKey();

		}
		public static void Foobar2()
		{
			//工作者线程最大数目，I/O线程的最大数目
			ThreadPool.SetMaxThreads(1000, 1000);
			//启动工作者线程
			ThreadPool.QueueUserWorkItem(new WaitCallback(RunWorkerThread));
			Console.ReadKey();
		}
		public static void RunWorkerThread(object state)
		{
			Console.WriteLine("RunWorkerThread开始工作");
			Console.WriteLine("工作者线程启动成功");
		}
		public static void RunWorkerThread2(object obj)
		{
			Thread.Sleep(200);
			Console.WriteLine("线程池线程开始");
			Person p = obj as Person;
			Console.WriteLine(p.Name);
			
		}
		public static void Foobar3()
		{
			Person p = new Person(1, "张一博");
			//启动工作者线程
			ThreadPool.QueueUserWorkItem(new WaitCallback(RunWorkerThread2),p);
			Console.ReadKey();
		}
	}
}
