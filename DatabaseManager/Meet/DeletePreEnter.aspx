<%@ Page Title="Delete Pre-Entries For Swimmer" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DeletePreEnter.aspx.cs" Inherits="DatabaseManager_Meet_DeletePreEnter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="NameLabel" runat="server" Text="Label"></asp:Label><br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SessionsID"
                DataSourceID="SessionsAdapter" OnRowDataBound="RowDataBound" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                OnRowCommand="RemovePreEnterClicked" ondatabound="GridViewDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Session Number" SortExpression="Session">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Session") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="SessionNumberLabel" runat="server" Text='<%# Bind("Session") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entered">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="EnteredCheckBox" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day" SortExpression="Day">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Day") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StartTime" SortExpression="StartTime">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StartTime") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="StartTimeLabel" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                            <asp:HiddenField ID="AMHiddenField" runat="server" Value='<%# Eval("AM") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Button" CommandName="Remove" Text="Remove Pre-Enter" />
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
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="SessionsAdapter" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSessionsByMeetID" TypeName="SessionsV2BLL">
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="MeetID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DatabaseManager/Meet/PreEntered.aspx">&lt;&lt; Return To Meet Pre-Entries</asp:HyperLink>
</asp:Content>
