using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Coach_ViewAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void DropDownListDataBound(object sender, EventArgs e)
    {
        int DefaultGroup = Convert.ToInt32(Profile.GroupID);

        for (int i = 0; i < DropDownList1.Items.Count; i++)
            DropDownList1.Items[i].Selected = (Convert.ToInt32(DropDownList1.Items[i].Value) == DefaultGroup);
    }
    int TotalYardage = 0;
    int TotalMeterage = 0;
    int TotalPractices = 0;
    int TotalOffered = 0;
    Table tbl;

    protected void DataBound(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count > 0)
        {
            TableCell LastNameCell = new TableCell();
            TableCell FirstNameCell = new TableCell();
            TableCell PercentageCell = new TableCell();
            TableCell AttendanceCell = new TableCell();
            TableCell OfferedCell = new TableCell();
            TableCell YardageCell = new TableCell();
            TableCell MeterageCell = new TableCell();
            TableCell NotesCell = new TableCell();

            Label DescriptionLabel = new Label();
            DescriptionLabel.Text = "Averages/Totals:";
            LastNameCell.Controls.Add(DescriptionLabel);
            LastNameCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");

            Label PercentageAverageLabel = new Label();
            if (TotalOffered == 0)
                PercentageAverageLabel.Text = "NA";
            else
                PercentageAverageLabel.Text = (Convert.ToDouble(TotalPractices) / Convert.ToDouble(TotalOffered)).ToString("P1");
            PercentageCell.Controls.Add(PercentageAverageLabel);
            PercentageCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            PercentageCell.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier New");

            Label TotalAttenedLabel = new Label();
            TotalAttenedLabel.Text = TotalPractices.ToString("N0");
            AttendanceCell.Controls.Add(TotalAttenedLabel);
            AttendanceCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            AttendanceCell.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier New");

            Label TotalOfferedLabel = new Label();
            TotalOfferedLabel.Text = TotalOffered.ToString("N0");
            OfferedCell.Controls.Add(TotalOfferedLabel);
            OfferedCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            OfferedCell.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier New");

            Label TotalYardageLabel = new Label();
            if (TotalYardage > 0)
                TotalYardageLabel.Text = TotalYardage.ToString("N0");
            YardageCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            YardageCell.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier New");
            YardageCell.Controls.Add(TotalYardageLabel);

            Label TotalMeterageLabel = new Label();
            if (TotalMeterage > 0)
                TotalMeterageLabel.Text = TotalMeterage.ToString("N0");
            MeterageCell.Controls.Add(TotalMeterageLabel);
            MeterageCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            MeterageCell.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier New");

            if (TotalMeterage > 0 && TotalYardage > 0)
            {
                //convert meters to yards and yards to meters;
                int TotalYardsIncludingConverted = TotalYardage + Convert.ToInt32(Math.Round((TotalMeterage * 1.0936133), 0));
                int TotalMetersIncludingConverted = TotalMeterage + Convert.ToInt32(Math.Round((TotalYardage * 0.9144), 0));

                Label ConversionLabel = new Label();
                ConversionLabel.Text = "Total Yards Converted: " + TotalYardage.ToString("N0") + "\nTotal Meterage Converted: " + TotalMeterage.ToString("N0");
                NotesCell.Controls.Add(ConversionLabel);
            }

            GridViewRow Footer = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

            if (this.GridView1.Rows[this.GridView1.Rows.Count - 1].RowState == DataControlRowState.Normal)
                Footer.RowState = DataControlRowState.Alternate;
            else if (this.GridView1.Rows[this.GridView1.Rows.Count - 1].RowState == DataControlRowState.Normal)
                Footer.RowState = DataControlRowState.Normal;


            TableRow NewRow = new TableRow();

            Footer.Cells.Add(FirstNameCell);
            Footer.Cells.Add(LastNameCell);
            Footer.Cells.Add(PercentageCell);
            Footer.Cells.Add(AttendanceCell);
            Footer.Cells.Add(OfferedCell);
            Footer.Cells.Add(YardageCell);
            Footer.Cells.Add(MeterageCell);
            Footer.Cells.Add(NotesCell);

            tbl.Rows.AddAt(tbl.Rows.Count - 1, Footer);

            
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView TempRow = e.Row.DataItem as DataRowView;

            if (!String.IsNullOrWhiteSpace(TempRow["Attended"].ToString()))
                TotalPractices += Convert.ToInt32(TempRow["Attended"]);
            if (!String.IsNullOrWhiteSpace(TempRow["Offered"].ToString()))
                TotalOffered += Convert.ToInt32(TempRow["Offered"]);
            if (!String.IsNullOrWhiteSpace(TempRow["Yardage"].ToString()))
                TotalYardage += Convert.ToInt32(TempRow["Yardage"]);
            if (!String.IsNullOrWhiteSpace(TempRow["Meterage"].ToString()))
                TotalMeterage += Convert.ToInt32(TempRow["Meterage"]);

            if (tbl == null)
                tbl = e.Row.Parent as Table;
        }
    }
}