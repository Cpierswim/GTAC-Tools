using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Globalization;

public partial class Parents_FamilyView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OpenMeetsForCoaches();
            JobSignUpsBLL JobSignupsAdapter = new JobSignUpsBLL();
            if (JobSignupsAdapter.AnyOpenings())
                this.JobSignupButton.Visible = true;
        }

        //FamilyIDHiddenField.Value = Session.Contents["FamilyID"].ToString();
        FamilyIDHiddenField.Value = Profile.FamilyID;
        int FamilyID = int.Parse(FamilyIDHiddenField.Value);
        MeetCreditsLabel.Text = new CreditsBLL().GetCreditsByFamilyID(FamilyID)[0].NumberOfCredits + "";

        try
        {
            int ErrorCode = int.Parse(Request.QueryString["Error"]);

            if (ErrorCode == 1)
            {
                ErrorLabel.Text = "Unable to Edit Swimmer. USA Swimming ID Number not found. " +
                    "Please try again.<br /><br />If the problem persists, please contact " +
                    "Chris Pierson at cpierson@sev.org.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 2)
            {
                ErrorLabel.Text = "Error saving swimmer. Swimmer information was not updated. " +
                    "Please try again.<br /><br />If the problem persists, please contact " +
                    "Chris Pierson at cpierson@sev.org.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 3)
            {
                ErrorLabel.Text = "You were redirected here because the swimmer's ID was not passed " +
                    "to the PickSessions.aspx page.<br /><br />Please try again using the links below. " +
                    "By using the \"Manage Meets\" button below, rather than entering in the page by hand. " +
                    "The page will work.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 4)
            {
                ErrorLabel.Text = "You were redirected here because the Meet ID was not passed " +
                    "to the PickSessions.aspx page.<br /><br />Please try again using the links below. " +
                    "By using the \"Manage Meets\" button below, rather than entering in the page by hand. " +
                    "The page will work.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 9)
            {
                ErrorLabel.Text = "You were redirected here because the Meet ID was not passed " +
                    "to the Meets.aspx page.<br /><br />Please try again using the links below. " +
                    "By using the \"Manage Meets\" button below, rather than entering in the page by hand. " +
                    "The page will work.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 5)
            {
                ErrorLabel.Text = "Unable to find which family to render Calendar for. Please click on \"View " +
                    "My Personal Calendar\" again.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 6)
            {
                ErrorLabel.Text = "The meet entry page was unable to find which swimmer to enter. Please click" +
                    " \"Manage Meets\" on the row of the swimmer you which to enter in a meet.";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 7)
            {
                ErrorLabel.Text = "There was an unknown error when entering the sessions. If you ever reach this error" +
                    " please contact Chris at cpierson@sev.org. The database could be corrupted.";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 8)
            {
                ErrorLabel.Text = "There was an unknown error when removing the swimmer from the sessions. If you ever reach" +
                    " this error, please contact Chris at cpierson@sev.org. The database could be corrupted.";
                ErrorLabel.Visible = true;
            }
        }
        catch (Exception)
        {
        }
        try
        {
            int BanquetMessageCode = int.Parse(Request.QueryString["BQM"]);
            if (BanquetMessageCode == 1)
            {
                if (!string.IsNullOrEmpty(ErrorLabel.Text))
                    ErrorLabel.Text += "<br />";
                ErrorLabel.Text = "You have sucessfully signed up your family for the banquet.";
                ErrorLabel.Visible = true;
            }
            else if (BanquetMessageCode == 2)
            {
                if (!string.IsNullOrEmpty(ErrorLabel.Text))
                    ErrorLabel.Text += "<br />";
                ErrorLabel.Text = "There was an error signing up for family for the banquet.  You were NOT sucessfully signed up.";
                ErrorLabel.Visible = true;
            }
            else if (BanquetMessageCode == 3)
            {
                if (!string.IsNullOrEmpty(ErrorLabel.Text))
                    ErrorLabel.Text += "<br />";
                ErrorLabel.Text = "You have sucessfully withdrawn from the banquet.";
                ErrorLabel.Visible = true;
            }
            else if (BanquetMessageCode == 2)
            {
                if (!string.IsNullOrEmpty(ErrorLabel.Text))
                    ErrorLabel.Text += "<br />";
                ErrorLabel.Text = "There was an error withdrawing your family from the banquet.  You were NOT sucessfully withdrawn.";
                ErrorLabel.Visible = true;
            }
        }
        catch (Exception)
        {
        }
        try
        {
            int SessionsAdded = int.Parse(Request.QueryString["Sessions"]);
            if (SessionsAdded == 0)
            {
                MeetEntryAddedLabel.Text = "No sessions were added. If you believe there was an error " +
                    "please contact Chris Pierson at cpierson@sev.org<br /><br />";
                MeetEntryAddedLabel.Visible = true;
            }
            else
            {
                MeetEntryAddedLabel.Text = "Added swimmer to " + SessionsAdded + " sessions.";
                if (SessionsAdded == 1)
                    MeetEntryAddedLabel.Text = MeetEntryAddedLabel.Text.Replace("sessions", "session");
                String emailsuccess = Request.QueryString["email"];
                if (emailsuccess == "1")
                    SessionsEmailLabel.Text = "<br />An email was sucessfully sent to the email address for this account.";
                else if (emailsuccess == "0")
                    SessionsEmailLabel.Text = "<br />An attempt was made to email a notification to the email address" +
                        " for this account, but the email failed to send.";
                MeetEntryAddedLabel.Visible = true;
                SessionsEmailLabel.Visible = true;
            }
        }
        catch (Exception) { }
        finally
        {
            if (MeetEntryAddedLabel.Visible && ErrorLabel.Visible)
                MeetEntryAddedLabel.Text = "<br /><br />" + MeetEntryAddedLabel.Text;
        }

        try
        {
            int SessionsRemoved = int.Parse(Request.QueryString["SessionsR"]);
            if (SessionsRemoved == 0)
            {
                MeetEntryRemovedLabel.Text = "No sessions were removed. If you believe there was an error " +
                    "please contact Chris Pierson at cpierson@sev.org<br /><br />";
                MeetEntryRemovedLabel.Visible = true;
            }
            else
            {
                MeetEntryRemovedLabel.Text = "Removed swimmer from " + SessionsRemoved + " sessions.";
                if (SessionsRemoved == 1)
                    MeetEntryRemovedLabel.Text = MeetEntryRemovedLabel.Text.Replace("sessions", "session");
                String emailsuccess = Request.QueryString["email"];
                if (emailsuccess == "1")
                    SessionsEmailLabel.Text = "<br />An email was sucessfully sent to the email address for this account.";
                else if (emailsuccess == "0")
                    SessionsEmailLabel.Text = "<br />An attempt was made to email a notification to the email address" +
                        " for this account, but the email failed to send.";
                MeetEntryRemovedLabel.Visible = true;
                SessionsEmailLabel.Visible = true;
            }
        }
        catch (Exception) { }
        finally
        {
            if ((MeetEntryAddedLabel.Visible || ErrorLabel.Visible) && MeetEntryRemovedLabel.Visible)
                MeetEntryRemovedLabel.Text = "<br /><br />" + MeetEntryRemovedLabel.Text;
        }

        SettingsBLL SettingsAdapter = new SettingsBLL();
        if (SettingsAdapter.DisplayBanquetButton())
        {
            BanquetButton.Visible = true;
            BanquetButton.Text = SettingsAdapter.GetBanquetButtonText();

            BanquetSignUpsBLL BanquetAdapter = new BanquetSignUpsBLL();
            if (BanquetAdapter.IsFamilySignedUpForBanquet(int.Parse(Profile.FamilyID)))
                BanquetButton.Text = "Edit Banquet Signup Info";
        }
    }

    protected void RegisterNewSwimmerButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            //test to see if the FamilyID already exists in the Session
            string test = Session.Contents["FamilyID"].ToString();

            //since we got here, it does so just go to the add swimmer page
            Response.Redirect("AddNewSwimmer.aspx", true);
        }
        catch (Exception)
        {
            //since we got here, FamilyID does not exist in the Session (probably because they were on the page
            //for a really long time) so set it and then go to the add
            //swimmer page

            Session.Add("FamilyID", FamilyIDHiddenField.Value);
            Response.Redirect("AddNewSwimmer.aspx", true);
        }
    }
    protected void GroupIDLabel_DataBinding(object sender, EventArgs e)
    {
        Label SendingLabel = ((Label)sender);

        GroupsBLL GroupAdapter = new GroupsBLL();

        int GroupID = int.Parse(SendingLabel.Text);
        if (String.IsNullOrEmpty(GroupsListHiddenField.Value))
            GroupsListHiddenField.Value += GroupID.ToString();
        else
            GroupsListHiddenField.Value += "," + GroupID;

        SwimTeamDatabase.GroupsDataTable groups = GroupAdapter.GetGroupByGroupID(GroupID);
        SendingLabel.Text = groups[0].GroupName;
    }
    protected void ActiveLabel_DataBinding(object sender, EventArgs e)
    {
        Label SendingLabel = ((Label)sender);

        bool Inactive = bool.Parse(SendingLabel.Text);
        if (Inactive)
            SendingLabel.Text = "Inactive";
        else
            SendingLabel.Text = "Active";
    }
    protected void ApprovedLabel_DataBinding(object sender, EventArgs e)
    {
        Label SendingLabel = ((Label)sender);
        bool ReadyToAdd = bool.Parse(SendingLabel.Text);
        if (ReadyToAdd)
            SendingLabel.Text = "Yes";
        else
            SendingLabel.Text = "No";
    }
    protected void RowButton_Clicked(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.CompareTo("EditSwimmer") == 0)
        {
            String USAID = ((String)SwimmersGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            Session.Add("EditSwimmer", USAID);
            String EditButtonText = ((Button)e.CommandSource).Text;
            if (EditButtonText == "Sign Up For Next Season")
                Response.Redirect("EditSwimmer.aspx?NS=1", true);
            else
                Response.Redirect("EditSwimmer.aspx", true);
        }
        else if (e.CommandName.CompareTo("ManageMeet") == 0)
        {
            String USAID = ((String)SwimmersGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            String EncryptedUSAID = new USAIDEncryptor(USAID, USAIDEncryptor.EncryptionStatus.Unencrypted)
                .GetUSAID(USAIDEncryptor.EncryptionStatus.Encrypted);
            Response.Redirect("~/Parents/Meet/Meets2.aspx?ID=" + EncryptedUSAID, true);
        }
    }
    protected void EditParentsButtonClicked(object sender, EventArgs e)
    {
        try
        {
            //test to see if the FamilyID already exists in the Session
            string test = Session.Contents["FamilyID"].ToString();
            //set it just in case
            Session.Contents["FamilyID"] = FamilyIDHiddenField.Value;

            //since we got here, it does so just go to the add swimmer page
        }
        catch (Exception)
        {
            //since we got here, FamilyID does not exist in the Session (probably because they were on the page
            //for a really long time) so set it and then go to the add
            //swimmer page

            Session.Add("FamilyID", FamilyIDHiddenField.Value);
        }
        Response.Redirect("EditParents.aspx", true);
    }
    protected void ViewEventsButtonCicked(object sender, EventArgs e)
    {
        Response.Redirect("~/Parents/Meet/ViewEntries.aspx");
    }
    protected void ParentsSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        int numberofparents = ((SwimTeamDatabase.ParentsDataTable)e.ReturnValue).Count;
        if (numberofparents == 0)
            Response.Redirect("AddParents.aspx?Code=1");
    }
    protected void ViewPersonalCalendarButtonCLicked(object sender, EventArgs e)
    {
        Session.Add("GroupsString", GroupsListHiddenField.Value);
        List<string> USAIDList = new List<string>();
        for (int i = 0; i < SwimmersGridView.Rows.Count; i++)
        {
            Control c = SwimmersGridView.Rows[i].FindControl("USAIDHiddenField");
            if (c != null)
            {
                HiddenField USAIDHiddenField = (HiddenField)c;
                USAIDList.Add(USAIDHiddenField.Value);
            }
        }
        Session.Add("USAIDList", null);

        Response.Redirect("~/Parents/Calendar.aspx", true);
    }
    protected void BanquetButtonClicked(object sender, EventArgs e)
    {
        Response.Redirect("~/Parents/Banquet.aspx", true);
    }
    protected void GridViewDatabound(object sender, EventArgs e)
    {

        foreach (GridViewRow Row in SwimmersGridView.Rows)
        {
            //AttendanceBLL AttendanceAdapter = new AttendanceBLL();
            if (Row.RowType == DataControlRowType.DataRow)
            {
                Label ActiveLabel = ((Label)Row.FindControl("ActiveLabel"));
                Button ManageMeetsButton = ((Button)Row.FindControl("ManageMeetsButton"));
                Button EditSwimmerButton = ((Button)Row.FindControl("EditSwimmerButton"));

                if (ActiveLabel.Text == "Inactive")
                {
                    ManageMeetsButton.Enabled = false;
                    EditSwimmerButton.Text = "Sign Up For Next Season";
                }
                else
                {
                    ManageMeetsButton.Enabled = true;
                    EditSwimmerButton.Text = "Edit Swimmer";
                }
            }
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //e.Row.datait
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView SwimmerView = (DataRowView)e.Row.DataItem;
            AttendanceBLL AttendanceAdapter = new AttendanceBLL();

            int GroupID = int.Parse(SwimmerView["GroupID"].ToString());
            String USAID = SwimmerView["USAID"].ToString();

            String PercentageString = this.GetAttendancePercentageString(GroupID, USAID);

            //if (!TotalPracticesForGroupDictionary.ContainsKey(GroupID))
            //    TotalPracticesForGroupDictionary.Add(GroupID, AttendanceAdapter.GetTotalPractiesForCertainGroup(GroupID));

            //int TotalPractices = TotalPracticesForGroupDictionary[GroupID];

            //SwimTeamDatabase.AttendanceDataTable AttendancesForSwimmer = AttendanceAdapter.GetAttendancesByUSAID(USAID);

            //SettingsBLL SettingsAdapter = new SettingsBLL();
            //DateTime StartDate = SettingsAdapter.GetRegistrationStartDate();
            //DateTime EndDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

            //SwimTeamDatabase.AttendanceDataTable AttendancesForGroup = AttendanceAdapter.GetBetweenTwoDatesByGroupID(GroupID, StartDate, EndDate);

            //String PercentageString = string.Empty;

            //if (TotalPractices == 0)
            //    PercentageString = "NA";
            //else
            //{
            //    double AttendnancePercentage;
            //    int PracticesGoneTo = 0;
            //    foreach (SwimTeamDatabase.AttendanceRow Attendance in AttendancesForSwimmer)
            //        if (Attendance.AttendanceType != "A")
            //            PracticesGoneTo++;

            //    Dictionary<AttendanceBLL.PracticeInfo, SwimTeamDatabase.AttendanceRow> AttendanceList =
            //        AttendanceAdapter.GetListForSwimmer(USAID, StartDate, EndDate, GroupID);
            //    //for (int i = 0; i < AttendanceList.Keys.Count; i++)
            //    //{

            //    //}

            //    AttendnancePercentage = (double.Parse(AttendancesForSwimmer.Count.ToString()) / double.Parse(TotalPractices.ToString())) * 100.0;
            //    AttendnancePercentage = Math.Round(AttendnancePercentage, 1);
            //    PercentageString = AttendancesForSwimmer.Count.ToString() + " of " + TotalPractices.ToString() + "= " + AttendnancePercentage.ToString() +
            //        "%";

            //    SwimTeamDatabase.PracticeDataTable AllPracticesOffered = AttendanceAdapter.GetPracticesForGroup(GroupID);
            //    SwimTeamDatabase.PracticeDataTable SwimmersPractices = AttendanceAdapter.GetPracticesSwimmerAttended(USAID);

            //    int PracticesOffered = AllPracticesOffered.Count;
            //    CultureInfo myCI = new CultureInfo("en-US");
            //    System.Globalization.Calendar cal = myCI.Calendar;
            //    int WeekOfFirstPracticeOffered = -1;
            //    int WeekOfFirstPracticeAttended = -1;
            //    int ReducePracticesOfferedBy = 0;
            //    if (AllPracticesOffered.Count == 0)
            //        PercentageString = "NA";
            //    else
            //    {
            //        WeekOfFirstPracticeOffered = cal.GetWeekOfYear(AllPracticesOffered[0].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            //        if (SwimmersPractices.Count > 0)
            //        {
            //            WeekOfFirstPracticeAttended = cal.GetWeekOfYear(SwimmersPractices[0].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
            //            for (int i = 0; i < AllPracticesOffered.Count; i++)
            //                if (AllPracticesOffered[i].Date.DayOfYear == SwimmersPractices[0].Date.DayOfYear &&
            //                    AllPracticesOffered[i].PracticeoftheDay == SwimmersPractices[0].PracticeoftheDay)
            //                    break;
            //                else
            //                    ReducePracticesOfferedBy++;
            //            WeekOfFirstPracticeAttended = WeekOfFirstPracticeAttended + ((SwimmersPractices[0].Date.Year - AllPracticesOffered[0].Date.Year) * 52);
            //            if (WeekOfFirstPracticeAttended - WeekOfFirstPracticeOffered > 2)
            //                PracticesOffered = PracticesOffered - ReducePracticesOfferedBy;

            //            AttendnancePercentage = SwimmersPractices.Count / PracticesOffered;

            //            PercentageString = AttendancesForSwimmer.Count.ToString() + " of " + TotalPractices.ToString() + "= " + 
            //                AttendnancePercentage.ToString() + "%";
            //        }
            //        else
            //        {
            //            PercentageString = "0 of " + PracticesOffered + "=  0%";
            //        }
            //    }
            //}

            ((Label)e.Row.FindControl("AttendanceLabel")).Text = PercentageString;
        }
    }

    private String GetAttendancePercentageString(int GroupID, String USAID)
    {
        AttendanceBLL AttendanceAdapter = new AttendanceBLL();
        String PercentageString = "";

        SwimTeamDatabase.PracticeDataTable AllPracticesOffered = AttendanceAdapter.GetPracticesForGroup(GroupID);
        SwimTeamDatabase.PracticeDataTable SwimmersPractices = AttendanceAdapter.GetPracticesSwimmerAttended(USAID);
        SwimTeamDatabase.AttendanceDataTable SwimmersAttendances = AttendanceAdapter.GetAttendancesByUSAID(USAID);

        double AttendnancePercentage;
        int PracticesOffered = AllPracticesOffered.Count;
        CultureInfo myCI = new CultureInfo("en-US");
        System.Globalization.Calendar cal = myCI.Calendar;
        int WeekOfFirstPracticeOffered = -1;
        int WeekOfFirstPracticeAttended = -1;
        int ReducePracticesOfferedBy = 0;
        if (AllPracticesOffered.Count == 0)
            PercentageString = "NA";
        else
        {
            WeekOfFirstPracticeOffered = cal.GetWeekOfYear(AllPracticesOffered[0].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            if (SwimmersPractices.Count > 0)
            {
                WeekOfFirstPracticeAttended = cal.GetWeekOfYear(SwimmersPractices[0].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                for (int i = 0; i < AllPracticesOffered.Count; i++)
                    if (AllPracticesOffered[i].Date.DayOfYear == SwimmersPractices[0].Date.DayOfYear &&
                        AllPracticesOffered[i].PracticeoftheDay == SwimmersPractices[0].PracticeoftheDay)
                        break;
                    else
                        ReducePracticesOfferedBy++;
                WeekOfFirstPracticeAttended = WeekOfFirstPracticeAttended + ((SwimmersPractices[0].Date.Year - AllPracticesOffered[0].Date.Year) * 52);
                if (WeekOfFirstPracticeAttended - WeekOfFirstPracticeOffered > 2)
                    PracticesOffered = PracticesOffered - ReducePracticesOfferedBy;

                //AttendnancePercentage = SwimmersPractices.Count / PracticesOffered;
                AttendnancePercentage = (Convert.ToDouble(SwimmersPractices.Count) / Convert.ToDouble(PracticesOffered)) * 100.0;
                AttendnancePercentage = Math.Round(AttendnancePercentage, 1);

                int ExcusedPractices = 0;
                for (int i = 0; i < SwimmersAttendances.Count; i++)
                {
                    if (SwimmersAttendances[i].AttendanceType.ToUpper() == "EA")
                        ExcusedPractices++;
                }

                if (ExcusedPractices > 0)
                    PracticesOffered = PracticesOffered - ExcusedPractices;

                PercentageString = SwimmersPractices.Count+ " of " + PracticesOffered + "= " +
                    AttendnancePercentage.ToString() + "%";
            }
            else
            {
                PercentageString = "0 of " + PracticesOffered + "=  0%";
            }
        }

        return PercentageString;
    }

    private Dictionary<int, int> _totalPracticesForGroupDictionary;
    private Dictionary<int, int> TotalPracticesForGroupDictionary
    {
        get
        {
            if (this._totalPracticesForGroupDictionary == null)
                this._totalPracticesForGroupDictionary = new Dictionary<int, int>();
            return this._totalPracticesForGroupDictionary;
        }
        set
        {
            this._totalPracticesForGroupDictionary = value;
        }
    }

    private void OpenMeetsForCoaches()
    {
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable MeetsToOpen = MeetsAdapter.GetMeetsToOpenForCoachces();
        foreach (SwimTeamDatabase.MeetsV2Row Meet in MeetsToOpen)
        {
            GroupCoachesBLL GroupCoachesAdapter = new GroupCoachesBLL();
            SwimTeamDatabase.GroupCoachDataTable GroupCoaches = GroupCoachesAdapter.GetAllGroupCoaches();
            List<String> Emails = new List<string>();
            for (int i = 0; i < GroupCoaches.Count; i++)
                if (!Emails.Contains(GroupCoaches[i].CoachEmail))
                    Emails.Add(GroupCoaches[i].CoachEmail);

            if (Emails.Count > 0)
            {
                DateTime CoachDeadline = Meet.Deadline.AddDays(2);
                while (CoachDeadline.DayOfWeek == DayOfWeek.Saturday || CoachDeadline.DayOfWeek == DayOfWeek.Sunday)
                    CoachDeadline = CoachDeadline.AddDays(1);
                CoachDeadline = new DateTime(CoachDeadline.Year, CoachDeadline.Month, CoachDeadline.Day, 14, 0, 0);

                //MailMessage email = new MailMessage("cpierson@sev.org", Emails[0]);
                //for (int i = 1; i < Emails.Count; i++)
                //    email.To.Add(Emails[i]);

                //email.Bcc.Add("cpierson@sev.org");
                //email.Subject = Meet.MeetName + " open for entry";
                //if (!Meet.MeetName.Contains("The"))
                //    email.Body = "The ";
                //email.Body += Meet.MeetName + " has closed. Please pick entries for your swimmers. If this meet has a late" +
                //    " entry period, you will recieve an e-mail when a swimmer in your group is added during the late entry period." +
                //    "<br /><br />The deadline for you to pick your entries for this meet is " + CoachDeadline.ToString("dddd MMMM d") +
                //    ", by " + CoachDeadline.ToString("h:mm tt") +
                //    "<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club<br />-OSI Webmaster";
                //email.IsBodyHtml = true;

                Emailer mail = new Emailer();
                for (int i = 0; i < Emails.Count; i++)
                    mail.To.Add(Emails[i]);
                mail.Subject = Meet.MeetName + " open for entry";
                if (!Meet.MeetName.Contains("The"))
                    mail.Message = "The ";
                mail.Message += Meet.MeetName + " has closed. Please pick entries for your swimmers. If this meet has a late" +
                    " entry period, you will recieve an e-mail when a swimmer in your group is added during the late entry period." +
                    "<br /><br />The deadline for you to pick your entries for this meet is " + CoachDeadline.ToString("dddd MMMM d") +
                    ", by " + CoachDeadline.ToString("h:mm tt") +
                    "<br /><br />--<br />Chris Pierson<br />-Greater Toledo Aquatic Club<br />-OSI Webmaster";
                mail.IsHTML = true;


                bool emailsuccess = true;
                try
                {
                    //System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                    //Client.Send(email);
                    mail.Send();

                }
                catch (Exception)
                {
                    emailsuccess = false;
                }

                if (emailsuccess)
                {

                    Meet.OpenForCoaches = true;
                    MeetsAdapter.Update(Meet);
                }
            }
        }
    }

    protected void JobSignupButtonClicked(object sender, EventArgs e)
    {
        Response.Redirect("~/Parents/JobSignup.aspx");
    }
}