<%@ Page Title="Meet Editor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="MeetEditor.aspx.cs" Inherits="DatabaseManager_Meet_MeetEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:GridView ID="MeetsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Meet"
        DataSourceID="MeetDataSource" OnRowCommand="EditSessionsClicked" OnRowDataBound="RowDataBound"
        Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        BorderWidth="1px" CellPadding="3" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MeetName" HeaderText="Meet" SortExpression="MeetName">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Start" DataFormatString="{0:%M/d}" HeaderText="Start Date"
                ReadOnly="True" SortExpression="Start">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="EndDate" DataFormatString="{0:M/d}" HeaderText="End Date"
                ReadOnly="True" SortExpression="EndDate">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Deadline" DataFormatString="{0:M/d}" HeaderText="Deadline"
                SortExpression="Deadline">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="LateEntryDeadline" DataFormatString="{0:M/d}" HeaderText="Late Entry Deadline"
                SortExpression="LateEntryDeadline">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Email Sent" SortExpression="OpenForCoaches">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("OpenForCoaches") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="OpenForCoachesLabel" runat="server" Text='<%# Eval("OpenForCoaches") %>'></asp:Label>
                    <asp:HiddenField ID="MeetIDHiddenField" runat="server" Value='<%# Eval("Meet") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:ButtonField CommandName="Meet" Text="View Meet">
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
            <asp:ButtonField CommandName="Sessions" Text="View Sessions">
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="IndividualMeetDataSource"
                GridLines="Horizontal" DataKeyNames="Meet" Width="100%" OnDataBound="MeetDetailsViewDataBound">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <Fields>
                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Meet Name" SortExpression="MeetName">
                        <EditItemTemplate>
                            <asp:TextBox ID="MeetNameTextBox" Columns="45" MaxLength="45" runat="server" Text='<%# Bind("MeetName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("MeetName") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("MeetName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Start" DataFormatString="{0:dddd MMMM, d yyyy}" HeaderText="Start Date"
                        ReadOnly="True" SortExpression="Start">
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EndDate" DataFormatString="{0:dddd MMMM, d yyyy}" HeaderText="EndDate"
                        ReadOnly="True" SortExpression="EndDate">
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Course" SortExpression="Course" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Bind("Course") %>'></asp:Label>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Course") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="CourseLabel" runat="server" Text='<%# Bind("Course") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max Ind Ent" SortExpression="MaxIndEnt" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:Label ID="MaxIndEntLabel" runat="server" Text='<%# Bind("MaxIndEnt") %>'></asp:Label>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("MaxIndEnt") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MaxIndEntLabel" runat="server" Text='<%# Bind("MaxIndEnt") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max Rel Ent" SortExpression="MaxRelEnt" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:Label ID="MaxRelEntLabel" runat="server" Text='<%# Bind("MaxRelEnt") %>'></asp:Label>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("MaxRelEnt") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MaxRelEntLabel" runat="server" Text='<%# Bind("MaxRelEnt") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max Total Entries" SortExpression="MaxEnt" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:Label ID="MaxEntLabel" runat="server" Text='<%# Bind("MaxEnt") %>'></asp:Label>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("MaxEnt") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="MaxEntLabel" runat="server" Text='<%# Bind("MaxEnt") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Location" SortExpression="Location">
                        <EditItemTemplate>
                            <asp:TextBox ID="LocationTextBox" MaxLength="45" Columns="45" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                        <ItemStyle Width="100%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Short Remarks" SortExpression="Remarks" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="RemarksTextBox" MaxLength="50" Columns="50" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                        <ItemStyle Width="100%" />
                    </asp:TemplateField>
                    <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Instructions" SortExpression="Instructions">
                        <EditItemTemplate>
                            <asp:TextBox ID="InstructionsTextBox" MaxLength="250" runat="server" Text='<%# Bind("Instructions") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Instructions") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Instructions") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="99%" />
                        <HeaderStyle Font-Bold="True" Width="225px" />
                        <ItemStyle Width="100%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Deadline" SortExpression="Deadline">
                        <EditItemTemplate>
                            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Black" Height="180px" SelectedDate='<%# Bind("Deadline") %>' Width="200px">
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                            </asp:Calendar>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Deadline", "{0:dddd MMMM, d yyyy}") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Deadline", "{0:dddd MMMM, d yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Late Entry Deadline" SortExpression="LateEntryDeadline">
                        <EditItemTemplate>
                            <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Black" Height="180px" SelectedDate='<%# Bind("LateEntryDeadline") %>'
                                Width="200px">
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                            </asp:Calendar>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LateEntryDeadline", "{0:dddd MMMM, d yyyy}") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("LateEntryDeadline", "{0:dddd MMMM, d yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email Sent" SortExpression="OpenForCoaches">
                        <EditItemTemplate>
                            <asp:DropDownList ID="OpenForCoachesDropDownList" runat="server" SelectedValue='<%# Bind("OpenForCoaches") %>'>
                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                <asp:ListItem Value="False">No</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("OpenForCoaches") %>' />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="OpenForCoachesLabel" runat="server" Text='<%# Bind("OpenForCoaches") %>' />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Width="225px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Coach Notes" SortExpression="CoachNotes" 
                        ConvertEmptyStringToNull="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="500" 
                                Text='<%# Bind("CoachNotes") %>' Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("CoachNotes") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("CoachNotes") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="99%" />
                        <HeaderStyle Font-Bold="True" Width="225px" />
                        <ItemStyle Width="100%" />
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Fields>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            </asp:DetailsView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemUpdated" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="DataBound" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="DataBinding" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemCreated" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="DataBinding" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="DataBound" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="Init" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="Load" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" 
                EventName="PageIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" 
                EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="PreRender" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" 
                EventName="RowCancelingEdit" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowCreated" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowDeleted" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowEditing" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowUpdated" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="RowUpdating" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" 
                EventName="SelectedIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="Sorted" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="Sorting" />
            <asp:AsyncPostBackTrigger ControlID="MeetsGridView" EventName="Unload" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="Init" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemCreated" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemDeleted" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemDeleting" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemInserted" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemInserting" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ItemUpdating" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="Load" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ModeChanged" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="ModeChanging" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" 
                EventName="PageIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" 
                EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="DetailsView1" EventName="PreRender" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <asp:GridView ID="SessionsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="SessionsID"
                DataSourceID="SessionsDataSource" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Width="100%" OnRowDataBound="SessionsRowDataBound">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="Session" HeaderText="Session Number" SortExpression="Session"
                        ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Max Session Entries" SortExpression="MaxInd">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MaxInd") %>' MaxLength="2" Columns="2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("MaxInd") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Day" HeaderText="Day" SortExpression="Day" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Start Time" SortExpression="StartTime">
                        <EditItemTemplate>
                            <asp:TextBox ID="StartTimeTextBox" Columns="5" runat="server" Text='<%# Bind("StartTime") %>'
                                MaxLength="5">
                            </asp:TextBox><asp:DropDownList ID="AMPMDropDownList" runat="server" SelectedValue='<%# Bind("AM") %>'>
                                <asp:ListItem Value="True">AM</asp:ListItem>
                                <asp:ListItem Value="False">PM</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="StartTimeLabel" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                            <asp:Label ID="AMLabel" runat="server" Text='<%# Bind("AM") %>' Enabled="false" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Guess Time" SortExpression="Guess">
                        <EditItemTemplate>
                            <asp:DropDownList ID="GuessTimeDropDownList" runat="server" SelectedValue='<%# Bind("Guess") %>'>
                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                <asp:ListItem Value="False">No</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="GuessTimeLabel" runat="server" Text='<%# Bind("Guess") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prelim/Final" SortExpression="PrelimFinal">
                        <EditItemTemplate>
                            <asp:DropDownList ID="PrelimFinalDropDownList" runat="server" SelectedValue='<%# Bind("PrelimFinal") %>'>
                                <asp:ListItem Value="True">Yes</asp:ListItem>
                                <asp:ListItem Value="False">No</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="PrelimFinalLabel" runat="server" Text='<%# Bind("PrelimFinal") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SessionsGridView" EventName="RowUpdated" />
            <asp:AsyncPostBackTrigger ControlID="SessionsGridView" 
                EventName="RowUpdating" />
            <asp:AsyncPostBackTrigger ControlID="SessionsGridView" EventName="DataBound" />
            <asp:AsyncPostBackTrigger ControlID="SessionsGridView" EventName="DataBound" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MeetDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsV2BLL" UpdateMethod="UpdateRow">
        <UpdateParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="Start" Type="DateTime" />
            <asp:Parameter Name="EndDate" Type="DateTime" />
            <asp:Parameter Name="Course" Type="String" />
            <asp:Parameter Name="Location" Type="String" />
            <asp:Parameter Name="Remarks" Type="String" />
            <asp:Parameter Name="MaxIndEnt" Type="Int32" />
            <asp:Parameter Name="MaxRelEnt" Type="Int32" />
            <asp:Parameter Name="MaxEnt" Type="Int32" />
            <asp:Parameter Name="Instructions" Type="String" />
            <asp:Parameter Name="Deadline" Type="DateTime" />
            <asp:Parameter Name="OpenForCoaches" Type="Boolean" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="Original_Meet" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SessionsDataSource" runat="server" SelectMethod="GetSessionsByMeetID"
        TypeName="SessionsV2BLL" UpdateMethod="Update">
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="ES" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="StartTime" Type="String" />
            <asp:Parameter Name="AM" Type="Boolean" />
            <asp:Parameter Name="Guess" Type="Boolean" />
            <asp:Parameter Name="SessionsID" Type="Int32" />
            <asp:Parameter Name="PrelimFinal" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="IndividualMeetDataSource" runat="server" SelectMethod="GetMeetByMeetID"
        TypeName="MeetsV2BLL" UpdateMethod="UpdateRow" OnUpdated="RowUpdated" OnUpdating="MeetUpdating">
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="EM" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="Location" Type="String" />
            <asp:Parameter Name="Remarks" Type="String" />
            <asp:Parameter Name="Instructions" Type="String" />
            <asp:Parameter Name="Deadline" Type="DateTime" />
            <asp:Parameter Name="OpenForCoaches" Type="Boolean" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="Meet" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
