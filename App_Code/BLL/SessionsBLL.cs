using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for SessionsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class SessionsBLL
{
    private SessionsTableAdapter _sessionsAdapter = null;
    protected SessionsTableAdapter Adapter
    {
        get
        {
            if (this._sessionsAdapter == null)
                this._sessionsAdapter = new SessionsTableAdapter();
            return this._sessionsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.SessionsDataTable GetAllSessions()
    {
        return Adapter.GetAllSessions();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionsDataTable GetSessionsByMeetID(int MeetID)
    {
        return Adapter.GetSessionsByMeet(MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateSession(int SessionID, DateTime SessionDate, DateTime WarmUpTime, bool WarmUpGuess, 
        bool PrelimFinals, int MeetID, int MinAge, int MaxAge)
    {
        return Adapter.UpdateSession(SessionDate, WarmUpTime, WarmUpGuess, PrelimFinals, MeetID,
            MinAge, MaxAge, SessionID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteSession(int SessionID)
    {
        return Adapter.DeleteSession(SessionID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool CreateSession(DateTime SessionDate, DateTime WarmUpTime, bool WarmUpGuess,
        bool PrelimFinals, int MeetId, int MinAge, int MaxAge)
    {
        return Adapter.CreateSession(SessionDate, WarmUpTime, WarmUpGuess, PrelimFinals, MeetId, MinAge, MaxAge) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionsDataTable GetSessionsBetweenDatesInclusive(DateTime StartDate, DateTime EndDate)
    {
        return Adapter.GetSessionsBetweenDatesInclusive(StartDate, EndDate);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionsDataTable GetSessionsForMonth(DateTime Month)
    {
        DateTime StartDate = new DateTime(Month.Year, Month.Month, 1, 0, 0, 0);
        DateTime EndDate = new DateTime(Month.Year, Month.Month, DateTime.DaysInMonth(Month.Year, Month.Month), 23, 59, 59);
        return this.GetSessionsBetweenDatesInclusive(StartDate, EndDate);
    }
}