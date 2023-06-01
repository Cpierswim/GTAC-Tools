using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Parents_JobSignup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.PopulateEventsDropDownList();
            JobEventsBLL JobEventsAdapter = new JobEventsBLL();
            int JobEventID;
            if (int.TryParse(this.JobEventDropDownList.SelectedValue, out JobEventID))
            {
                SwimTeamDatabase.JobEventsRow JobEvent = JobEventsAdapter.GetByJobEventID(JobEventID);
                if (JobEvent != null)
                    if (!JobEvent.IsNotesNull())
                        this.NotesLabel.Text = JobEvent.Notes;
            }
        }
        this.OtherTextBox.Attributes.Add("onkeypress", "CheckToEnableAddButton();");
        this.OtherTextBox.Attributes.Add("onblur", "CheckToEnableAddButton();");
    }
    private void PopulateEventsDropDownList()
    {
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsDataTable JobEvents = JobEventsAdapter.GetAll();
        List<ListItem> Items = new List<ListItem>();
        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        for (int i = 0; i < JobEvents.Count; i++)
        {
            ListItem TempItem = new ListItem();
            if (!JobEvents[i].IsMeetIDNull())
            {
                for (int j = 0; j < Meets.Count; j++)
                    if (JobEvents[i].MeetID == Meets[j].Meet)
                    {
                        TempItem.Value = JobEvents[i].JobEventID.ToString();
                        TempItem.Text = Meets[j].MeetName + " - Session " + JobEvents[i].MeetSessionID;
                        break;
                    }
            }
            if (!JobEvents[i].IsOtherEventNameNull())
            {
                TempItem.Value = JobEvents[i].JobEventID.ToString();
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
            this.JobEventDropDownList.Items.Add(Items[i]);
        if (this.JobEventDropDownList.Items.Count == 0)
        {
            ListItem TempItem = new ListItem();
            TempItem.Text = "No Job Events";
            TempItem.Value = "-99";
            this.JobEventDropDownList.Items.Add(TempItem);
            this.JobEventDropDownList.Enabled = false;
        }
        else
        {
            this.JobEventDropDownList.Enabled = true;
        }
    }
    private SwimTeamDatabase.JobTypesDataTable JobTypes;
    private int LastJobTypeID;
    private int JobTypeIDCurrentCount;
    protected void BeginJobSessionsDataBind(object sender, EventArgs e)
    {
        JobTypesBLL JobTypesAdapter = new JobTypesBLL();
        JobTypes = JobTypesAdapter.GetAll();
        LastJobTypeID = -1;
        JobTypeIDCurrentCount = -1;
        this.ToolTipPanel.Controls.Clear();
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
                    HyperLink SubHeaderHyperLink = new HyperLink();
                    //SubHeaderHyperLink.ID = "JobHeaderHyperLink";
                    SubHeaderHyperLink.CssClass = "hasToolTip";
                    SubHeaderHyperLink.Style.Add(HtmlTextWriterStyle.Color, "White");
                    SubHeaderHyperLink.Style.Add(HtmlTextWriterStyle.TextDecoration, "Underline");
                    int index = -1;
                    for (int i = 0; i < JobTypes.Count; i++)
                    {
                        if (JobTypes[i].JobTypeID == CurrentJobTypeID)
                        {

                            SubHeaderHyperLink.Text = JobTypes[i].Name;
                            index = i;
                            break;
                        }
                    }
                    SubHeaderHyperLink.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    String ID = JobTypes[index].Name.Replace(" ", "").Trim();
                    SubHeaderHyperLink.ID = ID + "JobHeaderHyperLink";
                    this.AddToolTip(SubHeaderHyperLink, JobTypes[index]);
                    cell.Controls.Add(SubHeaderHyperLink);
                    //Panel ToolTipPanel = new Panel();
                    //ToolTipPanel.ID = "ToolTipPanel";
                    //ToolTipPanel.CssClass = "toolTipLarge";
                    //Table ToolTipTable = new Table();
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.Width, "85%");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.Color, "White");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.MarginLeft, "auto");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.MarginRight, "auto");
                    //ToolTipTable.Style.Add(HtmlTextWriterStyle.Height, "100%");
                    //ToolTipTable.CellPadding = 0;
                    //ToolTipTable.CellSpacing = 0;
                    //Label DescriptionLabel = new Label();
                    //DescriptionLabel.Text = "Description: " + JobTypes[index].Description;
                    //DescriptionLabel.Text += "<br /><br />";
                    //DescriptionLabel.Text += "Time To Learn: " + JobTypes[index].TimeToLearn;
                    //TableCell TC = new TableCell();
                    //TC.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
                    //TC.Controls.Add(DescriptionLabel);
                    //TableRow TR = new TableRow();
                    //TR.Cells.Add(TC);
                    //ToolTipTable.Rows.Add(TR);
                    //ToolTipPanel.Controls.Add(ToolTipTable);
                    ////SubHeader.Controls.Add(ToolTipPanel);


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
                if (View["USAID"].GetType() != typeof(System.DBNull))
                {
                    SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                    SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmerByUSAID(View["USAID"].ToString());
                    if (Swimmers.Count > 0)
                    {
                        SignUpLabel.Text = Swimmers[0].PreferredName + " " + Swimmers[0].LastName;
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
                            SignUpLabel.Text = Parents[0].FirstName + " " + Parents[0].LastName;
                        }
                    }
                }
                else if (View["Other"].GetType() != typeof(System.DBNull))
                {
                    SignUpLabel.Text = View["Other"].ToString();
                }
                else
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
                            SignUpLabel.Text = LastName + " Family Signed Up";
                        }
                        else
                            SignUpLabel.Text = "Family Signed Up";
                    }
                    else
                    {
                        SignUpLabel.Text = "Family Signed Up";
                    }
                }

                LinkButton LinkButton = e.Row.Cells[1].Controls[0] as LinkButton;
                LinkButton.Text = "Delete SignUp";
                LinkButton.CommandName = "EmptyJobSignup";
            }
            else
            {
                SignUpLabel.Text = "Job Open";
                LinkButton LinkButton = e.Row.Cells[1].Controls[0] as LinkButton;
                LinkButton.Text = "Sign Up For Job";
                LinkButton.CommandName = "SignUp";
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
                        JobCountLabel.Text = "Jobs Available: " + TypeCount;
                        LastSubHeaderRow = GVTable.Rows[i];
                        TypeCount = 0;
                    }
                }
                else
                {
                    if (GVTable.Rows[i].Cells[0].Controls.Count >= 2)
                        if (GVTable.Rows[i].Cells[0].Controls[1].GetType() == typeof(Label))
                        {
                            Label SignUpLabel = GVTable.Rows[i].Cells[0].Controls[1] as Label;
                            if (SignUpLabel != null)
                                if (SignUpLabel.Text == "Job Open")
                                    TypeCount++;
                        }
                }
            }
            if (LastSubHeaderRow != null)
            {
                Label JobCountLabel = LastSubHeaderRow.FindControl("JobCountLabel") as Label;
                JobCountLabel.Text = "Jobs Available: " + TypeCount;
            }
        }
    }
    protected void GridViewRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SignUp")
        {
            this.SelectedRowHiddenField.Value = ((HiddenField)this.GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("JobSignupIDHiddenField")).Value;
            this.SignupPanel.Visible = true;
            this.AddTypeDropDownList.SelectedValue = "";
            this.ToAddDropDownList.Items.Clear();
            this.ToAddDropDownList.Visible = false;
            this.AddButton.Visible = false;
            this.OtherLabel.Visible = false;
            this.OtherTextBox.Visible = false;
        }
        else if (e.CommandName == "EmptyJobSignup")
        {
            JobSignUpsBLL JobSignupsAdapter = new JobSignUpsBLL();
            JobSignupsAdapter.BlankOutJobSignUp(int.Parse(((HiddenField)this.GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("JobSignupIDHiddenField")).Value));
            this.GridView1.DataBind();
            this.SelectedRowHiddenField.Value = "";
            this.AddTypeDropDownList.SelectedValue = "";
            this.SignupPanel.Visible = false;
        }
    }
    protected void AddButtonClicked(object sender, EventArgs e)
    {
        bool added = false;
        int SelectedJobOpening = int.Parse(this.SelectedRowHiddenField.Value);
        JobSignUpsBLL JobSignupsAdapter = new JobSignUpsBLL();
        if (AddTypeDropDownList.SelectedValue == "FAM")
        {
            if (JobSignupsAdapter.SignupFamily(SelectedJobOpening, int.Parse(Profile.FamilyID)) > 0)
                added = true;
        }
        else if (AddTypeDropDownList.SelectedValue == "SWI")
        {
            String USAID = this.ToAddDropDownList.SelectedValue;
            if (!String.IsNullOrWhiteSpace(USAID))
                if (JobSignupsAdapter.SignupSwimmer(SelectedJobOpening, int.Parse(Profile.FamilyID), USAID) > 0)
                    added = true;
        }
        else if (AddTypeDropDownList.SelectedValue == "PAR")
        {
            int ParentID;
            if (int.TryParse(this.ToAddDropDownList.SelectedValue, out ParentID))
                if (JobSignupsAdapter.SignupParent(SelectedJobOpening, int.Parse(Profile.FamilyID), ParentID) > 0)
                    added = true;
        }
        else if (AddTypeDropDownList.SelectedValue == "OTH")
        {
            if (!string.IsNullOrWhiteSpace(this.OtherTextBox.Text))
                if (JobSignupsAdapter.SignupOther(SelectedJobOpening, int.Parse(Profile.FamilyID), this.OtherTextBox.Text) > 0)
                    added = true;
        }

        if (added)
        {
            this.SelectedRowHiddenField.Value = "";
            this.AddTypeDropDownList.SelectedValue = "";
            this.SignupPanel.Visible = false;
            this.GridView1.DataBind();
        }
    }
    protected void CancelClicked(object sender, EventArgs e)
    {
        this.SelectedRowHiddenField.Value = "";
        this.AddTypeDropDownList.SelectedValue = "";
        this.SignupPanel.Visible = false;
    }
    protected void TypeDropDownListChanged(object sender, EventArgs e)
    {
        if (AddTypeDropDownList.SelectedValue == "FAM")
        {
            this.OtherLabel.Visible = false;
            this.OtherTextBox.Visible = false;
            this.ToAddDropDownList.Visible = false;
            this.AddButton.Visible = true;
            this.CancelButton.Visible = true;
        }
        else if (AddTypeDropDownList.SelectedValue == "SWI")
        {
            this.OtherLabel.Visible = false;
            this.OtherTextBox.Visible = false;
            this.ToAddDropDownList.Visible = true;
            this.AddButton.Visible = false;
            this.CancelButton.Visible = true;
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersByFamilyID(int.Parse(Profile.FamilyID));
            this.ToAddDropDownList.Items.Clear();
            this.ToAddDropDownList.Items.Add("");
            foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
            {
                ListItem Item = new ListItem();
                Item.Text = Swimmer.PreferredName;
                Item.Value = Swimmer.USAID;
                this.ToAddDropDownList.Items.Add(Item);
            }
        }
        else if (AddTypeDropDownList.SelectedValue == "PAR")
        {
            this.OtherLabel.Visible = false;
            this.OtherTextBox.Visible = false;
            this.ToAddDropDownList.Visible = true;
            this.AddButton.Visible = false;
            this.CancelButton.Visible = true;
            ParentsBLL ParentsAdapter = new ParentsBLL();
            SwimTeamDatabase.ParentsDataTable Parents = ParentsAdapter.GetParentsByFamilyID(int.Parse(Profile.FamilyID));
            this.ToAddDropDownList.Items.Clear();
            this.ToAddDropDownList.Items.Add("");
            foreach (SwimTeamDatabase.ParentsRow Parent in Parents)
            {
                ListItem Item = new ListItem();
                Item.Text = Parent.FirstName;
                Item.Value = Parent.ParentID.ToString();
                this.ToAddDropDownList.Items.Add(Item);
            }
        }
        else if (AddTypeDropDownList.SelectedValue == "OTH")
        {
            this.OtherLabel.Visible = true;
            this.OtherTextBox.Visible = true;
            this.ToAddDropDownList.Visible = false;
            this.AddButton.Visible = true;
            this.AddButton.Enabled = false;
            this.CancelButton.Visible = true;
        }
        else
        {
            this.OtherLabel.Visible = false;
            this.OtherTextBox.Visible = false;
            this.ToAddDropDownList.Visible = false;
            this.AddButton.Visible = false;
            this.CancelButton.Visible = true;
        }
    }
    protected void ToAddDropDownListSelectedItemChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrWhiteSpace(this.ToAddDropDownList.SelectedValue))
        {
            this.AddButton.Visible = true;
            this.CancelButton.Visible = true;
        }
        else
        {
            this.AddButton.Visible = false;
            this.CancelButton.Visible = true;
        }
    }
    protected void EventChanged(object sender, EventArgs e)
    {
        JobEventsBLL JobEventsAdapter = new JobEventsBLL();
        SwimTeamDatabase.JobEventsRow JobEvent = JobEventsAdapter.GetByJobEventID(int.Parse(this.JobEventDropDownList.SelectedValue));
        this.NotesLabel.Text = JobEvent.Notes;
    }

    private void AddToolTip(HyperLink Anchor, SwimTeamDatabase.JobTypesRow JobType)
    {
        Panel TipPanel = new Panel();
        TipPanel.CssClass = "toolTipLarge";
        String ID = JobType.Name.Replace(" ", "").Trim();
        TipPanel.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        TipPanel.ID = ID + "TT";

        Table TempTable = new Table();
        TempTable.CellPadding = 2;
        TempTable.Style.Add(HtmlTextWriterStyle.MarginTop, "60px;");
        TempTable.Style.Add(HtmlTextWriterStyle.MarginLeft, "30px;");
        TempTable.Style.Add(HtmlTextWriterStyle.MarginRight, "30px;");
        TempTable.Style.Add(HtmlTextWriterStyle.Width, "310px;");

        Label DescriptionLabelLabel = new Label();
        DescriptionLabelLabel.Text = "Description: ";
        TableCell Cell1 = new TableCell();
        Cell1.Style.Add(HtmlTextWriterStyle.Width, "90px;");
        Cell1.Style.Add(HtmlTextWriterStyle.TextAlign, "right;");
        Cell1.Style.Add(HtmlTextWriterStyle.FontWeight, "bold;");
        Cell1.Controls.Add(DescriptionLabelLabel);

        Label DescriptionLabel = new Label();

        DescriptionLabel.Text = JobType.Description;
        TableCell Cell2 = new TableCell();
        Cell2.Style.Add(HtmlTextWriterStyle.Width, "220px;");
        Cell2.Style.Add(HtmlTextWriterStyle.TextAlign, "left;");
        Cell2.Controls.Add(DescriptionLabel);

        TableRow TR1 = new TableRow();
        TR1.Cells.Add(Cell1);
        TR1.Cells.Add(Cell2);
        TempTable.Rows.Add(TR1);

        Label TTLLabelLabel = new Label();
        TTLLabelLabel.Text = "Time to Learn: ";
        TableCell Cell3 = new TableCell();
        Cell3.Style.Add(HtmlTextWriterStyle.Width, "90px;");
        Cell3.Style.Add(HtmlTextWriterStyle.TextAlign, "right;");
        Cell3.Style.Add(HtmlTextWriterStyle.FontWeight, "bold;");
        Cell3.Controls.Add(TTLLabelLabel);

        Label TTLLabel = new Label();
        if (!JobType.IsTimeToLearnNull())
            TTLLabel.Text = JobType.TimeToLearn;
        TableCell Cell4 = new TableCell();
        Cell4.Style.Add(HtmlTextWriterStyle.Width, "220px");
        Cell4.Style.Add(HtmlTextWriterStyle.TextAlign, "left;");
        Cell4.Controls.Add(TTLLabel);

        TableRow TR2 = new TableRow();
        TR2.Cells.Add(Cell3);
        TR2.Cells.Add(Cell4);
        TempTable.Rows.Add(TR2);
        TipPanel.Controls.Add(TempTable);
        this.ToolTipPanel.Controls.Add(TipPanel);

        StringBuilder s = new StringBuilder();

        s.Append("$(\"#" + Anchor.ClientID + "\").tooltip({\n" +
            "\ttip: '#" + TipPanel.ID + "',\n" +
            "\tposition: 'center right',\n" +
            "\toffset: [0, 15],\n" +
            "\tdelay: 0" +
            "});");
        s.Append("\n\n");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), TipPanel.ID + "Script", s.ToString(), true);
    }
}