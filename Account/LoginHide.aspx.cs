using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink RegisterLink = ((HyperLink)LoginView1.Controls[0].FindControl("RegisterHyperLink"));
        if (RegisterLink != null)
            RegisterLink.NavigateUrl = "CreateAccount.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        //CheckApplicationStatus();
    }
    protected void LoginUser_LoggedIn(object sender, EventArgs e)
    {
        Login tempLogin = ((Login)LoginView1.Controls[0].FindControl("LoginUser"));

        MembershipUser CurrentUser = Membership.GetUser(tempLogin.UserName);

        object temp = CurrentUser.ProviderUserKey;
        Guid currentUserId = (Guid)CurrentUser.ProviderUserKey;
        try
        {

            FamiliesBLL FamilyAdapter = new FamiliesBLL();

            SwimTeamDatabase.FamiliesDataTable families = FamilyAdapter.GetFamilyID(currentUserId);
            SwimTeamDatabase.FamiliesRow CurrentFamily = families[0];

            int FamilyID = CurrentFamily.FamilyID;

            Session.Add("FamilyID", FamilyID);

            ProfileCommon p = Profile.GetProfile(CurrentUser.UserName);

            if (string.IsNullOrEmpty(p.FamilyID))
            {
                p.FamilyID = FamilyID.ToString();
                p.Save();
            }
        }
        catch
        {
            ;
        }
        finally
        {
            if (Roles.IsUserInRole(CurrentUser.UserName, "Administrator") ||
                  Roles.IsUserInRole(CurrentUser.UserName, "Coach") ||
                  Roles.IsUserInRole(CurrentUser.UserName, "OfficeManager") ||
                  Roles.IsUserInRole(CurrentUser.UserName, "DatabaseManager"))
                tempLogin.DestinationPageUrl = "~/Features/Default.aspx";
        }


    }
    private void CheckApplicationStatus()
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();
        SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetApplicationStatusSetting();
        SwimTeamDatabase.SettingsRow ApplicationStatus = settings[0];

        if (ApplicationStatus.SettingValue != "Online")
        {
            ApplicationOfflineLablel.Visible = true;
            LoginView1.Visible = false;
        }
    }
}
