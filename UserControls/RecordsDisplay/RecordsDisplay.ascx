<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecordsDisplay.ascx.cs"
    Inherits="UserControls_RecordsDisplay_RecordsDisplay" %>
<asp:Label ID="ErrorLabel" runat="server" Text="Label" Visible="False" Style="text-align: center"></asp:Label>
<table width="100%">
    <tr>
        <td align="center">
            <asp:Image ID="Image1" runat="server" AlternateText="Greater Toledo Aquatic Club"
                ImageUrl="~/Images/RecordsGTACLabel.png" Style="text-align: center" Visible="False" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="Label1" runat="server" Font-Names="Comic Sans MS" ForeColor="#2C4B88"
                Style="text-align: center; font-size: x-large; font-family: 'Comic Sans MS'"
                Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
</table>
<asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell VerticalAlign="Top"></asp:TableCell><asp:TableCell
            VerticalAlign="Top"></asp:TableCell></asp:TableRow>
</asp:Table>