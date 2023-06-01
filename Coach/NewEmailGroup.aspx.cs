﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class Coach_NewEmailGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HttpContext.Current.User.IsInRole("DatabaseManager"))
        {
            this.DropDownList1.Visible = false;
        }

        //this.DropDownList1.Visible = false;

        if (HttpContext.Current.User != null)
            if (HttpContext.Current.User.Identity != null)
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                    if (!String.IsNullOrEmpty(Membership.GetUser(HttpContext.Current.User.Identity.Name).Email))
                        this.FromTextBox.Text = Membership.GetUser(HttpContext.Current.User.Identity.Name).Email;
        if (this.FromTextBox.Text == "cpierson@sev.org" && String.IsNullOrWhiteSpace(this.MainTextBox.Text))
            this.MainTextBox.Text = "\n\n--\nChris Pierson\n-Greater Toledo Aquatic Club\n-OSI Webmaster";

        this.ErrorLabel.Visible = false;
    }

    protected void ChangeDropDownListType(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedItem.Value == "By Group")
        {
            this.GroupsPanel.Visible = true;
            this.MeetsPanel.Visible = false;
        }
        else
        {
            this.GroupsPanel.Visible = false;
            this.MeetsPanel.Visible = true;
        }
    }
    protected void LoadEmails(object sender, EventArgs e)
    {
        bool SwimmersAlso = false;
        if (this.RadioButtonList1.SelectedValue != "Parents")
            SwimmersAlso = true;

        List<String> EmailList = new List<string>();
        EmailList.Add("tkurth@sfstoledo.org");
        if (this.DropDownList1.SelectedValue == "By Group")
        {
            List<int> GroupIDs = new List<int>();


            for (int i = 0; i < this.GroupsCheckBoxList.Items.Count; i++)
                if (this.GroupsCheckBoxList.Items[i].Selected)
                    GroupIDs.Add(int.Parse(this.GroupsCheckBoxList.Items[i].Value));

            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            List<SwimTeamDatabase.SwimmersRow> Swimmers = new List<SwimTeamDatabase.SwimmersRow>();
            GroupCoachesBLL GroupCoachesAdapter = new GroupCoachesBLL();
            SwimTeamDatabase.GroupCoachDataTable GroupCoaches = GroupCoachesAdapter.GetAllGroupCoaches();
            for (int i = 0; i < GroupIDs.Count; i++)
            {
                SwimTeamDatabase.SwimmersDataTable SwimmersTable = SwimmersAdapter.GetSwimmersByGroupID(GroupIDs[i]);
                for (int j = 0; j < SwimmersTable.Count; j++)
                    if (!Swimmers.Contains(SwimmersTable[j]))
                        Swimmers.Add(SwimmersTable[j]);


                foreach (SwimTeamDatabase.GroupCoachRow GroupCoach in GroupCoaches)
                {
                    if (GroupCoach.GroupID == GroupIDs[i])
                        if (!EmailList.Contains(GroupCoach.CoachEmail))
                            EmailList.Add(GroupCoach.CoachEmail);
                }
            }
            List<int> FamilyIDs = new List<int>();

            for (int i = 0; i < Swimmers.Count; i++)
                if (!FamilyIDs.Contains(Swimmers[i].FamilyID))
                    FamilyIDs.Add(Swimmers[i].FamilyID);

            FamiliesBLL FamilyAdapter = new FamiliesBLL();
            for (int i = 0; i < FamilyIDs.Count; i++)
            {
                List<String> TempEmails = new List<string>();
                if (SwimmersAlso)
                    TempEmails = FamilyAdapter.GetDistinctListOfAllEmailsForFamily(FamilyIDs[i]);
                else
                    TempEmails = FamilyAdapter.GetDistinctListOfParentsEmailsForFamily(FamilyIDs[i]);

                for (int j = 0; j < TempEmails.Count; j++)
                    if (!EmailList.Contains(TempEmails[j]))
                        EmailList.Add(TempEmails[j]);
            }

        }
        else
        {
            List<int> MeetIDs = new List<int>();

            for (int i = 0; i < this.MeetsCheckBoxList.Items.Count; i++)
                if (this.MeetsCheckBoxList.Items[i].Selected)
                    MeetIDs.Add(int.Parse(this.MeetsCheckBoxList.Items[i].Value));

            PreEnteredV2BLL PreEntriesAdapter = new PreEnteredV2BLL();
            //EntryBLL EntriesAdapter = new EntryBLL();
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            List<String> USAIDs = new List<string>();
            for (int i = 0; i < MeetIDs.Count; i++)
            {
                SwimTeamDatabase.PreEnteredV2DataTable MeetPreEntries = PreEntriesAdapter.GetPreEntriesByMeetID(MeetIDs[i]);

                for (int j = 0; j < MeetPreEntries.Count; j++)
                    if (!USAIDs.Contains(MeetPreEntries[j].USAID))
                        USAIDs.Add(MeetPreEntries[j].USAID);
            }

            List<int> FamilyIDs = new List<int>();

            for (int i = 0; i < USAIDs.Count; i++)
            {
                SwimTeamDatabase.SwimmersRow Swimmer = SwimmersAdapter.GetSwimmerByUSAID(USAIDs[i])[0];
                if (!FamilyIDs.Contains(Swimmer.FamilyID))
                    FamilyIDs.Add(Swimmer.FamilyID);
            }

            FamiliesBLL FamilyAdapter = new FamiliesBLL();
            for (int i = 0; i < FamilyIDs.Count; i++)
            {
                List<String> TempEmails = new List<string>();
                if (SwimmersAlso)
                    TempEmails = FamilyAdapter.GetDistinctListOfAllEmailsForFamily(FamilyIDs[i]);
                else
                    TempEmails = FamilyAdapter.GetDistinctListOfParentsEmailsForFamily(FamilyIDs[i]);
                for (int j = 0; j < TempEmails.Count; j++)
                    if (!EmailList.Contains(TempEmails[j]))
                        EmailList.Add(TempEmails[j]);
            }
            GroupCoachesBLL GroupCoachesAdapter = new GroupCoachesBLL();
            SwimTeamDatabase.GroupCoachDataTable GroupCoaches = GroupCoachesAdapter.GetAllGroupCoaches();
            foreach (SwimTeamDatabase.GroupCoachRow GroupCoach in GroupCoaches)
            {
                if (!EmailList.Contains(GroupCoach.CoachEmail))
                    EmailList.Add(GroupCoach.CoachEmail);
            }
        }

        this.ToTextBox.Text = "";

        for (int i = 0; i < EmailList.Count; i++)
            this.ToTextBox.Text += EmailList[i] + ", ";
        if (this.ToTextBox.Text.EndsWith(", "))
            this.ToTextBox.Text = this.ToTextBox.Text.Substring(0, this.ToTextBox.Text.Length - 2);
    }
    protected void SendEmailClicked(object sender, EventArgs e)
    {
        MailMessage Email = new MailMessage();
        //Email.From = new MailAddress(FromTextBox.Text);
        Email.Bcc.Add(ToTextBox.Text);
        //Email.Sender = new System.Net.Mail.MailAddress(FromTextBox.Text);
        //Email.Subject = SubjectTextBox.Text;
        //Email.Body = this.MainTextBox.Text;
        //Email.IsBodyHtml = false;
        //Email.Bcc.Add("cpierson@sev.org");

        Emailer mailer = new Emailer();
        mailer.From = FromTextBox.Text;
        for (int i = 0; i < Email.Bcc.Count; i++)
            mailer.To.Add(Email.Bcc[i].ToString());
        mailer.Subject = SubjectTextBox.Text;
        mailer.Message = this.MainTextBox.Text;
        mailer.IsHTML = false;


        if (Email.Bcc.Count == 0)
        {
            this.ErrorLabel.Visible = true;
            this.ErrorLabel.Text = "There must be at least 1 e-mail address to send the e-mail to";
        }
        else
        {
            //MailAddress[] Address = new MailAddress[Email.Bcc.Count];

            //Email.Bcc.CopyTo(Address, 0);

            //SmtpClient Client = new SmtpClient();

            //Email.Bcc.Clear();
            //for (int i = 0; i < Address.Length; i++)
            //{
            //    Email.Bcc.Add(Address[i]);
            //    if (Email.Bcc.Count == 5)
            //    {
            //        Client.Send(Email);
            //        Email.Bcc.Clear();
            //    }
            //}

            //if (Email.Bcc.Count > 0)
            //    Client.Send(Email);

            mailer.Send();

            Response.Redirect("~/Features/Default.aspx", true);
        }
    }
}