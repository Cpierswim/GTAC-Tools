<%@ Page Title="Meet Pre-Entries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="PreEntered.aspx.cs" Inherits="DatabaseManager_Meet_PreEntered" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="MeetsDataSource"
        DataTextField="MeetName" DataValueField="Meet" OnDataBound="MeetsDropDownListDataBound">
    </asp:DropDownList>
    
    <br />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Button ID="Button1" runat="server" Text="Enter Swimer in Meet" 
                onclick="EnterSwimmerInMeetClicked" />
            <br />
            <br />
            Entries:
            <asp:Label runat="server" ID="EntryCountLabel" Text="0"></asp:Label><br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PreEntryID"
                DataSourceID="PreEnteredDataSource" OnDataBinding="GridViewDataBinding" OnRowDataBound="RowDataBound"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" GridLines="Vertical" OnRowCommand="ButtonClicked" OnDataBound="GridViewDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Name" SortExpression="USAID">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("USAID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="USAIDHiddenField" runat="server" Value='<%# Bind("USAID") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="PreEntryIDHiddenField" runat="server" Value='<%# Bind("PreEntryID") %>'>
                            </asp:HiddenField>
                            <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sessions" SortExpression="Session1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Session1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Table ID="SessionDisplayTable" runat="server">
                            </asp:Table>
                            <asp:HiddenField ID="Session1HiddenField" runat="server" Value='<%# Bind("Session1") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session2HiddenField" runat="server" Value='<%# Bind("Session2") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session3HiddenField" runat="server" Value='<%# Bind("Session3") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session4HiddenField" runat="server" Value='<%# Bind("Session4") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session5HiddenField" runat="server" Value='<%# Bind("Session5") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session6HiddenField" runat="server" Value='<%# Bind("Session6") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session7HiddenField" runat="server" Value='<%# Bind("Session7") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session8HiddenField" runat="server" Value='<%# Bind("Session8") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session9HiddenField" runat="server" Value='<%# Bind("Session9") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="Session10HiddenField" runat="server" Value='<%# Bind("Session10") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="PreEnteredHiddenField" runat="server" Value='<%# Bind("PreEntered") %>'>
                            </asp:HiddenField>
                            <asp:HiddenField ID="NotAttendingHiddenField" runat="server" Value='<%# Bind("NotAttending") %>'>
                            </asp:HiddenField>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="In Database" SortExpression="IsInDatabase">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsInDatabase") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="InDatabaseCheckBox" runat="server" Checked='<%# Bind("IsInDatabase") %>'
                                Enabled="false" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Button" CommandName="InDatabase" Text="Set as In Database" />
                    <asp:ButtonField ButtonType="Button" CommandName="Remove" Text="Remove From Sessions" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="PreEnteredDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPreEntriesByMeetID" TypeName="PreEnteredV2BLL" OnSelected="DataSelected">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="-1" Name="MeetID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
    <br />
</asp:Content>
