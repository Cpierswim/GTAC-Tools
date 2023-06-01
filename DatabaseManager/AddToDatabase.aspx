<%@ Page MaintainScrollPositionOnPostback="true" Title="Set Swimmers Status as Added to Database" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AddToDatabase.aspx.cs" Inherits="DatabaseManager_AddToDatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:FormView ID="FormView1" runat="server" DataSourceID="IndividualSwimmerDataSource"
        OnDataBound="FormView_Databound" Width="100%" OnItemCommand="Button_Clicked">
        <ItemTemplate>
            <asp:Label ID="USAIDLabel" runat="server" Text='<%# Eval("USAID") %>' Visible="false"></asp:Label>
            <asp:Label ID="FamilyIDLabel" runat="server" Text='<%# Eval("FamilyID") %>' Visible="False"></asp:Label>
            <table style="width: 100%;">
                <tr style="vertical-align: top;">
                    <td>
                        <h3>
                            Primary Contact Parent</h3>
                        <br />
                        <asp:Label ID="PrimaryContactParentFirstNameLabel" runat="server" Text="FirstName"></asp:Label>
                        <asp:Label ID="PrimaryContactParentLastNameLabel" runat="server" Text="LastName"></asp:Label><br />
                        <asp:Label ID="PrimaryContactParentAddressLineOne" runat="server" Text="AddressLineOne"></asp:Label><br />
                        <asp:Label ID="PrimaryContactParentAddressLineTwo" runat="server" Text="AddressLineTwo"
                            Visible="false"></asp:Label>
                        <asp:Label ID="PrimaryContactParentCity" runat="server" Text="City"></asp:Label>
                        <asp:Label ID="PrimaryContactParentState" runat="server" Text="ST"></asp:Label>
                        <asp:Label ID="PrimaryContactParentZip" runat="server" Text="Zipco"></asp:Label>
                    </td>
                    <td>
                        <h3>
                            Secondary Contact Parent</h3>
                        <br />
                        <asp:Label ID="SecondaryContactParentFirstNameLabel" runat="server" Text="FirstName"
                            Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentLastNameLabel" runat="server" Text="LastName"
                            Visible="false"></asp:Label><br />
                        <asp:Label ID="SecondaryContactParentAddressLineOne" runat="server" Text="AddressLineOne"
                            Visible="false"></asp:Label><br />
                        <asp:Label ID="SecondaryContactParentAddressLineTwo" runat="server" Text="AddressLineTwo"
                            Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentCity" runat="server" Text="City" Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentState" runat="server" Text="ST" Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentZip" runat="server" Text="Zipco" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td>
                        <asp:Label ID="PrimaryContactParentHomePhone" runat="server" Text="Home Phone: 123-456-7890"></asp:Label>
                        <asp:Label ID="PrimaryContactParentCellPhone" runat="server" Text="Cell Phone: 123-456-7890"
                            Visible="false"></asp:Label>
                        <asp:Label ID="PrimaryContactParentWorkPhone" runat="server" Text="Work Phone: 123-456-7890"
                            Visible="false"></asp:Label>
                        <asp:Label ID="PrimaryContactParentEmail" runat="server" Text="E-mail: Random@email.com"
                            Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SecondaryContactParentHomePhone" runat="server" Text="Home Phone: 123-456-7890"
                            Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentCellPhone" runat="server" Text="Cell Phone: 123-456-7890"
                            Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentWorkPhone" runat="server" Text="Work Phone: 123-456-7890"
                            Visible="false"></asp:Label>
                        <asp:Label ID="SecondaryContactParentEmail" runat="server" Text="E-mail: Random@email.com"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td>
                        <asp:Label ID="PrimaryContactParentNotes" runat="server" Text="Primary Contact Notes"
                            Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SecondaryContactParentNotes" runat="server" Text="Secondary Contact Notes"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <h5>
                <asp:Label ID="AthleteLegalFirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>&nbsp;<asp:Label
                    ID="AthleteMiddleNameLabel" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>&nbsp;<asp:Label
                        ID="AthleteLastNameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label></h5>
            <table style="width: 100%;">
                <tr style="vertical-align: top;">
                    <td style="width: 50%;">
                        Preferred First Name:
                        <asp:Label ID="AthletePreferredFirstNameLabel" runat="server" Text='<%# Eval("PreferredName") %>'></asp:Label><br />
                        Gender:
                        <asp:Label ID="AthleteGenderLabel" runat="server" Text='<%# Eval("Gender") %>'></asp:Label><br />
                        Birthday:
                        <asp:Label ID="AthleteBirthdayLabel" runat="server" Text='<%# Eval("Birthday", "{0:d}") %>'></asp:Label><br />
                        Athlete Phone:
                        <asp:Label ID="AthletePhoneLabel" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label><br />
                        Email:
                        <asp:Label ID="AthleteEmailLabel" runat="server" Text='<%# Eval("Email") %>'></asp:Label><br />
                    </td>
                    <td style="width: 50%;">
                        Group:
                        <asp:Label ID="AthleteGroupLabel" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label><br />
                        Ethnicity:
                        <asp:Label ID="AthleteEthnicityLabel" runat="server" Text='<%# Eval("Ethnicity") %>'></asp:Label><br />
                        Disability:
                        <asp:Label ID="AthleteDisabilityLabel" runat="server" Text='<%# Eval("Disability") %>'></asp:Label><br />
                        US Citizen:
                        <asp:Label ID="AthleteCitizienshipLabel" runat="server" Text='<%# Eval("USCitizen") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Notes:<br />
                        <asp:Label ID="AthleteNotesLabel" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Spacer" runat="server" Text="<br /><br />" Visible="false"></asp:Label>
            <asp:Button ID="MarkAsInDatabaseButton" runat="server" Text="Mark as in Database" />
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="IndividualSwimmerDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSwimmerByUSAID" TypeName="SwimmersBLL">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="&quot;&quot;" Name="USAID" QueryStringField="USAID"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        DataSourceID="SwimmersReadyToAddDataSource" OnRowDataBound="Row_Databound" Width="608px">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="NameLink" runat="server">HyperLink</asp:HyperLink>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' Visible="False"></asp:Label>
                    <asp:Label ID="PreferredFirstNameLabel" runat="server" Text='<%# Eval("PreferredName") %>'
                        Visible="False"></asp:Label>
                    <asp:Label ID="USAIDLabel" runat="server" Text='<%# Eval("USAID") %>' Visible="False"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Created" DataFormatString="{0:G}" HeaderText="Created"
                SortExpression="Created" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersReadyToAddDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSwimmersReadyToAddButNotInDatabase" TypeName="SwimmersBLL">
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
