<%@ Page Title="Job Types Editor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="JobTypes.aspx.cs" Inherits="OfficeManager_Jobs_JobTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="JobTypeID"
                    DataSourceID="ObjectDataSource1" Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Columns="25" MaxLength="250" Text='<%# Bind("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Columns="50" MaxLength="500" Text='<%# Bind("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time To Learn" SortExpression="TimeToLearn">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Columns="20" MaxLength="50" Text='<%# Bind("TimeToLearn") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("TimeToLearn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Length" SortExpression="Length">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Columns="20" MaxLength="50" Text='<%# Bind("Length") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Length") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button ID="AddJobButton" runat="server" Text="Add Job" OnClick="SwitchPanels" />
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server">
                <asp:FormView ID="FormView1" runat="server" DataKeyNames="JobTypeID" DataSourceID="ObjectDataSource1"
                    DefaultMode="Insert" Visible="False" OnItemCommand="FormViewButtonClicked" OnItemInserting="FormViewInserting"
                    OnItemInserted="FormViewInserted">
                    <EditItemTemplate>
                        JobTypeID:
                        <asp:Label ID="JobTypeIDLabel1" runat="server" Text='<%# Eval("JobTypeID") %>' />
                        <br />
                        Description:
                        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                        <br />
                        Name:
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                        <br />
                        TimeToLearn:
                        <asp:TextBox ID="TimeToLearnTextBox" runat="server" Text='<%# Bind("TimeToLearn") %>' />
                        <br />
                        Length:
                        <asp:TextBox ID="LengthTextBox" runat="server" Text='<%# Bind("Length") %>' />
                        <br />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update" />
                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' MaxLength="250"
                                        Columns="80" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is Required"
                                ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Description:
                                </td>
                                <td>
                                    <asp:TextBox ID="DescriptionTextBox" runat="server" MaxLength="500" Text='<%# Bind("Description") %>'
                                        Columns="80" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Description is Required"
                                ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Time To Learn:
                                </td>
                                <td>
                                    <asp:TextBox ID="TimeToLearnTextBox" runat="server" MaxLength="50" Text='<%# Bind("TimeToLearn") %>'
                                        Columns="35" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Job Length of Time:
                                </td>
                                <td>
                                    <asp:TextBox ID="LengthTextBox" MaxLength="50" runat="server" Text='<%# Bind("Length") %>'
                                        Columns="35" />
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                            Text="Insert" />
                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="Cancel" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        JobTypeID:
                        <asp:Label ID="JobTypeIDLabel" runat="server" Text='<%# Eval("JobTypeID") %>' />
                        <br />
                        Description:
                        <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>' />
                        <br />
                        Name:
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
                        <br />
                        TimeToLearn:
                        <asp:Label ID="TimeToLearnLabel" runat="server" Text='<%# Bind("TimeToLearn") %>' />
                        <br />
                        Length:
                        <asp:Label ID="LengthLabel" runat="server" Text='<%# Bind("Length") %>' />
                        <br />
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit" />
                        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                            Text="New" />
                    </ItemTemplate>
                </asp:FormView>
                <asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="AddJobButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="FormView1" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        OldValuesParameterFormatString="{0}" SelectMethod="GetAll" TypeName="JobTypesBLL"
        UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Insert">
        <DeleteParameters>
            <asp:Parameter Name="JobTypeID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TimeToLearn" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TimeToLearn" Type="String" />
            <asp:Parameter Name="Length" Type="String" />
            <asp:Parameter Name="JobTypeID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
