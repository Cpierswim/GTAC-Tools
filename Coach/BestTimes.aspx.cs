using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Coach_BestTimes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
                this.DropDownList1.SelectedValue = Profile.GroupID;
        }
        catch (Exception)
        {
        }
    }

    private String LastSubHeaderSwimmer = "";
    private SwimTeamDatabase.SwimmersDataTable Swimmers;

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Swimmers == null)
        {
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            Swimmers = SwimmersAdapter.GetSwimmersByGroupID(int.Parse(this.DropDownList1.SelectedValue));
        }
        else if (Swimmers.Count == 0)
        {
            SwimmersBLL SwimmersAdapter = new SwimmersBLL();
            Swimmers = SwimmersAdapter.GetSwimmersByGroupID(int.Parse(this.DropDownList1.SelectedValue));
        }

        GridView SendingGrid = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView TempRow = e.Row.DataItem as DataRowView;

            if (LastSubHeaderSwimmer != TempRow["USAID"].ToString())
            {
                LastSubHeaderSwimmer = TempRow["USAID"].ToString();

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow SubHeader = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();

                    cell.ColumnSpan = SendingGrid.Columns.Count;
                    cell.Width = new Unit("100%");
                    cell.Style.Add("font-weight", "bold");
                    cell.Style.Add("background-color", "#9DB0C6");
                    cell.Style.Add("color", "white");
                    HtmlGenericControl span = new HtmlGenericControl("span");

                    String USAID = TempRow["USAID"].ToString();

                    for (int i = 0; i < Swimmers.Count; i++)
                        if (Swimmers[i].USAID == USAID)
                        {
                            span.InnerText = Swimmers[i].PreferredName + " " + Swimmers[i].LastName;
                            break;
                        }

                    cell.Controls.Add(span);
                    SubHeader.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, SubHeader);
                }
            }

            Label EventLabel = e.Row.FindControl("EventDescriptionLabel") as Label;
            int Distance = int.Parse(TempRow["Distance"].ToString());
            int StrokeCode = int.Parse(TempRow["Stroke"].ToString());
            String Course = TempRow["Course"].ToString();
            EventLabel.Text = MeetEventHelper.GetEventName(Distance, StrokeCode, Course, MeetEventHelper.StrokeStringLength.Middle);

            Label TimeLabel = e.Row.FindControl("TimeLabel") as Label;

            int Time = int.Parse(TimeLabel.Text);

            HyTekTime CustomTime = new HyTekTime(Time, Course);

            TimeLabel.Text = CustomTime.ToString();
        }
    }
    protected void GridDatabound(object sender, EventArgs e)
    {
        //bool rowDeleted = false;
        //while (!rowDeleted)
        //{
        //    if (this.GridView1.Rows.Count > 0)
        //    {
        //        Table tbl = this.GridView1.Rows[0].Parent as Table;
        //        for (int i = 0; i < tbl.Rows.Count; i++)
        //        {
        //            Label TimeLabel = tbl.Rows[i].FindControl("TimeLabel") as Label;
        //            if (TimeLabel != null)
        //            {
        //                HyTekTime Time = new HyTekTime(TimeLabel.Text);
        //                if (Time.Course != this.DropDownList2.SelectedValue)
        //                {
        //                    tbl.Rows.RemoveAt(i);
        //                    rowDeleted = true;
        //                    break;
        //                }
        //            }
        //        }
        //        rowDeleted = false;
        //    }
        //}
    }
}