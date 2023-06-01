<%@ Page Title="E-mail meet Attendees" MaintainScrollPositionOnPostback="true" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmailMeetAttendees.aspx.cs"
    Inherits="DatabaseManager_EmailMeetAttendees" %>

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
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="MeetsPanel" runat="server">
                                        <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
                                        <asp:CheckBoxList ID="MeetsCheckBoxList" runat="server" DataSourceID="MeetsDataSource"
                                            DataTextField="MeetName" DataValueField="Meet">
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
