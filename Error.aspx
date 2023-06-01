<%@ Page MaintainScrollPositionOnPostback="true" Title="Programming Error" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="ErrorLabel" runat="server"><p>
        Whoops. Something went wrong.....an ERROR has happened. (This website is still 
        in the beginning stages, so these things can happen from time to time.)
    </p>
    <p>
        An error file has already been created and sent to Chris Pierson. It would be 
        helpful if you could describe what you just doing when the error happend. That 
        way, Chris can fix the bug and make the site usable for you. The more people
        that fill out this box, the faster the errors disappear from the site.</p></asp:Label>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Columns="100" Rows="8" 
            TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SendErrorMesageButton" runat="server" 
            Text="Send Error Message" onclick="SendErrorMessage" />
    </p>
    <p><asp:Label ID="DatabaseManagerLabel" runat="server"></asp:Label></p>


</asp:Content>

