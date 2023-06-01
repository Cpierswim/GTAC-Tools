<%@ Page Title="Pick swimmer to Pre-Enter" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddPreEnterPickSwimmer.aspx.cs" Inherits="DatabaseManager_Meet_AddPreEnterPickSwimmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        DataSourceID="SwimmersDataSource" OnRowDataBound="RowDataBound" 
        ondatabound="GridDataBound" onrowcommand="EnterMeetClicked">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    <asp:HiddenField ID="USAIDHiddenField" runat="server" Value='<%# Bind("USAID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Enter" Text="Enter in Meet" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveSwimmers" TypeName="SwimmersBLL"></asp:ObjectDataSource>
</asp:Content>
