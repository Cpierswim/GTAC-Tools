<%@ Page MaintainScrollPositionOnPostback="true" Title="Edit Parents" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditParents.aspx.cs"
    Inherits="Parents_EditParents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
        $(function () {
            $('input:submit').button();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="SwitchingErrorLabel" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    Editing Parent:
    <asp:DropDownList ID="ParentSeclectorDropDownList" runat="server" AutoPostBack="True"
        OnSelectedIndexChanged="SelectedParent_Changed">
        <asp:ListItem Value="Primary">Primary Contact Parent</asp:ListItem>
        <asp:ListItem Value="Secondary">Secondary Contact Parent</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="SwitchParentsButton" runat="server" Text="Set this Parent as the Primary Contact"
        OnClick="SwitchParentsClicked" />
    <br />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ParentID"
        DataSourceID="ParentsDataSource" DefaultMode="Edit" Width="125px" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
        GridLines="Horizontal" OnDataBound="DetailsView_DataBound" OnItemUpdated="ParentUpdated">
        <EditRowStyle Font-Bold="True" />
        <Fields>
            <asp:TemplateField HeaderText="Contact Status:" SortExpression="PrimaryContact">
                <EditItemTemplate>
                    <asp:Label ID="ContactStatusLabel" runat="server" Text='<%# Eval("PrimaryContact") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name:" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' Columns="35"
                        MaxLength="35"></asp:TextBox><asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator"
                            runat="server" ErrorMessage="Last name is required." ControlToValidate="LastNameTextBox"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:HiddenField ID="FamilyIDHiddenField" runat="server" Value='<%# Eval("FamilyID") %>' />
                    <asp:HiddenField ID="PrimaryContactHiddenField" runat="server" Value='<%# Eval("PrimaryContact") %>' />
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name:" SortExpression="FirstName">
                <EditItemTemplate>
                    <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>'
                        Columns="35" MaxLength="35"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ControlToValidate="FirstNameTextBox"
                        ErrorMessage="First name required." ForeColor="Red">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address :" SortExpression="AddressLineOne">
                <EditItemTemplate>
                    <asp:TextBox ID="AddressLineOneTextBox" runat="server" Text='<%# Bind("AddressLineOne") %>'
                        Columns="50" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="AddressRequiredFieldValidator" runat="server" ControlToValidate="AddressLineOneTextBox"
                        ErrorMessage="Address Required." ForeColor="Red">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" SortExpression="AddressLineTwo">
                <EditItemTemplate>
                    <asp:TextBox ID="AddressLineTwo" runat="server" Text='<%# Bind("AddressLineTwo") %>'
                        Columns="50" MaxLength="35"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City:" SortExpression="City">
                <EditItemTemplate>
                    <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' Columns="50"
                        MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CityRequiredFieldValidator" runat="server" ControlToValidate="CityTextBox"
                        ErrorMessage="City required." ForeColor="Red">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State:" SortExpression="State">
                <EditItemTemplate>
                    <asp:DropDownList ID="PrimaryState" runat="server" SelectedValue='<%# Bind("State") %>'>
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
                        <asp:ListItem>OH</asp:ListItem>
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
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Zip:" SortExpression="Zip">
                <EditItemTemplate>
                    <asp:TextBox ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' Columns="10"
                        MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ZipCodeRequiredFieldValidator" runat="server" ControlToValidate="ZipTextBox"
                        ErrorMessage="Zip code required." ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ZipCodeRegularExpressionValidator" runat="server"
                        ControlToValidate="ZipTextBox" ErrorMessage="Zip code not recognized." ForeColor="Red"
                        ValidationExpression="\d{5}(-\d{4})?">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Home Phone:" SortExpression="HomePhone">
                <EditItemTemplate>
                    <asp:TextBox ID="HomePhoneTextBox" runat="server" Text='<%# Bind("HomePhone") %>'
                        Columns="25" MaxLength="25"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="HomePhoneRequiredFieldValidator" runat="server" ControlToValidate="HomePhoneTextBox"
                        ErrorMessage="Home phone required." ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="HomePhoneRegularExpressionValidator" runat="server"
                        ControlToValidate="HomePhoneTextBox" ErrorMessage="Home phone not recognized as a phone number.  All phone numbers must be in the format 123-456-7890."
                        ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cell Phone:" SortExpression="CellPhone">
                <EditItemTemplate>
                    <asp:TextBox ID="CellPhoneTextBox" runat="server" Text='<%# Bind("CellPhone") %>'
                        Columns="25" MaxLength="25"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="CellPhoneRegularExpressionValidator" runat="server"
                        ControlToValidate="CellPhoneTextBox" ErrorMessage="Cell phone number not recognized as a phone number.  All phone numbers must be in the format 123-456-7890."
                        ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Work Phone:" SortExpression="WorkPhone">
                <EditItemTemplate>
                    <asp:TextBox ID="WorkPhoneTextBox" runat="server" Text='<%# Bind("WorkPhone") %>'
                        Columns="25" MaxLength="35"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email:" SortExpression="Email">
                <EditItemTemplate>
                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' Columns="50"
                        MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                        ControlToValidate="EmailTextBox" ErrorMessage="Email not recognized as an email address."
                        ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes:" SortExpression="Notes">
                <EditItemTemplate>
                    <asp:TextBox ID="NotesText" runat="server" Text='<%# Bind("Notes") %>' Columns="50"
                        Rows="3" MaxLength="256" TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="SaveEditsButton" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Save Edits" Font-Size="20px"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    </asp:DetailsView>
    <asp:ObjectDataSource ID="ParentsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPrimaryContactParentByFamilyID" TypeName="ParentsBLL" UpdateMethod="UpdateParent"
        OnUpdating="ParentUpdating">
        <SelectParameters>
            <asp:SessionParameter Name="FamilyID" SessionField="FamilyID" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="AddressLineOne" Type="String" />
            <asp:Parameter Name="AddressLineTwo" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="HomePhone" Type="String" />
            <asp:Parameter Name="CellPhone" Type="String" />
            <asp:Parameter Name="WorkPhone" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="PrimaryContact" Type="Boolean" />
            <asp:Parameter Name="original_ParentID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Parents/FamilyView.aspx">&lt;&lt; Go Back to Family View</asp:HyperLink>
</asp:Content>
