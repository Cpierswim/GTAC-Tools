<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ViewAttendance.aspx.cs" Inherits="Coach_ViewAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="GroupsDataSource"
                DataTextField="GroupName" DataValueField="GroupID" OnDataBound="DropDownListDataBound">
            </asp:DropDownList>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="USAID" DataSourceID="AttendanceDataSource" ForeColor="#333333"
                GridLines="None" ondatabound="DataBound" onrowdatabound="RowDataBound" 
                Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Percentage" HeaderText="Percentage" 
                        SortExpression="Percentage" DataFormatString="{0:P1}" >
                    <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Attended" HeaderText="Attended" 
                        SortExpression="Attended" >
                    <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Offered" HeaderText="Offered" 
                        SortExpression="Offered" >
                    <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Yardage" HeaderText="Yardage" 
                        SortExpression="Yardage" DataFormatString="{0:N0}" >
                    <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Meterage" HeaderText="Meterage" 
                        SortExpression="Meterage" DataFormatString="{0:N0}" >
                    <ItemStyle Font-Names="Courier New" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
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
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="AttendanceDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAttendancesTableForGroup" TypeName="AttendanceBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="GroupID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
</asp:Content>
