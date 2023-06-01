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
        CheckApplicationStatus();
        if (!Roles.RoleExists("Administrator"))
            Roles.CreateRole("Administrator");
        MembershipUserCollection Users = Membership.FindUsersByName("ApplicationAdmin");
        if (Users.Count == 0)
        {
            MembershipUser AdminAccount = Membership.CreateUser("ApplicationAdmin", "toledoswim");
            Roles.AddUserToRole(AdminAccount.UserName, "Administrator");
        }

        //TODO: Redirect user to appropriate page if already logged in
        if (User.Identity.IsAuthenticated)
        {
            if (Roles.IsUserInRole(User.Identity.Name, "Administrator") ||
                  Roles.IsUserInRole(User.Identity.Name, "Coach") ||
                  Roles.IsUserInRole(User.Identity.Name, "OfficeManager") ||
                  Roles.IsUserInRole(User.Identity.Name, "DatabaseManager"))
                Response.Redirect("~/Features/Default.aspx");
            else
                Response.Redirect("~/Parents/Default.aspx");
        }
        
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
        catch (Exception)
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
    protected void LoginFailure(object sender, EventArgs e)
    {
        // Determine why the user could not login...
        Login myLogin = ((Login)sender);
        myLogin.FailureText = "Your login attempt was not successful. Please try again.";

        // Does there exist a User account for this user?
        MembershipUser usrInfo = Membership.GetUser(myLogin.UserName);
        if (usrInfo != null)
        {
            // Is this user locked out?
            if (usrInfo.IsLockedOut)
            {
                myLogin.FailureText = "Your account has been locked out because of too many invalid login attempts. Please contact cpierson@sev.org to have your account unlocked.";
            }
            else if (!usrInfo.IsApproved)
            {
                myLogin.FailureText = "Your account has not yet been approved. You cannot login until you activate your account. Follow the link that was e-mailed to you. If you never recieved an e-mail, contact cpierson@sev.org.";
            }
        }

    }
}
