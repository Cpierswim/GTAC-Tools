<%@ Page MaintainScrollPositionOnPostback="true" Title="Edit Swimmer" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EditSwimmer.aspx.cs" Inherits="Parents_Edit_Swimmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Unable to save swimmer. Please review the following errors:" />
    <strong>Edits are not saved unless you click "Update" at the bottom of the page.<br />
        <br />
    </strong>
    <asp:Button ID="EditParentsButton" runat="server" Text="Edit Parents" />
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="USAID"
        DataSourceID="SwimmerDataSource" ForeColor="Black" GridLines="Horizontal" DefaultMode="Edit"
        Font-Size="0.9em" OnDataBound="DetailsView_DataBound" OnItemUpdated="SwimmerUpdated">
        <EditRowStyle BackColor="White" Font-Bold="True" />
        <Fields>
            <asp:TemplateField HeaderText="Legal First Name:" SortExpression="FirstName">
                <EditItemTemplate>
                    <asp:TextBox ID="LegalFirstNameTextBox" runat="server" Columns="35" MaxLength="35"
                        Text='<%# Bind("FirstName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LegalFirstNameTextBox"
                        ErrorMessage="Legal first name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" Value='<%# Eval("FamilyID") %>' />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Font-Strikeout="False" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Preferred Name:" SortExpression="PreferredName">
                <EditItemTemplate>
                    <asp:TextBox ID="PreferredNameTextBox" runat="server" Columns="35" MaxLength="35"
                        Text='<%# Bind("PreferredName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PreferredNameTextBox"
                        ErrorMessage="A preferred name is required. (The name as it should appear in the heat sheet.) If you just want the legal first name to appear in the heat sheet, you must enter it into the Preferred Name."
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("PreferredName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("PreferredName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Middle Name:" SortExpression="MiddleName">
                <EditItemTemplate>
                    <asp:TextBox ID="MiddleNameTextBox" runat="server" Columns="35" MaxLength="35" Text='<%# Bind("MiddleName") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("MiddleName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("MiddleName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name:" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Columns="35" MaxLength="35" Text='<%# Bind("LastName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LastNameTextBox"
                        ErrorMessage="Last name is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Birthday:" SortExpression="Birthday">
                <EditItemTemplate>
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
                    <asp:DropDownList ID="BirthdayYearDropDownList" runat="server">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="CustomBirthdayYearRangeValidator" runat="server" ControlToValidate="CustomYearEntryTextBox"
                        Enabled="False" ErrorMessage="Birthday year entered must be a 4 year date." ForeColor="Red">*</asp:RangeValidator>
                    <asp:CustomValidator ID="CustomBirthdayValidator" runat="server" ControlToValidate="BirthdayYearDropDownList"
                        ErrorMessage="Birthday entered is not a valid date." OnServerValidate="ValidateBirthday"
                        ForeColor="Red">*</asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="YearValidator" runat="server" ControlToValidate="CustomYearEntryTextBox"
                        Enabled="False" ErrorMessage="A year is required." ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:Button ID="YearNotInListButton" runat="server" OnClick="YearNotInList_Clicked"
                        Text="Year not in list" />
                    <asp:HiddenField ID="BirthdayHiddenField" runat="server" Value='<%# Bind("Birthday") %>' />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Birthday") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Birthday") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gender:" SortExpression="Gender">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Gender") %>'>
                        <asp:ListItem Value="M">Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Gender") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone Number:" SortExpression="PhoneNumber">
                <EditItemTemplate>
                    <asp:TextBox ID="AthletePhoneNumberTextBox" runat="server" Columns="25" MaxLength="25"
                        Text='<%# Bind("PhoneNumber") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="AthletePhoneNumberTextBox"
                        ErrorMessage="Phone number not recognized.<br />All Phone numbers must be in the 123-456-7890 format." ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email:" SortExpression="Email">
                <EditItemTemplate>
                    <asp:TextBox ID="AthleteEmailTextBox" runat="server" Columns="35" MaxLength="100"
                        Text='<%# Bind("Email") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="AthleteEmailTextBox"
                        ErrorMessage="Email not recognized." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes:" SortExpression="Notes">
                <EditItemTemplate>
                    <asp:TextBox ID="NotesTextBox" runat="server" Columns="50" MaxLength="800" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:HiddenField ID="NotesHiddenField" runat="server" Value='<%# Eval("Notes") %>' />
                    <asp:HiddenField ID="SystemGenderatedHiddenField" runat="server" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active Swimmer:" SortExpression="Inactive">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList4" runat="server" SelectedValue='<%# Bind("Inactive") %>'>
                        <asp:ListItem Value="True">Inactive</asp:ListItem>
                        <asp:ListItem Value="False">Active</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("Inactive") %>' />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("Inactive") %>' Enabled="false" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group:" SortExpression="GroupID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="GroupsDataSource"
                        DataTextField="GroupName" DataValueField="GroupID" Enabled="False" SelectedValue='<%# Bind("GroupID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetAllGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("GroupID") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ethnicity:" SortExpression="Ethnicity">
                <EditItemTemplate>
                    <asp:HiddenField ID="OriginalEthnicityCodes" runat="server" Value='<%# Eval("Ethnicity") %>' />
                    <asp:CheckBoxList ID="EthnicityCheckBoxList" runat="server">
                        <asp:ListItem Value="Q">Black or African American</asp:ListItem>
                        <asp:ListItem Value="R">Asian</asp:ListItem>
                        <asp:ListItem Value="S">White</asp:ListItem>
                        <asp:ListItem Value="T">Hispanic or Latino</asp:ListItem>
                        <asp:ListItem Value="U">American Indian &amp; Alaska Native</asp:ListItem>
                        <asp:ListItem Value="V">Some Other Race</asp:ListItem>
                        <asp:ListItem Value="W">Native Hawaiian &amp; Other Pacific Islander</asp:ListItem>
                    </asp:CheckBoxList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Ethnicity") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Ethnicity") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="USCitizen:" SortExpression="USCitizen">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" SelectedValue='<%# Bind("USCitizen") %>'>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("USCitizen") %>' />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("USCitizen") %>' Enabled="false" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Disability:" SortExpression="Disability">
                <EditItemTemplate>
                    <asp:HiddenField ID="OriginalDisabilityCodesHiddenField" runat="server" Value='<%# Eval("Disability") %>' />
                    <asp:CheckBoxList ID="DisabilityCheckBoxList" runat="server">
                        <asp:ListItem Value="A">Legally Blind or Visually Impaired</asp:ListItem>
                        <asp:ListItem Value="B">Deaf or Hard of Hearing</asp:ListItem>
                        <asp:ListItem Value="C">Physical Disability such as amputation, cerebral palsy, dwarfism, spinal injury, mobility impairment</asp:ListItem>
                        <asp:ListItem Value="D">Cognitive Disability such as mental retardation, severe learning disorder, autism</asp:ListItem>
                    </asp:CheckBoxList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("Disability") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Disability") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ready For Database Manager<br />To Add To Database:"
                SortExpression="ReadyToAdd">
                <EditItemTemplate>
                    <asp:DropDownList ID="ReadyToAddDropDownList" runat="server" SelectedValue='<%# Bind("ReadyToAdd") %>'>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("Disability") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("Disability") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Font-Size="20px" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        OnClick="CancelButton_Clicked" Text="Cancel" 
                        PostBackUrl="~/OfficeManager/Swimmers.aspx"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" Font-Strikeout="False" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <InsertRowStyle Font-Strikeout="False" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    </asp:DetailsView>
    <asp:ObjectDataSource ID="SwimmerDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        OnUpdating="SwimmerUpdating" SelectMethod="GetSwimmerByUSAID" TypeName="SwimmersBLL"
        UpdateMethod="UpdateSwimmer">
        <SelectParameters>
            <asp:QueryStringParameter Name="USAID" QueryStringField="USAID" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="GroupID" Type="Int32" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="MiddleName" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="PreferredName" Type="String" />
            <asp:Parameter Name="Birthday" Type="DateTime" />
            <asp:Parameter Name="Gender" Type="String" />
            <asp:Parameter Name="PhoneNumber" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="Inactive" Type="Boolean" />
            <asp:Parameter Name="Ethnicity" Type="String" />
            <asp:Parameter Name="USCitizen" Type="Boolean" />
            <asp:Parameter Name="Disability" Type="String" />
            <asp:Parameter Name="original_USAID" Type="String" />
            <asp:Parameter Name="ReadyToAdd" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="USAIDHiddenField" runat="server" />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
