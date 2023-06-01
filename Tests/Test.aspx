<%@ Page MaintainScrollPositionOnPostback="true" Title="Test1" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Test.aspx.cs" Inherits="Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function CheckTime(source)
        {
            var DefaultCourse = document.getElementById("ctl00_MainContent_CourseHiddenField").value;

            if (source.value.length == 0)
            {
                source.className = "Invalid";
            } else if (source.value.toUpperCase() == "NT")
            {
                source.value = source.value.toUpperCase();
                source.className = "";
            }
            else
            {
                try
                {
                    var Course = GetCourse(source.value, DefaultCourse);
                    var Time = GetTime(source.value);
                    var TimeArray = GetTimeArray(Time);
                    source.value = SetFromValues(TimeArray, Course);

                    source.className = "";
                }
                catch (Error)
                {
                    source.className = "Invalid";
                }
            }
        }

        function GetTime(Time)
        {
            if (AcceptableCourseValue(Time.charAt(Time.length - 1)))
                return Time.substr(0, (Time.length - 1));
            return Time;
        }

        function isNumeric(elem)
        {
            var numericExpression = /^[0-9]+$/;
            if (elem.match(numericExpression))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        function GetCourse(Time, DefaultCourse)
        {
            if (isNumeric(Time.charAt(Time.length - 1)))
                return DefaultCourse;
            else
            {
                if (AcceptableCourseValue(Time.charAt(Time.length - 1)))
                    return Time.charAt(Time.length - 1);
                else
                    throw "Not acceptable course value";
            }
        }

        function AcceptableCourseValue(Char)
        {
            if ((Char == "Y") || (Char == "y"))
                return true;
            if ((Char == "S") || (Char == "s"))
                return true;
            if ((Char == "L") || (Char == "l"))
                return true;
            return false;
        }



        function GetTimeArray(Time)
        {
            var ReturnArray = new Array();
            if (Time.length == 0)
                throw "Malformed Time";
            var Hundredths = 0;

            if (Time.indexOf(".") == -1)
            {
                if (Time.length > 0 && Time.length <= 2)
                {
                    if (!isNumeric(Time))
                        throw "Malformed Time";

                    Hundredths = Time;
                    TimeArray[0] = 0;
                    TimeArray[1] = 0;
                    TimeArray[2] = 0;
                    TimeArray[3] = Hundredths;
                    return TimeArray;
                }
                else
                {
                    Time = Time.substr(0, (Time.length - 2)) + "." + Time.substring(Time.length - 2);
                }
            }
            else
            {
                if (Time.indexOf(".") == (Time.length - 1))
                    throw "Malformed Time";

                var TempTime = Time.substring((Time.indexOf(".") + 1));
                if (TempTime.indexOf(".") != -1)
                    throw "Malformed Time";
                if (TempTime.length > 2)
                    throw "Malformed Time";
            }

            Hundredths = Time.substring((Time.indexOf(".") + 1));
            if (!isNumeric(Hundredths))
                throw "Malformed Time";
            if (Hundredths.length == 1)
                Hundredths = Hundredths + 0;

                Time = Time.substr(0, Time.indexOf("."));

            if (Time.length > 2 && Time.indexOf(":") == -1)
                Time = InsertColons(Time);

            var TimeArray = ToStringArray(Time, ":");


            for (i = 0; i < TimeArray.length; i++)
                if (!isNumeric(TimeArray[i]))
                    throw "Malformed Time";

            var hours = 0;
            var minutes = 0;
            var seconds = 0;
            if (TimeArray.length == 3)
            {
                hours = TimeArray[0];
                minutes = TimeArray[1];
                seconds = TimeArray[2];
            }
            else if (TimeArray.length == 2)
            {
                minutes = TimeArray[0];
                seconds = TimeArray[1];
            }
            else if (TimeArray.length == 1)
            {
                seconds = TimeArray[0];
            } else
                throw "Malformed Time";

            

            ReturnArray[0] = hours;
            ReturnArray[1] = minutes;
            ReturnArray[2] = seconds;
            ReturnArray[3] = Hundredths;

            var FirstNonZero = -1;
            for (i = 0; i < ReturnArray.length; i++)
            {
                if (ReturnArray[i] != 0)
                {
                    FirstNonZero = i;
                    i = ReturnArray.length;
                }
            }

            if (FirstNonZero == -1)
                throw "Malformed Time";


            for (i = (FirstNonZero + 1); i < ReturnArray.length; i++)
            {
                var testvalue = ReturnArray[i];
                var length = testvalue.toString().length
                if (length != 2)
                    throw "Malformed Time"
            }

            while (ReturnArray[2] >= 60)
            {
                ReturnArray[1]++;
                ReturnArray[2] = ReturnArray[2] - 60;
                if (ReturnArray[2].toString().length == 1)
                    ReturnArray[2] = "0" + ReturnArray[2];
            }
            while (ReturnArray[1] >= 60)
            {
                ReturnArray[0]++;
                ReturnArray[1] = ReturnArray[1] - 60;
                if (ReturnArray[1].toString().length == 1)
                    ReturnArray[1] = "0" + ReturnArray[1];
            }

            return ReturnArray;
        }


        function ToStringArray(StringToChop, Splitter)
        {
            var ReturnArray = new Array();
            var index = 0;
            var WorkingOnString = "";
            for (i = 0; i < StringToChop.length; i++)
            {
                if (StringToChop.charAt(i) != Splitter)
                    WorkingOnString = WorkingOnString + StringToChop.charAt(i);
                else
                {
                    if (i != 0)
                    {
                        ReturnArray[index] = WorkingOnString;
                        WorkingOnString = "";
                        index++;
                    }
                }
            }

            if (WorkingOnString.length > 0)
                ReturnArray[index] = WorkingOnString;


            return ReturnArray;
        }

        function SetFromValues(TimeArray, Course)
        {
            var result = "";
            if (TimeArray[0] != 0)
                result = TimeArray[0] + ":";
            if (TimeArray[1] != "0")
            {
                result = result + TimeArray[1] + ":";
            }
            if (TimeArray[2] != "0")
            {
                result = result + TimeArray[2];
            }
            result = result + "." + TimeArray[3] + Course.toUpperCase();

            return result;
        }

        function InsertColons(Time)
        {
            var ReturnString = "";
            while (Time.length > 1)
            {
                var test1 = Time.length - 2;
                var test2 = Time.length - 1;
                var TempString = Time.substr(test1);
                Time = Time.slice(0, (Time.length - 2));
                ReturnString = ":" + TempString + ReturnString;
            }

            if (Time.length == 1)
                ReturnString = Time + ReturnString;
            else
                ReturnString = ReturnString.substr(1);

            return ReturnString; 
        }

        
        
    </script>
    <style type="text/css">
        .Invalid
        {
            background-color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:TextBox ID="TextBox1" runat="server" Columns="10"></asp:TextBox><br />
    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateTime"
        ControlToValidate="TextBox1" ErrorMessage="CustomValidator"></asp:CustomValidator>
    <asp:HiddenField ID="CourseHiddenField" runat="server" Value="Y" />
    <br />
    <asp:TextBox ID="Output" runat="server" Columns="120"></asp:TextBox>
</asp:Content>
