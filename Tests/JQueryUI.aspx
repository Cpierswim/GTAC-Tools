<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JQueryUI.aspx.cs" Inherits="Tests_JQueryUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<script src="../Scripts/jquery-1.6.4.js" type="text/javascript"></script>--%>
    <%--<script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>--%>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#accordion").accordion();
        });

        function ToggleClass() {

            var color = $("#Section2").css("background-color");
            if (color == 'rgb(255, 0, 0)') {
                $("#Section2").animate({ backgroundColor: "#507CD1" }, 2500).toggleClass("testerrorpanel");
            }
            else {
                $("#Section2").toggleClass("testerrorpanel");
                $("#Section2").animate({ backgroundColor: "red" }, 2500);
            }

            $("#Secion2HeaderText").toggleClass("errortext");
        }
    </script>
</head>
<body>
    <div id="accordion">
        <h3 id="Section1">
            <a href="#">Section 1</a></h3>
        <div>
            <p>
                Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque.
                Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a
                nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada.
                Vestibulum a velit eu ante scelerisque vulputate.
            </p>
        </div>
        <h3 id="Section2">
            <a id="Secion2HeaderText" href="#">Section 2</a></h3>
        <div>
            <p>
                Sed non urna. Donec et ante. Phasellus eu ligula. Vestibulum sit amet purus. Vivamus
                hendrerit, dolor at aliquet laoreet, mauris turpis porttitor velit, faucibus interdum
                tellus libero ac justo. Vivamus non quam. In suscipit faucibus urna.
            </p>
        </div>
        <h3 id="Section3">
            <a href="#">Section 3</a></h3>
        <div>
            <p>
                Nam enim risus, molestie et, porta ac, aliquam ac, risus. Quisque lobortis. Phasellus
                pellentesque purus in massa. Aenean in pede. Phasellus ac libero ac tellus pellentesque
                semper. Sed ac felis. Sed commodo, magna quis lacinia ornare, quam ante aliquam
                nisi, eu iaculis leo purus venenatis dui.
            </p>
            <ul>
                <li>List item one</li>
                <li>List item two</li>
                <li>List item three</li>
            </ul>
        </div>
        <h3 id="Section4">
            <a href="#">Section 4</a></h3>
        <div>
            <p>
                Cras dictum. Pellentesque habitant morbi tristique senectus et netus et malesuada
                fames ac turpis egestas. Vestibulum ante ipsum primis in faucibus orci luctus et
                ultrices posuere cubilia Curae; Aenean lacinia mauris vel est.
            </p>
            <p>
                Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus. Class
                aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.
            </p>
        </div>
    </div>
    <!-- End demo -->
    <div class="demo-description" style="display: none;">
        <p>
            Click headers to expand/collapse content that is broken into logical sections, much
            like tabs. Optionally, toggle sections open/closed on mouseover.
        </p>
        <p>
            The underlying HTML markup is a series of headers (H3 tags) and content divs so
            the content is usable without JavaScript.
        </p>
    </div>
    <p>
        <a href="#" onclick="ToggleClass()">Toggle Section 2</a>
    </p>
</body>
</html>
