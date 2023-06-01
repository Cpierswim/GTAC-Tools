using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class TimeControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                this.DefaultCourse = DefaultCourse;
            }
            this.TimeTextBox.Attributes.Add("onblur", "CheckTime(this)");
            //if (TimeControlCount < 2)
            //{
            this.AddJavaScript();
            this.AddCSS();
            //}
        }
        catch (Exception)
        {
        }
    }

    private bool AddJavascriptToPage;

    public TimeControl()
    {
        LoadFromPreviousValues = false;
        AddJavascriptToPage = true;
    }

    private bool LoadFromPreviousValues;
    public TimeControl(bool AddJavascriptAndCSSToPage, bool LoadFromPreviousValues)
    {
        this.AddJavascriptToPage = AddJavascriptAndCSSToPage;
        this.LoadFromPreviousValues = LoadFromPreviousValues;
    }

    public TimeControl(bool LoadFromPreviousValues)
    {
        AddJavascriptToPage = true;
        this.LoadFromPreviousValues = LoadFromPreviousValues;
    }

    public int Columns
    {
        get
        {
            return this.TimeTextBox.Columns;
        }
        set
        {
            this.TimeTextBox.Columns = value;
        }
    }

    public enum Course { LongCourseMeters, ShortCourseMeters, ShortCourseYards };

    public Course DefaultCourse
    {
        get
        {
            if (String.IsNullOrEmpty(this.CourseHiddenField.Value))
                return TimeControl.Course.ShortCourseYards;
            else
            {
                if (this.CourseHiddenField.Value == "Y")
                    return TimeControl.Course.ShortCourseYards;
                else if (this.CourseHiddenField.Value == "S")
                    return TimeControl.Course.ShortCourseMeters;
                else if (this.CourseHiddenField.Value == "L")
                    return TimeControl.Course.LongCourseMeters;
                else
                    throw new Exception("Unreachable Error");
            }
        }
        set
        {
            if (value == Course.LongCourseMeters)
                this.CourseHiddenField.Value = "L";
            else if (value == Course.ShortCourseYards)
                this.CourseHiddenField.Value = "Y";
            else if (value == Course.ShortCourseMeters)
                this.CourseHiddenField.Value = "S";
        }
    }

    public void AddJavaScript()
    {
        String csName = "TextBoxTimeCheck";
        Type csType = this.GetType();
        ClientScriptManager cs = Page.ClientScript;

        if (!cs.IsClientScriptBlockRegistered(csName))
        {
            StringBuilder csText = new StringBuilder();
            csText.Append("\n<script type=\"text/javascript\">"); csText.Append("\n");

            //CheckTime Function
            csText.Append("function CheckTime(source)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var trimmed = source.value.replace(/^\\s+|\\s+$/g, '') ;"); csText.Append("\n");
            csText.Append("\n");
            //csText.Append("\t"); csText.Append("//var DefaultCourse = document.getElementById(\"");
            //if (this.MeetCourseClientString == null)
            //    csText.Append(this.CourseHiddenField.NamingContainer.ClientID.ToString()
            //        + "_" + this.CourseHiddenField.ID);
            //else
            //    csText.Append(this.MeetCourseClientString);
            //csText.Append("\").value;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var DefaultCourse = $(source).parent().children(\":nth-child(4)\").get(0).value;"); csText.Append("\n");
            csText.Append("\n");
            csText.Append("\t"); csText.Append("if (trimmed.length == 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("source.className = \"Invalid\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else if (trimmed.toUpperCase() == \"NT\")"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("source.value = trimmed.toUpperCase();"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("source.className = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("try"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var Course = GetCourse(trimmed, DefaultCourse);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var Time = GetTime(trimmed);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var TimeArray = GetTimeArray(Time);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("source.value = SetFromValues(TimeArray, Course);"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("source.className = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("catch (Error)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("source.className = \"Invalid\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //GetTime function
            csText.Append("function GetTime(Time)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (AcceptableCourseValue(Time.charAt(Time.length - 1)))"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("return Time.substr(0, (Time.length - 1));"); csText.Append("\n");
            csText.Append("\t"); csText.Append("return Time;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //IsNumeric function
            csText.Append("function isNumeric(elem)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var numericExpression = /^[0-9]+$/;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (elem.match(numericExpression))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("return true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("return false;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //Get Course function
            csText.Append("function GetCourse(Time, DefaultCourse)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (isNumeric(Time.charAt(Time.length - 1)))"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("return DefaultCourse;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (AcceptableCourseValue(Time.charAt(Time.length - 1)))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t\t"); csText.Append("return Time.charAt(Time.length - 1);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t\t"); csText.Append("throw \"Not acceptable course value\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //AcceptableCourseValue function
            csText.Append("function AcceptableCourseValue(Char)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if ((Char == \"Y\") || (Char == \"y\"))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("return true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if ((Char == \"S\") || (Char == \"s\"))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("return true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if ((Char == \"L\") || (Char == \"l\"))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("return true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("return false;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //GetTimeArray function
            csText.Append("function GetTimeArray(Time)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var ReturnArray = new Array();"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (Time.length == 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var Hundredths = 0;"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (Time.indexOf(\".\") == -1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (Time.length > 0 && Time.length <= 2)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("if (!isNumeric(Time))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("Hundredths = Time;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("TimeArray[0] = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("TimeArray[1] = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("TimeArray[2] = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("TimeArray[3] = Hundredths;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("return TimeArray;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("Time = Time.substr(0, (Time.length - 2)) + \".\" + Time.substring(Time.length - 2);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (Time.indexOf(\".\") == (Time.length - 1))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var TempTime = Time.substring((Time.indexOf(\".\") + 1));"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (TempTime.indexOf(\".\") != -1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (TempTime.length > 2)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("Hundredths = Time.substring((Time.indexOf(\".\") + 1));"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (!isNumeric(Hundredths))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (Hundredths.length == 1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("Hundredths = Hundredths + 0;"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("Time = Time.substr(0, Time.indexOf(\".\"));"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (Time.length > 2 && Time.indexOf(\":\") == -1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("Time = InsertColons(Time);"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("var TimeArray = ToStringArray(Time, \":\");"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("for (i = 0; i < TimeArray.length; i++)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (!isNumeric(TimeArray[i]))"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("var hours = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var minutes = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var seconds = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (TimeArray.length == 3)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("hours = TimeArray[0];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("minutes = TimeArray[1];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("seconds = TimeArray[2];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else if (TimeArray.length == 2)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("minutes = TimeArray[0];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("seconds = TimeArray[1];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else if (TimeArray.length == 1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("seconds = TimeArray[0];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("} else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("ReturnArray[0] = hours;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("ReturnArray[1] = minutes;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("ReturnArray[2] = seconds;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("ReturnArray[3] = Hundredths;"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("var FirstNonZero = -1;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("for (i = 0; i < ReturnArray.length; i++)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (ReturnArray[i] != 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("FirstNonZero = i;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("i = ReturnArray.length;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (FirstNonZero == -1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\";"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("for (i = (FirstNonZero + 1); i < ReturnArray.length; i++)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var testvalue = ReturnArray[i];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var length = testvalue.toString().length"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (length != 2)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("throw \"Malformed Time\""); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("while (ReturnArray[2] >= 60)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("ReturnArray[1]++;"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("ReturnArray[2] = ReturnArray[2] - 60;"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("if (ReturnArray[2].toString().length == 1)"); csText.Append("\n");
            csText.Append("\t\t\t"); csText.Append("ReturnArray[2] = \"0\" + ReturnArray[2];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("while (ReturnArray[1] >= 60)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnArray[0]++;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnArray[1] = ReturnArray[1] - 60;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (ReturnArray[1].toString().length == 1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnArray[1] = \"0\" + ReturnArray[1];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("return ReturnArray;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //ToStringArray function
            csText.Append("function ToStringArray(StringToChop, Splitter)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var ReturnArray = new Array();"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var index = 0;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var WorkingOnString = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("for (i = 0; i < StringToChop.length; i++)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("if (StringToChop.charAt(i) != Splitter)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("WorkingOnString = WorkingOnString + StringToChop.charAt(i);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("if (i != 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnArray[index] = WorkingOnString;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("WorkingOnString = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("index++;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (WorkingOnString.length > 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnArray[index] = WorkingOnString;"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("return ReturnArray;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //SetFromValues function
            csText.Append("function SetFromValues(TimeArray, Course)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if(TimeArray[0] == 0 && TimeArray[1] != 0 && TimeArray[1].substr(0, 1) == \"0\")"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("TimeArray[1] = TimeArray[1].substr(1, 1);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var result = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (TimeArray[0] != 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("result = TimeArray[0] + \":\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (TimeArray[1] != \"0\")"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("result = result + TimeArray[1];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (TimeArray[2] != \"0\")"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("if(TimeArray[2] < 10  && TimeArray[2].substr(0,1) != \"0\")"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t\t"); csText.Append("result = result + \":0\" + TimeArray[2];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t\t"); csText.Append("result = result + \":\" + TimeArray[2];"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("result = result + \".\" + TimeArray[3] + Course.toUpperCase();"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("return result;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");

            //InsertColons function
            csText.Append("function InsertColons(Time)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var ReturnString = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("while (Time.length > 1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var test1 = Time.length - 2;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var test2 = Time.length - 1;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("var TempString = Time.substr(test1);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("Time = Time.slice(0, (Time.length - 2));"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnString = \":\" + TempString + ReturnString;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("if (Time.length == 1)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnString = Time + ReturnString;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("ReturnString = ReturnString.substr(1);"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("return ReturnString;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append("\n");
            csText.Append("");

            //ValidateTime function
            csText.Append("function ValidateTime(source, args)"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var ValidatingControl = document.getElementById(source.controltovalidate.toString());"); csText.Append("\n");
            csText.Append("\t"); csText.Append("ValidatingControl.value = ValidatingControl.value.replace(/^\\s+|\\s+$/g, '');"); csText.Append("\n");
            csText.Append("\n");
            csText.Append("\t"); csText.Append("if(!ValidatingControl.disabled)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            //csText.Append("\t"); csText.Append("\t"); csText.Append("var DefaultCourse = document.getElementById(\"");
            //csText.Append(this.CourseHiddenField.NamingContainer.ClientID.ToString()
            //    + "_" + this.CourseHiddenField.ID);
            //csText.Append("\").value;"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("var DefaultCourse = $(\"#\" + source.id.toString()).parent().children(\":nth-child(4)\").get(0).value;"); csText.Append("\n");

            csText.Append("\t"); csText.Append("\t"); csText.Append("if (ValidatingControl.value.length == 0)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.className = \"Invalid\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("args.IsValid = false;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else if (ValidatingControl.value.toUpperCase() == \"NT\")"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.value = ValidatingControl.value.toUpperCase();"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.className = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("args.IsValid = true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("try"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var Course = GetCourse(ValidatingControl.value, DefaultCourse);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var Time = GetTime(ValidatingControl.value);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("var TimeArray = GetTimeArray(Time);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.value = SetFromValues(TimeArray, Course);"); csText.Append("\n");
            csText.Append("\t"); csText.Append(""); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.className = \"\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("args.IsValid = true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("catch (Error)"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("ValidatingControl.className = \"Invalid\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("args.IsValid = false;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("\t"); csText.Append("args.IsValid = true;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append(""); csText.Append("\n");
            csText.Append("</script>");

            String script = csText.ToString().Replace(" ", null).Replace("\t", null).Replace("\n", null);
            cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
        }


    }
    public void AddCSS()
    {
        String csName = "InvalidCSS";
        Type csType = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        if (!cs.IsClientScriptBlockRegistered(csName))
        {
            StringBuilder csText = new StringBuilder();
            csText.Append("\n<style type=\"text/css\">"); csText.Append("\n");
            csText.Append(".Invalid"); csText.Append("\n");
            csText.Append("{"); csText.Append("\n");
            csText.Append("\t"); csText.Append("color: White;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("background-color: Red;"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append("</style>");

            cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
        }
    }
    private HyTekTime _Time;
    public HyTekTime Time
    {
        get
        {
            if (IsValidTime(this.TimeTextBox.Text))
            {
                this._Time = new HyTekTime(this.TimeTextBox.Text);
                return this._Time;
            }
            return null;
        }
        set
        {
            this._Time = value;
        }
    }
    protected void ValidateTime(object source, ServerValidateEventArgs args)
    {
        String Value = args.Value.Trim();
        if (Value.Length == 0)
            args.IsValid = false;
        else if (Value.ToUpper() == "NT")
            args.IsValid = true;
        else
        {
            try
            {
                Course TimeCourse = GetCourse(Value, this.DefaultCourse);
                String TimePartOnly = GetTimePartOnly(Value);
                args.IsValid = IsValidTime(TimePartOnly);
                TimeTextBox.Text = FormatTimeString(TimePartOnly) + GetCourseString(TimeCourse);
            }
            catch (InvalidTimeException)
            {
                args.IsValid = false;
            }
        }
    }

    private static bool IsNumberCharacter(char CharacterToCheck)
    {
        switch (CharacterToCheck)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return true;
            default: return false;
        }
    }
    private static bool IsAllNumbers(String PossibleNumbers)
    {
        foreach (char X in PossibleNumbers)
        {
            if (IsNumberCharacter(X) == false)
                return false;
        }
        return true;
    }
    private static Course TurnCharToCourse(char CourseString)
    {
        if (CourseString == 'l' || CourseString == 'L')
            return Course.LongCourseMeters;
        else if (CourseString == 'y' || CourseString == 'Y')
            return Course.ShortCourseYards;
        else if (CourseString == 's' || CourseString == 'S')
            return Course.ShortCourseMeters;
        else
            throw new InvalidCourseException();
    }
    private Course GetCourse(String Value, Course DefaultCourse)
    {
        if (IsNumberCharacter(Char.Parse(Value.Substring(Value.Length - 1))))
            return DefaultCourse;
        else
        {
            try
            {
                return TurnCharToCourse(char.Parse(Value.Substring(Value.Length - 1)));
            }
            catch (InvalidCourseException)
            {
                throw new InvalidTimeException();
            }
        }
    }
    private static String GetTimePartOnly(String Value)
    {
        try
        {
            TurnCharToCourse(char.Parse(Value.Substring(Value.Length - 1)));
            return Value.Substring(0, Value.Length - 1);
        }
        catch (InvalidCourseException)
        {
            return Value;
        }
    }
    private static bool IsValidTime(String Time)
    {
        Time = Time.ToUpper();
        if (Time == "NT")
            return true;
        if (Time.EndsWith("L") || Time.EndsWith("S") || Time.EndsWith("Y"))
            if (char.IsNumber(Time, (Time.Length - 2)))
                Time = Time.Substring(0, (Time.Length - 1));
        char[] seperator = new char[1];
        seperator[0] = '.';

        String[] SplitByPeriod = Time.Split(seperator);
        if (SplitByPeriod.Length > 2)
            return false;
        if (SplitByPeriod.Length == 1)
            if (SplitByPeriod[0].Length > 2)
            {
                SplitByPeriod[0] = SplitByPeriod[0].Substring(0, SplitByPeriod[0].Length - 2) + "." +
                    SplitByPeriod[0].Substring(SplitByPeriod[0].Length - 2);
                SplitByPeriod = SplitByPeriod[0].Split(seperator);
            }
        try
        {
            int hundredths = int.Parse(SplitByPeriod[SplitByPeriod.Length - 1]);
            if (hundredths > 99)
                return false;
        }
        catch (InvalidCastException)
        {
            return false;
        }
        catch (FormatException)
        {
            return false;
        }

        String TimeBeforePeriod = SplitByPeriod[0];
        if (string.IsNullOrEmpty(TimeBeforePeriod))
            return true;
        if (TimeBeforePeriod.Length > 2 && !TimeBeforePeriod.Contains(":"))
            TimeBeforePeriod = AddColons(TimeBeforePeriod);
        seperator[0] = ':';
        String[] SplitByColon = TimeBeforePeriod.Split(seperator);
        if (SplitByColon.Length > 3)
            return false;
        for (int i = 0; i < SplitByColon.Length; i++)
        {
            try
            {
                if (!(i == 0 && String.IsNullOrEmpty(SplitByColon[i])))
                    if (int.Parse(SplitByColon[i]) > 99)
                        return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        return true;
    }
    private static String FormatTimeString(String Time)
    {
        if (string.IsNullOrEmpty(Time))
            return "NT"; // currently will never be reached, but possibly in the future

        char[] seperator = new char[1];
        seperator[0] = '.';

        String[] SplitByPeriod = Time.Split(seperator);
        int hundredths = 0, seconds = 0, minutes = 0, hours = 0;
        if (SplitByPeriod.Length == 1)
            if (SplitByPeriod[0].Length > 2)
            {
                SplitByPeriod[0] = SplitByPeriod[0].Substring(0, SplitByPeriod[0].Length - 2) + "." +
                    SplitByPeriod[0].Substring(SplitByPeriod[0].Length - 2);
                SplitByPeriod = SplitByPeriod[0].Split(seperator);
            }

        hundredths = int.Parse(SplitByPeriod[SplitByPeriod.Length - 1]);
        seperator[0] = ':';
        if (SplitByPeriod.Length > 1 && !SplitByPeriod[0].Contains(":"))
            SplitByPeriod[0] = AddColons(SplitByPeriod[0]);

        String[] BeforePeriod = SplitByPeriod[0].Split(seperator);
        if (BeforePeriod.Length == 1)
            seconds = int.Parse(BeforePeriod[0]);
        else if (BeforePeriod.Length == 2)
        {
            if (String.IsNullOrEmpty(BeforePeriod[1]))
                seconds = 0;
            else
                seconds = int.Parse(BeforePeriod[1]);
            if (String.IsNullOrEmpty(BeforePeriod[0]))
                minutes = 0;
            else
                minutes = int.Parse(BeforePeriod[0]);
        }
        else if (BeforePeriod.Length == 3)
        {
            seconds = int.Parse(BeforePeriod[2]);
            minutes = int.Parse(BeforePeriod[1]);
            hours = int.Parse(BeforePeriod[0]);
        }
        while (hundredths > 100)
        {
            hundredths = hundredths - 100;
            seconds++;
        }
        while (seconds > 60)
        {
            seconds = seconds - 60;
            minutes++;
        }
        while (minutes > 60)
        {
            minutes = minutes - 60;
            hours++;
        }

        String ReturnString = "";
        if (hours != 0)
        {
            ReturnString = ReturnString + hours + ":";
            if (minutes < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + minutes + ":";
            if (seconds < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + seconds + ".";
            if (hundredths < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + hundredths;
        }
        else if (minutes != 0)
        {
            ReturnString = ReturnString + minutes + ":";
            if (seconds < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + seconds + ".";
            if (hundredths < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + hundredths;
        }
        else
        {
            if (seconds < 10)
                ReturnString = ReturnString + ":0" + seconds + ".";
            else
                ReturnString = ReturnString + ":" + seconds + ".";
            if (hundredths < 10)
                ReturnString = ReturnString + "0";
            ReturnString = ReturnString + hundredths;
        }


        return ReturnString;
    }
    private static String GetCourseString(Course Value)
    {
        if (Value == Course.LongCourseMeters)
            return "L";
        else if (Value == Course.ShortCourseYards)
            return "Y";
        else
            return "S";
    }

    private static String AddColons(String StringToAddColonsTo)
    {
        String ReturnString = "";
        while (StringToAddColonsTo.Length > 2)
        {
            ReturnString = ":" + StringToAddColonsTo.Substring(StringToAddColonsTo.Length - 2) + ReturnString;
            StringToAddColonsTo = StringToAddColonsTo.Substring(0, StringToAddColonsTo.Length - 2);
        }
        if (StringToAddColonsTo.Length <= 2)
            ReturnString = StringToAddColonsTo + ReturnString;
        if (ReturnString.StartsWith(":"))
            ReturnString = ReturnString.Substring(1);
        return ReturnString;
    }

    private class InvalidTimeException : Exception
    {
    }
    private class InvalidCourseException : Exception { }

    private String _dynamicNamingContainer;
    private String DynamicNamingContainer
    {
        get
        {
            if (String.IsNullOrEmpty(_dynamicNamingContainer))
            {
                for (int i = 0; i < Request.Form.Keys.Count; i++)
                    if (Request.Form.Keys[i].Contains("TimeTextBox"))
                    {
                        String Key = Request.Form.Keys[i];
                        int index = Key.IndexOf("TimeTextBox");
                        _dynamicNamingContainer = Key.Substring(0, index);
                    }
            }

            return _dynamicNamingContainer;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.TimeTextBox.Enabled;
        }
        set
        {
            this.TimeTextBox.Enabled = value;
        }
    }

    //private TextBox TimeTextBox;
    //private CustomValidator TimeValidator;
    //private HiddenField CourseHiddenField;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (TimeTextBox == null)
        {
            TimeTextBox = new TextBox();
            TimeTextBox.ID = "TimeTextBox";
            TimeTextBox.Columns = 11;
            TimeTextBox.MaxLength = 11;
            this.Controls.Add(TimeTextBox);
        }

        if (TimeValidator == null)
        {
            //TimeValidator = new CustomValidator();
            TimeValidator.ID = "TimeValidator";
            TimeValidator.ClientValidationFunction = "ValidateTime";
            TimeValidator.ControlToValidate = TimeTextBox.ID;
            TimeValidator.ErrorMessage = "*";
            TimeValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.ValidateTime);
            this.Controls.Add(TimeValidator);
        }

        if (CourseHiddenField == null)
        {
            CourseHiddenField = new HiddenField();
            CourseHiddenField.ID = "CourseHiddenField";
        }
        this.Controls.Add(CourseHiddenField);
        if (this.LoadFromPreviousValues)
        {
            if (this.TimeTextBox != null)
                if (Request.Form[this.TimeTextBox.UniqueID] != null)
                    this.TimeTextBox.Text = Request.Form[this.TimeTextBox.UniqueID];
            if (this.CourseHiddenField != null)
                if (Request.Form[this.CourseHiddenField.UniqueID] != null)
                    this.CourseHiddenField.Value = Request.Form[this.CourseHiddenField.UniqueID];
        }
    }

    //public void ReLoadValues()
    //{
    //    if (Request.Form[this.TimeTextBox.UniqueID] != null)
    //        this.TimeTextBox.Text = Request.Form[this.TimeTextBox.UniqueID];
    //    if (Request.Form[this.CourseHiddenField.UniqueID] != null)
    //        this.CourseHiddenField.Value = Request.Form[this.CourseHiddenField.UniqueID];
    //}

    private String _MeetCourseClientString;
    public String MeetCourseClientString
    {
        get
        {
            return this._MeetCourseClientString;
        }
        set
        {
            this._MeetCourseClientString = value;
        }
    }
}