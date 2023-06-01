<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EntryControl.ascx.cs" Inherits="EntryControl" %>
<%@ Register Src="TimeControl/TimeControl.ascx" TagName="TimeControl" TagPrefix="uc1" %>
<asp:CheckBox ID="EntryCheckBox" runat="server" />
<uc1:TimeControl ID="TimeControl1" Enabled="false" runat="server" />

