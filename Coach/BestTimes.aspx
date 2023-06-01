<%@ Page Title="Best Times" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BestTimes.aspx.cs" Inherits="Coach_BestTimes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="GroupsDataSource"
        DataTextField="GroupName" DataValueField="GroupID" AutoPostBack="True">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
        <asp:ListItem Value="Y">Yards</asp:ListItem>
        <asp:ListItem Value="L">Long Course Meters</asp:ListItem>
        <asp:ListItem Value="S">Short Course Meters</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="BestTimeID,USAID" DataSourceID="BestTimesDataSource" ForeColor="#333333"
        GridLines="None" OnRowDataBound="RowDataBound" OnDataBound="GridDatabound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Event" SortExpression="Distance">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Distance") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EventDescriptionLabel" runat="server" Text='<%# Bind("Distance") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time" SortExpression="Score">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Score") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="TimeLabel" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:ObjectDataSource ID="BestTimesDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBestTimesByGroupIDOrderedForDisplay" TypeName="BestTimesBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="-1" Name="GroupID"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownList2" DefaultValue="Y" Name="Course"
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
