using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Coach_MeetSignupsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CreateBackgroundColorStrings();
    }
    List<int> SelectedMeets;
    protected void LoadMeets(object sender, EventArgs e)
    {
        this.SelectedMeets = new List<int>();
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
            if (this.CheckBoxList1.Items[i].Selected)
                SelectedMeets.Add(int.Parse(this.CheckBoxList1.Items[i].Value));
        this.CreateTable();
        this.GroupSelectPanel.Visible = false;
        this.MeetSelectPanel.Visible = false;
        this.ShowPanel.Visible = true;
    }
    protected void GroupPicked(object sender, EventArgs e)
    {
        this.GroupSelectPanel.Visible = false;
        this.MeetSelectPanel.Visible = true;
        this.ShowPanel.Visible = false;
    }
    protected void StartOverClicked(object sender, EventArgs e)
    {
        this.GroupSelectPanel.Visible = true;
        this.MeetSelectPanel.Visible = false;
        this.ShowPanel.Visible = false;
        this.GroupsDropDownList.DataBind();
    }
    private void CreateTable()
    {
        this.SignupsTable.Rows.Clear();
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersByGroupID(int.Parse(this.GroupsDropDownList.SelectedValue));
        PreEnteredV2BLL PreEntryAdapter = new PreEnteredV2BLL();
        SwimTeamDatabase.PreEnteredV2DataTable PreEntries = PreEntryAdapter.GetPreEntriesByGroupID(int.Parse(this.GroupsDropDownList.SelectedValue));

        String HeaderRowBackgroundColor = this.GetBackgroundColorString();
        TableCell BlankCell = new TableCell();
        BlankCell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
        BlankCell.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
        BlankCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, HeaderRowBackgroundColor);
        TableRow HeaderRow = new TableRow();
        HeaderRow.Cells.Add(BlankCell);

        MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
        SwimTeamDatabase.MeetsV2DataTable Meets = MeetsAdapter.GetAllMeets();
        foreach (int MeetID in SelectedMeets)
        {
            String SelectedMeetName = "";
            foreach (SwimTeamDatabase.MeetsV2Row Meet in Meets)
            {
                if (Meet.Meet == MeetID)
                {
                    SelectedMeetName = Meet.MeetName;
                    break;
                }
            }
            Label MeetNameLabel = new Label();
            MeetNameLabel.Text = SelectedMeetName;

            TableCell MeetNameCell = new TableCell();
            MeetNameCell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            MeetNameCell.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            MeetNameCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            MeetNameCell.Style.Add(HtmlTextWriterStyle.VerticalAlign, "bottom");
            MeetNameCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, HeaderRowBackgroundColor);
            MeetNameCell.Controls.Add(MeetNameLabel);
            HeaderRow.Cells.Add(MeetNameCell);
        }

        this.SignupsTable.Rows.Add(HeaderRow);

        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
        {
            String RowBackgroundColor = this.GetBackgroundColorString();

            Label NameLabel = new Label();
            NameLabel.Text = Swimmer.PreferredName + " " + Swimmer.LastName;

            TableCell NameCell = new TableCell();
            NameCell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            NameCell.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            NameCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, RowBackgroundColor);
            NameCell.Controls.Add(NameLabel);

            TableRow Row = new TableRow();
            Row.Cells.Add(NameCell);


            foreach (int Meet in this.SelectedMeets)
            {
                bool SwimmerPreEnteredInMeet = false;
                foreach (SwimTeamDatabase.PreEnteredV2Row PreEntry in PreEntries)
                {
                    if (PreEntry.MeetID == Meet && PreEntry.USAID == Swimmer.USAID)
                    {
                        SwimmerPreEnteredInMeet = true;
                        break;
                    }
                }

                Label EnteredLabel = new Label();
                if (SwimmerPreEnteredInMeet)
                    EnteredLabel.Text = "X";

                TableCell EnteredCell = new TableCell();
                EnteredCell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px;");
                EnteredCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                EnteredCell.Style.Add(HtmlTextWriterStyle.BackgroundColor, RowBackgroundColor);
                EnteredCell.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                EnteredCell.Controls.Add(EnteredLabel);
                Row.Cells.Add(EnteredCell);

            }

            this.SignupsTable.Rows.Add(Row);
        }
    }

    private String[] BackgroundColorStrings = new String[2];
    private int Current = -1;

    private String GetBackgroundColorString()
    {
        if (Current == 1)
            Current--;
        else
            Current++;
        return BackgroundColorStrings[Current];
    }

    private void CreateBackgroundColorStrings()
    {
        if (String.IsNullOrWhiteSpace(BackgroundColorStrings[0]))
        {
            this.BackgroundColorStrings[0] = "White";
            this.BackgroundColorStrings[1] = "LightBlue";
        }
    }
}