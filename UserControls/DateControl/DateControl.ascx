<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateControl.ascx.cs" Inherits="UserControls_DateControl_DateControl" %>
<asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPostedBacked">
    <asp:ListItem Value="1">January</asp:ListItem>
    <asp:ListItem Value="2">February</asp:ListItem>
    <asp:ListItem Value="3">March</asp:ListItem>
    <asp:ListItem Value="4">April</asp:ListItem>
    <asp:ListItem Value="5">May</asp:ListItem>
    <asp:ListItem Value="6">June</asp:ListItem>
    <asp:ListItem Value="7">July</asp:ListItem>
    <asp:ListItem Value="8">August</asp:ListItem>
    <asp:ListItem Value="9">September</asp:ListItem>
    <asp:ListItem Value="10">October</asp:ListItem>
    <asp:ListItem Value="11">November</asp:ListItem>
    <asp:ListItem Value="12">December</asp:ListItem>
</asp:DropDownList>
<asp:DropDownList ID="DayDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPostedBacked">
</asp:DropDownList>
<asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPostedBacked">
</asp:DropDownList>
&nbsp;
<asp:Button ID="ChangeDateButton" runat="server" Text="Change Date" Visible="False"
    OnClick="ChangeDateButtonClicked" />&nbsp;
<asp:Button ID="ChangeToTodayButton" runat="server" Text="Today" Visible="True" OnClick="ChangeToTodayButtonClicked" />
