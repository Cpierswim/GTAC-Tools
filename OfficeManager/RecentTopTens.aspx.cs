using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_RecentTopTens : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime CurrentTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");

            DateTime FirstDayOfMonth = new DateTime(CurrentTime.Year, CurrentTime.Month, 1);
            Calendar1.SelectedDate = FirstDayOfMonth;
        }

        
    }
    protected void FormatRow(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            String Last = ((HiddenField)e.Row.FindControl("LastNameHiddenField")).Value;
            String First = ((HiddenField)e.Row.FindControl("FirstNameHiddenField")).Value;
            String Preferred = ((HiddenField)e.Row.FindControl("PreferredNameHiddenField")).Value;

            String FullName = "";

            if (String.IsNullOrEmpty(Preferred))
                FullName = First + " " + Last;
            else 
                FullName = Preferred + " " + Last;

            ((Label)e.Row.FindControl("NameLabel")).Text = FullName;

            Label SexLabel = (Label)e.Row.FindControl("SexLabel");
            if (SexLabel.Text == "f" || SexLabel.Text == "F")
                SexLabel.Text = "Female";
            else if (SexLabel.Text == "m" || SexLabel.Text == "M")
                SexLabel.Text = "Male";

            String Distance = ((HiddenField)e.Row.FindControl("DistanceHiddenField")).Value;
            String Stroke = ((HiddenField)e.Row.FindControl("StrokeHiddenField")).Value;
            String Course = ((HiddenField)e.Row.FindControl("CourseHiddenField")).Value;

            String EventLabelText = Distance + " ";
            if (Course == "y" || Course == "Y")
                EventLabelText += "Yard ";
            else if (Course == "l" || Course == "L")
                EventLabelText += "Meter ";

            if (Stroke == "1")
                EventLabelText += "Freestyle";
            else if (Stroke == "2")
                EventLabelText += "Backstroke";
            else if (Stroke == "3")
                EventLabelText += "Breaststroke";
            else if (Stroke == "4")
                EventLabelText += "Butterfly";
            else if (Stroke == "5")
                EventLabelText += "IM";

            ((Label)e.Row.FindControl("EventLabel")).Text = EventLabelText;

            Label TimeLabel = (Label)e.Row.FindControl("TimeLabel");
            String Time = TimeLabel.Text;

            if (Time.Length == 0)
                Time = "00";
            else if (Time.Length == 1)
                Time = "0" + Time;

            String hundreths = Time.Substring(Time.Length - 2, 2);
            String SecondsAsString = Time.Substring(0, Time.Length - 2);
            int seconds = 0;
            if (!String.IsNullOrEmpty(SecondsAsString))
                seconds = int.Parse(SecondsAsString);
            int minutes = seconds / 60;
            seconds = seconds % 60;
            int hours = minutes / 60;
            minutes = minutes % 60;

            String TimeAsString = "";
            if (hours > 0)
            {
                TimeAsString += hours + ":";
                if (minutes < 10)
                    TimeAsString += "0";
                TimeAsString += minutes + ":";
                if (seconds < 10)
                    TimeAsString += "0";
                TimeAsString += seconds + "." + hundreths;
            }
            else if (minutes > 0)
            {
                TimeAsString += minutes + ":";
                if (seconds < 10)
                    TimeAsString += "0";
                TimeAsString += seconds + "." + hundreths;
            }
            else
            {
                TimeAsString += seconds + "." + hundreths;
            }

            TimeLabel.Text = TimeAsString;
        }
    }
    protected void PrintableVersionClicked(object sender, EventArgs e)
    {
        Response.Redirect("TopTenSheets.aspx?Date=" + this.Calendar1.SelectedDate.ToString("MM/d/yyy"));
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count > 0)
        {
            this.Button1.Visible = true;
            this.HyperLink1.Visible = true;
            this.OnlyWorksLabel.Visible = true;
        }
        else
        {
            this.Button1.Visible = false;
            this.HyperLink1.Visible = false;
            this.OnlyWorksLabel.Visible = false;
        }
    }
}