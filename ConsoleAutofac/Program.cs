using Autofac;
using ConsoleAutofac.Entity;
using Dapper;
using MySql.Data.MySqlClient;
using RabbitMQ.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
			string connectionString = "server=192.168.2.47;user id=root;password=goldeneye;database=testzhang;allow zero datetime=true;charset=utf8;MaximumPoolSize=1000";
			builder.RegisterInstance<IDbConnection>(new MySqlConnection(connectionString));
			ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.2.47");
			IDatabase db = redis.GetDatabase();
			ISubscriber sub = redis.GetSubscriber();
			builder.RegisterInstance<IDatabase>(db);//注入redisdb
			builder.RegisterInstance<ISubscriber>(sub);//注入redissub
			container = builder.Build();
			SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
			TestWrite();
			//TestDapper();
			//TestRedis();
			//TestRedisSubscriber();
			TestRabbitmq();
			Console.Read();
		}
		public static void TestRabbitmq()
		{
			ConnectionFactory factory = new ConnectionFactory();
			// "guest"/"guest" by default, limited to localhost connections
			factory.UserName = "tangxd";
			factory.Password = "123456";
			factory.VirtualHost = "/";
			factory.HostName = "192.168.2.47";
			IConnection conn = factory.CreateConnection();
			IModel model = conn.CreateModel();
			Console.WriteLine(conn.ToString());
			string exchangeName = "exchangeName1";
			string queueName = "queueName1";
			string routingKey = "routingKey1";
			model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
			model.QueueDeclare(queueName, false, false, false, null);
			model.QueueBind(queueName, exchangeName, routingKey, null);
			//发布
			byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
			model.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
			//接收
			bool noAck = false;
			BasicGetResult result = model.BasicGet(queueName, noAck);
			if (result == null)
			{
				// No message available at this time.
			}
			else
			{
				IBasicProperties props = result.BasicProperties;
				byte[] body = result.Body;
				
			}
		}
		public static void TestRedisSubscriber()
		{
			ISubscriber sub = container.Resolve<ISubscriber>();
			sub.Subscribe("messages", (channel, message) =>
			{
				Console.WriteLine((string)message);
			});//非阻塞
			Console.WriteLine("请输入后，回车发送消息");
			string send = Console.ReadLine();
			sub.Publish("messages", send);
		}
		public static void TestRedis()
		{
			IDatabase db = container.Resolve<IDatabase>();
			//string
			string value = "abcdefg";
			db.StringSet("mykey", value);
			string valueGet = db.StringGet("mykey");
			Console.WriteLine(valueGet); // writes: "abcdefg"
										 //hash
			string hashkey = "hashkey";
			string hashField = "hashfield";
			string hashValue = "hashValue";
			db.HashSet(hashkey, hashField, hashValue);
			string valueHash = db.HashGet(hashkey, hashField);
			Console.WriteLine(valueHash);
			//set
			string setKey = "setKey";
			string setValue = "setValue";
			db.SetAdd(setKey, setValue);
			var setResult = db.SetMembers(setKey);
			Console.WriteLine(setResult[0]);
			//list
			string listKey = "listKey";
			string listValue = "listValue";
			db.ListRightPush(listKey, listValue);
			var listResult = db.ListLeftPop(listKey);
			Console.WriteLine(listResult);
			//sortedlist
			string zlistkey = "zlistKey";
			string zlistValue = "zlistValue";
			db.SortedSetAdd(zlistkey, zlistValue, 10);
			db.SortedSetAdd(zlistkey, zlistValue + "2", 5);
			var zlistResult = db.SortedSetRangeByRank(zlistkey);
			Console.WriteLine(zlistResult[0]);
		}
		public static void TestDapper()
		{
			IDbConnection connection = container.Resolve<IDbConnection>();
			var result = connection.Get<UserEntity>(1);
			// var resultInsert=connection.Insert(new UserEntity { FirstName = "User3", LastName = "Person3", Age = 13});
			var list = connection.GetList<UserEntity>();
			list = connection.GetList<UserEntity>(new { Age = 10 });
			list = connection.GetList<UserEntity>("where age = 10 or lastname like '%son%'");
			Console.WriteLine(list.Count());
		}
		public static void TestWrite()
		{
			IWrite write = container.Resolve<IWrite>();
			write.WriteLine("haha");
			Console.WriteLine("");
			IWrite write2 = container.Resolve<IWrite>();
			Console.WriteLine(write == write2);
			IWriteLine writeLine1 = container.Resolve<IWriteLine>();
			IWriteLine writeLine2 = container.Resolve<IWriteLine>();
			Console.WriteLine(writeLine1 == writeLine2);
		}
	}
}
