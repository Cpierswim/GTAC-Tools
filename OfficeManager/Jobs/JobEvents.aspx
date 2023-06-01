<%@ Page Title="Jobs Setup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobEvents.aspx.cs" Inherits="OfficeManager_Jobs_JobEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="EventChanged">
            </asp:DropDownList>
            <span style="margin-left: 10px;">
                <asp:Button ID="AddMeetsButton" Text="Add Meet & Session" runat="server" OnClick="AddMeetButtonClicked" />
            </span><span style="margin-left: 10px;">
                <asp:Button ID="AddOtherButton" Text="Add Other Event" runat="server" OnClick="AddOtherEventClicked" /></span>
            <span style="margin-left: 10px;">
                <asp:Button ID="DeleteEventButton" runat="server" Text="Delete Event" OnClick="DeleteEvent" /></span>
            <asp:Panel ID="AddOtherPanel" runat="server" Visible="false">
                Event Name:
                <asp:TextBox ID="OtherEventNameTextBox" runat="server" MaxLength="250"></asp:TextBox><br />
                Notes:
                <asp:TextBox ID="OtherEventNotesTextBox" runat="server" MaxLength="2500" Columns="80"></asp:TextBox><br />
                <asp:Button ID="AddOtherEventButton" runat="server" Text="Add" OnClick="AddOtherEvent" />
            </asp:Panel>
            <asp:Panel ID="AddMeetPanel" runat="server" Visible="False">
                Meet:
                <asp:DropDownList ID="MeetsDropDownList" runat="server" DataSourceID="MeetsDataSource"
                    DataTextField="MeetName" DataValueField="Meet" AutoPostBack="True" OnSelectedIndexChanged="MeetIndexChanged">
                </asp:DropDownList>
                <span style="margin-left: 10px;">Session:
                    <asp:DropDownList ID="SessionsDropDownList" runat="server" DataSourceID="SessionsDataSource"
                        DataTextField="Session" DataValueField="Session">
                    </asp:DropDownList>
                </span>
                <br />
                Notes:
                <asp:TextBox ID="NotesTextBox" runat="server" Columns="80" MaxLength="2500"></asp:TextBox><span
                    style="margin-left: 10px;">
                    <asp:Button ID="AddMeetSessionButton" runat="server" OnClick="AddMeetAndSession"
                        Text="Add" /></span>
            </asp:Panel>
            <asp:Panel ID="AddJobsPanel" runat="server" Style="margin-top: 30px;">
                <asp:GridView ID="JobSignupsGridView" runat="server" DataSourceID="JobsDataSource"
                    AutoGenerateColumns="False" DataKeyNames="JobSignUpID" OnDataBinding="BeginJobSessionsDataBind"
                    OnRowDataBound="JobSessionsRowDataBound" OnDataBound="JobSessionsDataBound" OnRowCommand="JobSignupsGridRowCommand"
                    Width="684px" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Signed Up" SortExpression="FamilyID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FamilyID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="SignUpLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="JobSignUpIDHiddenField" runat="server" Value='<%# Bind("JobSignUpID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="Remove" Text="Remove Sign-Up">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="DeleteJob" Text="Delete Job">
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                        </asp:ButtonField>
                    </Columns>
                    <EmptyDataTemplate>
                        No jobs assigned to event.</EmptyDataTemplate>
                </asp:GridView>
                <br />
                <span style="margin-left: 10px;">
                    <asp:Button ID="AddJobsButton" runat="server" Text="Add Jobs" OnClick="AddJobsButtonClicked" /></span>
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="false" OnClick="CancelButtonClicked" />
                <asp:Panel ID="AddJobPanel" runat="server" Style="margin-top: 25px;" ViewStateMode="Disabled"
                    Visible="False">
                    Job Type:
                    <asp:DropDownList ID="JobTypesDropDownList" runat="server" DataSourceID="JobTypesDataSource"
                        DataTextField="Name" DataValueField="JobTypeID">
                    </asp:DropDownList>
                    <span style="margin-left: 10px;">Number:
                        <asp:DropDownList ID="NumberDropDownList" runat="server">
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                    <asp:Button ID="AddJobNumbersButton" runat="server" Text="Add" OnClick="AddJobs"
                        Style="margin-left: 10px;" />
                </asp:Panel>
            </asp:Panel>
            <%--
    <asp:HiddenField ID="MeetIDHiddenField" runat="server" />
    <asp:HiddenField ID="SessionNumberHiddenField" runat="server" />
    <asp:HiddenField ID="OtherHiddenField" runat="server" />--%>
            <asp:HiddenField ID="JobEventIDHiddenField" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="AddMeetsButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AddOtherButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DeleteEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AddOtherEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="MeetsDropDownList" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="AddMeetSessionButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="JobSignupsGridView" 
                EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="AddJobsButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="CancelButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AddJobNumbersButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="JobSignupsGridView" 
                EventName="DataBound" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="JobsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetByJobEventID" TypeName="JobSignUpsBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="JobEventIDHiddenField" Name="JobEventID" PropertyName="Value"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="JobTypesDataSource" runat="server" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" TypeName="JobTypesBLL"
        UpdateMethod="Update">
        <InsertParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TimeToLearn" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TimeToLearn" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="JobTypeID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetAllMeets" TypeName="MeetsV2BLL"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SessionsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSessionsByMeetID" TypeName="SessionsV2BLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="MeetsDropDownList" DefaultValue="-1" Name="MeetID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
