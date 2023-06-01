<%@ Page MaintainScrollPositionOnPostback="true" Title="Password Mailer" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PasswordMailer.aspx.cs" Inherits="Account_PasswordMailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        Enter your e-mail address in the box below. Your address will be reset to a 
        random password. (Password look-ups are not possible due to extra levels of 
        security.) You can reset your password once you login with the new password.</p>
    <p>
        Email Address:
        <asp:TextBox ID="EmailAddressTextBox" runat="server" Columns="50"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="ResetPasswordButton" runat="server" OnClick="ResetPassword" Text="Reset Password" />
    </p>
    <p>
        <asp:Label ID="ResultLabel" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:HyperLink ID="GoBackLink" runat="server" 
            NavigateUrl="~/Account/Login.aspx" Visible="False">HyperLink</asp:HyperLink>
    </p>
</asp:Content>
