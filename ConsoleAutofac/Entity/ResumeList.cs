using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAutofac.Entity
{
	[Table("resume_list")]
	public class ResumeList
	{
		[Key]
		public int Id { get; set; }
		public int Uid { get; set; }
	}
}
