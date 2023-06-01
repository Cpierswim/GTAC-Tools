<%@ Page MaintainScrollPositionOnPostback="true" Title="Delete Pre-Entries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DeletePreEnter.aspx.cs" Inherits="DatabaseManager_DeletePreEnter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSource1" EnableModelValidation="True" 
        ondatabound="GridDataBound" onrowdeleting="RowDeletingTest">
        <Columns>
            <asp:BoundField DataField="EntryID" HeaderText="EntryID" InsertVisible="False" 
                SortExpression="EntryID" />
            <asp:TemplateField HeaderText="Session Date and Time" 
                SortExpression="SessionID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SessionID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="SessionIDLabel" runat="server" Text='<%# Bind("SessionID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="USAID" HeaderText="USAID" SortExpression="USAID" />
            <asp:BoundField DataField="MeetID" HeaderText="MeetID" 
                SortExpression="MeetID" />
            <asp:CheckBoxField DataField="InDatabase" HeaderText="InDatabase" 
                SortExpression="InDatabase" />
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DeleteMethod="DeleteEntry"
        SelectMethod="GetEntriesByMeetAndSwimmer" TypeName="EntryBLL" 
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="EntryID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="MID" Type="Int32" />
            <asp:ControlParameter ControlID="HiddenField1" Name="USAID" 
                PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource><br />
    <asp:Button ID="Button1" runat="server" Text="Add Meet Credit To Family" 
        onclick="AddMeetCreditClicked" />
    <br /><br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/MeetEntries.aspx">&lt;&lt; Return To Meet</asp:HyperLink>
</asp:Content>

