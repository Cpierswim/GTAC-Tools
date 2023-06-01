<%@ Page MaintainScrollPositionOnPostback="true" Title="Attendance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Coach_Attendance" %>

<%@ Register src="../UserControls/Attendance/AttendanceControl.ascx" tagname="AttendanceControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <br />
    <uc1:AttendanceControl ID="AttendanceControl1" runat="server" />
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>

