using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using System.IO;

public partial class Account_PasswordMailer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ResetPassword(object sender, EventArgs e)
    {
        MembershipUserCollection Users = Membership.FindUsersByEmail(EmailAddressTextBox.Text);

        if (Users.Count > 0)
        {
            int numberreset = 0, numbermailed = 0;
            foreach (MembershipUser user in Users)
            {
                if (user.IsLockedOut)
                {
                    ResultLabel.Text = "Unable to send change password for your account: " + user.UserName +
                        " because the account is Locked out. You must contact cpierson@sev.org to have your account unlocked.";
                    GoBackLink.Visible = true;
                    GoBackLink.Text = "<< Go Back to Login Page";
                    break;
                }
                String temppassword = user.ResetPassword();
                String newpassword = RandomPasswordGenerator.GenerateRandomPassword();
                bool sucess = user.ChangePassword(temppassword, newpassword);

                if (sucess)
                {
                    numberreset++;
                    String username = user.UserName;

                    try
                    {
                        MailAddress FromAddress = new MailAddress("cpierson@sev.org");
                        MailAddress ToAddress = new MailAddress(user.Email);
                        MailAddress BCCAddress = new MailAddress("cpierson@sev.org");

                        MailMessage Notification = new MailMessage(FromAddress, ToAddress);
                        Notification.Bcc.Add(BCCAddress);

                        String body = "Your account on the GTAC Tools Website has been changed.<br />" +
                                        "<br />" +
                                        "Your password has been changed.<br />" +
                                        "<br />" +
                                        "Username: <%Username%><br />" +
                                        "Password: <%Password%><br />" +
                                        "<br />" +
                                        "Make sure you are logging-in at the correct page. (Numerous people have been trying to " +
                                        "log-in at the wrong page.)<br /><br />The correct page is <a href=\"http://tools.gtacswim.info/tools/Account/Login.aspx\">" +
                                        "http://tools.gtacswim.info/tools/Account/Login.aspx</a><br />" +
                                        "<br />Once you are logged in, you can change your password by clicking on your username in the " +
                                        "upper right hand corner of the page.<br /><br />" +
                                        "-- <br />" +
                                        "Chris Pierson<br />" +
                                        "-Greater Toledo Aquatic Club<br />" +
                                        "-OSI Webmaster";

                        body = body.Replace("<%Username%>", username);
                        body = body.Replace("<%Password%>", newpassword);


                        Notification.Subject = "GTAC Tools Password Reset";
                        Notification.Body = body;
                        Notification.IsBodyHtml = true;

                        SmtpClient Client = new SmtpClient();
                        Client.Send(Notification);

                        numbermailed++;

                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    ResultLabel.Text = "There was an error creating a random password for accout: " + user.UserName +
                        " You must contact cpierson@sev.org to have your password created.";
                    GoBackLink.Visible = true;
                    GoBackLink.Text = "<< Go Back to Login Page";
                    break;
                }
            }

            ResultLabel.Visible = true;
            if (numbermailed == numberreset)
                if (numbermailed == 0 && numberreset == 0)
                    numberreset = 0;
                else if (numberreset == 1)
                    ResultLabel.Text = "Password reset and mailed for this account. (Remember that it can take several mintues for an e-mail to show up.)<br /><br />";
                else
                    ResultLabel.Text = "Password reset and mailed for " + numberreset + " accounts. (Remember that it can take several mintues for an e-mail to show up.)<br /><br />";
            else
            {
                if (numberreset == 1 && numbermailed == 0)
                    ResultLabel.Text = "Password reset for this account, but there was an error sending the" +
                        " email. Please try again.";
                else
                {
                    if (numberreset == 1)
                        ResultLabel.Text = "Password reset for " + numberreset + " account";
                    else
                        ResultLabel.Text = "Password reset for " + numberreset + " accounts";
                    if (numbermailed == 0)
                        ResultLabel.Text += ", but the e-mail failed to send for all of them. Please try again.";
                    else if (numbermailed == 1)
                        ResultLabel.Text += ", but the e-mail was only sent for " + numbermailed +
                            ". You can try again. (Remember that it can take several mintues for an e-mail to show up, for the e-mails that did send.)";
                }

                ResultLabel.Text += "<br /><br />";
            }
            GoBackLink.Text = "<< Go Back to Login Page";
            GoBackLink.Visible = true;
        }
        else
        {
            ResultLabel.Visible = true;
            ResultLabel.Text = "User account not found.<br /><br />";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
            GoBackLink.Visible = true;
            GoBackLink.Text = "<< Go Back to Login Page";
        }
    }

    private static class RandomPasswordGenerator
    {
        public static String GenerateRandomPassword()
        {
            List<String> PasswordParts = new List<string>();
            PasswordParts.Add("Decorum");
            PasswordParts.Add("Orderly");
            PasswordParts.Add("Bricklayer");
            PasswordParts.Add("Android");
            PasswordParts.Add("Fledgling");
            PasswordParts.Add("Population");
            PasswordParts.Add("Pancreas");
            PasswordParts.Add("Gramophone");
            PasswordParts.Add("Garment");
            PasswordParts.Add("Healer");
            PasswordParts.Add("Sneer");
            PasswordParts.Add("Probation");
            PasswordParts.Add("Furrow");
            PasswordParts.Add("Nuance");
            PasswordParts.Add("Classmate");
            PasswordParts.Add("Bewilderment");
            PasswordParts.Add("Spotting");
            PasswordParts.Add("Seeing");
            PasswordParts.Add("Incline");
            PasswordParts.Add("Factor");
            PasswordParts.Add("Settle");
            PasswordParts.Add("Damage");
            PasswordParts.Add("Season");
            PasswordParts.Add("Here");
            PasswordParts.Add("Passing");
            PasswordParts.Add("Possible");
            PasswordParts.Add("Bite");
            PasswordParts.Add("Dying");
            PasswordParts.Add("Wire");
            PasswordParts.Add("Guy");
            PasswordParts.Add("Role");
            PasswordParts.Add("Worrying");
            PasswordParts.Add("Sentence");
            PasswordParts.Add("Hide");
            PasswordParts.Add("Selection");
            PasswordParts.Add("Structure");
            PasswordParts.Add("Button");
            PasswordParts.Add("Term");
            PasswordParts.Add("Incompatible");
            PasswordParts.Add("Content");
            PasswordParts.Add("Connected");
            PasswordParts.Add("Largest");
            PasswordParts.Add("Illegal");
            PasswordParts.Add("Many");
            PasswordParts.Add("Scientific");
            PasswordParts.Add("Raw");
            PasswordParts.Add("Basis");
            PasswordParts.Add("Tailors");
            PasswordParts.Add("Steady");
            PasswordParts.Add("Surface");
            PasswordParts.Add("Curse");
            PasswordParts.Add("Burns");
            PasswordParts.Add("How");
            PasswordParts.Add("Will");
            PasswordParts.Add("Infrequent");
            PasswordParts.Add("Convenience");
            PasswordParts.Add("Enter");
            PasswordParts.Add("Enabling");
            PasswordParts.Add("Aardvark");
            PasswordParts.Add("Will");
            PasswordParts.Add("Unknown");
            PasswordParts.Add("Problem");
            PasswordParts.Add("Display");
            PasswordParts.Add("Bomb");
            PasswordParts.Add("Hardship");
            PasswordParts.Add("Collapses");
            PasswordParts.Add("Ally");
            PasswordParts.Add("Motor");
            PasswordParts.Add("Wading");
            PasswordParts.Add("Police");
            PasswordParts.Add("Nuts");
            PasswordParts.Add("Cuckoo");
            PasswordParts.Add("Pardon");
            PasswordParts.Add("Damnation");
            PasswordParts.Add("Content");
            PasswordParts.Add("Inside");
            PasswordParts.Add("After");
            PasswordParts.Add("Round");
            PasswordParts.Add("Ripped");
            PasswordParts.Add("Adventure");
            PasswordParts.Add("Register");
            PasswordParts.Add("Arrive");
            PasswordParts.Add("Default");
            PasswordParts.Add("Sell");
            PasswordParts.Add("Joking");
            PasswordParts.Add("Expire");
            PasswordParts.Add("Scan");
            PasswordParts.Add("Golf");
            PasswordParts.Add("Believed");
            PasswordParts.Add("Stage");
            PasswordParts.Add("Learn");
            PasswordParts.Add("Typed");
            PasswordParts.Add("Collating");
            PasswordParts.Add("Duplicating");
            PasswordParts.Add("Deleted");
            PasswordParts.Add("Budget");
            PasswordParts.Add("Declined");
            PasswordParts.Add("Demolish");
            PasswordParts.Add("Surname");
            PasswordParts.Add("Joint");
            PasswordParts.Add("Dispute");
            PasswordParts.Add("Spare");
            PasswordParts.Add("Lined");
            PasswordParts.Add("Utter");
            PasswordParts.Add("Bring");
            PasswordParts.Add("Knight");
            PasswordParts.Add("Cupboard");
            PasswordParts.Add("Carrot");
            PasswordParts.Add("Assist");
            PasswordParts.Add("Gasoline");
            PasswordParts.Add("Hanging");
            PasswordParts.Add("Society");
            PasswordParts.Add("Smoke");
            PasswordParts.Add("Competence");
            PasswordParts.Add("Timer");
            PasswordParts.Add("Perceiving");
            PasswordParts.Add("Raving");
            PasswordParts.Add("Flat");
            PasswordParts.Add("Bell");
            PasswordParts.Add("Dependent");
            PasswordParts.Add("Pronoun");
            PasswordParts.Add("Rhythm");
            PasswordParts.Add("Horse");
            PasswordParts.Add("Shouting");
            PasswordParts.Add("Finance");
            PasswordParts.Add("Cabinet");
            PasswordParts.Add("Rhyme");
            PasswordParts.Add("Postulate");
            PasswordParts.Add("Horde");
            PasswordParts.Add("Photograph");
            PasswordParts.Add("Pressing");
            PasswordParts.Add("Calculator");
            PasswordParts.Add("Integral");
            PasswordParts.Add("Try");
            PasswordParts.Add("Spit");
            PasswordParts.Add("Deduction");
            PasswordParts.Add("Biologist");
            PasswordParts.Add("Lasting");
            PasswordParts.Add("Command");
            PasswordParts.Add("Knowing");
            PasswordParts.Add("Gas");
            PasswordParts.Add("Bake");
            PasswordParts.Add("Baking");
            PasswordParts.Add("Baked");
            PasswordParts.Add("Gassed");
            PasswordParts.Add("Finished");
            PasswordParts.Add("Transmitter");
            PasswordParts.Add("Sacrifice");
            PasswordParts.Add("Climbing");
            PasswordParts.Add("Hydrogen");

            Random rand = new Random();
            String FirstPart = PasswordParts[rand.Next(PasswordParts.Count)];
            String SecondPart = PasswordParts[rand.Next(PasswordParts.Count)];



            return FirstPart + rand.Next(1000, 9999) + SecondPart;
        }
    }
}