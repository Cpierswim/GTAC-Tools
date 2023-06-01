<%@ Page MaintainScrollPositionOnPostback="true" Title="Register new Swimmer with GTAC"
    Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddNewSwimmer.aspx.cs"
    Inherits="Parents_AddNewSwimmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function ()
        {

            $(".hasToolTip").tooltip({ effect: 'slide', position: 'center right' });

        });
    </script>
    <style type="text/css">
        .toolTipLarge
        {
            display: none;
            background: url("../Styles/images/ToolTip/black_big.png");
            height: 194px;
            width: 370px;
            font-size: 11px;
            color: White;
            text-align: center;
        }
        .toolTipSmall
        {
            display: none;
            background: url("../Styles/images/ToolTip/black.png");
            height: 109px;
            width: 209px;
            font-size: 11px;
            color: White;
            text-align: center;
        }
        .tooltip p
        {
            margin-left: 30px;
            margin-right: 30px;
            margin-top: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="HiddenFamilyID" runat="server" />
    <asp:HiddenField ID="FamilyPhoneNumbersHiddenField" runat="server" />
    <asp:Label ID="ErrorLabel" runat="server" Text="The swimmer was not added. A swimmer with the same Last Name, First Name, and Birthday already exists in the database. Make sure that you are not trying to add a swimmer that you already have."
        ForeColor="Red" Visible="false"></asp:Label>
    <asp:HiddenField ID="FamilyEmailsHiddenField" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="The Wizard was unable to advance to the next step. Please check these issues:"
        Font-Size="1em" />
    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE"
        BorderWidth="1px" Font-Names="Verdana" Font-Size="1.1em" Width="100%" OnActiveStepChanged="Wizard_ActiveStepChanged"
        OnNextButtonClick="NextButtonClicked">
        <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
            Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
        <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
        <SideBarButtonStyle BackColor="#507CD1" Font-Names="Verdana" ForeColor="White" />
        <SideBarStyle BackColor="#507CD1" Font-Size="0.7em" VerticalAlign="Top" Width="225px" />
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" BackColor="White" BorderColor="#507CD1"
                BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="MovePrevious"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" BackColor="White" BorderColor="#507CD1"
                BorderStyle="Solid" BorderWidth="1px" CommandName="MoveNext" Font-Names="Verdana"
                Font-Size="0.8em" ForeColor="#284E98" Text="Next" />
        </StepNavigationTemplate>
        <StepStyle Font-Size="0.8em" ForeColor="#333333" />
        <WizardSteps>
            <asp:WizardStep runat="server" StepType="Start" Title="Start" AllowReturn="False">
                <asp:Label ID="StartNavigationLabel" runat="server" Text="This Wizard will add a swimmer to your family. Please follow each step
                in order to add your swimmer."></asp:Label>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Swimmer Information Step 1">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="FirstNameLabel" runat="server" Text="Legal First Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="LegalFirstNameTextBox" runat="server" Columns="35" MaxLength="35"
                                CssClass="hasToolTip"></asp:TextBox>
                            <div class="toolTipSmall">
                                <table style="width: 90%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                                    margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="vertical-align: middle;">
                                            The swimmer's name on their birth certificate.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RequiredFieldValidator ID="FirstNameRequiredValidator" runat="server" ControlToValidate="LegalFirstNameTextBox"
                                ErrorMessage="Legal First Name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator2" runat="server" ControlToValidate="LegalFirstNameTextBox"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="PreferredNameLabel" runat="server" Text="First name as it should &lt;br /&gt;appear in heat sheet:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="PreferredNameTextBox" runat="server" Columns="35" MaxLength="35"
                                CssClass="hasToolTip"></asp:TextBox>
                            <div class="toolTipLarge">
                                <table style="width: 85%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                                    margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="vertical-align: middle;">
                                            This is the swimmer's name as you want it to appear in the heat sheet, not their
                                            legal name.
                                            <br />
                                            <br />
                                            For example: Chris instead of Christopher<br />
                                            <br />
                                            This box is required.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RequiredFieldValidator ID="PreferredNameRequiredValidator" runat="server" ControlToValidate="PreferredNameTextBox"
                                ErrorMessage="A preferred First Name is required. This is first name as it should appear in the heat sheet. If you want the legal first name to appear in the heat sheet, you need to enter the legal first name again."
                                ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="PreferredNameTextBox"
                                ErrorMessage="The Preferred First Name should ONLY contain the first name." OnServerValidate="ValidatePreferredFirstName">*</asp:CustomValidator>
                            <asp:CustomValidator ID="CapitolValidator3" runat="server" ControlToValidate="PreferredNameTextBox"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="MiddleNameTextBox" runat="server" Columns="35" MaxLength="35" CssClass="hasToolTip"></asp:TextBox>
                            <div class="toolTipLarge">
                                <table style="width: 85%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                                    margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="vertical-align: middle;">
                                            If the swimmer has a middle name, it is required. The form can not check for this
                                            because not all people have a middle name, but if it is not included, it can break
                                            the database.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:CustomValidator ID="CapitolValidator4" runat="server" ControlToValidate="MiddleNameTextBox"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="LastNameLabel" runat="server" Text="Last Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="LastNameTextBox" runat="server" Columns="35" MaxLength="35"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LastNameRequiredValidator" runat="server" ControlToValidate="LegalFirstNameTextBox"
                                ErrorMessage="A Last Name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CapitolValidator5" runat="server" ControlToValidate="LastNameTextBox"
                                ErrorMessage="Capitolization Error. Check to make sure there is at least 1 capitol letter, and that the CAPS LOCK is not stuck on."
                                OnServerValidate="ValidateCapitolization">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="BirthdayLabel" runat="server" Text="Birthday:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="BirthdayMonthDropDownList" runat="server">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="BirthdayDayDropDownList" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="CustomYearEntryTextBox" runat="server" Columns="4" Visible="False"
                                MaxLength="4"></asp:TextBox>
                            <asp:DropDownList ID="BirthdayYearDropDown" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="YearNotInListButton" runat="server" OnClick="YearNotInList_Clicked"
                                Text="Year not in list" />
                            <asp:RequiredFieldValidator ID="CustomYearValidator" runat="server" ControlToValidate="CustomYearEntryTextBox"
                                Enabled="False" ErrorMessage="A birthday year is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="CustomBirthdayYearRangeValidator" runat="server" ControlToValidate="CustomYearEntryTextBox"
                                Enabled="False" ErrorMessage="Birthday year not recognized.&lt;br /&gt;Make sure you have entered a 4 digit date."
                                ForeColor="Red">*</asp:RangeValidator>
                            <asp:CustomValidator ID="BirthdayCustomValidator" runat="server" ControlToValidate="BirthdayDayDropDownList"
                                ErrorMessage="Birthday provided is not a valid date." ForeColor="Red" OnServerValidate="ValidateBirthday"
                                ValidateEmptyText="True">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="GenderLabel" runat="server" Text="Gender:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="GenderDropDownList" runat="server">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Swimmer Information Step 2" StepType="Step">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="GroupLabel" runat="server" Text="Group:&lt;br /&gt;(If not known, select &quot;Unknown&quot;)"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:DropDownList ID="GroupDropDownList" runat="server" DataSourceID="GroupsDataSource"
                                DataTextField="GroupName" DataValueField="GroupID">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="USCitizenLabel" runat="server" Text="US Citizen?:"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:RadioButtonList ID="USCitizenRadioButton" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="FINAOrginizationLabel" runat="server" Text="Member of another FINA orginization?*:"></asp:Label>
                        </td>
                        <td style="width: 50%">
                            <asp:RadioButtonList ID="MemberOfOtherFINAOrginizationRadioButton" runat="server"
                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="FINAOrginizationRadioButton_IndexChanged">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="WhichFINAOrginizationLabel" runat="server" Text="Which Orginization?:"
                                Visible="false"></asp:Label>
                            <asp:TextBox ID="OtherFINAOrginizationTextBox" runat="server" Visible="false" Columns="35"
                                MaxLength="35"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="OtherFINAOrginizationRequiredFieldValidator" runat="server"
                                ControlToValidate="OtherFINAOrginizationTextBox" Enabled="False" ErrorMessage="Since you have stated that the swimmer is also a member of another FINA Orginization, you must specify which orginization."
                                ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Swimmer Contact Info" StepType="Step">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="AthleteContactExplainLabel" runat="server" Text="The spaces below are for the contact information for the athlete themself (NOT the parent). These are optional."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="AthletePhoneLabel" runat="server" Text="Athlete Phone #:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="AthletePhoneTextBox" runat="server" MaxLength="25" Columns="25"
                                CssClass="hasToolTip"></asp:TextBox>
                            <div class="toolTipSmall">
                                <table style="width: 90%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                                    margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="vertical-align: middle;">
                                            This is for the athlete's personal phone number only. This is optional.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RegularExpressionValidator ID="PhoneNumberRegularExpressionValidator" runat="server"
                                ControlToValidate="AthletePhoneTextBox" ErrorMessage="Phone Number entered not recognized.<br />All phone numbers must be in the 123-456-7890 format."
                                ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="AthletePhoneTextBox"
                                ErrorMessage="The phone number below is the same as a parent's e-mail or phone number. This page is for contact info for the swimmer themselves. (These can be left blank.)"
                                OnServerValidate="ValidateAthletePhone">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="AthleteEmailLabel" runat="server" Text="Athlete Email:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="AthleteEmailTextBox" runat="server" MaxLength="100" Columns="50"
                                CssClass="hasToolTip"></asp:TextBox>
                            <div class="toolTipSmall">
                                <table style="width: 90%; text-align: center; font-weight: bold; color: White; margin-left: auto;
                                    margin-right: auto; height: 100%;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="vertical-align: middle;">
                                            This is for the athlete's personal Email only. This is optional.
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                ControlToValidate="AthleteEmailTextBox" ErrorMessage="Email entered not recognized."
                                ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="AthleteEmailTextBox"
                                ErrorMessage="The e-mail below is the same as a parent's e-mail or phone number. This page is for contact info for the swimmer themselves. (These can be left blank.)"
                                OnServerValidate="ValidateAthleteEmail">*</asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="School Info">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="SchoolNameLabel" runat="server" Text="School Name: "></asp:Label>
                            <asp:DropDownList ID="SchoolNameDropDownList" runat="server" DataSourceID="DirectSQLDatasource"
                                DataTextField="SchoolName" DataValueField="SchoolName" AutoPostBack="True" OnDataBound="SchoolNameDropDownListDataItemsAdded"
                                OnSelectedIndexChanged="SchoolNameDropDownListSelectedIndexChanged">
                                <asp:ListItem>Other...</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="CustomSchoolNameTextBox" runat="server" Visible="false" MaxLength="200"
                                Rows="150" Width="387px" Wrap="False"></asp:TextBox>
                            <asp:SqlDataSource ID="DirectSQLDatasource" runat="server" ConnectionString="Data Source=gtacregistration.db.3253734.hostedresource.com;Initial Catalog=gtacregistration;Persist Security Info=True;User ID=gtacregistration;Password=SW8765im!"
                                ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT SchoolName FROM SchoolInfos ORDER BY SchoolName">
                            </asp:SqlDataSource>
                            <asp:RequiredFieldValidator ID="SchoolNameRequiredFieldValidator" runat="server"
                                ControlToValidate="CustomSchoolNameTextBox" Enabled="False" ErrorMessage="A school name is required.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="SchoolGradeLabel" runat="server" Text="Grade: "></asp:Label>
                            <asp:DropDownList ID="GradeDropDownList" runat="server">
                                <asp:ListItem Value="0">Before Kindergarden</asp:ListItem>
                                <asp:ListItem Value="1">Kindergarden</asp:ListItem>
                                <asp:ListItem Value="2">1st</asp:ListItem>
                                <asp:ListItem Value="3">2nd</asp:ListItem>
                                <asp:ListItem Value="4">3rd</asp:ListItem>
                                <asp:ListItem Value="5">4th</asp:ListItem>
                                <asp:ListItem Value="6">5th</asp:ListItem>
                                <asp:ListItem Value="7">6th</asp:ListItem>
                                <asp:ListItem Value="8">7th</asp:ListItem>
                                <asp:ListItem Value="9">8th</asp:ListItem>
                                <asp:ListItem Value="10">Freshman - High School</asp:ListItem>
                                <asp:ListItem Value="11">Sophomore - High School</asp:ListItem>
                                <asp:ListItem Value="12">Junior - High School</asp:ListItem>
                                <asp:ListItem Value="13">Senior - High School</asp:ListItem>
                                <asp:ListItem Value="14">Freshman - College</asp:ListItem>
                                <asp:ListItem Value="15">Sophomore - College</asp:ListItem>
                                <asp:ListItem Value="16">Junior - College</asp:ListItem>
                                <asp:ListItem Value="17">Senior - College</asp:ListItem>
                                <asp:ListItem Value="18">Post - Senior - College</asp:ListItem>
                                <asp:ListItem Value="19">Post - Grad or beyond</asp:ListItem>
                                <asp:ListItem Value="20">None</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Disability Info">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="DisabilityExplainLabel" runat="server" Text="Below enter information about any disabilities the swimmer may have.&lt;br /&gt;&lt;br /&gt;Select as many disabilities as apply.&lt;br /&gt;&lt;br /&gt;If your swimmer has disabilites, there may be extra opportunities available. GTAC is nationally renown  for our disability swimming program. E-mail Keith at kkjrswim@yahoo.com for more information."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="DisabilityLabel" runat="server" Text="Disability:"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="DisabilityCheckBoxList" runat="server">
                                <asp:ListItem Value="A">Legally blind or visually impaired</asp:ListItem>
                                <asp:ListItem Value="B">Deaf or hard of hearing</asp:ListItem>
                                <asp:ListItem Value="C">Physical disability such as amputation, cerebral palsy, dwarfism, spinal injury, mobility impairment</asp:ListItem>
                                <asp:ListItem Value="D">Cognitive disability such as mental retardation, severe learning disorder, autism</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Ethnicity Info">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="EthnicityExplainLabel" runat="server" Text="USA Swimming collects information about the ethnicity of its swimmers to train and ensure that the sport grows among all communities.&lt;br /&gt;&lt;br /&gt;Select as many as apply."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="EthnicityLabel" runat="server" Text="Ethnicity:"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="EthnicityCheckBoxList" runat="server">
                                <asp:ListItem Value="Q">Black or African American</asp:ListItem>
                                <asp:ListItem Value="R">Asian</asp:ListItem>
                                <asp:ListItem Value="S">White</asp:ListItem>
                                <asp:ListItem Value="T">Hispanic or Latino</asp:ListItem>
                                <asp:ListItem Value="U">American Indian &amp; Alaska Native</asp:ListItem>
                                <asp:ListItem Value="V">Some Other Race</asp:ListItem>
                                <asp:ListItem Value="W">Native Hawaiian &amp; Other Pacific Islander</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Transfer Info">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="TransferExplainLabel" runat="server" 
                                Text="&lt;span style=&quot;color:Red;&quot;&gt;Complete the information below ONLY if the swimmer has ever been a member of another USA Swimming Club (whether you did meets or not).&lt;/span&gt;&lt;br /&gt;&lt;br /&gt;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="PreviousTeamLabel" runat="server" Text="Previous Team Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="PreviousTeamTextBox" runat="server" Columns="50" MaxLength="100"></asp:TextBox>
                            <asp:CustomValidator ID="NotGTACCustomValidator" runat="server" ControlToValidate="PreviousTeamTextBox"
                                ErrorMessage="This page should only be filled out if the swimmer was previously registered to a different team."
                                OnServerValidate="ValidateThatGTACWasNotEnteredAsPreviousTeam">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="DateOfLastCompLabel" runat="server" Text="Last date of competition for previous team*:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DateOfLastCompetitionMonthDropDownList" runat="server">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DateOfLastCompetitionDayDropDownList" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DateOfLastCompetitionYearDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Date provided is not a valid date."
                                ForeColor="Red" OnServerValidate="ValidateDateofLastCompetition">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="DateofLastComExplainLabel" runat="server" 
                                Text="&lt;br /&gt;&lt;br /&gt;*(This is the exact last date the swimmer swam in a meet attached to a USA Swimming Club other than GTAC. This date could fall during the middle of a meet. If the swimmer swam unattached, it does not count as swimming attached to the previous club.)"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Notes">
                <asp:Label ID="Label3" runat="server" Text="List any necessary notes about the swimmer here:"></asp:Label><br />
                <asp:TextBox ID="SwimmerNotesTextBox" runat="server" Columns="75" MaxLength="800"
                    Rows="10" TextMode="MultiLine">
                </asp:TextBox>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Complete" Title="Complete">
                <asp:Label ID="CompleteLabel" runat="server" Text="Information about Wizard. (Completed or Errors)"></asp:Label>
                <br />
                <br />
                Your swimmer is now in the registered online. To complete the registration process,
                you must complete the steps below.<br />
                <br />
                1. <a href="http://www.gtacswim.info/main/LinkClick.aspx?fileticket=9kj7x5bhmx4%3d&tabid=229">
                    Download and fill out this form</a> (the 2011-12 winter fees) and turn it in,
                along with your payment to the black box in Coach Chris&#39;s office at St. Francis.
                Once this form has been recieved and the initial fees paid, the swimmer&#39;s registration
                will be approved by the office manager and the swimmer will be officially registered.<br />
                <br />
                <br />
                <br />
                <asp:Label ID="PreviousTeamIndicatedLabel" runat="server" Text="2. You indicated that your swimmer is transferring from another team. You also need to download and fill out the form below and give it to Chris Pierson. This form is required for all swimmers that transfer by Ohio Swimming."
                    Visible="False"></asp:Label><br />
                <br />
                <asp:HyperLink ID="TransferFormHyperlink" runat="server" Target="_blank" Visible="False"
                    Font-Underline="True" ForeColor="Blue" NavigateUrl="~/RegistrationForm/2012 Transfer Form.doc">Ohio Swimming Transfer Form</asp:HyperLink>
                <br />
                <br />
                <br />
                <asp:HyperLink ID="ReturnHyperlink" runat="server" Font-Underline="True" ForeColor="Blue"
                    NavigateUrl="~/Parents/FamilyView.aspx">&lt;&lt;&lt; RETURN TO FAMILY VIEW</asp:HyperLink>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Parents/FamilyView.aspx">&lt;&lt; Go Back to Family View</asp:HyperLink>
</asp:Content>
