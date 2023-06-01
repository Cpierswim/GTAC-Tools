<%@ Page MaintainScrollPositionOnPostback="true" Title="School Info" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AthleteSchoolInfo.aspx.cs"
    Inherits="Coach_AthleteSchoolInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
    <br />
    <asp:DropDownList ID="SchoolNameDropDownList" runat="server" AutoPostBack="True"
        DataSourceID="DirectSQLDatasource" DataTextField="SchoolName" DataValueField="SchoolName"
        OnDataBound="SchoolsDropDownListDataBound">
        <asp:ListItem>All Schools</asp:ListItem>
    </asp:DropDownList>
    <asp:SqlDataSource ID="DirectSQLDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        SelectCommand="SELECT SchoolName FROM SchoolInfos GROUP BY SchoolName ORDER BY SchoolName">
    </asp:SqlDataSource>
    <asp:DropDownList ID="GroupsDropDownList" runat="server" DataSourceID="GroupsDataSource"
        DataTextField="GroupName" DataValueField="GroupName" AutoPostBack="True" OnDataBound="GroupsDropDownListDataBound">
    </asp:DropDownList>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
    <asp:DropDownList ID="GradeDropDownList" runat="server" AutoPostBack="True">
        <asp:ListItem Value="-1">All Grades</asp:ListItem>
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
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            Count:
            <asp:Label runat="server" ID="CountLabel"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                DataSourceID="AdvancedSchoolDataSource1" OnRowDataBound="RowDataBound" CellPadding="4"
                ForeColor="#333333" GridLines="None" OnDataBound="GridDataBound" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="PreferredName" HeaderText="First Name" SortExpression="PreferredName" />
                    <asp:BoundField DataField="GroupName" HeaderText="Group" SortExpression="GroupName" />
                    <asp:BoundField DataField="SchoolName" HeaderText="School" SortExpression="SchoolName" />
                    <asp:TemplateField HeaderText="Grade" SortExpression="Grade">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Grade") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="GradeLabel" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GradeDropDownList" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GroupsDropDownList" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="SchoolNameDropDownList" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorted" />
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="AdvancedSchoolDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAdvancedSchoolInfos" TypeName="SchoolInfoBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="SchoolNameDropDownList" Name="SchoolName" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="GroupsDropDownList" Name="GroupName" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="GradeDropDownList" Name="GradeValue" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
