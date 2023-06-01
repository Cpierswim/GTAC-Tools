<%@ Page Title="Select Meet" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Meets2.aspx.cs" Inherits="Parents_Meet_Meets2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
        $(function ()
        {
            $('input:submit').button();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="ErrorLabel" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Label ID="LateEntriesOpenLabel" runat="server" Text="There are meets open for late entries at the bottom of the page.<br /><br />"
        Visible="false"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="Meets open for entry:" Style="font-weight: bold;"></asp:Label>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMeetsOpenForEntry" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="MeetsDataSource" OnItemDataBound="RowDataBound"
        OnPreRender="RepeaterDataBound" OnItemCommand="ButtonClicked">
        <ItemTemplate>
            <asp:HiddenField ID="MeetIDHiddenField" runat="server" Value='<%# Eval("Meet") %>' />
            <asp:Panel ID="DisplayPanel" runat="server" Width="100%">
                <h2 style="text-align: center;">
                    <asp:Label ID="MeetNameLabel" runat="server" Text='<%# Bind("MeetName") %>' /></h2>
                <table cellpadding="5" cellspacing="5" style="width: 100%;">
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <span style="font-weight: bold;">Location:</span>
                            <asp:Label ID="LocationLabel" runat="server" Text='<%# Bind("Location") %>' />
                            <br />
                            <span style="font-weight: bold;">Remarks:</span>
                            <asp:Label ID="RemarksLabel" runat="server" Text='<%# Bind("Remarks") %>' /><br />
                            <br />
                            <span style="font-weight: bold;">Instructions:</span>
                            <asp:Label ID="InstructionsLabel" runat="server" Text='<%# Bind("Instructions") %>' />
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            Start Date:<span style="font-weight: bold;"><asp:Label ID="StartLabel" runat="server"
                                Text='<%# Bind("Start") %>' /></span>
                            <br />
                            End Date:<span style="font-weight: bold;">
                                <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>' /><br />
                            </span>Course:
                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Bind("Course") %>' /><br />
                            <br />
                            <span style="color: Red;">Deadline:<span style="font-weight: Bold;">
                                <asp:Label ID="DeadlineLabel" runat="server" Text='<%# Bind("Deadline") %>' /></span></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="EnterMeetButton" runat="server" Text="Enter " Width="40%" CommandArgument="Enter" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Label ID="Label2" runat="server" Text="&lt;br /&gt;&lt;br /&gt;These meets are open for late entry. Please note that late entries can be refused by the meet host."></asp:Label>
    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="LateEntryDataSource" OnItemCommand="ButtonClicked"
        OnItemDataBound="RowDataBound" OnPreRender="RepeaterDataBound">
        <ItemTemplate>
            <asp:HiddenField ID="MeetIDHiddenField" runat="server" Value='<%# Eval("Meet") %>' />
            <asp:Panel ID="DisplayPanel" runat="server" Width="100%">
                <h2 style="text-align: center;">
                    <asp:Label ID="MeetNameLabel" runat="server" Text='<%# Bind("MeetName") %>' /></h2>
                <table cellpadding="5" cellspacing="5" style="width: 100%;">
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <span style="font-weight: bold;">Location:</span>
                            <asp:Label ID="LocationLabel" runat="server" Text='<%# Bind("Location") %>' />
                            <br />
                            <span style="font-weight: bold;">Remarks:</span>
                            <asp:Label ID="RemarksLabel" runat="server" Text='<%# Bind("Remarks") %>' /><br />
                            <br />
                            <span style="font-weight: bold;">Instructions:</span>
                            <asp:Label ID="InstructionsLabel" runat="server" Text='<%# Bind("Instructions") %>' />
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            Start Date:<span style="font-weight: bold;"><asp:Label ID="StartLabel" runat="server"
                                Text='<%# Bind("Start") %>' /></span>
                            <br />
                            End Date:<span style="font-weight: bold;">
                                <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>' /><br />
                            </span>Course:
                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Bind("Course") %>' /><br />
                            <br />
                            <span style="color: Red;">Late Entry Deadline:<span style="font-weight: Bold;">
                                <asp:Label ID="DeadlineLabel" runat="server" Text='<%# Bind("LateEntryDeadline") %>' /></span></span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="EnterMeetButton" runat="server" Text="Enter " Width="40%" CommandArgument="Enter" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="LateEntryDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMeetsPastDeadlineButOpenForLateEntry" TypeName="MeetsV2BLL">
    </asp:ObjectDataSource>
</asp:Content>
