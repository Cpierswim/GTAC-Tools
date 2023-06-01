using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Address
/// </summary>
public class Address
{
    private String _addresslineone, _addresslinetwo, _city, _state, _zip, _street_number, _county, _country, _route;
    private StatusType _status;
    public enum StatusType { VALID, INVALID, UNKNOWN };
    private String _latitude, _longitude;

    public String AddressLineOne
    {
        get { return this._addresslineone; }
        set { this._addresslineone = value; }
    }
    public String AddressLineTwo
    {
        get { return this._addresslinetwo; }
        set { this._addresslinetwo = value; }
    }
    public String City
    {
        get { return this._city; }
        set { this._city = value; }
    }
    public String State
    {
        get { return this._state; }
        set { this._state = value; }
    }
    public String Zip
    {
        get { return this._zip; }
        set { this._zip = value; }
    }
    public String StreetNumber
    {
        get { return this._street_number; }
        set { this._street_number = value; }
    }
    public String County
    {
        get { return this._county; }
        set { this._county = value; }
    }
    public String Country
    {
        get { return this._country; }
        set { this._country = value; }
    }
    public StatusType Status
    {
        get { return this._status; }
    }
    public String Latitude
    {
        get { return this._latitude; }
    }
    public String Longitude
    {
        get { return this._longitude; }
    }
    public String Route
    {
        get { return this._route; }
        set { this._route = value; }
    }

    public Address()
    {
        this._addresslineone = "";
        this._addresslinetwo = "";
        this._city = "";
        this._state = "";
        this._zip = "";
        this._street_number = "";
        this._county = "";
        this._country = "";
        this._latitude = "";
        this._longitude = "";
        this._route = "";
        this._status = Address.StatusType.INVALID;
    }
    public Address(String AddressLineOne, String AddressLineTwo, String City, String State, String Zip)
    {
        this.AddressLineOne = AddressLineOne;
        this.AddressLineTwo = AddressLineTwo;
        this.City = City;
        this.State = State;
        this.Zip = Zip;
        this._street_number = "";
        this._country = "";
        this._county = "";
        this._latitude = "";
        this._longitude = "";
        this._route = "";
        this._status = Address.StatusType.UNKNOWN;
    }
    public Address(String AddressText)
    {
        //int stop = 1;
    }

    public void SetFromGoogle()
    {

        String GoogleMapsXMLResultsAddres = "http://maps.google.com/maps/api/geocode/xml?";
        String AddressForGoogleMaps = "address=";
        AddressForGoogleMaps += this.ToString().Replace(' ', '+');

        AddressForGoogleMaps += "&sensor=false";
        String FullGoogleMapsURL = GoogleMapsXMLResultsAddres + AddressForGoogleMaps;

        XmlReader reader = XmlReader.Create(FullGoogleMapsURL);
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);

        XmlNodeList TempList = doc.GetElementsByTagName("status");
        if (TempList.Count != 1)
        {
            this._status = Address.StatusType.INVALID;
            throw new AddressException("\"Status\" node was not found in the XML string.");
        }
        XmlElement StatusNode = ((XmlElement)TempList[0]);
        if (StatusNode.InnerText != "OK")
            this._status = Address.StatusType.INVALID;
        else
        {
            this._status = Address.StatusType.VALID;

            TempList = doc.GetElementsByTagName("result");
            if (TempList.Count < 1)
                throw new AddressException("\"result\" node was not found in the XML string.");
            XmlElement ResultNode = ((XmlElement)TempList[0]);
            XmlNodeList AddressComponentList = ResultNode.GetElementsByTagName("address_component");
            for (int i = 0; i < AddressComponentList.Count; i++)
            {
                XmlElement AddressComponentNode = ((XmlElement)AddressComponentList[i]);
                String ShortName = AddressComponentNode.GetElementsByTagName("short_name")[0].InnerText;

                if (AddressComponentNode.InnerText.Contains("street_number"))
                    this._street_number = ShortName;
                else if (AddressComponentNode.InnerText.Contains("route"))
                    this._route = ShortName;
                else if (AddressComponentNode.InnerText.Contains("locality"))
                    this._city = ShortName;
                else if (AddressComponentNode.InnerText.Contains("administrative_area_level_2"))
                    this._county = ShortName;
                else if (AddressComponentNode.InnerText.Contains("administrative_area_level_1"))
                    this._state = ShortName;
                else if (AddressComponentNode.InnerText.Contains("country"))
                    this._country = ShortName;
                else if (AddressComponentNode.InnerText.Contains("postal_code"))
                    this._zip = ShortName;
            }

            this._addresslineone = this._street_number + " " + this._route;

            XmlElement LocationNode = ((XmlElement)ResultNode.GetElementsByTagName("location")[0]);

            this._latitude = LocationNode.GetElementsByTagName("lat")[0].InnerText;
            this._longitude = LocationNode.GetElementsByTagName("lng")[0].InnerText;

            if (String.IsNullOrEmpty(this.StreetNumber))
            {
                if ((this._route == "5th St") && (this._city == "Clay Center"))
                    this._status = StatusType.VALID;
                else
                    this._status = StatusType.INVALID;
            }
        }
    }
    public override string ToString()
    {
        String returnstring = "";

        returnstring += AddressLineOne;
        if (!String.IsNullOrEmpty(AddressLineTwo))
            returnstring += " " + AddressLineTwo;
        returnstring += " " + City;
        returnstring += " " + State;
        returnstring += " " + Zip;

        return returnstring;
    }



}