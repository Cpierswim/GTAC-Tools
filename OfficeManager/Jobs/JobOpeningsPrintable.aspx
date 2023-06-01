<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobOpeningsPrintable.aspx.cs"
    Inherits="OfficeManager_Jobs_JobOpeningsPrintable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Printable Job Signups Page</title>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-373813-4']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="JobEventsGridView" runat="server" DataSourceID="JobSignupsObjectDataSource"
            AutoGenerateColumns="False" DataKeyNames="JobSignUpID" OnDataBinding="BeginJobSessionsDataBind"
            OnRowDataBound="JobSessionsRowDataBound" OnDataBound="JobSessionsDataBound" Width="684px"
            EnableViewState="False" Style="margin-top: 30px; margin: auto;">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <input id="Checkbox1" type="checkbox" />
                    </ItemTemplate>
                    <HeaderStyle Width="15px" />
                    <ItemStyle Width="15px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Signed Up" SortExpression="FamilyID">
                    <ItemTemplate>
                        <asp:Label ID="SignUpLabel" runat="server" style="margin-left:4px;"></asp:Label>
                        <asp:HiddenField ID="JobSignUpIDHiddenField" runat="server" Value='<%# Bind("JobSignUpID") %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="669px" />
                    <ItemStyle Width="669px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No jobs assigned to event.</EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="JobSignupsObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetByJobEventID" TypeName="JobSignUpsBLL">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="JobEventID" QueryStringField="JE"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
