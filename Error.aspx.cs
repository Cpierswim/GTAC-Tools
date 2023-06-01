using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Security;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.User.IsInRole("DatabaseManager"))
                if (Session["Error"] != null)
                {
                    Exception ex = Session["Error"] as Exception;

                    DatabaseManagerLabel.Text = "Type: " + ex.GetType().ToString() + "<br /><br />";
                    DatabaseManagerLabel.Text += "Message: " + ex.Message.ToString() + "<br /><br />";
                    DatabaseManagerLabel.Text += "Stack Trace: " + ex.StackTrace.ToString() + "<br /><br />";
                    DatabaseManagerLabel.Text += "Source: " + ex.Source.ToString() + "<br /><br />";
                    DatabaseManagerLabel.Text += "Target Site: " + ex.TargetSite.ToString() + "<br /><br />";
                    bool InnerException = ex.InnerException != null;
                    int InnerExceptionCount = 0;
                    while (InnerException)
                    {
                        InnerExceptionCount++;
                        Exception Inner = ex.InnerException as Exception;

                        DatabaseManagerLabel.Text += "InnerException " + InnerExceptionCount + " Type: " + Inner.GetType().ToString() + "<br /><br />";
                        if (Inner.Message != null)
                            DatabaseManagerLabel.Text += "InnerException " + InnerExceptionCount + " Message: " + Inner.Message.ToString() + "<br /><br />";
                        if (Inner.StackTrace != null)
                            DatabaseManagerLabel.Text += "InnerException " + InnerExceptionCount + " Stack Trace: " + ex.StackTrace.ToString() + "<br /><br />";
                        if (Inner.TargetSite != null)
                            DatabaseManagerLabel.Text += "InnerException " + InnerExceptionCount + " Target Site: " + ex.TargetSite.ToString() + "<br /><br />";
                        if (Inner.GetType() == typeof(System.Net.WebException))
                        {
                            System.Net.WebException Webex = Inner as System.Net.WebException;

                            if (Webex.Response != null)
                                DatabaseManagerLabel.Text += "WebException " + InnerExceptionCount + " Response: " + Webex.Response.ToString() + "<br /><br />";
                            if(Webex.Status != null)
                                DatabaseManagerLabel.Text += "WebException " + InnerExceptionCount + " Status: " + Webex.Status.ToString() + "<br /><br />";
                        }
                        InnerException = Inner.InnerException != null;
                    }
                }
        }
    }
    protected void SendErrorMessage(object sender, EventArgs e)
    {
        MembershipUser User = Membership.GetUser(Page.User.Identity.Name);
        MailMessage Email = null;
        if (User != null)
        {
            if (User.UserName != null)
                Email = new MailMessage("website@gtacswim.com", "cpierson@sev.org",
                "ERROR in GTAC Tools - Message From User", ("Message From " + User.UserName + "<br /><br />" + this.TextBox1.Text));
            else
                Email = new MailMessage("website@gtacswim.com", "cpierson@sev.org",
            "ERROR in GTAC Tools - Message From User", ("Message From " + "unknown user" + "<br /><br />" + this.TextBox1.Text));
        }
        else
            Email = new MailMessage("website@gtacswim.com", "cpierson@sev.org",
            "ERROR in GTAC Tools - Message From User", ("Message From " + "unknown user" + "<br /><br />" + this.TextBox1.Text));

        Email.IsBodyHtml = true;
        Email.Priority = MailPriority.High;



        new System.Net.Mail.SmtpClient().Send(Email);

        if (User != null)
        {

            if (Roles.IsUserInRole(User.UserName, "Parent"))
                Response.Redirect("~/Parents/FamilyView.aspx");
            else if (Roles.IsUserInRole(User.UserName, "OfficeManager") ||
                Roles.IsUserInRole(User.UserName, "DatabaseManager") ||
                Roles.IsUserInRole(User.UserName, "Coach") ||
                Roles.IsUserInRole(User.UserName, "Administrator"))
                Response.Redirect("~/Features/Default.aspx");
            else
                Response.Redirect("~/Default.aspx");
        }
        else
            Response.Redirect("~/Default.aspx");
    }
}