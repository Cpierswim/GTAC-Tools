using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_MeetEntries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            object o = Request.QueryString["SEL"];
            if (o != null)
                DropDownList1.SelectedValue = o.ToString();
        }
    }

    protected void AdjustRows(object sender, EventArgs e)
    {
        GridView Grid = ((GridView)sender);
        bool DataRowFound = false;
        for(int i = 0; i < Grid.Rows.Count; i++)
            if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
            {
                DataRowFound = true;
                break;
            }
        if (DataRowFound)
        {
            int MeetID = int.Parse(DropDownList1.SelectedValue);

            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmers();
            SwimTeamDatabase.EntriesDataTable Entries = new EntryBLL().GetEntriesForMeet(MeetID);
            SwimTeamDatabase.SessionsDataTable Sessions = new SessionsBLL().GetSessionsByMeetID(MeetID);

            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                if (Grid.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    HiddenField USAIDHiddenField = ((HiddenField)Grid.Rows[i].FindControl("USAIDHiddenField"));
                    String USAID = USAIDHiddenField.Value;

                    SwimTeamDatabase.SwimmersRow Swimmer = Swimmers.NewSwimmersRow();
                    for (int j = 0; j < Swimmers.Count; j++)
                        if (Swimmers[j].USAID == USAID)
                        {
                            Swimmer = Swimmers[j];
                            j = Swimmers.Count;
                        }
                    Label NameLabel = ((Label)Grid.Rows[i].FindControl("NameLabel"));
                    NameLabel.Text = Swimmer.LastName + ", " + Swimmer.PreferredName;

                    List<SwimTeamDatabase.EntriesRow> SwimmerEntriesInMeet = new List<SwimTeamDatabase.EntriesRow>();
                    foreach (SwimTeamDatabase.EntriesRow entry in Entries)
                        if (entry.USAID == USAID)
                            SwimmerEntriesInMeet.Add(entry);

                    List<SwimTeamDatabase.SessionsRow> SessionsEntered = new List<SwimTeamDatabase.SessionsRow>();
                    foreach (SwimTeamDatabase.EntriesRow entry in SwimmerEntriesInMeet)
                    {
                        for (int j = 0; j < Sessions.Count; j++)
                            if (Sessions[j].SessionID == entry.SessionID)
                                SessionsEntered.Add(Sessions[j]);
                    }


                    TableCell SessionCell = Grid.Rows[i].Cells[1];
                    Table InnerTable = new Table();
                    InnerTable.Width = new Unit(100.0, UnitType.Percentage);
                    TableRow UpperRow = new TableRow();
                    for (int j = 0; j < Sessions.Count; j++)
                    {
                        TableCell UpperCell = new TableCell();
                        UpperCell.HorizontalAlign = HorizontalAlign.Center;
                        Label TempLabel = new Label();
                        TempLabel.Text = (j + 1).ToString();
                        UpperCell.Controls.Add(TempLabel);
                        UpperRow.Cells.Add(UpperCell);
                    }
                    InnerTable.Rows.Add(UpperRow);
                    TableRow LowerRow = new TableRow();
                    for (int j = 0; j < Sessions.Count; j++)
                    {
                        TableCell LowerCell = new TableCell();
                        LowerCell.HorizontalAlign = HorizontalAlign.Center;
                        SwimTeamDatabase.SessionsRow SessionOffered = Sessions[j];
                        CheckBox TempCheckBox = new CheckBox();
                        TempCheckBox.ID = "SessionCheckBox" + USAID + "Session" + i;
                        TempCheckBox.Enabled = false;
                        bool SessionEntered = false;
                        for (int k = 0; k < SessionsEntered.Count; k++)
                            if (SessionOffered.SessionID == SessionsEntered[k].SessionID)
                            {
                                SessionEntered = true;
                                k = SessionsEntered.Count;
                            }
                        if (SessionEntered)
                            TempCheckBox.Checked = true;

                        LowerCell.Controls.Add(TempCheckBox);
                        LowerRow.Cells.Add(LowerCell);
                    }
                    InnerTable.Rows.Add(LowerRow);
                    SessionCell.Controls.Add(InnerTable);


                    bool AllSessionsInDatabase = true;
                    for (int j = 0; j < SwimmerEntriesInMeet.Count; j++)
                        if (!SwimmerEntriesInMeet[j].InDatabase)
                        {
                            AllSessionsInDatabase = false;
                            j = SwimmerEntriesInMeet.Count;
                        }

                    TableCell InDatabaseCell = Grid.Rows[i].Cells[2];
                    InDatabaseCell.HorizontalAlign = HorizontalAlign.Center;
                    CheckBox InDatabaseCheckBox = new CheckBox();
                    InDatabaseCheckBox.Checked = AllSessionsInDatabase;
                    InDatabaseCheckBox.Enabled = false;
                    InDatabaseCell.Controls.Add(InDatabaseCheckBox);
                }
            }
        }
    }
    protected void RowButtonPressed(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Mark")
        {
            GridView Grid = ((GridView)sender);
            int RowID = int.Parse(e.CommandArgument.ToString());
            GridViewRow SelectedRow = Grid.Rows[RowID];

            HiddenField USAIDHiddenField = ((HiddenField)SelectedRow.FindControl("USAIDHiddenField"));
            String USAID = USAIDHiddenField.Value;

            EntryBLL EntriesAdapter = new EntryBLL();
            SwimTeamDatabase.EntriesDataTable Entries = EntriesAdapter.GetEntriesForSwimmer(USAID);

            int MeetID = int.Parse(DropDownList1.SelectedValue);
            foreach (SwimTeamDatabase.EntriesRow entry in Entries)
                if (entry.MeetID == MeetID)
                    EntriesAdapter.SetEntryAsInDatabase(entry.EntryID);

            Grid.DataBind();
        }
        if (e.CommandName == "Delete")
        {
            GridView Grid = ((GridView)sender);
            int rowID = int.Parse(e.CommandArgument.ToString());
            GridViewRow SelectedRow = Grid.Rows[rowID];

            HiddenField USAIDHiddenField = ((HiddenField)SelectedRow.FindControl("USAIDHiddenField"));
            String USAID = USAIDHiddenField.Value;

            String MeetID = DropDownList1.SelectedValue;

            USAIDEncryptor Encryptor = new USAIDEncryptor(USAID, USAIDEncryptor.EncryptionStatus.Unencrypted);

            String ResponseURL = "~/DatabaseManager/DeletePreEnter.aspx" + "?ID=" + Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Encrypted) + 
                "&MID=" + MeetID;

            Response.Redirect(ResponseURL, true);
        }
    }
}