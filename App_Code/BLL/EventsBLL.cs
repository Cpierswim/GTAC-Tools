using System;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;
using System.Collections.Generic;

/// <summary>
/// Summary description for EventsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class EventsBLL
{
    private EventsTableAdapter _eventsAdapter = null;
    protected EventsTableAdapter Adapter
    {
        get
        {
            if (this._eventsAdapter == null)
                this._eventsAdapter = new EventsTableAdapter();
            return this._eventsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EventsDataTable GetAllEvents()
    {
        return Adapter.GetEvents();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.EventsDataTable GetEventsBetweenTwoDatesInclusive(DateTime StartTime, DateTime EndTime)
    {
        DateTime StartTimeBeginningOfDay = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day, 0, 0, 0);
        DateTime EndTimeEndOfDay = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day, 23, 59, 59);

        return Adapter.GetEventsBetweenDatesInclusive(StartTimeBeginningOfDay, EndTimeEndOfDay);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EventsDataTable GetEventsByGroupID(int GroupID)
    {
        return Adapter.GetEventsByGroupID(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool CreateEvent(String Name, DateTime DateandTime, int GroupID, DateTime EndTime)
    {
        return Adapter.InsertEvent(Name, DateandTime, GroupID, EndTime) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteEvent(int EventID)
    {
        return Adapter.DeleteEvent(EventID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateEvent(int EventID, String Name, DateTime DateandTime, int GroupID, DateTime EndTime)
    {
        return Adapter.UpdateEvent(Name, DateandTime, GroupID, EndTime, EventID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.EventsDataTable GetEvents(DateTime StartDate, DateTime EndDate, int StartHour, int StartMinute, int EndHour, int EndMinute, 
        List<int> GroupIDs, List<DayOfWeek> DaysOfWeek)
    {
        StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 0, 0, 0);
        EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, 23, 59, 59);
        SwimTeamDatabase.EventsDataTable Events = this.Adapter.GetEventsBetweenDatesInclusive(StartDate, EndDate);

        SwimTeamDatabase.EventsDataTable ReturnEvents = new SwimTeamDatabase.EventsDataTable();

        foreach (SwimTeamDatabase.EventsRow Event in Events)
        {
            SwimTeamDatabase.EventsRow Temp = ReturnEvents.NewEventsRow();
            bool Copy = false;
            if (Event.DateandTime.Hour >= StartHour && Event.DateandTime.Hour <= EndHour)
            {
                if (Event.DateandTime.Hour == StartHour && Event.DateandTime.Minute >= StartMinute)
                    Copy = true;
                else if (Event.DateandTime.Hour == EndHour && Event.DateandTime.Minute <= EndMinute)
                    Copy = true;
                else
                    Copy = true;
                if (Copy)
                    Copy = GroupIDs.Contains(Event.GroupID);
                if (Copy)
                    Copy = DaysOfWeek.Contains(Event.DateandTime.DayOfWeek);
                
            }
            if (Copy)
            {
                Temp.ItemArray = Event.ItemArray;
                ReturnEvents.AddEventsRow(Temp);
            }
        }

        ReturnEvents.AcceptChanges();

        return ReturnEvents;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int BatchUpdate(SwimTeamDatabase.EventsDataTable EventsTable)
    {
        return this.Adapter.BatchUpdateIgnoreDBConcurrency(EventsTable, EventsTable.Count);
    }
}