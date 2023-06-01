using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for JobTypesBLL
/// </summary>
 [System.ComponentModel.DataObject]
public class JobTypesBLL
{
    private JobTypesTableAdapter _JobTypesAdapter = null;
    protected JobTypesTableAdapter Adapter
    {
        get
        {
            if (this._JobTypesAdapter == null)
                this._JobTypesAdapter = new JobTypesTableAdapter();
            return this._JobTypesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.JobTypesDataTable GetAll()
    {
        return this.Adapter.GetAll();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public int Update(string Description, string Name, string TimeToLearn, string Length, int JobTypeID)
    {
        return this.Adapter.Update(Description, Name, TimeToLearn, Length, JobTypeID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public int Insert(string Description, string Name, string TimeToLearn, string Length)
    {
        return this.Adapter.Insert(Description, Name, TimeToLearn, Length);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public int Delete(int JobTypeID)
    {
        return this.Adapter.Delete(JobTypeID);
    }
}