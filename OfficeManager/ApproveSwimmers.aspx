<%@ Page MaintainScrollPositionOnPostback="true" Title="Approve Swimmer to be added to Database" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ApproveSwimmers.aspx.cs" Inherits="OfficeManager_ApproveSwimmers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:FormView ID="FormView1" runat="server" DataSourceID="IndividualSwimmerDataSource"
        Width="650px" OnDataBound="FormView_Databound">
        <ItemTemplate>
            <h1>
                <asp:Label ID="PreferredFirstNameLabel" runat="server" Text='<%# Eval("PreferredName") %>'></asp:Label>
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
            </h1>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%;">
                        Legal First Name:
                        <asp:Label ID="LegalFirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        Birthday:
                        <asp:Label ID="BirthdayLabel" runat="server" Text='<%# Eval("Birthday", "{0:D}") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%;">
                        Middle Name:
                        <asp:Label ID="MiddleNameLabel" runat="server" Text='<%# Eval("MiddleName") %>'></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        Gender:
                        <asp:Label ID="GenderLabel" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%;">
                        Added:
                        <asp:Label ID="CreatedTimeLabel" runat="server" Text='<%# Eval("Created", "{0:G}") %>'></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        Group:
                        <asp:Label ID="GroupLabel" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <asp:FormView ID="FormView2" runat="server" DataKeyNames="ParentID" DataSourceID="PrimaryParentDataSource"
                            Width="100%" OnDataBound="ParentDataBound">
                            <ItemTemplate>
                                <span style="text-align: center; font-weight: bold;">Primary Contact</span><br />
                                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>' />
                                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>' />
                                <br />
                                <asp:HiddenField ID="ZipHiddenField" runat="server" Value='<%# Eval("Zip") %>' />
                                <asp:HiddenField ID="StateHiddenField" runat="server" Value='<%# Eval("State") %>' />
                                <asp:HiddenField ID="CityHiddenField" runat="server" Value='<%# Eval("City") %>' />
                                <asp:HiddenField ID="AddressLineTwoHiddenField" runat="server" Value='<%# Eval("AddressLineTwo") %>' />
                                <asp:HiddenField ID="AddressLineOneHiddenField" runat="server" Value='<%# Eval("AddressLineOne") %>' />
                                <br />
                                <asp:Label ID="AddressLabel" runat="server" Text='<%# Bind("AddressLineOne") %>' />
                                &nbsp;<br />
                                <br />
                                HomePhone:
                                <asp:Label ID="HomePhoneLabel" runat="server" Text='<%# Bind("HomePhone") %>' />
                                <br />
                                CellPhone:
                                <asp:Label ID="CellPhoneLabel" runat="server" Text='<%# Bind("CellPhone") %>' />
                                <br />
                                WorkPhone:
                                <asp:Label ID="WorkPhoneLabel" runat="server" Text='<%# Bind("WorkPhone") %>' />
                                <br />
                                Email:
                                <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
                                <br />
                                <br />
                                <asp:Label ID="NotesLabel" runat="server" Text='<%# Bind("Notes") %>' />
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                    <td>
                        <asp:FormView ID="FormView3" runat="server" DataKeyNames="ParentID" DataSourceID="SecondaryParentDataSource"
                            Width="100%" OnDataBound="ParentDataBound">
                            <ItemTemplate>
                                <span style="text-align: center; font-weight: bold;">Secondary Contact</span><br />
                                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>' />
                                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>' />
                                <br />
                                <asp:HiddenField ID="ZipHiddenField" runat="server" Value='<%# Eval("Zip") %>' />
                                <asp:HiddenField ID="StateHiddenField" runat="server" Value='<%# Eval("State") %>' />
                                <asp:HiddenField ID="CityHiddenField" runat="server" Value='<%# Eval("City") %>' />
                                <asp:HiddenField ID="AddressLineTwoHiddenField" runat="server" Value='<%# Eval("AddressLineTwo") %>' />
                                <asp:HiddenField ID="AddressLineOneHiddenField" runat="server" Value='<%# Eval("AddressLineOne") %>' />
                                <br />
                                <asp:Label ID="AddressLabel" runat="server" Text='<%# Bind("AddressLineOne") %>' />
                                &nbsp;<br />
                                <br />
                                HomePhone:
                                <asp:Label ID="HomePhoneLabel" runat="server" Text='<%# Bind("HomePhone") %>' />
                                <br />
                                CellPhone:
                                <asp:Label ID="CellPhoneLabel" runat="server" Text='<%# Bind("CellPhone") %>' />
                                <br />
                                WorkPhone:
                                <asp:Label ID="WorkPhoneLabel" runat="server" Text='<%# Bind("WorkPhone") %>' />
                                <br />
                                Email:
                                <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
                                <br />
                                <br />
                                <asp:Label ID="NotesLabel" runat="server" Text='<%# Bind("Notes") %>' />
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
            </table>
            <asp:ObjectDataSource ID="SecondaryParentDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetSecondaryContactParentByFamilyID" TypeName="ParentsBLL">
                <SelectParameters>
                    <asp:QueryStringParameter Name="FamilyID" QueryStringField="FamilyID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="PrimaryParentDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetPrimaryContactParentByFamilyID" TypeName="ParentsBLL">
                <SelectParameters>
                    <asp:QueryStringParameter Name="FamilyID" QueryStringField="FamilyID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <asp:HiddenField ID="FamilyIDHiddenField" runat="server" Value='<%# Eval("FamilyID") %>' />
            <br />
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="IndividualSwimmerDataSource" runat="server" InsertMethod="InsertSwimmer"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSwimmerByUSAID"
        TypeName="SwimmersBLL">
        <InsertParameters>
            <asp:Parameter Name="USAID" Type="String" />
            <asp:Parameter Name="FamilyID" Type="Int32" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="MiddleName" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="PreferredName" Type="String" />
            <asp:Parameter Name="Birthday" Type="DateTime" />
            <asp:Parameter Name="Gender" Type="String" />
            <asp:Parameter Name="ReadyToAdd" Type="Boolean" />
            <asp:Parameter Name="IsInDatabase" Type="Boolean" />
            <asp:Parameter Name="PhoneNumber" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="Inactive" Type="Boolean" />
            <asp:Parameter Name="GroupID" Type="Int32" />
            <asp:Parameter Name="EthnicityCodes" Type="String" />
            <asp:Parameter Name="USCitizen" Type="Boolean" />
            <asp:Parameter Name="DisabilityCodes" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="USAID" QueryStringField="USAID"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USAID"
        DataSourceID="SwimmersDataSource" OnRowCommand="ReadyToAddButton_Clicked" OnRowDataBound="RowDatabound"
        Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="FullNameHyperlink" runat="server">HyperLink</asp:HyperLink>
                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>' Visible="False"></asp:Label>
                    <asp:Label ID="FirstNameLabel" runat="server" OnDataBinding="FirstNameBinding" Text='<%# Bind("FirstName") %>'
                        Visible="False"></asp:Label>
                    <asp:Label ID="USAIDLabel" runat="server" Text='<%# Bind("USAID") %>' Visible="False"></asp:Label>
                    <asp:Label ID="FamilyIDLabel" runat="server" Text='<%# Eval("FamilyID") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Created" DataFormatString="{0:G}" HeaderText="Created"
                SortExpression="Created" />
            <asp:TemplateField HeaderText="Group" SortExpression="GroupID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("GroupID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="GroupLabel" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" Text="Ready To Add" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SwimmersDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSwimmersNotReadyToAdd" TypeName="SwimmersBLL"></asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
