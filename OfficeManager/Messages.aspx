<%@ Page MaintainScrollPositionOnPostback="true" Title="Recent Updates Messages - Office Manager" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="DatabaseManager_Messages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="MessageID" DataSourceID="MessageDataSource" 
        onrowcommand="DeleteMessageClicked" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Message No:" InsertVisible="False" 
                SortExpression="MessageID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MessageID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("MessageID") %>'></asp:Label>
                    <asp:HiddenField ID="MessageIDHiddenField" runat="server" 
                        Value='<%# Eval("MessageID") %>' />
                </ItemTemplate>
                <ItemStyle Width="25px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Message" SortExpression="MessageText">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("MessageText") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("MessageText") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="DeleteMessage" 
                Text="Delete Message">
            <ItemStyle Width="35px" />
            </asp:ButtonField>
        </Columns>
        <EmptyDataTemplate>
            There are currently no messages.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="MessageDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetMessagesNotSeenByOfficeManager" TypeName="MessagesBLL" 
        UpdateMethod="SetMessageAsSeenByOfficeManager">
        <UpdateParameters>
            <asp:Parameter Name="MessageID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>

