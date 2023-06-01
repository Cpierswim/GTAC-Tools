using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class Admin_FamiliesWithNoSwimmers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int FamilyID = int.Parse(((HiddenField)e.Row.FindControl("FamilyIDHiddenField")).Value);
            Label NameLabel = ((Label)e.Row.FindControl("NameLabel"));
            Label EmailLabel = ((Label)e.Row.FindControl("EmailLabel"));
            HiddenField UsernameHiddenField = ((HiddenField)e.Row.FindControl("UsernameHiddenField"));
            SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParentsByFamilyID(FamilyID);

            //NameLabel.Text = "Family ID: " + FamilyID + "<br />";
            MembershipUser User = new FamiliesBLL().GetUserAccountForFamily(FamilyID);
            if (User != null)
            {
                NameLabel.Text = "Username: " + User.UserName + " (Created : " + User.CreationDate + ")<br />";
                UsernameHiddenField.Value = User.UserName;
            }
            else
            {
                NameLabel.Text = "No associated account.";
                UsernameHiddenField.Value = "";
            }

            SwimTeamDatabase.ParentsRow PrimaryContactParent = null;
            for (int i = 0; i < parents.Count; i++)
            {
                if (parents[i].PrimaryContact)
                {
                    NameLabel.Text += "Primary Contact: ";
                    PrimaryContactParent = parents[i];
                }
                else
                    NameLabel.Text += "Secondary Contact: ";
                NameLabel.Text += parents[i].FirstName + " " + parents[i].LastName + "<br />";
            }
            if (PrimaryContactParent != null)
                NameLabel.Text += "Primary Email: " + PrimaryContactParent.Email;
            else
                NameLabel.Text += "<br />" + "No parents added";

            if (User != null)
                EmailLabel.Text = "Account Email: " + User.Email;
            else
                EmailLabel.Text = "Account Email: ";
        }
    }
    protected void ButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        GridView source = ((GridView)e.CommandSource);
        GridViewRow row = source.Rows[int.Parse(e.CommandArgument.ToString())];
        String username = ((HiddenField)row.FindControl("UsernameHiddenField")).Value;
        int FamilyID = int.Parse(((HiddenField)row.FindControl("FamilyIDHiddenField")).Value);
        if (username != "")
        {
            if (e.CommandName == "EmailAccount")
                SendEmail(username);
            else if (e.CommandName == "DeleteAccount")
                DeleteAccount(username, FamilyID, source, true);
            else if (e.CommandName == "DeleteAccountNoEmail")
                DeleteAccount(username, FamilyID, source, false);
        }
        else
        {
            NotificationLabel.Text = "Exception: Row does not have an associated Account.<br /><br />";
            NotificationLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void SendEmail(String user)
    {
        MembershipUser User = Membership.GetUser(user);
        if (User != null)
        {
            String body = "You have not added any swimmers to your account on the GTAC Tools Website yet.<br /><br />" +
                "Please go to <a href=\"http://tools.gtacswim.com\">http://tools.gtacswim.com</a> and login to add a swimmer" +
                " to your account.<br /><br />" +
                "If you do not have a swimmer registered on the webstie, then swimmer has not completed their registration with GTAC.<br /><br />" +
                "If you have any questions, reply to this e-mail.<br /><br />" +
                "--<br />" +
                "Chris Pierson<br />" +
                "-Greater Toledo Aquatic Club<br />" +
                "-OSI Webmaster";
            MailMessage message = new MailMessage("cpierson@sev.org", User.Email);
            message.Bcc.Add("cpierson@sev.org");
            message.Subject = "GTAC Tools Website Message";
            message.Body = body;
            message.IsBodyHtml = true;
            new System.Net.Mail.SmtpClient().Send(message);

            NotificationLabel.Text = "Message sent to " + User.UserName + "<br /><br />";
        }
        else
        {
            NotificationLabel.Text = "Exception: Error finding username " + user;
            NotificationLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void DeleteAccount(String user, int FamilyID, GridView source, bool Email)
    {
        int FamiliesDeleted = new FamiliesBLL().DeleteFamily(FamilyID);
        MembershipUser User = Membership.GetUser(user);
        String EmailAddress = "";
        bool memberDeleted;
        if (User != null)
        {
            EmailAddress = User.Email;
            memberDeleted = Membership.DeleteUser(user);
        }
        else
            memberDeleted = false;
        

        NotificationLabel.Text = FamiliesDeleted + " families deleted.";
        if (memberDeleted)
            NotificationLabel.Text += " User " + user + " sucessfully deleted.";
        else
            NotificationLabel.Text += " User " + user + " was not deleted.";
        NotificationLabel.ForeColor = System.Drawing.Color.Red;

        if (memberDeleted && !String.IsNullOrEmpty(EmailAddress) && Email)
        {
            String body = "The Account " + user + " has been deleted on the GTAC Tools Website for not having added" +
                " any swimmers.<br /><br />" +
                "If you feel this was done in error, you can reply to this e-mail.<br /><br />" +
                "To register a swimmer with GTAC, you will have to create a new account.<br /><br />" +
                "--<br />" +
                "Chris Pierson<br />" +
                "-Greater Toledo Aquatic Club<br />" +
                "-OSI Webmaster<br />";

            MailMessage message = new MailMessage("cpierson@sev.org", EmailAddress);
            message.Bcc.Add("cpierson@sev.org");
            message.Subject = "GTAC Tools Website Message";
            message.Body = body;
            message.IsBodyHtml = true;
            new System.Net.Mail.SmtpClient().Send(message);
        }

        source.DataBind();
    }
}