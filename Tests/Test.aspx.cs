using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class Test : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        //AttendanceBLL Test = new AttendanceBLL();
        //SwimTeamDatabase.AttendanceDataTable GoldAttendances = Test.GetAttendancesByGroupID(-2);

        //foreach (SwimTeamDatabase.AttendanceRow Row in GoldAttendances)
        //{
        //    Row.GroupID = 7;
        //}
        //int Count = GoldAttendances.Count;
        //GoldAttendances.AddAttendanceRow(-2, "TESTAGAIN", DateTime.Now, 1, -7, "X", "", 0, 0, 0);
        //GoldAttendances[Count].Delete();



        //DateTime FirstStartTime = DateTime.Now;
        //Test.BatchUpdate(GoldAttendances);
        //DateTime FirstEndTime = DateTime.Now;
        //TimeSpan FirstTimeTaken = FirstStartTime - FirstEndTime;

        //i++;

        //AttendanceBLL SecondTest = new AttendanceBLL();
        //SwimTeamDatabase.AttendanceDataTable ChangeBackAttendances = SecondTest.GetAttendancesByGroupID(-2);
        //foreach (SwimTeamDatabase.AttendanceRow Row in ChangeBackAttendances)
        //    Row.GroupID = 7;


        //DateTime SecondStartTime = DateTime.Now;
        //SecondTest.TestBatchUpdate(ChangeBackAttendances);
        //DateTime SecondEndTime = DateTime.Now;
        //TimeSpan SecondTimeTaken = SecondStartTime - SecondEndTime;

        //i++;

        //AttendanceBLL AnotherTest = new AttendanceBLL();
        //AnotherTest.BeginBatchInsert();

        //for (int i = 0; i < 2; i++)
        //    AnotherTest.BatchInsert("SL", System.DateTime.Now, 9, -33, "S", null, null, null, null);
        //DateTime ThirdStartTime = DateTime.Now;
        //AnotherTest.CommitBatchInsert();
        //DateTime ThirdEndTime = DateTime.Now;
        //TimeSpan ThirdTimeTaken = ThirdStartTime - ThirdEndTime;
        //AnotherTest.EndBatchInsert();

        //SwimTeamDatabaseTableAdapters.AttendanceTableAdapter TestAdapter = new SwimTeamDatabaseTableAdapters.AttendanceTableAdapter();
        //SwimTeamDatabase.AttendanceDataTable Testtable = TestAdapter.GetAttendancesByGroupID(7);

        //TestAdapter.SetBatchSize(Testtable.Count);

        //DateTime FourthStartTime = DateTime.Now;

        //TestAdapter.PrePareForBatchProcessing();
        //for (int j = 0; j < Testtable.Count; j++)
        //{
        //    int? Lane, Yards, Meters;
        //    if (Testtable[j].IsLaneNull())
        //        Lane = null;
        //    else
        //        Lane = Testtable[j].Lane;
        //    if (Testtable[j].IsYardsNull())
        //        Yards = null;
        //    else
        //        Yards = Testtable[j].Yards;
        //    if (Testtable[j].IsMetersNull())
        //        Meters = null;
        //    else
        //        Meters = Testtable[j].Meters;
        //    if (!Testtable[j].IsNoteNull())
        //        TestAdapter.Update(Testtable[j].USAID, Testtable[j].Date, Testtable[j].PracticeoftheDay, -8,
        //            Testtable[j].AttendanceType, Testtable[j].Note, Lane, Yards,
        //            Meters, Testtable[j].AttendanceID);
        //    else
        //        TestAdapter.Update(Testtable[j].USAID, Testtable[j].Date, Testtable[j].PracticeoftheDay, -8,
        //            Testtable[j].AttendanceType, null, Lane, Yards,
        //            Meters, Testtable[j].AttendanceID);
        //}
        //TestAdapter.EndBatchProcess();
        //DateTime FourthEndTime = DateTime.Now;
        //TimeSpan FourthTimeTaken = FourthStartTime - FourthEndTime;

        //SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        //SwimmersAdapter.UpdateGroup("121900REGCBOHM", 9);
        //SwimTeamDatabase.SwimmersDataTable swimmers = SwimmersAdapter.GetSwimmerByUSAID("091297ERIZZHU*");
        //foreach (SwimTeamDatabase.SwimmersRow Row in swimmers)
        //{
        //    String PhoneNumber = null, Email = null, Notes = null,  Disability = null;
        //    if (!Row.IsPhoneNumberNull())
        //        PhoneNumber = Row.PhoneNumber;
        //    if (!Row.IsEmailNull())
        //        Email = Row.Email;
        //    if (!Row.IsNotesNull())
        //        Notes = Row.Notes;
        //    if (!Row.IsDisabilityNull())
        //        Disability = Row.Disability;
        //    SwimmersAdapter.UpdateSwimmer(9, Row.LastName, Row.MiddleName, Row.FirstName, Row.PreferredName,
        //        Row.Birthday, Row.Gender, PhoneNumber, Email, Notes, Row.Inactive, Row.Ethnicity,
        //        Row.USCitizen, Disability, Row.USAID);
        //}
        if (!this.IsPostBack)
            this.TextBox1.Attributes.Add("onblur", "CheckTime(this)");
    }
}