using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class OfficeManager_Jobs_JobOpenings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.PopulateDropDownList();
            this.JobEventsGridView.DataBind();
        }

    }
    private void PopulateDropDownList()
    {
        this.JobEventsDropDownList.Items.Clear();
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        foreach (SwimTeamDatabase.JobEventsRow JobEvent in JobEvents)
        {
            if (!JobEvent.IsMeetIDNull())
            {
                foreach (SwimTeamDatabase.MeetsV2Row Meet in Meets)
                {
                    if (Meet.Meet == JobEvent.MeetID)
                    {
                        ListItem Item = new ListItem();
                        Item.Value = JobEvent.JobEventID.ToString();
                        Item.Text = Meet.MeetName + " - Session " + JobEvent.MeetSessionID;
                        this.JobEventsDropDownList.Items.Add(Item);
                    }
                }
            }
            else if (!JobEvent.IsOtherEventNameNull())
            {
                ListItem Item = new ListItem();
                Item.Value = JobEvent.JobEventID.ToString();
                Item.Text = JobEvent.OtherEventName;
                this.JobEventsDropDownList.Items.Add(Item);
            }
        }

        if (JobEventsDropDownList.Items.Count == 0)
        {
            this.JobEventsDetailPanel.Visible = false;
            this.JobEventsDropDownList.Enabled = false;
            this.DeleteEventButton.Visible = false;
            ListItem Item = new ListItem();
            Item.Value = "-1";
            Item.Text = "No Job Events";
            this.JobEventsDropDownList.Items.Add(Item);
            this.AddJobOpeningButton.Visible = false;
            this.ViewPrintableButton.Visible = false;
        }
        else
        {
            this.DeleteEventButton.Visible = true;
            this.JobEventsDropDownList.Enabled = true;
            this.JobEventsDetailPanel.Visible = true;
            this.AddJobOpeningButton.Visible = true;
            this.ViewPrintableButton.Visible = true;
        }
    }
    protected void DeleteEvent(object sender, EventArgs e)
    {
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        int JobEventID;
        if (int.TryParse(this.JobEventsDropDownList.SelectedValue, out JobEventID))
        {
            JobEventsAdapter.Delete(JobEventID);
            this.PopulateDropDownList();
            this.JobEventsGridView.DataBind();
            this.AddEventPanel.Visible = false;
            this.JobEventsDetailPanel.Visible = true;
        }
    }


    private static String OtherListItemValue = "ONM";
    private static String OtherListItemText = "Other - Non-Meet Event";
    protected void PrepareToAddEvent(object sender, EventArgs e)
    {
        this.AddEventPanel.Visible = true;
        this.JobEventsDetailPanel.Visible = false;
        this.HeaderPanel.Visible = false;
        this.ViewPrintableButton.Visible = false;

        this.MeetsDropDownList.Items.Clear();
        this.MeetsDropDownList.Items.Add(new ListItem(""));

        ListItem OtherListItem = new ListItem(OtherListItemText, OtherListItemValue);
        this.MeetsDropDownList.Items.Add(OtherListItem);

        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        foreach (SwimTeamDatabase.MeetsV2Row Meet in Meets)
        {
            ListItem Item = new ListItem();
            Item.Value = Meet.Meet.ToString();
            Item.Text = Meet.MeetName;
            this.MeetsDropDownList.Items.Add(Item);
        }
        this.PopulateSessionsDropDownList();

    }
    private bool PopulateSessionsDropDownList()
    {
        this.SessionsDropDownList.Items.Clear();
        int MeetID;
        if (int.TryParse(this.MeetsDropDownList.SelectedValue, out MeetID))
        {
            SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
            SwimTeamDatabase.SessionV2DataTable Sessions = SessionsAdapter.GetSessionsByMeetID(MeetID);
            for (int i = 0; i < Sessions.Count; i++)
            {
                ListItem Item = new ListItem();
                Item.Value = Sessions[i].Session.ToString();
                Item.Text = Sessions[i].Session.ToString();
                this.SessionsDropDownList.Items.Add(Item);
            }
            this.OtherTextBox.Visible = false;
            this.OtherLabel.Visible = false;
            this.SessionsDropDownList.Visible = true;
            this.CreateNewEventButton.Visible = true;
            this.SessionLabel.Visible = true;
            this.NotesTextBox.Visible = true;
            this.NotesLabel.Visible = true;
            this.ViewPrintableButton.Visible = true;
            return true;
        }
        if (this.MeetsDropDownList.SelectedValue == OtherListItemValue)
        {
            this.OtherTextBox.Visible = true;
            this.OtherLabel.Visible = true;
            this.SessionsDropDownList.Visible = false;
            this.SessionLabel.Visible = false;
            this.CreateNewEventButton.Visible = true;
            this.NotesLabel.Visible = true;
            this.NotesTextBox.Visible = true;
            this.ViewPrintableButton.Visible = true;
        }
        else
        {
            this.OtherTextBox.Visible = false;
            this.OtherLabel.Visible = false;
            this.NotesLabel.Visible = false;
            this.NotesTextBox.Visible = false;
            this.SessionsDropDownList.Visible = false;
            this.SessionLabel.Visible = false;
            this.CreateNewEventButton.Visible = false;
            this.ViewPrintableButton.Visible = false;
        }
        return false;
    }
    protected void MeetSelectionChanged(object sender, EventArgs e)
    {
        this.PopulateSessionsDropDownList();
    }
    protected void CancelCreateJobEvent(object sender, EventArgs e)
    {
        this.AddEventPanel.Visible = false;
        this.JobEventsDetailPanel.Visible = true;
        this.HeaderPanel.Visible = true;
        this.ViewPrintableButton.Visible = true;
    }
    protected void CreateJobEvent(object sender, EventArgs e)
    {
        this.AddEventPanel.Visible = false;
        this.JobEventsDetailPanel.Visible = true;
        this.HeaderPanel.Visible = true;
        this.ViewPrintableButton.Visible = true;

        JobEventsBLL JobEventsAdapter = new JobEventsBLL();

        String NewItemLabel = "";
        String SessionNumber = "";
        int MeetID;
        if (int.TryParse(this.MeetsDropDownList.SelectedValue, out MeetID))
        {
            int SessionID = int.Parse(this.SessionsDropDownList.SelectedValue);
            JobEventsAdapter.Insert(MeetID, null, this.NotesTextBox.Text, SessionID);
            NewItemLabel = this.MeetsDropDownList.SelectedItem.Text;
            SessionNumber = SessionID.ToString();
        }
        else
        {
            JobEventsAdapter.Insert(null, this.OtherTextBox.Text, this.NotesTextBox.Text, null);
            NewItemLabel = this.OtherTextBox.Text;
        }
        this.PopulateDropDownList();
        for (int i = 0; i < this.JobEventsDropDownList.Items.Count; i++)
        {
            if (this.JobEventsDropDownList.Items[i].Text.Contains(NewItemLabel))
            {
                if (this.JobEventsDropDownList.Items[i].Text.Contains(" - Session "))
                {
                    if (this.JobEventsDropDownList.Items[i].Text.EndsWith(SessionNumber))
                    {
                        this.JobEventsDropDownList.Items[i].Selected = true;
                        break;
                    }
                }
                else
                {
                    this.JobEventsDropDownList.Items[i].Selected = true;
                    break;
                }
            }
        }
        this.JobEventsGridView.DataBind();
    }
    protected void JobSignupsGridRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int arg = int.Parse(e.CommandArgument.ToString());
        GridView GV = sender as GridView;
        int JobSignUpID = -1;
        HiddenField JobSignUpIDHiddenField = GV.Rows[arg].FindControl("JobSignUpIDHiddenField") as HiddenField;
        JobSignUpID = int.Parse(JobSignUpIDHiddenField.Value);

        JobSignUpsBLL JobSignUpsAdapter = new JobSignUpsBLL();
        if (e.CommandName == "DeleteJob")
        {
            JobSignUpsAdapter.Delete(JobSignUpID);
        }
        else
        {
            JobSignUpsAdapter.BlankOutJobSignUp(JobSignUpID);
        }
        this.JobEventsGridView.DataBind();
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
                    if (GVTable.Rows[i].Cells[0].Controls.Count >= 2)
                        if (GVTable.Rows[i].Cells[0].Controls[1].GetType() == typeof(Label))
                        {
                            Label SignUpLabel = GVTable.Rows[i].Cells[0].Controls[1] as Label;
                            if (SignUpLabel != null)
                                if (SignUpLabel.Text == "Job Open")
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
                SignUpLabel.Text = "Job Open";
            }
        }
    }
    protected void AddJobOpeningButtonClicked(object sender, EventArgs e)
    {
        this.AddJobOpeningButton.Visible = false;
        this.JobOpeningPanel.Visible = true;
        this.JobEventsDropDownList.Enabled = false;
        this.AddEventButton.Enabled = false;
        this.DeleteEventButton.Enabled = false;
        this.ViewPrintableButton.Enabled = false;
    }
    protected void CancelAddJobOpenings(object sender, EventArgs e)
    {
        this.AddJobOpeningButton.Visible = true;
        this.JobOpeningPanel.Visible = false;
        this.JobEventsDropDownList.Enabled = true;
        this.AddEventButton.Enabled = true;
        this.DeleteEventButton.Enabled = true;
        this.ViewPrintableButton.Enabled = true;
    }
    protected void AddJobOpenings(object sender, EventArgs e)
    {
        JobSignUpsBLL JobSignupsAdapter = new JobSignUpsBLL();
        int JobEventID;
        if (int.TryParse(this.JobEventsDropDownList.SelectedValue, out JobEventID))
        {
            int JobTypeID = int.Parse(JobTypesDropDownList.SelectedValue);
            SwimTeamDatabase.JobSignUpsDataTable JobSignups = JobSignupsAdapter.GetByJobEventID(JobEventID);
            int JobsToAdd = int.Parse(this.NumberDropDownList.SelectedValue);
            for (int i = 0; i < JobsToAdd; i++)
            {
                SwimTeamDatabase.JobSignUpsRow NewRow = JobSignups.NewJobSignUpsRow();
                NewRow.SetFamilyIDNull();
                NewRow.SetParentIDNull();
                NewRow.SetUSAIDNull();
                NewRow.SetOtherNull();
                NewRow.JobEventID = JobEventID;
                NewRow.JobTypeID = JobTypeID;
                JobSignups.AddJobSignUpsRow(NewRow);
            }
            JobSignupsAdapter.Update(JobSignups);
        }
        this.JobEventsGridView.DataBind();
        this.JobOpeningPanel.Visible = false;
        this.AddJobOpeningButton.Visible = true;
        this.JobEventsDropDownList.Enabled = true;
        this.AddEventButton.Enabled = true;
        this.DeleteEventButton.Enabled = true;
        this.ViewPrintableButton.Enabled = true;
    }
    protected void ViewPrintableClicked(object sender, EventArgs e)
    {
        int JobEventID;
        if (int.TryParse(this.JobEventsDropDownList.SelectedValue, out JobEventID))
        {
            String RedirectLocation = "~/OfficeManager/Jobs/JobOpeningsPrintable.aspx?JE=";
            RedirectLocation += this.JobEventsDropDownList.SelectedValue;
            Response.Redirect(RedirectLocation);
        }
    }
}