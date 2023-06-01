<%@ Page Title="Email Group" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="EmailGroup.aspx.cs" Inherits="Coach_EmailGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <br />
    <asp:DropDownList ID="GroupsDropDownList" runat="server" AutoPostBack="True" DataSourceID="GroupsDatasource"
        DataTextField="GroupName" DataValueField="GroupID" OnSelectedIndexChanged="LoadToBox">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="GroupsDatasource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
    <br />
    <br />
    <asp:RadioButtonList ID="EmailTypeRadioButtonList" runat="server" AutoPostBack="True"
        RepeatDirection="Horizontal">
        <asp:ListItem Selected="True">Parents Only</asp:ListItem>
        <asp:ListItem>Parents and Swimmers</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    (To send an e-mail with HTML formatting or with an attachment, copy and paste the
    &quot;To:&quot; section to your e-mail client.)<br />
    <br />
    From:
    <asp:TextBox ID="FromTextBox" runat="server" Width="100%"></asp:TextBox>
    <br />
    <br />
    To:<br />
    <asp:TextBox ID="ToTextBox" runat="server" Width="100%"></asp:TextBox>
    <br />
    <br />
    Subject:
    <asp:TextBox ID="SubjectTextBox" runat="server" Width="100%"></asp:TextBox>
    <br />
    <br />
    Body:<asp:TextBox ID="BodyTextBox" runat="server" Rows="25" TextMode="MultiLine"
        Width="100%"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="SendEmailButton" runat="server" OnClick="SendMailClicked" Text="Send E-mail" />
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
