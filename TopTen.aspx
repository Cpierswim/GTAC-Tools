<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" AutoEventWireup="true" CodeFile="TopTen.aspx.cs" Inherits="TopTen" %>

<%@ Register src="UserControls/RecordsDisplay/RecordsDisplay.ascx" tagname="RecordsDisplay" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTAC Top Ten</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:RecordsDisplay ID="RecordsDisplay1" runat="server" />
    
    </div>
    </form>
</body>
</html>
