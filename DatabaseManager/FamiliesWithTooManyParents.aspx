<%@ Page MaintainScrollPositionOnPostback="true" Title="Families With Too Many Parents" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FamiliesWithTooManyParents.aspx.cs" Inherits="DatabaseManager_FamiliesWithTooManyParents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="FamilyID" DataSourceID="FamiliesDataSource" 
        EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="FamilyID" HeaderText="FamilyID" 
                InsertVisible="False" ReadOnly="True" SortExpression="FamilyID" />
            <asp:BoundField DataField="UserID" HeaderText="UserID" 
                SortExpression="UserID" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="FamiliesDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetFamiliesWithTooManyParents" TypeName="FamiliesBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>

