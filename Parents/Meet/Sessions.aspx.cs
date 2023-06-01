using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Security;
using System.Data;

public partial class Parents_Meet_Sessions : System.Web.UI.Page
{
    private DateTime? _MeetStartDate;
    private DateTime MeetStartDate
    {
        get
        {
            if (this._MeetStartDate == null)
                if (ViewState["MeetStartDate"] != null)
                    this._MeetStartDate = (DateTime)ViewState["MeetStartDate"];
            return this._MeetStartDate.Value;
        }
        set
        {
            this._MeetStartDate = value;
            ViewState["MeetStartDate"] = value;
        }
    }
    private SwimTeamDatabase.PreEnteredV2DataTable _PreEntries;
    private SwimTeamDatabase.PreEnteredV2DataTable PreEntries
    {
        get
        {
            if (this._PreEntries == null)
                if (ViewState["PreEntries"] != null)
                    this._PreEntries = (SwimTeamDatabase.PreEnteredV2DataTable)ViewState["PreEntries"];
            return this._PreEntries;
        }
        set
        {
            this._PreEntries = value;
            ViewState["PreEntries"] = value;
        }
    }
    private bool _FirstTimeInMeet;
    private bool FirstTimeInMeet
    {
        get
        {
            if (this._FirstTimeInMeet == null)
                if (ViewState["FirstTimeInMeet"] != null)
                    this._FirstTimeInMeet = (bool)ViewState["FirstTimeInMeet"];
            return this._FirstTimeInMeet;
        }
        set
        {
            this._FirstTimeInMeet = value;
            ViewState["FirstTimeInMeet"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] == null)
            Response.Redirect("~/Parents/FamilyView.aspx?Error=6");
        if (Request.QueryString["MeetID"] == null)
            Response.Redirect("~/Parents/Meet/Meets.aspx?ErrorCode=1");

        if (!Page.IsPostBack)
        {
            MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
            SwimTeamDatabase.MeetsV2Row Meet = MeetsAdapter.GetMeetByMeetID(int.Parse(Request.QueryString["MeetID"]));
            this.MeetStartDate = Meet.Start;
            this.MeetNameLabel.Text = "<h1 style=\"text-align: center;\">" + Meet.MeetName + "</h1>";

            USAIDEncryptor Encryptor = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted);
            String USAID = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);

            PreEnteredV2BLL PreEntryAdapter = new PreEnteredV2BLL();
            this.PreEntries = PreEntryAdapter.GetPreEntriesByMeetAndSwimmer(USAID,
                                                                        int.Parse(Request.QueryString["MeetID"]));
        }
        int Loads = int.Parse(this.LoadsHiddenField.Value) + 1;
        if (this.GridView1.SelectedIndex == -1)
            Loads = 1;
        this.LoadsHiddenField.Value = Loads.ToString();
        if (Loads == 1)
            this.DisplayPanel.Style.Add(HtmlTextWriterStyle.Display, "none");
        else
        {
            this.DisplayPanel.Style.Remove(HtmlTextWriterStyle.Display);
            this.DisplayPanel.Style.Add(HtmlTextWriterStyle.Display, "inherit");
            GridView2.DataBind();
        }
    }

    protected void SelectedSessionChanged(object sender, EventArgs e)
    {
        this.WhichEventsRadioButtonList.Visible = true;
        this.SelectionNumberHiddenField.Value =
        ((Label)this.GridView1.Rows[this.GridView1.SelectedIndex].FindControl("SessionNumberLabel")).Text;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label EventNumberLabel = ((Label)e.Row.FindControl("EventNumberLabel"));
            EventNumberLabel.Text += ((HiddenField)e.Row.FindControl("EventNumberExtendedHiddenField")).Value;

            HiddenField AgeCodeHiddenField = ((HiddenField)e.Row.FindControl("AgeCodeHiddenField"));
            HiddenField DistanceHiddenField = ((HiddenField)e.Row.FindControl("DistanceHiddenField"));
            HiddenField StrokeHiddenField = ((HiddenField)e.Row.FindControl("StrokeHiddenField"));
            HiddenField CourseHiddenField = ((HiddenField)e.Row.FindControl("CourseHiddenField"));
            HiddenField GenderHiddenField = ((HiddenField)e.Row.FindControl("GenderHiddenField"));

            String AgeString = MeetEventHelper.GetAgeString(AgeCodeHiddenField.Value);
            String StrokeString = MeetEventHelper.GetStrokeString(StrokeHiddenField.Value, MeetEventHelper.StrokeStringLength.Middle);
            String GenderString = "";
            if (GenderHiddenField.Value.ToUpper() == "F")
                GenderString = "Girls";
            else if (GenderHiddenField.Value.ToUpper() == "M")
                GenderString = "Boys";
            else if (GenderHiddenField.Value.ToUpper() == "X")
                GenderString = "Mixed Girls and Boys";
            String CourseString = "";
            if (CourseHiddenField.Value.ToUpper() == "L")
                CourseString = "Meter";
            else if (CourseHiddenField.Value.ToUpper() == "Y")
                CourseString = "Yard";
            else
                CourseString = "Short Course Meter";

            String Description = GenderString + " " + AgeString + " " + DistanceHiddenField.Value + " " + CourseString +
                " " + StrokeString;

            ((Label)e.Row.FindControl("EventDescriptionLabel")).Text = Description;

            HiddenField SCYFastCutHiddenField = ((HiddenField)e.Row.FindControl("SCYFastCutHiddenField"));
            HiddenField SCYSlowCutHiddenField = ((HiddenField)e.Row.FindControl("SCYSlowCutHiddenField"));
            HiddenField LCMFastCutHiddenField = ((HiddenField)e.Row.FindControl("LCMFastCutHiddenField"));
            HiddenField LCMSlowCutHiddenField = ((HiddenField)e.Row.FindControl("LCMSlowCutHiddenField"));
            HiddenField SCMFastCutHiddenField = ((HiddenField)e.Row.FindControl("SCMFastCutHiddenField"));
            HiddenField SCMSlowCutHiddenField = ((HiddenField)e.Row.FindControl("SCMSlowCutHiddenField"));

            HyTekTime SCYFastCut, SCYSlowCut, LCMFastCut, LCMSlowCut, SCMFastCut, SCMSlowCut;

            if (!String.IsNullOrWhiteSpace(SCYFastCutHiddenField.Value))
                SCYFastCut = new HyTekTime(int.Parse(SCYFastCutHiddenField.Value), "Y");
            else
                SCYFastCut = HyTekTime.NOTIME;
            if (!string.IsNullOrWhiteSpace(SCYSlowCutHiddenField.Value))
                SCYSlowCut = new HyTekTime(int.Parse(SCYSlowCutHiddenField.Value), "Y");
            else
                SCYSlowCut = HyTekTime.NOTIME;
            if (!string.IsNullOrWhiteSpace(LCMFastCutHiddenField.Value))
                LCMFastCut = new HyTekTime(int.Parse(LCMFastCutHiddenField.Value), "L");
            else
                LCMFastCut = HyTekTime.NOTIME;
            if (!string.IsNullOrWhiteSpace(LCMSlowCutHiddenField.Value))
                LCMSlowCut = new HyTekTime(int.Parse(LCMSlowCutHiddenField.Value), "L");
            else
                LCMSlowCut = HyTekTime.NOTIME;
            if (!string.IsNullOrWhiteSpace(SCMFastCutHiddenField.Value))
                SCMFastCut = new HyTekTime(int.Parse(SCMFastCutHiddenField.Value), "S");
            else
                SCMFastCut = HyTekTime.NOTIME;
            if (!string.IsNullOrWhiteSpace(SCMSlowCutHiddenField.Value))
                SCMSlowCut = new HyTekTime(int.Parse(SCMSlowCutHiddenField.Value), "S");
            else
                SCMSlowCut = HyTekTime.NOTIME;

            Label FastCutLabel = ((Label)e.Row.FindControl("FastCutLabel"));
            Label SlowCutLabel = ((Label)e.Row.FindControl("SlowCutLabel"));

            if (!SCYFastCut.IsNoTime)
                FastCutLabel.Text = SCYFastCut.ToString();
            if (!LCMFastCut.IsNoTime)
            {
                if (!String.IsNullOrWhiteSpace(FastCutLabel.Text))
                    FastCutLabel.Text += "<br />";
                FastCutLabel.Text += LCMFastCut.ToString();
            }
            if (!SCMFastCut.IsNoTime)
            {
                if (!String.IsNullOrWhiteSpace(FastCutLabel.Text))
                    FastCutLabel.Text += "<br />";
                FastCutLabel.Text += SCMFastCut.ToString();
            }

            if (!SCYSlowCut.IsNoTime)
                SlowCutLabel.Text = SCYSlowCut.ToString();
            if (!LCMSlowCut.IsNoTime)
            {
                if (!string.IsNullOrWhiteSpace(SlowCutLabel.Text))
                    SlowCutLabel.Text += "<br />";
                SlowCutLabel.Text += LCMSlowCut.ToString();
            }
            if (!SCMSlowCut.IsNoTime)
            {
                if (!string.IsNullOrWhiteSpace(SlowCutLabel.Text))
                    SlowCutLabel.Text += "<br />";
                SlowCutLabel.Text += SCMSlowCut.ToString();
            }

            bool IncludeSCM = !SCMFastCut.IsNoTime || !SCMSlowCut.IsNoTime;
            bool IncludeSCY = !SCYFastCut.IsNoTime || !SCYSlowCut.IsNoTime;
            bool IncludeLCM = !LCMFastCut.IsNoTime || !LCMSlowCut.IsNoTime;

            if (CourseHiddenField.Value.ToUpper() == "Y")
                IncludeSCY = true;
            else if (CourseHiddenField.Value.ToUpper() == "L")
                IncludeLCM = true;
            else
                IncludeSCM = true;

            DataRowView RowView = e.Row.DataItem as DataRowView;
            AddBestTimeLabels(RowView["Distance"].ToString(), RowView["StrokeCode"].ToString(),
                RowView["Course"].ToString(), IncludeSCM, IncludeSCY, IncludeLCM, e.Row.Cells[2]);

        }
    }

    protected void SessionsGridDataBound(object sender, EventArgs e)
    {
        if (this.WhichEventsRadioButtonList.Items[0].Selected)
        {
            GridView Grid = ((GridView)sender);
            if (Grid.Rows.Count > 0)
            {
                SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                USAIDEncryptor Encryptor = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted);
                String USAID = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
                SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmerByUSAID(USAID);

                if (Swimmers.Count == 1)
                {
                    int age = MeetEventHelper.AgeOnDate(this.MeetStartDate, Swimmers[0].Birthday);
                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            HiddenField AgeCodeHiddenField = ((HiddenField)Grid.Rows[i].FindControl("AgeCodeHiddenField"));
                            int MinAge = MeetEventHelper.GetMinAgeFromAgeString(AgeCodeHiddenField.Value);
                            int MaxAge = MeetEventHelper.GetMaxAgeFromAgeString(AgeCodeHiddenField.Value);

                            if (MinAge > age || MaxAge < age)
                                Grid.Rows[i].Visible = false;

                            HiddenField GenderHiddenField = ((HiddenField)Grid.Rows[i].FindControl("GenderHiddenField"));
                            if (GenderHiddenField.Value.ToUpper() != "X")
                            {
                                if (Swimmers[0].Gender != GenderHiddenField.Value)
                                    Grid.Rows[i].Visible = false;
                            }
                        }
                    }
                }

                DataControlRowState NextAssignState = DataControlRowState.Normal;
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        if (Grid.Rows[i].Visible)
                        {
                            Grid.Rows[i].RowState = NextAssignState;
                            if (NextAssignState == DataControlRowState.Normal)
                                NextAssignState = DataControlRowState.Alternate;
                            else
                                NextAssignState = DataControlRowState.Normal;
                        }
                    }
                }
            }
        }
    }
    protected void MainGridRowDatabound(object sender, GridViewRowEventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField AMHiddenField = ((HiddenField)e.Row.FindControl("AMHiddenField"));
                Label StartTimeLabel = ((Label)e.Row.FindControl("StartTimeLabel"));

                if (StartTimeLabel.Text.StartsWith("0"))
                    StartTimeLabel.Text = StartTimeLabel.Text.Substring(1);
                if (bool.Parse(AMHiddenField.Value))
                    StartTimeLabel.Text += " AM";
                else
                    StartTimeLabel.Text += " PM";

                HiddenField WarmupTimeGuessHiddenField = ((HiddenField)e.Row.FindControl("WarmupTimeGuessHiddenField"));
                if (bool.Parse(WarmupTimeGuessHiddenField.Value))
                    StartTimeLabel.Text += " <span style=\"font-style:italic;\">This is a guesstimate</span>";
                HiddenField PrelimFinalHiddenField = ((HiddenField)e.Row.FindControl("PrelimFinalHiddenField"));
                if (bool.Parse(PrelimFinalHiddenField.Value))
                    StartTimeLabel.Text += " Prelims Session";

                Label DayLabel = ((Label)e.Row.FindControl("DayLabel"));
                int Day = int.Parse(DayLabel.Text);

                DateTime Date = MeetStartDate.AddDays((Day - 1));

                DayLabel.Text = Date.ToString("ddd, MMM d");



                Label SessionNumberLabel = ((Label)e.Row.FindControl("SessionNumberLabel"));
                CheckBox EnteredCheckBox = ((CheckBox)e.Row.FindControl("EnteredCheckBox"));
                int Session = int.Parse(SessionNumberLabel.Text);

                bool InSession = false;
                for (int i = 0; i < this.PreEntries.Count; i++)
                    if (this.PreEntries[i].IsPreEnteredInSession(Session))
                    {
                        InSession = true;
                        break;
                    }
                EnteredCheckBox.Checked = InSession;



                //LinkButton ViewEventsLinkButton = ((LinkButton)e.Row.FindControl("ViewEventsLinkButton"));
                //ViewEventsLinkButton.PostBackUrl = "~/Parents/Meet/Sessions.aspx?MeetID="
                //    + Request["MeetID"] + "&ID=" + Request["ID"] +
                //    "&SI=" + SessionNumberLabel.Text;
            }
        }
    }
    protected void SaveEntriesButtonClicked(object sender, EventArgs e)
    {
        PreEnteredV2BLL.RemoveFromSessionOptions RemoveOptions = PreEnteredV2BLL.RemoveFromSessionOptions.DeleteBlankDeclaredEntry;
        PreEnteredV2BLL PreEntryAdpter = new PreEnteredV2BLL();
        List<int> SessionsToAddTo = new List<int>();
        USAIDEncryptor Encryptor = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted);
        String USAID = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
        int MeetID = int.Parse(Request.QueryString["MeetID"]);
        int SessionsRemovedFrom = -5;
        bool SaveNecesary = false;

        //for (int j = 0; j < this.GridView1.Rows.Count; j++)
        //{
        //    if (((CheckBox)this.GridView1.Rows[j].FindControl("EnteredCheckBox")).Checked)
        //    {
        //        RemoveOptions = PreEnteredV2BLL.RemoveFromSessionOptions.BlankDeclaredSessionsToGeneralPreEntry;
        //        break;
        //    }
        //}

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                int SessionNumber = int.Parse(((Label)GridView1.Rows[i].FindControl("SessionNumberLabel")).Text);

                bool EnteredInSession = false;
                for (int j = 0; j < this.PreEntries.Count; j++)
                {
                    if (this.PreEntries[j].IsPreEnteredInSession(SessionNumber))
                    {
                        EnteredInSession = true;
                        break;
                    }
                }

                CheckBox EnteredCheckBox = ((CheckBox)GridView1.Rows[i].FindControl("EnteredCheckBox"));
                bool ShouldBeEntered = EnteredCheckBox.Checked;
                if (!EnteredCheckBox.Visible)
                    ShouldBeEntered = false;
                if (ShouldBeEntered != EnteredInSession)
                {
                    if (ShouldBeEntered && !EnteredInSession)
                    //enter the swimmer in the session
                    {
                        SessionsToAddTo.Add(SessionNumber);
                        SaveNecesary = true;
                    }
                    else if (!ShouldBeEntered && EnteredInSession)
                    {
                        //remove the swimmer from the session. This functionality may not be necessary
                        SaveNecesary = true;
                        if (this.PreEntries[0].IsPreEnteredInSession(SessionNumber))
                            if (PreEntryAdpter.RemoveFromSession(USAID, MeetID,
                                SessionNumber, this.PreEntries[0], RemoveOptions))
                            {
                                if (SessionsRemovedFrom == -5)
                                    SessionsRemovedFrom = 1;
                                else
                                    SessionsRemovedFrom++;
                                this.DeleteSwimmerEventsFromSession(SessionNumber, USAID, MeetID);
                            }
                            else
                                SessionsRemovedFrom = -10000;
                    }
                }
            }
        }
        if (SaveNecesary)
        {
            int SessionsAdded = -5;
            if (SessionsToAddTo.Count != 0)
            {
                if (this.PreEntries.Count > 0)
                {
                    SessionsAdded = PreEntryAdpter.AddToSessions(SessionsToAddTo, this.PreEntries[0]);
                    this.FirstTimeInMeet = false;
                }
                else
                {
                    SessionsAdded = PreEntryAdpter.CreatePreEntryForSession(SessionsToAddTo, USAID, MeetID);
                    this.FirstTimeInMeet = true;
                }
            }

            String QueryString = "";
            if (SessionsAdded != -5)
                if (SessionsAdded >= 0)
                {
                    if (!String.IsNullOrWhiteSpace(QueryString))
                        QueryString += "&";
                    QueryString += "Sessions=" + SessionsAdded;
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(QueryString))
                        QueryString += "&";
                    QueryString += "Error=7";
                }
            if (SessionsRemovedFrom != -5)
                if (SessionsRemovedFrom >= 0)
                {
                    if (!String.IsNullOrWhiteSpace(QueryString))
                        QueryString += "&";
                    QueryString += "SessionsR=" + SessionsRemovedFrom;
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(QueryString))
                        QueryString += "&";
                    QueryString += "Error=8";
                }

            QueryString += "&" + SendEmail(SessionsAdded, SessionsRemovedFrom, USAID);

            Response.Redirect("~/Parents/FamilyView.aspx?" + QueryString);
        }
    }
    protected void MainGridDatabound(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GridView Grid = ((GridView)sender);
            if (Grid.Rows.Count > 0)
            {
                SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                USAIDEncryptor Encryptor = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted);
                String USAID = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
                SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmerByUSAID(USAID);
                int MeetID = int.Parse(Request.QueryString["MeetID"]);

                if (Swimmers.Count == 1)
                {
                    int age = MeetEventHelper.AgeOnDate(this.MeetStartDate, Swimmers[0].Birthday);
                    MeetEventsBLL MeetEventsAdapter = new MeetEventsBLL();

                    for (int i = 0; i < Grid.Rows.Count; i++)
                    {
                        if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            int SessionNumber = int.Parse(((Label)Grid.Rows[i].FindControl("SessionNumberLabel")).Text);

                            SwimTeamDatabase.MeetEventsDataTable Events = MeetEventsAdapter.GetByMeetIDAndSessionNumber(MeetID, SessionNumber);

                            bool EligibleEventFound = false;
                            for (int j = 0; j < Events.Count; j++)
                            {
                                int MinAge = MeetEventHelper.GetMinAgeFromAgeString(Events[j].AgeCode.ToString());
                                int MaxAge = MeetEventHelper.GetMaxAgeFromAgeString(Events[j].AgeCode.ToString());

                                if ((age >= MinAge && age <= MaxAge) && Swimmers[0].Gender == Events[j].SexCode)
                                {
                                    EligibleEventFound = true;
                                    break;
                                }
                                else if ((age >= MinAge && age <= MaxAge) && Events[j].SexCode.ToUpper() == "X")
                                {
                                    EligibleEventFound = true;
                                    break;
                                }
                            }

                            if (!EligibleEventFound)
                            {
                                ((CheckBox)Grid.Rows[i].FindControl("EnteredCheckBox")).Visible = false;
                            }
                        }
                    }
                }
            }
        }
    }

    private String SendEmail(int SessionsAdded, int SessionsRemovedFrom, String USAID)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersRow Swimmer = SwimmersAdapter.GetSwimmerByUSAID(USAID)[0];

        String SessionsAddedBody = "", SessionsRemovedBody = "";

        String MeetName = MeetNameLabel.Text;
        MeetName = MeetName.Replace("<h1 style=\"text-align: center;\">", "");
        MeetName = MeetName.Replace("</h1>", "");
        if (!MeetName.ToUpper().Contains("THE"))
            MeetName = "The " + MeetName;

        if (SessionsAdded != -5)
        {
            if (SessionsAdded < 1)
                SessionsAddedBody = "There seems to have been an error entering " + Swimmer.PreferredName + " " + Swimmer.LastName +
                    " in " + MeetName + ". The swimmer was not added to any sessions. Please go back and " +
                    "try to enter the swimmer again. If the error persists, I have already been notified and may contact you to try " +
                    "and figure out what is causing this bug." +
                    "<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club<br />-OSI Webmaster";
            else
            {
                if (this.FirstTimeInMeet)
                    SessionsAddedBody = Swimmer.PreferredName + " " + Swimmer.LastName + " has been entered in "
                        + SessionsAdded + " sessions in " + MeetName +
                        ".<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club" +
                    "<br />-OSI Webmaster";
                else
                    SessionsAddedBody = Swimmer.PreferredName + " " + Swimmer.LastName + " has been entered in "
                        + SessionsAdded + " additional sessions in " + MeetName +
                        ".<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club" +
                    "<br />-OSI Webmaster";
                if (SessionsAdded == 1)
                    SessionsAddedBody = SessionsAddedBody.Replace("sessions", "session");
            }
        }

        if (SessionsRemovedFrom != -5)
        {
            if (SessionsRemovedFrom < 1)
                SessionsRemovedBody = "There seems to have been an error removing " + Swimmer.PreferredName + " " + Swimmer.LastName +
                    " from " + MeetName + " sessions. The swimmer was not removed from any sessions. Please go back and " +
                    "try to remove the swimmer from the meet again. If the error persists, I have already been notified and may " +
                    "contact you to try and figure out what is causing this bug.<br /><br />--<br />Chris Pierson" +
                    "<br />-Greater Toledo Aquatic Club<br />-OSI Webmaster";
            else
            {
                SessionsRemovedBody = Swimmer.PreferredName + " " + Swimmer.LastName + " has been removed from " +
                    SessionsRemovedFrom + " sessions in " + MeetName + ".<br /><br />--<br />Chris Pierson<br />" +
                    "-Greater Toledo Aquatic Club<br />-OSI Webmaster";
                SessionsRemovedBody = SessionsRemovedBody.Replace("sessions", "session");
            }
        }

        String FullBody = SessionsAddedBody + SessionsRemovedBody;
        if (!string.IsNullOrWhiteSpace(SessionsAddedBody) && !string.IsNullOrWhiteSpace(SessionsRemovedBody))
            FullBody = SessionsAddedBody.Replace("<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club" +
                    "<br />-OSI Webmaster", "") + "<br /><br />" + SessionsRemovedBody;

        String Subject = "";

        if (SessionsAdded != -5 && SessionsRemovedFrom == -5)
            if (SessionsAdded > 0)
                Subject = Swimmer.PreferredName + " " + Swimmer.LastName + " entered in " + MeetName;
            else
                Subject = "Error adding " + Swimmer.PreferredName + " " + Swimmer.LastName + " to " + MeetName;
        else if (SessionsAdded == -5 && SessionsRemovedFrom != -5)
            if (SessionsRemovedFrom > 0)
                Subject = Swimmer.PreferredName + " " + Swimmer.LastName + " removed from events in " + MeetName;
            else
                Subject = "Error removing " + Swimmer.PreferredName + " " + Swimmer.LastName + " from sessions in " + MeetName;
        else if (SessionsAdded != -5 && SessionsRemovedFrom != -5)
        {
            if (SessionsAdded > 0 && SessionsRemovedFrom > 0)
                Subject = Swimmer.PreferredName + " " + Swimmer.LastName + " added to and removed from sessions in " + MeetName;
            else if (SessionsAdded > 0 && SessionsRemovedFrom < 1)
                Subject = Swimmer.PreferredName + " " + Swimmer.LastName + " added to sessions in " + MeetName + ", but errors removing from sessions";
            else if (SessionsAdded < 1 && SessionsRemovedFrom > 0)
                Subject = Swimmer.PreferredName + " " + Swimmer.LastName + " removed from sessions in " + MeetName + ", but errors adding to sessions";
            else if (SessionsAdded < 1 && SessionsRemovedFrom < 1)
                Subject = "Error adding to and removing " + Swimmer.PreferredName + " " + Swimmer.LastName + " from sessions in " + MeetName;
        }

        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2Row Meet = MeetsAdapter.GetMeetByMeetID(int.Parse(Request.QueryString["MeetID"]));

        DateTime ModifiedCurrentTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time");
        DateTime DateForDeadline = new DateTime(Meet.Deadline.Year, Meet.Deadline.Month, Meet.Deadline.Day, 23, 59, 59);
        if (ModifiedCurrentTime > DateForDeadline)
            Subject = "Late Meet Entry Update: " + Subject;

        bool emailsucess = true;
        //MailMessage email = new MailMessage("cpierson@sev.org", Membership.GetUser().Email);
        //email.Bcc.Add("cpierson@sev.org");
        //email.Subject = Subject;
        //email.Body = FullBody;
        //email.IsBodyHtml = true;

        Emailer Mailer = new Emailer();
        Mailer.To.Add(Membership.GetUser().Email);
        Mailer.Subject = Subject;
        Mailer.Message = FullBody;
        Mailer.IsHTML = true;

        try
        {
            //System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
            //Client.Send(email);

            Mailer.Send();
        }
        catch (Exception)
        {
            emailsucess = false;
        }

        if (SessionsAdded > 0)
            if (Subject.StartsWith("Late Meet Entry Update"))
            {
                GroupCoachesBLL GroupCoachesAdapter = new GroupCoachesBLL();
                SwimTeamDatabase.GroupCoachDataTable GroupCoaches = GroupCoachesAdapter.GetAllGroupCoaches();
                List<String> Emails = new List<string>();
                for (int i = 0; i < GroupCoaches.Count; i++)
                    if (GroupCoaches[i].GroupID == Swimmer.GroupID)
                        if (!Emails.Contains(GroupCoaches[i].CoachEmail))
                            Emails.Add(GroupCoaches[i].CoachEmail);

                Emailer CoachMailer = new Emailer();
                for (int i = 0; i < Emails.Count; i++)
                    CoachMailer.To.Add(Emails[1]);
                CoachMailer.Subject = Subject;
                CoachMailer.Message = FullBody;
                CoachMailer.IsHTML = true;

                //for (int i = 0; i < Emails.Count; i++)
                //{
                try
                {
                    //MailMessage coachemail = new MailMessage();
                    //coachemail.From = new MailAddress("cpierson@sev.org");
                    //coachemail.To.Add(new MailAddress(Emails[i]));
                    ////coachemail.Bcc.Add("cpierson@sev.org");
                    //coachemail.Subject = Subject;
                    //coachemail.Body = FullBody;
                    //coachemail.IsBodyHtml = true;
                    //System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                    //Client.Send(email);

                    CoachMailer.Send();


                }
                catch (Exception)
                {
                    ;
                }
                //}
            }

        if (emailsucess)
            return "email=1";
        else
            return "email=0";

    }
    protected void WhichEventsToDisplaySwitched(object sender, EventArgs e)
    {
        this.GridView2.DataBind();
    }
    protected void GridViewRowButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RollDown", "Layout()", true);
    }

    private int DeleteSwimmerEventsFromSession(int SessionNumber, String USAID, int MeetID)
    {
        EntriesV2BLL EntriesAdapter = new EntriesV2BLL();
        return EntriesAdapter.DeleteSwimmerEntriesFromMeetSession(MeetID, SessionNumber, USAID);
    }

    private SwimTeamDatabase.BestTimesDataTable BestTimes;

    public void AddBestTimeLabels(String Distance, String StrokeCode, String EventCourse, bool IncludeSCM,
        bool IncludeSCY, bool IncludeLCM, Control CollectionOwner)
    {
        if (BestTimes == null)
        {
            BestTimesBLL BestTimesAdapter = new BestTimesBLL();
            USAIDEncryptor Encryptor = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted);

            this.BestTimes = BestTimesAdapter.GetBestTimesByUSAID(Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted));
        }

        int DistanceAsInt = int.Parse(Distance);
        int StrokeCodeAsInt = int.Parse(StrokeCode);
        HyTekTime YardBestTime = HyTekTime.NOTIME, MeterBestTime = HyTekTime.NOTIME, SCMBestTime = HyTekTime.NOTIME;

        for (int i = 0; i < BestTimes.Count; i++)
        {
            if (StrokeCodeAsInt == BestTimes[i].Stroke)
            {
                if (EventCourse == BestTimes[i].Course && DistanceAsInt == BestTimes[i].Distance)
                {
                    if (EventCourse == "Y")
                        YardBestTime = new HyTekTime(BestTimes[i].Score, EventCourse);
                    else if (EventCourse == "L")
                        MeterBestTime = new HyTekTime(BestTimes[i].Score, EventCourse);
                    else if (EventCourse == "S")
                        SCMBestTime = new HyTekTime(BestTimes[i].Score, EventCourse);
                }

                if (StrokeCodeAsInt != 1 && EventCourse != BestTimes[i].Course && DistanceAsInt == BestTimes[i].Distance)
                {
                    if (BestTimes[i].Course == "Y")
                        YardBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                    else if (BestTimes[i].Course == "L")
                        MeterBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                    if (BestTimes[i].Course == "S")
                        SCMBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                }

                if (StrokeCodeAsInt == 1 && EventCourse != BestTimes[i].Course)
                {
                    bool DistancesMatch = false;
                    if (EventCourse == "Y" && BestTimes[i].Course == "L")
                        if ((DistanceAsInt == 500 && BestTimes[i].Distance == 400) ||
                            (DistanceAsInt == 1000 && BestTimes[i].Distance == 800) ||
                            (DistanceAsInt == 1650 && BestTimes[i].Distance == 1500) ||
                            (DistanceAsInt == BestTimes[i].Distance))
                            DistancesMatch = true;
                    if (EventCourse == "L" && BestTimes[i].Course == "Y")
                        if ((DistanceAsInt == 400 && BestTimes[i].Distance == 500) ||
                            (DistanceAsInt == 800 && BestTimes[i].Distance == 1000) ||
                            (DistanceAsInt == 1500 && BestTimes[i].Distance == 1650) ||
                            (DistanceAsInt == BestTimes[i].Distance))
                            DistancesMatch = true;
                    if (EventCourse == "L" && BestTimes[i].Course == "S")
                        if ((DistanceAsInt == 400 && BestTimes[i].Distance == 500) ||
                            (DistanceAsInt == 800 && BestTimes[i].Distance == 1000) ||
                            (DistanceAsInt == 1500 && BestTimes[i].Distance == 1650) ||
                            (DistanceAsInt == BestTimes[i].Distance))
                            DistancesMatch = true;
                    if (DistancesMatch)
                    {
                        if (BestTimes[i].Course == "Y")
                            YardBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                        else if (BestTimes[i].Course == "L")
                            MeterBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                        if (BestTimes[i].Course == "S")
                            SCMBestTime = new HyTekTime(BestTimes[i].Score, BestTimes[i].Course);
                    }
                }
            }
        }

        ControlCollection Collection = CollectionOwner.Controls;

        if (IncludeSCY)
        {
            Label YardBestTimeLabel = new Label();
            YardBestTimeLabel.Text = YardBestTime.ToString();
            Collection.Add(YardBestTimeLabel);
            if (IncludeLCM)
            {
                Literal Break = new Literal();
                Break.Text = "<br />";
                Collection.Add(Break);
            }
        }
        if (IncludeLCM)
        {
            Label MeterBestTimeLabel = new Label();
            MeterBestTimeLabel.Text = MeterBestTime.ToString();
            Collection.Add(MeterBestTimeLabel);
            if (IncludeSCM)
            {
                Literal Break = new Literal();
                Break.Text = "<br />";
                Collection.Add(Break);
            }
        }
        if (IncludeSCM)
        {
            Label SCMBestTimeLabel = new Label();
            SCMBestTimeLabel.Text = SCMBestTime.ToString();
            Collection.Add(SCMBestTimeLabel);
        }

    }
}