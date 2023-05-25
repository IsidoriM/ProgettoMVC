﻿using OCM.DatiGraph;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Graph
    {
        public List<GrandRecord> ImpostaRecord()
        {
            List<GrandRecord> grandRecords = new List<GrandRecord>();

            var id = int.MinValue;
            foreach (var grandRecord in grandRecords)
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

            return grandRecords;
        }
    }
}
