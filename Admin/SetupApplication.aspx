<%@ Page MaintainScrollPositionOnPostback="true" Title="Setup Website" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SetupApplication.aspx.cs" Inherits="Admin_SetupApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="SetupLabel" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    Application is <asp:DropDownList ID="ApplicationStatusDropDownList" 
        runat="server" AutoPostBack="True" 
        onselectedindexchanged="ApplicationStatus_Changed">
        <asp:ListItem>Online</asp:ListItem>
        <asp:ListItem>Offline</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    Registration Start Date For This year:
    <asp:Calendar ID="RegStartDateCalendar" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
        onselectionchanged="NewRegStartDate" ShowGridLines="True" Width="200px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    <br />
    E-mail to send notification to when a new swimmer signs up:
    <asp:TextBox ID="NotificationEmailTextBox" runat="server" Columns="40" 
        style="margin-top: 0px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" 
        runat="server" ControlToValidate="NotificationEmailTextBox" 
        ErrorMessage="Not an Email" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    <asp:Button ID="SaveEmailButton" runat="server" 
        onclick="SaveEmailButtonClicked" Text="Save Email" />
    <br />
    <br />
    Display Banquet Button:
    <asp:Label ID="BanquetButtonDisplayLabel" runat="server" Text="Label"></asp:Label>
&nbsp; 
    <asp:Button ID="SwitchBanquetButtonDisplayButton" runat="server" 
        onclick="SwitchBanquetButtonDisplayStatus" Text="Switch to " />
    Text To Display:
    <asp:TextBox ID="BanquetTextTextBox" runat="server" Width="282px"></asp:TextBox>&nbsp;
    <asp:Button ID="SaveBanquetButtonTextButton" runat="server" Text="Save Text" 
        onclick="SaveBanquetButtonTextButtonClicked" /><br /><br />
    <asp:Button ID="DeleteBanquetSignupsButton" runat="server" 
        Text="Delete All Banquet Signups" onclick="DeleteBanquetSignups" 
        Visible="False" />
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>

