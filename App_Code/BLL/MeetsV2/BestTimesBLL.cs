using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for BestTimes
/// </summary>
[System.ComponentModel.DataObject]
public class BestTimesBLL
{
    private SwimTeamDatabaseTableAdapters.BestTimesTableAdapter _Adapter;
    private SwimTeamDatabaseTableAdapters.BestTimesTableAdapter Adapter
    {
        get
        {
            if (this._Adapter == null)
                this._Adapter = new BestTimesTableAdapter();
            return this._Adapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BestTimesDataTable GetAllBestTimes()
    {
        return this.Adapter.GetAllBestTimes();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BestTimesDataTable GetBestTimesByGroupID(int GroupID)
    {
        return this.Adapter.GetByGroupID(GroupID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BestTimesDataTable GetBestTimesByFamilyID(int FamilyID)
    {
        return this.Adapter.GetBestTimesByFamilyID(FamilyID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BestTimesDataTable GetBestTimesByUSAID(String USAID)
    {
        return this.Adapter.GetBestTimesByUSAID(USAID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BestTimesDataTable GetBestTimesByGroupIDOrderedForDisplay(int GroupID, String Course)
    {
        return this.Adapter.GetByGroupIDOrderedForDisplay(GroupID, Course);
    }
}