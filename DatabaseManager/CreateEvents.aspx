<%@ Page MaintainScrollPositionOnPostback="true" Title="Create Event" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateEvents.aspx.cs" Inherits="DatabaseManager_CreateEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 87px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="ErrorPanel" runat="server" Visible="False">
        <asp:Label ID="ErrorLabel" runat="server" Text="Label" ForeColor="Red"></asp:Label>
        <br />
    </asp:Panel>
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DatabaseManager/EventsManager.aspx">&lt;&lt; Return to Main Events Page</asp:HyperLink>
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                Event Name:
            </td>
            <td colspan="3">
                <asp:TextBox ID="EventNameTextBox" runat="server" Columns="100" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                For Group(s):
            </td>
            <td colspan="3">
                <asp:ListBox ID="GroupsListBox" runat="server" DataSourceID="GroupsDataSource" DataTextField="GroupName"
                    DataValueField="GroupID" Width="257px" OnDataBound="ListBoxDatabound" SelectionMode="Multiple">
                </asp:ListBox>
                <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                Start Time:
            </td>
            <td>
                <asp:DropDownList ID="StartHourDropDownList" runat="server">
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                </asp:DropDownList>
                :<asp:DropDownList ID="StartMinuteDropDownList" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="StartAMPMDropDownList" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style1">
                End Time:
            </td>
            <td>
                <asp:DropDownList ID="EndHourDropDownList" runat="server">
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                </asp:DropDownList>
                :<asp:DropDownList ID="EndMinuteDropDownList" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="EndAMPMDropDownList" runat="server">
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                For a single day event, start and end dates should be the day of the event.
            </td>
        </tr>
        <tr>
            <td class="style1">
                Start Date:
            </td>
            <td>
                &nbsp;
                <asp:Calendar ID="StartDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                    CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                    ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </td>
            <td class="style1">
                &nbsp; End Date:
            </td>
            <td>
                &nbsp;
                <asp:Calendar ID="EndDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                    CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                    ForeColor="Black" Height="180px" Width="200px">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
                </asp:Calendar>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Repeat Days:
            </td>
            <td colspan="3">
                <asp:CheckBoxList ID="DaysCheckBoxList" runat="server" RepeatDirection="Horizontal"
                    Width="100%">
                    <asp:ListItem>Sunday</asp:ListItem>
                    <asp:ListItem>Monday</asp:ListItem>
                    <asp:ListItem>Tuesday</asp:ListItem>
                    <asp:ListItem>Wednesday</asp:ListItem>
                    <asp:ListItem>Thursday</asp:ListItem>
                    <asp:ListItem>Friday</asp:ListItem>
                    <asp:ListItem>Saturday</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    <asp:Button ID="CreateButton" runat="server" Text="Create Event(s)" OnClick="CreateEvents"
        Width="100%" />
    <asp:Label ID="ResultLabel" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
</asp:Content>
