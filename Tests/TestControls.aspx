<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" CodeFile="TestControls.aspx.cs" Inherits="TestControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dynamic Adding of Rows in ASP Table Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add New Row" />
    <asp:Table ID="Table1" runat="server"></asp:Table>
    </form>
</body>
</html>
