using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SwimTeamDatabaseTableAdapters;
using System.Collections.Generic;

/// <summary>
/// Summary description for SwimmersInMeetBLL
/// </summary>
[System.ComponentModel.DataObject]
public class SwimmersInMeetBLL
{
    public enum ReturnType { Active, Inactive, Both };

    private SwimmersInMeetTableAdapter _swimmersInMeetAdapter = null;
    protected SwimmersInMeetTableAdapter Adapter
    {
        get
        {
            if (_swimmersInMeetAdapter == null)
                _swimmersInMeetAdapter = new SwimmersInMeetTableAdapter();

            return _swimmersInMeetAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersInMeetDataTable GetSwimmersInMeet(int MeetID)
    {
        SwimTeamDatabase.SwimmersInMeetDataTable SwimmersIDs = Adapter.GetSwimmersInMeet(MeetID);
        EntriesTableAdapter EntriesAdapter = new EntriesTableAdapter();
        SwimTeamDatabase.EntriesDataTable Entries = EntriesAdapter.GetEntriesByMeet(MeetID);
        SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmers();

        SwimTeamDatabase.SwimmersInMeetDataTable ReturnTable = new SwimTeamDatabase.SwimmersInMeetDataTable();
        foreach (SwimTeamDatabase.SwimmersInMeetRow SwimmerID in SwimmersIDs)
        {
            String lastname = "", firstname = "", middlename = "";
            for(int i = 0; i < Swimmers.Count; i++)
                if (Swimmers[i].USAID == SwimmerID.USAID)
                {
                    lastname = Swimmers[i].LastName;
                    firstname = Swimmers[i].FirstName;
                    middlename = Swimmers[i].MiddleName;
                    i = Swimmers.Count;
                }

            SwimTeamDatabase.SwimmersInMeetDataTable TempTable = new SwimTeamDatabase.SwimmersInMeetDataTable();
            foreach (SwimTeamDatabase.SwimmersInMeetRow Swimmer in ReturnTable)
            {
                SwimTeamDatabase.SwimmersInMeetRow TempRow = TempTable.NewSwimmersInMeetRow();
                TempRow.USAID = Swimmer.USAID;
                TempTable.AddSwimmersInMeetRow(TempRow);
            }
            ReturnTable = new SwimTeamDatabase.SwimmersInMeetDataTable();

            bool NamePlaced = false;
            if (TempTable.Count == 0)
            {
                ReturnTable.AddSwimmersInMeetRow(SwimmerID.USAID);
            }
            else
            {
                foreach (SwimTeamDatabase.SwimmersInMeetRow Swimmer in TempTable)
                {
                    //go through temp table
                    //if row in temp table comes alphabetically after compare names, add
                    //the compare name, then the temp table row
                    //otherwise just add the temp table row

                    String Templastname = "", Tempfirstname = "", Tempmiddlename = "";
                    for (int i = 0; i < Swimmers.Count; i++)
                        if (Swimmers[i].USAID == Swimmer.USAID)
                        {
                            Templastname = Swimmers[i].LastName;
                            Tempfirstname = Swimmers[i].FirstName;
                            Tempmiddlename = Swimmers[i].MiddleName;
                            i = Swimmers.Count;
                        }

                    bool AlphabeticalResult = NameHelper.IsSecondNameAlphabeticallyAfterFirstName(
                        lastname, firstname, middlename,
                        Templastname, Tempfirstname, Tempmiddlename);
                    if (AlphabeticalResult && !NamePlaced)
                    {
                        ReturnTable.AddSwimmersInMeetRow(SwimmerID.USAID);
                        ReturnTable.AddSwimmersInMeetRow(Swimmer.USAID);
                        NamePlaced = true;
                    }
                    else
                        ReturnTable.AddSwimmersInMeetRow(Swimmer.USAID);
                }

                if(ReturnTable.Count != (TempTable.Count +1))
                    ReturnTable.AddSwimmersInMeetRow(SwimmerID.USAID);
            }     
        }
        //once the whole thing is sorted alphabetically
        //resort by in database

        //ResultTable is now Alphabatized
        SwimTeamDatabase.SwimmersInMeetDataTable AllEntriesinDatabaseTable = new SwimTeamDatabase.SwimmersInMeetDataTable();
        SwimTeamDatabase.SwimmersInMeetDataTable NotAllEntriesinDatabaseTable = new SwimTeamDatabase.SwimmersInMeetDataTable();
        foreach (SwimTeamDatabase.SwimmersInMeetRow Swimmer in ReturnTable)
        {
            List<SwimTeamDatabase.EntriesRow> EntriesList = new List<SwimTeamDatabase.EntriesRow>();

            for (int i = 0; i < Entries.Count; i++)
                if (Entries[i].USAID == Swimmer.USAID)
                    EntriesList.Add(Entries[i]);
            
            bool AllEntriesInDatabase = false;
            int NumberOfEntries = 0;
            foreach (SwimTeamDatabase.EntriesRow Entry in EntriesList)
                if (Entry.InDatabase == true)
                    NumberOfEntries++;
            if (NumberOfEntries == EntriesList.Count)
                AllEntriesInDatabase = true;

            if (AllEntriesInDatabase)
                AllEntriesinDatabaseTable.AddSwimmersInMeetRow(Swimmer.USAID);
            else
                NotAllEntriesinDatabaseTable.AddSwimmersInMeetRow(Swimmer.USAID);
        }

        ReturnTable = new SwimTeamDatabase.SwimmersInMeetDataTable();
        foreach (SwimTeamDatabase.SwimmersInMeetRow Swimmer in NotAllEntriesinDatabaseTable)
            ReturnTable.AddSwimmersInMeetRow(Swimmer.USAID);
        foreach (SwimTeamDatabase.SwimmersInMeetRow Swimmer in AllEntriesinDatabaseTable)
            ReturnTable.AddSwimmersInMeetRow(Swimmer.USAID);
        

        return ReturnTable;
    }

    
}