<%@ Page Title="Pick Sessions For Swimmer" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="Sessions.aspx.cs" Inherits="Parents_Meet_Sessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Parents/RollOutSessionsDisplay.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function ()
        {
            $('input:submit').button();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="MeetNameLabel" runat="server" Font-Size="Medium"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SessionsID"
        DataSourceID="SessionsDataSource" OnSelectedIndexChanged="SelectedSessionChanged"
        OnRowDataBound="MainGridRowDatabound" OnDataBound="MainGridDatabound" Width="571px"
        OnRowCommand="GridViewRowButtonClicked">
        <Columns>
            <asp:TemplateField HeaderText="Enter">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("AM") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="EnteredCheckBox" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:CommandField SelectText="View Events Offered" ShowSelectButton="True">
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
            <asp:TemplateField HeaderText="Session" SortExpression="Session">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Session") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="SessionNumberLabel" runat="server" Text='<%# Bind("Session") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Day" SortExpression="Day">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Day") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="DayLabel" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StartTime" SortExpression="StartTime">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StartTime") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="StartTimeLabel" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                    <asp:HiddenField ID="AMHiddenField" runat="server" Value='<%# Eval("AM") %>' />
                    <asp:HiddenField ID="WarmupTimeGuessHiddenField" runat="server" Value='<%# Eval("Guess") %>' />
                    <asp:HiddenField ID="PrelimFinalHiddenField" runat="server" Value='<%# Eval("PrelimFinal") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SessionsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSessionsByMeetID" TypeName="SessionsV2BLL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="MeetID" QueryStringField="MeetID"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="SaveEntriesButton" runat="server" OnClick="SaveEntriesButtonClicked"
        Text="Save Entries" />
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="DisplayPanel" runat="server">
                <asp:HiddenField ID="SelectionNumberHiddenField" runat="server" />
                <asp:RadioButtonList ID="WhichEventsRadioButtonList" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="WhichEventsToDisplaySwitched" RepeatDirection="Horizontal"
                    Visible="False">
                    <asp:ListItem Selected="True" Value="1">View only events swimmer eligible for (ignores cut times)</asp:ListItem>
                    <asp:ListItem Value="2">View all events</asp:ListItem>
                </asp:RadioButtonList>
                <asp:HiddenField ID="LoadsHiddenField" runat="server" Value="0" />
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="EventID"
                    DataSourceID="EventsDataSource" Width="100%" OnDataBound="SessionsGridDataBound"
                    OnRowDataBound="RowDataBound" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="Event Number" SortExpression="EventNumber">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EventNumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="EventNumberLabel" runat="server" Text='<%# Bind("EventNumber") %>'></asp:Label>
                                <asp:HiddenField ID="EventNumberExtendedHiddenField" runat="server" Value='<%# Eval("EventNumberExtended") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Event" SortExpression="Distance">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Distance") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="EventDescriptionLabel" runat="server" Text='Label'></asp:Label>
                                <asp:HiddenField ID="DistanceHiddenField" runat="server" Value='<%# Eval("Distance") %>' />
                                <asp:HiddenField ID="StrokeHiddenField" runat="server" Value='<%# Eval("StrokeCode") %>' />
                                <asp:HiddenField ID="AgeCodeHiddenField" runat="server" Value='<%# Eval("AgeCode") %>' />
                                <asp:HiddenField ID="CourseHiddenField" runat="server" Value='<%# Eval("Course") %>' />
                                <asp:HiddenField ID="GenderHiddenField" runat="server" Value='<%# Eval("SexCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Faster Than Cut Time" SortExpression="SCYFastCut">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SCYFastCut") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="FastCutLabel" runat="server" Text='<%# Bind("SCYFastCut") %>'></asp:Label>
                                <asp:HiddenField ID="SCMFastCutHiddenField" runat="server" Value='<%# Eval("SCMFastCut") %>' />
                                <asp:HiddenField ID="SCMSlowCutHiddenField" runat="server" Value='<%# Eval("SCMSlowCut") %>' />
                                <asp:HiddenField ID="LCMFastCutHiddenField" runat="server" Value='<%# Eval("LCMFastCut") %>' />
                                <asp:HiddenField ID="LCMSlowCutHiddenField" runat="server" Value='<%# Eval("LCMSlowCut") %>' />
                                <asp:HiddenField ID="SCYFastCutHiddenField" runat="server" Value='<%# Eval("SCYFastCut") %>' />
                                <asp:HiddenField ID="SCYSlowCutHiddenField" runat="server" Value='<%# Eval("SCYSlowCut") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Slower Than Cut Time" SortExpression="SCYSlowCut">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SCYSlowCut") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="SlowCutLabel" runat="server" Text='<%# Bind("SCYSlowCut") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="WhichEventsRadioButtonList" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="EventsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetByMeetIDAndSessionNumber" TypeName="MeetEventsBLL">
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="MeetID" Type="Int32" DefaultValue="" />
            <asp:ControlParameter ControlID="SelectionNumberHiddenField" DefaultValue="" Name="SessionNumber"
                PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
