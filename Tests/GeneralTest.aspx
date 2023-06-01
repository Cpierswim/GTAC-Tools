<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GeneralTest.aspx.cs" Inherits="Tests_GeneralTest" %>

<%@ Register src="../UserControls/TimeControl/TimeControl.ascx" tagname="TimeControl" tagprefix="uc1" %>
<%@ Register src="../UserControls/EntryControl.ascx" tagname="EntryControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <uc2:EntryControl ID="EntryControl1" runat="server" />
        
        
    </asp:Panel>
</asp:Content>

