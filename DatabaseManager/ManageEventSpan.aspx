<%@ Page Title="Manage Event Span" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageEventSpan.aspx.cs" Inherits="DatabaseManager_ManageEventSpan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="SelectPanel" runat="server">
        Start Date:
        <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="StartDateTextBoxExtender" runat="server" TargetControlID="StartDateTextBox"
            Format="MMMM d, yyyy" PopupButtonID="Image1">
        </ajaxToolkit:CalendarExtender>
        <span style="margin-left: 35px;">End Date:</span>
        <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="EndDateTextBoxExtender" runat="server" TargetControlID="EndDateTextBox"
            Format="MMMM d, yyyy" PopupButtonID="Image2">
        </ajaxToolkit:CalendarExtender>
        <br />
        Start Time:
        <asp:DropDownList ID="StartTimeDropDownList" runat="server">
        </asp:DropDownList>
        <span style="margin-left: 106px;">End Time:</span>
        <asp:DropDownList ID="EndTimeDropDownList" runat="server" />
        <br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="0">Sunday</asp:ListItem>
            <asp:ListItem Value="1">Monday</asp:ListItem>
            <asp:ListItem Value="2">Tuesday</asp:ListItem>
            <asp:ListItem Value="3">Wednesday</asp:ListItem>
            <asp:ListItem Value="4">Thursday</asp:ListItem>
            <asp:ListItem Value="5">Friday</asp:ListItem>
            <asp:ListItem Value="6">Saturday</asp:ListItem>
        </asp:CheckBoxList>
        <br />
        <table>
            <tr style="vertical-align: top;">
                <td>
                    Groups:
                </td>
                <td>
                    <asp:ListBox ID="GroupsListBox" runat="server" DataSourceID="GroupsDataSource" DataTextField="GroupName"
                        DataValueField="GroupID" SelectionMode="Multiple"></asp:ListBox>
                    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Button ID="LoadButton" runat="server" OnClick="LoadButtonClicked" Text="Load" />
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" Visible="false">
        Change to: <span style="font-weight: bold;">Notice: Be careful with these settings.
            The selectors below do NOT default to the original values.</span>
        <br />
        <br />
        New Start Time:
        <asp:DropDownList ID="NewStartTimeDropDownList" runat="server">
        </asp:DropDownList>
        <span style="margin-left: 35px;">New End Time: </span>
        <asp:DropDownList ID="NewEndTimeDropDownList" runat="server">
        </asp:DropDownList>
        <br />
        Group:
        <asp:DropDownList ID="NewGroupDropDownList" runat="server" DataSourceID="GroupsDataSource"
            DataTextField="GroupName" DataValueField="GroupID" 
            ondatabound="GroupsDropDownListDataBound">
        </asp:DropDownList>
        <br />
        New Description:
        <asp:TextBox ID="NewDescription" runat="server" Text="Do not edit to keep same description."
            MaxLength="100" Columns="45"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="CancelClicked" OnCommand="SaveButtonClicked" />
        <asp:Button ID="CancelButton" runat="server" OnClick="CancelClicked" Text="Cancel"
            Style="margin-left: 35px;" />
        <asp:Button ID="DeleteButton" runat="server" OnClick="DeleteClicked" Text="Delete All"
            Style="margin-left: 350px;" />
    </asp:Panel>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnDataBound="GridViewDataBound"
        CellPadding="4" ForeColor="#333333" GridLines="None" Width="648px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start">
                <ItemTemplate>
                    <asp:Label ID="StartLabel" runat="server" Text='<%# Bind("DateandTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End">
                <ItemTemplate>
                    <asp:Label ID="EndLabel" runat="server" Text='<%# Bind("EndTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group">
                <ItemTemplate>
                    <asp:Label ID="GroupLabel" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>
