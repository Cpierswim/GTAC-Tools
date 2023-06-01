<%@ Page MaintainScrollPositionOnPostback="true" Title="Add Parents" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddParents.aspx.cs" Inherits="Parents_AddParents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="CodeLabel" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:Wizard ID="Wizard1" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
        BorderWidth="1px" DisplaySideBar="False" Font-Names="Verdana" Font-Size="1.0em"
        Width="100%" OnNextButtonClick="AdvancingToNextStep" ActiveStepIndex="0" 
        OnActiveStepChanged="ActiveStepChanged" 
        CancelDestinationPageUrl="~/Parents/FamilyView.aspx" DisplayCancelButton="True" 
        FinishDestinationPageUrl="~/Parents/FamilyView.aspx">
        <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
            Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" />
        <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" ForeColor="White" />
        <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" />
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" BackColor="White" BorderColor="#507CD1"
                BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="MovePrevious"
                Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284E98" Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" BackColor="White" BorderColor="#507CD1"
                BorderStyle="Solid" BorderWidth="1px" CommandName="MoveNext" Font-Names="Verdana"
                Font-Size="1.0em" ForeColor="#284E98" Text="Next" />
        </StepNavigationTemplate>
        <StepStyle Font-Size="1.0em" ForeColor="#333333" />
        <WizardSteps>
            <asp:WizardStep ID="PrimaryContactParent" runat="server" StepType="Step" Title="PrimaryContactParent"
                AllowReturn="False">
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
                            <asp:CustomValidator ID="CapitolValidator1" runat="server" 
                                ControlToValidate="PrimaryFirstName" 
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
                            <asp:CustomValidator ID="CapitolValidator2" runat="server" 
                                ControlToValidate="PrimaryLastName" 
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
                            <asp:CustomValidator ID="PrimaryParentCustomValidator" runat="server" 
                                ControlToValidate="PrimaryAddressLineOne" ErrorMessage="Address Error" 
                                OnServerValidate="CheckAddress">*</asp:CustomValidator>
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
                            <asp:TextBox ID="PrimaryEmail" runat="server" Columns="50" MaxLength="100" AutoCompleteType="Email"
                                CausesValidation="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PrimaryEmail"
                                ErrorMessage="E-mail provided does not appear to be a valid e-mail addres." ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="PrimaryEmail"
                                ErrorMessage="E-Mail Is Required" ForeColor="Red">*</asp:RequiredFieldValidator>
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
            <asp:WizardStep ID="SecondaryContactParent" runat="server" StepType="Step" Title="SecondaryContactParent"
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
                            <asp:CustomValidator ID="CapitolValidator3" runat="server" 
                                ControlToValidate="SecondaryFirstName" 
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
                            <asp:CustomValidator ID="CapitolValidator4" runat="server" 
                                ControlToValidate="SecondaryLastName" 
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
                            <asp:CustomValidator ID="SecondaryParentCustomValidator" runat="server" 
                                ControlToValidate="SecondaryAddressLineOne" ErrorMessage="Address Error" 
                                OnServerValidate="CheckAddress">*</asp:CustomValidator>
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
            <asp:WizardStep ID="Complete" runat="server" StepType="Complete" Title="Complete">
                <asp:Label ID="NumberOfParentsAddedTextBox" runat="server" Text="Number"></asp:Label>
                been sucessfully added to your account. If this is not the number you expected,
                please contact Chris Pierson at <a href="mailto:cpierson@sev.org">cpierson@sev.org</a>.
                <br />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Parents/FamilyView.aspx">&lt;&lt; Go Back to Family View</asp:HyperLink>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
