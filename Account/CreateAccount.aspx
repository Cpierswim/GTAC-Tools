<%@ Page MaintainScrollPositionOnPostback="true" Title="Create New Account" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs"
    Inherits="Account_CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckUserNameForSpaces() {

            var $TextBox = $("#ctl00_MainContent_CreateUserWizard1_CreateUserStepContainer_UserName");
            var username = $TextBox.get(0).value;
            var spacefound = false;
            for (i = 0; i < username.length; i++) {
                if (username.charAt(i) == " ") {
                    spacefound = true;
                    i = username.length;
                }
            }

            var $DisplayError = $("#UserNameSpaceErrorSpan");

            if (spacefound) {
                $DisplayError.get(0).innerHTML = "The Username should not contain a space.";
                $TextBox.get(0).value = "";
                $TextBox.get(0).focus();
            }
            else {
                $DisplayError.get(0).innerHTML = "";
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 65px;
        }
        .style2
        {
            width: 110px;
        }
        .style3
        {
            color: #FF3300;
        }
        .style4
        {
            font-family: "Segoe UI";
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="ApplicationOfflineLablel" runat="server" Visible="false">The Website
        is currently <span style="font-weight:bold;">Offline</span>.<br />
            <br />
            Please check back soon.</asp:Label>
    <div id="CustomSpaceValidation" style="color: Red; display: none;">
        The username should not contain a space.
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" Style="font-size: medium"
        OnCreatedUser="WizardCreatedUser" OnCreatingUser="WizardCreatingUser" CancelDestinationPageUrl="~/Default.aspx"
        ContinueDestinationPageUrl="~/Account/Login.aspx" FinishDestinationPageUrl="~/Default.aspx"
        OnActiveStepChanged="WizardStepChanged" UnknownErrorMessage="Your account was not created. Please try again. If you continue to have this error, please contact Chris."
        CreateUserButtonText="Create Login Account" LoginCreatedUser="False" DisableCreatedUser="True"
        OnSendingMail="SendingActivationEmail" OnFinishButtonClick="NavigateButtonClicked"
        OnNextButtonClick="NavigateButtonClicked" ActiveStepIndex="1">
        <MailDefinition BodyFileName="~/EmailTemplates/FamilyCreated.txt" From="cpierson@sev.org"
            IsBodyHtml="True" Subject="Welcome to the Greater Toledo Aquatic Club">
        </MailDefinition>
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Register New Family">
                This page is to register a new family with the Greater Toledo Aquatic Club.
                <br />
                <br />
                <ul>
                    <li>Each family has 1 user account to login.</li>
                    <li>The next series of pages will create a new user account to login to the &quot;GTAC
                        Online Tools&quot; page, then register a new family with GTAC. Please follow each
                        step until the process is complete.</li>
                    <li>Once a family has been created, you can log in and add swimmers to the family.</li>
                </ul>
                <p>
                    &nbsp;</p>
                <p>
                    <strong>Completion of this process creates a new billing account on the swim team. Do
                        not continue if you are here for swim lessons. Do not continue if you have already
                        created an account since August 19</strong><sup><span class="style4"><strong>nd</strong></span></sup>.</p>
                <p>
                    &nbsp;</p>
                <p>
                    Click &quot;Next&quot; to register a new family with GTAC.</p>
            </asp:WizardStep>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center" colspan="2">
                                First, you must create a family login account. This creates the login that the family
                                uses to login to the GTAC Online Tools site. Currently, you can create only 1 login
                                account per family. If you need more than one login account per family, contact
                                Chris Pierson at <a href="mailto:cpierson@sev.org">cpierson@sev.org</a>.<br />
                                <br />
                            </td>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" onblur="CheckUserNameForSpaces();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                        ErrorMessage="E-mail is required." ToolTip="E-mail is required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: Red;">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: Red;">
                                    <span id="UserNameSpaceErrorSpan"></span>
                                </td>
                            </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Primary Contact Parent" AllowReturn="False">
                First, we need information about the Primary Contact Parent. This is the parent
                that will will be the usual contact in the event of questions about things such
                as meet entries.<br />
                <br />
                <table>
                    <tr>
                        <td class="style2">
                            First Name:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryFirstName" runat="server" Columns="35" MaxLength="35" AutoCompleteType="FirstName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PrimaryFirstName"
                                ErrorMessage="First Name Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator" runat="server" ControlToValidate="PrimaryFirstName"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Last Name:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryLastName" runat="server" Columns="35" MaxLength="35" AutoCompleteType="LastName"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PrimaryLastName"
                                ErrorMessage="Last Name Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator0" runat="server" ControlToValidate="PrimaryLastName"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Address:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryAddressLineOne" runat="server" Columns="50" MaxLength="100"
                                AutoCompleteType="HomeStreetAddress"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PrimaryAddressLineOne"
                                ErrorMessage="Address Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="PrimaryParentCustomValidator" runat="server" ControlToValidate="PrimaryAddressLineOne"
                                ErrorMessage="Address Error" OnServerValidate="CheckAddress">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryAddressLineTwo" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="3">
                            City:
                            <asp:TextBox ID="PrimaryCity" runat="server" Columns="35" MaxLength="35" AutoCompleteType="HomeCity"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="PrimaryCity"
                                ErrorMessage="City Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            &nbsp;State:
                            <asp:DropDownList ID="PrimaryState" runat="server">
                                <asp:ListItem>AL</asp:ListItem>
                                <asp:ListItem>AK</asp:ListItem>
                                <asp:ListItem>AZ</asp:ListItem>
                                <asp:ListItem>AR</asp:ListItem>
                                <asp:ListItem>CA</asp:ListItem>
                                <asp:ListItem>CO</asp:ListItem>
                                <asp:ListItem>CT</asp:ListItem>
                                <asp:ListItem>DE</asp:ListItem>
                                <asp:ListItem>DC</asp:ListItem>
                                <asp:ListItem>FL</asp:ListItem>
                                <asp:ListItem>GA</asp:ListItem>
                                <asp:ListItem>GU</asp:ListItem>
                                <asp:ListItem>HI</asp:ListItem>
                                <asp:ListItem>ID</asp:ListItem>
                                <asp:ListItem>IL</asp:ListItem>
                                <asp:ListItem>IN</asp:ListItem>
                                <asp:ListItem>IA</asp:ListItem>
                                <asp:ListItem>KS</asp:ListItem>
                                <asp:ListItem>KY</asp:ListItem>
                                <asp:ListItem>LA</asp:ListItem>
                                <asp:ListItem>ME</asp:ListItem>
                                <asp:ListItem>MD</asp:ListItem>
                                <asp:ListItem>MA</asp:ListItem>
                                <asp:ListItem>MI</asp:ListItem>
                                <asp:ListItem>MN</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                                <asp:ListItem>MO</asp:ListItem>
                                <asp:ListItem>MT</asp:ListItem>
                                <asp:ListItem>NE</asp:ListItem>
                                <asp:ListItem>NV</asp:ListItem>
                                <asp:ListItem>NH</asp:ListItem>
                                <asp:ListItem>NJ</asp:ListItem>
                                <asp:ListItem>NM</asp:ListItem>
                                <asp:ListItem>NY</asp:ListItem>
                                <asp:ListItem>NC</asp:ListItem>
                                <asp:ListItem>ND</asp:ListItem>
                                <asp:ListItem Selected="True">OH</asp:ListItem>
                                <asp:ListItem>OK</asp:ListItem>
                                <asp:ListItem>OR</asp:ListItem>
                                <asp:ListItem>PA</asp:ListItem>
                                <asp:ListItem>RI</asp:ListItem>
                                <asp:ListItem>SC</asp:ListItem>
                                <asp:ListItem>SD</asp:ListItem>
                                <asp:ListItem>TN</asp:ListItem>
                                <asp:ListItem>TX</asp:ListItem>
                                <asp:ListItem>UT</asp:ListItem>
                                <asp:ListItem>VT</asp:ListItem>
                                <asp:ListItem>VA</asp:ListItem>
                                <asp:ListItem>WA</asp:ListItem>
                                <asp:ListItem>WV</asp:ListItem>
                                <asp:ListItem>WI</asp:ListItem>
                                <asp:ListItem>WY</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;Zip:
                            <asp:TextBox ID="PrimaryZip" runat="server" Columns="10" MaxLength="10" AutoCompleteType="HomeZipCode"
                                CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ValidationExpression="\d{5}(-\d{4})?"
                                ForeColor="Red" ControlToValidate="PrimaryZip" ErrorMessage="Zip Code provided does not appear to be a valid Zip Code."
                                ID="RegularExpressionValidator2">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="PrimaryZip"
                                ErrorMessage="Zip Code Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Home Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryHomePhone" runat="server" Columns="35" MaxLength="25" AutoCompleteType="HomePhone"
                                CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="PrimaryHomePhone"
                                ErrorMessage="Home Phone provided does not appear to be a valid Phone Number. All phone numbers must be in the format 123-456-7890."
                                ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="PrimaryHomePhone"
                                ErrorMessage="Home Phone Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            A Home Phone number is required by USA Swimming. If you do not have one, enter your
                            cell phone.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Cell Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryCellPhone" runat="server" Columns="35" MaxLength="25" AutoCompleteType="Cellular"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="PrimaryCellPhone"
                                ErrorMessage="Cell Phone provided does not appear to be a valid Phone Number. All phone numbers must be in the format 123-456-7890."
                                ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                        </td>
                        <td>
                            If you entered a Cell Phone as your home phone, enter it again here.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Work Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="PrimaryWorkPhone" runat="server" Columns="35" MaxLength="35" AutoCompleteType="BusinessPhone"></asp:TextBox>
                        </td>
                        <td>
                            Include any extentions.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            E-mail:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="PrimaryEmail" runat="server" Columns="50" MaxLength="100" AutoCompleteType="Email"
                                            CausesValidation="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PrimaryEmail"
                                            ErrorMessage="E-mail provided does not appear to be a valid e-mail addres." ForeColor="Red"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="PrimaryEmail"
                                            ErrorMessage="E-Mail Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        &nbsp;This e-mail will be used for billing purposes. Make sure to allow the email
                                        address gtacbilling@bex.net on this e-mail address.
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            If you have any notes that you need about this parent, enter them here.
                        </td>
                        <td>
                            <asp:TextBox ID="PrimaryNotes" runat="server" Columns="35" MaxLength="256" Rows="3"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                Is there another parent to add to this account?
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:RadioButtonList>
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep3" runat="server" Title="Secondary Contact Parent"
                AllowReturn="False">
                Next, we need information about the Secondary Contact Parent.<br />
                <br />
                <table>
                    <tr>
                        <td class="style2">
                            First Name:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryFirstName" runat="server" Columns="35" MaxLength="35"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="SecondaryFirstName"
                                ErrorMessage="First Name Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator1" runat="server" ControlToValidate="SecondaryFirstName"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Last Name:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryLastName" runat="server" Columns="35" MaxLength="35"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="SecondaryLastName"
                                ErrorMessage="Last Name Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator2" runat="server" ControlToValidate="SecondaryLastName"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Address:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryAddressLineOne" runat="server" Columns="50" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="SecondaryAddressLineOne"
                                ErrorMessage="Address Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="SecondaryParentCustomValidator" runat="server" ControlToValidate="SecondaryAddressLineOne"
                                ErrorMessage="Address Error" OnServerValidate="CheckAddress">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryAddressLineTwo" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="3">
                            City:
                            <asp:TextBox ID="SecondaryCity" runat="server" Columns="35" MaxLength="35"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="SecondaryCity"
                                ErrorMessage="City Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            &nbsp;State:
                            <asp:DropDownList ID="SecondaryState" runat="server">
                                <asp:ListItem>AL</asp:ListItem>
                                <asp:ListItem>AK</asp:ListItem>
                                <asp:ListItem>AZ</asp:ListItem>
                                <asp:ListItem>AR</asp:ListItem>
                                <asp:ListItem>CA</asp:ListItem>
                                <asp:ListItem>CO</asp:ListItem>
                                <asp:ListItem>CT</asp:ListItem>
                                <asp:ListItem>DE</asp:ListItem>
                                <asp:ListItem>DC</asp:ListItem>
                                <asp:ListItem>FL</asp:ListItem>
                                <asp:ListItem>GA</asp:ListItem>
                                <asp:ListItem>GU</asp:ListItem>
                                <asp:ListItem>HI</asp:ListItem>
                                <asp:ListItem>ID</asp:ListItem>
                                <asp:ListItem>IL</asp:ListItem>
                                <asp:ListItem>IN</asp:ListItem>
                                <asp:ListItem>IA</asp:ListItem>
                                <asp:ListItem>KS</asp:ListItem>
                                <asp:ListItem>KY</asp:ListItem>
                                <asp:ListItem>LA</asp:ListItem>
                                <asp:ListItem>ME</asp:ListItem>
                                <asp:ListItem>MD</asp:ListItem>
                                <asp:ListItem>MA</asp:ListItem>
                                <asp:ListItem>MI</asp:ListItem>
                                <asp:ListItem>MN</asp:ListItem>
                                <asp:ListItem>MS</asp:ListItem>
                                <asp:ListItem>MO</asp:ListItem>
                                <asp:ListItem>MT</asp:ListItem>
                                <asp:ListItem>NE</asp:ListItem>
                                <asp:ListItem>NV</asp:ListItem>
                                <asp:ListItem>NH</asp:ListItem>
                                <asp:ListItem>NJ</asp:ListItem>
                                <asp:ListItem>NM</asp:ListItem>
                                <asp:ListItem>NY</asp:ListItem>
                                <asp:ListItem>NC</asp:ListItem>
                                <asp:ListItem>ND</asp:ListItem>
                                <asp:ListItem Selected="True">OH</asp:ListItem>
                                <asp:ListItem>OK</asp:ListItem>
                                <asp:ListItem>OR</asp:ListItem>
                                <asp:ListItem>PA</asp:ListItem>
                                <asp:ListItem>RI</asp:ListItem>
                                <asp:ListItem>SC</asp:ListItem>
                                <asp:ListItem>SD</asp:ListItem>
                                <asp:ListItem>TN</asp:ListItem>
                                <asp:ListItem>TX</asp:ListItem>
                                <asp:ListItem>UT</asp:ListItem>
                                <asp:ListItem>VT</asp:ListItem>
                                <asp:ListItem>VA</asp:ListItem>
                                <asp:ListItem>WA</asp:ListItem>
                                <asp:ListItem>WV</asp:ListItem>
                                <asp:ListItem>WI</asp:ListItem>
                                <asp:ListItem>WY</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="SecondaryState"
                                ErrorMessage="State Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                            &nbsp;Zip:
                            <asp:TextBox ID="SecondaryZip" runat="server" Columns="10" MaxLength="10" CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="SecondaryZip"
                                ErrorMessage="Zip Code provided does not appear to be a valid Zip Code." ForeColor="Red"
                                ValidationExpression="\d{5}(-\d{4})?">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="SecondaryZip"
                                ErrorMessage="Zip Code Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Home Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryHomePhone" runat="server" Columns="35" MaxLength="25" CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="SecondaryHomePhone"
                                ErrorMessage="Home Phone provided does not appear to be a valid Phone Number. All phone numbers must be in the format 123-456-7890."
                                ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="SecondaryHomePhone"
                                ErrorMessage="Home Phone Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            A Home Phone number is required by USA Swimming. If you do not have one, enter your
                            cell phone.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Cell Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryCellPhone" runat="server" Columns="35" MaxLength="25"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="SecondaryCellPhone"
                                ErrorMessage="Cell Phone provided does not appear to be a valid Phone Number. All phone numbers must be in the format 123-456-7890."
                                ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                        </td>
                        <td>
                            If you entered a Cell Phone as your home phone, enter it again here.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Work Phone:
                        </td>
                        <td style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryWorkPhone" runat="server" Columns="35" MaxLength="35"></asp:TextBox>
                        </td>
                        <td>
                            Include any extentions.
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            E-mail:
                        </td>
                        <td colspan="2" style="margin-left: 40px">
                            <asp:TextBox ID="SecondaryEmail" runat="server" Columns="50" MaxLength="100" CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="SecondaryEmail"
                                ErrorMessage="E-mail provided does not appear to be a valid e-mail addres." ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            If you have any notes that you need about this parent, enter them here.
                        </td>
                        <td>
                            <asp:TextBox ID="SecondaryNotes" runat="server" Columns="35" MaxLength="256" Rows="3"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep ID="AlmostCompleteStep" runat="server" Title="Add Parents">
                <asp:Label ID="NumberOfParentsAddedTextBox" runat="server" Text="Number"></asp:Label>
                been sucessfully added to your account. If this is not the number you expected,
                please contact Chris Pierson at <a href="mailto:cpierson@sev.org">cpierson@sev.org</a>.<br />
                <br />
                <h1>
                    YOUR REGISTRATION PROCESS IS NOT YET COMPLETE.</h1>
                <br />
                <br />
                You must now activate your account. You cannot sign up any swimmers, or use your
                account in any way until your account is activated!
                <br />
                <br />
                <span class="style3"><strong>An e-mail has been sent to the address you provided for
                    the User Account. (Sometimes it can take a while to reach you, but it has sucessfully
                    been sent. If it takes more than a day to reach you, contact Chris Pierson at cpierson@sev.org)</strong></span>
                You will need to visit the link provided in the e-mail in order to activate your
                account.<br />
                <br />
                Once your account is active, you can login and you will need to add swimmers to
                your family.
                <br />
                <br />
                Check your e-mail to continue the registration process.
            </asp:WizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center">
                                Complete
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Your account has been successfully created. Check your e-mail to activate your account.
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue"
                                    Text="Continue" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StepNavigationTemplate>
    </asp:CreateUserWizard>
</asp:Content>
