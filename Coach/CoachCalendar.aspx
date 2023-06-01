<%@ Page MaintainScrollPositionOnPostback="true" Title="Coach Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CoachCalendar.aspx.cs" Inherits="Coach_CoachCalendar" %>

<%@ Register src="../UserControls/Calendar/EventsCalendar.ascx" tagname="EventsCalendar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <br />
    Select Group(s): (Control+Click to select more than 1 group)<br />
    <asp:ListBox ID="GroupsListBox" runat="server" DataSourceID="GroupsDataSource" 
        DataTextField="GroupName" DataValueField="GroupID" 
        ondatabound="GroupsListBoxDataBound" SelectionMode="Multiple"></asp:ListBox>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveGroups" 
        TypeName="GroupsBLL"></asp:ObjectDataSource>
    <asp:Button ID="DisplayButton" runat="server" 
        onclick="DisplayCalendarButtonClicked" Text="Display Calendar for Group(s)" />
    <br />
    <br />
    <uc1:EventsCalendar ID="EventsCalendar1" runat="server" />
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>

