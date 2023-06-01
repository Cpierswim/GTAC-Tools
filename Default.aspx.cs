using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.RoleExists("Administrator"))
            Roles.CreateRole("Administrator");
        if (Membership.FindUsersByName("GTACADMIN").Count == 0)
        {
            Membership.CreateUser("GTACADMIN", "sdf#$kd!DK");
        }

        if (User.IsInRole("Parent"))
            Response.Redirect("~/Parents/FamilyView.aspx");
        if (User.IsInRole("Administrator") || User.IsInRole("Coach") ||
            User.IsInRole("DatabaseManager") || User.IsInRole("OfficeManager"))
            Response.Redirect("~/Features/Default.aspx");
    }
}
