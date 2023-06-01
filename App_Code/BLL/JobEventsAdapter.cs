using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for JobEventsAdapter
/// </summary>
 [System.ComponentModel.DataObject]
public class JobEventsAdapter
{
    private JobEventsTableAdapter _JobEventsAdapter = null;
    protected JobEventsTableAdapter Adapter
    {
        get
        {
            if (this._JobEventsAdapter == null)
                this._JobEventsAdapter = new JobEventsTableAdapter();
            return this._JobEventsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    protected SwimTeamDatabase.JobEventsDataTable GetAll()
    {
        return this.Adapter.GetAll();
    }
}