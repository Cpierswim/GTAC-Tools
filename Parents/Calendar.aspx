<%@ Page MaintainScrollPositionOnPostback="true" Title="Parent Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Parents_Calendar" %>

<%@ Register src="../UserControls/Calendar/EventsCalendar.ascx" tagname="EventsCalendar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(function ()
        {
            $('input:submit').button();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:EventsCalendar ID="EventsCalendar" runat="server" />
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Parents/FamilyView.aspx">&lt;&lt; Go Back to Family View</asp:HyperLink>
</asp:Content>

