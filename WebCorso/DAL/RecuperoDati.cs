using OCM.DatiGraph;
using System.Data;

namespace DAL
{
    public class RecuperoDati
    {
        public DataTable recupera(List<GrandRecord> graficoPieno)
        {
            DataTable recordTable = new DataTable("RecordTableType");
            recordTable.Columns.Add("Id", typeof(Int32));
            recordTable.Columns.Add("GrandRecordId", typeof(Int32));
            recordTable.Columns.Add("Name", typeof(String));
            DataTable table = new DataTable();
            var records = graficoPieno.SelectMany(gr => gr.Records);
            foreach (var record in records)
            {
                table.Rows.Add(new object[] { record.Id, record.GrandRecordId, record.Name });
            }
            return table;
        }

    }
}