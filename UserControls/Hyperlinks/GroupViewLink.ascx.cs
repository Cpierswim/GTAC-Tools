using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_GroupViewLink : System.Web.UI.UserControl
{
    public enum DisplayType { Coach, OfficeManager, DatabaseManager, Other }

    private DisplayType _displayType;

    public DisplayType DisplayAs
    {
        get { return this._displayType; }
        set
        {
            this._displayType = value;

            if (value == DisplayType.Coach)
            {
                this.Visible = true;
                this.GroupViewHyperLink.NavigateUrl = "~/DatabaseManager/GroupsView.aspx";
            }
            if (value == DisplayType.DatabaseManager)
            {
                this.Visible = true;
                this.GroupViewHyperLink.NavigateUrl = "~/DatabaseManager/GroupsView.aspx";
            }
            if (value == DisplayType.OfficeManager)
            {
                this.Visible = true;
                this.GroupViewHyperLink.NavigateUrl = "~/OfficeManager/GroupsView.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public UserControls_Hyperlinks_GroupViewLink() : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}