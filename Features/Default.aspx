<%@ Page MaintainScrollPositionOnPostback="true" Title="Backend User" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Features_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="ErrorLabel" ForeColor="Red" Visible="false" runat="server"></asp:Label>
    So I have finally upgraded the site layout a bit. I'm still working on it, but this
    should be a bit more logical.
    <asp:Label ID="BanquetLabel" runat="server" OnLoad="DisplayBanquetStatus"></asp:Label>
    <asp:Label ID="DefaultGroupLabel" runat="server" OnLoad="DisplayDefaultGroup"></asp:Label>
    <asp:HyperLink ID="WaitingToBeApprovedHyperLink" runat="server" OnLoad="WaitingToBeApprovedStatus"
        NavigateUrl="~/OfficeManager/ApproveSwimmers.aspx" Visible="false"></asp:HyperLink>
    <asp:HyperLink ID="WaitingToBeAddedHyperLink" runat="server" OnLoad="WaitingToBeAddedStatus"
        NavigateUrl="~/DatabaseManager/AddToDatabase.aspx" Visible="false"></asp:HyperLink>
    <asp:Label ID="LeadingLabel" runat="server" Visible="false"></asp:Label>
    <asp:HyperLink ID="UpdateMessagesHyperLink" runat="server" Text="" Visible="false"></asp:HyperLink>
    <asp:Label ID="TrailingLabel" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="NumberOfActiveSwimmersTextBox" runat="server" Text="<br /><br />There are currently [n] active GTAC Swimmers."></asp:Label>
    <asp:Label ID="NumberRegisteredSinceRegDateTextBox" runat="server" Text="There have been [n] swimmers in the water since [d]."></asp:Label>
    (<asp:HyperLink ID="ContactsHyperLink" runat="server" Text="Click here to create a file of contacts to import 
        into Gmail" NavigateUrl="../BackendUser/Contacts.aspx"></asp:HyperLink>)
</asp:Content>
