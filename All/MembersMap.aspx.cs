using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.Security;
using System.Net;

public partial class All_MembersMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Create the Dictonary of the available Groups
            SwimTeamDatabase.GroupsDataTable groups = new GroupsBLL().GetAllGroups();
            Dictionary<int, String> GroupsDictionary = new Dictionary<int, string>();

            AddJavaScriptMethodtoBodyTag();
            List<Marker> MarkerList = GetListOfGoogleMarkersWithThisGroup(this.DropDownList1.SelectedValue);
            this.CreateGoogleMapsJavaScriptFromMarkers(MarkerList, "41.66324629199249", "-83.59949827194214",
                "St. Francis De Sales");


            if (Roles.IsUserInRole("Parent"))
            {
                this.HyperLink1.Text = "&lt;&lt; Go Back to Family View";
                this.HyperLink1.NavigateUrl = "~/Parents/FamilyView.aspx";
            }

            DisplayingLabel.Text = "Displaying " + MarkerList.Count + " families.";
            DisplayingLabel.Visible = true;
        }
        catch (Exception exce)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "Server side Exception.<br />";
            ErrorLabel.Text += "Message: " + exce.Message + "<br />";
            if (exce.InnerException != null)
                ErrorLabel.Text += AddInnerException(0, exce.InnerException);
            ErrorLabel.Text += "Source: " + exce.Source + "<br />";
            ErrorLabel.Text += "Stack Trace: " + exce.StackTrace + "<br />";
        }
    }

    //Developing Methods and InnerClasses
    private class Family
    {
        private int _primaryParentIndex, _secondaryParentIndex;

        private int _FamilyID;
        public int FamilyID
        {
            get { return this._FamilyID; }
            set { this._FamilyID = value; }
        }
        private List<SwimTeamDatabase.ParentsRow> _parents;
        private List<SwimTeamDatabase.ParentsGeocodesRow> _parentsGeocodes;
        public int ParentsCount
        {
            get { return this._parents.Count; }
        }
        private List<SwimTeamDatabase.SwimmersRow> _swimmers;
        public List<SwimTeamDatabase.SwimmersRow> Swimmers { get { return this._swimmers; } }
        public int SwimmersCount
        {
            get { return this._swimmers.Count; }
        }
        public SwimTeamDatabase.ParentsRow PrimaryParent
        {
            get
            {
                if (this._primaryParentIndex != -1)
                    return this._parents[this._primaryParentIndex];
                else
                    return null;
            }
        }
        public SwimTeamDatabase.ParentsGeocodesRow PrimaryParentGeocode
        {
            get
            {
                if (this._primaryParentIndex != -1)
                    return this._parentsGeocodes[this._primaryParentIndex];
                else
                    return null;
            }
        }
        public SwimTeamDatabase.ParentsRow SecondaryParent
        {
            get
            {
                if (this._secondaryParentIndex != -1)
                    return this._parents[this._secondaryParentIndex];
                else
                    return null;
            }
        }
        public SwimTeamDatabase.ParentsGeocodesRow SecondaryParentGeocode
        {
            get
            {
                if (this._secondaryParentIndex != -1)
                    return this._parentsGeocodes[this._secondaryParentIndex];
                else
                    return null;
            }
        }

        public Family(int FamilyID)
        {
            this._swimmers = new List<SwimTeamDatabase.SwimmersRow>();
            this._parents = new List<SwimTeamDatabase.ParentsRow>();
            this._parentsGeocodes = new List<SwimTeamDatabase.ParentsGeocodesRow>();
            this._FamilyID = FamilyID;
            this._primaryParentIndex = -1;
            this._secondaryParentIndex = -1;
        }

        public void AddParent(SwimTeamDatabase.ParentsRow ParentToAdd, SwimTeamDatabase.ParentsGeocodesRow ParentGeocode)
        {
            if (ParentToAdd.PrimaryContact)
                this._primaryParentIndex = this._parents.Count;
            else
                this._secondaryParentIndex = this._parents.Count;
            this._parents.Add(ParentToAdd);
            this._parentsGeocodes.Add(ParentGeocode);
        }
        public void AddSwimmer(SwimTeamDatabase.SwimmersRow SwimmerToAdd)
        {
            this._swimmers.Add(SwimmerToAdd);
        }
    }
    private class Marker
    {
        private Address _address;
        private String _lat, _long, _displayname, _homePhone;
        private List<SwimTeamDatabase.SwimmersRow> _swimmers;
        public List<SwimTeamDatabase.SwimmersRow> Swimmers { set { this._swimmers = value; } }

        public String City { get { return this._address.City; } }
        public String AddressLineOne { get { return this._address.AddressLineOne; } }
        public string AddressLineTwo { get { return this._address.AddressLineTwo; } }
        public String State { get { return this._address.State; } }
        public String Zip { get { return this._address.Zip; } }
        public String Latitude
        {
            get { return this._lat; }
            set { this._lat = value; }
        }
        public String Longitude
        {
            get { return this._long; }
            set { this._long = value; }
        }
        public String ToolTipText
        {
            get
            {
                return this._displayname.Replace("'", "\'");
            }
            set { this._displayname = value; }
        }
        public String HomePhone
        {
            get { return this._homePhone; }
            set { this._homePhone = value; }
        }
        public String InfoWindowHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<b>" + this._displayname + "</b><br />");
                sb.Append(this.AddressLineOne + "<br />");
                if (!String.IsNullOrEmpty(this.AddressLineTwo))
                    sb.Append(this.AddressLineTwo + "<br />");
                sb.Append(this.City + ", " + this.State + " " + this.Zip + "<br />");
                foreach (SwimTeamDatabase.SwimmersRow swimmer in this._swimmers)
                {
                    sb.Append(swimmer.PreferredName);
                    sb.Append(" " + swimmer.LastName);
                    sb.Append("<br />");
                }
                sb.Append("<br />");
                sb.Append("Home Phone: " + this._homePhone);
                String returnstring = sb.ToString();
                if (returnstring.Contains("'"))
                    returnstring = returnstring.Replace("'", "\\'");
                return returnstring;
            }
        }

        public Marker(String AddressLineOne, String AddressLineTwo, String City, String State, String Zip,
            String Latitude, String Longitude, String FamilyLastName)
        {
            this._address = new Address(AddressLineOne, AddressLineTwo, City, State, Zip);
            this._lat = Latitude;
            this._long = Longitude;
            this._displayname = FamilyLastName;
        }

        public void AddSwimmer(SwimTeamDatabase.SwimmersRow Swimmer)
        {
            this._swimmers.Add(Swimmer);

        }

        public override string ToString()
        {
            return this._displayname + " Family. " + this._swimmers.Count + " swimmers.";
        }
    }
    private String AddInnerException(int ExceptionsAbove, Exception excep)
    {
        ExceptionsAbove++;
        if (excep.InnerException == null)
            return "InnerException" + ExceptionsAbove + ": " + excep.Message.Replace("\n", "<br />") + "<br />";
        else
            return "InnerException" + ExceptionsAbove + ": "
                + excep.Message.Replace("\n", "<br />") + "<br />" + AddInnerException(ExceptionsAbove, excep.InnerException);
    }
    private void AddJavaScriptMethodtoBodyTag()
    {
        //Get the Body tag that is set in the master page and add the initialize javascript function to it
        HtmlGenericControl Body = ((HtmlGenericControl)Master.FindControl("Body"));
        Body.Attributes.Add("onload", "initialize(); AdjustWidth()");
    }
    private List<Marker> GetListOfGoogleMarkersWithThisGroup(String GroupID)
    {
        //first get all the families
        List<Family> FamiliesList = GetFamiliesWithASwimmerInGroup(GroupID);
        List<Marker> MarkersList = new List<Marker>();

        //Now, create a marker for each family. If the parents have different addresses, create
        //two markers for the same family
        foreach (Family fam in FamiliesList)
        {
            bool CreateTwoMarkers = false;
            SwimTeamDatabase.ParentsRow primary = fam.PrimaryParent;
            SwimTeamDatabase.ParentsRow secondary = fam.SecondaryParent;
            if (secondary != null)
                if ((primary.AddressLineOne != secondary.AddressLineOne) ||
                    (primary.AddressLineTwo != secondary.AddressLineTwo) ||
                    (primary.City != secondary.City) ||
                    (primary.State != secondary.State) ||
                    (primary.Zip != secondary.Zip))
                    CreateTwoMarkers = true;

            if (!CreateTwoMarkers)
            {
                Marker MarkerWorkingOn = new Marker(primary.AddressLineOne,
                    primary.AddressLineTwo, primary.City, primary.State,
                    primary.Zip, fam.PrimaryParentGeocode.Latitude, fam.PrimaryParentGeocode.Longitude,
                    primary.LastName);
                MarkerWorkingOn.Swimmers = fam.Swimmers;
                if (secondary != null)
                    if (primary.LastName != secondary.LastName)
                        MarkerWorkingOn.ToolTipText = primary.LastName + "/" + secondary.LastName + " Family";
                    else
                        MarkerWorkingOn.ToolTipText = primary.LastName + " Family";
                else
                    MarkerWorkingOn.ToolTipText = primary.LastName + " Family";
                MarkerWorkingOn.HomePhone = primary.HomePhone;
                MarkersList.Add(MarkerWorkingOn);
            }
            else
            {
                Marker MarkerWorkingon1 = new Marker(primary.AddressLineOne,
                    primary.AddressLineTwo, primary.City, primary.State,
                    primary.Zip, fam.PrimaryParentGeocode.Latitude, fam.PrimaryParentGeocode.Longitude,
                    primary.LastName);
                MarkerWorkingon1.Swimmers = fam.Swimmers;
                Marker MarkerWorkingon2 = new Marker(secondary.AddressLineOne,
                    secondary.AddressLineTwo, secondary.City, secondary.State,
                    secondary.Zip, fam.SecondaryParentGeocode.Latitude, fam.SecondaryParentGeocode.Longitude,
                    secondary.LastName);
                MarkerWorkingon2.Swimmers = fam.Swimmers;

                MarkerWorkingon1.ToolTipText = primary.LastName + " of the " + primary.LastName + "/" + secondary.LastName + " Family";
                MarkerWorkingon1.HomePhone = primary.HomePhone;
                MarkerWorkingon2.ToolTipText = secondary.LastName + " of the " + primary.LastName + "/" + secondary.LastName + " Family";
                MarkerWorkingon2.HomePhone = secondary.HomePhone;

                MarkersList.Add(MarkerWorkingon1);
                MarkersList.Add(MarkerWorkingon2);
            }
        }

        return MarkersList;
    }
    private List<Family> GetFamiliesWithASwimmerInGroup(String GroupID)
    {
        List<Family> ListOfFamilies = new List<Family>();
        int GroupIDasint = int.Parse(GroupID);
        SwimTeamDatabase.FamiliesDataTable families = new FamiliesBLL().GetFamilies();
        SwimTeamDatabase.ParentsDataTable parents = new ParentsBLL().GetParents();
        SwimTeamDatabase.ParentsGeocodesDataTable parentsgeocodes = new SwimTeamDatabaseTableAdapters.ParentsGeocodesTableAdapter().GetAllParentGeocodes();
        SwimTeamDatabase.SwimmersDataTable swimmers = new SwimmersBLL().GetSwimmers();
        //SwimTeamDatabase.GroupsDataTable groups = new GroupsBLL().GetAllGroups();
        //Dictionary<int, String> GroupsDictionary = new Dictionary<int, string>();
        //foreach (SwimTeamDatabase.GroupsRow group in groups)
        //    GroupsDictionary.Add(group.GroupID, group.GroupName);

        foreach (SwimTeamDatabase.FamiliesRow family in families)
        {
            Family FamilyWorkingOn = new Family(family.FamilyID);

            //get all the active swimmers for the family where the group is the group we are looking for
            foreach (SwimTeamDatabase.SwimmersRow swimmer in swimmers)
                if ((swimmer.FamilyID == family.FamilyID) &&//add if the swimmer is in the Family we are looking for
                    (!swimmer.Inactive) &&                  //and only add if the swimmer is active
                    ((swimmer.GroupID == GroupIDasint) || (GroupIDasint == -1))) //and only add if the swimmer is in the group we are looking for (-1 means all)
                    FamilyWorkingOn.AddSwimmer(swimmer);

            //if there are no swimmers in the family, we don't need to continue working on the family
            if (FamilyWorkingOn.SwimmersCount != 0)
            {
                //get all the parents for the family, and add the parent and the corosponding Geocode to the Family Class
                foreach (SwimTeamDatabase.ParentsRow parent in parents)
                    if (parent.FamilyID == family.FamilyID)
                        for (int i = 0; i < parentsgeocodes.Count; i++)
                            if (parent.ParentID == parentsgeocodes[i].ParentID)
                            {
                                FamilyWorkingOn.AddParent(parent, parentsgeocodes[i]);
                                i = parentsgeocodes.Count;
                            }

                //We have now added all the parents, parents location and the swimmers to the family
                //and only the swimmers that match our group have been added
                //so now we can add the family to the list.
                ListOfFamilies.Add(FamilyWorkingOn);
            }
        }

        return ListOfFamilies;
    }
    private void
        CreateGoogleMapsJavaScriptFromMarkers(List<Marker> MarkerList, String MapCenterLatitude, String MapCenterLongitude,
        String MapCenterName)
    {
        //not really sure why this is needed, but aparantly it is.
        String csname2 = "locate";
        Type cstype = this.GetType();

        // Get a ClientScriptManager reference from the Page class.
        ClientScriptManager cs = Page.ClientScript;

        if (!cs.IsStartupScriptRegistered(csname2))
        {
            StringBuilder scripttag = new StringBuilder();
            scripttag.Append("\tfunction initialize() {\n");
            scripttag.Append("\t\ttry{\n");
            this.CreateWindowResizeJavascript(cstype, csname2, scripttag);
            scripttag.Append("\t\tvar latlng = new google.maps.LatLng(" + MapCenterLatitude + ", "
                + MapCenterLongitude + ");\n");
            scripttag.Append("\t\tvar myOptions = {\n");
            scripttag.Append("\t\t\tzoom: 11,\n");
            scripttag.Append("\t\t\tcenter: latlng,\n");
            scripttag.Append("\t\t\tmapTypeId: google.maps.MapTypeId.ROADMAP,\n");
            scripttag.Append("\t\t\tstreetViewControl: true\n");
            scripttag.Append("\t\t\t};\n");
            scripttag.Append("\t\tvar map = new google.maps.Map(document.getElementById(\"map_canvas\"),\n");
            scripttag.Append("\t\t\tmyOptions);\n");
            scripttag.Append("\n\n");
            scripttag.Append("\t\tvar TempLatLng = new google.maps.LatLng(" + MapCenterLatitude + ", " + MapCenterLongitude + ");\n");
            scripttag.Append("\t\tvar infowindow = new google.maps.InfoWindow({\n");
            scripttag.Append("\t\t\tcontent: '<b>St. Francis De Sales High School</b><br />2323 W Bancroft St<br />Toledo, OH 43607'\n");
            scripttag.Append("\t\t});\n");
            scripttag.Append("\t\tvar marker = new google.maps.Marker({\n");
            scripttag.Append("\t\t\tposition: TempLatLng,\n");
            scripttag.Append("\t\t\tmap: map,\n");
            scripttag.Append("\t\t\ticon: 'http://google-maps-icons.googlecode.com/files/swim-outdoor.png',\n");
            scripttag.Append("\t\t\ttitle:\"" + MapCenterName + "\"\n");
            scripttag.Append("\t\t\t});\n");
            scripttag.Append("\t\tgoogle.maps.event.addListener(marker, 'click', function() {\n");
            scripttag.Append("\t\t\tinfowindow.open(map, marker);\n");
            scripttag.Append("\t\t});\n");

            //scripttag.Append("\n\n");
            //scripttag.Append("\t\tvar TempLatLngA = new google.maps.LatLng(41.7126380, -83.7108740);\n");
            //scripttag.Append("\t\tvar infowindowA = new google.maps.InfoWindow({\n");
            //scripttag.Append("\t\t\tcontent: '<b>Sylvania Northview High School</b><br />5403 Silica Drive<br />Sylvania, OH 43560'\n");
            //scripttag.Append("\t\t});\n");
            //scripttag.Append("\t\tvar markerA = new google.maps.Marker({\n");
            //scripttag.Append("\t\t\tposition: TempLatLngA,\n");
            //scripttag.Append("\t\t\tmap: map,\n");
            //scripttag.Append("\t\t\ticon: 'http://google-maps-icons.googlecode.com/files/swim-outdoor.png',\n");
            //scripttag.Append("\t\t\ttitle:\"Sylvania Northview\"\n");
            //scripttag.Append("\t\t\t});\n");
            //scripttag.Append("\t\tgoogle.maps.event.addListener(markerA, 'click', function() {\n");
            //scripttag.Append("\t\t\tinfowindowA.open(map, markerA);\n");
            //scripttag.Append("\t\t});\n");

            scripttag.Append("\n\n");
            scripttag.Append("\t\tvar TempLatLngB = new google.maps.LatLng(41.656609,-83.611279);\n");
            scripttag.Append("\t\tvar infowindowB = new google.maps.InfoWindow({\n");
            scripttag.Append("\t\t\tcontent: '<b>University of Toledo</b><br />Rec Center Pool'\n");
            scripttag.Append("\t\t});\n");
            scripttag.Append("\t\tvar markerB = new google.maps.Marker({\n");
            scripttag.Append("\t\t\tposition: TempLatLngB,\n");
            scripttag.Append("\t\t\tmap: map,\n");
            scripttag.Append("\t\t\ticon: 'http://google-maps-icons.googlecode.com/files/swim-outdoor.png',\n");
            scripttag.Append("\t\t\ttitle:\"UT - Rec\"\n");
            scripttag.Append("\t\t\t});\n");
            scripttag.Append("\t\tgoogle.maps.event.addListener(markerB, 'click', function() {\n");
            scripttag.Append("\t\t\tinfowindowB.open(map, markerB);\n");
            scripttag.Append("\t\t});\n");

            scripttag.Append("\n\n");
            scripttag.Append("\t\tvar TempLatLngC = new google.maps.LatLng(41.658116,-83.611826);\n");
            scripttag.Append("\t\tvar infowindowC = new google.maps.InfoWindow({\n");
            scripttag.Append("\t\t\tcontent: '<b>University of Toledo</b><br />ROTC Building'\n");
            scripttag.Append("\t\t});\n");
            scripttag.Append("\t\tvar markerC = new google.maps.Marker({\n");
            scripttag.Append("\t\t\tposition: TempLatLngC,\n");
            scripttag.Append("\t\t\tmap: map,\n");
            scripttag.Append("\t\t\ticon: 'http://google-maps-icons.googlecode.com/files/swim-outdoor.png',\n");
            scripttag.Append("\t\t\ttitle:\"UT - ROTC\"\n");
            scripttag.Append("\t\t\t});\n");
            scripttag.Append("\t\tgoogle.maps.event.addListener(markerC, 'click', function() {\n");
            scripttag.Append("\t\t\tinfowindowC.open(map, markerC);\n");
            scripttag.Append("\t\t});\n");

            scripttag.Append("\n\n");
            scripttag.Append("\t\tvar TempLatLngD = new google.maps.LatLng(41.599045,-83.65505);\n");
            scripttag.Append("\t\tvar infowindowD = new google.maps.InfoWindow({\n");
            scripttag.Append("\t\t\tcontent: '<b>Laurel Hill Swim and Tennis Club</b><br />2222 Cass Rd.<br />Toledo, OH 43614'\n");
            scripttag.Append("\t\t});\n");
            scripttag.Append("\t\tvar markerD = new google.maps.Marker({\n");
            scripttag.Append("\t\t\tposition: TempLatLngD,\n");
            scripttag.Append("\t\t\tmap: map,\n");
            scripttag.Append("\t\t\ticon: 'http://google-maps-icons.googlecode.com/files/swim-outdoor.png',\n");
            scripttag.Append("\t\t\ttitle:\"Laurel Hill\"\n");
            scripttag.Append("\t\t\t});\n");
            scripttag.Append("\t\tgoogle.maps.event.addListener(markerD, 'click', function() {\n");
            scripttag.Append("\t\t\tinfowindowD.open(map, markerD);\n");
            scripttag.Append("\t\t});\n");


            scripttag.Append("\n\n");
            for (int i = 0; i < MarkerList.Count; i++)
            {
                scripttag.Append("\t\tvar TempLatLng" + i + " = new google.maps.LatLng(" + MarkerList[i].Latitude + ", " + MarkerList[i].Longitude + ");\n");
                scripttag.Append("\t\tvar infowindow" + i + " = new google.maps.InfoWindow({\n");
                scripttag.Append("\t\t\tcontent: '" + MarkerList[i].InfoWindowHTML + "'\n");
                scripttag.Append("\t\t});\n");
                scripttag.Append("\t\tvar marker" + i + " = new google.maps.Marker({\n");
                scripttag.Append("\t\t\tposition: TempLatLng" + i + ",\n");
                scripttag.Append("\t\t\tmap: map,\n");
                scripttag.Append("\t\t\ttitle:\"" + MarkerList[i].ToolTipText + "\"\n");
                scripttag.Append("\t\t\t});\n");
                scripttag.Append("\t\tgoogle.maps.event.addListener(marker" + i + ", 'click', function() {\n");
                scripttag.Append("\t\t\tinfowindow" + i + ".open(map, marker" + i + ");\n");
                scripttag.Append("\t\t});\n");
                if ((i + 1) != MarkerList.Count)
                    scripttag.Append("\n\n");
            }
            scripttag.Append("\t\t}\n");
            scripttag.Append("\t\tcatch(err)\n");
            scripttag.Append("\t\t{\n");
            scripttag.Append("\t\t\tvar txt = \"Error\\n\\n\";\n");
            scripttag.Append("\t\t\ttxt += \"Error description: \" + err.description + \"\\n\\n\";\n");
            scripttag.Append("\t\t\ttxt += \"Click OK to continue.\";\n");
            scripttag.Append("\t\t\talert(txt);\n");
            scripttag.Append("\t\t}\n");
            scripttag.Append("\t}");
            
            cs.RegisterStartupScript(cstype, csname2, scripttag.ToString(), true);
        }
    }

    private void CreateWindowResizeJavascript(Type cstype, String csname2, StringBuilder scripttag)
    {
        //scripttag.Append("\n");
        ////scripttag.Append("\tfunction AdjustWidth()\n");
        ////scripttag.Append("\t{\n");
        //scripttag.Append("\t\tvar winW = 630;\n\n");

        //scripttag.Append("\t\tif (document.body && document.body.offsetWidth) \n");
        //scripttag.Append("\t\t\twinW = document.body.offsetWidth;\n");
        //scripttag.Append("\t\tif (document.compatMode == 'CSS1Compat' &&\n");
        //scripttag.Append("\t\t\tdocument.documentElement &&\n");
        //scripttag.Append("\t\t\tdocument.documentElement.offsetWidth) \n");
        //scripttag.Append("\t\t\twinW = document.documentElement.offsetWidth;\n");
        //scripttag.Append("\t\tif (window.innerWidth)\n");
        //scripttag.Append("\t\t\twinW = window.innerWidth;\n\n");

        //scripttag.Append("\t\tvar TextBox;\n");
        //scripttag.Append("\t\tTextBox = document.getElementById(\"ctl00$MainContent$TextBox1\");\n");
        //scripttag.Append("\t\t//TextBox1.value = winW;\n");
        //scripttag.Append("\t\twinW = 44;\n\n");
        ////scripttag.Append("\t}");
    }
}