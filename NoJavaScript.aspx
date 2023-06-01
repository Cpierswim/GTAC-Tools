<%@ Page Title="No Javascript" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="NoJavaScript.aspx.cs" Inherits="NoJavaScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        It seems that you are either using a browser that does not run JavaScript, or you
        have turned it off.<br />
        <br />
        The page you were just viewing requires JavaScript to be running in order to work
        properly. Please check your browser settings.
    </p>
</asp:Content>
