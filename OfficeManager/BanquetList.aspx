<%@ Page MaintainScrollPositionOnPostback="true" Title="Banquet List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BanquetList.aspx.cs" Inherits="OfficeManager_BanquetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="BanquetDataSource" EnableModelValidation="True" 
        onrowdatabound="ChangeFamilyIDtoFamilyName" CellPadding="4" 
        ForeColor="#333333" GridLines="None" Width="609px" 
        ondatabound="GridView1DataBound" onrowcommand="RemoveFamilyClicked">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Family Name" SortExpression="FamilyID">
                <EditItemTemplate>
                     <asp:Label ID="FamilyNameLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" 
                        Value='<%# Eval("FamilyID") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="FamilyNameLabel" runat="server"></asp:Label>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" 
                        Value='<%# Eval("FamilyID") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="80%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adults" SortExpression="Adults">
                <EditItemTemplate>
                    <asp:TextBox ID="AdultsTextBox" runat="server" Text='<%# Bind("Adults") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="AdultsLabel" runat="server" Text='<%# Bind("Adults") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Children" SortExpression="Children">
                <EditItemTemplate>
                    <asp:TextBox ID="ChildrenTextBox" runat="server" Text='<%# Bind("Children") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="ChildrenLabel" runat="server" Text='<%# Bind("Children") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" Text="Remove Family" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    </asp:GridView>
    <asp:ObjectDataSource ID="BanquetDataSource" runat="server" 
        DeleteMethod="DeleteFamilySignUp" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetAllBanquetSignUps" TypeName="BanquetSignUpsBLL" 
        UpdateMethod="UpdateFamilySignUp">
        <DeleteParameters>
            <asp:Parameter Name="FamilyID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="FamilyID" Type="Int32" />
            <asp:Parameter Name="adults" Type="Int32" />
            <asp:Parameter Name="children" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="DeleteAllSignups" 
        Text="DELETE ALL SIGNUPS - This action is NOT reversable" />
    <br />
    <asp:Label ID="TotalLabel" runat="server" Text="Label"></asp:Label>
</asp:Content>

