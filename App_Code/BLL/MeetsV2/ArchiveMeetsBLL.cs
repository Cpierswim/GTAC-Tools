using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for ArchiveMeetsBLL
/// </summary>
public class ArchiveMeetsBLL
{
    private ArchiveMeetsTableAdapter _ArchiveMeetAdapter = null;
    protected ArchiveMeetsTableAdapter Adapter
    {
        get
        {
            if (this._ArchiveMeetAdapter == null)
                this._ArchiveMeetAdapter = new ArchiveMeetsTableAdapter();
            return this._ArchiveMeetAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ArchiveMeetsDataTable GetAllMeets()
    {
        return this.Adapter.GetAllArchiveMeets();
    }
}