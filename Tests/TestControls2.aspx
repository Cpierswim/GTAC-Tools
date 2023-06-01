<%@ Page MaintainScrollPositionOnPostback="true" Title="Test Controls 2" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TestControls2.aspx.cs" Inherits="TestControls2" %>

<%@ Register src="~/UserControls/RecordsDisplay/RecordsDisplay.ascx" tagname="RecordsDisplay" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:RecordsDisplay ID="RecordsDisplay1" runat="server" />
</asp:Content>
