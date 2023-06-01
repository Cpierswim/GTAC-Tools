<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MeetSignupsList.aspx.cs" Inherits="Coach_MeetSignupsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="GroupSelectPanel" runat="server">
                Select group:<br />
                <asp:DropDownList ID="GroupsDropDownList" runat="server" AutoPostBack="True" DataSourceID="GroupsDatasource"
                    DataTextField="GroupName" DataValueField="GroupID" OnSelectedIndexChanged="GroupPicked">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="GroupsDatasource" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
            </asp:Panel>
            <asp:Panel ID="MeetSelectPanel" runat="server" Visible="false">
                Select meets:<br />
                <br />
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="MeetsDatasource"
                    DataTextField="MeetName" DataValueField="Meet">
                </asp:CheckBoxList>
                <br />
                <br />
                <asp:Button ID="LoadButton" runat="server" OnClick="LoadMeets" Text="Load Signup List" />
            </asp:Panel>
            <asp:Panel ID="ShowPanel" runat="server" Visible="false">
                <asp:Table ID="SignupsTable" runat="server" CellPadding="7" CellSpacing="0">
                </asp:Table>
                <br />
                <br />
                <asp:Button ID="RestartButton" runat="server" Text="Start Over" OnClick="StartOverClicked" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MeetsDatasource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMeetsThatEndAfterToday" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
</asp:Content>
