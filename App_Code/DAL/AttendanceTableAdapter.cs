using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;



namespace SwimTeamDatabaseTableAdapters
{
    public partial class AttendanceTableAdapter
    {
        private bool _BatchInsertOpen = false;
        public bool BatchInsertOpen
        { get { return this._BatchInsertOpen; } }

        public void BeginBatchInsert()
        {

            InsertTable = new SwimTeamDatabase.AttendanceDataTable();
            int? temp = GetHighestAttendanceID();
            if (temp == null)
                this.HighestAttendanceID = 0;
            else
                this.HighestAttendanceID = System.Convert.ToInt32(temp);
            this._BatchInsertOpen = true;
        }

        private SwimTeamDatabase.AttendanceDataTable InsertTable;
        private int HighestAttendanceID;

        public bool BatchInsert(String USAID, DateTime Date, int PracticeoftheDay, int GroupID, String AttendanceType,
            String Note, int? Lane, int? Yards, int? Meters)
        {
            if (_BatchInsertOpen)
            {
                SwimTeamDatabase.AttendanceRow NewRow = InsertTable.NewAttendanceRow();
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
                HighestAttendanceID++;
                NewRow.AttendanceID = HighestAttendanceID;

                InsertTable.AddAttendanceRow(NewRow);

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

        private void ChangeUpdateSize(int updateSize)
        {
            this.Adapter.UpdateBatchSize = updateSize;
        }

        public int BatchUpdate(SwimTeamDatabase.AttendanceDataTable Table, int BatchSize)
        {
            this.Adapter.UpdateBatchSize = BatchSize;

            UpdateRowSource InitialUpdateCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialInsertCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialDeleteCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialSelectCommandRowSource = new UpdateRowSource();

            if (this.Adapter.UpdateCommand != null)
                InitialUpdateCommandRowSource = this.Adapter.UpdateCommand.UpdatedRowSource;
            if (this.Adapter.InsertCommand != null)
                InitialInsertCommandRowSource = this.Adapter.InsertCommand.UpdatedRowSource;
            if (this.Adapter.DeleteCommand != null)
                InitialDeleteCommandRowSource = this.Adapter.DeleteCommand.UpdatedRowSource;
            if (this.Adapter.SelectCommand != null)
                InitialSelectCommandRowSource = this.Adapter.SelectCommand.UpdatedRowSource;

            if (this.Adapter.UpdateCommand != null)
                this.Adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.InsertCommand != null)
                this.Adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.DeleteCommand != null)
                this.Adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.SelectCommand != null)
                this.Adapter.SelectCommand.UpdatedRowSource = UpdateRowSource.None;

            int ReturnValue = this.Update(Table);

            if (this.Adapter.UpdateCommand != null)
                this.Adapter.UpdateCommand.UpdatedRowSource = InitialUpdateCommandRowSource;
            if (this.Adapter.InsertCommand != null)
                this.Adapter.InsertCommand.UpdatedRowSource = InitialInsertCommandRowSource;
            if (this.Adapter.DeleteCommand != null)
                this.Adapter.DeleteCommand.UpdatedRowSource = InitialDeleteCommandRowSource;
            if (this.Adapter.SelectCommand != null)
                this.Adapter.SelectCommand.UpdatedRowSource = InitialSelectCommandRowSource;

            return ReturnValue;

        }
        public int BatchUpdateIgnoreDBConcurrency(SwimTeamDatabase.AttendanceDataTable Table, int BatchSize)
        {
            this.Adapter.UpdateBatchSize = BatchSize;

            UpdateRowSource InitialUpdateCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialInsertCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialDeleteCommandRowSource = new UpdateRowSource();
            UpdateRowSource InitialSelectCommandRowSource = new UpdateRowSource();

            if (this.Adapter.UpdateCommand != null)
                InitialUpdateCommandRowSource = this.Adapter.UpdateCommand.UpdatedRowSource;
            if (this.Adapter.InsertCommand != null)
                InitialInsertCommandRowSource = this.Adapter.InsertCommand.UpdatedRowSource;
            if (this.Adapter.DeleteCommand != null)
                InitialDeleteCommandRowSource = this.Adapter.DeleteCommand.UpdatedRowSource;
            if (this.Adapter.SelectCommand != null)
                InitialSelectCommandRowSource = this.Adapter.SelectCommand.UpdatedRowSource;

            if (this.Adapter.UpdateCommand != null)
                this.Adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.InsertCommand != null)
                this.Adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.DeleteCommand != null)
                this.Adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
            if (this.Adapter.SelectCommand != null)
                this.Adapter.SelectCommand.UpdatedRowSource = UpdateRowSource.None;
            int ReturnValue = -1;
            try
            {
                ReturnValue = this.Update(Table);
            }
            catch (DBConcurrencyException)
            {

            }

            if (this.Adapter.UpdateCommand != null)
                this.Adapter.UpdateCommand.UpdatedRowSource = InitialUpdateCommandRowSource;
            if (this.Adapter.InsertCommand != null)
                this.Adapter.InsertCommand.UpdatedRowSource = InitialInsertCommandRowSource;
            if (this.Adapter.DeleteCommand != null)
                this.Adapter.DeleteCommand.UpdatedRowSource = InitialDeleteCommandRowSource;
            if (this.Adapter.SelectCommand != null)
                this.Adapter.SelectCommand.UpdatedRowSource = InitialSelectCommandRowSource;

            return ReturnValue;


        }
        private UpdateRowSource InitialUpdateCommandRowSource;
        private UpdateRowSource InitialInsertCommandRowSource;
        private UpdateRowSource InitialDeleteCommandRowSource;
        private UpdateRowSource InitialSelectCommandRowSource;

        public void SetBatchSize(int BatchSize)
        {
            this.Adapter.UpdateBatchSize = BatchSize;
        }

        public void PrePareForBatchProcessing()
        {
            InitialUpdateCommandRowSource = this.Adapter.UpdateCommand.UpdatedRowSource;
            InitialInsertCommandRowSource = this.Adapter.InsertCommand.UpdatedRowSource;
            InitialDeleteCommandRowSource = this.Adapter.DeleteCommand.UpdatedRowSource;
            InitialSelectCommandRowSource = this.Adapter.SelectCommand.UpdatedRowSource;

            this.Adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            this.Adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            this.Adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
            this.Adapter.SelectCommand.UpdatedRowSource = UpdateRowSource.None;

            this.Adapter.UpdateCommand.Connection.Open();
        }
        public void EndBatchProcess()
        {
            this.Adapter.UpdateCommand.UpdatedRowSource = InitialUpdateCommandRowSource;
            this.Adapter.InsertCommand.UpdatedRowSource = InitialInsertCommandRowSource;
            this.Adapter.DeleteCommand.UpdatedRowSource = InitialDeleteCommandRowSource;
            this.Adapter.SelectCommand.UpdatedRowSource = InitialSelectCommandRowSource;

            this.Adapter.UpdateCommand.Connection.Close();
        }
    }
}