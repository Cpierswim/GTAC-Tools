<%@ Page MaintainScrollPositionOnPostback="true" Title="List of Swimmers by Group" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="GroupsView.aspx.cs" Inherits="DatabaseManager_GroupsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="CoachGroupHiddenField" runat="server" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        DataSourceID="ObjectDataSource1" OnRowCancelingEdit="RowCanceledEdit" OnRowEditing="RowEditingClicked"
        OnRowUpdated="RowUpdated" OnRowDataBound="RowDatabound" ViewStateMode="Enabled">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName" InsertVisible="False">
                <EditItemTemplate>
                    <asp:Label ID="PreferredNameLabel" runat="server" Text='<%# Bind("PreferredName") %>'></asp:Label>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="PreferredNameLabel" runat="server" Text='<%# Bind("PreferredName") %>'></asp:Label>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gender" InsertVisible="False" SortExpression="Gender">
                <EditItemTemplate>
                    <asp:Label ID="GenderLabel" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GenderLabel" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Age" InsertVisible="False">
                <EditItemTemplate>
                    <asp:Label ID="AgeLabel" runat="server" Text="Label"></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="AgeLabel" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Birthday" InsertVisible="False" SortExpression="Birthday">
                <EditItemTemplate>
                    <asp:Label ID="BirthdayLabel" runat="server" Text='<%# Bind("Birthday", "{0:d}") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="BirthdayLabel" runat="server" Text='<%# Bind("Birthday", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="GroupID" Visible="False">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="InnerGroupsDataSource"
                        DataTextField="GroupName" DataValueField="GroupID" SelectedValue='<%# Bind("GroupID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="InnerGroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1"
                        DataTextField="GroupName" DataValueField="GroupID" SelectedValue='<%# Bind("GroupID") %>'
                        Enabled="False">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" EditText="Change Group" UpdateText="Save Edits" />
        </Columns>
        <EmptyDataTemplate>
            No swimmers in group.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        SelectMethod="GetSwimmersByGroupID" TypeName="SwimmersBLL"
        OnUpdating="SwimmerUpdating" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:ControlParameter ControlID="CoachGroupHiddenField" Name="GroupID" PropertyName="Value"
                Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
