using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_AddMeet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MeetCreated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null && ((bool)e.ReturnValue) == true)
        {
            SwimTeamDatabase.MeetsDataTable meets = new MeetsBLL().GetMostRecentlyCreatedMeet();
            if (meets.Count > 0)
            {
                int MeetID = meets[0].MeetID;
                Response.Redirect("~/DatabaseManager/AddSessions.aspx?MeetID=" + MeetID, true);
            }
            else
            {
                ErrorLabel.Text = "There was an error inserting the meet into the database.";
                ErrorLabel.Visible = true;
            }
        }
        else
        {
            ErrorLabel.Text = "There was an error inserting the meet into the database.";
            ErrorLabel.Visible = true;
        }
    }
    protected void DetailsViewInsertingMeet(object sender, DetailsViewInsertEventArgs e)
    {
        e.Values["Closed"] = false;
    }
}