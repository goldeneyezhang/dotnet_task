using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac
{
	public class WriteString : IWrite
	{
		public void WriteLine(string text)
		{
			Console.WriteLine("out:" + text);
		}
	}
}
