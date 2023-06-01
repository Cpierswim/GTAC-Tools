<%@ Page MaintainScrollPositionOnPostback="true" Title="Manage Meets" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ManageMeets.aspx.cs" Inherits="DatabaseManager_ManageMeets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="MeetID"
        DataSourceID="MeetsDetailDatasource" EnableModelValidation="True" Height="50px"
        Width="100%" OnItemDeleted="MeetDeleted" OnItemUpdated="MeetUpdated">
        <Fields>
            <asp:BoundField DataField="MeetName" HeaderText="Meet Name:" 
                SortExpression="MeetName" >
            <HeaderStyle Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="GuaranteeDeadline" HeaderText="Guarantee Deadline:" 
                SortExpression="GuaranteeDeadline" />
            <asp:BoundField DataField="LateEntryDeadline" HeaderText="Late Entry Deadline:" 
                SortExpression="LateEntryDeadline" />
            <asp:BoundField DataField="ChangeDeadline" HeaderText="Change Deadline:" 
                SortExpression="ChangeDeadline" />
            <asp:CheckBoxField DataField="Closed" HeaderText="Closed:" 
                SortExpression="Closed" />
            <asp:BoundField DataField="MeetNotes" HeaderText="Notes:" 
                SortExpression="MeetNotes" />
            <asp:BoundField DataField="MeetLocation" HeaderText="Location:" 
                SortExpression="MeetLocation" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Fields>
    </asp:DetailsView>
    <br />
    <br />
    <asp:ObjectDataSource ID="MeetsDetailDatasource" runat="server" DeleteMethod="DeleteMeet"
        SelectMethod="GetMeetByMeetID" TypeName="MeetsBLL" UpdateMethod="UpdateMeet">
        <DeleteParameters>
            <asp:Parameter Name="MeetID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="MeetID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="GuaranteeDeadline" Type="DateTime" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="ChangeDeadline" Type="DateTime" />
            <asp:Parameter Name="Closed" Type="Boolean" />
            <asp:Parameter Name="MeetNotes" Type="String" />
            <asp:Parameter Name="MeetLocation" Type="String" />
            <asp:Parameter Name="MeetID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="MeetID"
        DataSourceID="MeetsDataSource" EnableModelValidation="True" Width="100%">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="MeetName" HeaderText="Meet Name" SortExpression="MeetName" />
            <asp:BoundField DataField="GuaranteeDeadline" HeaderText="Guarantee Deadline" SortExpression="GuaranteeDeadline"
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="LateEntryDeadline" HeaderText="Late Entry Deadline" SortExpression="LateEntryDeadline"
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="ChangeDeadline" HeaderText="Change Deadline" SortExpression="ChangeDeadline"
                DataFormatString="{0:d}" />
            <asp:CheckBoxField DataField="Closed" HeaderText="Closed" SortExpression="Closed" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" DeleteMethod="DeleteMeet"
        InsertMethod="CreateMeet" SelectMethod="GetAllMeets" TypeName="MeetsBLL" UpdateMethod="UpdateMeet">
        <DeleteParameters>
            <asp:Parameter Name="MeetID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="GuaranteeDeadline" Type="DateTime" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="ChangeDeadline" Type="DateTime" />
            <asp:Parameter Name="MeetNotes" Type="String" />
            <asp:Parameter Name="MeetLocation" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="MeetName" Type="String" />
            <asp:Parameter Name="GuaranteeDeadline" Type="DateTime" />
            <asp:Parameter Name="LateEntryDeadline" Type="DateTime" />
            <asp:Parameter Name="ChangeDeadline" Type="DateTime" />
            <asp:Parameter Name="Closed" Type="Boolean" />
            <asp:Parameter Name="MeetNotes" Type="String" />
            <asp:Parameter Name="MeetLocation" Type="String" />
            <asp:Parameter Name="MeetID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
