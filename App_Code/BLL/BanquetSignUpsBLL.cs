using System;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for BanquetSignUpsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class BanquetSignUpsBLL
{
    private BanquentSignUpsTableAdapter _signupsAdapter = null;
    protected BanquentSignUpsTableAdapter Adapter
    {
        get
        {
            if (this._signupsAdapter == null)
                this._signupsAdapter = new BanquentSignUpsTableAdapter();
            return this._signupsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BanquentSignUpsDataTable GetAllBanquetSignUps()
    {
        return Adapter.GetAllBanquetSignUps();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.BanquentSignUpsDataTable GetBanquetSignUpByFamilyID(int FamilyID)
    {
        return Adapter.GetBanquestSignUpByFamilyID(FamilyID);
    }

    public bool IsFamilySignedUpForBanquet(int FamilyID)
    {
        return (Adapter.GetBanquestSignUpByFamilyID(FamilyID).Count > 0);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool SignUpFamilyForBanquet(int FamilyID, int adults, int children)
    {
        if (!this.IsFamilySignedUpForBanquet(FamilyID))
            return Adapter.SignFamilyUpForBanquet(FamilyID, adults, children) == 1;
        else
            return false;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool UpdateFamilySignUp(int FamilyID, int adults, int children)
    {
        if (Adapter.UpdateFamilyBanquetSignUpInfo(adults, children, FamilyID) == 1)
        {
            FamiliesBLL FamiliesAdapter = new FamiliesBLL();
            String FamilyLastName = FamiliesAdapter.GetFamilyLastName(FamilyID);
            MessagesBLL MessagesAdapter = new MessagesBLL();
            String Message = "The " + FamilyLastName + " family has updated their banquet sign-up to " + adults;
            if (adults != 1)
                Message += " adults and " + children;
            else
                Message += " adult and " + children;
            if (children != 1)
                Message += " children.";
            else
                Message += " child.";
            MessagesAdapter.InsertMessage(Message);
            return true;
        }
        else
            return false;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteFamilySignUp(int FamilyID)
    {
        if (Adapter.DeleteFamilyBanquetSignUp(FamilyID) == 1)
        {
            FamiliesBLL FamiliesAdapter = new FamiliesBLL();
            String FamilyLastName = FamiliesAdapter.GetFamilyLastName(FamilyID);
            MessagesBLL MessagesAdapter = new MessagesBLL();
            String Message = "The " + FamilyLastName + " family has withdrawn from the banquet.";
            MessagesAdapter.InsertMessage(Message);
            return true;
        }
        else
            return false;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteFamilySignUpNoMessage(int FamilyID)
    {
        return Adapter.DeleteFamilyBanquetSignUp(FamilyID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public void DeleteAllBanquetSignUps()
    {
        Adapter.DeleteAllBanquetSignUps();
    }
}