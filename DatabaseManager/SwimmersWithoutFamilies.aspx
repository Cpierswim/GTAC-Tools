<%@ Page MaintainScrollPositionOnPostback="true" Title="Swimmers Without Families" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SwimmersWithoutFamilies.aspx.cs" Inherits="DatabaseManager_SwimmersWithoutFamilies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="USAID" DataSourceID="SwimmersDataSource" 
        EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="USAID" HeaderText="USAID" ReadOnly="True" 
                SortExpression="USAID" />
            <asp:BoundField DataField="FamilyID" HeaderText="FamilyID" 
                SortExpression="FamilyID" />
            <asp:BoundField DataField="LastName" HeaderText="LastName" 
                SortExpression="LastName" />
            <asp:BoundField DataField="MiddleName" HeaderText="MiddleName" 
                SortExpression="MiddleName" />
            <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="PreferredName" HeaderText="PreferredName" 
                SortExpression="PreferredName" />
            <asp:BoundField DataField="Birthday" HeaderText="Birthday" 
                SortExpression="Birthday" />
            <asp:BoundField DataField="Gender" HeaderText="Gender" 
                SortExpression="Gender" />
            <asp:CheckBoxField DataField="ReadyToAdd" HeaderText="ReadyToAdd" 
                SortExpression="ReadyToAdd" />
            <asp:CheckBoxField DataField="IsInDatabase" HeaderText="IsInDatabase" 
                SortExpression="IsInDatabase" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" 
                SortExpression="PhoneNumber" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
            <asp:BoundField DataField="Created" HeaderText="Created" 
                SortExpression="Created" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" 
                SortExpression="Inactive" />
            <asp:BoundField DataField="GroupID" HeaderText="GroupID" 
                SortExpression="GroupID" />
            <asp:BoundField DataField="Ethnicity" HeaderText="Ethnicity" 
                SortExpression="Ethnicity" />
            <asp:CheckBoxField DataField="USCitizen" HeaderText="USCitizen" 
                SortExpression="USCitizen" />
            <asp:BoundField DataField="Disability" HeaderText="Disability" 
                SortExpression="Disability" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetSwimmersWithNoFamily" TypeName="SwimmersBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>

