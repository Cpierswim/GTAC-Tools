<%@ Page MaintainScrollPositionOnPostback="true" Title="Swimmers With Notes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SwimmersWithNotes.aspx.cs" Inherits="DatabaseManager_SwimmersWithNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
        AutoGenerateRows="False" BackColor="White" BorderColor="#CC9966" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="USAID" 
        DataSourceID="SwimmersDataSource" EnableModelValidation="True" Height="50px" 
        Width="80%">
        <EditRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <Fields>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                SortExpression="LastName">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="PreferredName" HeaderText="Preferred Name" 
                SortExpression="PreferredName">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Created" DataFormatString="{0:D}" 
                HeaderText="Created" SortExpression="Created">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
        </Fields>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" />
    </asp:DetailsView>
    <asp:ObjectDataSource ID="SwimmersDataSource" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetSwimmersWithSystemGeneratedNotes" TypeName="SwimmersBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/DatabaseManager/SpecialDatabaseManagerPage.aspx">&lt;&lt;Return to Special Database Manager Page</asp:HyperLink>
</asp:Content>

