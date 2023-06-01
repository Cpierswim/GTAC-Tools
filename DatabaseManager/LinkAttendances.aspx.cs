using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_LinkAttendances : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadDropDownLists();

        }
    }

    private const String SearchString = "SYSTEMGENERATED-NAME: ";

    private void LoadDropDownLists()
    {
        SwimmersDropDownList.Items.Clear();
        UnlinkedDropDownList.Items.Clear();

        SwimmersDropDownList.Items.Add(new ListItem(" ", "NONE"));
        UnlinkedDropDownList.Items.Add(new ListItem(" ", "NONE"));

        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmers();
        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
            SwimmersDropDownList.Items.Add(
                new ListItem(Swimmer.LastName + ", " + Swimmer.PreferredName, Swimmer.USAID));
        SwimTeamDatabaseTableAdapters.AttendanceAdditionalSwimmersHelperTableAdapter AttendanceHelperAdatper =
            new SwimTeamDatabaseTableAdapters.AttendanceAdditionalSwimmersHelperTableAdapter();
        SwimTeamDatabase.AttendanceAdditionalSwimmersHelperDataTable AttendanceHelperTable =
            AttendanceHelperAdatper.GetSwimmersWithSystemGeneratedNotes();
        foreach (SwimTeamDatabase.AttendanceAdditionalSwimmersHelperRow Row in AttendanceHelperTable)
        {
            
            if (Row.Note.Contains(SearchString))
            {
                String NameString = Row.Note.Substring(Row.Note.IndexOf(SearchString) + SearchString.Length);
                char[] seperator = new char[1];
                seperator[0] = '$';
                String[] FirstLast = NameString.Split(seperator);

                UnlinkedDropDownList.Items.Add(new ListItem(FirstLast[1] + ", " + FirstLast[0], Row.USAID));
            }
        }
    }
    protected void LinkNames(object sender, EventArgs e)
    {
        if (SwimmersDropDownList.SelectedValue != "NONE" &&
            UnlinkedDropDownList.SelectedValue != "NONE")
        {
            AttendanceBLL AttendanceAdapter = new AttendanceBLL();

            int rowsAffected = AttendanceAdapter.UpdateUSAID(UnlinkedDropDownList.SelectedValue, SwimmersDropDownList.SelectedValue);

            RowsAffectedLabel.Text = rowsAffected + " rows changed.";

            SwimTeamDatabase.AttendanceDataTable Attendances = AttendanceAdapter.GetAttendancesByUSAID(UnlinkedDropDownList.SelectedValue);

            foreach (SwimTeamDatabase.AttendanceRow Attendance in Attendances)
            {
                if (!Attendance.IsNoteNull())
                {
                    if (Attendance.Note.Contains(SearchString))
                    {
                        String CoachNotes = Attendance.Note.Substring(0, Attendance.Note.IndexOf(SearchString));
                        Attendance.Note = CoachNotes;
                    }
                }
            }

            AttendanceAdapter.BatchUpdate(Attendances);

            LoadDropDownLists();
        }
    }
}