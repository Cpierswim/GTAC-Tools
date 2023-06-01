<%@ Page MaintainScrollPositionOnPostback="true" Title="List of Swimmers - Office Manager" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Swimmers.aspx.cs" Inherits="OfficeManager_Swimmers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        <asp:ListItem Value="Active">Active Swimmers</asp:ListItem>
        <asp:ListItem Value="Inactive">Inactive Swimmers</asp:ListItem>
        <asp:ListItem Value="All">All Swimmers</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        OnRowDataBound="RowDataBound" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="NameHyperLink" runat="server">HyperLink</asp:HyperLink>
                    <asp:HiddenField ID="PreferredNameHiddenField" runat="server" Value='<%# Bind("PreferredName") %>'/>
                    <asp:HiddenField ID="LastNameHiddenField" runat="server" Value='<%# Bind("LastName") %>'/>
                    <asp:HiddenField ID="USAIDHiddenField1" runat="server" Value='<%# Bind("USAID") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Gender") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GenderLabel" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Registration Progess Status" SortExpression="ReadyToAdd">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("ReadyToAdd") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="RegistrationProgressLabel" runat="server" Text='<%# Bind("ReadyToAdd") %>'></asp:Label>
                    <asp:HiddenField ID="ReadyToAddHiddenField" runat="server" Value='<%# Bind("ReadyToAdd") %>' />
                    <asp:HiddenField ID="IsInDatabaseHiddenField" runat="server" Value='<%# Bind("IsInDatabase") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active Status" SortExpression="Inactive">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# Bind("Inactive") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="ActiveStatusLabel" runat="server" Text='<%# Bind("Inactive") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="GroupID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("GroupID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GroupLabel" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
