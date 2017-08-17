using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterview
{
	class Program
	{
		static void Main(string[] args)
		{
			IInterface classA = new ClassA();
			IInterface classB = new ClassB();
			IInterface classC = new ClassC();
			classA.Publish();
			classB.Publish();
			classC.Publish();
			Basic classAA = new ClassA();
			Basic classBB = new ClassB();
			Basic classCC = new ClassC();
			classAA.Publish();
			classBB.Publish();
			classCC.Publish();
			var classAAA = new ClassA();
			var classBBB = new ClassB();
			var classCCC = new ClassC();
			classAAA.Publish();
			classBBB.Publish();
			classCCC.Publish();
			B b = new B();
			b.PrintFields();
			Console.ReadLine();
			
		}
	}
	class A
	{
		public A()
		{
			PrintFields();
		}
		public virtual void PrintFields() { }
	}
	class B : A
	{
		int x = 1;
		int y;
		public B()
		{
			y = -1;
		}
		public override void PrintFields()
		{
			Console.WriteLine("x={0},y={1}", x, y);
		}
	}
	//密封类型
	public sealed class SealClass
	{

	}
}
