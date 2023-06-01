using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_Meet_PreEntered : System.Web.UI.Page
{
    private SwimTeamDatabase.SwimmersDataTable Swimmers;
    private List<int> SessionNumbers;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DropDownList1.Items.Count > 0)
            if (DropDownList1.SelectedItem != null)
                this.Button1.Text = "Enter Swimmer(s) in " + DropDownList1.SelectedItem.Text;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField USAIDHiddenField = ((HiddenField)e.Row.FindControl("USAIDHiddenField"));
            Label NameLabel = ((Label)e.Row.FindControl("NameLabel"));
            String USAID = USAIDHiddenField.Value;
            for (int i = 0; i < Swimmers.Count; i++)
            {
                if (Swimmers[i].USAID == USAID)
                {
                    NameLabel.Text = Swimmers[i].LastName + ", " + Swimmers[i].FirstName;
                    break;
                }
            }

            Table SessionDisplayTable = ((Table)e.Row.FindControl("SessionDisplayTable"));

            CreateSessionTable(e.Row, SessionDisplayTable);

            CheckBox InDatabaseCheckBox = ((CheckBox)e.Row.FindControl("InDatabaseCheckBox"));

            InDatabaseCheckBox.Visible = InDatabaseCheckBox.Checked;
        }
    }
    protected void GridViewDataBinding(object sender, EventArgs e)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        this.Swimmers = SwimmersAdapter.GetSwimmers();
    }

    protected void DataSelected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        SwimTeamDatabase.PreEnteredV2DataTable PreEnteredTable = ((SwimTeamDatabase.PreEnteredV2DataTable)e.ReturnValue);
        if (PreEnteredTable.Count > 0)
        {
            int MeetID = PreEnteredTable[0].MeetID;

            SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
            SwimTeamDatabase.SessionV2DataTable Sessions = SessionsAdapter.GetSessionsByMeetID(MeetID);

            this.SessionNumbers = new List<int>();
            //for (int i = 0; i < PreEnteredTable.Count; i++)
            //{
            //    if (PreEnteredTable[i].MaxSessionNumberPreEnteredIn != null)
            //        if (!SessionNumbers.Contains(PreEnteredTable[i].MaxSessionNumberPreEnteredIn.Value))
            //            SessionNumbers.Add(PreEnteredTable[i].MaxSessionNumberPreEnteredIn.Value);
            //}
            for (int i = 0; i < Sessions.Count; i++)
            {
                SessionNumbers.Add(Sessions[i].Session);
            }
        }
    }

    private void CreateSessionTable(GridViewRow Row, Table SeedTable)
    {
        if (IsNotAttending(Row))
            SetRowAsNotAttending(SeedTable);
        else if (IsGeneralPreEntry(Row))
            SetRowAsGeneralEntryTable(SeedTable);
        else
            CreateIndividualSessionEntryTable(Row, SeedTable);
    }

    private static bool EnteredInSession(int SessionID, GridViewRow row)
    {
        HiddenField Session1HiddenField = ((HiddenField)row.FindControl("Session1HiddenField"));
        if (Session1HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session2HiddenField = ((HiddenField)row.FindControl("Session2HiddenField"));
        if (Session2HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session3HiddenField = ((HiddenField)row.FindControl("Session3HiddenField"));
        if (Session3HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session4HiddenField = ((HiddenField)row.FindControl("Session4HiddenField"));
        if (Session4HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session5HiddenField = ((HiddenField)row.FindControl("Session5HiddenField"));
        if (Session5HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session6HiddenField = ((HiddenField)row.FindControl("Session6HiddenField"));
        if (Session6HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session7HiddenField = ((HiddenField)row.FindControl("Session7HiddenField"));
        if (Session7HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session8HiddenField = ((HiddenField)row.FindControl("Session8HiddenField"));
        if (Session8HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session9HiddenField = ((HiddenField)row.FindControl("Session9HiddenField"));
        if (Session9HiddenField.Value == SessionID.ToString())
            return true;
        HiddenField Session10HiddenField = ((HiddenField)row.FindControl("Session10HiddenField"));
        if (Session10HiddenField.Value == SessionID.ToString())
            return true;

        return false;
    }
    private static bool IsGeneralPreEntry(GridViewRow row)
    {
        HiddenField Session1HiddenField = ((HiddenField)row.FindControl("Session1HiddenField"));
        HiddenField Session2HiddenField = ((HiddenField)row.FindControl("Session2HiddenField"));
        HiddenField Session3HiddenField = ((HiddenField)row.FindControl("Session3HiddenField"));
        HiddenField Session4HiddenField = ((HiddenField)row.FindControl("Session4HiddenField"));
        HiddenField Session5HiddenField = ((HiddenField)row.FindControl("Session5HiddenField"));
        HiddenField Session6HiddenField = ((HiddenField)row.FindControl("Session6HiddenField"));
        HiddenField Session7HiddenField = ((HiddenField)row.FindControl("Session7HiddenField"));
        HiddenField Session8HiddenField = ((HiddenField)row.FindControl("Session8HiddenField"));
        HiddenField Session9HiddenField = ((HiddenField)row.FindControl("Session9HiddenField"));
        HiddenField Session10HiddenField = ((HiddenField)row.FindControl("Session10HiddenField"));
        HiddenField PreEnteredHiddenField = ((HiddenField)row.FindControl("PreEnteredHiddenField"));

        return (String.IsNullOrWhiteSpace(Session1HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session2HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session3HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session4HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session5HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session6HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session7HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session8HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session9HiddenField.Value) &&
            String.IsNullOrWhiteSpace(Session10HiddenField.Value) &&
            bool.Parse(PreEnteredHiddenField.Value));
    }
    private static bool IsNotAttending(GridViewRow row)
    {
        HiddenField NotAttendingHiddenField = ((HiddenField)row.FindControl("NotAttendingHiddenField"));
        return bool.Parse(NotAttendingHiddenField.Value);
    }

    private static void SetRowAsGeneralEntryTable(Table SeedTable)
    {
        Label GeneralLabel = new Label();
        GeneralLabel.ID = "GeneralLabel";
        GeneralLabel.Text = "All Sessions";
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Controls.Add(GeneralLabel);
        tr.Cells.Add(tc);
        SeedTable.Rows.Add(tr);
    }
    private static void SetRowAsNotAttending(Table SeedTable)
    {
        Label GeneralLabel = new Label();
        GeneralLabel.ID = "NotAttendingLabel";
        GeneralLabel.Text = "Not Attending";
        GeneralLabel.Style.Add("font-weight", "bold");
        GeneralLabel.Style.Add("color", "red");
        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Controls.Add(GeneralLabel);
        tr.Cells.Add(tc);
        SeedTable.Rows.Add(tr);
    }
    private void CreateIndividualSessionEntryTable(GridViewRow Row, Table SeedTable)
    {
        TableRow headerrow = CreateHeaderRow();
        SeedTable.Rows.Add(headerrow);
        TableRow Checkboxrow = CreateCheckBoxRow(Row);
        SeedTable.Rows.Add(Checkboxrow);
    }
    private TableRow CreateHeaderRow()
    {
        TableRow Row = new TableRow();
        for (int i = 0; i < this.SessionNumbers.Count; i++)
        {
            Label l = new Label();
            l.ID = "Session" + this.SessionNumbers[i] + "Label";
            l.Text = SessionNumbers[i].ToString();

            TableCell c = new TableCell();
            c.Controls.Add(l);
            c.Style.Add("text-align", "center");

            Row.Cells.Add(c);
        }

        return Row;
    }
    private TableRow CreateCheckBoxRow(GridViewRow Row)
    {
        TableRow row = new TableRow();

        for (int i = 0; i < SessionNumbers.Count; i++)
        {
            CheckBox Box = new CheckBox();
            Box.ID = "Session" + SessionNumbers[i] + "CheckBox";
            Box.Enabled = false;
            Box.Checked = EnteredInSession(SessionNumbers[i], Row);

            TableCell c = new TableCell();
            c.Controls.Add(Box);
            c.Style.Add("text-align", "center");

            row.Cells.Add(c);
        }

        return row;
    }
    protected void ButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "InDatabase")
        {
            int PreEntryID = int.Parse(((HiddenField)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("PreEntryIDHiddenField")).Value);

            PreEnteredV2BLL PreEntryAdapter = new PreEnteredV2BLL();
            PreEntryAdapter.SetAsInDatabase(PreEntryID);

            GridView1.DataBind();
        }
        else if (e.CommandName == "Remove")
        {
            CheckBox InDatabaseCheckBox = ((CheckBox)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("InDatabaseCheckBox"));
            Table SessionDisplayTable = ((Table)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SessionDisplayTable"));

            if (!bool.Parse(((HiddenField)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("NotAttendingHiddenField")).Value))
            {
                    String QueryString = "";

                    QueryString = "?MeetID=" + this.DropDownList1.SelectedValue + "&USAID=" +
                        ((HiddenField)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("USAIDHiddenField")).Value;

                    Response.Redirect("~/DatabaseManager/Meet/DeletePreEnter.aspx" + QueryString, true);
                
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataBind();
            }
        }
    }
    protected void MeetsDropDownListDataBound(object sender, EventArgs e)
    {
        if (Request["MeetID"] != null)
        {
            String MeetID = Request["MeetID"];
            for(int i =0; i < this.DropDownList1.Items.Count; i++)
                if (this.DropDownList1.Items[i].Value == MeetID)
                {
                    this.DropDownList1.Items[i].Selected = true;
                    break;
                }
        }

        if (DropDownList1.Items.Count > 0)
            if (DropDownList1.SelectedItem != null)
                this.Button1.Text = "Enter Swimmer(s) in " + DropDownList1.SelectedItem.Text;
    }
    protected void GridViewDataBound(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count > 0)
        {
            int EntryCount = 0;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
                if (this.GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                    EntryCount++;
        }
        this.EntryCountLabel.Text = this.GridView1.Rows.Count.ToString();
    }
    protected void EnterSwimmerInMeetClicked(object sender, EventArgs e)
    {
        if(DropDownList1.Items.Count > 0)
            if (DropDownList1.SelectedItem != null)
            {
                String MeetID = this.DropDownList1.SelectedItem.Value;
                Response.Redirect("~/DatabaseManager/Meet/AddPreEnterPickSwimmer.aspx?MeetID=" + MeetID);
            }
    }
}