using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for PreEnteredV2
/// </summary>
[System.ComponentModel.DataObject]
public class PreEnteredV2BLL
{
    private PreEnteredV2TableAdapter _PreEnteredAdapter;
    private PreEnteredV2TableAdapter PreEnteredAdapter
    {
        get
        {
            if (this._PreEnteredAdapter == null)
                this._PreEnteredAdapter = new PreEnteredV2TableAdapter();
            return this._PreEnteredAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PreEnteredV2DataTable GetAllPreEntries()
    {
        return this.PreEnteredAdapter.GetAllPreEntries();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PreEnteredV2DataTable GetPreEntriesByMeetAndSwimmer(String USAID, int MeetID)
    {
        return this.PreEnteredAdapter.GetPreEnteredByMeetAndSwimmer(USAID, MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool RemoveFromSession(String USAID, int MeetID, int SessionNumber, SwimTeamDatabase.PreEnteredV2Row PreEnteredRow,
        RemoveFromSessionOptions Options)
    {
        //Need to also remove entries from this session

        bool RowChanged = false;

        if (PreEnteredRow.IsPreEnteredInSession(SessionNumber))
        {
            if (PreEnteredRow.IsIndividualSessionsDeclared())
            {
                if (!PreEnteredRow.IsSession1Null())
                    if (PreEnteredRow.Session1 == SessionNumber)
                    {
                        PreEnteredRow.SetSession1Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession2Null())
                    if (PreEnteredRow.Session2 == SessionNumber)
                    {
                        PreEnteredRow.SetSession2Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession3Null())
                    if (PreEnteredRow.Session3 == SessionNumber)
                    {
                        PreEnteredRow.SetSession3Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession4Null())
                    if (PreEnteredRow.Session4 == SessionNumber)
                    {
                        PreEnteredRow.SetSession4Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession5Null())
                    if (PreEnteredRow.Session5 == SessionNumber)
                    {
                        PreEnteredRow.SetSession5Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession6Null())
                    if (PreEnteredRow.Session6 == SessionNumber)
                    {
                        PreEnteredRow.SetSession6Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession7Null())
                    if (PreEnteredRow.Session7 == SessionNumber)
                    {
                        PreEnteredRow.SetSession7Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession8Null())
                    if (PreEnteredRow.Session8 == SessionNumber)
                    {
                        PreEnteredRow.SetSession8Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession9Null())
                    if (PreEnteredRow.Session9 == SessionNumber)
                    {
                        PreEnteredRow.SetSession9Null();
                        RowChanged = true;
                    }
                if (!PreEnteredRow.IsSession10Null())
                    if (PreEnteredRow.Session10 == SessionNumber)
                    {
                        PreEnteredRow.SetSession10Null();
                        RowChanged = true;
                    }

                if (!PreEnteredRow.IsIndividualSessionsDeclared())
                {
                    if (Options == RemoveFromSessionOptions.DeleteBlankDeclaredEntry)
                        PreEnteredRow.Delete();
                    else
                    {
                        PreEnteredRow.PreEntered = false;
                        RowChanged = true;
                    }
                }
            }
            else
            {
                SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
                SwimTeamDatabase.SessionV2DataTable Sessions = SessionsAdapter.GetSessionsByMeetID(MeetID);
                int SessionWorkingOn = 0;
                for (int i = 0; i < Sessions.Count; i++)
                {
                    if (Sessions[i].Session != SessionNumber)
                    {
                        SessionWorkingOn++;
                        PreEnteredRow.Session1 = Sessions[i].Session;
                        RowChanged = true;
                    }
                }

                if (RowChanged)
                {
                    PreEnteredRow.IsInDatabase = false;
                    if (!PreEnteredRow.IsIndividualSessionsDeclared())
                    {
                        if (Options == RemoveFromSessionOptions.DeleteBlankDeclaredEntry)
                            PreEnteredRow.Delete();
                        else
                        {
                            PreEnteredRow.PreEntered = false;
                            RowChanged = true;
                        }
                    }
                }
            }
        }

        if (!RowChanged)
            return false;
        {
            if (PreEnteredRow.RowState != System.Data.DataRowState.Deleted)
                PreEnteredRow.IsInDatabase = false;
            //remove the swimmers events for the session
            MeetEventsBLL EventsAdapter = new MeetEventsBLL();

            SwimTeamDatabase.MeetEventsDataTable Events = EventsAdapter.GetByMeetIDAndSessionNumber(MeetID, SessionNumber);

            EntriesV2BLL EntriesAdapter = new EntriesV2BLL();
            SwimTeamDatabase.EntriesV2DataTable Entries = EntriesAdapter.GetEntriesByMeetandSwimmer(MeetID, USAID);

            foreach (SwimTeamDatabase.EntriesV2Row Entry in Entries)
            {
                bool EntryInSession = false;
                for (int i = 0; i < Events.Count; i++)
                    if (Events[i].EventID == Entry.EventID)
                    {
                        EntryInSession = true;
                        break;
                    }
                if (EntryInSession)
                    Entry.Delete();
            }

            int EntriesDeleted = EntriesAdapter.BatchUpdate(Entries);
        }

        int RowsAffected = this.PreEnteredAdapter.Update(PreEnteredRow);
        return RowsAffected == 1;
    }

    public bool RemoveFromSession(String USAID, int MeetID, int SessionNumber, RemoveFromSessionOptions Options)
    {
        SwimTeamDatabase.PreEnteredV2DataTable PreEntries = this.GetPreEntriesByMeetAndSwimmer(USAID, MeetID);
        if (PreEntries.Count != 1)
            return false;
        return this.RemoveFromSession(USAID, MeetID, SessionNumber, PreEntries[0], Options);
    }

    public enum RemoveFromSessionOptions { BlankDeclaredSessionsToGeneralPreEntry, DeleteBlankDeclaredEntry };

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool AddToSession(int SessionNumber, SwimTeamDatabase.PreEnteredV2Row PreEnteredRow)
    {
        if (!PreEnteredRow.PreEntered)
            PreEnteredRow.PreEntered = true;
        if (PreEnteredRow.IsSession1Null())
            PreEnteredRow.Session1 = SessionNumber;
        else if (PreEnteredRow.IsSession2Null())
            PreEnteredRow.Session2 = SessionNumber;
        else if (PreEnteredRow.IsSession3Null())
            PreEnteredRow.Session3 = SessionNumber;
        else if (PreEnteredRow.IsSession4Null())
            PreEnteredRow.Session4 = SessionNumber;
        else if (PreEnteredRow.IsSession5Null())
            PreEnteredRow.Session5 = SessionNumber;
        else if (PreEnteredRow.IsSession6Null())
            PreEnteredRow.Session6 = SessionNumber;
        else if (PreEnteredRow.IsSession7Null())
            PreEnteredRow.Session7 = SessionNumber;
        else if (PreEnteredRow.IsSession8Null())
            PreEnteredRow.Session8 = SessionNumber;
        else if (PreEnteredRow.IsSession9Null())
            PreEnteredRow.Session9 = SessionNumber;
        else if (PreEnteredRow.IsSession10Null())
            PreEnteredRow.Session10 = SessionNumber;
        else
            throw new Exception("Already Pre-Entered in 10 Sessions.");

        PreEnteredRow.IsInDatabase = false;

        return this.PreEnteredAdapter.Update(PreEnteredRow) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int AddToSessions(List<int> SessionNumbers, SwimTeamDatabase.PreEnteredV2Row PreEnteredRow)
    {
        for (int i = 0; i < SessionNumbers.Count; i++)
        {
            if (!PreEnteredRow.PreEntered)
                PreEnteredRow.PreEntered = true;
            if (PreEnteredRow.IsSession1Null())
                PreEnteredRow.Session1 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession2Null())
                PreEnteredRow.Session2 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession3Null())
                PreEnteredRow.Session3 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession4Null())
                PreEnteredRow.Session4 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession5Null())
                PreEnteredRow.Session5 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession6Null())
                PreEnteredRow.Session6 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession7Null())
                PreEnteredRow.Session7 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession8Null())
                PreEnteredRow.Session8 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession9Null())
                PreEnteredRow.Session9 = SessionNumbers[i];
            else if (PreEnteredRow.IsSession10Null())
                PreEnteredRow.Session10 = SessionNumbers[i];
            else
                throw new Exception("Already Pre-Entered in 10 Sessions.");
        }

        PreEnteredRow.IsInDatabase = false;

        if (this.PreEnteredAdapter.Update(PreEnteredRow) == 1)
            return SessionNumbers.Count;
        else
            return -1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int CreatePreEntryForSession(List<int> SessionsToAddTo, String USAID, int MeetID)
    {
        if (SessionsToAddTo.Count > 0)
        {
            SwimTeamDatabase.PreEnteredV2Row NewRow = new SwimTeamDatabase.PreEnteredV2DataTable().NewPreEnteredV2Row();

            int? Session1 = null, Session2 = null, Session3 = null, Session4 = null, Session5 = null, Session6 = null, Session7 = null,
                Session8 = null, Session9 = null, Session10 = null;

            for (int i = 0; i < SessionsToAddTo.Count; i++)
            {
                if (!Session1.HasValue)
                    Session1 = SessionsToAddTo[i];
                else if (!Session2.HasValue)
                    Session2 = SessionsToAddTo[i];
                else if (!Session3.HasValue)
                    Session3 = SessionsToAddTo[i];
                else if (!Session4.HasValue)
                    Session4 = SessionsToAddTo[i];
                else if (!Session5.HasValue)
                    Session5 = SessionsToAddTo[i];
                else if (!Session6.HasValue)
                    Session6 = SessionsToAddTo[i];
                else if (!Session7.HasValue)
                    Session7 = SessionsToAddTo[i];
                else if (!Session8.HasValue)
                    Session8 = SessionsToAddTo[i];
                else if (!Session9.HasValue)
                    Session9 = SessionsToAddTo[i];
                else if (!Session10.HasValue)
                    Session10 = SessionsToAddTo[i];
                else
                    throw new Exception("Already Pre-Entered in 10 Sessions.");
            }


            bool PreEntered = (Session1.HasValue || Session2.HasValue || Session3.HasValue || Session4.HasValue ||
                Session5.HasValue || Session6.HasValue || Session7.HasValue || Session8.HasValue || Session9.HasValue ||
                Session10.HasValue);

            int AthleteID = new SwimmerAthleteJoinBLL().GetAtheleIDFromDatabase(USAID);

            int rowsAffected = this.PreEnteredAdapter.Insert(USAID, MeetID, PreEntered, Session1,
                Session2, Session3, Session4, Session5, Session6, Session7,
                Session8, Session9, Session10, false, AthleteID, false);
            if (rowsAffected == 1)
                return SessionsToAddTo.Count;
            else
                return -1;
        }
        return 0;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PreEnteredV2DataTable GetPreEntriesByMeetID(int MeetID)
    {
        return this.PreEnteredAdapter.GetPreEnteredByMeetID(MeetID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetAsInDatabase(int PreEntryID)
    {
        return this.PreEnteredAdapter.SetAsInDatabase(PreEntryID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PreEnteredV2DataTable GetPreEntriesByUSAID(String USAID)
    {
        return this.PreEnteredAdapter.GetPreEntryByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PreEnteredV2DataTable GetPreEntriesByGroupID(int GroupID)
    {
        return this.PreEnteredAdapter.GetPreEnteredByGroupID(GroupID);
    }
}