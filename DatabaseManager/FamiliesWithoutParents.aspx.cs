using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class DatabaseManager_FamiliesWithoutParents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RowDatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int FamilyID = int.Parse(((HiddenField)e.Row.FindControl("FamilyIDHiddenField")).Value);
            Guid UserID = new Guid(((HiddenField)e.Row.FindControl("UserIDHiddenField")).Value);

            Label HTMLLabel = ((Label)e.Row.FindControl("HTMLLabel"));

            HTMLLabel.Text = "Username: ";
            MembershipUser User = Membership.GetUser(UserID);

            HTMLLabel.Text += User.UserName;
        }
    }
    protected void RowButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        GridView sourceGrid = ((GridView)e.CommandSource);
        GridViewRow sourceRow = sourceGrid.Rows[int.Parse(e.CommandArgument.ToString())];
        if (e.CommandName == "EmailFamily")
        {
            Guid UserID = new Guid(((HiddenField)sourceRow.FindControl("UserIDHiddenField")).Value);
            MembershipUser User = Membership.GetUser(UserID);
            String body = "You have not finished the registration process for your account on the GTAC Tools Website." +
                " You have created an account, but have not added parents yet.<br /><br />" +
                "Please go to <a href=\"http://tools.gtacswim.com\">http://tools.gtacswim.com</a> and login to finish " +
                "the registration process.<br /><br />" +
                "If you have not completed the registration process through to adding swimmers to your account, then " +
                "your swimmer(s) are not yet registered with GTAC.<br /><br />" +
                "If you have any questions, reply to this e-mail.<br /><br />" +
                "--<br />" +
                "Chris Pierson<br />" +
                "-Greater Toledo Aquatic Club<br />" +
                "-OSI Webmaster";

            MailMessage message = new MailMessage("cpierson@sev.org", User.Email);
            message.Bcc.Add("cpierson@sev.org");
            message.IsBodyHtml = true;
            message.Subject = "GTAC Tools Website Message";
            message.Body = body;

            new System.Net.Mail.SmtpClient().Send(message);

            NotificationLabel.Text = "Email sent to " + User.UserName;
        }
        else if (e.CommandName == "DeleteFamily")
        {
            int FamilyID = int.Parse(((HiddenField)sourceRow.FindControl("FamilyIDHiddenField")).Value);
            MembershipUser User = Membership.GetUser(new Guid(((HiddenField)sourceRow.FindControl("UserIDHiddenField")).Value));

            int familiesDeleted = new FamiliesBLL().DeleteFamily(FamilyID);
            String Username = User.UserName;
            String UserEmail = User.Email;
            bool memberDeleted = Membership.DeleteUser(User.UserName);

            NotificationLabel.Text = "Deleted " + familiesDeleted + " families. ";

            if (memberDeleted)
                NotificationLabel.Text += "Sucessfully deleted user " + Username + ".";
            else
                NotificationLabel.Text += "User " + Username + " was not sucessfully deleted.";

            NotificationLabel.ForeColor = System.Drawing.Color.Red;

            sourceGrid.DataBind();
        }
    }
}