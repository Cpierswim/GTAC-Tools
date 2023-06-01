<%@ Page MaintainScrollPositionOnPostback="true" Title="Family Activated" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Activate.aspx.cs" Inherits="Activate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="StatusMessage" runat="server" Text="Label"></asp:Label>
<br />
<br />
<asp:Button ID="LoginButton" runat="server" onclick="LoginButtonClicked" 
    Text="Login" Visible="False" />
&nbsp;<asp:Label ID="ButtonExplainLabel" runat="server" 
    Text="Click this button to automatically Login and add your first athlete to your account." 
    Visible="False"></asp:Label>
</asp:Content>
