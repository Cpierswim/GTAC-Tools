using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SwimTeamDatabaseTableAdapters;

[System.ComponentModel.DataObject]
public class RecordsBLL
{
    private RecordsTableAdapter _recordsAdapter = null;
    protected RecordsTableAdapter Adapter
    {
        get
        {
            if (_recordsAdapter == null)
                _recordsAdapter = new RecordsTableAdapter();

            return _recordsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.RecordsDataTable GetRecordsForRecordsPage(byte MinAge, byte MaxAge, String Course, String Sex)
    {
        Course = Course.Trim().ToLower();
        Sex = Sex.Trim().ToLower();
        if (Course != "y" && Course != "l")
            throw new Exception("Wrong Course");
        if (Sex != "f" && Sex != "m")
            throw new Exception("Wrong Sex");

        return Adapter.GetRecordsForRecordPage(MaxAge, MinAge, Course, Sex);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.RecordsDataTable GetTopTensByDate(DateTime SinceDate)
    {
        return Adapter.GetTopTensByDate(SinceDate); ;
    }
}