using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Features_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool OfficeManager = HttpContext.Current.User.IsInRole("OfficeManager");
        bool DatabaseManager = HttpContext.Current.User.IsInRole("DatabaseManager");
        if (OfficeManager && !DatabaseManager)
        {
            MessagesBLL MessagesAdapter = new MessagesBLL();
            int MessageCount = MessagesAdapter.GetMessagesNotSeenByOfficeManager().Count;
            if (MessageCount > 0)
            {
                this.UpdateMessagesHyperLink.Text = MessageCount.ToString() + " Update Messages";
                this.UpdateMessagesHyperLink.Visible = true;
                this.UpdateMessagesHyperLink.NavigateUrl = "~/OfficeManager/Messages.aspx";
                this.LeadingLabel.Text = "<br /><br />";
                this.LeadingLabel.Visible = true;
                this.TrailingLabel.Text = "<br /><br />";
                this.TrailingLabel.Visible = true;
            }
        }
        else if (DatabaseManager)
        {
            MessagesBLL MessagesAdapter = new MessagesBLL();
            int MessageCount = MessagesAdapter.GetMessagesByNotSeenByDatabaseManager().Count;
            if (MessageCount > 0)
            {
                this.UpdateMessagesHyperLink.Text = MessageCount.ToString() + " Update Messages";
                this.UpdateMessagesHyperLink.Visible = true;
                this.UpdateMessagesHyperLink.NavigateUrl = "~/DatabaseManager/Messages.aspx";
                this.LeadingLabel.Text = "<br /><br />";
                this.LeadingLabel.Visible = true;
                this.TrailingLabel.Text = "<br /><br />";
                this.TrailingLabel.Visible = true;
            }
        } 

        if (HttpContext.Current.User.IsInRole("Coach"))
        {
            if (Profile.GroupID == null)
                Response.Redirect("~/Coach/GroupPick.aspx?RP=1");
            else
            {
                if (String.IsNullOrWhiteSpace(Profile.GroupID))
                {
                    Response.Redirect("~/Coach/GroupPick.aspx?RP=1");
                }
            }
        }
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        int ActiveSwimmers = SwimmersAdapter.NumberOfActiveSwimmers();
        if (ActiveSwimmers == 0)
            NumberOfActiveSwimmersTextBox.Text = "<br /><br />There are no currently active GTAC swimmers.";
        else if (ActiveSwimmers == 1)
            NumberOfActiveSwimmersTextBox.Text = "<br /><br />There is currently 1 active GTAC swimmer.";
        else
            NumberOfActiveSwimmersTextBox.Text = "<br /><br />There are currently " + ActiveSwimmers.ToString() + " active GTAC" +
                " swimmers.";

        int NumRegSwimmersSinceRegDate = SwimmersAdapter.NumberCreatedSinceRegistrationDate();
        SettingsBLL SettingsAdapter = new SettingsBLL();
        DateTime RegistrationDate = SettingsAdapter.GetRegistrationStartDate();
        if (NumRegSwimmersSinceRegDate == 0)
            NumberRegisteredSinceRegDateTextBox.Text = "<br />There have been no registered swimmers since " + RegistrationDate.ToString("d");
        if (NumRegSwimmersSinceRegDate == 1)
            NumberRegisteredSinceRegDateTextBox.Text = "<br />There has been 1 swimmer registered since " + RegistrationDate.ToString("d");
        else
            NumberRegisteredSinceRegDateTextBox.Text = "<br />There have been " + NumRegSwimmersSinceRegDate + " swimmers in the water since " +
                RegistrationDate.ToString("d");

        if (this.Request.QueryString["Error"] != null)
            if (this.Request.QueryString["Error"] == "1")
            {
                this.ErrorLabel.Text = "Error finding meet.<br /><br />";
                this.ErrorLabel.Visible = true;
            }
    }

    protected void DisplayBanquetStatus(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("OfficeManager") || HttpContext.Current.User.IsInRole("DatabaseManager"))
        {
            SettingsBLL SettingsAdapter = new SettingsBLL();
            this.BanquetLabel.Text = "<br /><br />The banquet signups are currently ";
            if (SettingsAdapter.DisplayBanquetButton())
                this.BanquetLabel.Text += "OPEN.";
            else
                this.BanquetLabel.Text += "CLOSED.";
        }
    }

    protected void DisplayDefaultGroup(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("Coach"))
        {
            if (Profile.GroupID == null)
                this.DefaultGroupLabel.Text = "<br /><br />Your group has not been set.";
            else
            {
                if (!String.IsNullOrWhiteSpace(Profile.GroupID))
                {
                    GroupsBLL GroupsAdapter = new GroupsBLL();
                    SwimTeamDatabase.GroupsDataTable Groups = GroupsAdapter.GetGroupByGroupID(int.Parse(Profile.GroupID));
                    if (Groups.Count == 1)
                        this.DefaultGroupLabel.Text = "<br /><br />Your group is " + Groups[0].GroupName + ".";
                    else
                        this.DefaultGroupLabel.Text = "<br /><br />Error loading which group you coach.";
                }
                else
                    this.DefaultGroupLabel.Text = "<br /><br />Your group has not been set.";
            }
        }
    }

    protected void WaitingToBeApprovedStatus(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("OfficeManager"))
        {
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersNotReadyToAdd();
            if (Swimmers.Count != 0)
            {
                this.WaitingToBeApprovedHyperLink.BackColor = System.Drawing.Color.Yellow;
                this.WaitingToBeApprovedHyperLink.Visible = true;
                this.WaitingToBeApprovedHyperLink.Text = "<br /><br />";
            }
            if (Swimmers.Count == 1)
                this.WaitingToBeApprovedHyperLink.Text += "There is a swimmer waiting for approval.";
            else if (Swimmers.Count > 1)
                this.WaitingToBeApprovedHyperLink.Text += "There are " + Swimmers.Count + " swimmers waiting for approval.";
        }

        SettingsBLL SettingsAdapter = new SettingsBLL();
        if (Request.Cookies["LastDownloaded"] != null)
        {
            String datestring = Request.Cookies["LastDownloaded"].Value;
            DateTime lastcontactdownload = DateTime.Parse(datestring.Substring(datestring.IndexOf("=") + 1));
            DateTime LastContactChanged = SettingsAdapter.GetContactInfoUpdatedTime();
            if (lastcontactdownload < LastContactChanged)
            {
                ContactsHyperLink.BackColor = System.Drawing.Color.Yellow;
                ContactsHyperLink.Text = "There have been contact updates since your last download. Click Here!";
            }
            else
            {
                ContactsHyperLink.Text = "No contact updates since last download.";
                ContactsHyperLink.BackColor = System.Drawing.Color.Transparent;
                ContactsHyperLink.Enabled = false;
            }

        }
        else
        {
            ContactsHyperLink.Text = "Click here to download a contacts file for GMail";
            ContactsHyperLink.BackColor = System.Drawing.Color.Transparent;
        }
    }

    protected void WaitingToBeAddedStatus(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("DatabaseManager"))
        {
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersReadyToAddButNotInDatabase();
            if (Swimmers.Count != 0)
            {
                this.WaitingToBeAddedHyperLink.BackColor = System.Drawing.Color.Yellow;
                this.WaitingToBeAddedHyperLink.Visible = true;
                this.WaitingToBeAddedHyperLink.Text = "<br /><br />";
            }
            if (Swimmers.Count == 1)
                this.WaitingToBeAddedHyperLink.Text += "There is a swimmer waiting to be added to the Hy-Tek database.";
            else if (Swimmers.Count > 1)
                this.WaitingToBeAddedHyperLink.Text += "There are " + Swimmers.Count + " swimmers waiting to be added to the Hy-Tek database.";
        }
    }
}