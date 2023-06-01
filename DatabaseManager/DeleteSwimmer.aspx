<%@ Page MaintainScrollPositionOnPostback="true" Title="Delete Swimmer" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DeleteSwimmer.aspx.cs" Inherits="DatabaseManager_DeleteSwimmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="USAID" DataSourceID="SwimmersDataSource" EnableModelValidation="True"
        Width="100%">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
            <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" SortExpression="MiddleName" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
            <asp:BoundField DataField="PreferredName" HeaderText="PreferredName" SortExpression="PreferredName" />
            <asp:CheckBoxField DataField="ReadyToAdd" HeaderText="ReadyToAdd" SortExpression="ReadyToAdd" />
            <asp:CheckBoxField DataField="IsInDatabase" HeaderText="IsInDatabase" SortExpression="IsInDatabase" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" SortExpression="Inactive" />
            <asp:BoundField DataField="Created" HeaderText="Created" SortExpression="Created" />
            <asp:BoundField DataField="GroupID" HeaderText="GroupID" SortExpression="GroupID" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersDataSource" runat="server" DeleteMethod="DeleteSwimmer"
        SelectMethod="GetSwimmers" TypeName="SwimmersBLL">
        <DeleteParameters>
            <asp:Parameter Name="USAID" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
