<%@ Page MaintainScrollPositionOnPostback="true" Title="Parents Without Families" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ParentsWithoutFamilies.aspx.cs" Inherits="DatabaseManager_ParentsWithoutFamilies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ParentID" DataSourceID="ParentsDataSource" 
        EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="ParentID" HeaderText="ParentID" 
                InsertVisible="False" ReadOnly="True" SortExpression="ParentID" />
            <asp:BoundField DataField="FamilyID" HeaderText="FamilyID" 
                SortExpression="FamilyID" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" 
                SortExpression="LastName" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="AddressLineOne" HeaderText="AddressLineOne" 
                SortExpression="AddressLineOne" />
            <asp:BoundField DataField="AddressLineTwo" HeaderText="AddressLineTwo" 
                SortExpression="AddressLineTwo" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" />
            <asp:BoundField DataField="HomePhone" HeaderText="HomePhone" 
                SortExpression="HomePhone" />
            <asp:BoundField DataField="CellPhone" HeaderText="CellPhone" 
                SortExpression="CellPhone" />
            <asp:BoundField DataField="WorkPhone" HeaderText="WorkPhone" 
                SortExpression="WorkPhone" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
            <asp:CheckBoxField DataField="PrimaryContact" HeaderText="PrimaryContact" 
                SortExpression="PrimaryContact" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ParentsDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetParentsWithNoFamilies" TypeName="ParentsBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>

