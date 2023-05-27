using System;
using System.Collections.Generic;

namespace ObjectGraphs.Models
{
	public class GrandRecord
	{
		public Int32 Id { get; set; }

		public String Name { get; set; }

		public List<Record> Records { get; set; }
	}

}