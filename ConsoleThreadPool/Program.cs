using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThreadPool
{
	delegate string MyDelegate(string name, int age);
	class Program
	{
		static void Main(string[] args)
		{
			Foobar1();
			Foobar2();
			Foobar3();
			Foobar4();
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
		public static void Foobar4()
		{
			//建立委托
			MyDelegate myDelegate = new MyDelegate(GetString);
			//异步调用委托，除最后两个参数外，前面的参数可以传进去
			IAsyncResult result = myDelegate.BeginInvoke("张一博",33,null,null);
			Console.WriteLine("主线程继续工作!");
			//比上个例子，只是利用多了一个IsCompleted属性，来判断异步线程是否完成
			while (!result.IsCompleted)
			{
				Thread.Sleep(500);
				Console.WriteLine("异步线程还没完成，主线程干其他事!");
			}
			//调用EndInvoke(IAsyncResult)获取运行结果，一旦调用了EndInvoke，即使结果还没来得及返回，主线程也阻塞等待了
			//注意获取返回值的方式
			string data = myDelegate.EndInvoke(result);
			Console.WriteLine(data);
			Console.ReadKey();
		}
		public static string GetString(string name,int age)
		{
			Console.WriteLine("我是不是线程池线程" + Thread.CurrentThread.IsThreadPoolThread);
			Thread.Sleep(2000);
			return $"我是{name} ,今年{age}";
		}
	}
}
