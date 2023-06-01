using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for MeetEventsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class MeetEventsBLL
{
    private MeetEventsTableAdapter _EventsAdapter;
    private MeetEventsTableAdapter EventsAdapter
    {
        get
        {
            if (this._EventsAdapter == null)
                this._EventsAdapter = new MeetEventsTableAdapter();
            return this._EventsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetEventsDataTable GetAllMeetEvents()
    {
        return this.EventsAdapter.GetAllEvents();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetEventsDataTable GetByMeetIDAndSessionNumber(int MeetID, int SessionNumber)
    {
        return this.EventsAdapter.GetByMeetIDAndSessionNumber(MeetID, SessionNumber);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetEventsDataTable GetEventByMeetID(int MeetID)
    {
        return this.EventsAdapter.GetEventByMeetID(MeetID);
    }
}