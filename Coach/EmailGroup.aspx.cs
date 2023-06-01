using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class Coach_EmailGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            FromTextBox.Text = Membership.GetUser().Email;
    }
    protected void LoadToBox(object sender, EventArgs e)
    {
        GroupsBLL GroupsAdapter = new GroupsBLL();
        List<String> Emails = new List<string>();

        if (EmailTypeRadioButtonList.SelectedValue == "Parents Only")
            Emails = GroupsAdapter.GetParentEmailsInGroup(int.Parse(GroupsDropDownList.SelectedValue));
        else if (EmailTypeRadioButtonList.SelectedValue == "Parents and Swimmers")
            Emails = GroupsAdapter.GetParentAndSwimmerEmailsInGroup(int.Parse(GroupsDropDownList.SelectedValue));

        String ToString = "";
        foreach (String Email in Emails)
        {
            if (ToString != "")
                ToString += ", " + Email;
            else
                ToString = Email;
        }

        ToTextBox.Text = ToString;
    }
    protected void SendMailClicked(object sender, EventArgs e)
    {
        MailMessage Email = new MailMessage();
        //Email.From = new MailAddress(FromTextBox.Text);
        Email.Bcc.Add(ToTextBox.Text);
        //Email.Sender = new System.Net.Mail.MailAddress(FromTextBox.Text);
        //Email.Subject = SubjectTextBox.Text;
        //Email.Body = BodyTextBox.Text;
        //Email.IsBodyHtml = false;
        //Email.Bcc.Add("cpierson@sev.org");


        //SmtpClient Client = new SmtpClient();
        //Client.Send(Email);

        Emailer mailer = new Emailer();
        for (int i = 0; i < Email.Bcc.Count; i++)
            mailer.To.Add(Email.Bcc[i].ToString());
        mailer.From = FromTextBox.Text;
        mailer.Subject = SubjectTextBox.Text;
        mailer.Message = BodyTextBox.Text;
        mailer.IsHTML = false;
        mailer.Send();

        Response.Redirect("~/Features/Default.aspx", true);
    }
}