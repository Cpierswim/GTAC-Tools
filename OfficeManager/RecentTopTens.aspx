<%@ Page MaintainScrollPositionOnPostback="true" Title="Recent Top Tens" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RecentTopTens.aspx.cs"
    Inherits="OfficeManager_RecentTopTens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    Use this Calendar to pick the last day you printed the Top 10 cards. All the Top
    10's since that date will be displayed. (This always defaults to the first day of
    the current month the first time you visit the page.)
    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="PrintableVersionClicked" Text="Printable Version" /><asp:Label
        ID="OnlyWorksLabel" runat="server" Text="
    &nbsp;&lt;--Only works correctly in Internet Explorer"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="RecordID" DataSourceID="RecordsDataSource" EnableModelValidation="True"
        ForeColor="#333333" GridLines="None" OnRowDataBound="FormatRow" Width="100%"
        OnDataBound="GridDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="Preferred">
                <EditItemTemplate>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Preferred") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Preferred") %>'></asp:Label>
                    <asp:HiddenField ID="LastNameHiddenField" runat="server" Value='<%#Eval("Last") %>' />
                    <asp:HiddenField ID="FirstNameHiddenField" runat="server" Value='<%#Eval("First") %>' />
                    <asp:HiddenField ID="PreferredNameHiddenField" runat="server" Value='<%#Eval("Preferred") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sex" SortExpression="Sex">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sex") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="SexLabel" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
            <asp:TemplateField HeaderText="Event" SortExpression="Distance">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Distance") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EventLabel" runat="server" Text='<%# Bind("Distance") %>'></asp:Label>
                    <asp:HiddenField ID="DistanceHiddenField" runat="server" Value='<%# Eval("Distance") %>' />
                    <asp:HiddenField ID="StrokeHiddenField" runat="server" Value='<%# Eval("Stroke") %>' />
                    <asp:HiddenField ID="CourseHiddenField" runat="server" Value='<%# Eval("Course") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time" SortExpression="Time">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Time") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="TimeLabel" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:BoundField DataField="Date" DataFormatString="{0:d}" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="MeetName" HeaderText="Meet Name" SortExpression="MeetName" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    <asp:ObjectDataSource ID="RecordsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTopTensByDate" TypeName="RecordsBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="Calendar1" Name="SinceDate" PropertyName="SelectedDate"
                Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
