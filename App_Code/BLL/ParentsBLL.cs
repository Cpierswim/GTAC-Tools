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

[System.ComponentModel.DataObject]
public class ParentsBLL
{
    private ParentsTableAdapter _parentsAdapter = null;
    protected ParentsTableAdapter Adapter
    {
        get
        {
            if (_parentsAdapter == null)
                _parentsAdapter = new ParentsTableAdapter();

            return _parentsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ParentsDataTable GetParents()
    {
        return Adapter.GetParents();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.ParentsDataTable GetParentsByFamilyID(int FamilyID)
    {
        return Adapter.GetParentsByFamilyID(FamilyID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ParentsDataTable GetPrimaryContactParentByFamilyID(int FamilyID)
    {
        return Adapter.GetPrimaryContactParentByFamilyID(FamilyID, true);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ParentsDataTable GetSecondaryContactParentByFamilyID(int FamilyID)
    {
        return Adapter.GetPrimaryContactParentByFamilyID(FamilyID, false);
    }

    //Original InsertParent method - does not use the InsertParent method of the DAL, but rather inserts the row directly
    //Also an awkward coding for inserting the Geocode with the matching parent ID
    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    //public bool InsertParent(string FirstName, string LastName, string AddressLineOne, string AddressLineTwo,
    //   string City, string State, string Zip, string HomePhone, string CellPhone,
    //    string WorkPhone, string Notes, string Email, bool PrimaryContact, int FamilyID)
    //{
    //    SwimTeamDatabase.ParentsDataTable parents = new SwimTeamDatabase.ParentsDataTable();
    //    SwimTeamDatabase.ParentsRow parent = parents.NewParentsRow();

    //    FirstName = FirstName.Trim();
    //    LastName = LastName.Trim();
    //    AddressLineOne = AddressLineOne.Trim();
    //    AddressLineTwo = AddressLineTwo.Trim();
    //    City = City.Trim();
    //    State = State.Trim();
    //    Zip = Zip.Trim();
    //    HomePhone = HomePhone.Trim();
    //    CellPhone = CellPhone.Trim();
    //    WorkPhone = WorkPhone.Trim();
    //    Notes = Notes.Trim();
    //    Email = Email.Trim();

    //    Address ParentAddress = new Address(AddressLineOne, AddressLineTwo, City, State, Zip);
    //    ParentAddress.SetFromGoogle();

    //    if (ParentAddress.Status == Address.StatusType.VALID)
    //    {
    //        AddressLineOne = ParentAddress.AddressLineOne;
    //        City = ParentAddress.City;
    //        State = ParentAddress.State;
    //        Zip = ParentAddress.Zip;
    //    }

    //    parent.FirstName = FirstName;
    //    parent.LastName = LastName;
    //    parent.AddressLineOne = AddressLineOne;
    //    parent.AddressLineTwo = AddressLineTwo;
    //    parent.City = City;
    //    parent.State = State;
    //    parent.Zip = Zip;
    //    parent.HomePhone = HomePhone;
    //    parent.CellPhone = CellPhone;
    //    parent.WorkPhone = WorkPhone;
    //    parent.Notes = Notes;
    //    parent.Email = Email;
    //    parent.PrimaryContact = PrimaryContact;
    //    parent.FamilyID = FamilyID;



    //    parents.AddParentsRow(parent);
    //    int rows_affected = Adapter.Update(parents);

    //    parents = Adapter.GetParentsByFamilyID(FamilyID);
    //    foreach (SwimTeamDatabase.ParentsRow parenttocheck in parents)
    //    {
    //        if (parenttocheck.PrimaryContact == PrimaryContact)
    //        {
    //            int ParentID = parenttocheck.ParentID;

    //            SwimTeamDatabaseTableAdapters.ParentsGeocodesTableAdapter GeocodeAdapter = new ParentsGeocodesTableAdapter();
    //            GeocodeAdapter.Insert(ParentID, ParentAddress.Latitude, ParentAddress.Longitude);
    //        }
    //    }

    //    return rows_affected == 1;
    //}
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, true)]
    public bool InsertParent(string FirstName, string LastName, string AddressLineOne, string AddressLineTwo,
       string City, string State, string Zip, string HomePhone, string CellPhone,
        string WorkPhone, string Notes, string Email, bool PrimaryContact, int FamilyID)
    {
        FirstName = FirstName.Trim();
        LastName = LastName.Trim();
        AddressLineOne = AddressLineOne.Trim();
        AddressLineTwo = AddressLineTwo.Trim();
        City = City.Trim();
        State = State.Trim();
        Zip = Zip.Trim();
        HomePhone = HomePhone.Trim();
        CellPhone = CellPhone.Trim();
        WorkPhone = WorkPhone.Trim();
        Notes = Notes.Trim();
        Email = Email.Trim();

        Address ParentAddress = new Address(AddressLineOne, AddressLineTwo, City, State, Zip);
        ParentAddress.SetFromGoogle();

        if (ParentAddress.Status == Address.StatusType.VALID)
        {
            AddressLineOne = ParentAddress.AddressLineOne;
            City = ParentAddress.City;
            State = ParentAddress.State;
            Zip = ParentAddress.Zip;
        }

        int rows_affected = Adapter.InsertParent(FamilyID, LastName, FirstName, AddressLineOne, AddressLineTwo,
            City, State, Zip, HomePhone, CellPhone, WorkPhone, Email, Notes, PrimaryContact);


        if (rows_affected > 1)
            throw new Exception("Database corrupted - multiple parents inserted. Error code: 0X142635");

        if (rows_affected == 0)
            throw new Exception("Parent not added. Error code: 0x9516216");

        int GeocodesInserted = 0;
        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParentsByFamilyID(FamilyID);
        SwimTeamDatabaseTableAdapters.ParentsGeocodesTableAdapter GeocodeAdapter = new ParentsGeocodesTableAdapter();
        foreach (SwimTeamDatabase.ParentsRow parenttocheck in parents)
            if (parenttocheck.PrimaryContact == PrimaryContact)
                GeocodesInserted += GeocodeAdapter.Insert(parenttocheck.ParentID, ParentAddress.Latitude, ParentAddress.Longitude);

        if (GeocodesInserted != 1)
            throw new Exception("Database corrupted - Error inserting Geocode to sucessful parent. Error code: 0x8833729");

        GeocodeAdapter.Dispose();

        return rows_affected == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateParent(String LastName, String FirstName, String AddressLineOne, String AddressLineTwo,
        String City, String State, String Zip, String HomePhone, String CellPhone, String WorkPhone, String Email, String Notes,
        bool PrimaryContact, int original_ParentID)
    {
        if (AddressLineTwo == null)
            AddressLineTwo = string.Empty;
        if (CellPhone == null)
            CellPhone = string.Empty;
        if (WorkPhone == null)
            WorkPhone = string.Empty;
        if (Email == null)
            Email = string.Empty;
        if (Notes == null)
            Notes = string.Empty;

        FirstName = FirstName.Trim();
        LastName = LastName.Trim();
        AddressLineOne = AddressLineOne.Trim();
        AddressLineTwo = AddressLineTwo.Trim();
        City = City.Trim();
        State = State.Trim();
        Zip = Zip.Trim();
        HomePhone = HomePhone.Trim();
        CellPhone = CellPhone.Trim();
        WorkPhone = WorkPhone.Trim();
        Notes = Notes.Trim();
        Email = Email.Trim();

        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParentByParentID(original_ParentID);
        if (parents.Count != 1)
            return false;



        SwimTeamDatabase.ParentsRow parent = parents[0];
        bool AddressChanged = false;
        if ((parent.AddressLineOne != AddressLineOne) ||
            (parent.AddressLineTwo != AddressLineTwo) ||
            (parent.City != City) ||
            (parent.State != State) ||
            (parent.Zip != Zip))
            AddressChanged = true;


        Address ParentAddress = new Address();
        if (AddressChanged)
        {
            ParentAddress = new Address(AddressLineOne, AddressLineTwo, City, State, Zip);
            ParentAddress.SetFromGoogle();

            if (ParentAddress.Status == Address.StatusType.VALID)
            {
                AddressLineOne = ParentAddress.AddressLineOne;
                City = ParentAddress.City;
                State = ParentAddress.State;
                Zip = ParentAddress.Zip;
            }
        }

        String Message = SetMessageFromData(parent, LastName, FirstName, AddressLineOne, AddressLineTwo,
            City, State, Zip, HomePhone, CellPhone, WorkPhone, Email, Notes);

        bool success = Adapter.UpdateParent(parent.FamilyID, LastName, FirstName, AddressLineOne, AddressLineTwo, City, State, Zip,
            HomePhone, CellPhone, WorkPhone, Email, Notes, PrimaryContact, original_ParentID) == 1;
        if (success && !String.IsNullOrEmpty(Message))
        {
            MessagesBLL MessageAdapter = new MessagesBLL();
            MessageAdapter.InsertMessage(Message);
        }

        if (success && AddressChanged && ParentAddress.Status == Address.StatusType.VALID)
        {
            ParentsGeocodesTableAdapter GeocodeAdapter = new ParentsGeocodesTableAdapter();
            GeocodeAdapter.Update(ParentAddress.Latitude, ParentAddress.Longitude, original_ParentID);
        }

        if (success)
        {
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            if (parent.LastName != LastName ||
                parent.FirstName != FirstName ||
                AddressChanged ||
                parent.HomePhone != HomePhone ||
                parent.CellPhone != CellPhone ||
                parent.WorkPhone != WorkPhone ||
                parent.Email != Email)
                SwimmersAdapter.ParentInformationChanged(parent.FamilyID, ChangesBLL.ChangeType.NonImportantChange);
            else
                SwimmersAdapter.ParentInformationChanged(parent.FamilyID, ChangesBLL.ChangeType.NonImportantChange);
            SettingsBLL SettingsAdapter = new SettingsBLL();
            try
            {
                SettingsAdapter.SetContactInfoUpdatedTime(TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time"));
            }
            catch (Exception)
            {
            }
        }

        return success;
    }

    public bool SwitchPrimaryAndSecondaryParent(int FamilyID)
    {

        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParentsByFamilyID(FamilyID);
        if (parents.Count != 2)
            return false;

        SwimTeamDatabase.ParentsRow parent1 = parents[0];
        SwimTeamDatabase.ParentsRow parent2 = parents[1];

        String Message = "Primary and Secondary Parents switched. <br /><br /><b>" +
         parent2.FirstName + " " + parent2.LastName + "</b> is now the Primary Contact Parent and <b>" +
         parent1.FirstName + " " + parent1.LastName + "</b> is now the Secondary Contact Parent.";


        bool IntermediatePrimary = parent1.PrimaryContact;
        parent1.PrimaryContact = parent2.PrimaryContact;
        parent2.PrimaryContact = IntermediatePrimary;

        bool parent1_changed = (Adapter.Update(parent1) == 1);
        bool parent2_changed = (Adapter.Update(parent2) == 1);
        bool sucess = (parent1_changed == true) && (parent2_changed == true);

        if (sucess)
        {
            MessagesBLL MessageAdapter = new MessagesBLL();
            MessageAdapter.InsertMessage(Message);
        }
        return sucess;
    }

    private String SetMessageFromData(SwimTeamDatabase.ParentsRow OriginalParent, String LastName, String FirstName, String AddressLineOne, String AddressLineTwo,
        String City, String State, String Zip, String HomePhone, String CellPhone, String WorkPhone, String Email, String Notes)
    {
        bool InformationChanged = false;
        String Message = "";
        if (OriginalParent.FirstName != FirstName || OriginalParent.LastName != LastName)
            Message = "Parent Updated. Name Before Change: " +
            OriginalParent.FirstName + " " + OriginalParent.LastName +
            " The following information has been changed:<br />";
        else
            Message = "Parent Updated. The following information for <b>" + OriginalParent.FirstName + " " + OriginalParent.LastName +
                "</b> has been changed:<br />";

        if (OriginalParent.LastName != LastName)
        {
            InformationChanged = true;
            Message += "<br />Last Name Changed from <b>" + OriginalParent.LastName + "</b> to <b>" + LastName + "</b>";
        }
        if (OriginalParent.FirstName != FirstName)
        {
            InformationChanged = true;
            Message += "<br />First Name Changed from <b>" + OriginalParent.FirstName + "</b> to <b>" + FirstName + "</b>";
        }
        if (OriginalParent.AddressLineOne != AddressLineOne)
        {
            InformationChanged = true;
            Message += "<br />Address Line One Changed from <b>" + OriginalParent.AddressLineOne + "</b> to <b>"
                + AddressLineOne + "</b>";
        }
        if (OriginalParent.AddressLineTwo != AddressLineTwo)
        {
            InformationChanged = true;
            Message += "<br />Address Line Two Changed from <b>" + OriginalParent.AddressLineTwo + "</b> to <b>"
                + AddressLineTwo + "</b>";
        }
        if (OriginalParent.City != City)
        {
            InformationChanged = true;
            Message += "<br />City changed from <b>" + OriginalParent.City + "</b> to <b>" + City + "</b>";
        }
        if (OriginalParent.State != State)
        {
            InformationChanged = true;
            Message += "<br />State changed from <b>" + OriginalParent.State + "</b> to <b>" + State + "</b>";
        }
        if (OriginalParent.Zip != Zip)
        {
            InformationChanged = true;
            Message += "<br />Zip changed from <b>" + OriginalParent.Zip + "</b> to <b>" + Zip + "</b>";
        }
        if (OriginalParent.HomePhone != HomePhone)
        {
            InformationChanged = true;
            Message += "<br />Home phone changed from <b>" + OriginalParent.HomePhone + "</b> to <b>" + HomePhone + "</b>";
        }
        if (OriginalParent.CellPhone != CellPhone)
        {
            InformationChanged = true;
            Message += "<br />Cell phone changed from <b>" + OriginalParent.CellPhone + "</b> to <b>" + CellPhone + "</b>";
        }
        if (OriginalParent.WorkPhone != WorkPhone)
        {
            InformationChanged = true;
            Message += "<br />Work phone changed from <b>" + OriginalParent.WorkPhone + "</b> to <b>" + WorkPhone + "</b>";
        }
        if (OriginalParent.Email != Email)
        {
            InformationChanged = true;
            Message += "<br />Email changed from <b>" + OriginalParent.Email + "</b> to <b>" + Email + "</b>";
        }
        if (OriginalParent.Notes != Notes)
        {
            InformationChanged = true;
            Message += "<br />Parent notes changed from <b>" + OriginalParent.Notes + "</b> to <b>" + Notes + "</b>";
        }

        if (!InformationChanged)
            return "";

        return Message;

    }

    public int DeleteParentsByFamilyID(int FamilyID)
    {
        int NumberOfParentsDeleted = 0;
        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParentsByFamilyID(FamilyID);
        ParentsGeocodesTableAdapter parentsGeocodeAdapter = new ParentsGeocodesTableAdapter();
        foreach (SwimTeamDatabase.ParentsRow parent in parents)
        {
            parentsGeocodeAdapter.DeleteParentsGeocodeByParentID(parent.ParentID);
            NumberOfParentsDeleted += Adapter.DeleteParent(parent.ParentID);
        }

        parentsGeocodeAdapter.Dispose();
        return NumberOfParentsDeleted;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ParentsDataTable GetParentsWithNoFamilies()
    {
        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParents();
        SwimTeamDatabase.FamiliesDataTable families = new FamiliesBLL().GetFamilies();

        SwimTeamDatabase.ParentsDataTable unmatchedParents = new SwimTeamDatabase.ParentsDataTable();

        foreach (SwimTeamDatabase.ParentsRow parent in parents)
        {
            bool hasFamily = false;
            for (int i = 0; i < families.Count; i++)
                if (families[i].FamilyID == parent.FamilyID)
                {
                    hasFamily = true;
                    i = families.Count;
                }
            if (!hasFamily)
            {

                SwimTeamDatabase.ParentsRow unmatchedParent = unmatchedParents.NewParentsRow();

                unmatchedParent.AddressLineOne = parent.AddressLineOne;
                unmatchedParent.AddressLineTwo = parent.AddressLineTwo;
                unmatchedParent.CellPhone = parent.CellPhone;
                unmatchedParent.City = parent.City;
                unmatchedParent.Email = parent.Email;
                unmatchedParent.FamilyID = parent.FamilyID;
                unmatchedParent.FirstName = parent.FirstName;
                unmatchedParent.HomePhone = parent.HomePhone;
                unmatchedParent.LastName = parent.LastName;
                unmatchedParent.Notes = parent.Notes;
                unmatchedParent.ParentID = parent.ParentID;
                unmatchedParent.PrimaryContact = parent.PrimaryContact;
                unmatchedParent.State = parent.State;
                unmatchedParent.WorkPhone = parent.WorkPhone;
                unmatchedParent.Zip = parent.Zip;

                unmatchedParents.AddParentsRow(unmatchedParent);
            }
        }

        return unmatchedParents;
    }

    public int DeleteOrphanedParentGeocodes()
    {
        int numberDeleted = 0;

        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParents();
        ParentsGeocodesTableAdapter GeocodeAdapter = new ParentsGeocodesTableAdapter();
        SwimTeamDatabase.ParentsGeocodesDataTable parentsgeocodes = GeocodeAdapter.GetAllParentGeocodes();

        foreach (SwimTeamDatabase.ParentsGeocodesRow geocode in parentsgeocodes)
        {
            bool MatchFound = false;
            for (int i = 0; i < parents.Count; i++)
                if (geocode.ParentID == parents[i].ParentID)
                {
                    MatchFound = true;
                    i = parents.Count;
                }

            if (!MatchFound)
                numberDeleted += GeocodeAdapter.DeleteParentsGeocodeByParentID(geocode.ParentID);


        }

        GeocodeAdapter.Dispose();

        return numberDeleted;
    }

    public SwimTeamDatabase.ParentsDataTable GetParentsWithNoGeocode()
    {
        SwimTeamDatabase.ParentsDataTable parents = Adapter.GetParents();
        SwimTeamDatabase.ParentsGeocodesDataTable parentsgeocodes = new ParentsGeocodesTableAdapter().GetAllParentGeocodes();

        SwimTeamDatabase.ParentsDataTable ReturnTable = new SwimTeamDatabase.ParentsDataTable();

        foreach (SwimTeamDatabase.ParentsRow parent in parents)
        {
            bool MatchFound = false;
            for (int i = 0; i < parentsgeocodes.Count; i++)
                if (parent.ParentID == parentsgeocodes[i].ParentID)
                {
                    MatchFound = true;
                    i = parentsgeocodes.Count;
                }
            if (!MatchFound)
            {
                SwimTeamDatabase.ParentsRow TempParent = ReturnTable.NewParentsRow();
                TempParent.AddressLineOne = parent.AddressLineOne;
                TempParent.AddressLineTwo = parent.AddressLineTwo;
                TempParent.CellPhone = parent.CellPhone;
                TempParent.City = parent.City;
                TempParent.Email = parent.Email;
                TempParent.FamilyID = parent.FamilyID;
                TempParent.FirstName = parent.FirstName;
                TempParent.HomePhone = parent.HomePhone;
                TempParent.LastName = parent.LastName;
                TempParent.Notes = parent.Notes;
                TempParent.ParentID = parent.ParentID;
                TempParent.PrimaryContact = parent.PrimaryContact;
                TempParent.State = parent.State;
                TempParent.WorkPhone = parent.WorkPhone;
                TempParent.Zip = parent.Zip;
                ReturnTable.AddParentsRow(TempParent);
            }
        }

        parentsgeocodes.Dispose();

        return ReturnTable;
    }

    public int CreateGeocodesForParentsThatDoNotAlreadyHaveOne()
    {
        int NumberCreated = 0;

        SwimTeamDatabase.ParentsDataTable parents = this.GetParentsWithNoGeocode();
        SwimTeamDatabaseTableAdapters.ParentsGeocodesTableAdapter GeocodeAdapter = new ParentsGeocodesTableAdapter();

        foreach (SwimTeamDatabase.ParentsRow parent in parents)
        {
            Address ParentAddress = new Address(parent.AddressLineOne, parent.AddressLineTwo,
                parent.City, parent.State, parent.Zip);
            ParentAddress.SetFromGoogle();


            if (GeocodeAdapter.Insert(parent.ParentID, ParentAddress.Latitude, ParentAddress.Longitude) == 1)
                NumberCreated++;
        }

        GeocodeAdapter.Dispose();

        return NumberCreated;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.ParentsDataTable GetParentByID(int ParentID)
    {
        return this.Adapter.GetParentByParentID(ParentID);
    }
}
