using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_ManageEventsForDay : System.Web.UI.Page
{
    private Dictionary<int, string> GroupsDictionary;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            Calendar1.SelectedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

        SwimTeamDatabase.GroupsDataTable Groups = new GroupsBLL().GetActiveGroups();
        GroupsDictionary = new Dictionary<int, string>();
        foreach (SwimTeamDatabase.GroupsRow Group in Groups)
            GroupsDictionary.Add(Group.GroupID, Group.GroupName);
    }
    protected void RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Calendar EventDateCalendar = (Calendar)e.Row.FindControl("EventDateCalendar");
            DateTime EventDate = EventDateCalendar.SelectedDate;

            if (e.Row.RowState == DataControlRowState.Normal ||
                e.Row.RowState == DataControlRowState.Alternate)
            {
                Label EventDateLabel = (Label)e.Row.FindControl("EventDateLabel");
                EventDateLabel.Text = EventDate.ToString("d");
                Label StartTimeLabel = (Label)e.Row.FindControl("StartTimeLabel");
                int StartHour = EventDate.Hour;
                int StartMinute = EventDate.Minute;
                bool StartPM = false;
                if (StartHour > 12)
                {
                    StartHour = StartHour - 12;
                    StartPM = true;
                }
                String StartTime = StartHour + ":";
                if (StartMinute == 0)
                    StartTime += "00";
                else if (StartMinute <= 10)
                    StartTime += "0" + StartMinute;
                else
                    StartTime += StartMinute.ToString();

                if (StartPM)
                    StartTime += " PM";
                else
                    StartTime += " AM";
                StartTimeLabel.Text = StartTime;

                Label EndTimeLabel = (Label)e.Row.FindControl("EndTimeLabel");
                HiddenField EndDateTimeHiddenField = (HiddenField)e.Row.FindControl("EndDateTimeHiddenField");
                DateTime EndDateTime = DateTime.Parse(EndDateTimeHiddenField.Value);
                int EndHour = EndDateTime.Hour;
                int EndMinute = EndDateTime.Minute;
                bool EndPM = false;
                if (EndHour > 12)
                {
                    EndHour = EndHour - 12;
                    EndPM = true;
                }
                String EndTime = EndHour + ":";
                if (EndMinute == 0)
                    EndTime += "00";
                else if (EndMinute <= 10)
                    EndTime += "0" + EndMinute;
                else
                    EndTime += EndMinute.ToString();

                if (EndPM)
                    EndTime += " PM";
                else
                    EndTime += " AM";

                if (EventDate.Year < EndDateTime.Year ||
                   ((EventDate.Year == EndDateTime.Year) && (EventDate.DayOfYear < EndDateTime.DayOfYear)))
                    EndTime += "<br />The next day";
                EndTimeLabel.Text = EndTime;

                Label GroupsLabel = (Label)e.Row.FindControl("GroupsLabel");
                int GroupID = int.Parse(GroupsLabel.Text);
                string GroupName = GroupsDictionary[GroupID];
                GroupsLabel.Text = GroupName;
            }
            else if (e.Row.RowState.ToString().Contains("Edit"))
            {
                int StartHour = EventDate.Hour;
                int StartMinute = EventDate.Minute;
                bool StartPM = false;

                if (StartHour > 12)
                {
                    StartHour -= 12;
                    StartPM = true;
                }

                DropDownList StartHourDropDownList = (DropDownList)e.Row.FindControl("StartHourDropDownList");
                StartHourDropDownList.SelectedValue = StartHour.ToString();
                DropDownList StartMinuteDropDownList = (DropDownList)e.Row.FindControl("StartMinuteDropDownList");

                for (int i = 0; i < 60; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = i.ToString();
                    if (i == 0)
                        li.Text = "00";
                    else if (i < 10)
                        li.Text = "0" + i;
                    else
                        li.Text = i.ToString();

                    if (i == StartMinute)
                        li.Selected = true;
                    else
                        li.Selected = false;

                    StartMinuteDropDownList.Items.Add(li);
                }

                DropDownList StartAMPMDropDownList = (DropDownList)e.Row.FindControl("StartAMPMDropDownList");
                if (StartPM)
                    StartAMPMDropDownList.SelectedValue = "PM";
                else
                    StartAMPMDropDownList.SelectedValue = "AM";

                HiddenField EndDateTimeHiddenField = (HiddenField)e.Row.FindControl("EndDateTimeHiddenField");
                DateTime EndDateTime = DateTime.Parse(EndDateTimeHiddenField.Value);

                int EndHour = EndDateTime.Hour;
                int EndMinute = EndDateTime.Minute;
                bool EndPM = false;

                if (EndHour > 12)
                {
                    EndHour -= 12;
                    EndPM = true;
                }

                DropDownList EndHourDropDownList = (DropDownList)e.Row.FindControl("EndHourDropDownList");
                EndHourDropDownList.SelectedValue = EndHour.ToString();
                DropDownList EndMinuteDropDownList = (DropDownList)e.Row.FindControl("EndMinuteDropDownList");

                for (int i = 0; i < 60; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = i.ToString();
                    if (i == 0)
                        li.Text = "00";
                    else if (i < 10)
                        li.Text = "0" + i;
                    else
                        li.Text = i.ToString();

                    if (i == EndMinute)
                        li.Selected = true;
                    else
                        li.Selected = false;

                    EndMinuteDropDownList.Items.Add(li);
                }

                DropDownList EndAMPMDropDownList = (DropDownList)e.Row.FindControl("EndAMPMDropDownList");
                if (EndPM)
                    EndAMPMDropDownList.SelectedValue = "PM";
                else
                    EndAMPMDropDownList.SelectedValue = "AM";
            }
        }
    }
    protected void DaySelectionChanged(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.DataBind();
    }
    protected void GridviewRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow Row = GridView1.Rows[e.RowIndex];

        DropDownList StartHourDropDownList = (DropDownList)Row.FindControl("StartHourDropDownList");
        DropDownList StartMinuteDropDownList = (DropDownList)Row.FindControl("StartMinuteDropDownList");
        DropDownList StartAMPMDropDownList = (DropDownList)Row.FindControl("StartAMPMDropDownList");

        int StartHour = int.Parse(StartHourDropDownList.SelectedValue);
        int StartMinute = int.Parse(StartMinuteDropDownList.SelectedValue);
        bool StartPM = false;
        if (StartAMPMDropDownList.SelectedValue == "PM")
            StartPM = true;
        if (StartPM)
            if (StartHour != 12)
                StartHour += 12;

        Calendar EventDateCalendar = (Calendar)Row.FindControl("EventDateCalendar");
        DateTime Date = EventDateCalendar.SelectedDate;

        DateTime NewDateTime = new DateTime(Date.Year, Date.Month, Date.Day, StartHour, StartMinute, 0);

        DropDownList EndHourDropDownList = (DropDownList)Row.FindControl("EndHourDropDownList");
        DropDownList EndMinuteDropDownList = (DropDownList)Row.FindControl("EndMinuteDropDownList");
        DropDownList EndAMPMDropDownList = (DropDownList)Row.FindControl("EndAMPMDropDownList");

        int EndHour = int.Parse(EndHourDropDownList.SelectedValue);
        int EndMinute = int.Parse(EndMinuteDropDownList.SelectedValue);
        bool EndPM = false;
        if (EndAMPMDropDownList.SelectedValue == "PM")
            EndPM = true;
        if (EndPM)
            if (EndHour != 12)
                EndHour += 12;

        DateTime EndDateTime = new DateTime(Date.Year, Date.Month, Date.Day, EndHour, EndMinute, 0);

        if (EndHour < StartHour)
            EndDateTime = EndDateTime.AddDays(1.0);

        e.NewValues["DateandTime"] = NewDateTime;
        e.NewValues["EndTime"] = EndDateTime;
    }
    protected void EventUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        GridView1.DataBind();
    }
    protected void EventDeleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        GridView1.DataBind();
    }
    protected void VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        GridView1.EditIndex = -1;
        Calendar1.SelectedDate = new DateTime(1900, 1, 1, 1, 1, 1);
        GridView1.DataBind();
    }
}