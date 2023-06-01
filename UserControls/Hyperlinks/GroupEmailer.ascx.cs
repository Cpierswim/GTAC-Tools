using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_GroupEmailer : System.Web.UI.UserControl
{
    public enum DisplayType { Coach, Other }

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
                this.GroupEmailerHyperLink.NavigateUrl = "~/Coach/EmailGroup.aspx";
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public UserControls_Hyperlinks_GroupEmailer()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}