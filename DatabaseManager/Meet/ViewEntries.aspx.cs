using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class DatabaseManager_Meet_ViewEntries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
            Sessions = SessionsAdapter.GetSessionsByMeetID(int.Parse(DropDownList1.SelectedValue));
            MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
            SwimTeamDatabase.MeetsV2Row Meet = MeetsAdapter.GetMeetByMeetID(int.Parse(DropDownList1.SelectedValue));
            StartDate = Meet.Start;

            DisplaySessions = bool.Parse(DropDownList2.SelectedValue);
        }
    }

    private bool DisplaySessions;
    private String LastSubHeaderSwimmer = "";
    private String LastSession = "";
    private SwimTeamDatabase.BestTimesDataTable BestTimes = null;
    private SwimTeamDatabase.SessionV2DataTable Sessions;
    private DateTime StartDate;
    private int Swimmers = 0;
    private int Entries = 0;

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (BestTimes == null)
        {
            BestTimesBLL BestTimesAdapter = new BestTimesBLL();
            BestTimes = BestTimesAdapter.GetAllBestTimes();
        }
        GridView SendingGrid = sender as GridView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView TempRow = e.Row.DataItem as DataRowView;

            if (LastSubHeaderSwimmer != TempRow["USAID"].ToString())
            {
                LastSubHeaderSwimmer = TempRow["USAID"].ToString();
                Swimmers++;

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

                    span.InnerText = TempRow["LastName"] + ", " + TempRow["FirstName"];

                    cell.Controls.Add(span);
                    SubHeader.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, SubHeader);
                }
                LastSession = "";
            }
            if (DisplaySessions && Sessions != null)
            {
                int SessionNumber = int.Parse(TempRow["SessionID"].ToString());
                if (SessionNumber.ToString() != LastSession)
                {
                    LastSession = SessionNumber.ToString();

                    SwimTeamDatabase.SessionV2Row EventSession = null;
                    for (int i = 0; i < Sessions.Count; i++)
                        if (Sessions[i].Session == SessionNumber)
                        {
                            EventSession = Sessions[i];
                            break;
                        }

                    Table tbl = e.Row.Parent as Table;
                    if (tbl != null)
                    {
                        GridViewRow SubHeader = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                        TableCell cell = new TableCell();

                        cell.ColumnSpan = SendingGrid.Columns.Count;
                        cell.Width = new Unit("100%");
                        cell.Style.Add("background-color", "#F0ED80");
                        HtmlGenericControl span = new HtmlGenericControl("span");

                        DateTime SessionDate = StartDate.AddDays(EventSession.Day - 1);

                        span.InnerText = "Session " + EventSession.Session + ": " + SessionDate.ToString("dddd MMMM, d yyyy") + " " +
                            EventSession.StartTime;
                        if (EventSession.AM)
                            span.InnerText += " AM";
                        else
                            span.InnerText += " PM";

                        cell.Controls.Add(span);
                        SubHeader.Cells.Add(cell);

                        tbl.Rows.AddAt(tbl.Rows.Count - 1, SubHeader);
                    }
                }
            }
            Label EventLabel = e.Row.FindControl("EventLabel") as Label;
            int Distance = int.Parse(TempRow["Distance"].ToString());
            int StrokeCode = int.Parse(TempRow["StrokeCode"].ToString());
            String SexCode = TempRow["SexCode"].ToString();
            String AgeCode = TempRow["AgeCode"].ToString();
            EventLabel.Text = MeetEventHelper.GetEventName(Distance, StrokeCode, SexCode, AgeCode,
                                                            MeetEventHelper.StrokeStringLength.Middle);

            Label TimeLabel = e.Row.FindControl("TimeLabel") as Label;

            int AutoTime = -10, CustomTime = -10;
            if (TempRow["AutoTime"] != null)
                if (!String.IsNullOrWhiteSpace(TempRow["AutoTime"].ToString()))
                    AutoTime = int.Parse(TempRow["AutoTime"].ToString());
            if (TempRow["CustomTime"] != null)
                if (!String.IsNullOrWhiteSpace(TempRow["CustomTime"].ToString()))
                    CustomTime = int.Parse(TempRow["CustomTime"].ToString());
            bool Bonus = bool.Parse(TempRow["Bonus"].ToString());
            bool Exhibition = bool.Parse(TempRow["Exhibition"].ToString());
            bool EnterEvent = bool.Parse(TempRow["EnterEvent"].ToString());
            String Course = TempRow["Course"].ToString();
            if (EnterEvent)
            {
                Entries++;
                HyTekTime Entrytime = HyTekTime.NOTIME;
                if (AutoTime != -10)
                {
                    if (AutoTime == 0)
                    {
                        for (int i = 0; i < BestTimes.Count; i++)
                        {
                            String USAID = TempRow["USAID"].ToString();

                            if (BestTimes[i].Course == Course &&
                                BestTimes[i].Distance == Distance &&
                                BestTimes[i].Stroke == StrokeCode &&
                                BestTimes[i].USAID == USAID)
                            {
                                Entrytime = BestTimes[i].Time;
                                break;
                            }
                        }
                    }
                    else
                        CustomTime = AutoTime;
                }
                else if (CustomTime != -10)
                    Entrytime = new HyTekTime(CustomTime, Course);

                TimeLabel.Text = Entrytime.ToString();
                if (Bonus)
                    TimeLabel.Text += " <i>Bonus</i>";
                if (Exhibition)
                    if (TimeLabel.Text.EndsWith(">"))
                        TimeLabel.Text += " <i>, Exhib</i>";
                    else
                        TimeLabel.Text += " <i>Exhib</i>";
            }
        }
    }
    protected void MeetsDropDownListDatabound(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SessionsV2BLL SessionsAdapter = new SessionsV2BLL();
            Sessions = SessionsAdapter.GetSessionsByMeetID(int.Parse(DropDownList1.SelectedValue));
            MeetsV2BLL MeetsAdapter = new MeetsV2BLL();
            SwimTeamDatabase.MeetsV2Row Meet = MeetsAdapter.GetMeetByMeetID(int.Parse(DropDownList1.SelectedValue));
            StartDate = Meet.Start;

            DisplaySessions = bool.Parse(DropDownList2.SelectedValue);
        }
    }
    protected void DisplaySessionsChanged(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        if (Swimmers > 0)
        {
            SwimmerCountLabel.Text = "Swimmers: " + Swimmers.ToString();
            EntryCountLabel.Text = "Entries: " + Entries.ToString();
        }
        else
        {
            SwimmerCountLabel.Text = "";
            EntryCountLabel.Text = "";
        }
    }
}