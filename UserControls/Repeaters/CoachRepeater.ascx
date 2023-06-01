<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoachRepeater.ascx.cs"
    Inherits="UserControls_Repeaters_CoachRepeater" %>
<asp:Repeater ID="MyRepeater" runat="server">
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Link") %>'
            Text='<%# DataBinder.Eval(Container.DataItem, "Text")%>' />
        <br>
    </ItemTemplate>
</asp:Repeater>
