using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_UnregisteredSwimmers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CreateTable();
    }

    private void CreateTable()
    {
        this.CreateHeaderRow();

        AttendanceBLL AttendanceAdapter = new AttendanceBLL();

        SwimTeamDatabase.AttendanceDataTable Attendances = AttendanceAdapter.GetUnregisteredAttendances();
        if (Attendances.Count > 0)
        {

            List<SwimTeamDatabase.AttendanceRow> SwimmersAttendances = new List<SwimTeamDatabase.AttendanceRow>();

            String SwimmerWorkingOnUSAID = Attendances[0].USAID;
            SwimmersAttendances.Add(Attendances[0]);

            for (int i = 1; i < Attendances.Count; i++)
            {
                if (Attendances[i].USAID == SwimmerWorkingOnUSAID)
                    SwimmersAttendances.Add(Attendances[i]);
                else
                {
                    this.CreateRowForSwimmerFromAttendances(SwimmersAttendances);
                    SwimmerWorkingOnUSAID = Attendances[i].USAID;
                    SwimmersAttendances.Clear();
                    SwimmersAttendances.Add(Attendances[i]);
                }
            }

            if (SwimmersAttendances.Count != 0)
                this.CreateRowForSwimmerFromAttendances(SwimmersAttendances);
        }

        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable UnapprovedSwimmers = SwimmersAdapter.GetSwimmersNotReadyToAdd();
        foreach (SwimTeamDatabase.SwimmersRow Swimmer in UnapprovedSwimmers)
        {
            SwimTeamDatabase.AttendanceDataTable AttendancesTable = AttendanceAdapter.GetAttendancesByUSAID(Swimmer.USAID);

            List<SwimTeamDatabase.AttendanceRow> SwimmersAttendances = new List<SwimTeamDatabase.AttendanceRow>();
            for (int i = 0; i < AttendancesTable.Count; i++)
                SwimmersAttendances.Add(AttendancesTable[i]);

            this.CreateRowFromSwimmer(Swimmer, SwimmersAttendances);
        }
    }

    private void CreateHeaderRow()
    {
        TableRow HeaderRow = new TableRow();
        HeaderRow.ID = "HeaderRow";
        TableCell HeaderCell1 = new TableCell();
        HeaderCell1.ID = "HeaderCell1";
        Label HeaderLabel = new Label();
        HeaderLabel.ID = "HeaderLabel";
        HeaderLabel.Text = "Name";

        HeaderCell1.Controls.Add(HeaderLabel);

        TableCell HeaderCell2 = new TableCell();
        HeaderCell2.ID = "HeaderCell2";
        Label StartDateLabel = new Label();
        StartDateLabel.ID = "StartDateLabel";
        StartDateLabel.Text = "Start Date";

        HeaderCell2.Controls.Add(StartDateLabel);

        TableCell HeaderCell3 = new TableCell();
        HeaderCell3.ID = "HeaderCell3";
        Label TotalPracticesLabel = new Label();
        TotalPracticesLabel.ID = "TotalPracticesLabel";
        TotalPracticesLabel.Text = "Total Practices";

        HeaderCell3.Controls.Add(TotalPracticesLabel);

        HeaderRow.Cells.Add(HeaderCell1);
        HeaderRow.Cells.Add(HeaderCell2);
        HeaderRow.Cells.Add(HeaderCell3);

        this.Table1.Rows.Add(HeaderRow);
    }
    private void CreateRowForSwimmerFromAttendances(List<SwimTeamDatabase.AttendanceRow> Attendances)
    {
        if (Attendances.Count > 0)
        {
            String SearchString = "SYSTEMGENERATED-NAME: ";
            String Name = Attendances[0].Note.Substring(Attendances[0].Note.IndexOf(SearchString) + SearchString.Length);
            Name = Name.Replace('$', ' ');

            DateTime StartDate = Attendances[0].Date;
            for (int i = 0; i < Attendances.Count; i++)
                if (Attendances[i].Date < StartDate)
                    StartDate = Attendances[i].Date;

            int TotalPractices = Attendances.Count;

            TableRow SwimmerRow = new TableRow();
            SwimmerRow.ID = Attendances[0].USAID + "Row";

            TableCell NameCell = new TableCell();
            NameCell.ID = Attendances[0].USAID + "NameCell";

            Label NameLabel = new Label();
            NameLabel.ID = Attendances[0].USAID + "NameLabel";
            NameLabel.Text = Name;

            NameCell.Controls.Add(NameLabel);

            TableCell StartDateCell = new TableCell();
            StartDateCell.ID = Attendances[0].USAID + "StartDateCell";

            Label StartDateLabel = new Label();
            StartDateLabel.ID = Attendances[0].USAID + "StartDateLabel";
            StartDateLabel.Text = StartDate.Month + "/" + StartDate.Day + "/" + StartDate.Year;

            StartDateCell.Controls.Add(StartDateLabel);

            TableCell TotalPracticesCell = new TableCell();
            TotalPracticesCell.ID = Attendances[0].USAID + "TotalPracticesCell";

            Label TotalPracticesLabel = new Label();
            TotalPracticesLabel.ID = Attendances[0].USAID + "TotalPracticesLabel";
            TotalPracticesLabel.Text = TotalPractices.ToString();

            TotalPracticesCell.Controls.Add(TotalPracticesLabel);

            SwimmerRow.Cells.Add(NameCell);
            SwimmerRow.Cells.Add(StartDateCell);
            SwimmerRow.Cells.Add(TotalPracticesCell);

            this.Table1.Rows.Add(SwimmerRow);
        }
    }
    private void CreateRowFromSwimmer(SwimTeamDatabase.SwimmersRow Swimmer, List<SwimTeamDatabase.AttendanceRow> Attendances)
    {
        if (Attendances.Count > 0)
        {
            String Name = Swimmer.PreferredName + " " + Swimmer.LastName;

            DateTime StartDate = Attendances[0].Date;
            for (int i = 0; i < Attendances.Count; i++)
                if (Attendances[i].Date < StartDate)
                    StartDate = Attendances[i].Date;
            int TotalPractices = Attendances.Count;

            TableRow SwimmerRow = new TableRow();
            SwimmerRow.ID = Attendances[0].USAID + "Row";

            TableCell NameCell = new TableCell();
            NameCell.ID = Attendances[0].USAID + "NameCell";

            Label NameLabel = new Label();
            NameLabel.ID = Attendances[0].USAID + "NameLabel";
            NameLabel.Text = Name;

            NameCell.Controls.Add(NameLabel);

            TableCell StartDateCell = new TableCell();
            StartDateCell.ID = Attendances[0].USAID + "StartDateCell";

            Label StartDateLabel = new Label();
            StartDateLabel.ID = Attendances[0].USAID + "StartDateLabel";
            StartDateLabel.Text = StartDate.Month + "/" + StartDate.Day + "/" + StartDate.Year;

            StartDateCell.Controls.Add(StartDateLabel);

            TableCell TotalPracticesCell = new TableCell();
            TotalPracticesCell.ID = Attendances[0].USAID + "TotalPracticesCell";

            Label TotalPracticesLabel = new Label();
            TotalPracticesLabel.ID = Attendances[0].USAID + "TotalPracticesLabel";
            TotalPracticesLabel.Text = TotalPractices.ToString();

            TotalPracticesCell.Controls.Add(TotalPracticesLabel);

            SwimmerRow.Cells.Add(NameCell);
            SwimmerRow.Cells.Add(StartDateCell);
            SwimmerRow.Cells.Add(TotalPracticesCell);

            this.Table1.Rows.Add(SwimmerRow);
        }
    }

}