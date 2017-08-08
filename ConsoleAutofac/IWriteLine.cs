using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac
{
	public interface IWriteLine: ISingleton
	{
		void WriteLine(string text);
	}
}
