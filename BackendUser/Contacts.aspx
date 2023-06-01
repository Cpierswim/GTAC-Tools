<%@ Page MaintainScrollPositionOnPostback="true" Title="Contacts Download" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="BackendUser_Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:CheckBox ID="IncludeSwimmersInfoCheckBox" runat="server" 
        Text="Click here to include swimmer's personal contact information." /><br /><br />
    <asp:Button ID="CreateContactsButton" runat="server" onclick="CreateContacts" 
        Text="Click Here to create contacts file" />
&nbsp;<br />
    <br />
    NOTE: The file that will be downloaded works only for GMAIL. I will be adding 
    another one for other email programs.<br />
    <br />
    <a href="../Features/Default.aspx">&lt;&lt; Go Back to Admin Page</a>  
</asp:Content>

