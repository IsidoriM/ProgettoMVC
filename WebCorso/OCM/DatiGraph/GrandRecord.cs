using System;
using System.Collections.Generic;
using System.Text;

namespace OCM.DatiGraph
{
    public class GrandRecord
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public IList<Record> Records { get; set; }
    }
}
