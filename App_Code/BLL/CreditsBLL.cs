using System;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for CreditsBLL
/// </summary>
[System.ComponentModel.DataObject]
public class CreditsBLL
{
    private MeetCreditsTableAdapter _creditsAdapter = null;
    protected MeetCreditsTableAdapter Adapter
    {
        get
        {
            if (this._creditsAdapter == null)
                this._creditsAdapter = new MeetCreditsTableAdapter();
            return this._creditsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MeetCreditsDataTable GetAllCredits()
    {
        return Adapter.GetAllCredits();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.MeetCreditsDataTable GetCreditsByFamilyID(int FamilyID)
    {
        return Adapter.GetCreditsByFamilyID(FamilyID);
    }

    public void CreateCreditAccountForFamily(int FamilyID)
    {
        Adapter.CreateCreditAccount(FamilyID, 0);
    }

    public int AddCreditToFamily(int FamilyID)
    {
        SwimTeamDatabase.MeetCreditsDataTable credits = Adapter.GetCreditsByFamilyID(FamilyID);
        if (credits.Count != 1)
            throw new Exception("Incorrect number of Meet Credits.");
        int NumberofCredits = credits[0].NumberOfCredits;

        NumberofCredits++;

        return Adapter.SetMeetCreditsForFamily(NumberofCredits, FamilyID);
    }

    public int SubtractCreditFromFamily(int FamilyID)
    {
        SwimTeamDatabase.MeetCreditsDataTable credits = Adapter.GetCreditsByFamilyID(FamilyID);
        if (credits.Count != 1)
            throw new Exception("Incorrect number of Meet Credits.");
        int NumberofCredits = credits[0].NumberOfCredits;

        NumberofCredits--;

        if (NumberofCredits >= 0)
            return Adapter.SetMeetCreditsForFamily(NumberofCredits, FamilyID);
        else
            return 0;
    }

    public int DeleteCreditAccount(int FamilyID)
    {
        int NumberOfCreditsDeleted = 0;
        
        SwimTeamDatabase.MeetCreditsDataTable credits = Adapter.GetCreditsByFamilyID(FamilyID);
        foreach (SwimTeamDatabase.MeetCreditsRow credit in credits)
            NumberOfCreditsDeleted += Adapter.DeleteMeetCreditAccount(credit.MeetCreditID);

        return NumberOfCreditsDeleted;
    }

    public int CreateNewAccountForEachFamilyThatDoesNotHaveOne()
    {
        int RowsAffected = 0;
        FamiliesBLL FamiliesAdapter = new FamiliesBLL();
        SwimTeamDatabase.FamiliesDataTable families = FamiliesAdapter.GetFamilies();
        SwimTeamDatabase.MeetCreditsDataTable credits = Adapter.GetAllCredits();
        

        foreach (SwimTeamDatabase.FamiliesRow family in families)
        {
            bool HasCredit = false;
            for (int i = 0; i < credits.Count; i++)
            {
                if (credits[i].FamilyID == family.FamilyID)
                {
                    HasCredit = true;
                    i = credits.Count;
                }
            }

            if (!HasCredit)
            {
                RowsAffected += Adapter.CreateCreditAccount(family.FamilyID, 0);
            }
        }

        return RowsAffected;
    }

    public bool AddCreditForSwimmersFamily(String USAID)
    {
        SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmerByUSAID(USAID);

        if (Swimmers.Count == 1)
            return this.AddCreditToFamily(Swimmers[0].FamilyID) == 1;

        return false;
    }
}