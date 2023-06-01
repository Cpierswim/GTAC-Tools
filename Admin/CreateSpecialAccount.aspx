<%@ Page MaintainScrollPositionOnPostback="true" Title="Create Special Account" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateSpecialAccount.aspx.cs" Inherits="Admin_CreateSpecialAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        Username:
        <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
    </p>
    <p>
        Password:
        <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
    </p>
    <p>
        Email:
        <asp:TextBox ID="EmailTestBox" runat="server" Width="421px"></asp:TextBox>
    </p>
    <p>
        Role:
        <asp:DropDownList ID="RoleDropDownList" runat="server" 
            DataSourceID="RolesDataSource" DataTextField="RoleName" 
            DataValueField="RoleName">
        </asp:DropDownList>
        <asp:SqlDataSource ID="RolesDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SwimSiteDevelopmentDatabaseConnectionString %>" 
            SelectCommand="SELECT [RoleName] FROM [vw_aspnet_Roles]">
        </asp:SqlDataSource>
    </p>
    <p>
        <asp:Button ID="AddUserButton" runat="server" onclick="AddUserButton_Clicked" 
            Text="Create Account" />
        
    </p>
    <p><asp:Label ID="CreatedLabel" runat="server" Visible="False"></asp:Label></p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    </p>
</asp:Content>

