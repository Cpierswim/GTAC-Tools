<%@ Page MaintainScrollPositionOnPostback="true" Title="Members Map" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MembersMap.aspx.cs" Inherits="All_MembersMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
        html
        {
            height: 100%;
        }
        body
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
        #map_canvas
        {
            height: 100%;
        }
    </style>
    <script id="include_map" type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"> 
    </script>
    <script id="adjust_width" type="text/javascript">
        function AdjustWidth() {
            var winW = 630, winH = 460;
            if (document.body && document.body.offsetWidth) {
                winW = document.body.offsetWidth;
                winH = document.body.offsetHeight;
            }
            if (document.compatMode == 'CSS1Compat' && document.documentElement && document.documentElement.offsetWidth) {
                winW = document.documentElement.offsetWidth;
                winH = document.documentElement.offsetHeight;
            }
            if (window.innerWidth && window.innerHeight) {
                winW = window.innerWidth;
                winH = window.innerHeight;
            }


            var PageDiv = document.getElementById("PageDiv");

            if ((winW < PageDiv.offsetWidth) || (winH < PageDiv.offsetHeight)) {
                //Set Widths
                PageDiv.style.width = winW.toString() + "px";
                PageDiv.style.border = "0px";
                PageDiv.style.margin = "0px";
                var MainDiv = document.getElementById("MainDiv");
                MainDiv.style.padding = "0px";
                MainDiv.style.margin = "0px";
                MainDiv.style.minHeight = "0px";

                var HeaderDiv = document.getElementById("HeaderDiv");
                HeaderDiv.style.visibility = "hidden";
                HeaderDiv.style.height = "0px";
                HeaderDiv.style.width = "0px";

                var TitleDiv = document.getElementById("TitleDiv");
                TitleDiv.style.visibility = "hidden";
                TitleDiv.style.height = "0px";
                TitleDiv.style.width = "0px";

                var ClearDiv = document.getElementById("ClearDiv");
                ClearDiv.style.visibility = "hidden";
                ClearDiv.style.height = "0px";
                ClearDiv.style.width = "0px";

                var FooterDiv = document.getElementById("FooterDiv");
                FooterDiv.style.visibility = "hidden";
                FooterDiv.style.height = "0px";
                FooterDiv.style.width = "0px";
                FooterDiv.style.padding = "0px";


                //Set Heights
                var DropDownBox = document.getElementById("ctl00_MainContent_DisplayingLabel");
                var DropDownBoxHeight = DropDownBox.offsetHeight; //Adjusting for uneditable margins
                var GoBackLink = document.getElementById("ctl00_MainContent_HyperLink1");
                var GoBackLinkHeight = GoBackLink.offsetHeight;

                var CalculatedHeight = winH - DropDownBoxHeight - GoBackLinkHeight;

                var MapTable = document.getElementById("MapTable");
                MapTable.style.height = CalculatedHeight.toString() + "px";

                var MapCanvas = document.getElementById("map_canvas");
                MapCanvas.style.border = "0px";
                MapCanvas.style.padding = "0px";
                MapCanvas.style.width = document.body.offsetWidth;

                //disappear texts
                var SwimmersFromGroupLabel = document.getElementById("ctl00_MainContent_SwimmersFromGroupLabel");
                SwimmersFromGroupLabel.style.visibility = "hidden";
                SwimmersFromGroupLabel.innerHTML = "";
                var DisplayingLabel = document.getElementById("ctl00_MainContent_DisplayingLabel");
                DisplayingLabel.style.visibility = "hidden";
                DisplayingLabel.innerHTML = "";
            }

            function removeElement(parentDiv, childDiv) {
                if (childDiv == parentDiv) {
                    alert("The parent div cannot be removed.");
                }
                else if (document.getElementById(childDiv)) {
                    var child = document.getElementById(childDiv);
                    var parent = document.getElementById(parentDiv);
                    parent.removeChild(child);
                }
                else {
                    alert("Child div has already been removed or does not exist.");
                    return false;
                }
            }
        }
    </script>
    <%-- <script id="dynamicresize" type="text/javascript">
        var addEvent = function (elem, type, eventHandle) {
            if (elem == null || elem == undefined) return;
            if (elem.addEventListener) {
                elem.addEventListener(type, eventHandle, false);
            } else if (elem.attachEvent) {
                elem.attachEvent("on" + type, eventHandle);
            }
        };

        //addEvent(window, "resize", function () { AdjustWidth(); });
        //TODO: Create future funtion that restores all the previous settings upon an upsize.
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="SwimmersFromGroupLabel" runat="server" Text="View Swimmers from Group: "></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="GroupsDataSource"
        DataTextField="GroupName" DataValueField="GroupID" AppendDataBoundItems="True">
        <asp:ListItem Value="-1">All</asp:ListItem>
    </asp:DropDownList>
    <asp:ObjectDataSource ID="GroupsDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveGroups" TypeName="GroupsBLL"></asp:ObjectDataSource>
    <asp:Label ID="DisplayingLabel" runat="server" Text="Label" Visible="False"></asp:Label>
    <div>
        <table style="width: 100%; height: 500px" id="MapTable">
            <tr>
                <td id="map_canvas" style="width: 100%;" align="center">
                </td>
            </tr>
        </table>
        <asp:Label ID="ErrorLabel" runat="server" Visible="false" Text="Label"></asp:Label>
    </div>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Features/Default.aspx">&lt;&lt; Go Back to Admin Page</asp:HyperLink>
</asp:Content>
