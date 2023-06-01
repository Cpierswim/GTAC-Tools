using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for EntryBLL
/// </summary>
[System.ComponentModel.DataObject]
public class EntryBLL
{
    private EntriesTableAdapter _entriesAdapter = null;
    protected EntriesTableAdapter Adapter
    {
        get
        {
            if (this._entriesAdapter == null)
                this._entriesAdapter = new EntriesTableAdapter();
            return this._entriesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.EntriesDataTable GetAllEntries()
    {
        return Adapter.GetAllEntries();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesDataTable GetEntriesForSwimmer(String USAID)
    {
        return Adapter.GetEntriesByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesDataTable GetEntriesForMeet(int MeetID)
    {
        return Adapter.GetEntriesByMeet(MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesDataTable GetEntriesForSession(int SessionID)
    {
        return Adapter.GetEntriesBySession(SessionID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool AddSwimmerToSession(int SessionID, String USAID, int MeetID)
    {
        return Adapter.AddEntry(SessionID, USAID, false, MeetID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteEntry(int EntryID)
    {
        return Adapter.DeleteEntry(EntryID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EntriesDataTable GetEntriesByMeetAndSwimmer(int MeetID, String USAID)
    {
        return Adapter.GetEntriesByMeetAndSwimmer(USAID, MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetEntryAsInDatabase(int EntryID)
    {
        return Adapter.SetEntryAsInDatabase(EntryID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool RemoveSwimmerFromAllMeets(String USAID)
    {
        return Adapter.RemoveSwimmerFromAllMeets(USAID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateUSAID(String OldUSAID, String NewUSAID)
    {
        return Adapter.UpdateUSAID(NewUSAID, OldUSAID) == 1;
    }
}