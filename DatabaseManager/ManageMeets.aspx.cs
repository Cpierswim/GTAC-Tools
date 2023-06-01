using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_ManageMeets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MeetDeleted(object sender, DetailsViewDeletedEventArgs e)
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataBind();
    }
    protected void MeetUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        GridView1.SelectedIndex = -1;
        GridView1.DataBind();
    }
}