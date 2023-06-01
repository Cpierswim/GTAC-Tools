using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for JobSignUpsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class JobSignUpsBLL
{
    private JobSignUpsTableAdapter _JobSignUpsAdapter = null;
    protected JobSignUpsTableAdapter Adapter
    {
        get
        {
            if (this._JobSignUpsAdapter == null)
                this._JobSignUpsAdapter = new JobSignUpsTableAdapter();
            return this._JobSignUpsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobSignUpsDataTable GetAll()
    {
        return this.Adapter.GetAll();
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobSignUpsDataTable GetByJobEventID(int JobEventID)
    {
        return this.Adapter.GetJobSignUpsByJobEventID(JobEventID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int Update(SwimTeamDatabase.JobSignUpsDataTable Table)
    {
        return this.Adapter.Update(Table);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int Delete(int JobSignUpID)
    {
        return this.Adapter.DeleteJobSignUp(JobSignUpID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int BlankOutJobSignUp(int JobSignUpID)
    {
        return this.Adapter.BlankOutJobSignup(JobSignUpID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int DeleteByEventID(int JobEventID)
    {
        return this.Adapter.DeleteByJobEventID(JobEventID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobSignUpsDataTable GetEmptySignupsByEventID(int EventID)
    {
        return this.Adapter.GetEmptySignupsByJobEventID(EventID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int SignupFamily(int JobSignupID, int FamilyID)
    {
        return this.Adapter.UpdateJobSignup(FamilyID, null, null, null, JobSignupID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int SignupSwimmer(int JobSignupID, int FamilyID, String USAID)
    {
        return this.Adapter.UpdateJobSignup(FamilyID, USAID, null, null, JobSignupID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int SignupParent(int JobSignupID, int FamilyID, int ParentID)
    {
        return this.Adapter.UpdateJobSignup(FamilyID, null, ParentID, null, JobSignupID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public int SignupOther(int JobSignupID, int FamilyID, String Other)
    {
        return this.Adapter.UpdateJobSignup(FamilyID, null, null, Other.Trim(), JobSignupID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.JobSignUpsDataTable GetByEmptySignupsOrFamilyID(int JobEventID, int? FamilyID)
    {
        if (FamilyID.HasValue)
            return this.Adapter.GetEmptyJobSignupsOrByFamilyID(JobEventID, FamilyID.Value);
        else
            return this.GetEmptySignupsByEventID(JobEventID);
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public int GetNumberOfJobEventsWithOpenings()
    {
        return this.Adapter.NumberOfJobEventsWithOpenings() ?? 0;
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public bool AnyOpenings()
    {
        return this.GetNumberOfJobEventsWithOpenings() > 0;
    }
}