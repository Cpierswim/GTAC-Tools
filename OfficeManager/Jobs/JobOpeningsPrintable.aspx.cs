using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class OfficeManager_Jobs_JobOpeningsPrintable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private int LastJobTypeID;
    private int JobTypeIDCurrentCount;
    private SwimTeamDatabase.JobTypesDataTable JobTypes;
    protected void BeginJobSessionsDataBind(object sender, EventArgs e)
    {
        JobTypesBLL JobTypesAdapter = new JobTypesBLL();
        JobTypes = JobTypesAdapter.GetAll();
        LastJobTypeID = -1;
        JobTypeIDCurrentCount = -1;
    }
    protected void JobSessionsDataBound(object sender, EventArgs e)
    {
        GridView GV = sender as GridView;
        Table GVTable = null;
        if (GV.Rows.Count > 0)
        {
            GVTable = GV.Rows[0].Parent as Table;
            TableRow LastSubHeaderRow = null;
            bool FirstSubHeaderFound = false;
            int TypeCount = 0, OpenCount = 0;
            for (int i = 0; i < GVTable.Rows.Count - 1; i++)
            {
                if (GVTable.Rows[i].FindControl("JobCountLabel") != null)
                {
                    if (!FirstSubHeaderFound)
                    {
                        FirstSubHeaderFound = true;
                        TypeCount = 0;
                        OpenCount = 0;
                        LastSubHeaderRow = GVTable.Rows[i];
                    }
                    else
                    {
                        Label JobCountLabel = LastSubHeaderRow.FindControl("JobCountLabel") as Label;
                        JobCountLabel.Text = "Count: " + TypeCount + " Open: " + OpenCount;
                        LastSubHeaderRow = GVTable.Rows[i];
                        TypeCount = 0;
                        OpenCount = 0;
                    }
                }
                else
                {
                    TypeCount++;
                    if (GVTable.Rows[i].Cells[1].Controls.Count >= 2)
                        if (GVTable.Rows[i].Cells[1].Controls[1].GetType() == typeof(Label))
                        {
                            Label SignUpLabel = GVTable.Rows[i].Cells[1].Controls[1] as Label;
                            if (SignUpLabel != null)
                                if (String.IsNullOrWhiteSpace(SignUpLabel.Text))
                                    OpenCount++;
                        }
                }
            }
            if (LastSubHeaderRow != null)
            {
                Label JobCountLabel = LastSubHeaderRow.FindControl("JobCountLabel") as Label;
                JobCountLabel.Text = "Count: " + TypeCount + " Open: " + OpenCount;
            }
        }
    }
    protected void JobSessionsRowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView SendingGrid = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView View = e.Row.DataItem as DataRowView;
            int CurrentJobTypeID = int.Parse(View["JobTypeID"].ToString());
            if (CurrentJobTypeID != LastJobTypeID)
            {
                LastJobTypeID = CurrentJobTypeID;
                JobTypeIDCurrentCount = 1;

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow SubHeader = new GridViewRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = SendingGrid.Columns.Count;
                    cell.Width = new Unit("100%");
                    cell.Style.Add("font-weight", "bold");
                    cell.Style.Add("background-color", "#9DB0C6");
                    cell.Style.Add("color", "white");
                    Label SubHeaderLabel = new Label();
                    SubHeaderLabel.ID = "JobHeaderLabel";
                    for (int i = 0; i < JobTypes.Count; i++)
                    {
                        if (JobTypes[i].JobTypeID == CurrentJobTypeID)
                        {

                            SubHeaderLabel.Text = JobTypes[i].Name;
                            break;
                        }
                    }
                    cell.Controls.Add(SubHeaderLabel);
                    Label JobCountLabel = new Label();
                    JobCountLabel.ID = "JobCountLabel";
                    JobCountLabel.Style.Add(HtmlTextWriterStyle.MarginLeft, "40px");
                    JobCountLabel.Style.Add("float", "right");
                    cell.Controls.Add(JobCountLabel);
                    SubHeader.Cells.Add(cell);
                    tbl.Rows.AddAt(tbl.Rows.Count - 1, SubHeader);
                }
            }
            else
            {
                JobTypeIDCurrentCount++;
            }

            Label SignUpLabel = e.Row.FindControl("SignUpLabel") as Label;
            if (View["FamilyID"].GetType() != typeof(System.DBNull))
            {
                int FamilyID;
                if (int.TryParse(View["FamilyID"].ToString(), out FamilyID))
                {
                    ParentsBLL ParentsAdapter = new ParentsBLL();
                    SwimTeamDatabase.ParentsDataTable Parents = ParentsAdapter.GetParentsByFamilyID(FamilyID);
                    if (Parents.Count > 0)
                    {
                        String LastName = Parents[0].LastName;
                        if (Parents.Count > 1)
                        {
                            if (Parents[0].LastName != Parents[1].LastName)
                                LastName = LastName + "/" + Parents[1].LastName;
                        }
                        SignUpLabel.Text = LastName + " Family";
                    }
                    else
                        SignUpLabel.Text = "Unknown Family Signed Up";
                }
                else
                {
                    SignUpLabel.Text = "Unknown Family Signed Up";
                }

                if (View["USAID"].GetType() != typeof(System.DBNull))
                {
                    SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                    SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmerByUSAID(View["USAID"].ToString());
                    if (Swimmers.Count > 0)
                    {
                        SignUpLabel.Text += ": " + Swimmers[0].PreferredName + " " + Swimmers[0].LastName;
                    }
                }
                else if (View["ParentID"].GetType() != typeof(System.DBNull))
                {
                    ParentsBLL ParentsAdapter = new ParentsBLL();
                    int ParentID;
                    if (int.TryParse(View["ParentID"].ToString(), out ParentID))
                    {
                        SwimTeamDatabase.ParentsDataTable Parents = ParentsAdapter.GetParentByID(ParentID);
                        if (Parents.Count == 1)
                        {
                            SignUpLabel.Text += ": " + Parents[0].FirstName + " " + Parents[0].LastName;
                        }
                    }
                }
                else if (View["Other"].GetType() != typeof(System.DBNull))
                {
                    SignUpLabel.Text += ": " + View["Other"].ToString();
                }
            }
            else
            {
                SignUpLabel.Text = "";
            }
        }
    }
}