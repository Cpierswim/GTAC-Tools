using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_ListOfSwimmersHyperlink : System.Web.UI.UserControl
{
    public enum DisplayType { OfficeManager, DatabaseManager, Other }

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
                this.ListOfSwimmersHyperLink.NavigateUrl = "~/DatabaseManager/Swimmers.aspx";
            }
            if (value == DisplayType.OfficeManager)
            {
                this.Visible = true;
                this.ListOfSwimmersHyperLink.NavigateUrl = "~/OfficeManager/Swimmers.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public UserControls_Hyperlinks_ListOfSwimmersHyperlink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}
