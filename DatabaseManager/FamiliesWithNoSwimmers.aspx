<%@ Page MaintainScrollPositionOnPostback="true" Title="Families With No Swimmers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FamiliesWithNoSwimmers.aspx.cs" Inherits="Admin_FamiliesWithNoSwimmers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="NotificationLabel" runat="server" ></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FamilyID"
        DataSourceID="FamiliesDataSource" EnableModelValidation="True" OnRowDataBound="RowDataBound"
        Width="100%" OnRowCommand="ButtonClicked">
        <Columns>
            <asp:TemplateField HeaderText="FamilyID" InsertVisible="False" SortExpression="FamilyID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FamilyID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" Value='<%# Eval("FamilyID") %>' />
                    <asp:HiddenField ID="UsernameHiddenField" runat="server" />
                    <asp:Label ID="NameLabel" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    Email:&nbsp;<asp:Label ID="EmailLabel" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70%" />
            </asp:TemplateField>
            <asp:ButtonField CommandName="EmailAccount" Text="E-mail Account" />
            <asp:ButtonField CommandName="DeleteAccount" Text="Delete Account" />
            <asp:ButtonField CommandName="DeleteAccountNoEmail" Text="Delete Account w/o Emailing" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="FamiliesDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFamiliesWithNoSwimmers" TypeName="FamiliesBLL"></asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>
