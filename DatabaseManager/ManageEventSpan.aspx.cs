using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_ManageEventSpan : System.Web.UI.Page
{
    private const String NewDescriptionText = "Do not edit to keep same description.";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateWithTimes(this.StartTimeDropDownList);
            PopulateWithTimes(this.EndTimeDropDownList);
        }
    }

    static private void PopulateWithTimes(DropDownList List)
    {
        String AMPM = "AM";
        int index = 0;
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
                AMPM = "PM";
            for (int j = 0; j < 12; j++)
            {
                for (int k = 0; k < 60; k = k + 15)
                {
                    String Minute = k.ToString();
                    String Hour = j.ToString();
                    if (j == 0)
                        Hour = "12";
                    if (k < 10)
                        Minute = "0" + Minute;
                    String Time = Hour + ":" + Minute + " " + AMPM;
                    ListItem Temp = new ListItem(Time, index.ToString());
                    if (Time == "3:00 PM")
                        Temp.Selected = true;
                    List.Items.Add(Temp);
                    index++;
                }
            }
        }
    }
    protected void LoadButtonClicked(object sender, EventArgs e)
    {
        DateTime StartDate, EndDate;
        if (DateTime.TryParse(this.StartDateTextBox.Text, out StartDate) &&
            DateTime.TryParse(this.EndDateTextBox.Text, out EndDate))
        {
            int StartHour, StartMinute, EndHour, EndMinute;

            String StartTime = this.StartTimeDropDownList.SelectedItem.Text;
            String EndTime = this.EndTimeDropDownList.SelectedItem.Text;

            if (TimeHelper.ParseTime(StartTime, out StartHour, out StartMinute) &&
            TimeHelper.ParseTime(EndTime, out EndHour, out EndMinute))
            {
                List<int> GroupIDs = new List<int>();
                for (int i = 0; i < this.GroupsListBox.Items.Count; i++)
                    if (this.GroupsListBox.Items[i].Selected)
                        GroupIDs.Add(int.Parse(this.GroupsListBox.Items[i].Value));
                List<DayOfWeek> DaysOfWeek = new List<DayOfWeek>();
                if (this.CheckBoxList1.Items[0].Selected)
                    DaysOfWeek.Add(DayOfWeek.Sunday);
                if (this.CheckBoxList1.Items[1].Selected)
                    DaysOfWeek.Add(DayOfWeek.Monday);
                if (this.CheckBoxList1.Items[2].Selected)
                    DaysOfWeek.Add(DayOfWeek.Tuesday);
                if (this.CheckBoxList1.Items[3].Selected)
                    DaysOfWeek.Add(DayOfWeek.Wednesday);
                if (this.CheckBoxList1.Items[4].Selected)
                    DaysOfWeek.Add(DayOfWeek.Thursday);
                if (this.CheckBoxList1.Items[5].Selected)
                    DaysOfWeek.Add(DayOfWeek.Friday);
                if (this.CheckBoxList1.Items[6].Selected)
                    DaysOfWeek.Add(DayOfWeek.Saturday);

                if (DaysOfWeek.Count == 0)
                    this.AddAllDaysOfWeek(out DaysOfWeek);

                EventsBLL EventsAdapter = new EventsBLL();
                SwimTeamDatabase.EventsDataTable CalendarEvents =
                    EventsAdapter.GetEvents(StartDate, EndDate, StartHour, StartMinute, EndHour, EndMinute, GroupIDs, DaysOfWeek);

                this.GridView1.DataSource = CalendarEvents;
                this.GridView1.DataBind();

                this.NewEndTimeDropDownList.Items.Clear();
                PopulateWithTimes(this.NewEndTimeDropDownList);
                this.NewStartTimeDropDownList.Items.Clear();
                PopulateWithTimes(this.NewStartTimeDropDownList);
                this.NewDescription.Text = NewDescriptionText;


                this.SelectPanel.Visible = false;
                this.EditPanel.Visible = true;
            }
        }
    }
    protected void GridViewDataBound(object sender, EventArgs e)
    {
        if (this.GridView1.DataSource != null)
        {
            if (((SwimTeamDatabase.EventsDataTable)this.GridView1.DataSource).Count > 0)
            {
                GroupsBLL GroupsAdapter = new GroupsBLL();
                SwimTeamDatabase.GroupsDataTable Groups = GroupsAdapter.GetActiveGroups();

                for (int i = 0; i < this.GridView1.Rows.Count; i++)
                {
                    if (this.GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        DateTime Start = DateTime.Parse(((Label)this.GridView1.Rows[i].Cells[1].Controls[1]).Text);
                        DateTime End = DateTime.Parse(((Label)this.GridView1.Rows[i].Cells[2].Controls[1]).Text);
                        int GroupID = int.Parse(((Label)this.GridView1.Rows[i].Cells[3].Controls[1]).Text);
                        int GroupIndex = -1;
                        for (int j = 0; j < Groups.Count; j++)
                            if (Groups[j].GroupID == GroupID)
                            {
                                GroupIndex = j;
                                break;
                            }

                        ((Label)this.GridView1.Rows[i].Cells[1].Controls[1]).Text = Start.ToString("ddd, M/d/yy h:mm tt");
                        ((Label)this.GridView1.Rows[i].Cells[2].Controls[1]).Text = End.ToString("h:mm tt");
                        ((Label)this.GridView1.Rows[i].Cells[3].Controls[1]).Text = Groups[GroupIndex].GroupName;
                    }
                }
            }
        }
    }
    protected void CancelClicked(object sender, EventArgs e)
    {
        this.SelectPanel.Visible = true;
        this.EditPanel.Visible = false;
    }
    protected void SaveButtonClicked(object sender, CommandEventArgs e)
    {
        DateTime StartDate, EndDate;
        if (DateTime.TryParse(this.StartDateTextBox.Text, out StartDate) &&
            DateTime.TryParse(this.EndDateTextBox.Text, out EndDate))
        {
            int StartHour, StartMinute, EndHour, EndMinute;

            String StartTime = this.StartTimeDropDownList.SelectedItem.Text;
            String EndTime = this.EndTimeDropDownList.SelectedItem.Text;

            if (TimeHelper.ParseTime(StartTime, out StartHour, out StartMinute) &&
            TimeHelper.ParseTime(EndTime, out EndHour, out EndMinute))
            {
                List<int> GroupIDs = new List<int>();
                for (int i = 0; i < this.GroupsListBox.Items.Count; i++)
                    if (this.GroupsListBox.Items[i].Selected)
                        GroupIDs.Add(int.Parse(this.GroupsListBox.Items[i].Value));
                List<DayOfWeek> DaysOfWeek = new List<DayOfWeek>();
                if (this.CheckBoxList1.Items[0].Selected)
                    DaysOfWeek.Add(DayOfWeek.Sunday);
                if (this.CheckBoxList1.Items[1].Selected)
                    DaysOfWeek.Add(DayOfWeek.Monday);
                if (this.CheckBoxList1.Items[2].Selected)
                    DaysOfWeek.Add(DayOfWeek.Tuesday);
                if (this.CheckBoxList1.Items[3].Selected)
                    DaysOfWeek.Add(DayOfWeek.Wednesday);
                if (this.CheckBoxList1.Items[4].Selected)
                    DaysOfWeek.Add(DayOfWeek.Thursday);
                if (this.CheckBoxList1.Items[5].Selected)
                    DaysOfWeek.Add(DayOfWeek.Friday);
                if (this.CheckBoxList1.Items[6].Selected)
                    DaysOfWeek.Add(DayOfWeek.Saturday);

                if (DaysOfWeek.Count == 0)
                    this.AddAllDaysOfWeek(out DaysOfWeek);

                EventsBLL EventsAdapter = new EventsBLL();
                SwimTeamDatabase.EventsDataTable CalendarEvents =
                    EventsAdapter.GetEvents(StartDate, EndDate, StartHour, StartMinute, EndHour, EndMinute, GroupIDs, DaysOfWeek);

                int NewStartHour, NewStartMinute, NewEndHour, NewEndMinute;
                if (TimeHelper.ParseTime(this.NewStartTimeDropDownList.SelectedItem.Text, out NewStartHour, out NewStartMinute) &&
                    TimeHelper.ParseTime(this.NewEndTimeDropDownList.SelectedItem.Text, out NewEndHour, out NewEndMinute))
                {
                    foreach (SwimTeamDatabase.EventsRow Event in CalendarEvents)
                    {
                        Event.DateandTime = new DateTime(Event.DateandTime.Year, Event.DateandTime.Month,
                            Event.DateandTime.Day, NewStartHour, NewStartMinute, 0);
                        Event.EndTime = new DateTime(Event.EndTime.Year, Event.EndTime.Month, Event.EndTime.Day,
                            NewEndHour, NewEndMinute, 0);
                        int GroupID = int.Parse(this.NewGroupDropDownList.SelectedItem.Value);
                        if (GroupID != -1)
                            Event.GroupID = GroupID;
                        if (this.NewDescription.Text != NewDescriptionText)
                            Event.Name = this.NewDescription.Text;
                    }

                    EventsAdapter.BatchUpdate(CalendarEvents);
                    this.EditPanel.Visible = false;
                    this.SelectPanel.Visible = true;
                    this.StartDateTextBox.Text = "";
                    this.EndDateTextBox.Text = "";
                    this.StartTimeDropDownList.Items.Clear();
                    PopulateWithTimes(this.StartTimeDropDownList);
                    this.EndTimeDropDownList.Items.Clear();
                    PopulateWithTimes(this.EndTimeDropDownList);
                    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                        this.CheckBoxList1.Items[i].Selected = false;
                    for (int i = 0; i < this.GroupsListBox.Items.Count; i++)
                        this.GroupsListBox.Items[i].Selected = false;
                    this.GridView1.DataSource = null;
                    this.GridView1.DataBind();

                }
            }
        }
    }

    protected void DeleteClicked(object sender, EventArgs e)
    {
        DateTime StartDate, EndDate;
        if (DateTime.TryParse(this.StartDateTextBox.Text, out StartDate) &&
            DateTime.TryParse(this.EndDateTextBox.Text, out EndDate))
        {
            int StartHour, StartMinute, EndHour, EndMinute;

            String StartTime = this.StartTimeDropDownList.SelectedItem.Text;
            String EndTime = this.EndTimeDropDownList.SelectedItem.Text;

            if (TimeHelper.ParseTime(StartTime, out StartHour, out StartMinute) &&
            TimeHelper.ParseTime(EndTime, out EndHour, out EndMinute))
            {
                List<int> GroupIDs = new List<int>();
                for (int i = 0; i < this.GroupsListBox.Items.Count; i++)
                    if (this.GroupsListBox.Items[i].Selected)
                        GroupIDs.Add(int.Parse(this.GroupsListBox.Items[i].Value));
                List<DayOfWeek> DaysOfWeek = new List<DayOfWeek>();
                if (this.CheckBoxList1.Items[0].Selected)
                    DaysOfWeek.Add(DayOfWeek.Sunday);
                if (this.CheckBoxList1.Items[1].Selected)
                    DaysOfWeek.Add(DayOfWeek.Monday);
                if (this.CheckBoxList1.Items[2].Selected)
                    DaysOfWeek.Add(DayOfWeek.Tuesday);
                if (this.CheckBoxList1.Items[3].Selected)
                    DaysOfWeek.Add(DayOfWeek.Wednesday);
                if (this.CheckBoxList1.Items[4].Selected)
                    DaysOfWeek.Add(DayOfWeek.Thursday);
                if (this.CheckBoxList1.Items[5].Selected)
                    DaysOfWeek.Add(DayOfWeek.Friday);
                if (this.CheckBoxList1.Items[6].Selected)
                    DaysOfWeek.Add(DayOfWeek.Saturday);

                if (DaysOfWeek.Count == 0)
                    this.AddAllDaysOfWeek(out DaysOfWeek);

                EventsBLL EventsAdapter = new EventsBLL();
                SwimTeamDatabase.EventsDataTable CalendarEvents =
                    EventsAdapter.GetEvents(StartDate, EndDate, StartHour, StartMinute, EndHour, EndMinute, GroupIDs, DaysOfWeek);

                int NewStartHour, NewStartMinute, NewEndHour, NewEndMinute;
                if (TimeHelper.ParseTime(this.NewStartTimeDropDownList.SelectedItem.Text, out NewStartHour, out NewStartMinute) &&
                    TimeHelper.ParseTime(this.NewEndTimeDropDownList.SelectedItem.Text, out NewEndHour, out NewEndMinute))
                {
                    foreach (SwimTeamDatabase.EventsRow Event in CalendarEvents)
                    {
                        Event.Delete();
                    }

                    EventsAdapter.BatchUpdate(CalendarEvents);
                    this.EditPanel.Visible = false;
                    this.SelectPanel.Visible = true;
                    this.StartDateTextBox.Text = "";
                    this.EndDateTextBox.Text = "";
                    this.StartTimeDropDownList.Items.Clear();
                    PopulateWithTimes(this.StartTimeDropDownList);
                    this.EndTimeDropDownList.Items.Clear();
                    PopulateWithTimes(this.EndTimeDropDownList);
                    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
                        this.CheckBoxList1.Items[i].Selected = false;
                    for (int i = 0; i < this.GroupsListBox.Items.Count; i++)
                        this.GroupsListBox.Items[i].Selected = false;
                    this.GridView1.DataSource = null;
                    this.GridView1.DataBind();
                }
            }
        }
    }
    protected void GroupsDrop(object sender, EventArgs e)
    {

    }
    protected void GroupsDropDownListDataBound(object sender, EventArgs e)
    {
        ListItem Item = new ListItem("Original Group", "-1");
        Item.Selected = true;
        List<ListItem> ItemList = new List<ListItem>();
        for (int i = 0; i < this.NewGroupDropDownList.Items.Count; i++)
            ItemList.Add(this.NewGroupDropDownList.Items[i]);
        this.NewGroupDropDownList.Items.Clear();
        this.NewGroupDropDownList.Items.Add(Item);
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].Selected = false;
            this.NewGroupDropDownList.Items.Add(ItemList[i]);
        }

    }
    protected void AddAllDaysOfWeek(out List<DayOfWeek> DaysOfWeek)
    {
        DaysOfWeek = new List<DayOfWeek>();
        DaysOfWeek.Add(DayOfWeek.Sunday);
        DaysOfWeek.Add(DayOfWeek.Monday);
        DaysOfWeek.Add(DayOfWeek.Tuesday);
        DaysOfWeek.Add(DayOfWeek.Wednesday);
        DaysOfWeek.Add(DayOfWeek.Thursday);
        DaysOfWeek.Add(DayOfWeek.Friday);
        DaysOfWeek.Add(DayOfWeek.Saturday);

        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            this.CheckBoxList1.Items[i].Selected = true;
        }
    }
}