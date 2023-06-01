<%@ Page MaintainScrollPositionOnPostback="true" Title="DELETE EVERYTHING" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SDDFKSNDELETEALLINFODDKSLSDN.aspx.cs" Inherits="Admin_SDDFKSNDELETEALLINFODDKSLSDN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="DeleteButton" runat="server" onclick="DeleteButtonclicked" 
        Text="Button" />
    <asp:HiddenField ID="TripsHiddenField" runat="server" />
    <asp:Label ID="DeletedLabel" runat="server" Text="Label" Visible="False"></asp:Label>
</asp:Content>

