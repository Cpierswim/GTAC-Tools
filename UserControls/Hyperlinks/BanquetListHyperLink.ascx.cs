using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_BanquetListHyperLink : System.Web.UI.UserControl
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
                this.ApproveSwimmersHyperLink.NavigateUrl = "~/OfficeManager/BanquetList.aspx";
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SettingsBLL SettingsAdapter = new SettingsBLL();

        //this.Visible = SettingsAdapter.DisplayBanquetButton();
        this.Visible = true;
    }
    public UserControls_Hyperlinks_BanquetListHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}