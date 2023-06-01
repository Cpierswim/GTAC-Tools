using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_DatabaseManagerHyperLink : System.Web.UI.UserControl
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
                this.DatabaseManagerHyperLink.NavigateUrl = "~/DatabaseManager/SpecialDatabaseManagerPage.aspx";
            }

        }
    }

    public UserControls_Hyperlinks_DatabaseManagerHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}