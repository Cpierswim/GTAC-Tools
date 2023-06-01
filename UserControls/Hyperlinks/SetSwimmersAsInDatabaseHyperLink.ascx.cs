using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_SetSwimmersAsInDatabaseHyperLink : System.Web.UI.UserControl
{
    public enum DisplayType { DatabaseManager, Other }

    private DisplayType _displayType;

    public DisplayType DisplayAs
    {
        get { return this._displayType; }
        set
        {
            this._displayType = value;

            if (value == DisplayType.DatabaseManager)
            {
                this.Visible = true;
                this.SetSwimmersAsInDatabaseHyperLink.NavigateUrl = "~/DatabaseManager/AddToDatabase.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        if (SwimmersAdapter.GetSwimmersReadyToAddButNotInDatabase().Count != 0)
            SetSwimmersAsInDatabaseHyperLink.BackColor = System.Drawing.Color.Yellow;
    }
    public UserControls_Hyperlinks_SetSwimmersAsInDatabaseHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}