using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

public partial class Account_xxCreateCoachAccountxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.RoleExists("Coach"))
            Roles.CreateRole("Coach");
        if (!Page.IsPostBack)
            Membership.DeleteUser("CpierswimTest");
    }
    protected void UserCreated(object sender, EventArgs e)
    {
        // Get the UserId of the just-added user
        MembershipUser newUser = Membership.GetUser(CreateCoachWizard.UserName);
        Guid newUserId = (Guid)newUser.ProviderUserKey;

        newUser.IsApproved = true;

        Roles.AddUserToRole(newUser.UserName, "Coach");
    }
    protected void RedirectToFeaturesPage(object sender, EventArgs e)
    {
        Response.Redirect("~/Features/Default.aspx");
    }
}