<%@ Page MaintainScrollPositionOnPostback="true" Title="My GTAC Family" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FamilyView.aspx.cs"
    Inherits="Parents_FamilyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
<!--        //
        function myPopup(url, windowname, w, h, x, y) {
            window.open(url, windowname,
            "resizable=no,location=no,copyhistory=no,toolbar=no,scrollbars=no,menubar=no,status=no,directories=no,width=" + w + ",height=" + h + ",left=" + x + ",top=" + y + "");
        }
//-->
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            $(".hasToolTip").tooltip({ effect: 'slide' });

        });
    </script>
    <style type="text/css">
        .toolTipLarge
        {
            display: none;
            background: url("../Styles/images/ToolTip/black_big.png");
            height: 194px;
            width: 370px;
            font-size: 11px;
            color: White;
            text-align: center;
        }
        .toolTipSmall
        {
            display: none;
            background: url("../Styles/images/ToolTip/black.png");
            height: 109px;
            width: 209px;
            font-size: 11px;
            color: White;
            text-align: center;
        }
        .tooltip p
        {
            margin-left: 30px;
            margin-right: 30px;
            margin-top: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <asp:Label ID="MeetEntryAddedLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <asp:Label ID="MeetEntryRemovedLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <asp:Label ID="SessionsEmailLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" />
    <h1>
        Parents</h1>
    <table style="width: 100%;">
        <tr>
            <asp:Repeater ID="ParentsRepeater" runat="server" DataSourceID="ParentsDataSource">
                <ItemTemplate>
                    <td style="width: 50%;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="FirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="LastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="AddressLineOne" runat="server" Text='<%# Eval("AddressLineOne") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="AddressLineTwo" runat="server" Text='<%# Eval("AddressLineTwo") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="City" runat="server" Text='<%# Eval("City") %>'></asp:Label>,
                                    <asp:Label ID="State" runat="server" Text='<%# Eval("State") %>'></asp:Label>
                                    <asp:Label ID="Zip" runat="server" Text='<%# Eval("Zip") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Home Phone:
                                    <asp:Label ID="HomePhone" runat="server" Text='<%# Eval("HomePhone") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Cell Phone:
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CellPhone") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Work Phone:
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("WorkPhone") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    E-mail:
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ParentsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetParentsByFamilyID" TypeName="ParentsBLL" OnSelected="ParentsSelected">
        <SelectParameters>
            <asp:ProfileParameter DefaultValue="-1" Name="FamilyID" PropertyName="FamilyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="Button1" runat="server" Text="Edit Parent Information" OnClick="EditParentsButtonClicked" />
    &nbsp;<br />
    <asp:HiddenField ID="GroupsListHiddenField" runat="server" />
    <br />
    <asp:Button ID="RideBoardButton" runat="server" Text="View Membership Map" PostBackUrl="~/All/MembersMap.aspx" />
    &nbsp;<asp:Button ID="PersonalCalendarButton" runat="server" Text="Vew My Personal Calendar"
        OnClick="ViewPersonalCalendarButtonCLicked" />&nbsp;<asp:Button ID="Button3" runat="server"
            Text="View Events Coach Picked" OnClick="ViewEventsButtonCicked" />&nbsp;<asp:Button
                ID="BanquetButton" runat="server" OnClick="BanquetButtonClicked" Text="[Banquet Button]"
                Visible="False" />&nbsp;<asp:Button
                ID="JobSignupButton" runat="server" OnClick="JobSignupButtonClicked" Text="Volunteer Sign-Up"
                Visible="False" />
    <br />
    <br />
    <asp:Label ID="CurrentCreditsLabel" runat="server" Text="Current Meet Credits: "
        Visible="False"></asp:Label>
    <asp:Label ID="MeetCreditsLabel" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <h1>
        Swimmers</h1>
    <asp:GridView ID="SwimmersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        DataSourceID="SwimmersDataSource2" EnableViewState="False" Width="100%" OnRowCommand="RowButton_Clicked"
        OnDataBound="GridViewDatabound" OnRowDataBound="RowDataBound" CellPadding="4"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="FirstName" runat="server" Text='<%# Eval("PreferredName") %>'></asp:Label>
                    <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    <asp:HiddenField ID="USAIDHiddenField" runat="server" Value='<%# Eval("USAID") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" SortExpression="GroupID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("GroupID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GroupIDLabel" runat="server" OnDataBinding="GroupIDLabel_DataBinding"
                        Text='<%# Eval("GroupID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Paid" SortExpression="ReadyToAdd">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ReadyToAdd") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="ApprovedLabel" runat="server" Text='<%# Bind("ReadyToAdd") %>' OnDataBinding="ApprovedLabel_DataBinding"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Inactive" HeaderText="Status">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Inactive") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="ActiveLabel" runat="server" OnDataBinding="ActiveLabel_DataBinding"
                        Text='<%# Eval("Inactive") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Attendance">
                <HeaderTemplate>
                    <a class="hasToolTip" style="color: White; text-decoration: underline;" title="">Attendance</a>
                    <div class="toolTipLarge">
                        <table style="width: 85%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                            margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="vertical-align: middle; font-size:1.1em;">
                                    The percentages given here are only as accurate as the coach who records the attendance.
                                    There is also still room for some bugs.<br />
                                    <br />
                                    Note: If the swimmer switches groups mid-season, attendance from the original group is ignored. 
                                    If a swimmer joins a group over 2 weeks after the first practice, all the practices that the
                                    swimmer missed are not counted.
                                </td>
                            </tr>
                        </table>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="AttendanceLabel" runat="server" Text="None"></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ManageMeetsButton" runat="server" CausesValidation="false" CommandName="ManageMeet"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Manage Meets" />
                    <div class="toolTipLarge">
                        <table style="width: 85%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                            margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="vertical-align: middle;">
                                    The percentages given here are only as accurate as the coach who records the attendance.
                                    There is also still room for some bugs.<br />
                                    <br />
                                    Known bugs: If the swimmer switches groups, it ignores the attendance from the original
                                    group. Also it should recognize start dates after Sept. differently. Also attendances
                                    are recorded before a swimmer signs up on the website, but sometimes this attendance
                                    form does not factor them into the math.
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="EditSwimmerButton" runat="server" CausesValidation="false" CommandName="EditSwimmer"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Edit Swimmer" />
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <EmptyDataTemplate>
            There are currently no swimmers in this family. Click the button below to Register
            a new Swimmer.
        </EmptyDataTemplate>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSwimmersByFamilyID" TypeName="SwimmersBLL">
        <SelectParameters>
            <asp:ProfileParameter DefaultValue="-1" Name="FamilyID" PropertyName="FamilyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <p>
        <asp:Button ID="Button2" runat="server" Text="Register New Swimmer" OnClick="RegisterNewSwimmerButton_Clicked" />
    </p>
</asp:Content>
