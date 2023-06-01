using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Security;

public partial class Parents_PickSessions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime MeetStartDate;
        SwimTeamDatabase.SwimmersRow swimmer = new SwimTeamDatabase.SwimmersDataTable().NewSwimmersRow();
        if (Request.QueryString["USAID"] != null)
        {
            String USAID = new USAIDEncryptor(Request.QueryString["USAID"], USAIDEncryptor.EncryptionStatus.Encrypted)
            .GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
            swimmer = new SwimmersBLL().GetSwimmerByUSAID(USAID)[0];
            HeaderLabel.Text = HeaderLabel.Text.Replace("[name]", swimmer.PreferredName);
            SwimmerNameHiddenField.Value = swimmer.PreferredName + " " + swimmer.LastName;
            if (Request.QueryString["MeetID"] != null)
            {
                MeetsBLL MeetsAdapter = new MeetsBLL();
                SwimTeamDatabase.MeetsRow meet = MeetsAdapter.GetMeetByMeetID(int.Parse(Request.QueryString["MeetID"]))[0];
                MeetStartDate = MeetsAdapter.MeetStartDate(int.Parse(Request.QueryString["MeetID"]));
                AgeAtMeetLabel.Text = new BirthdayHelper(swimmer.Birthday).AgeOnDate(MeetStartDate).ToString();



                if (meet.MeetName.ToUpper().Contains("THE"))
                    MeetNameHiddenField.Value = meet.MeetName;
                else
                    MeetNameHiddenField.Value = "The " + meet.MeetName;

                HeaderLabel.Text = HeaderLabel.Text.Replace("[meet]", MeetNameHiddenField.Value);
            }
            else
            {
                Response.Redirect("~/Parents/FamilyView.aspx?Error=4");
            }

            if (Request.QueryString["Error"] != null)
            {
                if (Request.QueryString["Error"] == "1")
                {
                    ErrorLabel.Text = "The swimmer was not entered in any sessions. Please make sure you check " +
                        "off a session that was not previously checked. (If all available sessions have already been " +
                        "entered, you will always see this error message.)<br /><br />";
                    ErrorLabel.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("~/Parents/FamilyView.aspx?Error=3", true);
        }
    }
    protected void CustomizeRow(object sender, GridViewRowEventArgs e)
    {
        GridViewRow Row = e.Row;
        if (Row.RowType == DataControlRowType.DataRow)
        {
            Label WarmUpTimeLabel = ((Label)Row.FindControl("WarmUpTimeLabel"));
            String WarmupTimeText = WarmUpTimeLabel.Text;
            WarmupTimeText = WarmupTimeText.Substring(WarmupTimeText.IndexOf(" ") + 1);
            String HoursPart = WarmupTimeText.Substring(0, WarmupTimeText.LastIndexOf(":"));
            String AMPM = WarmupTimeText.Substring((WarmupTimeText.Length - 2), 2);

            WarmUpTimeLabel.Text = HoursPart + " " + AMPM;

            HiddenField WarmUpGuessHiddenField = ((HiddenField)e.Row.FindControl("WarmUpGuessHiddenField"));
            if (WarmUpGuessHiddenField.Value == "True")
                WarmUpTimeLabel.Text += "<br /><i>(This is a guesstimate)</i>";

            Label AgeLabel = ((Label)e.Row.FindControl("AgeLabel"));

            int MinAge = int.Parse(((HiddenField)e.Row.FindControl("MinAgeHiddenField")).Value);
            int MaxAge = int.Parse(((HiddenField)e.Row.FindControl("MaxAgeHiddenField")).Value);

            if ((MinAge == 0) && (MaxAge == 99))
                AgeLabel.Text = "All Ages";
            else if (MinAge == 0)
                AgeLabel.Text = MaxAge + " and Under";
            else if (MaxAge == 99)
                AgeLabel.Text = MinAge + " and Over";
            else
                AgeLabel.Text = MinAge + "-" + MaxAge;

            CheckBox SelectSessionCheckBox = ((CheckBox)e.Row.FindControl("SelectSessionCheckBox"));
            int AgeAtMeet = int.Parse(AgeAtMeetLabel.Text);
            if ((AgeAtMeet < MinAge) || (AgeAtMeet > MaxAge))
                SelectSessionCheckBox.Visible = false;

            Label PrelimFinalsLabel = ((Label)e.Row.FindControl("PrelimFinalsLabel"));
            if (PrelimFinalsLabel.Text == "False")
                PrelimFinalsLabel.Text = "Timed Finals";
            else
                PrelimFinalsLabel.Text = "Prelim/Finals";
        }
    }
    protected void EnterSessionsButtonClicked(object sender, EventArgs e)
    {
        EntryBLL EntriesAdapter = new EntryBLL();
        String USAID = new USAIDEncryptor(Request.QueryString["USAID"], USAIDEncryptor.EncryptionStatus.Encrypted)
        .GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
        int MeetID = int.Parse(Request.QueryString["MeetID"]);
        SwimTeamDatabase.EntriesDataTable entries = EntriesAdapter.GetEntriesByMeetAndSwimmer(MeetID, USAID);

        List<int> Sessions = new List<int>();
        foreach (SwimTeamDatabase.EntriesRow entry in entries)
            Sessions.Add(entry.SessionID);

        bool FirstTimeEnteredInMeet = true;
        if (Sessions.Count > 0)
            FirstTimeEnteredInMeet = false;
        bool EnteredInSession = false;
        int SessionsAdded = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                CheckBox SelectSessionCheckBox = ((CheckBox)GridView1.Rows[i].FindControl("SelectSessionCheckBox"));
                int SessionID = int.Parse(((HiddenField)GridView1.Rows[i].FindControl("SessionIDHiddenField")).Value);
                if ((!Sessions.Contains(SessionID)) && (SelectSessionCheckBox.Checked))
                {
                    bool AddingResult = EntriesAdapter.AddSwimmerToSession(SessionID, USAID, MeetID);
                    if (AddingResult)
                    {
                        EnteredInSession = true;
                        SessionsAdded++;
                    }
                }
            }
        }

        bool emailsucess = true;
        if (EnteredInSession && SessionsAdded == 0)
        {
            int FamilyID = new SwimmersBLL().GetSwimmerByUSAID(USAID)[0].FamilyID;
            CreditsBLL CreditsAdapter = new CreditsBLL();
            CreditsAdapter.SubtractCreditFromFamily(FamilyID);

            MailMessage email = new MailMessage("cpierson@sev.org", Membership.GetUser().Email);
            email.Bcc.Add("cpierson@sev.org");
            email.Subject = SwimmerNameHiddenField.Value + " entered in " + MeetNameHiddenField.Value;
            email.Body = "There seems to have been an error entering " + SwimmerNameHiddenField.Value +
            " in " + MeetNameHiddenField.Value + ". The swimmer was not added to any sessions. Please go back and" +
       "try to enter the swimmer again. If the error persists, I have already been notified and may contact you to try" +
       "and figure out what is causing this bug because I can't figure out why it happens." +
       "<br /><br />--Chris Pierson<br />-Greater Toledo Aquatic Club" + "<br />-OSI Webmaster";
            email.IsBodyHtml = true;

            if (SessionsAdded == 1)
                email.Body = email.Body.Replace("sessions", "session");

            try
            {

                System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                Client.Send(email);
            }
            catch (Exception)
            {
                emailsucess = false;
            }
        }
        else
        {
            if (FirstTimeEnteredInMeet && EnteredInSession)
            {
                int FamilyID = new SwimmersBLL().GetSwimmerByUSAID(USAID)[0].FamilyID;
                CreditsBLL CreditsAdapter = new CreditsBLL();
                CreditsAdapter.SubtractCreditFromFamily(FamilyID);

                MailMessage email = new MailMessage("cpierson@sev.org", Membership.GetUser().Email);
                email.Bcc.Add("cpierson@sev.org");
                email.Subject = SwimmerNameHiddenField.Value + " entered in " + MeetNameHiddenField.Value;
                email.Body = SwimmerNameHiddenField.Value + " has been entered in " + SessionsAdded + " sessions " +
                    "in " + MeetNameHiddenField.Value + ".<br /><br />--Chris Pierson<br />-Greater Toledo Aquatic Club" +
                    "<br />-OSI Webmaster";
                email.IsBodyHtml = true;

                if (SessionsAdded == 1)
                    email.Body = email.Body.Replace("sessions", "session");

                try
                {

                    System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                    Client.Send(email);
                }
                catch (Exception)
                {
                    emailsucess = false;
                }
            }
            else if (!FirstTimeEnteredInMeet && EnteredInSession)
            {
                int FamilyID = new SwimmersBLL().GetSwimmerByUSAID(USAID)[0].FamilyID;
                CreditsBLL CreditsAdapter = new CreditsBLL();
                CreditsAdapter.SubtractCreditFromFamily(FamilyID);

                MailMessage email = new MailMessage("cpierson@sev.org", Membership.GetUser().Email);
                email.Bcc.Add("cpierson@sev.org");
                email.Subject = SwimmerNameHiddenField.Value + " entered in " + MeetNameHiddenField.Value;
                email.Body = SwimmerNameHiddenField.Value + " has been entered in " + SessionsAdded + " additional sessions " +
                    "in " + MeetNameHiddenField.Value + ".<br /><br />--Chris Pierson<br />-Greater Toledo Aquatic Club" +
                    "<br />-OSI Webmaster";
                email.IsBodyHtml = true;

                if (SessionsAdded == 1)
                    email.Body = email.Body.Replace("sessions", "session");

                try
                {

                    System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                    Client.Send(email);
                }
                catch (Exception)
                {
                    emailsucess = false;
                }
            }
        }





        String RedirectPage = "~/Parents/FamilyView.aspx";
        RedirectPage += "?Sessions=" + SessionsAdded + "&email=";
        if (emailsucess)
            RedirectPage += "1";
        else
            RedirectPage += "0";
        if (EnteredInSession)
            Response.Redirect(RedirectPage, true);
        else
        {
            RedirectPage = "~/Parents/PickSessions.aspx?USAID=";
            RedirectPage += Request.QueryString["USAID"];
            RedirectPage += "&MeetID=";
            RedirectPage += Request.QueryString["MeetID"];
            RedirectPage += "&Error=1";

            Response.Redirect(RedirectPage, true);
        }
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        GridView Grid = ((GridView)sender);
        EntryBLL EntriesAdapter = new EntryBLL();
        String USAID = new USAIDEncryptor(Request.QueryString["USAID"], USAIDEncryptor.EncryptionStatus.Encrypted)
        .GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
        int MeetID = int.Parse(Request.QueryString["MeetID"]);
        SwimTeamDatabase.EntriesDataTable entries = EntriesAdapter.GetEntriesByMeetAndSwimmer(MeetID, USAID);

        List<int> Sessions = new List<int>();
        foreach (SwimTeamDatabase.EntriesRow entry in entries)
            Sessions.Add(entry.SessionID);

        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
            {
                CheckBox SelectSessionCheckBox = ((CheckBox)Grid.Rows[i].FindControl("SelectSessionCheckBox"));
                int SessionID = int.Parse(((HiddenField)Grid.Rows[i].FindControl("SessionIDHiddenField")).Value);
                if (Sessions.Contains(SessionID))
                {
                    SelectSessionCheckBox.Checked = true;
                    SelectSessionCheckBox.Enabled = false;
                    SelectSessionCheckBox.Visible = true;
                }
            }
        }
    }
}