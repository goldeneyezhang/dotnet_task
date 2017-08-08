﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac
{
	class Program
	{
		private static IContainer container { get; set; }
		static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			//builder.RegisterType<WriteString>().As<IWrite>();
			//builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x=>x.GetInterfaces().Contains(typeof(IDependency))).AsImplementedInterfaces().InstancePerDependency();
			//builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => x.GetInterfaces().Contains(typeof(ISingleton))).AsImplementedInterfaces().SingleInstance();

			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().InstancePerDependency();
			//使用Single Instance作用域，所有对父容器或者嵌套容器的请求都会返回同一个实例。
			//builder.RegisterType<WriteString>().As<IWrite>().SingleInstance();
			//　这个作用域适用于嵌套的生命周期。一个使用Per Lifetime 作用域的component在一个 nested lifetime scope内最多有一个实例。
			//builder.RegisterType<WriteString>().As<IWrite>().InstancePerLifetimeScope();
			//　在其他容器中也称作瞬态或者工厂，使用Per Dependency作用域，服务对于每次请求都会返回互补影响实例。
			//builder.RegisterType<WriteString>().As<IWrite>().InstancePerDependency();
			container =builder.Build();
			IWrite write = container.Resolve<IWrite>();
			write.WriteLine("haha");
			Console.WriteLine("");
			IWrite write2= container.Resolve<IWrite>();
			Console.WriteLine(write == write2);
			IWriteLine writeLine1 = container.Resolve<IWriteLine>();
			IWriteLine writeLine2 = container.Resolve<IWriteLine>();
			Console.WriteLine(writeLine1 == writeLine2);
			IDbConnection connection=
			Console.Read();
		}
	}
}