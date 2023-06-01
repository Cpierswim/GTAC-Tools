using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;



namespace SwimTeamDatabaseTableAdapters
{
    public partial class ArchiveAttendanceTableAdapter
    {
        private bool _BatchInsertOpen = false;
        public bool BatchInsertOpen
        { get { return this._BatchInsertOpen; } }

        private SwimTeamDatabase.ArchiveAttendanceDataTable InsertTable;

        private int HighestAttendanceID;

        public void BeginBatchInsert()
        {

            InsertTable = new SwimTeamDatabase.ArchiveAttendanceDataTable();
            HighestAttendanceID = -1;
            this._BatchInsertOpen = true;
        }

        public bool BatchInsert(String USAID, DateTime Date, int PracticeoftheDay, int GroupID, String AttendanceType,
            String Note, int? Lane, int? Yards, int? Meters)
        {
            if (_BatchInsertOpen)
            {
                SwimTeamDatabase.ArchiveAttendanceRow NewRow = InsertTable.NewArchiveAttendanceRow();
                NewRow.USAID = USAID;
                NewRow.Date = Date;
                NewRow.PracticeoftheDay = PracticeoftheDay;
                NewRow.GroupID = GroupID;
                if (AttendanceType == null)
                    NewRow.SetAttendanceTypeNull();
                else
                    NewRow.AttendanceType = AttendanceType;
                if (Note == null)
                    NewRow.SetNoteNull();
                else
                    NewRow.Note = Note;
                if (Lane == null)
                    NewRow.SetLaneNull();
                else
                    NewRow.Lane = System.Convert.ToInt32(Lane);
                if (Yards == null)
                    NewRow.SetYardsNull();
                else
                    NewRow.Yards = System.Convert.ToInt32(Yards);
                if (Meters == null)
                    NewRow.SetMetersNull();
                else
                    NewRow.Meters = System.Convert.ToInt32(Meters);
                this.HighestAttendanceID--;
                NewRow.OldAttendanceID = HighestAttendanceID;

                InsertTable.AddArchiveAttendanceRow(NewRow);

                return true;
            }
            else
                return false;
        }

        public void CommitBatchInsert()
        {
            if (this._BatchInsertOpen)
            {
                int BatchSize = InsertTable.Count;
                SqlConnection Connection = this.Connection;
                Connection.Open();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(Connection);


                if (this.Adapter.TableMappings.Count != 1)
                    throw new Exception("Error. AttendanceTableAdapter mapped to " + this.Adapter.TableMappings.Count + " Tables.");
                bulkCopy.DestinationTableName = this.Adapter.TableMappings[0].DataSetTable;
                bulkCopy.BatchSize = BatchSize;


                bulkCopy.WriteToServer(InsertTable);
                InsertTable.Clear();
                bulkCopy.Close();
            }
        }
        public void EndBatchInsert()
        {
            _BatchInsertOpen = false;
            this.InsertTable.Clear();
            this.InsertTable.Dispose();
            this.InsertTable = null;
        }
        public void ClearBatchInserts()
        {
            if (this.BatchInsertOpen)
                InsertTable.Clear();
        }
    }
}