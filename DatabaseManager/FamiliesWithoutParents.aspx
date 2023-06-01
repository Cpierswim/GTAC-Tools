<%@ Page MaintainScrollPositionOnPostback="true" Title="Families Without Parents" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FamiliesWithoutParents.aspx.cs" Inherits="DatabaseManager_FamiliesWithoutParents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="NotificationLabel" runat="server"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FamilyID"
        DataSourceID="ObjectDataSource1" EnableModelValidation="True" OnRowDataBound="RowDatabound"
        Width="100%" OnRowCommand="RowButtonClicked">
        <Columns>
            <asp:TemplateField HeaderText="Account without Parents" SortExpression="UserID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" Value='<%# Eval("FamilyID") %>' />
                    <asp:HiddenField ID="UserIDHiddenField" runat="server" Value='<%# Eval("UserID") %>' />
                    <asp:Label ID="HTMLLabel" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80%" />
            </asp:TemplateField>
            <asp:ButtonField CommandName="EmailFamily" Text="Email Family" />
            <asp:ButtonField CommandName="DeleteFamily" Text="Delete Family" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFamiliesWithNoParents" TypeName="FamiliesBLL"></asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>
