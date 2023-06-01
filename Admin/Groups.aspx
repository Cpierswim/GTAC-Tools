<%@ Page MaintainScrollPositionOnPostback="true" Title="Edit Groups" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Groups.aspx.cs" Inherits="Admin_Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="GroupSelectorDropDownList" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="GroupSelectorDropDownList_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="Active">Active Groups</asp:ListItem>
        <asp:ListItem Value="Inactive">Inactive Groups</asp:ListItem>
        <asp:ListItem Value="All">All Groups</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="GroupID" DataSourceID="GroupsDataSource" OnRowUpdating="RowUpdating">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="GroupName" HeaderText="GroupName" SortExpression="GroupName">
                <ItemStyle HorizontalAlign="Center" Width="250px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Created" InsertVisible="False" SortExpression="Created">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Created", "{0:d}") %>'></asp:Label>
                    <asp:HiddenField ID="CreatedHiddenField" runat="server" Value='<%# Eval("Created") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Created", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="75px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active" SortExpression="Active">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Active") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" OnDataBinding="ChangeTrueToYes" Text='<%# Eval("Active") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DefaultGroup" SortExpression="DefaultGroup">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("DefaultGroup") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" OnDataBinding="ChangeTrueToYes" Text='<%# Eval("DefaultGroup") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" InsertMethod="InsertGroup"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveGroups"
        TypeName="GroupsBLL" UpdateMethod="UpdateGroup">
        <InsertParameters>
            <asp:Parameter Name="GroupName" Type="String" />
            <asp:Parameter Name="Created" Type="String" />
            <asp:Parameter Name="DefaultGroup" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="GroupName" Type="String" />
            <asp:Parameter Name="Created" Type="String" />
            <asp:Parameter Name="Active" Type="Boolean" />
            <asp:Parameter Name="DefaultGroup" Type="Boolean" />
            <asp:Parameter Name="original_GroupID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <h1>
        Add New Group</h1>
    <p>
        Name:
        <asp:TextBox ID="NewGroupNameTextBox" runat="server" Columns="35"></asp:TextBox>
        &nbsp; Is Default Group:
        <asp:CheckBox ID="IsDefaultGroupCheckBox" runat="server" />
    </p>
    <p>
        <asp:Button ID="AddNewGroupButton" runat="server" OnClick="AddNewGroupButtonClicked"
            Text="Add New Group" />
    </p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    </p>
</asp:Content>
