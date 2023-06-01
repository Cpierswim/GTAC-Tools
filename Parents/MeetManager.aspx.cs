using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_MeetManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String USAID = new USAIDEncryptor(Request.QueryString["ID"], USAIDEncryptor.EncryptionStatus.Encrypted)
        .GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
        USAIDHiddenField.Value = USAID;

        //SwimTeamDatabase.SwimmersDataTable swimmers = new SwimmersBLL().GetSwimmerByUSAID(USAID);
        //if (swimmers.Count == 1)
        //{
        //    int Credits = new CreditsBLL().GetCreditsByFamilyID(swimmers[0].FamilyID)[0].NumberOfCredits;

        //    AvailableCreditsLabel.Text += Credits;

        //    MeetCreditsHiddenField.Value = Credits.ToString();
        //}


    }
    protected void SignupGridDataBound(object sender, EventArgs e)
    {
        //if (MeetCreditsHiddenField.Value == "0")
        //{
        //    EntryBLL EntriesAdapter = new EntryBLL();
        //    SwimTeamDatabase.EntriesDataTable entries = EntriesAdapter.GetEntriesForSwimmer(USAIDHiddenField.Value);
        //    List<int> CurrentEntriesInMeetsList = new List<int>();
        //    foreach (SwimTeamDatabase.EntriesRow entry in entries)
        //        if (!CurrentEntriesInMeetsList.Contains(entry.MeetID))
        //            CurrentEntriesInMeetsList.Add(entry.MeetID);


        //    GridView Grid = ((GridView)sender);
        //    for (int i = 0; i < Grid.Rows.Count; i++)
        //    {
        //        GridViewRow Row = Grid.Rows[i];
        //        if (Row.RowType == DataControlRowType.DataRow)
        //        {
        //            int MeetID = int.Parse(((HiddenField)Row.FindControl("MeetIDHiddenField")).Value);

        //            if (!CurrentEntriesInMeetsList.Contains(MeetID))
        //            {

        //                Button SignUpButton = ((Button)Row.Cells[Row.Cells.Count - 1].Controls[0]);
        //                SignUpButton.Enabled = false;
        //                SignUpButton.Text = "No Meet Credits";
        //            }
        //        }
        //    }
        //}
    }
    protected void SignUpButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        int RowID = int.Parse(e.CommandArgument.ToString());

        GridView Grid = ((GridView)sender);
        GridViewRow Row = Grid.Rows[RowID];

        int MeetID = int.Parse(((HiddenField)Row.FindControl("MeetIDHiddenField")).Value);

        String RedirectLocation = "~/Parents/PickSessions.aspx?";
        RedirectLocation += "MeetID=" + MeetID;
        RedirectLocation += "&USAID=" + Request.QueryString["ID"];



        Response.Redirect(RedirectLocation, true);
    }
    protected void SetMeetDates(object sender, GridViewRowEventArgs e)
    {
        GridViewRow Row = e.Row;
        if (Row.RowType == DataControlRowType.DataRow)
        {
            Label MeetDatesLabel = ((Label)Row.FindControl("MeetDatesLabel"));
            int MeetID = int.Parse(((HiddenField)Row.FindControl("MeetIDHiddenField")).Value);

            SessionsBLL SessionsAdapter = new SessionsBLL();
            SwimTeamDatabase.SessionsDataTable sessions = SessionsAdapter.GetSessionsByMeetID(MeetID);
            List<DateTime> MeetDates = new List<DateTime>();
            foreach (SwimTeamDatabase.SessionsRow session in sessions)
                if (!MeetDates.Contains(session.SessionDate))
                    MeetDates.Add(session.SessionDate);

            if (MeetDates.Count == 1)
                MeetDatesLabel.Text = MeetDates[0].Month + "/" + MeetDates[0].Day;
            else if (MeetDates.Count > 1)
            {
                DateTime startdate, enddate;
                if (MeetDates[0] < MeetDates[1])
                {
                    startdate = MeetDates[0];
                    enddate = MeetDates[1];
                }
                else
                {
                    startdate = MeetDates[1];
                    enddate = MeetDates[0];
                }
                for (int i = 2; i < MeetDates.Count; i++)
                {
                    if (MeetDates[i] < startdate)
                        startdate = MeetDates[i];
                    if (MeetDates[i] > enddate)
                        enddate = MeetDates[i];
                }

                MeetDatesLabel.Text = startdate.Month + "/" + startdate.Day + " - " +
                    enddate.Month + "/" + enddate.Day;
            }
        }
    }
}