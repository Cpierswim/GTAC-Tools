<%@ Page Title="Signup for Volunteer Jobs" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="JobSignup.aspx.cs" Inherits="Parents_JobSignup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 57%;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckToEnableAddButton() {
            var AddButton = $("#ctl00_MainContent_AddButton").get(0);
            var TextBox = $("#ctl00_MainContent_OtherTextBox").get(0);
            var trimmed = TextBox.value.replace(/^\s+|\s+$/g, '');
            if (trimmed.length == 0)
                AddButton.disabled = true;
            else
                AddButton.disabled = false;
        }

//        $(function () {

//            $(".hasToolTip").tooltip({ effect: 'slide' });

        //        });

//        $("#mytable img").tooltip({

//		// each trashcan image works as a trigger
//		tip: '#tooltip',

//		// custom positioning
//		position: 'center right',

//		// move tooltip a little bit to the right
//		offset: [0, 15],

//		// there is no delay when the mouse is moved away from the trigger
//		delay: 0
//	});

//$("#ComputerOperatorJobHeaderHyperLink").tooltip({
//	tip: '#ComputerOperatorTT'
//	position: 'center right',
//	offset: [0, 15],
//	delay: 0});

//$("#TimerJobHeaderHyperLink").tooltip({
//	tip: '#TimerTT'
//	position: 'center right',
//	offset: [0, 15],
//	delay: 0});
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
        .tooltip table
        {
            margin-left: 30px;
            margin-right: 30px;
            margin-top: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="JobEventDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="EventChanged">
    </asp:DropDownList>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="ToolTipPanel" runat="server">
            </asp:Panel>
            <br />
            <br />
            <asp:Label ID="NotesLabel" runat="server"></asp:Label>
            <br />
            <br />
            <table style="width: 100%">
                <tr>
                    <td class="style2" style="vertical-align: top;">
                        Hover over the job name for a description.
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="JobSignUpID"
                            DataSourceID="JobSignupsDataSource" OnDataBinding="BeginJobSessionsDataBind"
                            OnRowDataBound="JobSessionsRowDataBound" EnableViewState="False" OnDataBound="JobSessionsDataBound"
                            OnRowCommand="GridViewRowCommand" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Available Jobs" SortExpression="FamilyID">
                                    <ItemTemplate>
                                        <asp:Label ID="SignUpLabel" runat="server"></asp:Label>
                                        <asp:HiddenField ID="JobSignupIDHiddenField" runat="server" Value='<%# Bind("JobSignUpID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField Text="Sign Up For Job" />
                            </Columns>
                            <EmptyDataTemplate>
                                There are no jobs available for this event.</EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                    <td style="width: 35%; vertical-align: top;">
                        <asp:Panel ID="SignupPanel" runat="server" Visible="False" BackColor="#FFFF99">
                            <asp:DropDownList ID="AddTypeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeDropDownListChanged">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="FAM">Add Family</asp:ListItem>
                                <asp:ListItem Value="SWI">Add Swimmer</asp:ListItem>
                                <asp:ListItem Value="PAR">Add Parent</asp:ListItem>
                                <asp:ListItem Value="OTH">Add Other</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;- Pick who to sign up<br />
                            <br />
                            <asp:DropDownList ID="ToAddDropDownList" runat="server" Visible="False" AutoPostBack="True"
                                OnSelectedIndexChanged="ToAddDropDownListSelectedItemChanged">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <table style="width: 200px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="OtherLabel" runat="server" Text="Name: " Visible="False" Style="float: right;"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="OtherTextBox" runat="server" Columns="35" MaxLength="150" Visible="False"
                                            Style="float: left;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButtonClicked" Visible="False" />
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelClicked"
                                Style="margin-left: 5px;"  />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="SelectedRowHiddenField" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="JobEventDropDownList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="AddTypeDropDownList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ToAddDropDownList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="AddButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="CancelButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="JobSignupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetByEmptySignupsOrFamilyID" TypeName="JobSignUpsBLL" DeleteMethod="Delete">
        <DeleteParameters>
            <asp:Parameter Name="JobSignUpID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="JobEventDropDownList" Name="JobEventID" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ProfileParameter DefaultValue="" Name="FamilyID" PropertyName="FamilyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
