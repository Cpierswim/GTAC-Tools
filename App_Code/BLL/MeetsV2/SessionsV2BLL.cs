using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for SessionsV2BLL
/// </summary>
[System.ComponentModel.DataObject]
public class SessionsV2BLL
{
    private SessionV2TableAdapter _SessionsAdapter;
    private SessionV2TableAdapter SessionsAdapter
    {
        get
        {
            if (this._SessionsAdapter == null)
                this._SessionsAdapter = new SessionV2TableAdapter();
            return this._SessionsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionV2DataTable GetAllSessions()
    {
        return this.SessionsAdapter.GetAllSessions();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionV2DataTable GetSessionsByMeetID(int MeetID)
    {
        return this.SessionsAdapter.GetSessionsByMeetID(MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool Update(string StartTime, bool AM, bool Guess, int SessionsID)
    {
        if (StartTime == null)
            StartTime = string.Empty;
        return this.SessionsAdapter.UpdateSession(StartTime, AM, Guess, SessionsID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool Update(string StartTime, bool AM, bool Guess, int SessionsID, bool PrelimFinal)
    {
        if (StartTime == null)
            StartTime = string.Empty;
        return this.SessionsAdapter.UpdateSessionWithPrelimFinal(StartTime, AM, Guess, PrelimFinal, SessionsID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool Update(string StartTime, bool AM, bool Guess, int SessionsID, bool PrelimFinal, int MaxInd)
    {
        if (StartTime == null)
            StartTime = string.Empty;

        return this.SessionsAdapter.UpdateWithMoreValues(StartTime, AM, Guess, MaxInd, SessionsID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SessionV2DataTable GetSessionsForMonth(DateTime Month)
    {
        DateTime StartDate = new DateTime(Month.Year, Month.Month, 1, 0, 0, 0);
        DateTime EndDate = new DateTime(Month.Year, Month.Month, DateTime.DaysInMonth(StartDate.Year, StartDate.Month), 0, 0, 0);
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetMeetsBetweenTwoDates(StartDate, EndDate);
        SwimTeamDatabase.SessionV2DataTable ReturnTable = new SwimTeamDatabase.SessionV2DataTable();
        foreach (SwimTeamDatabase.MeetsV2Row Meet in Meets)
        {
            SwimTeamDatabase.SessionV2DataTable Sessions = this.SessionsAdapter.GetSessionsByMeetID(Meet.Meet);
            foreach (SwimTeamDatabase.SessionV2Row Session in Sessions)
            {
                DateTime SessionDate = Meet.Start.AddDays(Session.Day - 1);
                if (SessionDate <= EndDate)
                {
                    SwimTeamDatabase.SessionV2Row NewRow = ReturnTable.NewSessionV2Row();
                    NewRow.ItemArray = Session.ItemArray;
                    ReturnTable.AddSessionV2Row(NewRow);
                }
            }
        }

        ReturnTable.AcceptChanges();

        return ReturnTable;
    }
}