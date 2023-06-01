using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Calendar_EventsCalendar : System.Web.UI.UserControl
{
    private enum Month
    {
        January = 1, February, March, April, May, June, July, August, September,
        October, November, December
    }

    public DateTime SetupTime
    {
        get
        {
            object o = ViewState["SetupTime"];
            if (o == null)
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            else
                return (DateTime)o;
        }
        set
        {
            ViewState["SetupTime"] = value;
        }
    }

    private List<int> _groupIDList;
    private List<int> Private_GroupIDList
    {
        get
        {
            if (_groupIDList == null)
            {
                object o = ViewState["GroupIDList"];
                if (o == null)
                    _groupIDList = new List<int>();
                else
                    _groupIDList = (List<int>)o;
            }
            return _groupIDList;
        }
        set
        {
            this._groupIDList = value;
            ViewState["GroupIDList"] = value;
        }
    }

    public List<int> GroupIDList
    {
        get
        {
            if (_groupIDList == null)
            {
                object o = ViewState["GroupIDList"];
                if (o == null)
                    _groupIDList = new List<int>();
                else
                    _groupIDList = (List<int>)o;
            }
            return _groupIDList;
        }
    }

    private List<String> _USAIDList;
    private List<String> Private_USAIDList
    {
        get
        {
            if (_USAIDList == null)
            {
                object o = ViewState["USAIDList"];
                if (o == null)
                    _USAIDList = new List<String>();
                else
                    _USAIDList = (List<String>)o;
            }
            return _USAIDList;
        }
        set
        {
            this._USAIDList = value;
            ViewState["USAIDList"] = value;
        }
    }

    public List<String> USAIDList
    {
        get
        {
            if (_USAIDList == null)
            {
                object o = ViewState["USAIDList"];
                if (o == null)
                    _USAIDList = new List<String>();
                else
                    _USAIDList = (List<String>)o;
            }
            return _USAIDList;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CreateCalendarAndBorders();
        PopulateCalendarWithEvents();
        if (Private_USAIDList != null)
            if (Private_USAIDList.Count != 0)
                PopulateWithMeetsV2();
    }

    private void CreateCalendarAndBorders()
    {
        String MonthInQuery = Request.QueryString["Month"];
        if (!String.IsNullOrEmpty(MonthInQuery))
            this.SetupTime = DateTime.Parse(MonthInQuery);

        DateTime StartDate = new DateTime(SetupTime.Year, SetupTime.Month, 1);
        int DaysInMonth = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);


        int WorkingOnDay = 1;

        switch (StartDate.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                WorkingOnDay -= 6;
                break;
            case DayOfWeek.Friday:
                WorkingOnDay -= 5;
                break;
            case DayOfWeek.Thursday:
                WorkingOnDay -= 4;
                break;
            case DayOfWeek.Wednesday:
                WorkingOnDay -= 3;
                break;
            case DayOfWeek.Tuesday:
                WorkingOnDay -= 2;
                break;
            case DayOfWeek.Monday:
                WorkingOnDay -= 1;
                break;
        }

        int NumberOfWeeks = 0;
        bool continueloop = true;
        while (continueloop)
        {
            int SundayofWeek = WorkingOnDay;
            int SaturdayofWeek = SundayofWeek + 6;
            if (SundayofWeek > DaysInMonth)
                continueloop = false;
            else
                NumberOfWeeks++;
            WorkingOnDay += 7;
        }


        List<TableRow> Weeks = new List<TableRow>();

        for (int i = 0; i < NumberOfWeeks; i++)
        {
            TableRow WeekRow = new TableRow();

            for (int j = 0; j < 7; j++)
            {
                TableCell DayCell = new TableCell();
                WeekRow.Cells.Add(DayCell);
            }

            CalendarTable.Rows.Add(WeekRow);
        }

        WorkingOnDay = 1;
        DateTime FirstDayOfMonth = new DateTime(StartDate.Year, StartDate.Month, 1);

        switch (FirstDayOfMonth.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                WorkingOnDay -= 6;
                break;
            case DayOfWeek.Friday:
                WorkingOnDay -= 5;
                break;
            case DayOfWeek.Thursday:
                WorkingOnDay -= 4;
                break;
            case DayOfWeek.Wednesday:
                WorkingOnDay -= 3;
                break;
            case DayOfWeek.Tuesday:
                WorkingOnDay -= 2;
                break;
            case DayOfWeek.Monday:
                WorkingOnDay -= 1;
                break;
        }

        for (int i = 0; i < CalendarTable.Rows.Count; i++)
            for (int j = 0; j < CalendarTable.Rows[i].Cells.Count; j++)
            {
                if ((WorkingOnDay > 0) && (WorkingOnDay <= DaysInMonth))
                {
                    TableCell Cell = CalendarTable.Rows[i].Cells[j];
                    Cell.ID = "CellDate" + WorkingOnDay;
                    Label DateLabel = new Label();
                    DateLabel.ID = "DateLabel_Date" + WorkingOnDay;
                    DateLabel.Text = "&nbsp;" + WorkingOnDay.ToString();



                    Cell.Controls.Add(DateLabel);
                }
                WorkingOnDay++;
            }

        for (int i = 0; i < CalendarTable.Rows.Count; i++)
            for (int j = 0; j < CalendarTable.Rows[i].Cells.Count; j++)
            {
                TableCell Cell = CalendarTable.Rows[i].Cells[j];
                if (Cell.Controls.Count != 0 || true)
                {
                    if (i == (CalendarTable.Rows.Count - 1))
                    {
                        if (j < (CalendarTable.Rows[i].Cells.Count - 1))
                            Cell.Style.Value = "border-top-style: solid; border-top-width:1px;" +
                                                "border-bottom-style: solid; border-bottom-width:1px;" +
                                                "border-left-style: solid; border-left-width:1px;";
                        else
                            Cell.Style.Value = "border-top-style: solid; border-top-width:1px;" +
                                                "border-bottom-style: solid; border-bottom-width:1px;" +
                                                "border-left-style: solid; border-left-width:1px;" +
                                                "border-right-style:solid; border-right-width:1px";
                    }
                    else
                    {
                        if (j < (CalendarTable.Rows[i].Cells.Count - 1))
                            Cell.Style.Value = "border-top-style: solid; border-top-width:1px;" +
                                                "border-left-style: solid; border-left-width:1px;";
                        else
                            Cell.Style.Value = "border-top-style: solid; border-top-width:1px;" +
                                                "border-left-style: solid; border-left-width:1px;" +
                                                "border-right-style:solid; border-right-width:1px";

                    }

                    Cell.VerticalAlign = VerticalAlign.Top;
                }
            }

        CalendarTable.CellSpacing = 0;
        CalendarTable.CellPadding = 0;

        TableRow DateRow = new TableRow();
        for (int i = 0; i < 7; i++)
        {
            TableCell DateCell = new TableCell();
            String DayLabelText = "";
            switch (i)
            {
                case (0):
                    DayLabelText = "Sunday";
                    break;
                case (1):
                    DayLabelText = "Monday";
                    break;
                case (2):
                    DayLabelText = "Tuesday";
                    break;
                case (3):
                    DayLabelText = "Wednesday";
                    break;
                case (4):
                    DayLabelText = "Thursday";
                    break;
                case (5):
                    DayLabelText = "Friday";
                    break;
                case (6):
                    DayLabelText = "Saturday";
                    break;
            }
            Label DayLabel = new Label();
            DayLabel.Text = DayLabelText;
            DateCell.Controls.Add(DayLabel);
            DateCell.Style.Value = "text-align:center;";
            DateRow.Cells.Add(DateCell);
        }
        CalendarTable.Rows.AddAt(0, DateRow);

        TableRow HeaderRow = new TableRow();
        TableCell PreviousMonthCell = new TableCell();
        HyperLink PreviousMonthHyperLink = new HyperLink();
        PreviousMonthHyperLink.Text = "<< Previous Month";
        Uri CurrentPage = Request.Url;
        String FullLinkString = CurrentPage.PathAndQuery;
        String[] URLParts = FullLinkString.Split('/');
        String ASPXPart = "";
        for (int i = 0; i < URLParts.Length; i++)
            if (URLParts[i].Contains(".aspx"))
            {
                ASPXPart = URLParts[i];
                i = URLParts.Length;
            }

        String NewNavigateURL = CurrentPage.GetLeftPart(UriPartial.Path) + "?Month=";
        DateTime PreviousMonth;
        if (SetupTime.Month == 1)
            PreviousMonth = new DateTime(SetupTime.Year - 1, 12, 1);
        else
            PreviousMonth = new DateTime(SetupTime.Year, SetupTime.Month - 1, 1);
        NewNavigateURL += PreviousMonth.ToShortDateString();
        PreviousMonthHyperLink.NavigateUrl = NewNavigateURL;
        PreviousMonthCell.Controls.Add(PreviousMonthHyperLink);
        HeaderRow.Cells.Add(PreviousMonthCell);


        TableCell HeaderCell = new TableCell();
        String LabelText = "";
        int Month = SetupTime.Month;
        switch (Month)
        {
            case (1):
                LabelText = "January";
                break;
            case (2):
                LabelText = "February";
                break;
            case (3):
                LabelText = "March";
                break;
            case (4):
                LabelText = "April";
                break;
            case (5):
                LabelText = "May";
                break;
            case (6):
                LabelText = "June";
                break;
            case (7):
                LabelText = "July";
                break;
            case (8):
                LabelText = "August";
                break;
            case (9):
                LabelText = "September";
                break;
            case (10):
                LabelText = "October";
                break;
            case (11):
                LabelText = "November";
                break;
            case (12):
                LabelText = "December";
                break;
        }

        LabelText += " " + SetupTime.Year;

        Label HeaderLabel = new Label();
        HeaderLabel.ID = "HeaderLabel";
        HeaderLabel.Text = LabelText;

        HeaderCell.Controls.Add(HeaderLabel);
        HeaderCell.Style.Value = "text-align:center; font-size:large; font-weight:bold;";
        HeaderCell.ColumnSpan = CalendarTable.Rows[0].Cells.Count;
        HeaderCell.ColumnSpan = 5;
        HeaderRow.Cells.Add(HeaderCell);

        TableCell NextMonthCell = new TableCell();
        HyperLink NextMonthHyperLink = new HyperLink();
        NextMonthHyperLink.Text = "Next Month>>";
        int NextMonth = 0, NextYear = SetupTime.Year;
        if (SetupTime.Month == 12)
        {
            NextMonth = 1;
            NextYear = SetupTime.Year + 1;
        }
        else
            NextMonth = SetupTime.Month + 1;
        DateTime NextMonthDate = new DateTime(NextYear, NextMonth, 1, 0, 0, 0);
        String NextMonthNavigateURL = CurrentPage.GetLeftPart(UriPartial.Path) + "?Month=" + NextMonthDate.ToShortDateString();
        NextMonthHyperLink.NavigateUrl = NextMonthNavigateURL;
        NextMonthCell.Controls.Add(NextMonthHyperLink);
        NextMonthCell.Style.Value = "text-align:right;";
        HeaderRow.Cells.Add(NextMonthCell);

        CalendarTable.Rows.AddAt(0, HeaderRow);

        double CellWidth = 14.28;

        for (int i = 0; i < CalendarTable.Rows.Count; i++)
            for (int j = 0; j < CalendarTable.Rows[i].Cells.Count; j++)
            {
                TableCell WorkingCell = CalendarTable.Rows[i].Cells[j];
                if (WorkingCell.ColumnSpan == 0)
                    if (j != (CalendarTable.Rows[i].Cells.Count - 1))
                        WorkingCell.Width = new Unit(CellWidth, UnitType.Percentage);
            }
    }
    private void PopulateCalendarWithEvents()
    {
        Label HeaderLabel = (Label)CalendarTable.FindControl("HeaderLabel");
        String HeaderText = HeaderLabel.Text;

        int month = 0;
        if (HeaderText.Contains("January"))
            month = 1;
        else if (HeaderText.Contains("February"))
            month = 2;
        else if (HeaderText.Contains("March"))
            month = 3;
        else if (HeaderText.Contains("April"))
            month = 4;
        else if (HeaderText.Contains("May"))
            month = 5;
        else if (HeaderText.Contains("June"))
            month = 6;
        else if (HeaderText.Contains("July"))
            month = 7;
        else if (HeaderText.Contains("August"))
            month = 8;
        else if (HeaderText.Contains("September"))
            month = 9;
        else if (HeaderText.Contains("October"))
            month = 10;
        else if (HeaderText.Contains("November"))
            month = 11;
        else if (HeaderText.Contains("December"))
            month = 12;

        String substring = HeaderText.Substring(HeaderText.Length - 4, 4);
        int year = int.Parse(substring);
        DateTime StartDate = new DateTime(year, month, 1, 0, 0, 0);

        DateTime EndDate = new DateTime(year, month, DateTime.DaysInMonth(StartDate.Year, StartDate.Month)
                        , 23, 59, 59);

        EventsBLL EventsAdapter = new EventsBLL();
        SwimTeamDatabase.EventsDataTable AllEvents = EventsAdapter.GetEventsBetweenTwoDatesInclusive(
            StartDate, EndDate);
        SwimTeamDatabase.EventsDataTable Events = new SwimTeamDatabase.EventsDataTable();
        for (int i = 0; i < AllEvents.Count; i++)
        {
            SwimTeamDatabase.EventsRow Event = AllEvents[i];
            for (int j = 0; j < this.Private_GroupIDList.Count; j++)
            {
                if (Event.GroupID == this.Private_GroupIDList[j])
                {
                    SwimTeamDatabase.EventsRow NewRow = Events.NewEventsRow();
                    NewRow.DateandTime = Event.DateandTime;
                    NewRow.EndTime = Event.EndTime;
                    NewRow.EventID = Event.EventID;
                    NewRow.GroupID = Event.GroupID;
                    NewRow.Name = Event.Name;
                    Events.AddEventsRow(NewRow);
                    j = this.Private_GroupIDList.Count;
                }
            }
        }

        DateTime WorkingOnDate = StartDate;
        for (int i = 1; i <= DateTime.DaysInMonth(EndDate.Year, EndDate.Month); i++)
        {
            TableCell Cell = (TableCell)CalendarTable.FindControl("CellDate" + i);

            int Day = WorkingOnDate.Day;

            //Already have limited the list of events to only the groups we are concerned with
            //now limit it to only events for the day we are working on
            SwimTeamDatabase.EventsDataTable DaysEvents = new SwimTeamDatabase.EventsDataTable();
            for (int j = 0; j < Events.Count; j++)
            {
                SwimTeamDatabase.EventsRow Event = Events[j];
                if (Event.DateandTime.Day == Day)
                {
                    SwimTeamDatabase.EventsRow NewRow = DaysEvents.NewEventsRow();
                    NewRow.DateandTime = Event.DateandTime;
                    NewRow.EndTime = Event.EndTime;
                    NewRow.EventID = Event.EventID;
                    NewRow.GroupID = Event.GroupID;
                    NewRow.Name = Event.Name;
                    DaysEvents.AddEventsRow(NewRow);
                }
                else if (Event.DateandTime.Day > Day)
                    j = Events.Count;
            }

            if (DaysEvents.Count > 0)
            {
                Label BreakLabel = new Label();
                BreakLabel.ID = "TopBreakLabel_Date" + i;
                BreakLabel.Text = "<br />";
                Cell.Controls.Add(BreakLabel);
                //we now have the cell we are working with and the events that go in that cell. 
                //Time to display them
                for (int j = 0; j < DaysEvents.Count; j++)
                {
                    SwimTeamDatabase.EventsRow Event = DaysEvents[j];

                    Label EventLabel = new Label();
                    EventLabel.ID = "EventLabel_Day" + i + "_Event" + j;
                    int StartHour = Event.DateandTime.Hour;
                    int StartMinute = Event.DateandTime.Minute;
                    bool StartPM = false;
                    if (StartHour > 12)
                    {
                        StartHour -= 12;
                        StartPM = true;
                    }
                    int EndHour = Event.EndTime.Hour;
                    int EndMinute = Event.EndTime.Minute;
                    bool EndPM = false;
                    if (EndHour > 12)
                    {
                        EndHour -= 12;
                        EndPM = true;
                    }

                    String LabelText = "<b>" + Event.Name + "</b>" + "<br />&nbsp;&nbsp;&nbsp;&nbsp;(" + StartHour;
                    if (StartMinute != 0)
                    {
                        if (StartMinute < 10)
                            LabelText += ":0" + StartMinute;
                        else
                            LabelText += ":" + StartMinute;
                    }
                    LabelText += " ";
                    if (StartPM)
                        LabelText += " PM";
                    else
                        LabelText += " AM";

                    LabelText += "-" + EndHour;
                    if (EndMinute != 0)
                    {
                        if (EndMinute < 10)
                            LabelText += ":0" + EndMinute;
                        else
                            LabelText += ":" + EndMinute;
                    }
                    LabelText += " ";
                    if (EndPM)
                        LabelText += " PM";
                    else
                        LabelText += " AM";

                    LabelText += ")";

                    if ((j + 1) < DaysEvents.Count)
                        LabelText += "<br />";

                    EventLabel.Text = LabelText;

                    Cell.Controls.Add(EventLabel);
                }
            }


            WorkingOnDate = WorkingOnDate.AddDays(1.0);
        }
    }
    //private void PopulateWithMeets()
    //{
    //    SessionsBLL SessionsAdapter = new SessionsBLL();

    //    SwimTeamDatabase.SessionsDataTable SessionsInMonth = SessionsAdapter.GetSessionsForMonth(this.SetupTime);
    //    List<SessionHelperDayInMonth> SessionHelperList = new List<SessionHelperDayInMonth>();
    //    SessionHelperList.Add(new SessionHelperDayInMonth());
    //    for (int i = 1; i <= DateTime.DaysInMonth(this.SetupTime.Year, this.SetupTime.Month); i++)
    //        SessionHelperList.Add(new SessionHelperDayInMonth());
    //    foreach (SwimTeamDatabase.SessionsRow Session in SessionsInMonth)
    //        SessionHelperList[Session.SessionDate.Day].AddSession(Session);

    //    EntryBLL EntriesAdapter = new EntryBLL();
    //    SwimTeamDatabase.EntriesDataTable EntriesForAllSwimmers = new SwimTeamDatabase.EntriesDataTable();
    //    for (int i = 0; i < this._USAIDList.Count; i++)
    //    {
    //        SwimTeamDatabase.EntriesDataTable EntriesForSwimmer = EntriesAdapter.GetEntriesForSwimmer(this._USAIDList[i]);
    //        SwimTeamDatabase.EntriesRow TempRow = EntriesForAllSwimmers.NewEntriesRow();
    //        foreach (SwimTeamDatabase.EntriesRow Entry in EntriesForSwimmer)
    //            EntriesForAllSwimmers.AddEntriesRow(Entry.SessionID, Entry.USAID, Entry.InDatabase, Entry.MeetID);
    //    }

    //    for (int i = 0; i < SessionHelperList.Count; i++)
    //        SessionHelperList[i].SetSessionsDisplayModeVersusEntries(EntriesForAllSwimmers);

    //    for (int i = 1; i <= DateTime.DaysInMonth(SetupTime.Year, SetupTime.Month); i++)
    //    {
    //        TableCell Cell = (TableCell)CalendarTable.FindControl("CellDate" + i);

    //        Label MeetLabel = new Label();
    //        MeetLabel.Text = SessionHelperList[i].LabelTextForDay();

    //        Cell.Controls.Add(MeetLabel);
    //    }

    //}
    private void PopulateWithMeetsV2()
    {
        SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
        DateTime StartDate = new DateTime(this.SetupTime.Year, this.SetupTime.Month, 1, 0, 0, 0);
        DateTime EndDate = new DateTime(this.SetupTime.Year, this.SetupTime.Month, DateTime.DaysInMonth(this.SetupTime.Year, this.SetupTime.Month), 0, 0, 0);
        SwimTeamDatabase.SessionV2DataTable SessionsInMonth = SessionsAdapter.GetSessionsForMonth(this.SetupTime);
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable MeetsInMonth = MeetsAdapter.GetMeetsBetweenTwoDates(StartDate, EndDate);
        List<SessionHelperDayInMonth> SessionHelperList = new List<SessionHelperDayInMonth>();
        SessionHelperList.Add(new SessionHelperDayInMonth());
        for (int i = 1; i <= DateTime.DaysInMonth(this.SetupTime.Year, this.SetupTime.Month); i++)
            SessionHelperList.Add(new SessionHelperDayInMonth());
        foreach (SwimTeamDatabase.SessionV2Row Session in SessionsInMonth)
        {
            SwimTeamDatabase.MeetsV2Row Meet = MeetsInMonth.NewMeetsV2Row();
            for (int i = 0; i < MeetsInMonth.Count; i++)
            {
                if (Session.MeetID == MeetsInMonth[i].Meet)
                {
                    Meet = MeetsInMonth[i];
                    break;
                }
            }
            DateTime SessionDate = Meet.Start.AddDays(Session.Day - 1);
            if (SessionDate.Month == this.SetupTime.Month)
                SessionHelperList[SessionDate.Day].AddSession(Session);
        }

        PreEnteredV2BLL PreEntriesAdapter = new PreEnteredV2BLL();
        SwimTeamDatabase.PreEnteredV2DataTable PreEntriesForAllSwimmers = new SwimTeamDatabase.PreEnteredV2DataTable();
        for (int i = 0; i < this._USAIDList.Count; i++)
        {
            SwimTeamDatabase.PreEnteredV2DataTable PreEntriesForSwimmer = PreEntriesAdapter.GetPreEntriesByUSAID(this._USAIDList[i]);
            foreach (SwimTeamDatabase.PreEnteredV2Row PreEntry in PreEntriesForSwimmer)
            {
                bool PreEntryInMonth = false;
                for(int j = 0; j < MeetsInMonth.Count; j++)
                    if (PreEntry.MeetID == MeetsInMonth[j].Meet)
                    {
                        PreEntryInMonth = true;
                        break;
                    }
                if (PreEntryInMonth)
                {
                    SwimTeamDatabase.PreEnteredV2Row TempRow = PreEntriesForAllSwimmers.NewPreEnteredV2Row();
                    TempRow.ItemArray = PreEntry.ItemArray;
                    PreEntriesForAllSwimmers.AddPreEnteredV2Row(TempRow);
                }
            }
        }

        for (int i = 0; i < SessionHelperList.Count; i++)
            SessionHelperList[i].SetSessionsDisplayModeVersusEntries(PreEntriesForAllSwimmers);

        for (int i = 1; i <= DateTime.DaysInMonth(SetupTime.Year, SetupTime.Month); i++)
        {
            TableCell Cell = (TableCell)CalendarTable.FindControl("CellDate" + i);

            Label MeetLabel = new Label();
            MeetLabel.Text = SessionHelperList[i].LabelTextForDay();

            Cell.Controls.Add(MeetLabel);
        }
    }

    public void AddGroup(int GroupID)
    {
        List<int> templist = this.Private_GroupIDList;
        if (!templist.Contains(GroupID))
            templist.Add(GroupID);
        this.Private_GroupIDList = templist;
    }
    public void ClearGroups()
    {
        this.Private_GroupIDList = new List<int>();
    }

    public void AddSwimmer(String USAID)
    {
        List<String> templist = this.Private_USAIDList;
        if (!templist.Contains(USAID))
            templist.Add(USAID);
        this.Private_USAIDList = templist;
    }
    public void ClearSwimmers()
    {
        this.Private_USAIDList = new List<String>();
    }


    private class SessionHelperDayInMonth
    {
        public enum SessionDisplayType { Display, DoNotDisplay }

        private List<SwimTeamDatabase.SessionV2Row> _session;
        private List<SessionDisplayType> _displayTypes;
        public List<SwimTeamDatabase.SessionV2Row> Session
        {
            get { return this._session; }
        }

        public SessionHelperDayInMonth()
        {
            this._session = new List<SwimTeamDatabase.SessionV2Row>();
            this._displayTypes = new List<SessionDisplayType>();
        }

        public void AddSession(SwimTeamDatabase.SessionV2Row SessionToAdd)
        {
            bool SessionAlreadyAdded = false;
            for (int i = 0; i < _session.Count; i++)
            {
                if (_session[i].SessionsID == SessionToAdd.SessionsID)
                {
                    SessionAlreadyAdded = true;
                    i = _session.Count;
                }
            }

            if (!SessionAlreadyAdded)
            {
                _session.Add(SessionToAdd);
                _displayTypes.Add(SessionDisplayType.DoNotDisplay);
            }
        }

        public void SetSessionsDisplayModeVersusEntries(SwimTeamDatabase.PreEnteredV2DataTable PreEntries)
        {
            MeetEventsBLL EventsAdapter = new MeetEventsBLL();
            for (int i = 0; i < this._session.Count; i++)
            {
                bool DisplaySession = false;
                for (int j = 0; j < PreEntries.Count; j++)
                {
                    if ((PreEntries[j].MeetID == this._session[i].MeetID)
                        && PreEntries[j].IsPreEnteredInSession(this._session[i].Session))
                    {
                        DisplaySession = true;
                        break;
                    }
                }

                if (DisplaySession)
                    this._displayTypes[i] = SessionDisplayType.Display;
            }
        }

        public String LabelTextForDay()
        {
            String ReturnString = "";
            bool MeetNameInString = false;
            for (int i = 0; i < this._session.Count; i++)
                if (this._displayTypes[i] == SessionDisplayType.Display)
                {
                    if (!MeetNameInString)
                    {
                        SwimTeamDatabase.MeetsV2Row Meet = new MeetsV2BLL().GetMeetByMeetID(_session[i].MeetID);
                        ReturnString += "<br /><b>" + Meet.MeetName + "</b>";
                        MeetNameInString = true;
                    }

                    if (!ReturnString.Contains("Warm up time:"))
                    {
                        ReturnString += "<br />Warm up time: ";
                        ReturnString += this._session[i].StartTime;
                        if (this._session[i].AM)
                            ReturnString += " AM";
                        else
                            ReturnString += " PM";
                        if (this._session[i].Guess)
                            ReturnString += " (GUESS)";
                    }
                    else
                    {
                        ReturnString = ReturnString.Replace("Warm up time:", "Warm up times:");
                        ReturnString += ", ";
                        ReturnString += this._session[i].StartTime;
                        if (this._session[i].AM)
                            ReturnString += " AM";
                        else
                            ReturnString += " PM";
                        if (this._session[i].Guess)
                            ReturnString += " (GUESS)";
                    }
                }

            return ReturnString;
        }
    }
}