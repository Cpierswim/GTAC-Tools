<%@ Page MaintainScrollPositionOnPostback="true" Title="Link Attendances" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LinkAttendances.aspx.cs" Inherits="DatabaseManager_LinkAttendances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <asp:Label ID="RowsAffectedLabel" runat="server" Text="Label" Visible="false"></asp:Label>
        Swimmer List:
        <asp:DropDownList ID="SwimmersDropDownList" runat="server" 
            ViewStateMode="Disabled">
        </asp:DropDownList>
        <br />
        <br />
        Unlinked Swimmers:
        <asp:DropDownList ID="UnlinkedDropDownList" runat="server" 
            ViewStateMode="Disabled">
        </asp:DropDownList>
    </div>
    <p>
        <asp:Button ID="LinkButton" runat="server" Text="Link" OnClick="LinkNames" />
    </p>
    <p>
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
    </p>
</asp:Content>
