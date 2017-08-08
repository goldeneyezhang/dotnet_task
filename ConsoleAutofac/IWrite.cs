using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
namespace ConsoleAutofac
{
	public interface IWrite: IDependency
	{
		 void WriteLine(string text);
	}
}
