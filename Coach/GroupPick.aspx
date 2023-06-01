<%@ Page MaintainScrollPositionOnPostback="true" Title="Choose Default Group" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GroupPick.aspx.cs" Inherits="Coach_GroupPick" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        Select what group you coach:
        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
            AutoPostBack="True" DataSourceID="GroupsDatasource" DataTextField="GroupName" 
            DataValueField="GroupID" onselectedindexchanged="GroupSelected">
            <asp:ListItem Value="-1">Select Group...</asp:ListItem>
        </asp:DropDownList>
        <asp:ObjectDataSource ID="GroupsDatasource" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveGroups" 
            TypeName="GroupsBLL"></asp:ObjectDataSource>
    </p>
    <p>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    </p>
</asp:Content>

