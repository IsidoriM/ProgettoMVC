using System;
using System.Collections.Generic;
using System.Text;

namespace OCM.DatiGraph
{
    public class Record
    {
        public Int32 Id { get; set; }
        public Int32 GrandRecordId { get; set; }
        public String Name { get; set; }
        public IList<ChildRecord> ChildRecords { get; set; }
    }
}
