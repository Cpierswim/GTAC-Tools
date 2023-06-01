<%@ Page MaintainScrollPositionOnPostback="true" Title="View User Accounts" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewMembers.aspx.cs" Inherits="Admin_ViewMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 48%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="UserInfoPanel" runat="server" Visible="False" Width="633px">
    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text="" EnableViewState="false" Visible="false"></asp:Label>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    User Name:
                    <asp:TextBox ID="UserNameTextBox" runat="server" Columns="40" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 32%;">
                    Online Status:
                    <asp:Label ID="OnlineStatusLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Email:
                    <asp:TextBox ID="EmailTextBox" runat="server" Columns="40"></asp:TextBox>
                </td>
                <td style="width: 32%;">
                    Created:
                    <asp:Label ID="CreatedDateLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Approved:<asp:CheckBox ID="ApprovedCheckBox" runat="server" />
                </td>
                <td style="width: 32%;">
                    Last Activity:
                    <asp:Label ID="LastActivityLabel" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Locked Out:
                    <asp:CheckBox ID="LockedOutCheckBox" runat="server" Enabled="False" />
                </td>
                <td style="width: 32%;">
                    &nbsp;
                </td>
            </tr>
        </table>
        Roles:
        <asp:CheckBoxList ID="RolesCheckBoxList" runat="server">
        </asp:CheckBoxList>
        <br />
        Admin Comments on User:
        <br />
        <asp:TextBox ID="UserCommentsTextBox" runat="server" Columns="50" MaxLength="254"
            Rows="3" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SaveChangesButton" runat="server" 
            onclick="SaveUserButtonClicked" Text="Save Changes" />
        &nbsp;
        <asp:Button ID="DeleteUserButton" runat="server" 
            onclick="DeleteUserButtonClicked" Text="DELETE USER, FAMILY, AND SWIMMERS" />
        <br />
    </asp:Panel>
    <asp:Repeater ID="FilteringUI" runat="server" OnItemCommand="FilteringUI_ItemCommand">
        <ItemTemplate>
            <asp:LinkButton runat="server" ID="lnkFilter" Text='<%# Container.DataItem %>' CommandName='<%# Container.DataItem %>'></asp:LinkButton>
        </ItemTemplate>
        <SeparatorTemplate>
            |</SeparatorTemplate>
    </asp:Repeater>
    <asp:GridView ID="UserAccountsGridView" runat="server" AutoGenerateColumns="False"
        OnSelectedIndexChanged="RowSelected">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:TemplateField HeaderText="UserName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                    <asp:HiddenField ID="UserNameHiddenField" runat="server" Value='<%# Eval("UserName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:CheckBoxField DataField="IsApproved" HeaderText="Approved" />
            <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked Out" />
            <asp:CheckBoxField DataField="IsOnline" HeaderText="Online" />
            <asp:BoundField DataField="Comment" HeaderText="Comment" />
        </Columns>
    </asp:GridView>
    <p>
        &nbsp;<asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">First</asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton
            ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">Prev</asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton
                ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>&nbsp;|&nbsp;<asp:LinkButton
                    ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>
        <asp:HiddenField ID="SelectedUsernameHiddenFIeld" runat="server"  />
    </p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    </p>
</asp:Content>
