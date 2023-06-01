using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class OfficeManager_Jobs_JobEvents : System.Web.UI.Page
{
    SwimTeamDatabase.JobEventsDataTable TempJobEvents;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack || Request.QueryString["R"] != null)
        {
            this.PopulateDropDownList();
        }
        if (Page.IsPostBack)
            this.JobSignupsGridView.DataBind();
        //if (this.DropDownList1.SelectedValue.Contains("-$-"))
        //{
        //    String MeetID = this.DropDownList1.SelectedValue.Substring(0, this.DropDownList1.SelectedValue.IndexOf("-$-"));
        //    String SessionNumber = this.DropDownList1.SelectedValue.Substring(this.DropDownList1.SelectedValue.IndexOf("-$-") +
        //        "-$-".Length);
        //    MeetIDHiddenField.Value = MeetID;
        //    SessionNumberHiddenField.Value = SessionNumber;
        //    OtherHiddenField.Value = null;
        //}
        //else
        //{
        //    OtherHiddenField.Value = this.DropDownList1.SelectedValue;
        //    MeetIDHiddenField.Value = null;
        //    SessionNumberHiddenField.Value = null;
        //}
        this.JobEventIDHiddenField.Value = this.DropDownList1.SelectedValue;

        //JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        //SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        //TempJobEvents = JobEvents;
        //for (int i = 0; i < JobEvents.Count; i++)
        //{
        //    if (!JobEvents[i].IsMeetIDNull())
        //    {
        //        if (this.DropDownList1.SelectedValue.Contains("-$-"))
        //        {
        //            int MeetID = int.Parse(this.DropDownList1.SelectedValue.Substring(0, this.DropDownList1.SelectedValue.IndexOf("-$-")));
        //            int SessionNumber = int.Parse(this.DropDownList1.SelectedValue.Substring(this.DropDownList1.SelectedValue.IndexOf("-$-") +
        //                "-$-".Length));
        //            if (JobEvents[i].MeetID == MeetID && JobEvents[i].MeetSessionID == SessionNumber)
        //            {
        //                this.JobEventIDHiddenField.Value = JobEvents[i].JobEventID.ToString();
        //            }
        //        }
        //    }
        //    if (!JobEvents[i].IsOtherEventNameNull())
        //    {
        //        if (this.DropDownList1.SelectedValue == JobEvents[i].OtherEventName)
        //        {
        //            this.JobEventIDHiddenField.Value = JobEvents[i].JobEventID.ToString();
        //            break;
        //        }
        //    }
        //}
    }

    private void PopulateDropDownList()
    {
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        List<ListItem> Items = new List<ListItem>();
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        //SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
        //SwimTeamDatabase.SessionV2DataTable Sessions = SessionsAdapter.GetAllSessions();
        for (int i = 0; i < JobEvents.Count; i++)
        {
            ListItem TempItem = new ListItem();
            if (!JobEvents[i].IsMeetIDNull())
            {
                for (int j = 0; j < Meets.Count; j++)
                    if (JobEvents[i].MeetID == Meets[j].Meet)
                    {
                        TempItem.Value = JobEvents[i].ToString();
                        TempItem.Text = Meets[j].MeetName + " - Session " + JobEvents[i].MeetSessionID;
                        break;
                    }
            }
            else if (!JobEvents[i].IsOtherEventNameNull())
            {
                TempItem.Value = JobEvents[i].OtherEventName;
                TempItem.Text = JobEvents[i].OtherEventName;
            }
            bool ItemFound = false;
            for (int j = 0; j < Items.Count; j++)
            {
                if (Items[j].Value == TempItem.Value)
                {
                    ItemFound = true;
                    break;
                }
            }

            if (!ItemFound)
                Items.Add(TempItem);
        }

        for (int i = 0; i < Items.Count; i++)
            this.DropDownList1.Items.Add(Items[i]);
        if (this.DropDownList1.Items.Count == 0)
        {
            this.DropDownList1.Items.Add("No Job Events");
            this.DropDownList1.Enabled = false;
            this.AddJobsButton.Visible = false;
        }
        else
        {
            this.DropDownList1.Enabled = true;
            this.AddJobsButton.Visible = true;
        }
    }
    private void PopulateDropDownList(int MeetID, int SessionNumber)
    {
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        List<ListItem> Items = new List<ListItem>();
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        //SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
        //SwimTeamDatabase.SessionV2DataTable Sessions = SessionsAdapter.GetAllSessions();
        for (int i = 0; i < JobEvents.Count; i++)
        {
            ListItem TempItem = new ListItem();
            if (!JobEvents[i].IsMeetIDNull())
            {
                for (int j = 0; j < Meets.Count; j++)
                    if (JobEvents[i].MeetID == Meets[j].Meet)
                    {
                        TempItem.Value = Meets[j].Meet + "-$-" + JobEvents[i].MeetSessionID;
                        TempItem.Text = Meets[j].MeetName + " - Session " + JobEvents[i].MeetSessionID;
                        if (Meets[j].Meet == MeetID && JobEvents[i].MeetSessionID == SessionNumber)
                            TempItem.Selected = true;
                        break;
                    }
            }
            if (!JobEvents[i].IsOtherEventNameNull())
            {
                TempItem.Value = JobEvents[i].OtherEventName;
                TempItem.Text = JobEvents[i].OtherEventName;
            }
            bool ItemFound = false;
            for (int j = 0; j < Items.Count; j++)
            {
                if (Items[j].Value == TempItem.Value)
                {
                    ItemFound = true;
                    break;
                }
            }

            if (!ItemFound)
                Items.Add(TempItem);
        }

        for (int i = 0; i < Items.Count; i++)
            this.DropDownList1.Items.Add(Items[i]);
        if (this.DropDownList1.Items.Count == 0)
        {
            this.DropDownList1.Items.Add("No Job Events");
            this.DropDownList1.Enabled = false;
            this.AddJobsButton.Visible = false;
        }
        else
        {
            this.DropDownList1.Enabled = true;
            this.AddJobsButton.Visible = true;
        }
    }
    protected void AddMeetButtonClicked(object sender, EventArgs e)
    {
        this.AddMeetPanel.Visible = true;
        this.AddOtherPanel.Visible = false;
        this.AddJobsPanel.Visible = false;
        this.AddJobPanel.Visible = false;
    }
    protected void AddOtherEventClicked(object sender, EventArgs e)
    {
        this.AddOtherPanel.Visible = true;
        this.AddMeetPanel.Visible = false;
        this.AddJobsPanel.Visible = false;
        this.AddJobPanel.Visible = false;
    }
    protected void MeetIndexChanged(object sender, EventArgs e)
    {
        this.SessionsDropDownList.DataBind();
    }
    protected void AddMeetAndSession(object sender, EventArgs e)
    {
        int MeetID = int.Parse(this.MeetsDropDownList.SelectedValue);
        int SessionNumber = int.Parse(this.SessionsDropDownList.SelectedValue);

        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        JobEventsAdapter.Insert(MeetID, null, this.NotesTextBox.Text, SessionNumber);
        this.DropDownList1.Items.Clear();
        this.PopulateDropDownList(MeetID, SessionNumber);
        this.JobSignupsGridView.DataBind();
        this.AddMeetPanel.Visible = false;
        this.AddJobsPanel.Visible = true;
        this.AddOtherPanel.Visible = false;
        this.AddJobPanel.Visible = false;
    }
    protected void AddOtherEvent(object sender, EventArgs e)
    {
        bool EventFound = false;
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        for (int i = 0; i < JobEvents.Count; i++)
            if (!JobEvents[i].IsOtherEventNameNull())
                if (JobEvents[i].OtherEventName == this.OtherEventNameTextBox.Text)
                {
                    EventFound = true;
                    break;
                }
        if (!EventFound)
            JobEventsAdapter.Insert(null, this.OtherEventNameTextBox.Text, this.OtherEventNotesTextBox.Text, null);
        this.DropDownList1.Items.Clear();
        this.PopulateDropDownList();
        this.AddOtherPanel.Visible = false;
        this.AddJobsPanel.Visible = true;
        this.AddJobPanel.Visible = false;
        this.AddMeetPanel.Visible = false;
    }
    protected void DeleteEvent(object sender, EventArgs e)
    {
        this.CancelButton.Visible = false;

        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        for (int i = 0; i < JobEvents.Count; i++)
        {
            if (!JobEvents[i].IsMeetIDNull())
            {
                if (this.DropDownList1.SelectedValue.Contains("-$-"))
                {
                    int MeetID = int.Parse(this.DropDownList1.SelectedValue.Substring(0, this.DropDownList1.SelectedValue.IndexOf("-$-")));
                    int SessionNumber = int.Parse(this.DropDownList1.SelectedValue.Substring(this.DropDownList1.SelectedValue.IndexOf("-$-") +
                        "-$-".Length));
                    if (JobEvents[i].MeetID == MeetID && JobEvents[i].MeetSessionID == SessionNumber)
                    {
                        JobEventsAdapter.Delete(JobEvents[i].JobEventID);
                        break;
                    }
                }
            }
            if (!JobEvents[i].IsOtherEventNameNull())
            {
                if (this.DropDownList1.SelectedValue == JobEvents[i].OtherEventName)
                {
                    JobEventsAdapter.Delete(JobEvents[i].JobEventID);
                    //JobEvents[i].Delete();
                    break;
                }
            }
        }
        //JobEventsAdapter.Update(JobEvents);
        this.DropDownList1.Items.Clear();
        this.PopulateDropDownList();
        this.JobSignupsGridView.DataBind();
    }
    protected void AddJobsButtonClicked(object sender, EventArgs e)
    {
        AddJobPanel.Visible = true;
        this.CancelButton.Visible = true;
        this.JobSignupsGridView.DataBind();
    }
    protected void CancelButtonClicked(object sender, EventArgs e)
    {
        AddJobPanel.Visible = false;
        CancelButton.Visible = false;
        this.JobSignupsGridView.DataBind();
    }
    private SwimTeamDatabase.JobTypesDataTable JobTypes;
    protected void BeginJobSessionsDataBind(object sender, EventArgs e)
    {
        JobTypesBLL JobTypesAdapter = new JobTypesBLL();
        JobTypes = JobTypesAdapter.GetAll();
        LastJobTypeID = -1;
        JobTypeIDCurrentCount = -1;
    }
    private int LastJobTypeID;
    private int JobTypeIDCurrentCount;
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
                SignUpLabel.Text = "Family SignedUp";
            }
            else if (View["USAID"].GetType() != typeof(System.DBNull))
            {
                SignUpLabel.Text = "Swimmer SignedUp";
            }
            else if (View["ParentID"].GetType() != typeof(System.DBNull))
            {
                SignUpLabel.Text = "Parent SignedUp";
            }
            else if (View["Other"].GetType() != typeof(System.DBNull))
            {
                SignUpLabel.Text = View["Other"].ToString();
            }
            else
            {
                SignUpLabel.Text = "Empty.";
            }
        }
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
            int TypeCount = 0;
            for (int i = 0; i < GVTable.Rows.Count - 1; i++)
            {
                if (GVTable.Rows[i].FindControl("JobCountLabel") != null)
                {
                    if (!FirstSubHeaderFound)
                    {
                        FirstSubHeaderFound = true;
                        TypeCount = 0;
                        LastSubHeaderRow = GVTable.Rows[i];
                    }
                    else
                    {
                        Label JobCountLabel = LastSubHeaderRow.FindControl("JobCountLabel") as Label;
                        JobCountLabel.Text = "Count: " + TypeCount;
                        LastSubHeaderRow = GVTable.Rows[i];
                        TypeCount = 0;
                    }
                }
                else
                    TypeCount++;
            }
            if (LastSubHeaderRow != null)
            {
                Label JobCountLabel = LastSubHeaderRow.FindControl("JobCountLabel") as Label;
                JobCountLabel.Text = "Count: " + TypeCount;
            }
        }
    }
    protected void AddJobs(object sender, EventArgs e)
    {
        //int JobEventID = -11;
        //for (int i = 0; i <  TempJobEvents.Count; i++)
        //{
        //    if (!TempJobEvents[i].IsMeetIDNull())
        //    {
        //        if (this.DropDownList1.SelectedValue.Contains("-$-"))
        //        {
        //            int MeetID = int.Parse(this.DropDownList1.SelectedValue.Substring(0, this.DropDownList1.SelectedValue.IndexOf("-$-")));
        //            int SessionNumber = int.Parse(this.DropDownList1.SelectedValue.Substring(this.DropDownList1.SelectedValue.IndexOf("-$-") +
        //                "-$-".Length));
        //            if (TempJobEvents[i].MeetID == MeetID && TempJobEvents[i].MeetSessionID == SessionNumber)
        //            {
        //                JobEventID = TempJobEvents[i].JobEventID;
        //            }
        //        }
        //    }
        //    if (!TempJobEvents[i].IsOtherEventNameNull())
        //    {
        //        if (this.DropDownList1.SelectedValue == TempJobEvents[i].OtherEventName)
        //        {
        //            JobEventID = TempJobEvents[i].JobEventID;
        //            break;
        //        }
        //    }
        //}
        //if (JobEventID != -11)
        //{
            JobSignUpsBLL JobSignupsAdapter = new JobSignUpsBLL();
            int JobEventID = int.Parse(JobEventIDHiddenField.Value);
            //int JobEventID = int.Parse(this.DropDownList1.SelectedValue);
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
            this.AddJobPanel.Visible = false;
            this.JobSignupsGridView.DataBind();
            this.CancelButton.Visible = false;
        //}
    }
    protected void JobSignupsGridRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int arg = int.Parse(e.CommandArgument.ToString());
        GridView GV = sender as GridView;
        //Table GVTable = null;
        int JobSignUpID = -1;
        HiddenField JobSignUpIDHiddenField = GV.Rows[arg].FindControl("JobSignUpIDHiddenField") as HiddenField;
        JobSignUpID = int.Parse(JobSignUpIDHiddenField.Value);
        //int index = int.Parse(e.CommandArgument.ToString());
        //for(int i = 0; i < GV.Rows.Count; i++)
        //    if (GV.Rows[i].DataItemIndex == index)
        //    {
        //        HiddenField JobSignUpIDHiddenField = GV.Rows[i].FindControl("JobSignUpIDHiddenField") as HiddenField;
        //        JobSignUpID = int.Parse(JobSignUpIDHiddenField.Value);
        //    }
        //if (GV.Rows.Count > 0)
        //{
        //    GVTable = GV.Rows[0].Parent as Table;
        //    int index = 0;
        //    for (int i = 0; i < GVTable.Rows.Count; i++)
        //    {
        //        object obj = GVTable.Rows[i].FindControl("JobSignUpIDHiddenField");
        //        if (obj != null)
        //        {
        //            HiddenField JobSignUpIDHiddenField = obj as HiddenField;
        //            if (!String.IsNullOrWhiteSpace(JobSignUpIDHiddenField.Value))
        //            {
        //                if (index == arg)
        //                {
        //                    JobSignUpID = int.Parse(JobSignUpIDHiddenField.Value);
        //                    break;
        //                }
        //                else
        //                    index++;
        //            }
        //        }
        //    }
        //}
        JobSignUpsBLL JobSignUpsAdapter = new JobSignUpsBLL();
        if (e.CommandName == "DeleteJob")
        {
            JobSignUpsAdapter.Delete(JobSignUpID);
        }
        else
        {
            JobSignUpsAdapter.BlankOutJobSignUp(JobSignUpID);
        }
        this.JobSignupsGridView.DataBind();
    }
    protected void EventChanged(object sender, EventArgs e)
    {
        this.JobSignupsGridView.DataBind();
    }
}