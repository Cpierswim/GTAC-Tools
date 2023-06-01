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
using System.Net.Mail;

[System.ComponentModel.DataObject]
public class SwimmersBLL
{
    public enum ReturnType { Active, Inactive, Both };

    private SwimmersTableAdapter _swimmersAdapter = null;
    protected SwimmersTableAdapter Adapter
    {
        get
        {
            if (_swimmersAdapter == null)
                _swimmersAdapter = new SwimmersTableAdapter();

            return _swimmersAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmers()
    {
        return Adapter.GetSwimmers();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersByGroupID(int GroupID)
    {
        return Adapter.GetSwimmersByGroupID(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersNotReadyToAdd()
    {
        return Adapter.GetSwimmersNotReadyToAdd();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersByFamilyID(int FamilyID)
    {
        return Adapter.GetSwimmersByFamilyID(FamilyID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmerByUSAID(string USAID)
    {
        USAID = USAID.Trim();

        return Adapter.GetSwimmerByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmerByInactive(ReturnType TypeToReturn)
    {
        if (TypeToReturn == ReturnType.Active)
            return Adapter.GetSwimmersByInactiveStatus(false);
        else if (TypeToReturn == ReturnType.Inactive)
            return Adapter.GetSwimmersByInactiveStatus(true);
        else if (TypeToReturn == ReturnType.Both)
            return Adapter.GetSwimmers();

        return null;
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetActiveSwimmers()
    {
        return GetSwimmerByInactive(ReturnType.Active);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersReadyToAddButNotInDatabase()
    {
        return Adapter.GetSwimmersByReadyToAddButNotInDatabase();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool InsertSwimmer(string USAID, int FamilyID, string LastName, string MiddleName, string FirstName, string PreferredName,
        DateTime Birthday, string Gender, bool ReadyToAdd, bool IsInDatabase, string PhoneNumber, string Email, string Notes,
        bool Inactive, int GroupID, string EthnicityCodes, bool USCitizen, string DisabilityCodes)
    {
        USAID = USAID.Trim();
        LastName = LastName.Trim();
        MiddleName = MiddleName.Trim();
        FirstName = FirstName.Trim();
        PreferredName = PreferredName.Trim();
        Gender = Gender.Trim();
        PhoneNumber = PhoneNumber.Trim();
        Email = Email.Trim();
        Notes = Notes.Trim();
        EthnicityCodes = EthnicityCodes.Trim();
        DisabilityCodes = DisabilityCodes.Trim();

        SwimTeamDatabase.SwimmersDataTable swimmers = new SwimTeamDatabase.SwimmersDataTable();
        SwimTeamDatabase.SwimmersRow swimmer = swimmers.NewSwimmersRow();

        swimmer.USAID = USAID;
        swimmer.FamilyID = FamilyID;
        swimmer.LastName = LastName;
        swimmer.MiddleName = MiddleName;
        swimmer.FirstName = FirstName;
        swimmer.PreferredName = PreferredName;
        swimmer.Birthday = Birthday;
        swimmer.Gender = Gender;
        swimmer.ReadyToAdd = ReadyToAdd;
        swimmer.IsInDatabase = IsInDatabase;
        swimmer.PhoneNumber = PhoneNumber;
        swimmer.Email = Email;
        swimmer.Notes = Notes;
        swimmer.Inactive = Inactive;
        swimmer.GroupID = GroupID;
        swimmer.Ethnicity = EthnicityCodes;
        swimmer.USCitizen = USCitizen;
        swimmer.Disability = DisabilityCodes;
        swimmer.Created = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

        swimmers.AddSwimmersRow(swimmer);
        int rows_affected = Adapter.Update(swimmers);

        if (rows_affected == 1)
        {
            SettingsBLL SettingsAdapter = new SettingsBLL();
            try
            {
                SettingsAdapter.SetContactInfoUpdatedTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time"));
            }
            catch (Exception)
            {
            }
            this.SendNotificationEmail(PreferredName, LastName);
        }

        return rows_affected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool InsertSwimmer(int FamilyID, string LastName, string MiddleName, string FirstName, string PreferredName,
        DateTime Birthday, string Gender, bool ReadyToAdd, bool IsInDatabase, string PhoneNumber, string Email, string Notes,
        bool Inactive, int GroupID, string EthnicityCodes, bool USCitizen, string DisabilityCodes)
    {
        LastName = LastName.Trim();
        MiddleName = MiddleName.Trim();
        FirstName = FirstName.Trim();
        PreferredName = PreferredName.Trim();
        Gender = Gender.Trim();
        PhoneNumber = PhoneNumber.Trim();
        Email = Email.Trim();
        Notes = Notes.Trim();
        EthnicityCodes = EthnicityCodes.Trim();
        DisabilityCodes = DisabilityCodes.Trim();
        string USAID = CreateUSAID(LastName, MiddleName, FirstName, Birthday);



        return this.InsertSwimmer(USAID, FamilyID, LastName, MiddleName, FirstName, PreferredName, Birthday, Gender, ReadyToAdd,
            IsInDatabase, PhoneNumber, Email, Notes, Inactive, GroupID, EthnicityCodes, USCitizen, DisabilityCodes);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetAsReadyToAdd(string USAID)
    {
        USAID = USAID.Trim();


        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(USAID);
        if (swimmers.Count != 1)
            return false;

        SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];
        swimmer.ReadyToAdd = true;

        int rows_affected = Adapter.Update(swimmer);

        return rows_affected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetAsInDatabase(string USAID)
    {
        USAID = USAID.Trim();

        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(USAID);
        if (swimmers.Count != 1)
            return false;

        SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];
        if (swimmer.ReadyToAdd)
            swimmer.IsInDatabase = true;
        else
            throw new Exception("Swimmer must be ready to add before setting the swimmer as in the database.");

        int rows_affected = Adapter.Update(swimmer);
        return rows_affected == 1;
    }

    //Currently, this does not need to notify that this is a swimmer change since group changes are not passed
    //to the hy-tek database
    //This will be updated in the future
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateGroup(string USAID, int GroupID)
    {
        USAID = USAID.Trim();


        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(USAID);
        SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];

        String Message = "Swimmer <b>" + swimmer.PreferredName + " " + swimmer.LastName + "</b> has been " +
            "switched from <b>";
        GroupsBLL GroupsAdapter = new GroupsBLL();
        Message += GroupsAdapter.GetGroupByGroupID(swimmer.GroupID)[0].GroupName;
        Message += "</b> to <b>";
        Message += GroupsAdapter.GetGroupByGroupID(GroupID)[0].GroupName + "</b>.";

        bool sucess = Adapter.UpdateGroup(GroupID, USAID) == 1;
        if (sucess)
        {
            this.ChangeGroup(swimmer.GroupID, GroupID, USAID);
            MessagesBLL MessageAdapter = new MessagesBLL();
            MessageAdapter.InsertMessage(Message);

            ChangesBLL ChangesAdapter = new ChangesBLL();
            //ChangesAdapter.AddChange(USAID, USAID, ChangesBLL.ChangeType.TEMPUNKNOWN);
            //this needs to be updated when group changes are added to the hy-tek updating program

            SettingsBLL SettingsAdapter = new SettingsBLL();
            SettingsAdapter.SetGroupUpdateAsPending();
            try
            {
                SettingsAdapter.SetContactInfoUpdatedTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time"));
            }
            catch (Exception)
            {
            }
        }
        return sucess;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateSwimmer(int GroupID, String LastName, String MiddleName, String FirstName, String PreferredName,
        DateTime Birthday, String Gender, String PhoneNumber, String Email, String Notes, bool Inactive,
        String Ethnicity, bool USCitizen, String Disability, String original_USAID)
    {
        if (LastName == null)
            LastName = string.Empty;
        if (MiddleName == null)
            MiddleName = string.Empty;
        if (FirstName == null)
            FirstName = string.Empty;
        if (PreferredName == null)
            PreferredName = string.Empty;
        if (Gender == null)
            Gender = string.Empty;
        if (PhoneNumber == null)
            PhoneNumber = string.Empty;
        if (Email == null)
            Email = string.Empty;
        if (Notes == null)
            Notes = string.Empty;
        if (Ethnicity == null)
            Ethnicity = string.Empty;
        if (Disability == null)
            Disability = string.Empty;

        original_USAID = original_USAID.Trim();
        LastName = LastName.Trim();
        MiddleName = MiddleName.Trim();
        FirstName = FirstName.Trim();
        PreferredName = PreferredName.Trim();
        Gender = Gender.Trim();
        PhoneNumber = PhoneNumber.Trim();
        Email = Email.Trim();
        Notes = Notes.Trim();
        Ethnicity = Ethnicity.Trim();
        Disability = Disability.Trim();


        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(original_USAID);
        if (swimmers.Count != 1)
            return false;
        SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];

        String New_USAID = CreateUSAID(LastName, MiddleName, FirstName, Birthday);

        //ChangesBLL.ChangeType ChangeType = ChangesBLL.ChangeType.NoChange;
        //if (swimmers.Count == 1)
        //    ChangeType = this.GetChangeType(swimmer, GroupID, LastName, MiddleName, FirstName, PreferredName, Birthday,
        //        Gender, PhoneNumber, Email, Notes, Inactive, Ethnicity, USCitizen, Disability, New_USAID);

        if (New_USAID != original_USAID)
            this.ChangeUSAID(original_USAID, New_USAID);
        if (GroupID != swimmer.GroupID)
            this.ChangeGroup(swimmer.GroupID, GroupID, New_USAID);

        String Message = SetMessageFromValues(swimmer, GroupID, LastName, MiddleName, FirstName,
            PreferredName, Birthday, Gender, PhoneNumber, Email, Notes, Ethnicity, USCitizen, Disability,
            original_USAID, Inactive);

        bool sucess = Adapter.UpdateSwimmer(New_USAID, swimmer.FamilyID, LastName, MiddleName, FirstName, PreferredName,
            Birthday.ToString("G"), Gender, swimmer.ReadyToAdd, false, PhoneNumber, Email, Notes,
            swimmer.Created, Inactive, GroupID, Ethnicity, USCitizen, Disability,
            original_USAID) == 1;
        if (sucess && !String.IsNullOrEmpty(Message))
        {
            MessagesBLL MessageAdapter = new MessagesBLL();
            MessageAdapter.InsertMessage(Message);
            SettingsBLL SettingsAdapter = new SettingsBLL();
            try
            {
                SettingsAdapter.SetContactInfoUpdatedTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time"));
            }
            catch (Exception)
            {
            }
        }

        if (sucess && original_USAID != New_USAID)
        {
            ChangesBLL ChangesAdapter = new ChangesBLL();
            ChangesAdapter.AddChange(original_USAID, New_USAID, ChangesBLL.ChangeType.ChangesThatNeedToBeSentToUSASwimming);
        }


        return sucess;
    }

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    //public bool UpdateSwimmer(int GroupID, String LastName, String MiddleName, String FirstName, String PreferredName,
    //    DateTime Birthday, String Gender, String PhoneNumber, String Email, String Notes, bool Inactive,
    //    String Ethnicity, bool USCitizen, String Disability, String original_USAID, bool ReadyToAdd)
    //{
    //    if (LastName == null)
    //        LastName = string.Empty;
    //    if (MiddleName == null)
    //        MiddleName = string.Empty;
    //    if (FirstName == null)
    //        FirstName = string.Empty;
    //    if (PreferredName == null)
    //        PreferredName = string.Empty;
    //    if (Gender == null)
    //        Gender = string.Empty;
    //    if (PhoneNumber == null)
    //        PhoneNumber = string.Empty;
    //    if (Email == null)
    //        Email = string.Empty;
    //    if (Notes == null)
    //        Notes = string.Empty;
    //    if (Ethnicity == null)
    //        Ethnicity = string.Empty;
    //    if (Disability == null)
    //        Disability = string.Empty;

    //    LastName = LastName.Trim();
    //    MiddleName = MiddleName.Trim();
    //    FirstName = FirstName.Trim();
    //    PreferredName = PreferredName.Trim();
    //    Gender = Gender.Trim();
    //    PhoneNumber = PhoneNumber.Trim();
    //    Email = Email.Trim();
    //    Notes = Notes.Trim();
    //    Ethnicity = Ethnicity.Trim();
    //    Disability = Disability.Trim();
    //    original_USAID = original_USAID.Trim();

    //    //TODO: Create Message to databasemanager
    //    SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(original_USAID);
    //    if (swimmers.Count != 1)
    //        return false;
    //    SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];

    //    String Message = SetMessageFromValues(swimmer, GroupID, LastName, MiddleName, FirstName,
    //        PreferredName, Birthday, Gender, PhoneNumber, Email, Notes, Ethnicity, USCitizen, Disability,
    //        original_USAID, Inactive);

    //    String New_USAID = CreateUSAID(LastName, MiddleName, FirstName, Birthday);
    //    if (New_USAID != original_USAID)
    //        this.ChangeUSAID(original_USAID, New_USAID);

    //    bool sucess = Adapter.UpdateSwimmer(New_USAID, swimmer.FamilyID, LastName, MiddleName, FirstName, PreferredName,
    //        Birthday.ToString("G"), Gender, ReadyToAdd, false, PhoneNumber, Email, Notes,
    //        swimmer.Created, Inactive, GroupID, Ethnicity, USCitizen, Disability,
    //        original_USAID) == 1;

    //    if (sucess && Message != "")
    //    {
    //        MessagesBLL MessageAdapter = new MessagesBLL();
    //        MessageAdapter.InsertMessage(Message);

    //        ChangesBLL ChangesAdapter = new ChangesBLL();
    //        ChangesAdapter.AddChange(original_USAID, New_USAID);
    //    }

    //    return sucess;
    //}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateSwimmer(int GroupID, String LastName, String MiddleName, String FirstName, String PreferredName,
        DateTime Birthday, String Gender, String PhoneNumber, String Email, String Notes, bool Inactive,
        String Ethnicity, bool USCitizen, String Disability, String original_USAID, bool ReadyToAdd, bool IsInDatabase)
    {
        if (LastName == null)
            LastName = string.Empty;
        if (MiddleName == null)
            MiddleName = string.Empty;
        if (FirstName == null)
            FirstName = string.Empty;
        if (PreferredName == null)
            PreferredName = string.Empty;
        if (Gender == null)
            Gender = string.Empty;
        if (PhoneNumber == null)
            PhoneNumber = string.Empty;
        if (Email == null)
            Email = string.Empty;
        if (Notes == null)
            Notes = string.Empty;
        if (Ethnicity == null)
            Ethnicity = string.Empty;
        if (Disability == null)
            Disability = string.Empty;

        LastName = LastName.Trim();
        MiddleName = MiddleName.Trim();
        FirstName = FirstName.Trim();
        PreferredName = PreferredName.Trim();
        Gender = Gender.Trim();
        PhoneNumber = PhoneNumber.Trim();
        Email = Email.Trim();
        Notes = Notes.Trim();
        Ethnicity = Ethnicity.Trim();
        Disability = Disability.Trim();
        original_USAID = original_USAID.Trim();


        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmerByUSAID(original_USAID);
        if (swimmers.Count != 1)
            return false;
        SwimTeamDatabase.SwimmersRow swimmer = swimmers[0];

        String Message = SetMessageFromValues(swimmer, GroupID, LastName, MiddleName,
            FirstName, PreferredName, Birthday, Gender, PhoneNumber, Email, Notes,
            Ethnicity, USCitizen, Disability, original_USAID, Inactive);

        String New_USAID = CreateUSAID(LastName, MiddleName, FirstName, Birthday);

        //ChangesBLL.ChangeType ChangeType = ChangesBLL.ChangeType.NoChange;
        //if (swimmers.Count == 1)
        //    ChangeType = this.GetChangeType(swimmer, GroupID, LastName, MiddleName, FirstName, PreferredName, Birthday,
        //        Gender, PhoneNumber, Email, Notes, Inactive, Ethnicity, USCitizen, Disability, New_USAID);

        if (New_USAID != original_USAID)
            this.ChangeUSAID(original_USAID, New_USAID);
        if (GroupID != swimmer.GroupID)
            this.ChangeGroup(swimmer.GroupID, GroupID, New_USAID);


        bool sucess = false;

        if (String.IsNullOrEmpty(Message) && swimmer.IsInDatabase)
            sucess = Adapter.UpdateSwimmer(New_USAID, swimmer.FamilyID, LastName, MiddleName, FirstName, PreferredName,
                Birthday.ToString("G"), Gender, ReadyToAdd, IsInDatabase, PhoneNumber, Email, Notes,
                swimmer.Created, Inactive, GroupID, Ethnicity, USCitizen, Disability,
                original_USAID) == 1; //This is the only update that doesn't fire off a message. The only change is that the
            //swimmer has been set to not in database. This will cause the computer program to think there is a change and
            //reset all the values. It should not matter since none of the values have actually been changed, so we will just let
            //this happen
        else
            sucess = Adapter.UpdateSwimmer(New_USAID, swimmer.FamilyID, LastName, MiddleName, FirstName, PreferredName,
            Birthday.ToString("G"), Gender, ReadyToAdd, false, PhoneNumber, Email, Notes,
            swimmer.Created, Inactive, GroupID, Ethnicity, USCitizen, Disability,
            original_USAID) == 1;

        if (sucess && !String.IsNullOrEmpty(Message))
        {
            MessagesBLL MessageAdapter = new MessagesBLL();
            MessageAdapter.InsertMessage(Message);

            SettingsBLL SettingsAdapter = new SettingsBLL();
            try
            {
                SettingsAdapter.SetContactInfoUpdatedTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time"));
            }
            catch (Exception)
            {
            }
        }

        if (sucess && original_USAID != New_USAID)
        {
            ChangesBLL ChangesAdapter = new ChangesBLL();
            ChangesAdapter.AddChange(original_USAID, New_USAID, ChangesBLL.ChangeType.ChangesThatNeedToBeSentToUSASwimming);
        }

        return sucess;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int DeleteSwimmersByFamilyID(int FamilyID)
    {
        int SwimmersDeleted = Adapter.DeleteSwimmerByFamilyID(FamilyID);

        if (SwimmersDeleted > 0)
        {
            EntryBLL EntriesAdapter = new EntryBLL();
            SwimTeamDatabase.SwimmersDataTable FamilySwimmers = Adapter.GetSwimmersByFamilyID(FamilyID);
            foreach (SwimTeamDatabase.SwimmersRow Swimmer in FamilySwimmers)
                EntriesAdapter.RemoveSwimmerFromAllMeets(Swimmer.USAID);
        }


        return SwimmersDeleted;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersWithNoFamily()
    {
        SwimTeamDatabase.SwimmersDataTable swimmers = Adapter.GetSwimmers();
        SwimTeamDatabase.FamiliesDataTable families = new FamiliesBLL().GetFamilies();

        SwimTeamDatabase.SwimmersDataTable unmatchedSwimmers = new SwimTeamDatabase.SwimmersDataTable();

        foreach (SwimTeamDatabase.SwimmersRow swimmer in swimmers)
        {
            bool SwimmerMatched = false;
            for (int i = 0; i < families.Count; i++)
                if (swimmer.FamilyID == families[i].FamilyID)
                {
                    SwimmerMatched = true;
                    i = families.Count;
                }

            if (!SwimmerMatched)
            {
                SwimTeamDatabase.SwimmersRow unmatchedswimmer = unmatchedSwimmers.NewSwimmersRow();

                unmatchedswimmer.Birthday = swimmer.Birthday;
                unmatchedswimmer.Created = swimmer.Created;
                unmatchedswimmer.Disability = swimmer.Disability;
                unmatchedswimmer.Email = swimmer.Email;
                unmatchedswimmer.Ethnicity = swimmer.Ethnicity;
                unmatchedswimmer.FamilyID = swimmer.FamilyID;
                unmatchedswimmer.FirstName = swimmer.FirstName;
                unmatchedswimmer.Gender = swimmer.Gender;
                unmatchedswimmer.GroupID = swimmer.GroupID;
                unmatchedswimmer.Inactive = swimmer.Inactive;
                unmatchedswimmer.IsInDatabase = swimmer.IsInDatabase;
                unmatchedswimmer.LastName = swimmer.LastName;
                unmatchedswimmer.MiddleName = swimmer.MiddleName;
                unmatchedswimmer.Notes = swimmer.Notes;
                unmatchedswimmer.PhoneNumber = swimmer.PhoneNumber;
                unmatchedswimmer.PreferredName = swimmer.PreferredName;
                unmatchedswimmer.ReadyToAdd = swimmer.ReadyToAdd;
                unmatchedswimmer.USAID = swimmer.USAID;
                unmatchedswimmer.USCitizen = swimmer.USCitizen;

                unmatchedSwimmers.AddSwimmersRow(unmatchedswimmer);
            }
        }

        return unmatchedSwimmers;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SwimmersDataTable GetSwimmersWithSystemGeneratedNotes()
    {
        return Adapter.GetSwimmersWithSystemGeneratedNotes();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public bool DeleteSwimmer(String USAID)
    {
        bool SwimmerDeleted = Adapter.DeleteSwimmer(USAID) == 1;

        if (SwimmerDeleted)
        {
            EntryBLL EntriesAdapter = new EntryBLL();
            EntriesAdapter.RemoveSwimmerFromAllMeets(USAID);
        }

        return SwimmerDeleted;
    }



    public static string CreateUSAID(string LastName, string MiddleName, string FirstName, DateTime Birthday)
    {
        string USAID = "";

        if (Birthday.Month < 10)
            USAID = USAID + "0";
        USAID = USAID + Birthday.Month;

        if (Birthday.Day < 10)
            USAID = USAID + "0";
        USAID = USAID + Birthday.Day;

        USAID = USAID + Birthday.Year.ToString().Substring(2, 2);

        string FirstNamePortion = RemoveNonCapitolizedLetters(FirstName.ToUpper());
        while (FirstNamePortion.Length < 3)
            FirstNamePortion += "*";
        FirstNamePortion = FirstNamePortion.Substring(0, 3);
        USAID = USAID + FirstNamePortion;


        string MiddleNamePortion = RemoveNonCapitolizedLetters(MiddleName.ToUpper());
        while (MiddleNamePortion.Length < 1)
            MiddleNamePortion += "*";
        MiddleNamePortion = MiddleNamePortion.Substring(0, 1);
        USAID += MiddleNamePortion;


        string LastNamePortion = RemoveNonCapitolizedLetters(LastName.ToUpper());
        while (LastNamePortion.Length < 4)
            LastNamePortion += "*";
        LastNamePortion = LastNamePortion.Substring(0, 4);
        USAID = USAID + LastNamePortion;
        return USAID;
    }
    private static String RemoveNonCapitolizedLetters(string StringToCheck)
    {
        char[] StringAsArray = StringToCheck.ToCharArray();
        string ReturnString = string.Empty;

        for (int i = 0; i < StringAsArray.Length; i++)
            if (IsLegalLetter(StringAsArray[i]))
                ReturnString += StringAsArray[i];

        return ReturnString;
    }
    private static bool IsLegalLetter(char CharToCheck)
    {
        switch (CharToCheck)
        {
            case 'A':
                return true;
            case 'B':
                return true;
            case 'C':
                return true;
            case 'D':
                return true;
            case 'E':
                return true;
            case 'F':
                return true;
            case 'G':
                return true;
            case 'H':
                return true;
            case 'I':
                return true;
            case 'J':
                return true;
            case 'K':
                return true;
            case 'L':
                return true;
            case 'M':
                return true;
            case 'N':
                return true;
            case 'O':
                return true;
            case 'P':
                return true;
            case 'Q':
                return true;
            case 'R':
                return true;
            case 'S':
                return true;
            case 'T':
                return true;
            case 'U':
                return true;
            case 'V':
                return true;
            case 'W':
                return true;
            case 'X':
                return true;
            case 'Y':
                return true;
            case 'Z':
                return true;
            default:
                return false;
        }
    }

    private String SetMessageFromValues(SwimTeamDatabase.SwimmersRow OriginalSwimmer,
        int GroupID, String LastName, String MiddleName, String FirstName, String PreferredName,
        DateTime Birthday, String Gender, String PhoneNumber, String Email, String Notes,
        String Ethnicity, bool USCitizen, String Disability, String original_USAID, bool Inactive)
    {
        bool informationChanged = false;
        String Message = "";
        if (OriginalSwimmer.LastName != LastName || OriginalSwimmer.PreferredName != PreferredName)
            Message = "Swimmer Updated. Name before change <b>" + OriginalSwimmer.PreferredName +
            " " + OriginalSwimmer.LastName + "</b>. The following information has been changed:<br />";
        else
            Message = "Swimmer Updated. The following information for <b>" + OriginalSwimmer.PreferredName +
                " " + OriginalSwimmer.LastName + "</b> has been changed<br />";

        if (OriginalSwimmer.LastName != LastName)
        {
            informationChanged = true;
            Message += "<br />Last Name Changed from <b>" + OriginalSwimmer.LastName + "</b> to <b>" + LastName + "</b>";
        }
        if (OriginalSwimmer.PreferredName != PreferredName)
        {
            informationChanged = true;
            Message += "<br />Preferred first name changed from <b>" + OriginalSwimmer.PreferredName
                + "</b> to <b>" + PreferredName + "</b>";
        }
        if (OriginalSwimmer.FirstName != FirstName)
        {
            informationChanged = true;
            Message += "<br />Legal first name changed from <b>" + OriginalSwimmer.FirstName + "</b> to <b>" +
                FirstName + "</b>";
        }
        if (OriginalSwimmer.MiddleName != MiddleName)
        {
            informationChanged = true;
            Message += "<br />Middle Name changed from <b>" + OriginalSwimmer.MiddleName + "</b> to <b>" +
                MiddleName + "</b>";
        }
        if (!OriginalSwimmer.Birthday.Equals(Birthday))
        {
            informationChanged = true;
            Message += "<br />Birthday changded from <b>" + OriginalSwimmer.Birthday.ToString("d") + "</b> to <b>" +
                Birthday.ToString("d") + "</b>";
        }
        if (OriginalSwimmer.Gender != Gender)
        {
            informationChanged = true;
            bool WasMale = (OriginalSwimmer.Gender == "M");
            if (WasMale)
                Message += "<br />Gender changed from <b>Male</b> to <b>Female</b>";
            else
                Message += "<br />Gender changed from <b>Female</b> to <b>Male</b>";
        }
        if (OriginalSwimmer.PhoneNumber != PhoneNumber)
        {
            informationChanged = true;
            Message += "<br />Athlete phone number changed from <b>" + OriginalSwimmer.PhoneNumber + "</b> to <b>" +
                PhoneNumber + "</b>";
        }
        if (OriginalSwimmer.Email != Email)
        {
            informationChanged = true;
            Message += "<br />Athlete email changed from <b>" + OriginalSwimmer.Email + "</b> to <b>" +
                Email + "</b>";
        }
        if (OriginalSwimmer.USCitizen != USCitizen)
        {
            informationChanged = true;
            Message += "<br />Athlete US citizenship changed from <b>" + OriginalSwimmer.USCitizen + "</b> to <b>" +
                USCitizen + "</b>";
        }
        if (OriginalSwimmer.GroupID != GroupID)
        {
            informationChanged = true;
            Message += "<br />Athlete Group changed from <b>";
            GroupsBLL GroupsAdapter = new GroupsBLL();
            Message += GroupsAdapter.GetGroupByGroupID(OriginalSwimmer.GroupID)[0].GroupName;
            Message += "</b> to <b>";
            Message += GroupsAdapter.GetGroupByGroupID(GroupID)[0].GroupName + "</b>";
            SettingsBLL SettingsAdapter = new SettingsBLL();
            SettingsAdapter.SetGroupUpdateAsPending();
        }

        if (OriginalSwimmer.Ethnicity != Ethnicity)
        {
            Message += "<br />Ethnicity Codes changed from <b>" + OriginalSwimmer.Ethnicity + "</b> to <b>" +
                Ethnicity + "</b> <i>(Check a USA Swimming Registration form to refer to what the Ethnicity" +
                " codes mean)</i>";
            informationChanged = true;
        }
        if (OriginalSwimmer.Disability != Disability)
        {
            informationChanged = true;
            Message += "<br />Disability Codes changed from <b>" + OriginalSwimmer.Disability + "</b> to <b>" +
                Disability + "</b> <i>(Check a USA Swimming Registration form to refer to what the " +
                "Disability codes mean)</i>";
        }
        if (OriginalSwimmer.Inactive != Inactive)
        {
            informationChanged = true;
            if (OriginalSwimmer.Inactive == true)
                Message += "<br />Swimmer has been changed from <b>INACTIVE</b> to <b>ACTIVE</b>";
            else
                Message += "<br />Swimmer has been changed from <b>ACTIVE</b> to <b>INACTIVE</b>";
        }
        if (OriginalSwimmer.Notes != Notes)
        {
            informationChanged = true;
            Message += "<br />Notes about swimmer changed from <b>" + OriginalSwimmer.Notes + "</b> to <b>" + Notes + "</b>";
        }

        if (!informationChanged)
            return "";
        return Message;
    }

    public int NumberOfActiveSwimmers()
    {
        return int.Parse(Adapter.NumberOfActiveSwimmers().ToString());
    }

    public int NumberCreatedSinceRegistrationDate()
    {
        return int.Parse(Adapter.NumberCreatedSinceRegistrationDate().ToString());
    }

    private void SendNotificationEmail(String PreferredName, String LastName)
    {
        PreferredName = PreferredName.Trim();
        LastName = LastName.Trim();

        SettingsBLL SettingsAdapter = new SettingsBLL();
        SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetNewSwimmerNotificationEmail();

        if (settings.Count != 0)
        {
            String Email = settings[0].SettingValue;

            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    //MailAddress FromAddress = new MailAddress("cpierson@sev.org");
                    //MailAddress ToAddress = new MailAddress(Email);
                    //MailAddress SecondToAddress = new MailAddress("4193442424@vtext.com");

                    //MailMessage Notification = new MailMessage(FromAddress, ToAddress);
                    //Notification.To.Add(SecondToAddress);

                    //Notification.Subject = "New Swimmer";
                    //Notification.Body = "New Swimmer #" + this.TotalNumberOfSwimmersInDatabase() + ", " +
                    //    PreferredName + " " + LastName + " Added. ";

                    //SmtpClient Client = new SmtpClient();
                    //Client.Send(Notification);

                    Emailer mail = new Emailer();
                    mail.To.Add(Email);
                    mail.To.Add("4193442424@vtext.com");

                    mail.Subject = "New Swimmer";
                    mail.Message = "New Swimmer #" + this.TotalNumberOfSwimmersInDatabase() + ", " +
                        PreferredName + " " + LastName + " Added. ";
                    mail.Send();
                }
                catch (Exception)
                {
                    //It's not a big deal if the mail message doesn't send. So if it doesn't work, don't bother
                    //doing anything.
                }
            }
        }
    }

    public int TotalNumberOfSwimmersInDatabase()
    {
        return Adapter.TotalNumberOfSwimmersInDatabase() ?? 0;
    }



    private void ChangeUSAID(String OldUSAID, String NewUSAID)
    {
        EntryBLL EntriesBLL = new EntryBLL();
        EntriesBLL.UpdateUSAID(OldUSAID, NewUSAID);

        AttendanceBLL AttendanceAdapter = new AttendanceBLL();
        AttendanceAdapter.ChangeUSAID(NewUSAID, OldUSAID);

        new SwimTeamDatabaseTableAdapters.ArchiveAttendanceTableAdapter().UpdateUSAID(NewUSAID, OldUSAID);
        new SwimTeamDatabaseTableAdapters.JobSignUpsTableAdapter().UpdateUSAID(NewUSAID, OldUSAID);
        new SwimTeamDatabaseTableAdapters.PreEnteredV2TableAdapter().UpdateUSAID(NewUSAID, OldUSAID);
        new SwimTeamDatabaseTableAdapters.SchoolInfosTableAdapter().UpdateUSAID(NewUSAID, OldUSAID);
        new SwimTeamDatabaseTableAdapters.SwimmerAthleteJoinTableAdapter().UpdateUSAID(NewUSAID, OldUSAID);
    }


    public int ParentInformationChanged(int FamilyID, ChangesBLL.ChangeType ChangeType)
    {
        SwimTeamDatabase.SwimmersDataTable Swimmers = Adapter.GetSwimmersByFamilyID(FamilyID);
        ChangesBLL ChangesAdapter = new ChangesBLL();

        int rowsaffected = 0;
        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
        {
            if (Swimmer.ReadyToAdd)
            {
                Swimmer.IsInDatabase = false;

                rowsaffected += Adapter.Update(Swimmer);
            }

            ChangesAdapter.AddChange(Swimmer.USAID, Swimmer.USAID, ChangeType);
        }


        return rowsaffected;
    }

    private ChangesBLL.ChangeType GetChangeType(SwimTeamDatabase.SwimmersRow SwimmerToCheck,
        int GroupID, String LastName, String MiddleName, String FirstName, String PreferredName,
        DateTime Birthday, String Gender, String PhoneNumber, String Email, String Notes, bool Inactive,
        String Ethnicity, bool USCitizen, String Disability, String NewUSAID)
    {
        if (SwimmerToCheck.LastName != LastName ||
            SwimmerToCheck.MiddleName != MiddleName ||
            SwimmerToCheck.FirstName != FirstName ||
            SwimmerToCheck.Birthday != Birthday ||
            SwimmerToCheck.Gender != Gender ||
            SwimmerToCheck.Ethnicity != Ethnicity ||
            SwimmerToCheck.USCitizen != USCitizen ||
            SwimmerToCheck.Disability != Disability ||
            SwimmerToCheck.USAID != NewUSAID)
            return ChangesBLL.ChangeType.ChangesThatNeedToBeSentToUSASwimming;
        else if (
            SwimmerToCheck.PhoneNumber != PhoneNumber ||
            SwimmerToCheck.Email != Email ||
            SwimmerToCheck.Notes != Notes ||
            SwimmerToCheck.Inactive != Inactive)
            return ChangesBLL.ChangeType.NonImportantChange;
        else
            return ChangesBLL.ChangeType.NoChange;
    }

    private void ChangeGroup(int Original_Group, int New_Group, String USAID)
    {
        AttendanceBLL AttendanceAdapter = new AttendanceBLL();
        ArchiveAttendanceBLL ArchiveAttendanceAdapter = new ArchiveAttendanceBLL();

        SwimTeamDatabase.AttendanceDataTable AttendancesForUSAID = AttendanceAdapter.GetAttendancesByUSAID(USAID);
        if (AttendancesForUSAID.Count > 0)
        {
            ArchiveAttendanceAdapter.BeginBatchInsert();
            foreach (SwimTeamDatabase.AttendanceRow Row in AttendancesForUSAID)
            {
                int? Lane = null, Yards = null, Meters = null;
                String Note = null;
                if (!Row.IsLaneNull())
                    Lane = Row.Lane;
                if (!Row.IsYardsNull())
                    Yards = Row.Yards;
                if (!Row.IsMetersNull())
                    Meters = Row.Meters;
                if(!Row.IsNoteNull())
                    Note = Row.Note;

                ArchiveAttendanceAdapter.BatchInsert(Row.USAID, Row.Date, Row.PracticeoftheDay, Row.GroupID, Row.AttendanceType,
                    Note, Lane, Yards, Meters);
            }

            ArchiveAttendanceAdapter.CommitBatchInserts();
            ArchiveAttendanceAdapter.EndBatchInsert();

            AttendanceAdapter.DeleteByUSAID(USAID);
        }
    }
}