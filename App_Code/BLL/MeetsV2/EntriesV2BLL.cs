using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for EntriesV2BLL
/// </summary>
[System.ComponentModel.DataObject]
public class EntriesV2BLL
{
    private EntriesV2TableAdapter _EntriesAdapter;
    private EntriesV2TableAdapter EntriesAdapter
    {
        get
        {
            if (this._EntriesAdapter == null)
                this._EntriesAdapter = new EntriesV2TableAdapter();
            return this._EntriesAdapter;
        }
    }

    private FullMeetEntryTableAdapter _FullEntriesAdapter;
    private FullMeetEntryTableAdapter FullEntriesAdapter
    {
        get
        {
            if (this._FullEntriesAdapter == null)
                this._FullEntriesAdapter = new FullMeetEntryTableAdapter();
            return this._FullEntriesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesV2DataTable GetAllEntries()
    {
        return this.EntriesAdapter.GetAllEntries();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesV2DataTable GetEntriesByMeetandSwimmer(int MeetID, String USAID)
    {
        return this.EntriesAdapter.GetEntriesByMeetAndSwimmer(USAID, MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int DeleteSwimmerFromMeetEvent(String USAID, int EventID)
    {
        SwimmerAthleteJoinBLL JoinAdapter = new SwimmerAthleteJoinBLL();
        int AthleteID = JoinAdapter.GetAtheleIDFromDatabase(USAID);

        return this.EntriesAdapter.DeleteAthelteFromEvent(AthleteID, EventID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int BatchUpdate(SwimTeamDatabase.EntriesV2DataTable EntriesTable)
    {
        return this.EntriesAdapter.BatchUpdateIgnoreDBConcurrency(EntriesTable, EntriesTable.Count);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int Update(SwimTeamDatabase.EntriesV2DataTable EntriesTable)
    {
        return this.EntriesAdapter.Update(EntriesTable);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesV2DataTable GetByMeetIDandGroupID(int MeetID, int GroupID)
    {
        return this.EntriesAdapter.GetEventsByMeetIDAndGroupID(MeetID, GroupID);
    }

    public int GetMaxEntryID()
    {
        return this.EntriesAdapter.GetMaxEntryID() ?? 0;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int DeleteSwimmerEntriesFromMeetSession(int MeetID, int SessionNumber, String USAID)
    {
        return this.EntriesAdapter.DeleteSwimmersEntriesFromMeetSession(USAID, MeetID, SessionNumber);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesV2DataTable GetAllEntriesForSwimmer(String USAID)
    {
        return this.EntriesAdapter.GetAllEntriesByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FullMeetEntryDataTable GetFullEntriesForMeetByFamilyID(int MeetID, int FamilyID)
    {
        return this.FullEntriesAdapter.GetFullEntryByMeetandFamily(MeetID, FamilyID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FullMeetEntryDataTable GetFullEntriesForMeet(int MeetID)
    {
        return this.FullEntriesAdapter.GetFullMeetEntryByMeet(MeetID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesV2DataTable GetByMeetID(int MeetID)
    {
        return this.EntriesAdapter.GetByMeetID(MeetID);
    }
}