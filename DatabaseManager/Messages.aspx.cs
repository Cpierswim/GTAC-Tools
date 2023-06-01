using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DeleteMessageClicked(object sender, GridViewCommandEventArgs e)
    {
        int MessageID = int.Parse(GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());

        MessagesBLL MessageAdapter = new MessagesBLL();
        MessageAdapter.SetMessageAsSeenByDatabaseManager(MessageID);

        GridView1.DataBind();
    }
}