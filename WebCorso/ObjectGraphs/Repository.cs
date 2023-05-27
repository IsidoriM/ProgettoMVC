using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using ObjectGraphs.Models;

namespace ObjectGraphs
{
    public class Repository
    {
        public List<GrandRecord> ImpostaRecord()
        {
            List<ChildRecord> R3 = new List<ChildRecord>() {
                new ChildRecord(){ Id = 3, RecordId = 2,Name="(A)A"},
                 new ChildRecord(){ Id = 0, RecordId = 2,Name="(A)A"},

            };

            List<Record> R2 = new List<Record>() {
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
        public static IList<GrandRecord> GetGrandRecords()
		{
			using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ObjectGraphs;Integrated Security=True;"))
			{
				using (var cmd = connection.CreateCommand())
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = "dbo.GetGrandRecords";
					
					var grandRecords = new List<GrandRecord>();
					var records = new List<Record>();
					var childRecords = new List<ChildRecord>();

					connection.Open();

					using (var reader = cmd.ExecuteReader())
					{
						ReadRecords(reader, grandRecords, records, childRecords);
					}

					connection.Close();
					
					JoinRecords(grandRecords, records, childRecords);

					return grandRecords;

				}
			}
		}

		public static GrandRecord GetGrandRecordById(int grandRecordId)
		{
			using (SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ObjectGraphs;Integrated Security=True;"))
			{
				using (var cmd = connection.CreateCommand())
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = "dbo.GetGrandRecordById";

					cmd.Parameters.Add( new SqlParameter
					{ 
						ParameterName = "@Id",
						Direction = ParameterDirection.Input,
						SqlDbType = SqlDbType.Int,
						Value = grandRecordId
					});
					
					var grandRecords = new List<GrandRecord>();
					var records = new List<Record>();
					var childRecords = new List<ChildRecord>();

					connection.Open();

					using (var reader = cmd.ExecuteReader())
					{
						ReadRecords(reader, grandRecords, records, childRecords);
					}

					connection.Close();
					
					JoinRecords(grandRecords, records, childRecords);

					return grandRecords.First();

				}
			}
		}



		public static IList<GrandRecord> SaveGrandRecords(IList<GrandRecord> grandRecords)
		{
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


			using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-KD5PU46V;Initial Catalog=Graphico;Integrated Security=True;"))
			{
				using (var cmd = connection.CreateCommand())
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = "dbo.SaveGrandRecords";


					var grandRecordTable = new DataTable("GrandRecordTableType");

					grandRecordTable.Columns.Add( "Id"   , typeof( Int32  ));
					grandRecordTable.Columns.Add( "Name" , typeof( String ));

					foreach(var grandRecord in grandRecords) 
					{
						grandRecordTable.Rows.Add(new object[] {grandRecord.Id, grandRecord.Name});
					}

					cmd.Parameters.Add( new SqlParameter
					{ 
						ParameterName = "@GrandRecords",
						Direction = ParameterDirection.Input,
						SqlDbType = SqlDbType.Structured,
						TypeName = grandRecordTable.TableName,
						Value = grandRecordTable
					});



					var recordTable = new DataTable("RecordTableType");

					recordTable.Columns.Add( "Id"            , typeof( Int32  ));
					recordTable.Columns.Add( "GrandRecordId" , typeof( Int32  ));
					recordTable.Columns.Add( "Name"          , typeof( String ));

					var records = grandRecords.SelectMany(gr => gr.Records);

					foreach(var record in records) 
					{
						recordTable.Rows.Add(new object[] {record.Id, record.GrandRecordId, record.Name});
					}

					cmd.Parameters.Add( new SqlParameter
					{ 
						ParameterName = "@Records",
						Direction = ParameterDirection.Input,
						SqlDbType = SqlDbType.Structured,
						TypeName = recordTable.TableName,
						Value = recordTable
					});


					var childRecordTable = new DataTable("ChildRecordTableType");

					childRecordTable.Columns.Add( "Id"       , typeof( Int32  ));
					childRecordTable.Columns.Add( "RecordId" , typeof( Int32  ));
					childRecordTable.Columns.Add( "Name"     , typeof( String ));

					var childRecords = records.SelectMany(r => r.ChildRecords);

					foreach(var childRecord in childRecords) 
					{
						childRecordTable.Rows.Add(new object[] {childRecord.Id, childRecord.RecordId, childRecord.Name});
					}

					cmd.Parameters.Add( new SqlParameter
					{ 
						ParameterName = "@ChildRecords",
						Direction = ParameterDirection.Input,
						SqlDbType = SqlDbType.Structured,
						TypeName = childRecordTable.TableName,
						Value = childRecordTable
					});

					
					var savedGrandRecords = new List<GrandRecord>();
					var savedRecords = new List<Record>();
					var savedChildRecords = new List<ChildRecord>();

					connection.Open();

					using (var reader = cmd.ExecuteReader())
					{
						ReadRecords(reader, savedGrandRecords, savedRecords, savedChildRecords);
					}


					connection.Close();
					
					JoinRecords(savedGrandRecords, savedRecords, savedChildRecords);

					return savedGrandRecords;

				}
			}

		}

	    private static void ReadRecords(SqlDataReader reader, List<GrandRecord> savedGrandRecords, List<Record> savedRecords, List<ChildRecord> savedChildRecords)
	    {
		    while (reader.Read())
		    {
			    savedGrandRecords.Add(
				    new GrandRecord
				    {
					    Id = reader.GetInt32(0),
					    Name = reader.GetString(1)
				    }
				    );
		    }

		    reader.NextResult();

		    while (reader.Read())
		    {
			    savedRecords.Add(
				    new Record
				    {
					    Id = reader.GetInt32(0),
					    GrandRecordId = reader.GetInt32(1),
					    Name = reader.GetString(2)
				    }
				    );
		    }

		    reader.NextResult();

		    while (reader.Read())
		    {
			    savedChildRecords.Add(
				    new ChildRecord
				    {
					    Id = reader.GetInt32(0),
					    RecordId = reader.GetInt32(1),
					    Name = reader.GetString(2)
				    }
				    );
		    }
	    }

	    private static void JoinRecords(IList<GrandRecord> grandRecords, List<Record> records, List<ChildRecord> childRecords)
	    {
		    var recordEnumerator = records.GetEnumerator();
		    var record = recordEnumerator.MoveNext() ? recordEnumerator.Current : null;

		    var childRecordEnumerator = childRecords.GetEnumerator();
		    var childRecord = childRecordEnumerator.MoveNext() ? childRecordEnumerator.Current : null;


		    foreach (var grandRecord in grandRecords)
		    {
				grandRecord.Records = new List<Record>();

			    while (record != null && record.GrandRecordId == grandRecord.Id)
			    {
					record.ChildRecords = new List<ChildRecord>();

				    while (childRecord != null && childRecord.RecordId == record.Id)
				    {
					    record.ChildRecords.Add(childRecord);

					    childRecord = childRecordEnumerator.MoveNext() ? childRecordEnumerator.Current : null;
				    }

				    grandRecord.Records.Add(record);

				    record = recordEnumerator.MoveNext() ? recordEnumerator.Current : null;
			    }
		    }
	    }
    }
}
