<%@ Page MaintainScrollPositionOnPostback="true" Title="Meet Entries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MeetEntries.aspx.cs" Inherits="DatabaseManager_MeetEntries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource1"
        DataTextField="MeetName" DataValueField="MeetID">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsBLL"></asp:ObjectDataSource>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
        EnableModelValidation="True" OnDataBound="AdjustRows" OnRowCommand="RowButtonPressed">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="USAID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("USAID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="USAIDHiddenField" runat="server" Value='<%# Eval("USAID") %>' />
                    <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sessions">
                <ItemTemplate>
                    <asp:Label ID="SessionsLabel" runat="server" Text=""></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="All Entries in Database"></asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="Mark" Text="Mark As In Database For All Sessions" />
            <asp:ButtonField ButtonType="Button" CommandName="Delete" 
                Text="Delete From Sessions" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSwimmersInMeet" TypeName="SwimmersInMeetBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="-1" Name="MeetID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
