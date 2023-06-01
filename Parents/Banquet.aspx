<%@ Page MaintainScrollPositionOnPostback="true" Title="Banquet Signup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Banquet.aspx.cs" Inherits="Parents_Banquet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" 
            Text="You cannot add 0 adults and 0 children.&lt;br /&gt;&lt;br /&gt;" 
            Visible="False"></asp:Label>
        Number of Adults:
        <asp:DropDownList ID="AdultsDropDownList" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        Number of Children:
        <asp:DropDownList ID="ChildrenDropDownList" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="SignUpButton" runat="server" onclick="Button1_Click" 
            Text="Sign Up For Banquet" />
&nbsp;<asp:Button ID="DeleteSignUpButton" runat="server" 
            onclick="WithdrawFromBanquetClicked" Text="Withdraw from Banquet" 
            Visible="False" />
    </p>
    </asp:Content>

