<%@ Page MaintainScrollPositionOnPostback="true" Title="Manage Events For Day" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageEventsForDay.aspx.cs" Inherits="DatabaseManager_ManageEventsForDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black"
        BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black"
        Height="250px" NextPrevFormat="ShortMonth" Width="330px" OnSelectionChanged="DaySelectionChanged"
        OnVisibleMonthChanged="VisibleMonthChanged">
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
        <DayStyle BackColor="#CCCCCC" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt"
            ForeColor="White" Height="12pt" />
        <TodayDayStyle BackColor="#999999" ForeColor="White" />
    </asp:Calendar>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EventID"
        DataSourceID="EventsDataSource" EnableModelValidation="True" OnRowDataBound="RowCreated"
        OnRowUpdating="GridviewRowUpdating">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:TemplateField HeaderText="Date" SortExpression="DateandTime">
                <EditItemTemplate>
                    <asp:Calendar ID="EventDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                        CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="Black" Height="180px" SelectedDate='<%# Bind("DateandTime") %>' Width="200px"
                        VisibleDate='<%# Eval("DateandTime") %>'>
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EventDateLabel" runat="server" Text='<%# Bind("DateandTime") %>'></asp:Label>
                    <asp:Calendar ID="EventDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                        CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="Black" Height="180px" SelectedDate='<%# Bind("DateandTime") %>' Width="200px"
                        Visible="false">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Time">
                <EditItemTemplate>
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
                    <asp:DropDownList ID="StartAMPMDropDownList" runat="server">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="StartTimeLabel" runat="server" Text="Label"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Time">
                <EditItemTemplate>
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
                    <asp:DropDownList ID="EndAMPMDropDownList" runat="server">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="EndDateTimeHiddenField" runat="server" Value='<%# Eval("EndTime") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EndTimeLabel" runat="server" Text="Label"></asp:Label>
                    <asp:HiddenField ID="EndDateTimeHiddenField" runat="server" Value='<%# Eval("EndTime") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="GroupID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="GroupsDataSource"
                        DataTextField="GroupName" DataValueField="GroupID" SelectedValue='<%# Bind("GroupID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GroupsLabel" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="EventsDataSource" runat="server" SelectMethod="GetEventsBetweenTwoDatesInclusive"
        TypeName="EventsBLL" DeleteMethod="DeleteEvent" UpdateMethod="UpdateEvent" OnDeleted="EventDeleted"
        OnUpdated="EventUpdated">
        <DeleteParameters>
            <asp:Parameter Name="EventID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="Calendar1" Name="StartTime" PropertyName="SelectedDate"
                Type="DateTime" />
            <asp:ControlParameter ControlID="Calendar1" Name="EndTime" PropertyName="SelectedDate"
                Type="DateTime" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="EventID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="DateandTime" Type="DateTime" />
            <asp:Parameter Name="EndTime" Type="DateTime" />
            <asp:Parameter Name="GroupID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/EventsManager.aspx">&lt;&lt; Return to Main Events Page</asp:HyperLink>
</asp:Content>
