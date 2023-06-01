using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_AttendanceHyperLink : System.Web.UI.UserControl
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
                this.AttendanceHyperLink.NavigateUrl = "~/Coach/Attendance.aspx";
            }
        }
    }

    public UserControls_Hyperlinks_AttendanceHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}