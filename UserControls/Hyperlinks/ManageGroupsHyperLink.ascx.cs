using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_ManageGroupsHyperLink : System.Web.UI.UserControl
{
    public enum DisplayType { Administrator, Other }

    private DisplayType _displayType;

    public DisplayType DisplayAs
    {
        get { return this._displayType; }
        set
        {
            this._displayType = value;

            if (value == DisplayType.Administrator)
            {
                this.Visible = true;
                this.ManageGroupsHyperLink.NavigateUrl = "~/Admin/Groups.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public UserControls_Hyperlinks_ManageGroupsHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}