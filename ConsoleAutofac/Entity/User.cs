using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac.Entity
{
	[Table("user")]
	public class User
	{
		[Key]
		public int UserId { get; set; }
		[Column("strFirstName")]  //真实列名
		public string FirstName { get; set; }//列别名
		public string LastName { get; set; }
		public int Age { get; set; }
	}
}
