<%@ Page Title="Email Group" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NewEmailGroup.aspx.cs" Inherits="Coach_NewEmailGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Label ID="ErrorLabel" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 25%;">
                        <table>
                            <tr style="text-align: center;">
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChangeDropDownListType">
                                        <asp:ListItem>By Group</asp:ListItem>
                                        <asp:ListItem>By Meet Entries</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="MeetsPanel" runat="server" Visible="False">
                                        <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
                                        <asp:CheckBoxList ID="MeetsCheckBoxList" runat="server" DataSourceID="MeetsDataSource"
                                            DataTextField="MeetName" DataValueField="Meet">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    <asp:Panel ID="GroupsPanel" runat="server">
                                        <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                                        <asp:CheckBoxList ID="GroupsCheckBoxList" runat="server" DataSourceID="GroupsDataSource"
                                            DataTextField="GroupName" DataValueField="GroupID">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Parents</asp:ListItem>
                                        <asp:ListItem>Parents &amp; Swimmers</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Load E-mails" OnClick="LoadEmails" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 40px;">
                                    From:
                                </td>
                                <td>
                                    <asp:TextBox ID="FromTextBox" runat="server" Width="100%"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40px;">
                                    To:
                                </td>
                                <td>
                                    <asp:TextBox ID="ToTextBox" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40px;">
                                    Subject:
                                </td>
                                <td>
                                    <asp:TextBox ID="SubjectTextBox" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="MainTextBox" runat="server" Rows="25" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="Button2" runat="server" Text="Send E-mail" OnClick="SendEmailClicked" />
                        <br />
                        (Note: Due to limitations in place from the hosting company, it can take several 
                        minutes to e-mail if there are more than 5 e-mail addresses being mailed to. If 
                        you have more than 5 e-mail addresses, please wait until this page redirects 
                        itself back to the main page.)</td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
