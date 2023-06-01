<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CorrectAttendanceDatabaseErrors.aspx.cs" Inherits="DatabaseManager_CorrectAttendanceDatabaseErrors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="InfoLabel" runat="server" Text=""></asp:Label>
    <asp:Button ID="CheckButton" runat="server" Text="Delete Duplicate Attendances" 
        onclick="DeleteDuplcateAttendances" />
</asp:Content>

