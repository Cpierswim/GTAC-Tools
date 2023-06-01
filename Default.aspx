<%@ Page MaintainScrollPositionOnPostback="true" Title="Home Page" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to GTAC online tools!
    </h2>
    <p>
        Welcome to the online system for the Greater Toledo Aquatic Club. Team members can
        use this page to register for the team, sign up for meets, view entries for a meet,
        sign up for various events (like the end of season banquet), sign up for volunteer
        positions, and view a map of GTAC members to help with carpools.</p>
    <p>
        <a href="Account/CreateAccount.aspx">Click here to create an account with GTAC.</a> (including all Non-Competitive groups)</p>
    <p>
        To login - click the &quot;Log In&quot; button in the upper right corner of the
        page.</p>
</asp:Content>
