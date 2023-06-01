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
public class SchoolInfoBLL
{
    private SchoolInfosTableAdapter _schoolinfosAdapter = null;
    private SwimTeamDatabaseTableAdapters.AdvancedSchoolInfoTableAdapter _AdvancedAdapter = null;

    protected SchoolInfosTableAdapter Adapter
    {
        get
        {
            if (_schoolinfosAdapter == null)
                _schoolinfosAdapter = new SchoolInfosTableAdapter();

            return _schoolinfosAdapter;
        }
    }

    protected AdvancedSchoolInfoTableAdapter AdvancedAdapter
    {
        get
        {
            if (_AdvancedAdapter == null)
                _AdvancedAdapter = new AdvancedSchoolInfoTableAdapter();

            return _AdvancedAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SchoolInfosDataTable GetAllSchoolInfos()
    {
        return this.Adapter.GetAllSchoolInfos();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SchoolInfosDataTable GetSchoolInfoByUSAID(String USAID)
    {
        return this.Adapter.GetSchoolInfoByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SchoolInfosDataTable GetSchoolInfosBySchoolName(String SchoolName)
    {
        return this.Adapter.GetSchoolInfosBySchoolName(SchoolName);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SchoolInfosDataTable GetSchoolInfosByGrade(int Grade)
    {
        return this.Adapter.GetSchoolInfosByGrade(Grade);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool AddSchoolInfo(String USAID, String SchoolName, int Grade)
    {
        return this.Adapter.Insert(USAID, SchoolName, Grade) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.AdvancedSchoolInfoDataTable GetAllAdvancedSchoolInfos()
    {
        return this.AdvancedAdapter.GetAllAdvancedSchoolInfos();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AdvancedSchoolInfoDataTable GetAdvancedSchoolInfos(String SchoolName, String GroupName, String GradeValue)
    {
        int GradeID = int.Parse(GradeValue);
        SwimTeamDatabase.AdvancedSchoolInfoDataTable AdvancedTable = this.AdvancedAdapter.GetAllAdvancedSchoolInfos();

        if (SchoolName != "All Schools" || GroupName != "All Groups" || GradeID != -1)
            for (int i = 0; i < AdvancedTable.Count; i++)
            {
                if (SchoolName != "All Schools" && AdvancedTable[i].SchoolName != SchoolName)
                    AdvancedTable[i].Delete();
                if (AdvancedTable[i].RowState != DataRowState.Deleted)
                    if (GroupName != "All Groups" && AdvancedTable[i].GroupName != GroupName)
                        AdvancedTable[i].Delete();
                if (AdvancedTable[i].RowState != DataRowState.Deleted)
                    if (GradeID != -1 && AdvancedTable[i].Grade != GradeID)
                        AdvancedTable[i].Delete();
            }
        AdvancedTable.AcceptChanges();

        //for (int i = 0; i < AdvancedTable.Count; i++)
        //    if (AdvancedTable[i].RowState != DataRowState.Deleted)
                return AdvancedTable;

        //return new SwimTeamDatabase.AdvancedSchoolInfoDataTable();
    }
}