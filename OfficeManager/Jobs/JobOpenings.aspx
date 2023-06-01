<%@ Page Title="Job Signups Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobOpenings.aspx.cs" Inherits="OfficeManager_Jobs_JobOpenings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="HeaderPanel" runat="server">
                <asp:DropDownList ID="JobEventsDropDownList" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="AddEventButton" runat="server" Text="Add Event" OnClick="PrepareToAddEvent"
                    Style="margin-left: 15px;" />
                <asp:Button ID="DeleteEventButton" runat="server" OnClick="DeleteEvent" Text="Delete Event"
                    Style="margin-left: 15px;" /><asp:Button ID="ViewPrintableButton" runat="server"
                        OnClick="ViewPrintableClicked" Text="View Printable Version" Style="margin-left: 15px;" /></asp:Panel>
            <asp:Panel ID="AddEventPanel" runat="server" Visible="false">
                Select Meet or Other:
                <asp:DropDownList ID="MeetsDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="MeetSelectionChanged">
                </asp:DropDownList>
                <asp:Label ID="SessionLabel" runat="server" Text="Session #: " Style="margin-left: 15px;"></asp:Label>
                <asp:DropDownList ID="SessionsDropDownList" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="OtherLabel" runat="server" Text="Event Name: "></asp:Label>
                <asp:TextBox ID="OtherTextBox" runat="server" Style="margin-left: 15px;" Columns="80"
                    MaxLength="250"></asp:TextBox><br />
                <br />
                <asp:Label ID="NotesLabel" runat="server" Text="Event Notes: "></asp:Label>
                <asp:TextBox ID="NotesTextBox" runat="server" Style="margin-left: 15px;" Columns="80"
                    MaxLength="2500"></asp:TextBox><br />
                <asp:Button ID="CreateNewEventButton" runat="server" Text="Create &quot;Job&quot; Event"
                    OnClick="CreateJobEvent" />
                <asp:Button ID="CancelCreateJobEventButton" runat="server" OnClick="CancelCreateJobEvent"
                    Text="Cancel" />
            </asp:Panel>
            <asp:Panel ID="JobEventsDetailPanel" runat="server">
                <asp:GridView ID="JobEventsGridView" runat="server" DataSourceID="JobSignupsObjectDataSource"
                    AutoGenerateColumns="False" DataKeyNames="JobSignUpID" OnDataBinding="BeginJobSessionsDataBind"
                    OnRowDataBound="JobSessionsRowDataBound" OnDataBound="JobSessionsDataBound" OnRowCommand="JobSignupsGridRowCommand"
                    Width="684px" EnableViewState="False" Style="margin-top: 30px;">
                    <Columns>
                        <asp:TemplateField HeaderText="Signed Up" SortExpression="FamilyID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FamilyID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="SignUpLabel" runat="server" style="margin-left:4px;"></asp:Label>
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
                <div style="margin-top: 15px;">
                    <asp:Button ID="AddJobOpeningButton" runat="server" Text="Add Job Opening" OnClick="AddJobOpeningButtonClicked" />
                    <asp:Panel ID="JobOpeningPanel" runat="server" Style="margin-top: 30px;" Visible="false">
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
                        <asp:Button ID="AddJobNumbersButton" runat="server" Text="Add" OnClick="AddJobOpenings"
                            Style="margin-left: 10px;" />
                        <asp:Button ID="CancelJobOpeningButton" runat="server" Text="Cancel" Style="margin-left: 15px;"
                            OnClick="CancelAddJobOpenings" />
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="InPrintableMode" Value="false" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="JobTypesDropDownList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="AddEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DeleteEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="MeetsDropDownList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="CreateNewEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="CancelCreateJobEventButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="JobEventsGridView" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="AddJobOpeningButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="AddJobNumbersButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="CancelJobOpeningButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ViewPrintableButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="JobSignupsObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetByJobEventID" TypeName="JobSignUpsBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="JobEventsDropDownList" DefaultValue="-1" Name="JobEventID"
                PropertyName="SelectedValue" Type="Int32" />
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
</asp:Content>
