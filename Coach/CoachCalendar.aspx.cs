using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Coach_CoachCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EventsCalendar1.SetupTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
        EventsCalendar1.ClearGroups();
        for (int i = 0; i < GroupsListBox.Items.Count; i++)
            if (GroupsListBox.Items[i].Selected)
                EventsCalendar1.AddGroup(int.Parse(GroupsListBox.Items[i].Value));
    }
    protected void GroupsListBoxDataBound(object sender, EventArgs e)
    {
        GroupsListBox.Rows = GroupsListBox.Items.Count;
    }
    protected void DisplayCalendarButtonClicked(object sender, EventArgs e)
    {
        //for (int i = 0; i < GroupsListBox.Items.Count; i++)
        //    if (GroupsListBox.Items[i].Selected)
        //        EventsCalendar1.AddGroup(int.Parse(GroupsListBox.Items[i].Value));

        
    }
}