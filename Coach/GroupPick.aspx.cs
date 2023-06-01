using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Coach_GroupPick : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GroupSelected(object sender, EventArgs e)
    {
        Profile.GroupID = DropDownList1.SelectedValue;
        if (Request.QueryString["RP"] != null)
            Response.Redirect("~/Features/Default.aspx");
        Response.Redirect("~/Coach/GroupsView.aspx");
    }
}