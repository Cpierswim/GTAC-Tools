using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_CreateEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            for (int i = 0; i < 60; i++)
            {
                ListItem li = new ListItem();
                String liText = "";
                if (i == 0)
                    liText = "00";
                else if (i < 10)
                    liText = "0" + i.ToString();
                else
                    liText = i.ToString();

                li.Text = liText;
                li.Value = i.ToString();

                StartMinuteDropDownList.Items.Add(li);
                EndMinuteDropDownList.Items.Add(li);
            }
        }
    }
    protected void ListBoxDatabound(object sender, EventArgs e)
    {
        ListBox lb = (ListBox)sender;

        lb.Rows = lb.Items.Count;
    }
    protected void CreateEvents(object sender, EventArgs e)
    {
        try
        {
            String ErrorString = "";
            if (String.IsNullOrEmpty(EventNameTextBox.Text))
                ErrorString += "<br />No name given to Event.";
            if (GroupsListBox.SelectedItem == null)
                ErrorString += "<br />No group assigned to Event.";
            if (StartDateCalendar.SelectedDate == DateTime.MinValue)
                ErrorString += "<br />No start date selected.";
            if (EndDateCalendar.SelectedDate == DateTime.MinValue)
                ErrorString += "<br />No end date selected.";
            bool DaySelected = false;
            for (int i = 0; i < DaysCheckBoxList.Items.Count; i++)
                if (DaysCheckBoxList.Items[i].Selected)
                    DaySelected = true;
            if (!DaySelected)
                ErrorString += "<br />No day of the week selected.";

            ErrorPanel.Visible = false;

            if (!String.IsNullOrEmpty(ErrorString))
                throw new HelperException(ErrorString);

            String NameForAllEvents = EventNameTextBox.Text;
            DateTime StartDate = StartDateCalendar.SelectedDate;
            DateTime EndDate = EndDateCalendar.SelectedDate;
            int starthour = int.Parse(StartHourDropDownList.SelectedValue);
            if (StartAMPMDropDownList.SelectedValue == "PM")
                if (starthour != 12)
                    starthour += 12;
            int startminute = int.Parse(StartMinuteDropDownList.SelectedValue);
            int endhour = int.Parse(EndHourDropDownList.SelectedValue);
            if (EndAMPMDropDownList.SelectedValue == "PM")
                if (endhour != 12)
                    endhour += 12;
            int endminute = int.Parse(EndMinuteDropDownList.SelectedValue);
            List<int> GroupIDs = new List<int>();
            for (int i = 0; i < GroupsListBox.Items.Count; i++)
                if (GroupsListBox.Items[i].Selected)
                    GroupIDs.Add(int.Parse(GroupsListBox.Items[i].Value));
            bool Sundays = DaysCheckBoxList.Items[0].Selected;
            bool Mondays = DaysCheckBoxList.Items[1].Selected;
            bool Tuesdays = DaysCheckBoxList.Items[2].Selected;
            bool Wednesdays = DaysCheckBoxList.Items[3].Selected;
            bool Thursdays = DaysCheckBoxList.Items[4].Selected;
            bool Fridays = DaysCheckBoxList.Items[5].Selected;
            bool Saturdays = DaysCheckBoxList.Items[6].Selected;

            List<EventHelper> Events = EventHelper.GetEventsBasedOffValues(
                NameForAllEvents, StartDate, EndDate, starthour, startminute, endhour, endminute,
                GroupIDs, Sundays, Mondays, Tuesdays,
                Wednesdays, Thursdays, Fridays, Saturdays);

            EventsBLL EventsAdapter = new EventsBLL();
            ResultLabel.Text = "<br /><br />";
            foreach (EventHelper Event in Events)
            {
                EventsAdapter.CreateEvent(Event.Name, Event.StartDateandTime, Event.GroupID, Event.EndDateandTime);
                ResultLabel.Text += "Event Created: " + Event.Name + " Time: " + Event.StartDateandTime.ToString("d") +
                    " For GroupID: " + Event.GroupID + "<br />";
            }
            if (Events.Count != 0)
                ResultLabel.Visible = true;
        }
        catch (HelperException ex)
        {
            Label ErrorLabel = (Label)ErrorPanel.FindControl("ErrorLabel");
            ErrorLabel.Text = ex.ErrorMessage;

            ErrorPanel.Visible = true;
            ErrorLabel.Visible = true;
        }
    }


    private class EventHelper
    {
        private String _name;
        private int _groupID;
        private DateTime _StartDateandTime;
        private DateTime _EndDateandTime;

        public String Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public int GroupID
        {
            get { return this._groupID; }
            set { this._groupID = value; }
        }
        public DateTime StartDateandTime
        {
            get { return this._StartDateandTime; }
            set { this._StartDateandTime = value; }
        }
        public DateTime EndDateandTime
        {
            get { return this._EndDateandTime; }
            set { this._EndDateandTime = value; }
        }

        public static List<EventHelper> GetEventsBasedOffValues(String NameForAllEvents,
            DateTime StartDate, DateTime EndDate, int StartHour, int StartMinute, int EndHour, int EndMinute,
            List<int> GroupIDs, bool Sundays,
            bool Mondays, bool Tuesdays, bool Wednesdays, bool Thursdays, bool Fridays,
            bool Saturdays)
        {
            List<EventHelper> EventList = new List<EventHelper>();

            for (int i = 0; i < GroupIDs.Count; i++)
            {
                int Group = GroupIDs[i];

                DateTime WorkingOnDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour,
                    StartMinute, 0);

                EndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, 23, 59, 59);

                while (WorkingOnDate <= EndDate)
                {
                    bool CreateEventOnThisDayOfWeek = false;
                    if (WorkingOnDate.DayOfWeek == DayOfWeek.Sunday && Sundays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Monday && Mondays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Tuesday && Tuesdays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Wednesday && Wednesdays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Thursday && Thursdays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Friday && Fridays)
                        CreateEventOnThisDayOfWeek = true;
                    else if (WorkingOnDate.DayOfWeek == DayOfWeek.Saturday && Saturdays)
                        CreateEventOnThisDayOfWeek = true;
                    if (CreateEventOnThisDayOfWeek)
                    {
                        EventHelper Event = new EventHelper();
                        Event.Name = NameForAllEvents;
                        Event.StartDateandTime = WorkingOnDate;
                        DateTime EndDateandTime =
                            new DateTime(WorkingOnDate.Year, WorkingOnDate.Month, WorkingOnDate.Day,
                                EndHour, EndMinute, 0);
                        if (EndHour < StartHour)
                            EndDateandTime = EndDateandTime.AddDays(1.0);
                        Event.EndDateandTime = EndDateandTime;
                        Event.GroupID = Group;

                        EventList.Add(Event);
                    }

                    WorkingOnDate = WorkingOnDate.AddDays(1.0);
                }
            }
            return EventList;
        }

        public override string ToString()
        {
            return this.Name + " " + StartDateandTime.ToShortDateString() + " " + StartDateandTime.ToShortTimeString();
        }
    }

    private class HelperException : Exception
    {
        private String _errorMessage;
        public String ErrorMessage
        {
            get { return this._errorMessage; }
            set { this._errorMessage = value; }
        }

        public HelperException(String ErrorMessage)
        {
            this._errorMessage = ErrorMessage;
        }
    }
}