<%@ Page MaintainScrollPositionOnPostback="true" Title="Add Meet" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddMeet.aspx.cs" Inherits="DatabaseManager_AddMeet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text="Label" 
        Visible="False"></asp:Label>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="MeetID"
        DataSourceID="ObjectDataSource1" DefaultMode="Insert" EnableModelValidation="True"
        Height="100px" Width="718px" oniteminserting="DetailsViewInsertingMeet">
        <CommandRowStyle HorizontalAlign="Center" />
        <Fields>
            <asp:BoundField DataField="MeetID" HeaderText="MeetID" InsertVisible="False" ReadOnly="True"
                SortExpression="MeetID" />
            <asp:TemplateField HeaderText="Meet Name:" SortExpression="MeetName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MeetName") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="MeetNameTextBox" runat="server" Text='<%# Bind("MeetName") %>' MaxLength="300"
                        Columns="100"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MeetName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Guarantee Deadline:" SortExpression="GuaranteeDeadline">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("GuaranteeDeadline") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        SelectedDate='<%# Bind("GuaranteeDeadline") %>' Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("GuaranteeDeadline") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Late Entry Deadline:" SortExpression="LateEntryDeadline">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LateEntryDeadline") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:Calendar ID="Calendar2" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        SelectedDate='<%# Bind("LateEntryDeadline") %>' Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("LateEntryDeadline") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Change Deadline:" SortExpression="ChangeDeadline">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ChangeDeadline") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:Calendar ID="Calendar3" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        SelectedDate='<%# Bind("ChangeDeadline") %>' Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ChangeDeadline") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes:" SortExpression="MeetNotes">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("MeetNotes") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="MeetNotesTextBox" runat="server" Text='<%# Bind("MeetNotes") %>'
                        MaxLength="500" Columns="100" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("MeetNotes") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location:" SortExpression="MeetLocation">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("MeetLocation") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="MeetLocationTextBox" runat="server" Text='<%# Bind("MeetLocation") %>'
                        MaxLength="100" Columns="100"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("MeetLocation") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <InsertItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Insert"
                        Font-Size="Larger" Text="Insert"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        PostBackUrl="~/Features/Default.aspx" Text="Cancel"></asp:LinkButton>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="New"
                        Text="New"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="CreateMeet"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllMeets" TypeName="MeetsBLL"
        OnInserted="MeetCreated">
        <InsertParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="GuaranteeDeadline" Type="DateTime" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="ChangeDeadline" Type="DateTime" />
            <asp:Parameter Name="MeetNotes" Type="String" />
            <asp:Parameter Name="MeetLocation" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
