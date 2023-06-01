<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopTenSheets.aspx.cs" Inherits="OfficeManager_TopTenSheets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Top Ten Sheets</title>
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
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <asp:TextBox ID="PickDateTextBox" Text="Pick Date..." runat="server" 
            ontextchanged="DatePicked" />
        <ajaxToolkit:calendarextender id="PickDateTextBoxExtender" runat="server" popupbuttonid="txt1" targetcontrolid="PickDateTextBox" />
        <asp:Label ID="PageDoesNotLoadLabel" runat="server" Text='<- If page does not reload when you select the date, hit "Enter"'></asp:Label>
    </div>
    </form>
</body>
</html>
