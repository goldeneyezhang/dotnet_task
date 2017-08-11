using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterview
{
	public abstract class Basic: IInterface
	{
		public virtual void Publish()
		{
			Console.WriteLine("Basic");
		}
	}
	public class ClassA:Basic
	{
		public void Publish()
		{
			Console.WriteLine("ClassA");
		}
	}
	public class ClassB:Basic
	{
		public override void Publish()
		{
			Console.WriteLine("ClassB");
		}
	}
	public class ClassC:Basic
	{
		public new void Publish()
		{
			Console.WriteLine("ClassC");
		}
	}
}
