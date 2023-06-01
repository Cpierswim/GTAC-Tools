using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_CorrectAttendanceDatabaseErrors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DeleteDuplcateAttendances(object sender, EventArgs e)
    {
        AttendanceBLL AttendanceAdapter = new AttendanceBLL();
        int deleted = AttendanceAdapter.DeleteDuplicateAttendances();

        InfoLabel.Text = "Deleted " + deleted + " attendances.<br /><br />";
    }
}