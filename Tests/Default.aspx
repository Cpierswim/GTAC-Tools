<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Tests_Default" %>

<%@ Register Src="../UserControls/TimeControl/TimeControl.ascx" TagName="TimeControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#accordion").accordion({
                autoHeight: false,
                navigation: true,
                collapsible: true
            });
            $("#Accordion2").accordion({
                autoHeight: false,
                navigation: true,
                collapsible: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1 style="text-align:center;">
        31<sup>st</sup> Annual GTAC Turkey Meet</h1>
    <br />
    Group:
    <asp:DropDownList runat="server">
        <asp:ListItem Text="Gold" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Pre-Senior"></asp:ListItem>
        <asp:ListItem Text="Silver"></asp:ListItem>
    </asp:DropDownList>
    <div id="accordion">
        <h3 id="USAIDHeader1">
            <a href="#">Agnes Bahr</a></h3>
        <div id="USAIDContent1">
            Max Entries For Meet: 6
            <div id="Accordion2">
                <h3 id="USAID1Session1">
                    <a href="#">Saturday AM</a></h3>
                <div id="USAID1Session1Content">
                    Max Entries for Session: 4
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-weight: bold;">
                                Event Name
                            </td>
                            <td style="font-weight: bold;">
                                Age Group
                            </td>
                            <td style="font-weight: bold;">
                                Cut Time
                            </td>
                            <td style="font-weight: bold;">
                                Best Time
                            </td>
                            <td>
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Bonus
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Exhibition
                            </td>
                        </tr>
                        <tr>
                            <td>
                                50 Free
                            </td>
                            <td>
                                11-12 Girls
                            </td>
                            <td>
                                36.09Y<br />
                                40.24L
                            </td>
                            <td>
                                29.89Y<br />
                                33.44L
                            </td>
                            <td>
                                <asp:CheckBox ID="USAID1Session1Event1" runat="server" /><uc1:TimeControl ID="USAID1Session1Event1TimeControl"
                                    runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="USAID1Session1Event1Bonus" runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="USAID1Session1Event1Ex" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                50 Back
                            </td>
                            <td>
                                11-12 Girls
                            </td>
                            <td>
                                39.09
                            </td>
                            <td>
                                34.23
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" /><uc1:TimeControl ID="TimeControl1"
                                    runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox3" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <h3 id="USAID1Session2">
                    <a href="#">Sunday AM</a></h3>
                <div id="USAID1Session2Content">
                    Max Entries for Session: 4
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center; font-weight: bold;">
                                Event Name
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Age Group
                            </td>
                            <td>
                                Cut Time
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Best Time
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Bonus
                            </td>
                            <td style="text-align: center; font-weight: bold;">
                                Exhibition
                            </td>
                        </tr>
                        <tr>
                            <td>
                                100 Free
                            </td>
                            <td>
                                11-12 Girls
                            </td>
                            <td>
                                1:45.09
                            </td>
                            <td>
                                1:12.89
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox4" runat="server" /><uc1:TimeControl ID="TimeControl2"
                                    runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox5" runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox6" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                100 Back
                            </td>
                            <td>
                                11-12 Girls
                            </td>
                            <td>
                                2:00.09
                            </td>
                            <td>
                                1:34.23
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox7" runat="server" /><uc1:TimeControl ID="TimeControl3"
                                    runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox8" runat="server" />
                            </td>
                            <td style="text-align: center;">
                                <asp:CheckBox ID="CheckBox9" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <h3 id="USAIDHeader2">
            <a href="#">Connor Bishop</a></h3>
        <div id="USAIDContent2">
            content
        </div>
        <h3 id="USAIDHeader3">
            <a href="#">Regan Bohm</a></h3>
        <div id="USAIDContent3">
            content
        </div>
        <h3 id="USAIDHeader4">
            <a href="#">Abbie Brodie</a></h3>
        <div id="USAIDContent4">
            content
        </div>
    </div>
</asp:Content>
