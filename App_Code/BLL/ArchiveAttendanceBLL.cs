using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for AttendanceBLL
/// </summary>
public class ArchiveAttendanceBLL
{
    private ArchiveAttendanceTableAdapter _archiveAttendanceAdapter = null;
    protected ArchiveAttendanceTableAdapter Adapter
    {
        get
        {
            if (this._archiveAttendanceAdapter == null)
                this._archiveAttendanceAdapter = new ArchiveAttendanceTableAdapter();
            return this._archiveAttendanceAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    protected SwimTeamDatabase.ArchiveAttendanceDataTable GetAllArchiveAttendances()
    {
        return this.Adapter.GetAllArchiveAttendances();
    }

    public void BeginBatchInsert()
    {
        this.Adapter.BeginBatchInsert();
    }

    public bool BatchInsert(String USAID, DateTime Date, int PracticeoftheDay, int GroupID, String AttendanceType,
        String Note, int? Lane, int? Yards, int? Meters)
    {
        return this.Adapter.BatchInsert(USAID, Date, PracticeoftheDay, GroupID, AttendanceType, Note, Lane, Yards, Meters);
    }

    public void CommitBatchInserts()
    {
        this.Adapter.CommitBatchInsert();
    }

    public void EndBatchInsert()
    {
        this.Adapter.EndBatchInsert();
    }
}