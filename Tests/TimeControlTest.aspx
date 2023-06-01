<%@ Page MaintainScrollPositionOnPostback="true" Title="Time Control Test" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TimeControlTest.aspx.cs" Inherits="Tests_TimeControlTest" %>

<%@ Register Src="../UserControls/TimeControl/TimeControl.ascx" TagName="TimeControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:TimeControl ID="TimeControl1" runat="server" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <br />
    <br />
    <asp:TextBox runat="server" ID="TestBox" Width="100%"></asp:TextBox>
</asp:Content>
