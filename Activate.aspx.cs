using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Activate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
            StatusMessage.Text = "The UserId was not included in the querystring...";
        else
        {
            Guid userId;
            try
            {
                userId = new Guid(Request.QueryString["ID"]);
            }

            catch
            {
                StatusMessage.Text = "The UserId passed into the querystring is not in the " +
                     "proper format...";
                return;
            }

            MembershipUser usr = Membership.GetUser(userId);
            if (usr == null)
                StatusMessage.Text = "User account could not be found...";
            else
            {
                // Approve the user
                usr.IsApproved = true;


                Membership.UpdateUser(usr);
                StatusMessage.Text = "Your account has been approved. " +
                     "<br /><br />You will now be able to add swimmers to your account.";
                FormsAuthentication.SetAuthCookie(usr.UserName, true);

                ButtonExplainLabel.Visible = true;
                LoginButton.Visible = true;
            }
        }
    }
    protected void LoginButtonClicked(object sender, EventArgs e)
    {
        //This should never throw an error because the button can't be clicked if these
        //would throw an error.
        //Guid userId = new Guid(Request.QueryString["ID"]);
        //MembershipUser usr = Membership.GetUser(userId);
        //String password = usr.GetPassword();


        //FormsAuthentication.SetAuthCookie(usr.UserName, true);

        Guid userId = new Guid(Request.QueryString["ID"]);

        FamiliesBLL FamilyAdapter = new FamiliesBLL();

        SwimTeamDatabase.FamiliesDataTable families = FamilyAdapter.GetFamilyID(userId);
        SwimTeamDatabase.FamiliesRow CurrentFamily = families[0];

        int FamilyID = CurrentFamily.FamilyID;

        Session.Add("FamilyID", FamilyID);
        Profile.FamilyID = FamilyID.ToString();

        Response.Redirect("~/Parents/AddNewSwimmer.aspx");
    }
}