<%@ Page Title="Coach Pick Events" Language="C#" MasterPageFile="~/Site.master" CodeFile="PickEvents.aspx.cs"
    Inherits="Coach_Meet_PickEvents" %>

<%@ Register Src="../../UserControls/TimeControl/TimeControl.ascx" TagName="TimeControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/EntryControl.ascx" TagName="EntryControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"
        type="text/javascript"></script>
    <%--<script src="../../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/TimeSpan.js" type="text/javascript"></script>
    <script src="../../Scripts/Coaches/PickEvents.js" type="text/javascript"></script>
    <script src="../../Scripts/Coaches/PickEventsControls.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <%--<script type='text/javascript' defer='defer'>
        var refSubmit = document.forms[0].onsubmit;

        function mySubmit() {
            var blnDoSubmit = refSubmit();
            if (blnDoSubmit) 
                ShowSplashScreen();
            return blnDoSubmit;
        }

        document.forms[0].onsubmit = mySubmit;
    </script>--%>
    <script type="text/javascript">
        function CreateAccordions() {
            $(function () {
                $("#ctl00_MainContent_MainAccordion").accordion({
                    autoHeight: false,
                    navigation: true,
                    collapsible: true,
                    active: false
                });
            });

            $(function () {
                $("input:text").focus(function () { $(this).select(); });
                $("input:text").mouseup(function (e) {
                    e.preventDefault();
                });
            });
        }

        $(function () {
            $("#dialog:ui-dialog").dialog("destroy");

            $("#dialog-modal").dialog({
                resizable: false,
                autoOpen: false,
                height: 140,
                modal: true,
                closeOnEscape: false,
                open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }
            });
        });
    </script>
    <style type="text/css">
        .style3
        {
            width: 16px;
            height: 16px;
        }
        .aspNetDisabled
        {
            background-color: #505050;
        }
        
        .Invalid
        {
            color: White;
            background-color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dialog-modal" title="">
        <div style="text-align: center; font-weight: bold; font-size: large;">
            <p id="DialogText">
                Processing....</p>
            <img alt="Loading..." class="style3" src="../../Styles/images/small-loading.gif" />
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <span style="margin-right: 2px;">Group:</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="GroupsDropDownList" runat="server" DataSourceID="GroupsDataSource"
                                DataTextField="GroupName" DataValueField="GroupID" OnDataBound="GroupDropDownListDatabound">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <span style="margin-left: 10px; margin-right: 2px;">Meet:</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="MeetsDropDownList" runat="server" OnDataBound="MeetsDropDownListDataBound">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="LoadMeetButton" runat="server" Text="Load Meet" Style="margin-left: 10px;"
                                OnClick="LoadMeetClicked" CausesValidation="False" OnClientClick="LoadMeetClicked(this)"
                                UseSubmitBehavior="false" />
                        </td>
                        <td>
                            <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
                                <ProgressTemplate>
                                    <img alt="Loading..." class="style3" src="../../Styles/images/small-loading.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="DeadlineLabel" runat="server" Style="margin-left: 10px;"></asp:Label>
                            </td>
                        </tr>
                    </tr>
                </tbody>
            </table>
            &nbsp;
            <br />
            <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
            <h1 style="text-align: center; margin-bottom: 20px;">
                <asp:Label ID="MeetNameLabel" runat="server" Visible="false"></asp:Label>
            </h1>
            <div style="margin-left: 30px; margin-right: 30px; text-align: center;">
                <asp:Label ID="CoachInfoLabel" runat="server" Style="font-weight: bold;"></asp:Label>
            </div>
            <asp:Panel ID="PagePanel" runat="server">
            </asp:Panel>
            <asp:HiddenField ID="DisplayingMeet" runat="server" Value="" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LoadMeetButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
    <div id="Preload" style="display: none;">
        <img alt="" src="../../Styles/images/ui-bg_diagonals-medium_20_040e2f_40x40.png" />
        <img alt="" src="../../Styles/images/ui-bg_highlight-hard_60_507cd1_1x100.png" />
        <img alt="" src="../../Styles/images/ui-bg_highlight-soft_100_4b6c9e_1x100.png" />
        <img alt="" src="../../Styles/images/ui-bg_highlight-soft_100_0711ca_1x100.png" />
        <img alt="" src="../../Styles/images/ui-bg_inset-hard_100_fcfdfd_1x100.png" />
        <img src="../../Styles/images/ui-bg_flat_55_999999_40x100.png" />
    </div>
</asp:Content>
