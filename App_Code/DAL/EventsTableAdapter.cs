using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for EventsTableAdapter
/// </summary>
namespace SwimTeamDatabaseTableAdapters
{
    public partial class EventsTableAdapter
    {
        public int BatchUpdateIgnoreDBConcurrency(SwimTeamDatabase.EventsDataTable EventsTable, int BatchSize)
        {
            this.Adapter.UpdateBatchSize = BatchSize;

            int ReturnValue = 0;
            if (EventsTable != null)
            {

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

                try
                {
                    ReturnValue = this.Update(EventsTable);
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
            }

            return ReturnValue;
        }
    }
}