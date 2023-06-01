using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_SetupApplication : System.Web.UI.Page
{
    const string ParentsRoleName = "Parent";
    const string OfficeManagerRoleName = "OfficeManager";
    const string AdministratorRoleName = "Administrator";
    const string DatabaseManagerRoleName = "DatabaseManager";
    const string CoachRoleName = "Coach";

    protected void Page_Load(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        if (!Page.IsPostBack)
        {
            CreateRoles();
            CreateAdministratorAccount();
            SetApplicationStatus();

            BanquetTextTextBox.Text = SettingsAdapter.GetBanquetButtonText();

            if (SettingsAdapter.DisplayBanquetButton())
            {
                BanquetButtonDisplayLabel.Text = "<b>Currently Displayed</b>";
                SwitchBanquetButtonDisplayButton.Text = "Switch to Do not display";
            }
            else
            {
                BanquetButtonDisplayLabel.Text = "<b>Not Currenlty Displayed</b>";
                SwitchBanquetButtonDisplayButton.Text = "Switch to Display";
            }

            BanquetSignUpsBLL BanquetSignUpsAdapter = new BanquetSignUpsBLL();
            if (BanquetSignUpsAdapter.GetAllBanquetSignUps().Count != 0)
                DeleteBanquetSignupsButton.Visible = true;
        }

        RegStartDateCalendar.SelectedDate = SettingsAdapter.GetRegistrationStartDate();
        NotificationEmailTextBox.Text = SettingsAdapter.GetNewSwimmerNotificationEmail()[0].SettingValue;
        
        
    }

    private void CreateRoles()
    {
        SetupLabel.Text = string.Empty;

        if (!Roles.RoleExists(ParentsRoleName))
        {
            Roles.CreateRole(ParentsRoleName);
            SetupLabel.Text = SetupLabel.Text + "<p>Role Created: " + ParentsRoleName + "</ p>";
        }

        if (!Roles.RoleExists(OfficeManagerRoleName))
        {
            Roles.CreateRole(OfficeManagerRoleName);
            SetupLabel.Text = SetupLabel.Text + "<p>Role Created: " + OfficeManagerRoleName + "</ p>";
        }

        if (!Roles.RoleExists(AdministratorRoleName))
        {
            Roles.CreateRole(AdministratorRoleName);
            SetupLabel.Text = SetupLabel.Text + "<p>Role Created: " + AdministratorRoleName + "</ p>";
        }

        if (!Roles.RoleExists(DatabaseManagerRoleName))
        {
            Roles.CreateRole(DatabaseManagerRoleName);
            SetupLabel.Text = SetupLabel.Text + "<p>Role Created: " + DatabaseManagerRoleName + "</ p>";
        }

        if (!Roles.RoleExists(CoachRoleName))
        {
            Roles.CreateRole(CoachRoleName);
            SetupLabel.Text = SetupLabel.Text + "<p>Role Created: " + CoachRoleName + "</ p>";
        }

        SetupLabel.Text = SetupLabel.Text + "<p>Roles Setup Complete!</p>";
    }
    private void SetApplicationStatus()
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetApplicationStatusSetting();
        SwimTeamDatabase.SettingsRow ApplicationStatus = settings[0];


        ApplicationStatusDropDownList.SelectedValue = ApplicationStatus.SettingValue;

    }
    protected void ApplicationStatus_Changed(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SettingsAdapter.SetNewApplicationStatus(ApplicationStatusDropDownList.SelectedValue);
    }
    private void CreateAdministratorAccount()
    {
        MembershipUserCollection Users = Membership.FindUsersByName("ApplicationAdmin");
        if (Users.Count == 0)
        {
            MembershipUser AdminAccount = Membership.CreateUser("ApplicationAdmin", "toledoswim");
            Roles.AddUserToRole(AdminAccount.UserName, AdministratorRoleName);
            SetupLabel.Text += "<p>Administrator Account Created.</p>";
        }

        SetupLabel.Text += "<p>Administrator Account UserName: ApplicationAdmin<br >password: toledoswim</p>";
    }
    protected void NewRegStartDate(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SettingsAdapter.SetNewRegistrationStartDate(RegStartDateCalendar.SelectedDate);
    }
    private void FindAndSetSwimmerNotificationEmail()
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetNewSwimmerNotificationEmail();
        NotificationEmailTextBox.Text = "";
        if (settings != null)
            if (settings.Count != 0)
                NotificationEmailTextBox.Text = settings[0].SettingValue;
    }
    protected void SaveEmailButtonClicked(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SettingsAdapter.UpdateSwimmerAddedEmail(NotificationEmailTextBox.Text);
    }
    
    protected void SaveBanquetButtonTextButtonClicked(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SettingsAdapter.SetBanquetButtonText(BanquetTextTextBox.Text);
    }
    protected void SwitchBanquetButtonDisplayStatus(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        if (BanquetButtonDisplayLabel.Text.ToUpper().Contains("NOT"))
            SettingsAdapter.SetDisplayBanquetButtonStatus(true);
        else
            SettingsAdapter.SetDisplayBanquetButtonStatus(false);

        if (SettingsAdapter.DisplayBanquetButton())
        {
            BanquetButtonDisplayLabel.Text = "<b>Currently Displayed</b>";
            SwitchBanquetButtonDisplayButton.Text = "Switch to Do not display";
        }
        else
        {
            BanquetButtonDisplayLabel.Text = "<b>Not Currenlty Displayed</b>";
            SwitchBanquetButtonDisplayButton.Text = "Switch to Display";
        }
    }
    protected void DeleteBanquetSignups(object sender, EventArgs e)
    {
        BanquetSignUpsBLL BanquetSignUpsAdapter = new BanquetSignUpsBLL();
        BanquetSignUpsAdapter.DeleteAllBanquetSignUps();
        if (BanquetSignUpsAdapter.GetAllBanquetSignUps().Count != 0)
            DeleteBanquetSignupsButton.Visible = true;
        else
            DeleteBanquetSignupsButton.Visible = false;
    }
}