using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        object o = Session["GroupsString"];
        if (o != null)
        {
            String GroupsString = o.ToString();

            EventsCalendar.SetupTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

            String[] GroupsStrings = GroupsString.Split(',');
            for (int i = 0; i < GroupsStrings.Length; i++)
            {
                int GroupID = -1;
                if (int.TryParse(GroupsStrings[i], out GroupID))
                    EventsCalendar.AddGroup(GroupID);
            }
        }
        else
        {
            String FamilyIDString = Profile.FamilyID;
            if (String.IsNullOrEmpty(FamilyIDString))
                Response.Redirect("~/Parents/FamilyView.aspx?Error=5", true);
            else
            {
                int FamilyID = int.Parse(FamilyIDString);
                SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmersByFamilyID(FamilyID);
                foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
                    EventsCalendar.AddGroup(Swimmer.GroupID);
            }
        }

        o = Session["USAIDList"];
        if (o != null)
        {
            List<string> USAIDList = (List<String>)o;
            foreach (String USAID in USAIDList)
            {
                EventsCalendar.AddSwimmer(USAID);
            }
        }
        else
        {
            String FamilyIDString = Profile.FamilyID;
            if (String.IsNullOrEmpty(FamilyIDString))
                Response.Redirect("~/Parents/FamilyView.aspx?Error=5", true);
            else
            {
                int FamilyID = int.Parse(FamilyIDString);
                SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmersByFamilyID(FamilyID);
                foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
                    EventsCalendar.AddSwimmer(Swimmer.USAID);
            }
        }
    }
}