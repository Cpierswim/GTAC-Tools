<%@ Page MaintainScrollPositionOnPostback="true" Title="Manage Meet Credits" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageCredits.aspx.cs" Inherits="OfficeManager_ManageCredits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
This page is a little difficult to use right now. Once I get everything working properly, I'm going to work on making this page
easier to use.<br />Right now, press CTRL+F and type in the family name you want to add a meet credit to and press ENTER. This
will bring you to the family name on the page. Make sure you have the right swimmer, and click Add or Subtract meet credit.<br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
&nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="FamilyID" DataSourceID="FamiliesDataSource" 
        EnableModelValidation="True" ondatabound="GridDatabound" 
        onrowcommand="ButtonClicked" Width="100%" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:TemplateField HeaderText="Family" InsertVisible="False" 
                SortExpression="FamilyID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FamilyID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" 
                        Value='<%# Eval("FamilyID") %>' />
                    <asp:Label ID="InnerHtmlLabel" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80%" />
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="Add" Text="Add Credit" />
            <asp:ButtonField ButtonType="Button" CommandName="Subtract" 
                Text="Subtract Credit" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:GridView>
    <asp:ObjectDataSource ID="FamiliesDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetFamiliesAlphabeticalOnlyThoseWithParents" 
        TypeName="FamiliesBLL"></asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>

