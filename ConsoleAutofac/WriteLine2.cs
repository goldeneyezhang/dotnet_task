using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac
{
	public class WriteLine2 : IWriteLine
	{
		public void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}
}
