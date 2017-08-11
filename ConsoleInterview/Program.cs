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
			Console.ReadLine();
		}
	}
}
