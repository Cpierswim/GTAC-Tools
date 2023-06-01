using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for Meets
/// </summary>
[System.ComponentModel.DataObject]
public class MeetsBLL
{
    private MeetsTableAdapter _meetsAdapter = null;
    protected MeetsTableAdapter Adapter
    {
        get
        {
            if (this._meetsAdapter == null)
                this._meetsAdapter = new MeetsTableAdapter();
            return this._meetsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.MeetsDataTable GetAllMeets()
    {
        return Adapter.GetAllMeets();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetAllOpenMeets()
    {
        return Adapter.GetOpenMeets();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetOpenMeetsPriorToGuaranteeDeadline()
    {
        return Adapter.GetOpenMeetsPriorToGuaranteeDeadline(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time"));
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetOpenMeetsAvailableForLateEntry()
    {
        return Adapter.GetOpenMeetsAvailableForLateEntry(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time"));
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetMetsEligibleForChanges()
    {
        return Adapter.GetOpenMeetsEligibleForChanges(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacifc Standard Time"));
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateMeet(String MeetName, DateTime GuaranteeDeadline, DateTime LateEntryDeadline, 
        DateTime ChangeDeadline, bool Closed, String MeetNotes, String MeetLocation, int MeetID)
    {
        return Adapter.UpdateMeet(MeetName, GuaranteeDeadline, LateEntryDeadline, ChangeDeadline, Closed,
            MeetNotes, MeetLocation, MeetID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteMeet(int MeetID)
    {
        return Adapter.DeleteMeet(MeetID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool CloseMeet(int MeetID)
    {
        return Adapter.CloseMeet(MeetID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool CreateMeet(String MeetName, DateTime GuaranteeDeadline, DateTime LateEntryDeadline,
        DateTime ChangeDeadline, String MeetNotes, String MeetLocation)
    {
        bool Closed = false;
        return Adapter.CreateMeet(MeetName, GuaranteeDeadline, LateEntryDeadline, ChangeDeadline, Closed,
            MeetNotes, MeetLocation) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool CreateMeet(String MeetName, DateTime GuaranteeDeadline, DateTime LateEntryDeadline,
        DateTime ChangeDeadline, String MeetNotes, String MeetLocation, bool Closed)
    {
        return Adapter.CreateMeet(MeetName, GuaranteeDeadline, LateEntryDeadline, ChangeDeadline, Closed,
            MeetNotes, MeetLocation) == 1;
    }

    public DateTime MeetStartDate(int MeetID)
    {
        SwimTeamDatabase.SessionsDataTable sessions = new SessionsBLL().GetSessionsByMeetID(MeetID);
        if (sessions.Count == 0)
            return DateTime.MaxValue;
        DateTime StartDate = DateTime.MaxValue;
        foreach (SwimTeamDatabase.SessionsRow session in sessions)
            if (session.SessionDate < StartDate)
                StartDate = session.SessionDate;
        return StartDate;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetMeetByMeetID(int MeetID)
    {
        return Adapter.GetMeetByMeetID(MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsDataTable GetMostRecentlyCreatedMeet()
    {
        return Adapter.GetMostRecentlyAddedMeet();
    }
}