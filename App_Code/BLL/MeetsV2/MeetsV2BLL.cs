using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for MeetsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class MeetsV2BLL
{
    private MeetsV2TableAdapter _MeetsAdapter;
    public MeetsV2TableAdapter MeetsAdapter
    {
        get
        {
            if (this._MeetsAdapter == null)
                this._MeetsAdapter = new MeetsV2TableAdapter();
            return this._MeetsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetAllMeets()
    {
        return this.MeetsAdapter.GetAllMeets();
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetMeetsOpenForEntry()
    {
        DateTime Now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time");
        DateTime Time = new DateTime(Now.Year, Now.Month, Now.Day);
        return this.MeetsAdapter.GetOpenForEntries(Time);
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2Row GetMeetByMeetID(int MeetID)
    {
        SwimTeamDatabase.MeetsV2DataTable Meets = this.MeetsAdapter.GetMeetByMeetID(MeetID);
        if (Meets.Count == 0)
            return null;
        else
            return Meets[0];
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateRow(string MeetName, string Location, string Remarks, string Instructions, 
        System.DateTime Deadline, bool OpenForCoaches, System.DateTime LateEntryDeadline, String CoachNotes,
        int Meet) 
    {
        if (MeetName == null)
            MeetName = String.Empty;
        if (Location == null)
            Location = String.Empty;
        if (Remarks == null)
            Remarks = String.Empty;
        if (Instructions == null)
            Instructions = String.Empty;
        Deadline = new DateTime(Deadline.Year, Deadline.Month, Deadline.Day, 11, 59, 59);
        LateEntryDeadline = new DateTime(LateEntryDeadline.Year, LateEntryDeadline.Month, LateEntryDeadline.Day, 11, 59, 59);
        return this.MeetsAdapter.UpdateRow(MeetName, Location, Remarks, Instructions, Deadline,
            OpenForCoaches, LateEntryDeadline, CoachNotes, Meet) == 1;
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetMeetsPastDeadlineButOpenForLateEntry()
    {
        DateTime NewDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time");
        NewDate.AddDays(1);
        NewDate = new DateTime(NewDate.Year, NewDate.Month, NewDate.Day);
        return this.MeetsAdapter.GetByPastDeadlineOpenForLateEntry(NewDate);
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetMeetsBetweenTwoDates(DateTime StartDate, DateTime EndDate)
    {
        return this.MeetsAdapter.GetMeetsBetweenTwoDates(StartDate, EndDate);
    }

    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetMeetsToOpenForCoachces()
    {
        DateTime NewDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time");
        NewDate.AddDays(1);
        NewDate = new DateTime(NewDate.Year, NewDate.Month, NewDate.Day, 0, 0, 0);
        return this.MeetsAdapter.GetMeetsToOpenForCoaches(NewDate);
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int Update(SwimTeamDatabase.MeetsV2Row Meet)
    {
        Meet.Deadline = new DateTime(Meet.Deadline.Year, Meet.Deadline.Month, Meet.Deadline.Day, 11, 59, 59);
        Meet.LateEntryDeadline = new DateTime(Meet.LateEntryDeadline.Year, Meet.LateEntryDeadline.Month, Meet.LateEntryDeadline.Day, 11, 59, 59);
        return this.MeetsAdapter.Update(Meet);
    }
    [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetsV2DataTable GetMeetsThatEndAfterToday()
    {
        DateTime Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
        Date = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59);
        return this.MeetsAdapter.GetByMeetsThatEndAfterDate(Date);
    }
}
