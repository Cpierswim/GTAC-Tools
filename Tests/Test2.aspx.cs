using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tests_Test2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SwimTeamDatabaseTableAdapters.AttendanceTableAdapter Adapter = new SwimTeamDatabaseTableAdapters.AttendanceTableAdapter();

        SwimTeamDatabase.AttendanceDataTable Table = Adapter.GetAllAttendance();

        foreach (SwimTeamDatabase.AttendanceRow Row in Table)
        {
            Row.Date = new DateTime(Row.Date.Year, Row.Date.Month, Row.Date.Day);
        }

        
        Adapter.BatchUpdateIgnoreDBConcurrency(Table, Table.Count);
    }
}