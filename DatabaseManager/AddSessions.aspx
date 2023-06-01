<%@ Page MaintainScrollPositionOnPostback="true" Title="Add Sessions" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddSessions.aspx.cs" Inherits="DatabaseManager_AddSessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
        CellPadding="4" DataKeyNames="SessionID" DataSourceID="SessionsDataSource" 
        EnableModelValidation="True" ForeColor="#333333" GridLines="None" Height="50px" 
        Width="450px">
        <AlternatingRowStyle BackColor="White" />
        <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
        <EditRowStyle BackColor="#7C6F57" />
        <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="SessionDate" HeaderText="Date" 
                SortExpression="SessionDate" />
            <asp:TemplateField HeaderText="Warm Up Time" SortExpression="WarmUpTime">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("WarmUpTime") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("WarmUpTime") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("WarmUpTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Warm Up Guess" SortExpression="WarmUpGuess">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("WarmUpGuess") %>' />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("WarmUpGuess") %>' />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("WarmUpGuess") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prelim/Finals" SortExpression="PrelimFinals">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" 
                        Checked='<%# Bind("PrelimFinals") %>' />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" 
                        Checked='<%# Bind("PrelimFinals") %>' />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" 
                        Checked='<%# Bind("PrelimFinals") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="MinAge" HeaderText="Min Age" 
                SortExpression="MinAge" />
            <asp:BoundField DataField="MaxAge" HeaderText="Max Age" 
                SortExpression="MaxAge" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
    </asp:DetailsView>
    <asp:ObjectDataSource ID="SessionsDataSource" runat="server" 
        InsertMethod="CreateSession" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetSessionsByMeetID" TypeName="SessionsBLL">
        <InsertParameters>
            <asp:Parameter Name="SessionDate" Type="DateTime" />
            <asp:Parameter Name="WarmUpTime" Type="DateTime" />
            <asp:Parameter Name="WarmUpGuess" Type="Boolean" />
            <asp:Parameter Name="PrelimFinals" Type="Boolean" />
            <asp:Parameter Name="MeetId" Type="Int32" />
            <asp:Parameter Name="MinAge" Type="Int32" />
            <asp:Parameter Name="MaxAge" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="MeetID" QueryStringField="MeetID" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

