<%@ Page MaintainScrollPositionOnPostback="true" Title="Backend Controls" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default2.aspx.cs" Inherits="Features_Default2" %>

<%@ Register Src="../UserControls/Hyperlinks/GroupViewLink.ascx" TagName="GroupViewLink"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Hyperlinks/ListOfSwimmersHyperlink.ascx" TagName="ListOfSwimmersHyperlink"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/Hyperlinks/ApproveSwimmersHyperLink.ascx" TagName="ApproveSwimmersHyperLink"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/Hyperlinks/SetSwimmersAsInDatabaseHyperLink.ascx"
    TagName="SetSwimmersAsInDatabaseHyperLink" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/Hyperlinks/CreateSpecialAccountHyperLink.ascx"
    TagName="CreateSpecialAccountHyperLink" TagPrefix="uc5" %>
<%@ Register Src="../UserControls/Hyperlinks/SetupApplicationHyperLink.ascx" TagName="SetupApplicationHyperLink"
    TagPrefix="uc6" %>
<%@ Register Src="../UserControls/Hyperlinks/MessagesHyperLink.ascx" TagName="MessagesHyperLink"
    TagPrefix="uc7" %>
<%@ Register Src="../UserControls/Hyperlinks/ManageGroupsHyperLink.ascx" TagName="ManageGroupsHyperLink"
    TagPrefix="uc8" %>
<%@ Register Src="../UserControls/Hyperlinks/ManagerUserAccountsHyperLink.ascx" TagName="ManagerUserAccountsHyperLink"
    TagPrefix="uc9" %>
<%@ Register Src="../UserControls/Hyperlinks/ManageCreditsHyperLink.ascx" TagName="ManageCreditsHyperLink"
    TagPrefix="uc10" %>
<%@ Register Src="../UserControls/Hyperlinks/ViewMeetEntriesHyperlink.ascx" TagName="ViewMeetEntriesHyperlink"
    TagPrefix="uc11" %>
<%@ Register Src="../UserControls/Hyperlinks/GroupEmailer.ascx" TagName="GroupEmailer"
    TagPrefix="uc12" %>
<%@ Register Src="../UserControls/Hyperlinks/EventsManagerHyperLink.ascx" TagName="EventsManagerHyperLink"
    TagPrefix="uc13" %>
<%@ Register Src="../UserControls/Hyperlinks/CoachViewCalendar.ascx" TagName="CoachViewCalendar"
    TagPrefix="uc14" %>
<%@ Register Src="../UserControls/Hyperlinks/TopTensSinceHyperLink.ascx" TagName="TopTensSinceHyperLink"
    TagPrefix="uc15" %>
<%@ Register Src="../UserControls/Hyperlinks/BanquetListHyperLink.ascx" TagName="BanquetListHyperLink"
    TagPrefix="uc16" %>
<%@ Register Src="../UserControls/Hyperlinks/AttendanceHyperLink.ascx" TagName="AttendanceHyperLink"
    TagPrefix="uc17" %>
<%@ Register src="../UserControls/Hyperlinks/DatabaseManagerHyperLink.ascx" tagname="DatabaseManagerHyperLink" tagprefix="uc18" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        Thanks for logging in. I&#39;m sorry this page is so spartan, but it&#39;s only
        visible to certain people and considering I just threw this together, I had to pick
        my battles as to what to spend a long time working on, and this page took the short
        straw.</p>
    <p>
        Listed below are links to the features that your account has.</p>
    <p>
        <uc1:GroupViewLink ID="GroupViewLink1" runat="server" />
        <br />
        <uc12:GroupEmailer ID="GroupEmailer1" runat="server" />
        <br />
        <uc14:CoachViewCalendar ID="CoachViewCalendar1" runat="server" />
        <br />
        <uc2:ListOfSwimmersHyperlink ID="ListOfSwimmersHyperlink1" runat="server" />
        <br />
        <uc3:ApproveSwimmersHyperLink ID="ApproveSwimmersHyperLink1" runat="server" />
        <br />
        <uc7:MessagesHyperLink ID="MessagesHyperLink1" runat="server" />
        <br />
        <uc4:SetSwimmersAsInDatabaseHyperLink ID="SetSwimmersAsInDatabaseHyperLink1" runat="server" />
        <br />
        <uc5:CreateSpecialAccountHyperLink ID="CreateSpecialAccountHyperLink1" runat="server" />
        <br />
        <uc6:SetupApplicationHyperLink ID="SetupApplicationHyperLink1" runat="server" />
        <br />
        <uc18:DatabaseManagerHyperLink 
            ID="DatabaseManagerSpecialPageHyperLink1" runat="server" />
        <br />
        <uc8:ManageGroupsHyperLink ID="ManageGroupsHyperLink1" runat="server" />
        <br />
        <uc13:EventsManagerHyperLink ID="EventsManagerHyperLink1" runat="server" />
        <br />
        <uc9:ManagerUserAccountsHyperLink ID="ManagerUserAccountsHyperLink1" runat="server" />
        <br />
        <uc10:ManageCreditsHyperLink ID="ManageCreditsHyperLink1" runat="server" />
        <br />
        <uc11:ViewMeetEntriesHyperlink ID="ViewMeetEntriesHyperlink1" runat="server" />
        <br />
        <uc17:AttendanceHyperLink ID="AttendanceHyperLink1" runat="server" />
    </p>
    <p>
        <a href="../All/MembersMap.aspx">Map of Currently Active Families</a></p>
    <p>
        <uc15:TopTensSinceHyperLink ID="TopTensSinceHyperLink1" runat="server" />
        <uc16:BanquetListHyperLink ID="BanquetListHyperLink1" runat="server" />
    </p>
    <p>
        <asp:Label ID="NumberOfActiveSwimmersTextBox" runat="server" Text="There are currently [n] active GTAC Swimmers."></asp:Label>
        <br />
        <asp:Label ID="NumberRegisteredSinceRegDateTextBox" runat="server" Text="There have been [n] swimmers in the water since [d]."></asp:Label>
        &nbsp;(<asp:HyperLink ID="ContactsHyperLink" runat="server" Text="Click here to create a file of contacts to import 
        into Gmail" NavigateUrl="../BackendUser/Contacts.aspx"></asp:HyperLink>)
    </p>
</asp:Content>
