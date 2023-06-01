<%@ Page MaintainScrollPositionOnPostback="true" Title="Hidden Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LoginHide.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="ApplicationOfflineLablel" runat="server" Visible="false">The Website
        is currently <span style="font-weight:bold;">Offline</span>.<br />
            <br />
            Please check back soon.</asp:Label>
                <asp:LoginView ID="LoginView1" runat="server">
                    <AnonymousTemplate>
                        <h2>
                            Log In
                        </h2>
                        <p>
                            Please enter your username and password.
                        </p>
                        <p>
                            <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="False" NavigateUrl="~/Account/CreateAccount.aspx">Click Here to register a new family with GTAC.</asp:HyperLink>
                        </p>
                        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                            DestinationPageUrl="~/Parents/FamilyView.aspx" OnLoggedIn="LoginUser_LoggedIn">
                            <LayoutTemplate>
                                <span class="failureNotification">
                                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                </span>
                                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                                    ValidationGroup="LoginUserValidationGroup" />
                                <div class="accountInfo" style="width:100%;">
                                    <fieldset class="login">
                                        <legend>Account Information</legend>
                                        <p>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <asp:CheckBox ID="RememberMe" runat="server" Checked="True" />
                                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                                        </p>
                                    </fieldset>
                                    <p class="submitButton">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                                    </p>
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        You are currently Logged in.
                        <br />
                        <br />
                        If you attempted to reach a page and were redirected here, it was because you did
                        not have appropriate permissions to view the page. This should not happen. Please
                        contact Chris Pierson at cpierson@sev.org in this event.
                    </LoggedInTemplate>
                </asp:LoginView>
</asp:Content>
