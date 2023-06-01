using System;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for ChangesBLL
/// </summary>
[System.ComponentModel.DataObject]
public class ChangesBLL
{
    public enum ChangeType { Renew, ChangesThatNeedToBeSentToUSASwimming, NonImportantChange, NoChange }

    private ChangesTableAdapter _changesAdapter = null;
    protected ChangesTableAdapter Adapter
    {
        get
        {
            if (this._changesAdapter == null)
                this._changesAdapter = new ChangesTableAdapter();
            return this._changesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.ChangesDataTable GetAllChanges()
    {
        return Adapter.GetAllChanges();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool AddChange(String OldUSAID, String NewUSAID, ChangesBLL.ChangeType ChangeType)
    {
        if (!this.IsChangeAlreadyInDatabase(OldUSAID, NewUSAID))
        {
            if (ChangeType == ChangesBLL.ChangeType.ChangesThatNeedToBeSentToUSASwimming)
                return Adapter.AddChange(OldUSAID, NewUSAID, "C") == 1;
            //if (ChangeType == ChangesBLL.ChangeType.NonImportantChange)
            //    return Adapter.AddChange(OldUSAID, NewUSAID, "X") == 1;
            //if (ChangeType == ChangesBLL.ChangeType.Renew)
            //    return Adapter.AddChange(OldUSAID, NewUSAID, "R") == 1;
        }
        return false;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteChange(int ChangeID)
    {
        return Adapter.Delete(ChangeID) == 1;
    }

    private bool IsChangeAlreadyInDatabase(String OldUSAID, String NewUSAID)
    {
        SwimTeamDatabase.ChangesDataTable Changes = Adapter.GetChangeByInfo(OldUSAID, NewUSAID);
        if (Changes.Count > 0)
            return true;
        return false;
    }
}