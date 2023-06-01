using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_TopTenSheets : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Date"] != null)
        {
            try
            {
                DateTime Date = DateTime.Parse(Request.QueryString["Date"]);
                this.PickDateTextBox.Visible = false;
                this.PageDoesNotLoadLabel.Visible = false;

                this.CreateRecordPanels(Date);
            }
            catch (Exception)
            {
                this.PickDateTextBox.Visible = true;
                PageDoesNotLoadLabel.Visible = true;
                this.PickDateTextBox.Text = "Reenter Date...";
            }
        }
    }
    protected void DatePicked(object sender, EventArgs e)
    {
        try
        {
            DateTime Date = DateTime.Parse(this.PickDateTextBox.Text);
            this.PickDateTextBox.Visible = false;

            this.CreateRecordPanels(Date);
        }
        catch (Exception)
        {
            this.PickDateTextBox.Visible = true;
            this.PickDateTextBox.Text = "Reenter Date...";
        }
    }

    private String GetEventString(SwimTeamDatabase.RecordsRow Record)
    {
        String S = "";
        if (Record.Age <= 8)
            S = "8 & Under";
        else if (Record.Age >= 9 && Record.Age <= 10)
            S = "9-10";
        else if (Record.Age >= 11 && Record.Age <= 12)
            S = "11-12";
        else if (Record.Age >= 13 && Record.Age <= 14)
            S = "13-14";
        else if (Record.Age >= 15)
            S = "Open";

        if (Record.Sex.ToUpper() == "F")
            S += " Girls";
        else
            S += " Boys";

        S += " " + Record.Distance + " ";

        if (Record.Course.ToUpper() == "Y")
            S += "Yard ";
        else if (Record.Course.ToUpper() == "L")
            S += "Meter ";
        else
            S += "SCM ";

        switch (Record.Stroke)
        {
            case 1:
                S += "Freestyle";
                break;
            case 2:
                S += "Backstroke";
                break;
            case 3:
                S += "Breaststroke";
                break;
            case 4:
                S += "Buttefly";
                break;
            case 5:
                S += "IM";
                break;
        }

        return S;
    }

    private void CreateRecordPanels(DateTime Date)
    {
        RecordsBLL RecordsAdapter = new RecordsBLL();
        SwimTeamDatabase.RecordsDataTable Records = RecordsAdapter.GetTopTensByDate(Date);
        bool FloatLeft = true;
        for (int i = 0; i < Records.Count; i++)
        {
            if (Records[i].Age <= 12)
            {
                Panel RecordPanel = new Panel();
                if (FloatLeft)
                    RecordPanel.Style.Add("float", "left");
                else
                    RecordPanel.Style.Add("float", "right");
                RecordPanel.Width = new Unit("250px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.Height, "250px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.MarginRight, "45px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.MarginTop, "22px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.MarginBottom, "22px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                RecordPanel.Style.Add(HtmlTextWriterStyle.BorderWidth, "3px");
                RecordPanel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                RecordPanel.Style.Add(HtmlTextWriterStyle.BorderColor, "Blue");
                RecordPanel.Style.Add("page-break-inside", "avoid");


                Label TopTenLabel = new Label();
                TopTenLabel.Text = "GTAC Top Ten";
                TopTenLabel.Style.Add(HtmlTextWriterStyle.FontSize, "30px");
                TopTenLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "Bolder");
                TopTenLabel.ForeColor = System.Drawing.Color.Blue;
                RecordPanel.Controls.Add(TopTenLabel);

                Label FirstBreakLabel = new Label();
                FirstBreakLabel.Text = "<br /><br />";
                RecordPanel.Controls.Add(FirstBreakLabel);

                Panel MeetNamePanel = new Panel();
                MeetNamePanel.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                MeetNamePanel.Style.Add(HtmlTextWriterStyle.MarginRight, "10px");

                Label MeetNameLabel = new Label();
                MeetNameLabel.Text = Records[i].MeetName;
                MeetNameLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                MeetNameLabel.Style.Add(HtmlTextWriterStyle.FontSize, "20px");
                MeetNamePanel.Controls.Add(MeetNameLabel);

                RecordPanel.Controls.Add(MeetNamePanel);

                Label SecondBreakLabel = new Label();
                SecondBreakLabel.Text = "<br />";
                RecordPanel.Controls.Add(SecondBreakLabel);

                Label DateLabel = new Label();
                DateLabel.Text = Records[i].Date.ToString("MMMM d, yyyy");
                DateLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                RecordPanel.Controls.Add(DateLabel);

                Label ThirdBreakLabel = new Label();
                ThirdBreakLabel.Text = "<br /><br />";
                RecordPanel.Controls.Add(ThirdBreakLabel);

                Label NameLabel = new Label();
                String Name = "";
                if (String.IsNullOrWhiteSpace(Records[i].Preferred))
                    Name += Records[i].First + " ";
                else
                    Name += Records[i].Preferred + " ";
                Name += Records[i].Last + "";
                NameLabel.Text = Name;
                NameLabel.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                NameLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                NameLabel.Style.Add(HtmlTextWriterStyle.FontSize, "20px");
                RecordPanel.Controls.Add(NameLabel);

                Label FourthBreakLabel = new Label();
                FourthBreakLabel.Text = "<br /><br />";
                RecordPanel.Controls.Add(FourthBreakLabel);

                Label EventLabel = new Label();
                EventLabel.Text = this.GetEventString(Records[i]);
                RecordPanel.Controls.Add(EventLabel);

                Label TimeLabel = new Label();
                HyTekTime Time = new HyTekTime(Records[i].Time, Records[i].Course);
                TimeLabel.Text = "<br />" + Time.ToStringNoCourse();
                RecordPanel.Controls.Add(TimeLabel);


                Page.Controls.Add(RecordPanel);
            }
        }
    }
}