using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class BackendUser_Contacts : System.Web.UI.Page
{
    private const String CSVFileName = "GTACContactInfo.csv";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateContacts(object sender, EventArgs e)
    {


        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmers();
        GroupsBLL GroupsAdapter = new GroupsBLL();
        SwimTeamDatabase.GroupsDataTable Groups = GroupsAdapter.GetAllGroups();

        Dictionary<int, String> GroupsDictionary = new Dictionary<int, string>();

        foreach (SwimTeamDatabase.GroupsRow Group in Groups)
            GroupsDictionary.Add(Group.GroupID, Group.GroupName);

        StringBuilder sb = new StringBuilder();
        String header = CreateHeaderLine();
        List<String> SwimmersLines = null;
        if (IncludeSwimmersInfoCheckBox.Checked)
            SwimmersLines = CreateSwimmersContactLines(Swimmers, GroupsDictionary);
        else
            SwimmersLines = new List<string>();
        List<String> ParentsLines = CreateParentsContactLines(Swimmers, GroupsDictionary);

        
        sb.Append(header);
        foreach (String Line in SwimmersLines)
            sb.Append(Line);

        foreach (String Line in ParentsLines)
            sb.Append(Line);


        String doc = sb.ToString();

        StringWriter oStringWriter = new StringWriter();
        oStringWriter.WriteLine("Line 1");
        Response.ContentType = "text/plain";
        Response.AddHeader("content-disposition", "attachment;filename=GTACContacts.csv");
        Response.Clear();
        using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
        {

            writer.Write(doc);

        }

        HttpCookie LastDownloadedCookie = new HttpCookie("LastDownloaded");
        LastDownloadedCookie.Values["LastDownloaded"] = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").ToString();
        LastDownloadedCookie.Expires = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").AddYears(1);
        Response.Cookies.Add(LastDownloadedCookie);

        Response.End();
    }

    private String CreateHeaderLine()
    {
        return "Name,Given Name,Additional Name,Family Name,Yomi Name,Given Name Yomi," +
            "Additional Name Yomi,Family Name Yomi,Name Prefix,Name Suffix,Initials,Nickname," +
        "Short Name,Maiden Name,Birthday,Gender,Location,Billing Information,Directory Server," +
        "Mileage,Occupation,Hobby,Sensitivity,Priority,Subject,Notes,Group Membership," +
        "E-mail 1 - Type,E-mail 1 - Value,E-mail 2 - Type,E-mail 2 - Value,Phone 1 - Type," +
        "Phone 1 - Value,Phone 2 - Type,Phone 2 - Value,Phone 3 - Type,Phone 3 - Value," +
        "Address 1 - Type,Address 1 - Formatted,Address 1 - Street,Address 1 - City," +
        "Address 1 - PO Box,Address 1 - Region,Address 1 - Postal Code,Address 1 - Country," +
        "Address 1 - Extended Address,Address 2 - Type,Address 2 - Formatted,Address 2 - Street," +
        "Address 2 - City,Address 2 - PO Box,Address 2 - Region,Address 2 - Postal Code," +
        "Address 2 - Country,Address 2 - Extended Address" + "\n";
    }
    private List<String> CreateSwimmersContactLines(SwimTeamDatabase.SwimmersDataTable Swimmers,
        Dictionary<int, String> GroupsDictionary)
    {
        List<String> SwimmerContactLines = new List<string>();



        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
        {
            if ((!String.IsNullOrEmpty(Swimmer.PhoneNumber) ||
                !String.IsNullOrEmpty(Swimmer.Email)) &&
                !Swimmer.Inactive)
            {
                StringBuilder sb = new StringBuilder();

                //Display name
                sb.Append(Swimmer.PreferredName + " " + Swimmer.LastName);

                sb.Append(",");

                //First name
                sb.Append(Swimmer.PreferredName);

                sb.Append(",,");

                //Last Name
                sb.Append(Swimmer.LastName);

                sb.Append(",,,,,,,,,,,");

                //Birthday
                String Birthday = Swimmer.Birthday.Year + "-";
                if (Swimmer.Birthday.Month < 10)
                    Birthday += "0";
                Birthday += Swimmer.Birthday.Month + "-";
                if (Swimmer.Birthday.Day < 10)
                    Birthday += "0";
                Birthday += Swimmer.Birthday.Day;
                sb.Append(Birthday);

                sb.Append(",,,,,,,,,,,");

                //Notes
                String Notes = Swimmer.Notes;
                if (Notes.Contains("SYSTEM GENERATED"))
                    Notes = Notes.Remove(Notes.IndexOf("SYSTEM GENERATED"));
                if (Notes.Contains("\""))
                    Notes = Notes.Replace("\"", "\"\"");
                sb.Append("\"" + Notes + "\"");

                sb.Append(",");

                //Groups
                sb.Append("GTAC Swimmers");
                sb.Append(" ::: ");
                sb.Append(" * " + GroupsDictionary[Swimmer.GroupID]);

                sb.Append(",");

                //E-mail
                if (!String.IsNullOrEmpty(Swimmer.Email))
                    sb.Append("Home," + Swimmer.Email);
                else
                    sb.Append(",");

                sb.Append(",,,");

                //Cell Phone
                if (!string.IsNullOrEmpty(Swimmer.PhoneNumber))
                    sb.Append("Mobile," + Swimmer.PhoneNumber);
                else
                    sb.Append(",");

                sb.Append(",,,,,");

                sb.Append(",,,,,,,,,,,,,,,,");

                sb.Append("\n");

                SwimmerContactLines.Add(sb.ToString());
            }
        }

        return SwimmerContactLines;
    }
    private List<String> CreateParentsContactLines(SwimTeamDatabase.SwimmersDataTable Swimmers,
        Dictionary<int, String> GroupsDictionary)
    {
        List<String> ParentsContacts = new List<String>();

        FamiliesBLL FamilyAdapter = new FamiliesBLL();
        SwimTeamDatabase.FamiliesDataTable Families = FamilyAdapter.GetFamilies();
        ParentsBLL ParentsAdapter = new ParentsBLL();
        SwimTeamDatabase.ParentsDataTable Parents = ParentsAdapter.GetParents();



        foreach (SwimTeamDatabase.FamiliesRow Family in Families)
        {
            List<SwimTeamDatabase.ParentsRow> TempFamiliesParents = new List<SwimTeamDatabase.ParentsRow>();
            for (int i = 0; i < Parents.Count; i++)
                if (Parents[i].FamilyID == Family.FamilyID)
                    TempFamiliesParents.Add(Parents[i]);
            SwimTeamDatabase.ParentsRow PrimaryContactParent = null;

            for (int i = 0; i < TempFamiliesParents.Count; i++)
                if (TempFamiliesParents[i].PrimaryContact)
                {
                    PrimaryContactParent = TempFamiliesParents[i];
                    TempFamiliesParents.RemoveAt(i);
                    i = TempFamiliesParents.Count + 1;
                }

            List<SwimTeamDatabase.ParentsRow> FamiliesParents = new List<SwimTeamDatabase.ParentsRow>();
            FamiliesParents.Add(PrimaryContactParent);
            for (int i = 0; i < TempFamiliesParents.Count; i++)
                FamiliesParents.Add(TempFamiliesParents[i]);
            //we now have a list of all the parents for the family we are working, with the primary contact parent being the 
            //first parent


            List<SwimTeamDatabase.SwimmersRow> FamiliesSwimmers = new List<SwimTeamDatabase.SwimmersRow>();
            foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
                if (Swimmer.FamilyID == Family.FamilyID)
                    FamiliesSwimmers.Add(Swimmer);


            //and now we have the swimmers for the family
            List<SwimTeamDatabase.SwimmersRow> ActiveFamilySwimmers = new List<SwimTeamDatabase.SwimmersRow>();
            foreach (SwimTeamDatabase.SwimmersRow Swimmer in FamiliesSwimmers)
                if (!Swimmer.Inactive)
                    ActiveFamilySwimmers.Add(Swimmer);

            if (ActiveFamilySwimmers.Count > 0)
            {

                foreach (SwimTeamDatabase.ParentsRow Parent in FamiliesParents)
                {
                    //create the contact

                    StringBuilder sb = new StringBuilder();

                    //Display name
                    //sb.Append("\"" + Parent.LastName + ", " + Parent.FirstName + "\"");
                    sb.Append("");

                    sb.Append(",");

                    //First Name
                    sb.Append(Parent.FirstName);

                    sb.Append(",,");

                    //Last Name
                    sb.Append(Parent.LastName);

                    sb.Append(",,,,,,,,,,,,,,,,,,,,,,");

                    //Notes
                    String ParentNotes = Parent.Notes;
                    ParentNotes = ParentNotes.Replace("\"", "\"\"");
                    if (Parent.PrimaryContact && !String.IsNullOrEmpty(Parent.Notes))
                        ParentNotes += "\nPrimary Contact";
                    else if(Parent.PrimaryContact)
                        ParentNotes += "Primary Contact";
                    sb.Append("\"" + ParentNotes + "\"");

                    sb.Append(",");

                    //Group Info
                    sb.Append("GTAC Parents");
                    if (Parent.PrimaryContact)
                        sb.Append(" :::  * Primary Contacts");
                    List<int> FamilyGroupIDs = new List<int>();
                    foreach (SwimTeamDatabase.SwimmersRow Swimmer in ActiveFamilySwimmers)
                        if (!FamilyGroupIDs.Contains(Swimmer.GroupID))
                            FamilyGroupIDs.Add(Swimmer.GroupID);

                    foreach (int GroupID in FamilyGroupIDs)
                        sb.Append(" :::  * " + GroupsDictionary[GroupID]);

                    sb.Append(",");

                    //Email
                    sb.Append("Home");
                    sb.Append(",");
                    sb.Append(Parent.Email);

                    sb.Append(",,,");

                    //Phone Numbers
                    List<String> PhoneNumbers = new List<string>();

                    if (String.IsNullOrEmpty(Parent.HomePhone) ||
                        Parent.HomePhone == Parent.CellPhone)
                        PhoneNumbers.Add(",,");
                    else
                        PhoneNumbers.Add("Home," + Parent.HomePhone + ",");

                    if (string.IsNullOrEmpty(Parent.CellPhone))
                        PhoneNumbers.Add(",,");
                    else
                        PhoneNumbers.Add("Mobile," + Parent.CellPhone + ",");

                    if (string.IsNullOrEmpty(Parent.WorkPhone))
                        PhoneNumbers.Add(",,");
                    else
                        PhoneNumbers.Add("Work," + Parent.WorkPhone + ",");

                    List<String> OrderedPhoneNumbers = new List<string>();
                    for (int i = 0; i < PhoneNumbers.Count; i++)
                        if (PhoneNumbers[i] != ",,")
                            OrderedPhoneNumbers.Add(PhoneNumbers[i]);
                    for (int i = 0; i < PhoneNumbers.Count; i++)
                        if (PhoneNumbers[i] == ",,")
                            OrderedPhoneNumbers.Add(PhoneNumbers[i]);
                    for (int i = 0; i < OrderedPhoneNumbers.Count; i++)
                        sb.Append(OrderedPhoneNumbers[i]);

                    //Address 1 type
                    sb.Append("Home");
                    sb.Append(",");

                    //Address 1 Formatted
                    String FormattedAddress = "\"";
                    FormattedAddress += Parent.AddressLineOne + "\n";
                    if (!String.IsNullOrEmpty(Parent.AddressLineTwo))
                        FormattedAddress += Parent.AddressLineTwo + "\n";
                    FormattedAddress += Parent.City + ", " + Parent.State + " " + Parent.Zip + "\"";
                    //sb.Append(FormattedAddress);

                    //sb.Append(",,,,,,,,");
                    //sb.Append(",");
                    //sb.Append(",,,,,,,");

                    FormattedAddress += ",,,,,,,," + "," + ",,,,,,,";


                    ParentsContacts.Add(sb.ToString() + FormattedAddress + "\n");
                }
            }
        }

        return ParentsContacts;
    }
}