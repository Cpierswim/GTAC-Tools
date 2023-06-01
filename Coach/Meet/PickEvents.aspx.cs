using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Coach_Meet_PickEvents : System.Web.UI.Page
{
    private bool IsSaveButtonOnPage
    {
        get
        {
            if (ViewState["SaveButtonOnPage"] == null)
                return false;
            else
                return true;
        }
        set
        {
            if (value == true)
                ViewState["SaveButtonOnPage"] = "True";
            else
                ViewState.Remove("SaveButtonOnPage");
        }
    }
    private String TempPreviousMeetValue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            this.DataBindMeetsDropDownList();
        try
        {
            if (!JavaScriptHelper.IsJavascriptEnabled)
                Response.Redirect("~/NoJavaScript.aspx");

            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsOnSubmitStatementRegistered("ShowSplashScreen"))
            {
                ClientScript.RegisterOnSubmitStatement(this.GetType(), "ShowSplashScreen", "ShowSplashScreen(this)");
            }

            if (DisplayingMeet.Value != MeetsDropDownList.SelectedValue)
            {
                this.TempPreviousMeetValue = this.MeetsDropDownList.SelectedValue;
                if (!String.IsNullOrWhiteSpace(DisplayingMeet.Value))
                    this.MeetsDropDownList.SelectedValue = DisplayingMeet.Value;
            }

            if (this.MeetsDropDownList.SelectedItem == null)
                this.MeetNameLabel.Text = "";
            else
                this.MeetNameLabel.Text = this.MeetsDropDownList.SelectedItem.Text;

            this.MeetNameLabel.Style.Add(HtmlTextWriterStyle.MarginBottom, "20px");


            if (!String.IsNullOrWhiteSpace(this.MeetsDropDownList.SelectedValue) &&
                this.MeetsDropDownList.SelectedValue != "-1")
            {
                SwimTeamDatabase.MeetsV2Row Meet = this.CreateAccordion(true);
                //adding the meet course should really be done in this step, but I couldn't efficiently (actually didn't want to take
                //the time) to get it to be the first thing added to the page. It is done in the above method.
            }
        }
        catch (Exception error)
        {
            if (Page.IsPostBack)
            {
                this.PagePanel.Controls.Clear();
                Label ErrorLabel = new Label();
                ErrorLabel.ID = "ErrorLabel1";
                ErrorLabel.Text = "There was an error when processing the page. An exception report will be sent to Chris Pierson, however it";
                ErrorLabel.Text += " has not already been sent, so if there is an error sending the e-mail, it would be best if you told him";
                ErrorLabel.Text += " what you were doing when this error happened.";
                this.PagePanel.Controls.Add(ErrorLabel);
                if (Page.IsPostBack)
                    this.SendErrorEmail(error);
                else
                    throw error;
            }
        }
    }

    private void DataBindMeetsDropDownList()
    {
        this.MeetsDropDownList.Items.Clear();
        this.MeetsDropDownList.Items.Add(new ListItem("", "-1", true));

        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        DateTime CurrentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

        bool FirstMeetPastTodaysDateFound = false;
        int MeetIndexToSelect = -1;
        ListItemCollection ItemCollection = new ListItemCollection();
        for (int i = 0; i < Meets.Count; i++)
        {
            ListItem Item = new ListItem(Meets[i].MeetName, Meets[i].Meet.ToString(), true);
            if (!FirstMeetPastTodaysDateFound)
                if (Meets[i].EndDate > CurrentDate)
                {
                    FirstMeetPastTodaysDateFound = true;
                    MeetIndexToSelect = Meets[i].Meet;
                }
            ItemCollection.Add(Item);
        }

        for (int i = ItemCollection.Count - 1; i >= 0; i--)
            this.MeetsDropDownList.Items.Add(ItemCollection[i]);
        this.MeetsDropDownList.SelectedValue = MeetIndexToSelect.ToString();
    }

    protected void MeetsDropDownListDataBound(object sender, EventArgs e)
    {
        ListItemCollection OriginalItems = new ListItemCollection();
        foreach (ListItem L in this.MeetsDropDownList.Items)
            OriginalItems.Add(L);
        this.MeetsDropDownList.Items.Clear();
        this.MeetsDropDownList.Items.Add(new ListItem("", "-1", true));
        foreach (ListItem L in OriginalItems)
            this.MeetsDropDownList.Items.Add(L);
    }
    protected void LoadMeetClicked(object sender, EventArgs e)
    {
        try
        {
            this.MeetNameLabel.Visible = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CreateAccordions", "CreateAccordions()", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Unblock", "Unblock()", true);
            //if (!String.IsNullOrWhiteSpace(this.MeetsDropDownList.SelectedValue))
            //{
            //    SwimTeamDatabase.MeetsV2Row Meet = this.CreateAccordion();
            //    //adding the meet course should really be done in this step, but I couldn't efficiently (actually didn't want to take
            //    //the time) to get it to be the first thing added to the page. It is done in the above method.
            //}
            //System.Web.HttpContext.Current.Request.Form.Clear();
            this.ErrorLabel.Text = "";
            this.PagePanel.Controls.Clear();
            if (!string.IsNullOrWhiteSpace(this.TempPreviousMeetValue))
                this.MeetsDropDownList.SelectedValue = this.TempPreviousMeetValue;
            if (!String.IsNullOrWhiteSpace(this.MeetsDropDownList.SelectedValue) &&
                this.MeetsDropDownList.SelectedValue != "-1")
            {
                SwimTeamDatabase.MeetsV2Row Meet = this.CreateAccordion(false);
                this.DisplayingMeet.Value = Meet.Meet.ToString();
                this.MeetNameLabel.Text = Meet.MeetName;
                DateTime CoachDeadline = Meet.Deadline.AddDays(2);
                while (CoachDeadline.DayOfWeek == DayOfWeek.Saturday || CoachDeadline.DayOfWeek == DayOfWeek.Sunday)
                    CoachDeadline = CoachDeadline.AddDays(1);
                CoachDeadline = new DateTime(CoachDeadline.Year, CoachDeadline.Month, CoachDeadline.Day, 14, 0, 0);
                DateTime CurrentTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
                if (CurrentTime < CoachDeadline)
                    this.DeadlineLabel.Text = "Event Pick Deadline: " + CoachDeadline.ToString("dddd MMMM d") + ", by " + CoachDeadline.ToString("h:mm tt");
                else
                {
                    if (CurrentTime < Meet.LateEntryDeadline)
                        this.DeadlineLabel.Text = "Deadline passed. Open for Late Entries.";
                    else
                        this.DeadlineLabel.Text = "Deadline passed. Late Entries Closed.";
                }
                if (!Meet.IsCoachNotesNull())
                    if (!string.IsNullOrWhiteSpace(Meet.CoachNotes))
                    {
                        this.CoachInfoLabel.Text = Meet.CoachNotes + "<br /><br />";
                        this.CoachInfoLabel.Visible = true;
                    }
                    else
                        this.CoachInfoLabel.Visible = false;
                else
                    this.CoachInfoLabel.Visible = false;
            }
            else if (this.MeetsDropDownList.SelectedValue == "-1")
            {
                Page.Form.DefaultButton = this.LoadMeetButton.ID;
                this.CoachInfoLabel.Text = "";
                this.DeadlineLabel.Text = "";
                this.ErrorLabel.Text = "";
                this.MeetNameLabel.Text = "";
            }
        }
        catch (Exception error)
        {
            if (Page.IsPostBack)
            {
                this.PagePanel.Controls.Clear();
                Label ErrorLabel = new Label();
                ErrorLabel.ID = "ErrorLabel2";
                ErrorLabel.Text = "There was an error when processing the page. An exception report will be sent to Chris Pierson, however it";
                ErrorLabel.Text += " has not already been sent, so if there is an error sending the e-mail, it would be best if you told him";
                ErrorLabel.Text += " what you were doing when this error happened.";
                this.PagePanel.Controls.Add(ErrorLabel);
                this.SendErrorEmail(error);
            }
        }
    }

    private SwimTeamDatabase.SwimmersDataTable _Swimmers;
    private SwimTeamDatabase.SwimmersDataTable Swimmers
    {
        get
        {
            if (this._Swimmers == null)
                if (ViewState["Swimmers"] != null)
                    this._Swimmers = ((SwimTeamDatabase.SwimmersDataTable)ViewState["Swimmers"]);
            return this._Swimmers;
        }
        set
        {
            this._Swimmers = value;
            ViewState["Swimmers"] = value;
        }
    }
    private SwimTeamDatabase.MeetEventsDataTable _Events;
    private SwimTeamDatabase.MeetEventsDataTable MeetEvents
    {
        get
        {
            if (this._Events == null)
                if (ViewState["Events"] != null)
                    this._Events = ((SwimTeamDatabase.MeetEventsDataTable)ViewState["Events"]);
            return this._Events;
        }
        set
        {
            this._Events = value;
            ViewState["Events"] = value;
        }
    }
    private SwimTeamDatabase.ArchiveMeetsDataTable _AllMeets;
    private SwimTeamDatabase.ArchiveMeetsDataTable AllMeets
    {
        get
        {
            if (this._AllMeets == null)
                this._AllMeets = new ArchiveMeetsBLL().GetAllMeets();
            return this._AllMeets;
        }
    }
    private SwimTeamDatabase.SessionV2DataTable _Sessions;
    private SwimTeamDatabase.SessionV2DataTable Sessions
    {
        get
        {
            if (this._Sessions == null)
                if (ViewState["Sessions"] != null)
                    this._Sessions = ((SwimTeamDatabase.SessionV2DataTable)ViewState["Sessions"]);
            return this._Sessions;
        }
        set
        {
            this._Sessions = value;
            ViewState["Sessions"] = value;
        }
    }
    private int? _DisplayingGroup;
    private int? DisplayingGroup
    {
        get
        {
            if (this._DisplayingGroup == null)
                if (ViewState["DisplayingGroup"] != null)
                    this._DisplayingGroup = int.Parse(ViewState["DisplayingGroup"].ToString());
            return this._DisplayingGroup;
        }
        set
        {
            this._DisplayingGroup = value;
            ViewState["DisplayingGroup"] = value;
        }
    }
    private List<String> _ErrorStrings;
    private List<String> ErrorStrings
    {
        get
        {
            if (_ErrorStrings == null)
                _ErrorStrings = new List<string>();
            return _ErrorStrings;
        }
        set
        {
            _ErrorStrings = value;
        }
    }
    private SwimTeamDatabase.EntriesV2DataTable Entries;
    private SwimTeamDatabase.SwimmerAthleteJoinDataTable Joins;
    private SwimTeamDatabase.BestTimesDataTable BestTimes;

    private SwimTeamDatabase.MeetsV2Row CreateAccordion(bool UsePreviousValues)
    {
        if ((Swimmers == null && !string.IsNullOrWhiteSpace(this.MeetsDropDownList.SelectedValue)) ||
            ((DisplayingGroup.ToString() != this.MeetsDropDownList.SelectedValue) && !string.IsNullOrWhiteSpace(this.MeetsDropDownList.SelectedValue)))
        {
            //First Time on the page for this session (basically)
            int MeetID = int.Parse(this.MeetsDropDownList.SelectedValue);
            int GroupID = int.Parse(this.GroupsDropDownList.SelectedValue);
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            if (GroupID != -1)
                this.Swimmers = SwimmersAdapter.GetSwimmersByGroupID(GroupID);
            else
                this.Swimmers = SwimmersAdapter.GetActiveSwimmers();
            MeetEventsBLL EventsAdapter = new MeetEventsBLL();
            this.MeetEvents = EventsAdapter.GetEventByMeetID(MeetID);
            SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
            this.Sessions = SessionsAdapter.GetSessionsByMeetID(MeetID);
            MeetsV2BLL MeetAdapter = new MeetsV2BLL();
            SwimTeamDatabase.MeetsV2Row Meet = MeetAdapter.GetMeetByMeetID(MeetID);
            PreEnteredV2BLL PreEntriesAdapter = new PreEnteredV2BLL();
            SwimTeamDatabase.PreEnteredV2DataTable PreEntries = PreEntriesAdapter.GetPreEntriesByMeetID(MeetID);
            EntriesV2BLL EntriesAdapter = new EntriesV2BLL();
            SwimTeamDatabase.EntriesV2DataTable Entries;
            if (GroupID != -1)
                Entries = EntriesAdapter.GetByMeetIDandGroupID(MeetID, GroupID);
            else
                Entries = EntriesAdapter.GetByMeetID(MeetID);
            this.Entries = Entries;
            SwimmerAthleteJoinBLL JoinsAdapter = new SwimmerAthleteJoinBLL();
            SwimTeamDatabase.SwimmerAthleteJoinDataTable Joins = JoinsAdapter.GetAllJoins();
            this.Joins = Joins;
            BestTimesBLL BestTimesAdapter = new BestTimesBLL();
            SwimTeamDatabase.BestTimesDataTable BestTimes;
            if (GroupID != -1)
                BestTimes = BestTimesAdapter.GetBestTimesByGroupID(GroupID);
            else
                BestTimes = BestTimesAdapter.GetAllBestTimes();
            this.BestTimes = BestTimes;

            Panel MainAccordion = new Panel();
            MainAccordion.ID = "MainAccordion";
            if (Meet != null)
            {
                HiddenField MeetCourseHiddenField = new HiddenField();
                MeetCourseHiddenField.ID = "MeetCourseHiddenField";
                MeetCourseHiddenField.Value = Meet.Course;

                this.PagePanel.Controls.Add(MeetCourseHiddenField);
            }

            bool PrelimFinalsMeet = false;
            for (int i = 0; i < this.Sessions.Count; i++)
                if (this.Sessions[i].PrelimFinal)
                {
                    PrelimFinalsMeet = true;
                    break;
                }
            if (PrelimFinalsMeet)
            {
                Panel PrelimFinalNotePanel = new Panel();
                PrelimFinalNotePanel.Width = new Unit("100%");
                PrelimFinalNotePanel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                Label PrelimFinalNoteLabel = new Label();
                PrelimFinalNoteLabel.ID = "PrelimFinalNoteLabel";
                PrelimFinalNoteLabel.Text = "This meet has Prelim/Finals events.<br />If you enter a swimer in a single Prelim/Final event" +
                    " on a day, the limit of events that day is 3.<br />This system does not check for this.";
                PrelimFinalNoteLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                PrelimFinalNoteLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                PrelimFinalNotePanel.Controls.Add(PrelimFinalNoteLabel);
                this.PagePanel.Controls.Add(PrelimFinalNotePanel);
            }

            List<SwimTeamDatabase.SwimmersRow> NotEntered = new List<SwimTeamDatabase.SwimmersRow>();
            int Entered = 0;
            foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
            {
                bool preentered = false;
                for (int i = 0; i < PreEntries.Count; i++)
                    if (PreEntries[i].USAID == Swimmer.USAID)
                    {
                        preentered = true;
                        break;
                    }
                if (preentered)
                {
                    Entered++;
                    int AthleteID = -1;
                    for (int i = 0; i < Joins.Count; i++)
                        if (Joins[i].SwimmerID == Swimmer.USAID)
                        {
                            AthleteID = Joins[i].HyTekAthleteID;
                            break;
                        }
                    if (AthleteID == -1)
                        throw new Exception("AthleteID Not Found - You probably need to merge the offline and online database.");
                    int SwimmersTotalEntriesInMeet = 0;
                    Panel SwimmerPanel = CreateSwimmerAccordion(Swimmer, MeetEvents, Sessions, Meet, Entries, AthleteID,
                        PreEntries, BestTimes, UsePreviousValues, ErrorStrings, out SwimmersTotalEntriesInMeet);
                    Literal header = new Literal();

                    header.Text = "<h3 id=\"" + Swimmer.USAID + "Header\"" + ">";
                    header.Text += "<a href=\"#\">";
                    header.Text += Swimmer.PreferredName + " " + Swimmer.LastName;
                    if (SwimmersTotalEntriesInMeet == 0)
                        header.Text = "<h3 id=\"" + Swimmer.USAID + "Header\"" + ">" + "<a href=\"#\">" + Swimmer.PreferredName + " " + Swimmer.LastName + " - No Events Picked";
                    header.Text += "</a></h3>";
                    MainAccordion.Controls.Add(header);
                    MainAccordion.Controls.Add(SwimmerPanel);
                }
                else
                    NotEntered.Add(Swimmer);
            }


            Literal footerheader = new Literal();
            footerheader.Text = "<h3 id=\"" + "NotAttending" + "Header\"" + ">";
            footerheader.Text += "<a href=\"#\">";
            footerheader.Text += "Not Entered: " + NotEntered.Count;
            footerheader.Text += "</a></h3>";
            MainAccordion.Controls.Add(footerheader);

            Panel FooterPanel = new Panel();
            FooterPanel.ID = "NotEntered";
            for (int i = 0; i < NotEntered.Count; i++)
            {
                Label NameLabel = new Label();
                NameLabel.ID = "Meet" + Meet.Meet + NotEntered[i].USAID + "NotEnteredLabel";
                NameLabel.Text = NotEntered[i].PreferredName + " " + NotEntered[i].LastName + "<br />";
                FooterPanel.Controls.Add(NameLabel);
            }
            MainAccordion.Controls.Add(FooterPanel);

            this.PagePanel.Controls.Add(MainAccordion);

            Button HiddenButton = new Button();

            if (Entered > 0)
            {
                Literal Breaks = new Literal();
                Breaks.Text = "<br /><br />";
                this.PagePanel.Controls.Add(Breaks);
                Button SaveButton;
                Object o = this.FindControl("SaveButton");
                if (o == null)
                    SaveButton = new Button();
                else
                    SaveButton = ((Button)o);
                SaveButton.ID = "SaveButton";
                SaveButton.Text = "Save Entries";
                SaveButton.CausesValidation = true;
                SaveButton.UseSubmitBehavior = false;
                SaveButton.Click += new EventHandler(SaveButton_Click);
                SaveButton.Attributes.Add("onclick", "SaveButtonClick(this)");
                IsSaveButtonOnPage = true;

                this.PagePanel.Controls.Add(SaveButton);
            }

            HiddenButton.ID = "HiddenButton";

            HiddenButton.Attributes.Add("onclick", "return false;");
            HiddenButton.Style.Add(HtmlTextWriterStyle.Display, "none");
            HiddenButton.CausesValidation = false;
            this.PagePanel.Controls.Add(HiddenButton);

            //MainAccordion.DefaultButton = HiddenButton.UniqueID;
            Page.Form.DefaultButton = HiddenButton.UniqueID;

            return Meet;
        }

        return null;
    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CreateAccordions", "CreateAccordions()", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Unblock", "Unblock()", true);

            bool ChosenMeetMatchesDisplayingMeet = true;
            //if (DisplayingMeet.Value != MeetsDropDownList.SelectedValue)
            //{
            //    this.MeetsDropDownList.SelectedValue = DisplayingMeet.Value;
            //    this.PagePanel.Controls.Clear();
            //    this.CreateAccordion(true);
            //    this.MeetNameLabel.Text = this.MeetsDropDownList.SelectedItem.Text;
            //    ChosenMeetMatchesDisplayingMeet = false;
            //}


            if (Page.IsValid && ChosenMeetMatchesDisplayingMeet)
            {
                #region SavingStuff
                EntriesV2BLL EntriesAdapter = new EntriesV2BLL();
                int NextEntryID = EntriesAdapter.GetMaxEntryID() + 1;
                int EntriesAdded = 0, EntriesUpdated = 0, EntriesDeleted = 0;
                //If we get here, everything has been validated as correct and now it's time to save the meet
                List<SwimTeamDatabase.EntriesV2Row> RowsToDelete = new List<SwimTeamDatabase.EntriesV2Row>();
                ControlCollection MainAccordion = ((ControlCollection)this.PagePanel.Controls[2].Controls);
                if (MainAccordion.Count == 1)
                    MainAccordion = ((ControlCollection)this.PagePanel.Controls[3].Controls);
                for (int i = 0; i < MainAccordion.Count; i++)
                {
                    if (MainAccordion[i].GetType() == typeof(Panel))
                    {
                        Panel SwimmerPanel = ((Panel)MainAccordion[i]);
                        if (SwimmerPanel.ID != "NotEntered")
                        {
                            String tempUSAID = SwimmerPanel.ID.Remove(SwimmerPanel.ID.Length - "Panel".Length);
                            String USAID = tempUSAID.Substring(tempUSAID.Length - 14);

                            SwimTeamDatabase.SwimmersRow Swimmer = null;

                            for (int j = 0; j < Swimmers.Count; j++)
                                if (Swimmers[j].USAID == USAID)
                                {
                                    Swimmer = Swimmers[j];
                                    break;
                                }

                            int AthleteID = -1;
                            for (int j = 0; j < Joins.Count; j++)
                                if (Joins[j].SwimmerID == Swimmer.USAID)
                                {
                                    AthleteID = Joins[j].HyTekAthleteID;
                                    break;
                                }

                            for (int j = 0; j < SwimmerPanel.Controls.Count; j++)
                            {
                                if (SwimmerPanel.Controls[j].GetType() == typeof(SessionTable))
                                {
                                    SessionTable Table = ((SessionTable)SwimmerPanel.Controls[j]);
                                    for (int k = 2; k < Table.Rows.Count; k++)
                                    {
                                        Label EventLabel = ((Label)Table.Rows[k].Cells[0].Controls[0]);
                                        TextBox EntryTextBox = ((TextBox)Table.Rows[k].Cells[3].Controls[0].Controls[1]);
                                        CheckBox EntryCheckBox = ((CheckBox)Table.Rows[k].Cells[3].Controls[0].Controls[0]);
                                        Panel FastCutTimePanel = ((Panel)Table.Rows[k].Cells[5].Controls[0]);
                                        Panel SlowCutTimePanel = ((Panel)Table.Rows[k].Cells[6].Controls[0]);
                                        CheckBox BonusCheckBox = ((CheckBox)Table.Rows[k].Cells[7].Controls[0]);
                                        CheckBox ExhibitionCheckBox = ((CheckBox)Table.Rows[k].Cells[8].Controls[0]);

                                        HyTekTime EntryTime = null;
                                        String Course = "";
                                        if (!String.IsNullOrWhiteSpace(EntryTextBox.Text))
                                        {
                                            EntryTime = new HyTekTime(EntryTextBox.Text);
                                            Course = EntryTextBox.Text.Substring(EntryTextBox.Text.Length - 1);
                                        }

                                        HyTekTime FastCutTime = null, SlowCutTime = null;
                                        if (EntryCheckBox.Checked)
                                            if (EntryTime != null)
                                            {
                                                for (int l = 0; l < FastCutTimePanel.Controls.Count; l++)
                                                {
                                                    if (FastCutTimePanel.Controls[l].GetType() == typeof(Label))
                                                    {
                                                        Label CutTimeLabel = ((Label)FastCutTimePanel.Controls[l]);
                                                        if (Course == CutTimeLabel.Text.Substring(CutTimeLabel.Text.Length - 1))
                                                        {
                                                            FastCutTime = new HyTekTime(CutTimeLabel.Text);
                                                            break;
                                                        }
                                                    }
                                                }
                                                for (int l = 0; l < SlowCutTimePanel.Controls.Count; l++)
                                                {
                                                    if (SlowCutTimePanel.Controls[l].GetType() == typeof(Label))
                                                    {
                                                        Label CutTimeLabel = ((Label)SlowCutTimePanel.Controls[l]);
                                                        if (Course == CutTimeLabel.Text.Substring(CutTimeLabel.Text.Length - 1))
                                                        {
                                                            SlowCutTime = new HyTekTime(CutTimeLabel.Text);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }

                                        int EventNumber = -1;
                                        String EventNumberExtended = "";
                                        if (!int.TryParse(EventLabel.Text, out EventNumber))
                                        {
                                            String temp = EventLabel.Text;
                                            int CharsToRemove = 0;
                                            while (!int.TryParse(temp, out EventNumber))
                                            {
                                                CharsToRemove++;
                                                temp = EventLabel.Text.Remove(EventLabel.Text.Length - CharsToRemove);
                                            }
                                            EventNumberExtended = EventLabel.Text.Remove(0, EventNumber.ToString().Length);
                                        }

                                        SwimTeamDatabase.MeetEventsRow MeetEvent = null;

                                        for (int l = 0; l < this.MeetEvents.Count; l++)
                                            if (this.MeetEvents[l].EventNumber == EventNumber)
                                            {
                                                bool EventFound = false;
                                                if (this.MeetEvents[l].IsEventNumberExtendedNull() && string.IsNullOrEmpty(EventNumberExtended))
                                                    EventFound = true;
                                                else if (!this.MeetEvents[l].IsEventNumberExtendedNull())
                                                    if (this.MeetEvents[l].EventNumberExtended == EventNumberExtended)
                                                        EventFound = true;
                                                if (EventFound)
                                                {
                                                    MeetEvent = this.MeetEvents[l];
                                                    break;
                                                }
                                            }
                                        SwimTeamDatabase.EntriesV2Row Entry = null;
                                        for (int l = 0; l < this.Entries.Count; l++)
                                            if (Entries[l].EventID == MeetEvent.EventID &&
                                                Entries[l].AthleteID == AthleteID)
                                            {
                                                Entry = Entries[l];
                                                break;
                                            }
                                        SwimTeamDatabase.BestTimesRow BestTime = null;
                                        if (EntryCheckBox.Checked)
                                        {
                                            for (int l = 0; l < this.BestTimes.Count; l++)
                                            {
                                                if (this.BestTimes[l].Course == Course &&
                                                    this.BestTimes[l].Distance == MeetEvent.Distance &&
                                                    this.BestTimes[l].Stroke == MeetEvent.StrokeCode &&
                                                    this.BestTimes[l].USAID == Swimmer.USAID)
                                                {
                                                    BestTime = this.BestTimes[l];
                                                    break;
                                                }

                                            }
                                        }
                                        if (Entry == null && EntryCheckBox.Checked)
                                        {
                                            SwimTeamDatabase.EntriesV2Row NewEntry = this.Entries.NewEntriesV2Row();

                                            //autotime, customtime, course
                                            NewEntry.MeetEntryID = NextEntryID;
                                            NextEntryID++;
                                            NewEntry.AthleteID = AthleteID;
                                            NewEntry.Bonus = BonusCheckBox.Checked;
                                            NewEntry.EnterEvent = true;
                                            NewEntry.EventID = MeetEvent.EventID;
                                            NewEntry.Exhibition = ExhibitionCheckBox.Checked;
                                            NewEntry.InDatabase = false;
                                            NewEntry.MeetID = MeetEvent.MeetID;
                                            bool EnterEvent = true;
                                            //TODO: This cuttime stuff may not be necessary because I think it is checked in validation
                                            if (!BonusCheckBox.Checked)
                                                if (FastCutTime != null)
                                                    if (EntryTime > FastCutTime)
                                                        EnterEvent = false;
                                            if (EnterEvent)
                                            {
                                                if (BestTime != null)
                                                {
                                                    if (EntryTime.Score == BestTime.Score)
                                                        NewEntry.AutoTime = 0;
                                                    else
                                                        NewEntry.CustomTime = EntryTime.Score ?? -1;
                                                }
                                                else
                                                {
                                                    NewEntry.CustomTime = EntryTime.Score ?? -1;
                                                }
                                                NewEntry.Course = Course;

                                                this.Entries.AddEntriesV2Row(NewEntry);
                                                EntriesAdded++;
                                            }
                                        }
                                        else if (Entry != null && EntryCheckBox.Checked)
                                        {
                                            bool EntryChanged = false;
                                            //autotime, customtime, course
                                            if (Entry.Bonus != BonusCheckBox.Checked)
                                            {
                                                Entry.Bonus = BonusCheckBox.Checked;
                                                EntryChanged = true;
                                            }
                                            if (Entry.EnterEvent != true)
                                            {
                                                Entry.EnterEvent = true;
                                                EntryChanged = true;
                                            }
                                            if (Entry.EventID != MeetEvent.EventID)
                                                throw new Exception("ooga booga");
                                            //Entry.EventID = MeetEvent.EventID;
                                            if (Entry.Exhibition != ExhibitionCheckBox.Checked)
                                            {
                                                Entry.Exhibition = ExhibitionCheckBox.Checked;
                                                EntryChanged = true;
                                            }
                                            if (Entry.InDatabase != false)
                                            {
                                                Entry.InDatabase = false;
                                                EntryChanged = true;
                                            }
                                            if (Entry.MeetID != MeetEvent.MeetID)
                                                throw new Exception("ooga booga 2");
                                            //Entry.MeetID = MeetEvent.MeetID;
                                            bool EnterEvent = true;
                                            //TODO: This cuttime stuff may not be necessary because I think it is checked in validation
                                            if (!BonusCheckBox.Checked)
                                                if (FastCutTime != null)
                                                    if (EntryTime > FastCutTime)
                                                        EnterEvent = false;
                                            if (EnterEvent)
                                            {
                                                if (BestTime != null)
                                                {
                                                    int AutoTime = -1, CustomTime = -1;
                                                    if (!Entry.IsAutoTimeNull())
                                                        AutoTime = Entry.AutoTime;
                                                    if (!Entry.IsCustomTimeNull())
                                                        CustomTime = Entry.CustomTime;
                                                    if (EntryTime.Score == BestTime.Score)
                                                    {
                                                        if (AutoTime != 0)
                                                        {
                                                            Entry.AutoTime = 0;
                                                            Entry.SetCustomTimeNull();
                                                            EntryChanged = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (CustomTime != EntryTime.Score)
                                                        {
                                                            Entry.CustomTime = EntryTime.Score ?? -1;
                                                            Entry.SetAutoTimeNull();
                                                            EntryChanged = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (Entry.IsCustomTimeNull())
                                                    {
                                                        Entry.CustomTime = EntryTime.Score ?? -1;
                                                        Entry.SetAutoTimeNull();
                                                        EntryChanged = true;
                                                    }
                                                    else
                                                        if (Entry.CustomTime != EntryTime.Score)
                                                        {
                                                            Entry.CustomTime = EntryTime.Score ?? -1;
                                                            Entry.SetAutoTimeNull();
                                                            EntryChanged = true;
                                                        }
                                                }
                                                if (Entry.Course != Course)
                                                {
                                                    Entry.Course = Course;
                                                    EntryChanged = true;
                                                }
                                            }
                                            if (EntryChanged)
                                                EntriesUpdated++;
                                        }
                                        else if (Entry != null && !EntryCheckBox.Checked)
                                        {
                                            RowsToDelete.Add(Entry);
                                            EntriesDeleted++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < RowsToDelete.Count; i++)
                    RowsToDelete[i].Delete();
                if (Entries != null)
                {
                    if (((SwimTeamDatabase.EntriesV2DataTable)Entries.GetChanges()) != null)
                    {
                        //everything has now been updated. send it to the database.
                        EntriesAdapter.BatchUpdate(Entries);

                        this.ErrorLabel.Text = "Entries saved";
                        if (EntriesAdded == 1)
                            ErrorLabel.Text += ". " + EntriesAdded + " entry added";
                        else if (EntriesAdded > 1)
                            ErrorLabel.Text += ". " + EntriesAdded + " entries added";
                        if (EntriesUpdated == 1)
                            if (ErrorLabel.Text == "Entries saved")
                                ErrorLabel.Text += ". " + EntriesUpdated + " entry updated";
                            else
                                ErrorLabel.Text += ", " + EntriesUpdated + " entry updated";
                        else if (EntriesUpdated > 1)
                            if (ErrorLabel.Text == "Entries saved")
                                ErrorLabel.Text += ". " + EntriesUpdated + " entries updated";
                            else
                                ErrorLabel.Text += ", " + EntriesUpdated + " entries updated";
                        if (EntriesDeleted == 1)
                            if (ErrorLabel.Text == "Entries saved")
                                ErrorLabel.Text += ". " + EntriesDeleted + " entry deleted";
                            else
                                ErrorLabel.Text += ", " + EntriesDeleted + " entry deleted";
                        if (EntriesDeleted > 1)
                            if (ErrorLabel.Text == "Entries saved")
                                ErrorLabel.Text += ". " + EntriesDeleted + " entries deleted";
                            else
                                ErrorLabel.Text += ", " + EntriesDeleted + " entries deleted";
                        ErrorLabel.Text += ".";
                    }
                    else
                    {
                        ErrorLabel.Text += "Meet Saved. There were no changes made.";
                    }
                }
                else
                {
                    ErrorLabel.Text += "Meet Saved. There were no changes made.";
                }
                #endregion

                this.PagePanel.Controls.Clear();
                this.CreateAccordion(false);
            }
            else
            {
                if (!ChosenMeetMatchesDisplayingMeet)
                {
                    ErrorLabel.Text += "ERRORS ON PAGE - MEET NOT SAVED<br /><br />You changed the meet selected and then clicked save. Double check all your" +
                        " entries and try again.";
                }
            }
        }
        catch (Exception error)
        {
            if (Page.IsPostBack)
            {
                this.PagePanel.Controls.Clear();
                Label ErrorLabel = new Label();
                ErrorLabel.ID = "ErrorLabel3";
                ErrorLabel.Text = "There was an error when processing the page. An exception report will be sent to Chris Pierson, however it";
                ErrorLabel.Text += " has not already been sent, so if there is an error sending the e-mail, it would be best if you told him";
                ErrorLabel.Text += " what you were doing when this error happened.";
                this.PagePanel.Controls.Add(ErrorLabel);
            }
        }
    }

    private Panel CreateSwimmerAccordion(SwimTeamDatabase.SwimmersRow Swimmer, SwimTeamDatabase.MeetEventsDataTable Events,
        SwimTeamDatabase.SessionV2DataTable Sessions, SwimTeamDatabase.MeetsV2Row Meet, SwimTeamDatabase.EntriesV2DataTable Entries,
        int AthleteID, SwimTeamDatabase.PreEnteredV2DataTable PreEntries, SwimTeamDatabase.BestTimesDataTable BestTimes,
        bool UsePreviousValues, List<String> ErrorStrings, out int SwimmersTotalEntriesForMeet)
    {
        Panel SwimmerPanel = new Panel();
        SwimmerPanel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Panel";

        Literal MaxEntriesLiteral = new Literal();
        MaxEntriesLiteral.Text = "Max Entries for Meet: ";
        SwimmerPanel.Controls.Add(MaxEntriesLiteral);

        Label MaxEntriesLabel = new Label();
        MaxEntriesLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "MaxMeetEntries";
        if (Meet.MaxIndEnt == 0)
            MaxEntriesLabel.Text += "No Limit";
        else
            MaxEntriesLabel.Text += Meet.MaxIndEnt;
        SwimmerPanel.Controls.Add(MaxEntriesLabel);

        HiddenField CurrentEntriesInMeet = new HiddenField();
        CurrentEntriesInMeet.ID = "Meet" + Meet.Meet + Swimmer.USAID + "CurrentEntriesInMeet";
        CurrentEntriesInMeet.Value = "0";
        SwimmerPanel.Controls.Add(CurrentEntriesInMeet);

        DateTime BaseDate = Meet.Start.Subtract(new TimeSpan(1, 0, 0, 0));

        int SessionForSwimmer = 0;
        SwimmersTotalEntriesForMeet = 0;
        foreach (SwimTeamDatabase.SessionV2Row Session in Sessions)
        {
            SessionForSwimmer++;

            bool PreEntered = false;
            for (int i = 0; i < PreEntries.Count; i++)
                if (PreEntries[i].USAID == Swimmer.USAID)
                    if (PreEntries[i].IsPreEnteredInSession(Session.Session))
                    {
                        PreEntered = true;
                        break;
                    }
            if (PreEntered)
            {
                List<SwimTeamDatabase.EntriesV2Row> EntriesList = new List<SwimTeamDatabase.EntriesV2Row>();
                for (int i = 0; i < Entries.Count; i++)
                    if (Entries[i].AthleteID == AthleteID)
                        EntriesList.Add(Entries[i]);
                SwimmersTotalEntriesForMeet += EntriesList.Count;
                List<SwimTeamDatabase.BestTimesRow> BestTimesList = new List<SwimTeamDatabase.BestTimesRow>();
                for (int i = 0; i < BestTimes.Count; i++)
                    if (BestTimes[i].USAID == Swimmer.USAID)
                        BestTimesList.Add(BestTimes[i]);
                SessionTable table = new SessionTable(Session, Swimmer, Events, EntriesList, Meet, SessionForSwimmer,
                    BestTimesList, UsePreviousValues, ErrorStrings, this);

                SwimmerPanel.Controls.Add(table);
            }
        }



        CustomValidator SwimmerValidator = new CustomValidator();
        SwimmerValidator.ID = "Meet" + Meet.Meet + Swimmer.USAID + "SwimmerValidator";
        SwimmerValidator.ClientValidationFunction = "ValidateSwimmer";
        if (SwimmerPanel.Controls[3].Controls.Count >= 3)
            SwimmerValidator.ControlToValidate = SwimmerPanel.Controls[3].Controls[2].Controls[3].Controls[0].Controls[1].ID;
        SwimmerValidator.ErrorMessage = "";
        SwimmerValidator.ServerValidate += new ServerValidateEventHandler(ValidateSwimmer);
        SwimmerValidator.ValidateEmptyText = true;
        SwimmerPanel.Controls.Add(SwimmerValidator);

        return SwimmerPanel;
    }

    void ValidateSwimmer(object source, ServerValidateEventArgs args)
    {
        Panel SwimmerPanel = ((Panel)((CustomValidator)source).Parent);
        //Max entries
        Label MaxMeetEntriesLabel = ((Label)((Panel)SwimmerPanel.Parent).FindControl(SwimmerPanel.ID.Remove(SwimmerPanel.ID.Length - "Panel".Length) + "MaxMeetEntries"));
        int MaxEntries = 99;
        if (!int.TryParse(MaxMeetEntriesLabel.Text, out MaxEntries))
            MaxEntries = 99;
        int CurrentMeetEntries = 0;
        for (int i = 0; i < SwimmerPanel.Controls.Count; i++)
        {
            if (SwimmerPanel.Controls[i].GetType() == typeof(SessionTable))
            {
                int CurrentSessionEntries = 0;
                SessionTable Table = ((SessionTable)SwimmerPanel.Controls[i]);
                Label MaxSessionEntriesLable = ((Label)((Panel)Table.Rows[0].Cells[0].Controls[2]).Controls[1]);
                int MaxSessionEntries = 99;
                if (!int.TryParse(MaxSessionEntriesLable.Text, out MaxSessionEntries))
                    MaxSessionEntries = 99;
                for (int j = 2; j < Table.Rows.Count; j++)
                {
                    CheckBox EnteredCheckBox = ((CheckBox)((Panel)Table.Rows[j].Cells[3].Controls[0]).Controls[0]);
                    if (EnteredCheckBox.Checked)
                    {
                        CurrentSessionEntries++;
                        CurrentMeetEntries++;
                        if (CurrentSessionEntries > MaxSessionEntries)
                        {
                            args.IsValid = false;
                            Panel MainAccordion = ((Panel)SwimmerPanel.Parent);
                            String EventNumber = ((Label)Table.Rows[j].Cells[0].Controls[0]).Text;
                            String SwimmerName = "";
                            for (int k = 0; k < MainAccordion.Controls.Count; k++)
                            {
                                if (MainAccordion.Controls[k] == SwimmerPanel)
                                {
                                    Literal Header = ((Literal)MainAccordion.Controls[k - 1]);
                                    int index = Header.Text.IndexOf("<a href=\"#\">");
                                    String temp = Header.Text.Remove(0, index);
                                    temp = temp.Remove(0, "<a href=\"#\">".Length);
                                    index = temp.IndexOf("</a");
                                    SwimmerName = temp.Remove(index);
                                    break;
                                }
                            }

                            bool PreviousErrorAddedTo = false;
                            for (int k = 0; k < this.ErrorStrings.Count; k++)
                            {
                                if (this.ErrorStrings[k].Contains(SwimmerName))
                                {
                                    PreviousErrorAddedTo = true;
                                    if (!this.ErrorStrings[k].Contains("session"))
                                    {
                                        if (this.ErrorStrings[k].EndsWith("."))
                                            this.ErrorStrings[k] = this.ErrorStrings[k].Remove(this.ErrorStrings[k].Length - 1);
                                        this.ErrorStrings[k] = this.ErrorStrings[k] + ", over a session entry limit.";
                                        break;
                                    }
                                }
                            }
                            if (!PreviousErrorAddedTo)
                            {
                                this.ErrorStrings.Add("Problem with " + SwimmerName + ": over a session entry limit.");
                            }

                            break;
                        }

                    }
                }
            }

            if (CurrentMeetEntries > MaxEntries)
            {
                args.IsValid = false;
                Panel MainAccordion = ((Panel)SwimmerPanel.Parent);
                String SwimmerName = "";
                for (int k = 0; k < MainAccordion.Controls.Count; k++)
                {
                    if (MainAccordion.Controls[k] == SwimmerPanel)
                    {
                        Literal Header = ((Literal)MainAccordion.Controls[k - 1]);
                        int index = Header.Text.IndexOf("<a href=\"#\">");
                        String temp = Header.Text.Remove(0, index);
                        temp = temp.Remove(0, "<a href=\"#\">".Length);
                        index = temp.IndexOf("</a");
                        SwimmerName = temp.Remove(index);
                        break;
                    }
                }

                bool PreviousErrorAddedTo = false;
                for (int k = 0; k < this.ErrorStrings.Count; k++)
                {
                    if (this.ErrorStrings[k].Contains(SwimmerName))
                    {
                        PreviousErrorAddedTo = true;
                        if (!this.ErrorStrings[k].Contains("meet entry"))
                        {
                            if (this.ErrorStrings[k].EndsWith("."))
                                this.ErrorStrings[k] = this.ErrorStrings[k].Remove(this.ErrorStrings[k].Length - 1);
                            this.ErrorStrings[k] = this.ErrorStrings[k] + ", over meet entry limit.";
                            break;
                        }
                    }
                }
                if (!PreviousErrorAddedTo)
                {
                    this.ErrorStrings.Add("Problem with " + SwimmerName + ": over meet entry limit.");
                }
                break;
            }
        }
    }



    private class SessionTable : Table
    {
        private Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.EntriesV2Row> EntriesDictionary;
        private SwimTeamDatabase.SessionV2Row Session;
        private SwimTeamDatabase.SwimmersRow Swimmer;
        private SwimTeamDatabase.MeetsV2Row Meet;
        private static List<System.Drawing.Color> ColorList;
        private List<SwimTeamDatabase.MeetEventsRow> MeetEvents;
        private Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow> YardsBestTimesDictionary;
        private Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow> MetersBestTimesDictionary;
        private Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow> SCMBestTimesDictionary;
        private bool UsePreviousValues;
        private List<String> ErrorStrings;
        private Page _Page;

        public SessionTable(SwimTeamDatabase.SessionV2Row Session, SwimTeamDatabase.SwimmersRow Swimmer,
            SwimTeamDatabase.MeetEventsDataTable MeetEvents, List<SwimTeamDatabase.EntriesV2Row> Entries, SwimTeamDatabase.MeetsV2Row Meet,
            int SessionForSwimmer, List<SwimTeamDatabase.BestTimesRow> BestTimesList, bool UsePreviousValues, List<String> ErrorStrings,
            Page Page)
        {
            if (ColorList == null)
                CreateColorList();
            this._Page = Page;
            this.ErrorStrings = ErrorStrings;
            this.UsePreviousValues = UsePreviousValues;
            this.Session = Session;
            this.Swimmer = Swimmer;
            this.Meet = Meet;
            this.EntriesDictionary = new Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.EntriesV2Row>();
            this.YardsBestTimesDictionary = new Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow>();
            this.MetersBestTimesDictionary = new Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow>();
            this.SCMBestTimesDictionary = new Dictionary<SwimTeamDatabase.MeetEventsRow, SwimTeamDatabase.BestTimesRow>();
            this.MeetEvents = new List<SwimTeamDatabase.MeetEventsRow>();
            this.CellPadding = 0;
            this.CellSpacing = 0;

            int Age = MeetEventHelper.AgeOnDate(Meet.Start, Swimmer.Birthday);

            //Match Events and Entries
            for (int i = 0; i < MeetEvents.Count; i++)
            {
                int MinAge = MeetEventHelper.GetMinAgeFromAgeString(MeetEvents[i].AgeCode.ToString());
                int MaxAge = MeetEventHelper.GetMaxAgeFromAgeString(MeetEvents[i].AgeCode.ToString());

                if (Age <= MaxAge && Age >= MinAge && ((Swimmer.Gender.ToUpper() == MeetEvents[i].SexCode.ToUpper()) ||
                    MeetEvents[i].SexCode.ToUpper() == "X") &&
                    MeetEvents[i].SessionID == Session.Session)
                {
                    int EntryIndex = -1;
                    for (int j = 0; j < Entries.Count; j++)
                        if (Entries[j].EventID == MeetEvents[i].EventID)
                        {
                            EntryIndex = j;
                            break;
                        }

                    if (EntryIndex == -1)
                        EntriesDictionary.Add(MeetEvents[i], null);
                    else
                        EntriesDictionary.Add(MeetEvents[i], Entries[EntryIndex]);
                    this.MeetEvents.Add(MeetEvents[i]);

                    int YardsBestTimeIndex = -1, MetersBestTimeIndex = -1, SCMBestTimeIndex = -1;
                    for (int j = 0; j < BestTimesList.Count; j++)
                    {
                        if (BestTimesList[j].Course.ToUpper() == MeetEvents[i].Course.ToUpper())
                        {
                            if ((BestTimesList[j].Distance == MeetEvents[i].Distance) &&
                            (BestTimesList[j].Stroke == MeetEvents[i].StrokeCode))
                                if (BestTimesList[j].Course.ToUpper() == "Y")
                                {
                                    YardsBestTimeIndex = j;
                                    //break;
                                }
                                else if (BestTimesList[j].Course.ToUpper() == "L")
                                {
                                    MetersBestTimeIndex = j;
                                    //break;
                                }
                                else
                                {
                                    SCMBestTimeIndex = j;
                                    //break;
                                }
                        }
                        else
                        {
                            if ((MeetEvents[i].StrokeCode == 2) || (MeetEvents[i].StrokeCode == 3) || (MeetEvents[i].StrokeCode == 4) ||
                                (MeetEvents[i].StrokeCode == 5))
                            {
                                if ((BestTimesList[j].Distance == MeetEvents[i].Distance) &&
                                    (BestTimesList[j].Stroke == MeetEvents[i].StrokeCode))
                                    if (BestTimesList[j].Course.ToUpper() == "Y")
                                    {
                                        YardsBestTimeIndex = j;
                                        //break;
                                    }
                                    else if (BestTimesList[j].Course.ToUpper() == "L")
                                    {
                                        MetersBestTimeIndex = j;
                                        //break;
                                    }
                                    else
                                    {
                                        SCMBestTimeIndex = j;
                                        //break;
                                    }
                            }
                            else
                            {
                                if (MeetEvents[i].Distance < 400)
                                {
                                    if ((BestTimesList[j].Distance == MeetEvents[i].Distance) &&
                                    (BestTimesList[j].Stroke == MeetEvents[i].StrokeCode))
                                        if (BestTimesList[j].Course.ToUpper() == "Y")
                                        {
                                            YardsBestTimeIndex = j;
                                            //break;
                                        }
                                        else if (BestTimesList[j].Course.ToUpper() == "L")
                                        {
                                            MetersBestTimeIndex = j;
                                            //break;
                                        }
                                        else
                                        {
                                            SCMBestTimeIndex = j;
                                            //break;
                                        }
                                }
                                else
                                {
                                    int BestTimeDistance = -1;
                                    if (MeetEvents[i].Course.ToUpper() == "L" || MeetEvents[i].Course.ToUpper() == "S")
                                    {
                                        if (BestTimesList[j].Course == "Y")
                                        {
                                            if (BestTimesList[j].Distance == 500)
                                                BestTimeDistance = 400;
                                            else if (BestTimesList[j].Distance == 1000)
                                                BestTimeDistance = 800;
                                            else if (BestTimesList[j].Distance == 1650)
                                                BestTimeDistance = 1500;
                                            else
                                                BestTimeDistance = BestTimesList[j].Distance;
                                        }
                                        else
                                            BestTimeDistance = BestTimesList[j].Distance;
                                    }
                                    else
                                    {
                                        if (BestTimesList[j].Course.ToUpper() == "L" || BestTimesList[j].Course.ToUpper() == "S")
                                        {
                                            if (BestTimesList[j].Distance == 400)
                                                BestTimeDistance = 500;
                                            else if (BestTimesList[j].Distance == 800)
                                                BestTimeDistance = 1000;
                                            else if (BestTimesList[j].Distance == 1500)
                                                BestTimeDistance = 1650;
                                            else
                                                BestTimeDistance = BestTimesList[j].Distance;
                                        }
                                        else
                                            BestTimeDistance = BestTimesList[j].Distance;
                                    }

                                    if ((BestTimeDistance == MeetEvents[i].Distance) &&
                                    (BestTimesList[j].Stroke == MeetEvents[i].StrokeCode))
                                        if (BestTimesList[j].Course.ToUpper() == "Y")
                                        {
                                            YardsBestTimeIndex = j;
                                            //break;
                                        }
                                        else if (BestTimesList[j].Course.ToUpper() == "L")
                                        {
                                            MetersBestTimeIndex = j;
                                            //break;
                                        }
                                        else
                                        {
                                            SCMBestTimeIndex = j;
                                            //break;
                                        }
                                }
                            }
                        }
                    }
                    //if ((BestTimesList[j].Distance == MeetEvents[i].Distance) &&
                    //    (BestTimesList[j].Stroke == MeetEvents[i].StrokeCode))
                    //    if ((BestTimesList[j].Course == "Y"))
                    //    {
                    //        YardsBestTimeIndex = j;
                    //        break;
                    //    }

                    if (YardsBestTimeIndex == -1)
                        YardsBestTimesDictionary.Add(MeetEvents[i], null);
                    else
                        YardsBestTimesDictionary.Add(MeetEvents[i], BestTimesList[YardsBestTimeIndex]);
                    if (MetersBestTimeIndex == -1)
                        MetersBestTimesDictionary.Add(MeetEvents[i], null);
                    else
                        MetersBestTimesDictionary.Add(MeetEvents[i], BestTimesList[MetersBestTimeIndex]);
                    if (SCMBestTimeIndex == -1)
                        SCMBestTimesDictionary.Add(MeetEvents[i], null);
                    else
                        SCMBestTimesDictionary.Add(MeetEvents[i], BestTimesList[SCMBestTimeIndex]);
                }
            }

            this.Width = new Unit("874px");
            this.CreateHeaderRow(SessionForSwimmer);
            this.CreateRows();
        }


        private static void CreateColorList()
        {
            ColorList = new List<System.Drawing.Color>();
            ColorList.Add(System.Drawing.Color.Yellow);
        }

        private void CreateHeaderRow(int SessionForSwimmer)
        {
            Label SessionHeaderLabel = new Label();
            SessionHeaderLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "Header";
            SessionHeaderLabel.Text = "Session " + Session.Session + ": " + Meet.Start.Subtract(new TimeSpan(1, 0, 0, 0))
                .Add(new TimeSpan(Session.Day, 0, 0, 0)).ToString("dddd MMMM, d") + " " + Session.StartTime;
            if (Session.AM)
                SessionHeaderLabel.Text += " AM";
            else
                SessionHeaderLabel.Text += " PM";
            if (Session.Guess)
                SessionHeaderLabel.Text += "  <span style=\"font-style:italic;\">Start time is a guess.</span>";
            SessionHeaderLabel.Style.Add(HtmlTextWriterStyle.MarginLeft, "15px");
            SessionHeaderLabel.Style.Add("float", "left");
            TableCell HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 9;
            HeaderCell.Controls.Add(SessionHeaderLabel);
            int ColorIndex = (SessionForSwimmer - 1) % ColorList.Count;
            HeaderCell.BackColor = ColorList[ColorIndex];
            HeaderCell.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            HeaderCell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");

            Panel LeftFloatPanel = new Panel();
            LeftFloatPanel.Style.Add("float", "left");
            LeftFloatPanel.Style.Add(HtmlTextWriterStyle.MarginLeft, "50px");

            Label PrelimFinalLabel = new Label();
            PrelimFinalLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "PrelimFinal";
            if (Session.PrelimFinal)
                PrelimFinalLabel.Text = "This is a Prelims Session";
            else
                PrelimFinalLabel.Text = "";
            PrelimFinalLabel.Style.Add(HtmlTextWriterStyle.FontStyle, "italic");

            LeftFloatPanel.Controls.Add(PrelimFinalLabel);
            HeaderCell.Controls.Add(LeftFloatPanel);

            Panel RightFloatPanel = new Panel();
            RightFloatPanel.Style.Add("float", "right");
            RightFloatPanel.Style.Add(HtmlTextWriterStyle.MarginRight, "15px");

            Label MaxEntriesDescriptorLabel = new Label();
            MaxEntriesDescriptorLabel.Text = "Max Entries for Session: ";
            RightFloatPanel.Controls.Add(MaxEntriesDescriptorLabel);

            Label MaxEntriesLabel = new Label();
            MaxEntriesLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "MaxEntries";
            MaxEntriesLabel.Text = Session.MaxInd.ToString();
            RightFloatPanel.Controls.Add(MaxEntriesLabel);

            HiddenField CurrentSessionEntriesHiddenField = new HiddenField();
            CurrentSessionEntriesHiddenField.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "CurrentSessionEntries";
            CurrentSessionEntriesHiddenField.Value = "0";
            RightFloatPanel.Controls.Add(CurrentSessionEntriesHiddenField);

            HeaderCell.Controls.Add(RightFloatPanel);

            TableRow Row = new TableRow();
            Row.Cells.Add(HeaderCell);
            this.Rows.Add(Row);
        }

        private void CreateRows()
        {
            TableRow Row = new TableRow();

            Label EventNameLabel = new Label();
            EventNameLabel.Text = "Event";
            EventNameLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            TableCell EventNameCell = new TableCell();
            EventNameCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            EventNameCell.ColumnSpan = 2;
            EventNameCell.Controls.Add(EventNameLabel);
            Row.Cells.Add(EventNameCell);

            Label AgeGroupLabel = new Label();
            AgeGroupLabel.Text = "Age Group";
            AgeGroupLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            AgeGroupLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            TableCell AgeGroupCell = new TableCell();
            AgeGroupCell.Controls.Add(AgeGroupLabel);
            Row.Cells.Add(AgeGroupCell);

            TableCell BlankCell = new TableCell();
            Row.Cells.Add(BlankCell);

            Label BestTimeLabel = new Label();
            BestTimeLabel.Text = "Best Time";
            BestTimeLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            TableCell BestTimeCell = new TableCell();
            BestTimeCell.Width = new Unit("90px");
            BestTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            BestTimeCell.Controls.Add(BestTimeLabel);
            Row.Cells.Add(BestTimeCell);

            Label FastCutTimeLabel = new Label();
            FastCutTimeLabel.Text = "Faster than:";
            FastCutTimeLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            TableCell FastCutTimeCell = new TableCell();
            FastCutTimeCell.Width = new Unit("90px");
            FastCutTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            FastCutTimeCell.Controls.Add(FastCutTimeLabel);
            Row.Cells.Add(FastCutTimeCell);

            Label SlowCutTimeLabel = new Label();
            SlowCutTimeLabel.Text = "Slower than:";
            SlowCutTimeLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            TableCell SlowCutTimeCell = new TableCell();
            SlowCutTimeCell.Width = new Unit("90px");
            SlowCutTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            SlowCutTimeCell.Controls.Add(SlowCutTimeLabel);
            Row.Cells.Add(SlowCutTimeCell);

            Label BonusLabel = new Label();
            BonusLabel.Text = "Bonus";
            BonusLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            BonusLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            TableCell BonusCell = new TableCell();
            BonusCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            BonusCell.Controls.Add(BonusLabel);
            Row.Cells.Add(BonusCell);

            Label ExhibitionLabel = new Label();
            ExhibitionLabel.Text = "Exhibition";
            ExhibitionLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            ExhibitionLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            TableCell ExhibitionCell = new TableCell();
            ExhibitionCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            ExhibitionCell.Controls.Add(ExhibitionLabel);
            Row.Cells.Add(ExhibitionCell);

            this.Rows.Add(Row);

            for (int i = 0; i < this.MeetEvents.Count; i++)
            {
                TableRow EventRow = new TableRow();

                Label EventNumberLabel = new Label();
                EventNumberLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "EventNumber" + i;
                EventNumberLabel.Text = this.MeetEvents[i].EventNumber.ToString();
                if (!this.MeetEvents[i].IsEventNumberExtendedNull())
                    if (!String.IsNullOrWhiteSpace(this.MeetEvents[i].EventNumberExtended))
                        EventNumberLabel.Text += this.MeetEvents[i].EventNumberExtended;
                EventNumberLabel.Style.Add(HtmlTextWriterStyle.MarginRight, "5px;");
                TableCell EventNumberCell = new TableCell();
                EventNumberCell.Width = new Unit("24px");
                EventNumberCell.Controls.Add(EventNumberLabel);
                EventRow.Cells.Add(EventNumberCell);

                Label SwimmerDistanceLabel = new Label();
                SwimmerDistanceLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "Event" + i;
                SwimmerDistanceLabel.Text = this.MeetEvents[i].Distance + " ";

                Label SwimmerCourseLabel = new Label();
                SwimmerCourseLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "Distance" + i;
                if (this.MeetEvents[i].Course.ToUpper() == "Y")
                    SwimmerCourseLabel.Text += "Yard";
                else if (this.MeetEvents[i].Course.ToUpper() == "L")
                    SwimmerCourseLabel.Text += "Meter";
                else
                    SwimmerCourseLabel.Text += "SCM";

                Label swimmerStrokeLabel = new Label();
                swimmerStrokeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "Stroke" + i;
                switch (this.MeetEvents[i].StrokeCode)
                {
                    case 1:
                        swimmerStrokeLabel.Text += " Free";
                        break;
                    case 2:
                        swimmerStrokeLabel.Text += " Back";
                        break;
                    case 3:
                        swimmerStrokeLabel.Text += " Breast";
                        break;
                    case 4:
                        swimmerStrokeLabel.Text += " Fly";
                        break;
                    case 5:
                        swimmerStrokeLabel.Text += " IM";
                        break;
                }
                TableCell SwimmerEventNameCell = new TableCell();
                SwimmerEventNameCell.Width = new Unit("146px");
                SwimmerEventNameCell.Controls.Add(SwimmerDistanceLabel);
                SwimmerEventNameCell.Controls.Add(SwimmerCourseLabel);
                SwimmerEventNameCell.Controls.Add(swimmerStrokeLabel);
                EventRow.Cells.Add(SwimmerEventNameCell);

                int MinAge = MeetEventHelper.GetMinAgeFromAgeString(this.MeetEvents[i].AgeCode.ToString());
                int MaxAge = MeetEventHelper.GetMaxAgeFromAgeString(this.MeetEvents[i].AgeCode.ToString());

                Label SwimmerAgeLabel = new Label();
                SwimmerAgeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "AgeGender" + i;
                if (MinAge == 0 && MaxAge == 99)
                    SwimmerAgeLabel.Text = "Open";
                else if (MinAge == 0 && MaxAge > 0)
                    SwimmerAgeLabel.Text = MaxAge + " and Under";
                else if (MaxAge == 99 && MinAge < 99)
                    SwimmerAgeLabel.Text = MinAge + " and Over";
                else if (MaxAge == MinAge)
                    SwimmerAgeLabel.Text = MaxAge + " year old";
                else
                    SwimmerAgeLabel.Text = MinAge + "-" + MaxAge;

                Label SwimmerGenderLabel = new Label();
                SwimmerGenderLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "Gender" + i;
                if (this.MeetEvents[i].SexCode.ToUpper() == "F")
                    SwimmerAgeLabel.Text += " Girls";
                else if (this.MeetEvents[i].SexCode.ToUpper() == "M")
                    SwimmerAgeLabel.Text += " Boys";
                else if (this.MeetEvents[i].SexCode.ToUpper() == "X")
                    SwimmerAgeLabel.Text += " Mixed";
                TableCell SwimmerAgeCell = new TableCell();
                SwimmerAgeCell.Width = new Unit("186px");
                SwimmerAgeCell.Controls.Add(SwimmerAgeLabel);
                SwimmerAgeCell.Controls.Add(SwimmerGenderLabel);
                EventRow.Cells.Add(SwimmerAgeCell);

                //Panel EntryControlPanel = new Panel();
                Panel EntryControlPanel = this.GetEventControlPanel(Swimmer, Session, Meet, i, Meet.Course.Substring(0, 1), this.MeetEvents[i]);
                EntryControlPanel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                TableCell EntryControlCell = new TableCell();
                EntryControlCell.Width = new Unit("158px");
                EntryControlCell.Controls.Add(EntryControlPanel);
                EventRow.Cells.Add(EntryControlCell);


                Panel FastCutTimesPanel = new Panel();
                Label SwimmerFastYardCutTimeLabel = new Label();
                Label SwimmerFastMeterCutTimeLabel = new Label();
                Label SwimmerFastSCMCutTimeLabel = new Label();
                SwimmerFastYardCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "FastYardCutTimes" + i;
                SwimmerFastMeterCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "FastMeterCutTimes" + i;
                SwimmerFastSCMCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "FastSCMCutTimes" + i;
                List<String> FastCutTimes = this.GetFastCutTimesString(this.MeetEvents[i]);
                //for (int j = 0; j < CutTimes.Count; j++)
                //    if (String.IsNullOrWhiteSpace(SwimmerYardCutTimeLabel.Text))
                //        SwimmerYardCutTimeLabel.Text = CutTimes[j];
                //    else
                //        SwimmerYardCutTimeLabel.Text += "<br />" + CutTimes[j];
                foreach (String st in FastCutTimes)
                {
                    if (st.EndsWith("Y"))
                        SwimmerFastYardCutTimeLabel.Text = st;
                    else if (st.EndsWith("L"))
                        SwimmerFastMeterCutTimeLabel.Text = st;
                    else if (st.EndsWith("S"))
                        SwimmerFastSCMCutTimeLabel.Text = st;
                }
                if (string.IsNullOrWhiteSpace(SwimmerFastYardCutTimeLabel.Text))
                    SwimmerFastYardCutTimeLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerFastMeterCutTimeLabel.Text))
                    SwimmerFastMeterCutTimeLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerFastSCMCutTimeLabel.Text))
                    SwimmerFastSCMCutTimeLabel.Text = "";
                foreach (String st in FastCutTimes)
                {
                    if (st.EndsWith("Y"))
                    {
                        if (FastCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            FastCutTimesPanel.Controls.Add(li);
                        }
                        FastCutTimesPanel.Controls.Add(SwimmerFastYardCutTimeLabel);
                    }
                    else if (st.EndsWith("L"))
                    {
                        if (FastCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            FastCutTimesPanel.Controls.Add(li);
                        }
                        FastCutTimesPanel.Controls.Add(SwimmerFastMeterCutTimeLabel);
                    }
                    else if (st.EndsWith("S"))
                    {
                        if (FastCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            FastCutTimesPanel.Controls.Add(li);
                        }
                        FastCutTimesPanel.Controls.Add(SwimmerFastSCMCutTimeLabel);
                    }
                }
                TableCell SwimmerFastCutTimeCell = new TableCell();
                SwimmerFastCutTimeCell.Width = new Unit("81px");
                SwimmerFastCutTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                SwimmerFastCutTimeCell.Controls.Add(FastCutTimesPanel);

                Panel SlowCutTimesPanel = new Panel();
                Label SwimmerSlowYardCutTimeLabel = new Label();
                Label SwimmerSlowMeterCutTimeLabel = new Label();
                Label SwimmerSlowSCMCutTimeLabel = new Label();
                SwimmerSlowYardCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "SlowYardCutTimes" + i;
                SwimmerSlowMeterCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "SlowMeterCutTimes" + i;
                SwimmerSlowSCMCutTimeLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "SlowSCMCutTimes" + i;
                List<String> SlowCutTimes = this.GetSlowCutTimesString(this.MeetEvents[i]);
                //for (int j = 0; j < CutTimes.Count; j++)
                //    if (String.IsNullOrWhiteSpace(SwimmerYardCutTimeLabel.Text))
                //        SwimmerYardCutTimeLabel.Text = CutTimes[j];
                //    else
                //        SwimmerYardCutTimeLabel.Text += "<br />" + CutTimes[j];
                foreach (String st in SlowCutTimes)
                {
                    if (st.EndsWith("Y"))
                        SwimmerSlowYardCutTimeLabel.Text = st;
                    else if (st.EndsWith("L"))
                        SwimmerSlowMeterCutTimeLabel.Text = st;
                    else if (st.EndsWith("S"))
                        SwimmerSlowSCMCutTimeLabel.Text = st;
                }
                if (string.IsNullOrWhiteSpace(SwimmerSlowYardCutTimeLabel.Text))
                    SwimmerSlowYardCutTimeLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerSlowMeterCutTimeLabel.Text))
                    SwimmerSlowMeterCutTimeLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerSlowSCMCutTimeLabel.Text))
                    SwimmerSlowSCMCutTimeLabel.Text = "";
                foreach (String st in SlowCutTimes)
                {
                    if (st.EndsWith("Y"))
                    {
                        if (SlowCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            SlowCutTimesPanel.Controls.Add(li);
                        }
                        SlowCutTimesPanel.Controls.Add(SwimmerSlowYardCutTimeLabel);
                    }
                    else if (st.EndsWith("L"))
                    {
                        if (SlowCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            SlowCutTimesPanel.Controls.Add(li);
                        }
                        SlowCutTimesPanel.Controls.Add(SwimmerSlowMeterCutTimeLabel);
                    }
                    else if (st.EndsWith("S"))
                    {
                        if (SlowCutTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            SlowCutTimesPanel.Controls.Add(li);
                        }
                        SlowCutTimesPanel.Controls.Add(SwimmerSlowSCMCutTimeLabel);
                    }
                }
                TableCell SwimmerSlowCutTimeCell = new TableCell();
                SwimmerSlowCutTimeCell.Width = new Unit("81px");
                SwimmerSlowCutTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                SwimmerSlowCutTimeCell.Controls.Add(SlowCutTimesPanel);


                Panel BestTimesPanel = new Panel();
                List<String> BestTimes = this.GetBestTimesString(this.MeetEvents[i]);
                Label SwimmerYardBestTimesLabel = new Label();
                Label SwimmerMeterBestTimesLabel = new Label();
                Label SwimmerSCMBestTimesLabel = new Label();
                SwimmerYardBestTimesLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "YardBestTimes" + i;
                SwimmerMeterBestTimesLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "MeterBestTimes" + i;
                SwimmerSCMBestTimesLabel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "SCMBestTimes" + i;
                //for (int j = 0; j < BestTimes.Count; j++)
                //    if (String.IsNullOrWhiteSpace(SwimmerYardBestTimesLabel.Text))
                //        SwimmerYardBestTimesLabel.Text = BestTimes[j];
                //    else
                //        SwimmerYardBestTimesLabel.Text += "<br />" + BestTimes[j];
                if (BestTimes[0] == "NT" && BestTimes.Count == 1)
                {
                    if (Meet.Course.ToUpper().StartsWith("Y"))
                        SwimmerYardBestTimesLabel.Text = BestTimes[0];
                    else if (Meet.Course.ToUpper().StartsWith("L"))
                        SwimmerMeterBestTimesLabel.Text = BestTimes[0];
                    else if (Meet.Course.ToUpper().StartsWith("S"))
                        SwimmerSCMBestTimesLabel.Text = BestTimes[0];
                }
                else
                    foreach (String st in BestTimes)
                    {
                        if (st.EndsWith("Y"))
                            SwimmerYardBestTimesLabel.Text = st;
                        else if (st.EndsWith("L"))
                            SwimmerMeterBestTimesLabel.Text = st;
                        else if (st.EndsWith("S"))
                            SwimmerSCMBestTimesLabel.Text = st;
                    }
                if (String.IsNullOrWhiteSpace(SwimmerYardBestTimesLabel.Text))
                    SwimmerYardBestTimesLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerMeterBestTimesLabel.Text))
                    SwimmerMeterBestTimesLabel.Text = "";
                if (string.IsNullOrWhiteSpace(SwimmerSCMBestTimesLabel.Text))
                    SwimmerFastSCMCutTimeLabel.Text = "";

                if (FastCutTimes.Count == 0 && SlowCutTimes.Count == 0 && BestTimes.Count == 1 && BestTimes[0] == "NT")
                {
                    if (!String.IsNullOrEmpty(SwimmerYardBestTimesLabel.Text))
                        BestTimesPanel.Controls.Add(SwimmerYardBestTimesLabel);
                    else if (!String.IsNullOrEmpty(SwimmerMeterBestTimesLabel.Text))
                        BestTimesPanel.Controls.Add(SwimmerMeterBestTimesLabel);
                    if (!String.IsNullOrEmpty(SwimmerSCMBestTimesLabel.Text))
                        BestTimesPanel.Controls.Add(SwimmerSCMBestTimesLabel);
                }

                List<String> IteratorList;
                bool IteratorIsCutTime = false;
                if (FastCutTimes.Count == 0 && SlowCutTimes.Count == 0)
                    IteratorList = BestTimes;
                else
                {
                    IteratorList = new List<string>();
                    for (int j = 0; j < FastCutTimes.Count; j++)
                    {
                        String EndsWidth = FastCutTimes[j].Substring(FastCutTimes[j].Length - 1).ToUpper();
                        if (EndsWidth == "Y" || EndsWidth == "L" || EndsWidth == "S")
                            if (!IteratorList.Contains(EndsWidth))
                                IteratorList.Add(EndsWidth);
                    }
                    for (int j = 0; j < SlowCutTimes.Count; j++)
                    {
                        String EndsWidth = SlowCutTimes[j].Substring(SlowCutTimes[j].Length - 1).ToUpper();
                        if (EndsWidth == "Y" || EndsWidth == "L" || EndsWidth == "S")
                            if (!IteratorList.Contains(EndsWidth))
                                IteratorList.Add(EndsWidth);
                    }
                    //IteratorList = FastCutTimes;
                    IteratorIsCutTime = true;
                }

                foreach (String st in IteratorList)
                {
                    if (st.EndsWith("Y"))
                    {
                        if (BestTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            BestTimesPanel.Controls.Add(li);
                        }
                        if (IteratorIsCutTime && String.IsNullOrWhiteSpace(SwimmerYardBestTimesLabel.Text))
                            SwimmerYardBestTimesLabel.Text = "NT";
                        BestTimesPanel.Controls.Add(SwimmerYardBestTimesLabel);

                    }
                    else if (st.EndsWith("L"))
                    {
                        if (BestTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            BestTimesPanel.Controls.Add(li);
                        }
                        if (IteratorIsCutTime && String.IsNullOrWhiteSpace(SwimmerMeterBestTimesLabel.Text))
                            SwimmerMeterBestTimesLabel.Text = "NT";
                        BestTimesPanel.Controls.Add(SwimmerMeterBestTimesLabel);
                    }
                    else if (st.EndsWith("S"))
                    {
                        if (BestTimesPanel.Controls.Count > 0)
                        {
                            Literal li = new Literal();
                            li.Text = "<br />";
                            BestTimesPanel.Controls.Add(li);
                        }
                        if (IteratorIsCutTime && String.IsNullOrWhiteSpace(SwimmerSCMBestTimesLabel.Text))
                            SwimmerSCMBestTimesLabel.Text = "NT";
                        BestTimesPanel.Controls.Add(SwimmerSCMBestTimesLabel);
                    }
                }

                TableCell SwimmerBestTimeCell = new TableCell();
                SwimmerBestTimeCell.Width = new Unit("92px");
                SwimmerBestTimeCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                SwimmerBestTimeCell.Controls.Add(BestTimesPanel);
                EventRow.Cells.Add(SwimmerBestTimeCell);



                EventRow.Cells.Add(SwimmerFastCutTimeCell);
                EventRow.Cells.Add(SwimmerSlowCutTimeCell);


                SwimTeamDatabase.EntriesV2Row Entry = this.EntriesDictionary[this.MeetEvents[i]];

                CheckBox BonusCheckBox = new CheckBox();
                BonusCheckBox.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "BonusBox" + i;
                BonusCheckBox.Attributes.Add("onclick", "SetAsBonus(this);");
                if (!UsePreviousValues && Entry != null)
                    if (Entry.Bonus)
                        BonusCheckBox.Checked = true;
                TableCell SwimmerBonusCell = new TableCell();
                SwimmerBonusCell.Width = new Unit("59px");
                SwimmerBonusCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                SwimmerBonusCell.Controls.Add(BonusCheckBox);
                EventRow.Cells.Add(SwimmerBonusCell);

                CheckBox SwimmerExhibitionCheckBox = new CheckBox();
                SwimmerExhibitionCheckBox.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "ExhibBox" + i;
                SwimmerExhibitionCheckBox.Attributes.Add("onclick", "ExhibClicked(this);");
                if (!UsePreviousValues && Entry != null)
                    if (Entry.Exhibition)
                        SwimmerExhibitionCheckBox.Checked = true;
                TableCell SwimmerExhibitionCell = new TableCell();
                SwimmerExhibitionCell.Width = new Unit("94px");
                SwimmerExhibitionCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                SwimmerExhibitionCell.Controls.Add(SwimmerExhibitionCheckBox);
                EventRow.Cells.Add(SwimmerExhibitionCell);

                EventRow.BackColor = GetBackgroundColor(EventRow.BackColor, BestTimesPanel, FastCutTimesPanel, SlowCutTimesPanel);

                this.Rows.Add(EventRow);
            }

            for (int i = 1; i < this.Rows.Count - 1; i++)
            {
                for (int j = 0; j < this.Rows[i].Cells.Count; j++)
                {
                    this.Rows[i].Cells[j].Style.Add("border-bottom", "1px dotted black !important");
                }
            }
        }

        private static System.Drawing.Color GetBackgroundColor(System.Drawing.Color CurrentColor, Panel BestTimesPanel, Panel FastCutTimesPanel, Panel SlowCutTimesPanel)
        {
            HyTekTime YardTime = GetTime("Y", BestTimesPanel);
            HyTekTime MeterTime = GetTime("L", BestTimesPanel);
            HyTekTime SCMTime = GetTime("S", BestTimesPanel);

            HyTekTime YardFastCut = GetTime("Y", FastCutTimesPanel);
            HyTekTime MeterFastCut = GetTime("L", FastCutTimesPanel);
            HyTekTime SCMFastCut = GetTime("S", FastCutTimesPanel);

            HyTekTime YardSlowCut = GetTime("Y", SlowCutTimesPanel);
            HyTekTime MeterSlowCut = GetTime("L", SlowCutTimesPanel);
            HyTekTime SCMSlowCut = GetTime("S", SlowCutTimesPanel);

            if (YardFastCut == HyTekTime.NOTIME && MeterFastCut == HyTekTime.NOTIME && SCMFastCut == HyTekTime.NOTIME &&
                YardSlowCut == HyTekTime.NOTIME && MeterSlowCut == HyTekTime.NOTIME && SCMSlowCut == HyTekTime.NOTIME)
                return CurrentColor;

            bool YardWithinCuts = false, MeterWithinCuts = false, SCMWithinCuts = false;
            if (YardTime == HyTekTime.NOTIME && YardFastCut != HyTekTime.NOTIME)
                YardWithinCuts = false;
            else
            {
                if (!(YardTime == HyTekTime.NOTIME && YardFastCut == HyTekTime.NOTIME && YardSlowCut == HyTekTime.NOTIME))
                    YardWithinCuts = IsTimeWithinCuts(YardTime, YardFastCut, YardSlowCut);
            }
            if (MeterTime == HyTekTime.NOTIME && MeterFastCut != HyTekTime.NOTIME)
                MeterWithinCuts = false;
            else
            {
                if (!(MeterTime == HyTekTime.NOTIME && MeterFastCut == HyTekTime.NOTIME && MeterSlowCut == HyTekTime.NOTIME))
                    MeterWithinCuts = IsTimeWithinCuts(MeterTime, MeterFastCut, MeterSlowCut);
            }
            if (SCMTime == HyTekTime.NOTIME && SCMFastCut != HyTekTime.NOTIME)
                SCMWithinCuts = false;
            else
            {
                if (!(SCMTime == HyTekTime.NOTIME && SCMFastCut == HyTekTime.NOTIME && SCMSlowCut == HyTekTime.NOTIME))
                    SCMWithinCuts = IsTimeWithinCuts(SCMTime, SCMFastCut, SCMSlowCut);
            }

            if (YardWithinCuts || MeterWithinCuts || SCMWithinCuts)
                return CurrentColor;
            else
                return System.Drawing.Color.LightGray;
        }

        private static HyTekTime GetTime(String Course, Panel TimePanel)
        {
            Course = Course.Trim().ToUpper();

            for (int i = 0; i < TimePanel.Controls.Count; i++)
            {
                if (TimePanel.Controls[i].GetType() == typeof(Label))
                {
                    Label TimeLabel = TimePanel.Controls[i] as Label;

                    if (TimeLabel.Text.EndsWith(Course))
                    {
                        return new HyTekTime(TimeLabel.Text);
                    }
                }
            }

            return HyTekTime.NOTIME;
        }
        private static bool IsTimeWithinCuts(HyTekTime BestTime, HyTekTime FastCut, HyTekTime SlowCut)
        {
            if (FastCut == HyTekTime.NOTIME && SlowCut == HyTekTime.NOTIME)
                return true;
            if (SlowCut == HyTekTime.NOTIME && FastCut != HyTekTime.NOTIME)
                return BestTime <= FastCut;
            if (SlowCut != HyTekTime.NOTIME && FastCut == HyTekTime.NOTIME)
                if (BestTime == HyTekTime.NOTIME)
                    return true;
                else
                    return BestTime > SlowCut;
            return BestTime <= FastCut && BestTime > SlowCut;
        }

        private List<String> GetFastCutTimesString(SwimTeamDatabase.MeetEventsRow MeetEvent)
        {
            List<String> Temp = new List<string>();
            if (this.Meet.Course.ToUpper().StartsWith("Y"))
            {
                if (this.Meet.Course.ToUpper() == "YO" || this.Meet.Course.ToUpper() == "Y")
                {
                    //only do yards
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "YS")
                {
                    //do yards SCM meters
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                }
                else
                {
                    //do yards lcm scm
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("L"))
            {
                if (this.Meet.Course.ToUpper() == "LO" || this.Meet.Course.ToUpper() == "L")
                {
                    //only do meters
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "LS")
                {
                    //do lcm scm yards
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                }
                else
                {
                    //do lcm yards scm
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("S"))
            {
                if (this.Meet.Course.ToUpper() == "SO" || this.Meet.Course.ToUpper() == "S")
                {
                    // only do scm
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "SL")
                {
                    //do scm lcm yards
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                }
                else
                {
                    //do scm yards lcm
                    if (!MeetEvent.IsSCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMFastCut, "S").ToString());
                    if (!MeetEvent.IsSCYFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYFastCut, "Y").ToString());
                    if (!MeetEvent.IsLCMFastCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMFastCut, "L").ToString());
                }
            }

            return Temp;
        }
        private List<String> GetSlowCutTimesString(SwimTeamDatabase.MeetEventsRow MeetEvent)
        {
            List<String> Temp = new List<string>();
            if (this.Meet.Course.ToUpper().StartsWith("Y"))
            {
                if (this.Meet.Course.ToUpper() == "YO" || this.Meet.Course.ToUpper() == "Y")
                {
                    //only do yards
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "YS")
                {
                    //do yards SCM meters
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                }
                else
                {
                    //do yards lcm scm
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("L"))
            {
                if (this.Meet.Course.ToUpper() == "LO" || this.Meet.Course.ToUpper() == "L")
                {
                    //only do meters
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "LS")
                {
                    //do lcm scm yards
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                }
                else
                {
                    //do lcm yards scm
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("S"))
            {
                if (this.Meet.Course.ToUpper() == "SO" || this.Meet.Course.ToUpper() == "S")
                {
                    // only do scm
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                }
                else if (this.Meet.Course.ToUpper() == "SL")
                {
                    //do scm lcm yards
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                }
                else
                {
                    //do scm yards lcm
                    if (!MeetEvent.IsSCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCMSlowCut, "S").ToString());
                    if (!MeetEvent.IsSCYSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.SCYSlowCut, "Y").ToString());
                    if (!MeetEvent.IsLCMSlowCutNull())
                        Temp.Add(new HyTekTime(MeetEvent.LCMSlowCut, "L").ToString());
                }
            }

            return Temp;
        }
        private List<String> GetBestTimesString(SwimTeamDatabase.MeetEventsRow MeetEvent)
        {
            List<String> Temp = new List<string>();
            SwimTeamDatabase.BestTimesRow YardsBestTime = this.YardsBestTimesDictionary[MeetEvent];
            SwimTeamDatabase.BestTimesRow MetersBestTime = this.MetersBestTimesDictionary[MeetEvent];
            SwimTeamDatabase.BestTimesRow SCMBestTime = this.SCMBestTimesDictionary[MeetEvent];

            int MeetID = -1;

            if (this.Meet.Course.ToUpper().StartsWith("Y"))
            {
                if (this.Meet.Course.ToUpper() == "YO" || this.Meet.Course.ToUpper() == "Y")
                {
                    //only do yards
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                        {
                            Temp.Add(YardsBestTime.Time.ToString());
                        }
                    }
                }
                else if (this.Meet.Course.ToUpper() == "YS")
                {
                    //do yards SCM meters
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                }
                else
                {
                    //do yards lcm scm
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("L"))
            {
                if (this.Meet.Course.ToUpper() == "LO" || this.Meet.Course.ToUpper() == "L")
                {
                    //only do meters
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                }
                else if (this.Meet.Course.ToUpper() == "LS")
                {
                    //do lcm scm yards
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                }
                else
                {
                    //do lcm yards scm
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                }
            }
            else if (this.Meet.Course.ToUpper().StartsWith("S"))
            {
                if (this.Meet.Course.ToUpper() == "SO" || this.Meet.Course.ToUpper() == "S")
                {
                    // only do scm
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                }
                else if (this.Meet.Course.ToUpper() == "SL")
                {
                    //do scm lcm yards
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                }
                else
                {
                    //do scm yards lcm
                    if (SCMBestTime != null)
                    {
                        if (!SCMBestTime.Time.IsNoTime)
                            Temp.Add(SCMBestTime.Time.ToString());
                    }
                    if (YardsBestTime != null)
                    {
                        if (!YardsBestTime.Time.IsNoTime)
                            Temp.Add(YardsBestTime.Time.ToString());
                    }
                    if (MetersBestTime != null)
                    {
                        if (!MetersBestTime.Time.IsNoTime)
                            Temp.Add(MetersBestTime.Time.ToString());
                    }
                }
            }

            if (Temp.Count == 0)
                Temp.Add("NT");

            return Temp;
        }

        private Panel GetEventControlPanel(SwimTeamDatabase.SwimmersRow Swimmer, SwimTeamDatabase.SessionV2Row Session,
            SwimTeamDatabase.MeetsV2Row Meet, int Number, String DefaultCourse, SwimTeamDatabase.MeetEventsRow Event)
        {
            SwimTeamDatabase.EntriesV2Row Entry = this.EntriesDictionary[Event];

            Panel EventControlPanel = new Panel();
            EventControlPanel.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "EventPanel" + Number;

            CheckBox EntryCheckBox = new CheckBox();
            EntryCheckBox.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "EntryCheckBox" + Number;
            EntryCheckBox.Attributes.Add("onclick", "ToggleTimeControl(this); CopyBestTime(this);");
            if (!UsePreviousValues && Entry != null)
                if (Entry.EnterEvent)
                    EntryCheckBox.Checked = true;
            EventControlPanel.Controls.Add(EntryCheckBox);

            TextBox TimeBox = new TextBox();
            TimeBox.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "TimeBox" + Number;
            TimeBox.Columns = 11;
            TimeBox.MaxLength = 11;
            if (EntryCheckBox.Checked)
                TimeBox.Enabled = true;
            else
                TimeBox.Enabled = false;
            TimeBox.Attributes.Add("onblur", "TimeTextBoxBlur(this)");
            //TimeBox.Attributes.Add("onfocus", "this.select()");
            TimeBox.AutoPostBack = false;
            if (!this.UsePreviousValues)
            {
                if (Entry != null)
                    if (Entry.EnterEvent)
                    {
                        HyTekTime EntryTime = HyTekTime.NOTIME;
                        if (!Entry.IsAutoTimeNull())
                        {
                            if (Entry.AutoTime == 0)
                            {
                                try
                                {
                                    if (Entry.Course == "Y" || Entry.Course == "y")
                                        EntryTime = new HyTekTime(this.YardsBestTimesDictionary[Event].Score, Entry.Course);
                                    else if (Entry.Course == "L" || Entry.Course == "l" || Entry.Course == "M" || Entry.Course == "m")
                                        EntryTime = new HyTekTime(this.MetersBestTimesDictionary[Event].Score, Entry.Course);
                                    else if (Entry.Course == "S" || Entry.Course == "s")
                                        EntryTime = new HyTekTime(this.SCMBestTimesDictionary[Event].Score, Entry.Course);
                                }
                                catch (Exception)
                                {
                                    TimeBox.BackColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                                EntryTime = new HyTekTime(Entry.AutoTime, Entry.Course);
                        }
                        else if (!Entry.IsCustomTimeNull())
                            EntryTime = new HyTekTime(Entry.CustomTime, Entry.Course);
                        TimeBox.Text = EntryTime.ToString();
                    }
                    else
                        TimeBox.Text = "";
                else
                    TimeBox.Text = "";
            }
            EventControlPanel.Controls.Add(TimeBox);

            CustomValidator TimeBoxValidator = new CustomValidator();
            TimeBoxValidator.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "TimeBoxValidator" + Number;
            TimeBoxValidator.ClientValidationFunction = "ValidateTime";
            TimeBoxValidator.ControlToValidate = TimeBox.ID;
            TimeBoxValidator.ErrorMessage = "";
            TimeBoxValidator.ServerValidate += new ServerValidateEventHandler(TimeBoxValidator_ServerValidate);
            TimeBoxValidator.ValidateEmptyText = true;
            EventControlPanel.Controls.Add(TimeBoxValidator);

            HiddenField DefaultCourseHiddenField = new HiddenField();
            DefaultCourseHiddenField.ID = "Meet" + Meet.Meet + Swimmer.USAID + "Session" + Session.Session + "DefaultCourseHiddenField" + Number;
            DefaultCourseHiddenField.Value = DefaultCourse;
            EventControlPanel.Controls.Add(DefaultCourseHiddenField);

            return EventControlPanel;
        }

        protected void TimeBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (((CheckBox)((TextBox)this.FindControl(((CustomValidator)source).ControlToValidate)).Parent.Controls[0]).Checked)
            {
                bool TimeOverCut = false;
                TextBox TimeTextBox = ((TextBox)this.FindControl(((CustomValidator)source).ControlToValidate));
                TimeTextBox.Enabled = true;
                try
                {
                    HyTekTime Time = new HyTekTime(TimeTextBox.Text);
                    if (Time.IsNoTime)
                        args.IsValid = false;
                    //get cut time for same course, if time is greater, args is invalid
                    else
                    {
                        Panel FastCutTimePanel = ((Panel)((TableRow)TimeTextBox.Parent.Parent.Parent).Cells[5].Controls[0]);
                        Panel SlowCutTimePanel = ((Panel)((TableRow)TimeTextBox.Parent.Parent.Parent).Cells[6].Controls[0]);
                        CheckBox BonusCheckBox = ((CheckBox)((TableRow)TimeTextBox.Parent.Parent.Parent).Cells[7].Controls[0]);
                        if (!BonusCheckBox.Checked)
                        {
                            HyTekTime FastCutTime = HyTekTime.NOTIME;
                            HyTekTime SlowCutTime = HyTekTime.NOTIME;
                            String EntryCourse = TimeTextBox.Text.Substring(TimeTextBox.Text.Length - 1);
                            for (int i = 0; i < FastCutTimePanel.Controls.Count; i++)
                                if (FastCutTimePanel.Controls[i].GetType() == typeof(Label))
                                {
                                    Label CutTimeLabel = ((Label)FastCutTimePanel.Controls[i]);
                                    if (CutTimeLabel.Text.Substring(CutTimeLabel.Text.Length - 1) == EntryCourse)
                                        FastCutTime = new HyTekTime(CutTimeLabel.Text);
                                }
                            for (int i = 0; i < SlowCutTimePanel.Controls.Count; i++)
                                if (SlowCutTimePanel.Controls[i].GetType() == typeof(Label))
                                {
                                    Label CutTimeLabel = ((Label)SlowCutTimePanel.Controls[i]);
                                    if (CutTimeLabel.Text.Substring(CutTimeLabel.Text.Length - 1) == EntryCourse)
                                        SlowCutTime = new HyTekTime(CutTimeLabel.Text);
                                }
                            if (SlowCutTime != HyTekTime.NOTIME && FastCutTime == HyTekTime.NOTIME)
                            {
                                if (Time < SlowCutTime)
                                {
                                    args.IsValid = false;
                                    TimeOverCut = true;
                                }
                            }
                            else if (FastCutTime != HyTekTime.NOTIME && SlowCutTime == HyTekTime.NOTIME)
                            {
                                if (Time > FastCutTime)
                                {
                                    args.IsValid = false;
                                    TimeOverCut = true;
                                }
                            }
                            else if (FastCutTime != HyTekTime.NOTIME && SlowCutTime != HyTekTime.NOTIME)
                            {
                                if ((Time < SlowCutTime) || (Time >= FastCutTime))
                                {
                                    args.IsValid = false;
                                    TimeOverCut = true;
                                }
                            }

                            //for (int i = 0; i < FastCutTimePanel.Controls.Count; i++)
                            //{
                            //    if (FastCutTimePanel.Controls[i].GetType() == typeof(Label))
                            //    {
                            //        Label CutTimeLabel = ((Label)FastCutTimePanel.Controls[i]);
                            //        if (TimeTextBox.Text.Substring(TimeTextBox.Text.Length - 1) ==
                            //            CutTimeLabel.Text.Substring(CutTimeLabel.Text.Length - 1))
                            //        {
                            //            HyTekTime CutTime = new HyTekTime(CutTimeLabel.Text);
                            //            if (Time > CutTime)
                            //            {
                            //                args.IsValid = false;
                            //                TimeOverCut = true;
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                catch (HyTekTime.InvalidTimeException)
                {
                    args.IsValid = false;
                }
                finally
                {
                    if (!args.IsValid)
                    {
                        Panel MainAccordion = ((Panel)TimeTextBox.Parent.Parent.Parent.Parent.Parent.Parent);
                        Panel SwimmerPanel = ((Panel)TimeTextBox.Parent.Parent.Parent.Parent.Parent);
                        String EventNumber = ((Label)((TableRow)TimeTextBox.Parent.Parent.Parent).Cells[0].Controls[0]).Text;
                        String SwimmerName = "";
                        for (int i = 0; i < MainAccordion.Controls.Count; i++)
                        {
                            if (MainAccordion.Controls[i] == SwimmerPanel)
                            {
                                Literal Header = ((Literal)MainAccordion.Controls[i - 1]);
                                int index = Header.Text.IndexOf("<a href=\"#\">");
                                String temp = Header.Text.Remove(0, index);
                                temp = temp.Remove(0, "<a href=\"#\">".Length);
                                index = temp.IndexOf("</a");
                                SwimmerName = temp.Remove(index);
                                break;
                            }
                        }

                        bool AlreadyTimeError = false;
                        for (int i = 0; i < ErrorStrings.Count; i++)
                            if (ErrorStrings[i].Contains(SwimmerName))
                                if (ErrorStrings[i].Contains("1 entry time"))
                                {
                                    AlreadyTimeError = true;
                                    break;
                                }
                        if (!AlreadyTimeError)
                        {
                            if (TimeOverCut)
                                ErrorStrings.Add("Problem with " + SwimmerName + ": an entry time is slower or faster than a cut time.");
                            else
                                ErrorStrings.Add("Problem with " + SwimmerName + ": at least 1 entry time is invalid.");
                        }
                    }
                }
            }
        }
    }

    public override void Validate()
    {
        this.ErrorStrings.Clear();
        this.ErrorLabel.Text = "";
        base.Validate();
        if (ErrorStrings.Count != 0)
            this.ErrorLabel.Text = "ERRORS ON PAGE - MEET NOT SAVED<br /><br />";
        for (int i = 0; i < ErrorStrings.Count; i++)
        {
            this.ErrorLabel.Text += ErrorStrings[i] + "<br />";
        }
        if (this.ErrorLabel.Text.EndsWith("<br />"))
            this.ErrorLabel.Text = this.ErrorLabel.Text.Remove(this.ErrorLabel.Text.Length - "<br />".Length);
        //if(!Page.IsValid)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CheckAllTextBoxes", "CheckAllTextBoxes()", true);
    }
    protected void GroupDropDownListDatabound(object sender, EventArgs e)
    {
        if (User.IsInRole("DatabaseManager"))
            GroupsDropDownList.Items.Add(new ListItem("All Groups", "-1"));
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrWhiteSpace(Profile.GroupID))
            {
                GroupsDropDownList.SelectedValue = Profile.GroupID;
            }
        }
    }
    private void SendErrorEmail(Exception ex)
    {
        MembershipUser User = Membership.GetUser();
        System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage("website@gtacswim.com", "cpierson@sev.org");
        Email.Subject = "ERROR in GTAC Tools";
        Email.Body = "Error page: " + "Coach/PickEvents.aspx" + "\n\n";
        Email.Body = "Type: " + ex.GetType() + "\n\n";
        Email.Body += "Message: " + ex.Message + "\n\n";
        if (User != null)
            Email.Body += "User: " + User.UserName + " (email: ) " + User.Email + "\n\n";
        Email.Body += "Stack Trace: " + ex.StackTrace + "\n\n";
        Email.Body += "Soucre: " + ex.Source + "\n\n";
        Email.Body += "Target Site: " + ex.TargetSite;
        Email.Priority = System.Net.Mail.MailPriority.High;

        System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
        Client.Send(Email);

    }
}