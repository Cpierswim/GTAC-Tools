<%@ Page MaintainScrollPositionOnPostback="true" Title="Manage Events Landing Page"
    Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EventsManager.aspx.cs"
    Inherits="DatabaseManager_EventsManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DatabaseManager/CreateEvents.aspx">Add New Event(s)</asp:HyperLink>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/DatabaseManager/ManageEventsForDay.aspx">Manage Existing Events for a Single Day</asp:HyperLink><br />
    <br />
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/DatabaseManager/ManageEventSpan.aspx">Manage Existing Events for Multipe Days</asp:HyperLink>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
