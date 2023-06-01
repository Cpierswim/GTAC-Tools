using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TopTen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int max = int.Parse(Request.QueryString["A"]);
            int min = int.Parse(Request.QueryString["I"]);

            String MaxString = max.ToString();
            String MinString = min.ToString();

            if (max >= 15)
                MaxString = "And Over";
            if (min < 8)
                MinString = "And Under";
            String GenderString = String.Empty;
            if (Request.QueryString["S"] == "M" || Request.QueryString["S"] == "m")
                GenderString = "Boys";
            else if (Request.QueryString["S"] == "F" || Request.QueryString["S"] == "f")
                GenderString = "Girls";
            else
                throw new Exception();

            String CourseString = string.Empty;
            if (Request.QueryString["C"] == "Y" || Request.QueryString["C"] == "y")
                CourseString = "Short Course";
            else if (Request.QueryString["C"] == "L" || Request.QueryString["C"] == "l")
                CourseString = "Long Course";
            else
                throw new Exception();

            String AgePart = string.Empty;

            if (min < 8)
                AgePart = max + " " + MinString;
            else
            {
                if (max >= 15)
                    AgePart = min + " " + MaxString;
                else
                    AgePart = min + "-" + max;
            }

            String Title = "GTAC " + AgePart + " " + GenderString + " " + CourseString + " " + "Top Ten";

            this.Title = Title;
        }
        catch (Exception)
        {
        }
    }
}