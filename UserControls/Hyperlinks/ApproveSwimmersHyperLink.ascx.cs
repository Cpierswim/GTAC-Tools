using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_ApproveSwimmersHyperLink : System.Web.UI.UserControl
{
    public enum DisplayType { OfficeManager, Other }

    private DisplayType _displayType;

    public DisplayType DisplayAs
    {
        get { return this._displayType; }
        set
        {
            this._displayType = value;

            if (value == DisplayType.OfficeManager)
            {
                this.Visible = true;
                this.ApproveSwimmersHyperLink.NavigateUrl = "~/OfficeManager/ApproveSwimmers.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        if (SwimmersAdapter.GetSwimmersNotReadyToAdd().Count != 0)
            ApproveSwimmersHyperLink.BackColor = System.Drawing.Color.Yellow;
    }
    public UserControls_Hyperlinks_ApproveSwimmersHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}