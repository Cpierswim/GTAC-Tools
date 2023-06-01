using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for JobEventsAdapter
/// </summary>
 [System.ComponentModel.DataObject]
public class JobEventsBLL
{
    private JobEventsTableAdapter _JobEventsAdapter = null;
    protected JobEventsTableAdapter Adapter
    {
        get
        {
            if (this._JobEventsAdapter == null)
                this._JobEventsAdapter = new JobEventsTableAdapter();
            return this._JobEventsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobEventsDataTable GetAll()
    {
        return this.Adapter.GetAll();
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobEventsRow GetByJobEventID(int JobEventID)
    {
        SwimTeamDatabase.JobEventsDataTable JobEvents = this.Adapter.GetByJobEventID(JobEventID);
        if (JobEvents.Count == 0)
            return null;
        else if (JobEvents.Count > 1)
            throw new Exception("Too many Job events found");
        else
            return JobEvents[0];
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public bool Insert(global::System.Nullable<int> MeetID, 
        string OtherEventName, string Notes, global::System.Nullable<int> MeetSessionID)
    {
        return this.Adapter.Insert(MeetID, OtherEventName, Notes, MeetSessionID) == 1;
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public int Update(SwimTeamDatabase.JobEventsDataTable JobEventsTable)
    {
        List<SwimTeamDatabase.JobEventsRow> DeletedRows = new List<SwimTeamDatabase.JobEventsRow>();
        for (int i = 0; i < JobEventsTable.Count; i++)
            if (JobEventsTable[i].RowState == DataRowState.Deleted)
                throw new Exception("This method cannot support deletes. Must use Delete(int) method.");
        return this.Adapter.Update(JobEventsTable);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int Delete(int JobEventID)
    {
        JobSignUpsBLL SignupsAdapter = new JobSignUpsBLL();
        SignupsAdapter.DeleteByEventID(JobEventID);
        return this.Adapter.Delete(JobEventID);
    }
}