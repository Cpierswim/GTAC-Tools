using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Features_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool DatabaseManager = Roles.IsUserInRole("DatabaseManager");
        bool OfficeManager = Roles.IsUserInRole("OfficeManager");
        bool Coach = Roles.IsUserInRole("Coach");
        bool Administrator = Roles.IsUserInRole("Administrator");

        if (DatabaseManager)
            GroupViewLink1.DisplayAs = UserControls_Hyperlinks_GroupViewLink.DisplayType.DatabaseManager;
        else if (OfficeManager)
            GroupViewLink1.DisplayAs = UserControls_Hyperlinks_GroupViewLink.DisplayType.OfficeManager;
        else if (Coach)
            GroupViewLink1.DisplayAs = UserControls_Hyperlinks_GroupViewLink.DisplayType.Coach;

       

        if (DatabaseManager)
            ListOfSwimmersHyperlink1.DisplayAs = UserControls_Hyperlinks_ListOfSwimmersHyperlink.DisplayType.DatabaseManager;
        else if (OfficeManager)
            ListOfSwimmersHyperlink1.DisplayAs = UserControls_Hyperlinks_ListOfSwimmersHyperlink.DisplayType.OfficeManager;

        if (DatabaseManager)
            DatabaseManagerSpecialPageHyperLink1.DisplayAs = UserControls_Hyperlinks_DatabaseManagerHyperLink.DisplayType.DatabaseManager;

        if (OfficeManager)
            ApproveSwimmersHyperLink1.DisplayAs = UserControls_Hyperlinks_ApproveSwimmersHyperLink.DisplayType.OfficeManager;

        if (OfficeManager)
            TopTensSinceHyperLink1.DisplayAs = UserControls_Hyperlinks_TopTensSinceHyperLink.DisplayType.OfficeManager;

        if (DatabaseManager)
            MessagesHyperLink1.DisplayAs = UserControls_Hyperlinks_MessagesHyperLink.DisplayType.DatabaseManager;
        else if (OfficeManager)
            MessagesHyperLink1.DisplayAs = UserControls_Hyperlinks_MessagesHyperLink.DisplayType.OfficeManager;

        if (DatabaseManager)
            SetSwimmersAsInDatabaseHyperLink1.DisplayAs = UserControls_Hyperlinks_SetSwimmersAsInDatabaseHyperLink.DisplayType.DatabaseManager;

        if (OfficeManager)
            ManageCreditsHyperLink1.DisplayAs = UserControls_Hyperlinks_ManageCreditsHyperLink.DisplayType.OfficeManager;
        if (OfficeManager)
        {
            BanquetListHyperLink1.DisplayAs = UserControls_Hyperlinks_BanquetListHyperLink.DisplayType.OfficeManager;
        }

        if (Coach)
            GroupEmailer1.DisplayAs = UserControls_Hyperlinks_GroupEmailer.DisplayType.Coach;
        if (Coach)
            AttendanceHyperLink1.DisplayAs = UserControls_Hyperlinks_AttendanceHyperLink.DisplayType.Coach;
        if (DatabaseManager)
            ViewMeetEntriesHyperlink1.DisplayAs = UserControls_Hyperlinks_ViewMeetEntriesHyperlink.DisplayType.DatabaseManager;
        if (DatabaseManager)
            EventsManagerHyperLink1.DisplayAs = UserControls_Hyperlinks_EventsManagerHyperLink.DisplayType.DatabaseManager;

        if (Coach)
            CoachViewCalendar1.DisplayAs = UserControls_Hyperlinks_CoachViewCalendar.DisplayType.Coach;

        if (Administrator)
        {
            CreateSpecialAccountHyperLink1.DisplayAs = UserControls_Hyperlinks_CreateSpecialAccountHyperLink.DisplayType.Administrator;
            SetupApplicationHyperLink1.DisplayAs = UserControls_Hyperlinks_SetupApplicationHyperLink.DisplayType.Administrator;
            ManageGroupsHyperLink1.DisplayAs = UserControls_Hyperlinks_ManageGroupsHyperLink.DisplayType.Administrator;
            ManagerUserAccountsHyperLink1.DisplayAs = UserControls_Hyperlinks_ManagerUserAccountsHyperLink.DisplayType.Administrator;
        }


        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        int ActiveSwimmers = SwimmersAdapter.NumberOfActiveSwimmers();
        if (ActiveSwimmers == 1)
            NumberOfActiveSwimmersTextBox.Text = "There is currently 1 active GTAC swimmer.";
        else
            NumberOfActiveSwimmersTextBox.Text = "There are currently " + ActiveSwimmers.ToString() + " active GTAC" +
                " swimmers.";

        int NumRegSwimmersSinceRegDate = SwimmersAdapter.NumberCreatedSinceRegistrationDate();
        SettingsBLL SettingsAdapter = new SettingsBLL();
        DateTime RegistrationDate = SettingsAdapter.GetRegistrationStartDate();
        if (NumRegSwimmersSinceRegDate == 1)
            NumberRegisteredSinceRegDateTextBox.Text = "There has been 1 swimmer registered since " + RegistrationDate.ToString("d");
        else
            NumberRegisteredSinceRegDateTextBox.Text = "There have been " + NumRegSwimmersSinceRegDate + " swimmers in the water since " +
                RegistrationDate.ToString("d");

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
}