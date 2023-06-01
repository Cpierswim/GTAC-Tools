<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttendanceControl.ascx.cs"
    Inherits="UserControls_Attendance_AttendanceControl" %>
<%@ Register Src="../DateControl/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>
<asp:Label ID="AttendanceSavedLabel" runat="server" ForeColor="Red" 
    Text="Practice Attendance Saved." Visible="False"></asp:Label>
<asp:TextBox ID="Tester" runat="server" Columns="150" Visible="False"></asp:TextBox>
<asp:Panel ID="RegularPanel" runat="server">
    <uc1:DateControl ID="DateControl1" runat="server" AutoChangeDate="False" YearsBeforeCurrent="1" />
    &nbsp;&nbsp;&nbsp;&nbsp; Group:
    <asp:DropDownList ID="GroupsDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChangeSelectedGroup">
    </asp:DropDownList>
    &nbsp;<asp:Button ID="ChangeDefaultGroupButton" runat="server" Text="Change Default Group"
        OnClick="ChangeDefaultGroupButtonClicked" />
    <br />
    <br />
    Practice #:
    <asp:DropDownList ID="PracticeOfTheDayDropDownList" runat="server" AutoPostBack="True"
        OnSelectedIndexChanged="PracticeOfTheDayDropDownListSelectionChanged">
    </asp:DropDownList>
    &nbsp;
    <asp:Button ID="AddPracticeButton" runat="server" Text="Add Practice" OnClick="AddPracticeButtonClicked" />
    <asp:Label ID="AddPracticeErrorLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Button ID="AssignYardsButton" runat="server" Text="Assign Yardage To Lanes"
        OnClick="AssignYardageToLanesButtonClicked" />&nbsp;
    <asp:Button ID="AddNewSwimmerButton" runat="server" Text="Add New Swimmer" OnClick="SwitchToAddNewSwimmerPanel" />
    <br />
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
    &nbsp;
    <asp:Button ID="SavePracticeButton" runat="server" 
        onclick="SavePracticeButtonClicked" Text="Save Practice" />
</asp:Panel>
<asp:Panel ID="AddSwimmerPanel" runat="server" Visible="False">
    First Name:
    <asp:TextBox ID="NewSwimmerFirstNameTextBox" runat="server" Width="300px"></asp:TextBox>
    <br />
    Last Name:
    <asp:TextBox ID="NewSwimmerLastNameTextBox" runat="server" Width="300px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="AddSwimmerButton" runat="server" Text="Add Swimmer" OnClick="AddSwimmerButtonClicked" />
    &nbsp;<asp:Button ID="CancelAddSwimmerButton" runat="server" OnClick="CancelAddSwimmerButtonClicked"
        Text="Cancel" />
    <br />
    <br />
    Note: This only adds the swimmer for attendance purposes. This does not register
    the swimmer. The team accountant will also recieve a report that a non-registered
    swimmer is practicing with you. <strong>This swimmer can swim for 1 week before they
        must be registered to be in the water.</strong> If they are not registered by
    this time, you may have to help them register.</asp:Panel>
<asp:Panel ID="ChangeDefaultGroupPanel" runat="server" Visible="False">
    Please set your default group. Your default group is the group that will show up
    first when there is a list of groups.<br />
    <br />
    <asp:DropDownList ID="DefaultListDropDownBox" runat="server" OnSelectedIndexChanged="DefaultGroupChanged"
        AutoPostBack="True">
    </asp:DropDownList>
    &nbsp;
    <asp:Button ID="KeepThisGroupButton" runat="server" Text="Keep This Group" OnClick="KeepThisGroupButtonClicked" />
</asp:Panel>
<asp:Panel ID="LanePanel" runat="server" Visible="False">
    <asp:DropDownList ID="YardsMetersDropDownList" runat="server">
        <asp:ListItem Value="Y">Yards</asp:ListItem>
        <asp:ListItem Value="M">Meters</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Table ID="LaneTable" runat="server">
    </asp:Table>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Assign Yardages" OnClick="AssignYardagesButtonclicked" />
</asp:Panel>
