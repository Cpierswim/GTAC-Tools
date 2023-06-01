<%@ Page MaintainScrollPositionOnPostback="true" Title="Family Meet Manager" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MeetManager.aspx.cs" Inherits="Parents_MeetManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="AvailableCreditsLabel" runat="server" 
        Text="Available Meet Credits: " Visible="False"></asp:Label>
    <asp:HiddenField ID="USAIDHiddenField" runat="server" />
    <asp:HiddenField ID="MeetCreditsHiddenField" runat="server" />
    <br />
    <br />
    Meets Open For Guaranteed Entry:<br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="MeetID"
        DataSourceID="MeetsDataSource" EnableModelValidation="True" Width="100%" OnDataBound="SignupGridDataBound"
        OnRowCommand="SignUpButtonClicked" onrowdatabound="SetMeetDates">
        <Columns>
            <asp:TemplateField HeaderText="Meet Name" SortExpression="MeetName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MeetName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MeetName") %>'></asp:Label>
                    <asp:HiddenField ID="MeetIDHiddenField" runat="server" Value='<%# Eval("MeetID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Meet Dates">
                <ItemTemplate>
                    <asp:Label ID="MeetDatesLabel" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GuaranteeDeadline" HeaderText="Guarantee Deadline" SortExpression="GuaranteeDeadline"
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="MeetNotes" HeaderText="Notes" SortExpression="MeetNotes" />
            <asp:BoundField DataField="MeetLocation" HeaderText="Location" SortExpression="MeetLocation" />
            <asp:ButtonField ButtonType="Button" Text="Sign Up For Meet">
                <ItemStyle Width="20px" />
            </asp:ButtonField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="MeetsDataSource" runat="server" SelectMethod="GetOpenMeetsPriorToGuaranteeDeadline"
        TypeName="MeetsBLL"></asp:ObjectDataSource>
    <br />
    <br />
    Meets Open For LATE Entry: (No guarantee you can get in, even if you sign up. I
    will try, but meet hosts can refuse late entries.)<br />
    
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="MeetID"
        DataSourceID="ObjectDataSource1" EnableModelValidation="True" Width="100%" OnDataBound="SignupGridDataBound"
        OnRowCommand="SignUpButtonClicked" onrowdatabound="SetMeetDates">
        <Columns>
            <asp:TemplateField HeaderText="Meet Name" SortExpression="MeetName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MeetName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MeetName") %>'></asp:Label>
                    <asp:HiddenField ID="MeetIDHiddenField" runat="server" Value='<%# Eval("MeetID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Meet Dates">
                <ItemTemplate>
                    <asp:Label ID="MeetDatesLabel" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LateEntryDeadline" DataFormatString="{0:d}" HeaderText="Late Entry Deadline"
                SortExpression="LateEntryDeadline" />
            <asp:BoundField DataField="MeetNotes" HeaderText="Notes" SortExpression="MeetNotes" />
            <asp:BoundField DataField="MeetLocation" HeaderText="Location" SortExpression="MeetLocation" />
            <asp:ButtonField ButtonType="Button" Text="Sign Up For Meet">
                <ItemStyle Width="20px" />
            </asp:ButtonField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetOpenMeetsAvailableForLateEntry"
        TypeName="MeetsBLL"></asp:ObjectDataSource>
</asp:Content>
