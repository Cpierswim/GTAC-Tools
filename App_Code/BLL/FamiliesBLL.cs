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
using System.Collections.Generic;
using System.Collections;

[System.ComponentModel.DataObject]
public class FamiliesBLL
{
    private FamiliesTableAdapter _familiesAdapter = null;
    protected FamiliesTableAdapter Adapter
    {
        get
        {
            if (_familiesAdapter == null)
                _familiesAdapter = new FamiliesTableAdapter();

            return _familiesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FamiliesDataTable GetFamilies()
    {
        return Adapter.GetFamilies();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FamiliesDataTable GetFamiliesAlphabeticalOnlyThoseWithParents()
    {
        SwimTeamDatabase.FamiliesDataTable families = Adapter.GetFamilies();
        SwimTeamDatabase.FamiliesDataTable AlphabeticalFamilies = new SwimTeamDatabase.FamiliesDataTable();
        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParents();

        List<SwimTeamDatabase.FamiliesRow> FamiliesList = new List<SwimTeamDatabase.FamiliesRow>();
        List<String> NameList = new List<String>();
        SortedList<String, SwimTeamDatabase.FamiliesRow> AlphaList = new SortedList<string, SwimTeamDatabase.FamiliesRow>();

        for (int j = 0; j < families.Count; j++)
        {
            SwimTeamDatabase.FamiliesRow family = families[j];

            SwimTeamDatabase.ParentsRow primaryparent = null;
            for (int i = 0; i < parents.Count; i++)
                if ((family.FamilyID == parents[i].FamilyID) && (parents[i].PrimaryContact))
                {
                    primaryparent = parents[i];
                    i = parents.Count;
                }

            SwimTeamDatabase.FamiliesRow TempFamily = families.NewFamiliesRow();
            TempFamily.FamilyID = family.FamilyID;
            TempFamily.UserID = family.UserID;

            FamiliesList.Add(TempFamily);
            if (primaryparent != null)
                if (!AlphaList.ContainsKey((primaryparent.LastName + ", " + primaryparent.FirstName + " ID:" + j)))
                    AlphaList.Add((primaryparent.LastName + ", " + primaryparent.FirstName + " ID:" + j), TempFamily);
        }

        for (int i = 0; i < AlphaList.Values.Count; i++)
        {
            SwimTeamDatabase.FamiliesRow TempFamily = AlphabeticalFamilies.NewFamiliesRow();
            TempFamily.FamilyID = AlphaList.Values[i].FamilyID;
            TempFamily.UserID = AlphaList.Values[i].UserID;
            AlphabeticalFamilies.AddFamiliesRow(TempFamily);
        }

        return AlphabeticalFamilies;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.FamiliesDataTable GetFamilyID(Guid UserID)
    {
        return Adapter.GetFamiliesByUserID(UserID);
    }

    public int GetNewFamilyID(Guid UserID)
    {
        SwimTeamDatabase.FamiliesDataTable tempfamilydatatable = Adapter.TempGetDataByUserID(UserID);
        int familyID = tempfamilydatatable[0].FamilyID;
        return familyID;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool CreateFamily(Guid UserID)
    {
        SwimTeamDatabase.FamiliesDataTable families = new SwimTeamDatabase.FamiliesDataTable();
        SwimTeamDatabase.FamiliesRow family = families.NewFamiliesRow();

        family.UserID = UserID;

        families.AddFamiliesRow(family);
        int rows_affected = Adapter.Update(families);

        int NewFamilyID = Adapter.GetFamiliesByUserID(UserID)[0].FamilyID;

        new CreditsBLL().CreateCreditAccountForFamily(NewFamilyID);

        families.Dispose();

        return rows_affected == 1;
    }

    public int DeleteFamily(int FamilyID)
    {
        new SwimmersBLL().DeleteSwimmersByFamilyID(FamilyID);
        new ParentsBLL().DeleteParentsByFamilyID(FamilyID);
        new CreditsBLL().DeleteCreditAccount(FamilyID);
        return Adapter.DeleteFamily(FamilyID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FamiliesDataTable GetFamiliesWithNoSwimmers()
    {
        SwimTeamDatabase.FamiliesDataTable FamiliesToIterate = Adapter.GetFamilies();
        SwimTeamDatabase.SwimmersDataTable swimmers = new SwimmersBLL().GetSwimmers();

        SwimTeamDatabase.FamiliesDataTable emptyfamilies = new SwimTeamDatabase.FamiliesDataTable();

        foreach (SwimTeamDatabase.FamiliesRow family in FamiliesToIterate)
        {
            bool HasSwimmer = false;
            for (int i = 0; i < swimmers.Count; i++)
            {
                if (swimmers[i].FamilyID == family.FamilyID)
                {
                    HasSwimmer = true;
                    i = swimmers.Count;
                }
            }

            if (!HasSwimmer)
            {
                SwimTeamDatabase.FamiliesRow EmptyFamily = emptyfamilies.NewFamiliesRow();
                EmptyFamily.FamilyID = family.FamilyID;
                EmptyFamily.UserID = family.UserID;

                SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParentsByFamilyID(family.FamilyID);

                if (parents.Count != 0)
                    emptyfamilies.AddFamiliesRow(EmptyFamily);
            }
        }

        return emptyfamilies;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FamiliesDataTable GetFamiliesWithNoParents()
    {
        SwimTeamDatabase.FamiliesDataTable FamiliesToIterate = Adapter.GetFamilies();
        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParents();

        SwimTeamDatabase.FamiliesDataTable emptyFamilies = new SwimTeamDatabase.FamiliesDataTable();

        foreach (SwimTeamDatabase.FamiliesRow family in FamiliesToIterate)
        {
            bool hasparent = false;
            for (int i = 0; i < parents.Count; i++)
                if (parents[i].FamilyID == family.FamilyID)
                {
                    hasparent = true;
                    i = parents.Count;
                }

            if (!hasparent)
            {
                SwimTeamDatabase.FamiliesRow EmptyFamily = emptyFamilies.NewFamiliesRow();
                EmptyFamily.FamilyID = family.FamilyID;
                EmptyFamily.UserID = family.UserID;

                if (GetUserAccountForFamily(family.FamilyID) != null)
                    emptyFamilies.AddFamiliesRow(EmptyFamily);
            }
        }

        return emptyFamilies;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.FamiliesDataTable GetFamiliesWithTooManyParents()
    {
        SwimTeamDatabase.FamiliesDataTable FamiliesToIterate = Adapter.GetFamilies();
        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParents();

        SwimTeamDatabase.FamiliesDataTable emptyFamilies = new SwimTeamDatabase.FamiliesDataTable();

        foreach (SwimTeamDatabase.FamiliesRow family in FamiliesToIterate)
        {
            int numberofparents = 0;
            for (int i = 0; i < parents.Count; i++)
                if (parents[i].FamilyID == family.FamilyID)
                    numberofparents++;

            if (numberofparents > 2)
            {
                SwimTeamDatabase.FamiliesRow EmptyFamily = emptyFamilies.NewFamiliesRow();
                EmptyFamily.FamilyID = family.FamilyID;
                EmptyFamily.UserID = family.UserID;

                emptyFamilies.AddFamiliesRow(EmptyFamily);
            }
        }

        return emptyFamilies;
    }

    public List<String> GetDistinctListOfAllEmailsForFamily(int FamilyID)
    {
        List<string> EmailList = new List<string>();

        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParentsByFamilyID(FamilyID);
        SwimTeamDatabase.SwimmersDataTable swimmers = new SwimmersBLL().GetSwimmersByFamilyID(FamilyID);

        foreach (SwimTeamDatabase.ParentsRow parent in parents)
            if (!String.IsNullOrEmpty(parent.Email))
            {
                String lowerCaseEmail = parent.Email.ToLower();
                if (!EmailList.Contains(lowerCaseEmail))
                    EmailList.Add(lowerCaseEmail);
            }

        foreach (SwimTeamDatabase.SwimmersRow swimmer in swimmers)
            if (!String.IsNullOrEmpty(swimmer.Email))
            {
                String lowerCaseEmail = swimmer.Email.ToLower();
                if (!EmailList.Contains(lowerCaseEmail))
                    EmailList.Add(lowerCaseEmail);
            }

        return EmailList;
    }

    public List<String> GetDistinctListOfParentsEmailsForFamily(int FamilyID)
    {
        List<string> EmailList = new List<string>();

        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParentsByFamilyID(FamilyID);
        foreach (SwimTeamDatabase.ParentsRow parent in parents)
            if (!String.IsNullOrEmpty(parent.Email))
            {
                String lowerCaseEmail = parent.Email.ToLower();
                if (!EmailList.Contains(lowerCaseEmail))
                    EmailList.Add(lowerCaseEmail);
            }

        return EmailList;
    }

    public List<String> GetUsernamesWithoutFamilies()
    {
        List<String> UsernameList = new List<string>();
        SwimTeamDatabase.FamiliesDataTable families = Adapter.GetFamilies();

        MembershipUserCollection Users = Membership.GetAllUsers();
        foreach (MembershipUser user in Users)
        {
            bool FamilyFound = false;
            for (int i = 0; i < families.Count; i++)
                if (families[i].UserID == ((Guid)user.ProviderUserKey))
                {
                    FamilyFound = true;
                    i = families.Count;
                }

            if (!FamilyFound)
                UsernameList.Add(user.UserName);
        }

        return UsernameList;
    }

    public SwimTeamDatabase.FamiliesDataTable GetFamiliesWithoutAccounts()
    {
        SwimTeamDatabase.FamiliesDataTable families = Adapter.GetFamilies();
        MembershipUserCollection users = Membership.GetAllUsers();
        List<MembershipUser> userslist = new List<MembershipUser>();
        foreach (MembershipUser user in users)
            userslist.Add(user);

        SwimTeamDatabase.FamiliesDataTable emptyfamilies = new SwimTeamDatabase.FamiliesDataTable();

        foreach (SwimTeamDatabase.FamiliesRow family in families)
        {
            bool AccountFound = false;
            for (int i = 0; i < users.Count; i++)
            {
                if (family.UserID == ((Guid)userslist[i].ProviderUserKey))
                {
                    AccountFound = true;
                    i = userslist.Count;
                }
            }

            if (!AccountFound)
            {
                SwimTeamDatabase.FamiliesRow tempFamily = emptyfamilies.NewFamiliesRow();
                tempFamily.FamilyID = family.FamilyID;
                tempFamily.UserID = family.UserID;

                emptyfamilies.AddFamiliesRow(tempFamily);
            }
        }

        return emptyfamilies;
    }

    public MembershipUser GetUserAccountForFamily(int FamilyID)
    {
        MembershipUser user = null;

        SwimTeamDatabase.FamiliesDataTable families = Adapter.GetFamilyByFamilyID(FamilyID);
        if (families.Count > 1)
            throw new Exception("Too Many Families");

        Guid UserID = families[0].UserID;

        user = Membership.GetUser(UserID);

        return user;
    }

    public String GetFamilyLastName(int FamilyID)
    {
        ParentsBLL ParentsAdapter = new ParentsBLL();
        SwimTeamDatabase.ParentsDataTable parents = ParentsAdapter .GetParentsByFamilyID(FamilyID);
        String LastNameString = "";
        for(int i = 0; i < parents.Count; i++)
        {
            if(i == 0)
                LastNameString = parents[i].LastName;
            if (i == 1)
                if (!LastNameString.Contains(parents[i].LastName))
                    LastNameString = LastNameString + "/" + parents[i].LastName;
        }

            return LastNameString;
    }
}