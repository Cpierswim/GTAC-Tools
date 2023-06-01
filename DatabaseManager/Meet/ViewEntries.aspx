<%@ Page Title="View All Meet Entries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="ViewEntries.aspx.cs" Inherits="DatabaseManager_Meet_ViewEntries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="MeetsDataSource"
        DataTextField="MeetName" DataValueField="Meet" AutoPostBack="True" OnDataBound="MeetsDropDownListDatabound">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DisplaySessionsChanged">
        <asp:ListItem Value="False">Do Not Display Sessions</asp:ListItem>
        <asp:ListItem Value="True">Display Sessions</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="SwimmerCountLabel"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="EntryCountLabel"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="EntriesDataSource"
                EmptyDataText="There are no entries for this meet." CellPadding="4" ForeColor="#333333"
                GridLines="None" OnRowDataBound="RowDataBound" OnDataBound="GridDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Event #" SortExpression="EventNumber">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EventNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="EventNumberLabel" runat="server" Text='<%# Bind("EventNumber") %>'></asp:Label>
                            <asp:Label ID="EventNumberExtendedLabel" runat="server" Text='<%# Eval("EventNumberExtended") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="AgeCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("AgeCode") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="EventLabel" runat="server" Text="Label"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entry Time" SortExpression="AutoTime">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AutoTime") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TimeLabel" runat="server" Text="TimeLabel"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="EntriesDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFullEntriesForMeet" TypeName="EntriesV2BLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="-1" Name="MeetID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
