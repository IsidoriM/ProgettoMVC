using Newtonsoft.Json;
using OCM.DatiGraph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace BLL
{
    public class Graph
    {
        public List<GrandRecord> ImpostaRecord()
        {
            List<ChildRecord> R3 = new List<ChildRecord>() {
                new ChildRecord(){ Id = 3, RecordId = 2,Name="(A)A"},
                 new ChildRecord(){ Id = 0, RecordId = 2,Name="(A)A"},

            };

            IList<Record> R2 = new List<Record>() {
                new Record(){ Id = 2, GrandRecordId = 1,Name="(A)A",ChildRecords=R3},

            };

            List<GrandRecord> R1 = new List<GrandRecord>() {
                new GrandRecord(){ Id = 1, Name="(A)",Records=R2,},
                
            };

           

            
            var id = int.MinValue;
            foreach (var grandRecord in R1)
            {
                if (grandRecord.Id == 0)
                    grandRecord.Id = id++;

                foreach (var record in grandRecord.Records)
                {
                    if (record.Id == 0)
                        record.Id = id++;

                    record.GrandRecordId = grandRecord.Id;

                    foreach (var childRecord in record.ChildRecords)
                    {
                        if (childRecord.Id == 0)
                            childRecord.Id = id++;

                        childRecord.RecordId = record.Id;
                    }
                }
            }

            return R1;
        }
    }
}
