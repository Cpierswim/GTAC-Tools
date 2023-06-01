using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

public partial class Admin_SDDFKSNDELETEALLINFODDKSLSDN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DeleteButton.Text = "Click here to delete ALL FAMILIES, SWIMMERS, and USER ACCOUNTS.";
            TripsHiddenField.Value = "1";
        }
    }
    protected void DeleteButtonclicked(object sender, EventArgs e)
    {
        if (TripsHiddenField.Value == "1")
        {
            DeleteButton.Text = "ARE YOU SURE YOU WANT TO RESET THE DATABASE?";
            TripsHiddenField.Value = "2";
        }
        else if (TripsHiddenField.Value == "2")
        {
            DeleteButton.Text = "LAST CHANCE. Clicking this button will delete EVERYTHING. ARE YOU SURE!?!?!?";
            TripsHiddenField.Value = "3";
        }
        else if (TripsHiddenField.Value == "3")
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["SwimSiteDevelopmentDatabaseConnectionString"].ConnectionString;
            
            SqlDataSource FamiliesDataSource = new SqlDataSource();
            FamiliesDataSource.ConnectionString = ConnectionString;
            FamiliesDataSource.DeleteCommand = "DELETE FROM Families WHERE (FamilyID <> - 1)";
            int FamiliesDeleted = FamiliesDataSource.Delete();

            SqlDataSource MessagesDataSource = new SqlDataSource();
            MessagesDataSource.ConnectionString = ConnectionString;
            MessagesDataSource.DeleteCommand = "DELETE FROM Messages WHERE (MessageID <> -1)";
            int MessagesDeleted = MessagesDataSource.Delete();

            SqlDataSource ParentsDataSource = new SqlDataSource();
            ParentsDataSource.ConnectionString = ConnectionString;
            ParentsDataSource.DeleteCommand = "DELETE FROM Parents WHERE (FamilyID <> -1)";
            int ParentsDeleted = ParentsDataSource.Delete();

            SqlDataSource SwimmersDataSource = new SqlDataSource();
            SwimmersDataSource.ConnectionString = ConnectionString;
            SwimmersDataSource.DeleteCommand = "DELETE FROM SWIMMERS WHERE (FamilyID <> -1)";
            int SwimmersDeleted = SwimmersDataSource.Delete();

            SqlDataSource BanquetSignUps = new SqlDataSource();
            BanquetSignUps.ConnectionString = ConnectionString;
            BanquetSignUps.DeleteCommand = "DELETE FROM BANQUETSIGNUPS WHERE (BanquentSignUpID <> -1)";
            int BanquetSignUpsDeleted = SwimmersDataSource.Delete();

            SqlDataSource EntriesDataSource = new SqlDataSource();
            EntriesDataSource.ConnectionString = ConnectionString;
            EntriesDataSource.DeleteCommand = "DELETE FROM Entries WHERE (EntryID <> -1)";
            int EntriesDeleted = SwimmersDataSource.Delete();

            SqlDataSource EventsDataSource = new SqlDataSource();
            EventsDataSource.ConnectionString = ConnectionString;
            EventsDataSource.DeleteCommand = "DELETE FROM Events WHERE (EventID <> -1)";
            int EventsDeleted = SwimmersDataSource.Delete();

            SqlDataSource GroupsDataSource = new SqlDataSource();
            GroupsDataSource.ConnectionString = ConnectionString;
            GroupsDataSource.DeleteCommand = "DELETE FROM GROUPS WHERE (GroupID <> -1)";
            int GroupsDeleted = SwimmersDataSource.Delete();

            SqlDataSource MeetCreditsDataSource = new SqlDataSource();
            MeetCreditsDataSource.ConnectionString = ConnectionString;
            MeetCreditsDataSource.DeleteCommand = "DELETE FROM MeetCredits WHERE (MeetCreditID <> -1)";
            int MeetCreditsDeleted = SwimmersDataSource.Delete();

            SqlDataSource MeetsDataSource = new SqlDataSource();
            MeetsDataSource.ConnectionString = ConnectionString;
            MeetsDataSource.DeleteCommand = "DELETE FROM Meets WHERE (MeetID <> -1)";
            int MeetsDeleted = SwimmersDataSource.Delete();

            SqlDataSource MeetsTwoPointOhDataSource = new SqlDataSource();
            MeetsTwoPointOhDataSource.ConnectionString = ConnectionString;
            MeetsTwoPointOhDataSource.DeleteCommand = "DELETE FROM MeetV2 WHERE (Meet <> -1)";
            int MeetsTwoPointOhDeleted = SwimmersDataSource.Delete();

            SqlDataSource ParentsGeocodeDataSource = new SqlDataSource();
            ParentsGeocodeDataSource.ConnectionString = ConnectionString;
            ParentsGeocodeDataSource.DeleteCommand = "DELETE FROM ParentsGeocodes WHERE (ParentID <> -1)";
            int ParentsGeocodesDeleted = SwimmersDataSource.Delete();

            SqlDataSource RecordsDataSource = new SqlDataSource();
            RecordsDataSource.ConnectionString = ConnectionString;
            RecordsDataSource.DeleteCommand = "DELETE FROM Records WHERE (RecordID <> -1)";
            int RecordsDeleted = SwimmersDataSource.Delete();

            SqlDataSource SessionsDataSource = new SqlDataSource();
            SessionsDataSource.ConnectionString = ConnectionString;
            SessionsDataSource.DeleteCommand = "DELETE FROM Sessions WHERE (SessionID <> -1)";
            int SessionsDeleted = SwimmersDataSource.Delete();

            SqlDataSource SwimmerAthleteJoinDataSource = new SqlDataSource();
            SwimmerAthleteJoinDataSource.ConnectionString = ConnectionString;
            SwimmerAthleteJoinDataSource.DeleteCommand = "DELETE FROM SwimmerAthleteJoin WHERE (HyTekAthleteID <> -1)";
            int SwimmerAthelteJoinsDeleted = SwimmersDataSource.Delete();

            SqlDataSource ChangesDataSource = new SqlDataSource();
            ChangesDataSource.ConnectionString = ConnectionString;
            ChangesDataSource.DeleteCommand = "DELETE FROM Changes WHERE (ChangeID <> -40)";
            int ChangesDelete  = ChangesDataSource.Delete();

            DeleteButton.Visible = false;
            DeletedLabel.Visible = true;

            DeletedLabel.Text = "";

            if (FamiliesDeleted != 1)
                DeletedLabel.Text += FamiliesDeleted + " families deleted.";
            else
                DeletedLabel.Text += FamiliesDeleted + " family deleted.";

            if (MessagesDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + MessagesDeleted + " messages deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + MessagesDeleted + " message deleted.";

            if (ParentsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + ParentsDeleted + " parents deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + ParentsDeleted + " parent deleted.";

            if (SwimmersDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + SwimmersDeleted + " swimmers deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + SwimmersDeleted + " swimmer deleted.";

            if (BanquetSignUpsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + BanquetSignUpsDeleted + " banquet sign ups deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + BanquetSignUpsDeleted + " banquet sign up deleted.";

            if (EntriesDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + EntriesDeleted + " entries deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + EntriesDeleted + " entry deleted.";

            if (EventsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + EventsDeleted + " events deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + EventsDeleted + " event deleted.";

            if (GroupsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + GroupsDeleted + " groups deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + GroupsDeleted + " group deleted.";

            if (MeetCreditsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + MeetCreditsDeleted + " meet credit accounts deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + MeetCreditsDeleted + " meet credit account deleted.";

            if (MeetsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + MeetsDeleted + " meets deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + MeetsDeleted + " meet deleted.";

            if (MeetsTwoPointOhDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + MeetsTwoPointOhDeleted + " meets version 2.0 deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + MeetsTwoPointOhDeleted + " meet version 2.0 deleted.";

            if (ParentsGeocodesDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + ParentsGeocodesDeleted + " parents geocodes deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + ParentsGeocodesDeleted + " parent geocode deleted.";

            if (RecordsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + RecordsDeleted + " records deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + RecordsDeleted + " record deleted.";

            if (SessionsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + SessionsDeleted + " sessions deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + SessionsDeleted + " session deleted.";

            if (SwimmerAthelteJoinsDeleted != 1)
                DeletedLabel.Text += "<br /><br />" + SwimmerAthelteJoinsDeleted + " swimmer/athlete joins deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + SwimmerAthelteJoinsDeleted + " swimmer/athlete join deleted.";

            if (ChangesDelete != 1)
                DeletedLabel.Text += "<br /><br />" + ChangesDelete + " changes deleted.";
            else
                DeletedLabel.Text += "<br /><br />" + ChangesDelete + " change deleted.";

            DeletedLabel.Text += "<br />";

            MembershipUserCollection Members = Membership.GetAllUsers();

            int workingon = 0;
            foreach (MembershipUser Member in Members)
            {
                workingon++;
                DeletedLabel.Text += "<br />Deleting Member" + workingon + ": " + Member.UserName;
                if (Roles.IsUserInRole(Member.UserName, "Parent"))
                {
                    Membership.DeleteUser(Member.UserName);
                    DeletedLabel.Text += "......Deleted";
                }
                else
                {
                    DeletedLabel.Text += "......User NOT DELETED. Not a parent Account. Must delete manually.";
                }
                
            }
        }
    }
}