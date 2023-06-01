<%@ Page MaintainScrollPositionOnPostback="true" Title="Database Manager Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialDatabaseManagerPage.aspx.cs" Inherits="DatabaseManager_SpecialDatabaseManagerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:HyperLink ID="SwimmersWithNotesHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/SwimmersWithNotes.aspx">All Swimmers with System Generated Notes</asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink ID="FamiliesNoSwimmersHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/FamiliesWithNoSwimmers.aspx">Families With No Swimmers</asp:HyperLink><br />
        <asp:HyperLink ID="FamiliesNoParentsHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/FamiliesWithoutParents.aspx">Families Without Parents</asp:HyperLink><br />
        <asp:HyperLink ID="FamiliesTooManyParentsHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/FamiliesWithTooManyParents.aspx">Families With Too Many Parents</asp:HyperLink><br />
        <asp:HyperLink ID="ParentsNoFamiliesHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/ParentsWithoutFamilies.aspx">Parents Without Families</asp:HyperLink><br />
        <asp:HyperLink ID="SwimmersNoFamiliesHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/SwimmersWithoutFamilies.aspx">Swimmers Without Families</asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink ID="SwimmersNoFamiliesHyperLink0" runat="server" 
            NavigateUrl="~/DatabaseManager/DeleteSwimmer.aspx">Delete Swimmers</asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink ID="LinkAttendacesHyperLink" runat="server" 
            NavigateUrl="~/DatabaseManager/LinkAttendances.aspx">Link Unregistered Attendances to Registered Swimmers</asp:HyperLink>
</p>
<p>
        <a href="CorrectAttendanceDatabaseErrors.aspx">Delete Duplicate Attendances</a></p>
    <p>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
        <br />
    </p>
</asp:Content>

