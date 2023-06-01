using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for GroupCoachesBLL
/// </summary>
public class GroupCoachesBLL
{
    private GroupCoachTableAdapter _GroupCoachesTableAdapter = null;
    protected GroupCoachTableAdapter Adapter
    {
        get
        {
            if (this._GroupCoachesTableAdapter == null)
                this._GroupCoachesTableAdapter = new GroupCoachTableAdapter();
            return this._GroupCoachesTableAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.GroupCoachDataTable GetAllGroupCoaches()
    {
        return this.Adapter.GetAllGroupCoaches();
    }
}