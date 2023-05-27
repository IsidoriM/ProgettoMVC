using System;
using System.Collections.Generic;

namespace ObjectGraphs.Models
{
	public class Record
	{
		public Int32 Id { get; set; }

		public Int32 GrandRecordId { get; set; }
		
		public String Name { get; set; }

		public List<ChildRecord> ChildRecords { get; set; }
	}

}