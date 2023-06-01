<%@ Page MaintainScrollPositionOnPostback="true" Title="Pick Sessions" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PickSessions.aspx.cs" Inherits="Parents_PickSessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="ErrorLabel" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="HeaderLabel" runat="server" 
            Text="Pick Sessions for [name] for [meet]."></asp:Label>
    </p>
    <p>
        &nbsp;Swimmer&#39;s age at meet: <asp:Label ID="AgeAtMeetLabel" runat="server" Text=""></asp:Label>
&nbsp;<asp:HiddenField ID="SwimmerAgeHiddenField" runat="server" />
        <asp:HiddenField ID="SwimmerNameHiddenField" runat="server" />
        <asp:HiddenField ID="MeetNameHiddenField" runat="server" />
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="SessionID" DataSourceID="SessionsDataSource" 
            EnableModelValidation="True" onrowdatabound="CustomizeRow" 
            ondatabound="GridDataBound">
            <Columns>
                <asp:BoundField DataField="SessionDate" DataFormatString="{0:D}" 
                    HeaderText="Date" SortExpression="SessionDate" />
                <asp:TemplateField HeaderText="Warm Up Time" SortExpression="WarmUpTime">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("WarmUpTime") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="WarmUpTimeLabel" runat="server" Text='<%# Bind("WarmUpTime") %>'></asp:Label>
                        <asp:HiddenField ID="SessionIDHiddenField" runat="server" 
                            Value='<%# Eval("SessionID") %>' />
                        <asp:HiddenField ID="WarmUpGuessHiddenField" runat="server" 
                            Value='<%# Eval("WarmUpGuess") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Session Type" SortExpression="PrelimFinals">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" 
                            Checked='<%# Bind("PrelimFinals") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        
                        <asp:Label ID="PrelimFinalsLabel" runat="server" Text='<%# Eval("PrelimFinals") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ages for Session" SortExpression="MinAge">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("MinAge") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="AgeLabel" runat="server"></asp:Label>
                        <asp:HiddenField ID="MinAgeHiddenField" runat="server" 
                            Value='<%# Eval("MinAge") %>' />
                        <asp:HiddenField ID="MaxAgeHiddenField" runat="server" 
                            Value='<%# Eval("MaxAge") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enter Session">
                    <ItemTemplate>
                        <asp:CheckBox ID="SelectSessionCheckBox" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="SessionsDataSource" runat="server" 
            SelectMethod="GetSessionsByMeetID" TypeName="SessionsBLL">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="MeetID" 
                    QueryStringField="MeetID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </p>
    <p>
        <asp:Button ID="EnterSessionsButton" runat="server" 
            onclick="EnterSessionsButtonClicked" Text="Click Here To Enter Sessions" />
    </p>
</asp:Content>

