<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TimeControl.ascx.cs" Inherits="TimeControl" %>
<asp:TextBox ID="TimeTextBox" runat="server" Columns="11" MaxLength="11"></asp:TextBox>
<asp:CustomValidator
    ID="TimeValidator" runat="server" ClientValidationFunction="ValidateTime"
    ControlToValidate="TimeTextBox" ErrorMessage="" 
    onservervalidate="ValidateTime" ValidateEmptyText="True"></asp:CustomValidator>
<asp:HiddenField ID="CourseHiddenField" runat="server" />
