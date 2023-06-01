using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_Meet_MeetEditor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.LoadComplete += this.Page_LoadComplete;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            if (Request.QueryString["SI"] != null)
                this.MeetsGridView.SelectedIndex = int.Parse(Request.QueryString["SI"]);
    }


    protected void RowUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        Response.Redirect("MeetEditor.aspx", true);
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label OpenForCoachesLabel = ((Label)e.Row.FindControl("OpenForCoachesLabel"));
            if (bool.Parse(OpenForCoachesLabel.Text))
                OpenForCoachesLabel.Text = "Yes";
            else
                OpenForCoachesLabel.Text = "No";
        }
    }
    protected void EditSessionsClicked(object sender, GridViewCommandEventArgs e)
    {
        String QueryString = "";
        int MeetID = int.Parse(((HiddenField)this.MeetsGridView.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("MeetIDHiddenField")).Value);
        if (e.CommandName == "Meet")
            QueryString = "?EM=" + MeetID;
        if (e.CommandName == "Sessions")
            QueryString = "?ES=" + MeetID;


        Response.Redirect("MeetEditor.aspx" + QueryString, true);
    }
    protected void SessionsRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal ||
                e.Row.RowState == DataControlRowState.Alternate)
            {
                Label AMLabel = ((Label)e.Row.FindControl("AMLabel"));
                if (bool.Parse(AMLabel.Text))
                    AMLabel.Text = "AM";
                else
                    AMLabel.Text = "PM";

                Label GuessTimeLabel = ((Label)e.Row.FindControl("GuessTimeLabel"));
                if (bool.Parse(GuessTimeLabel.Text))
                    GuessTimeLabel.Text = "Yes";
                else
                    GuessTimeLabel.Text = "No";

                Label PrelimFinalLabel = ((Label)e.Row.FindControl("PrelimFinalLabel"));
                if (bool.Parse(PrelimFinalLabel.Text))
                    PrelimFinalLabel.Text = "Yes";
                else
                    PrelimFinalLabel.Text = "No";
            }
        }
    }
    protected void MeetDetailsViewDataBound(object sender, EventArgs e)
    {
        Label CourseLabel = ((Label)this.DetailsView1.FindControl("CourseLabel"));
        if (CourseLabel != null)
        {

            CourseLabel.Text = CourseLabel.Text.ToUpper();

            switch (CourseLabel.Text)
            {
                case "Y":
                    CourseLabel.Text = "Yards or yard coversions";
                    break;
                case "YO":
                    CourseLabel.Text = "Yards times only";
                    break;
                case "L":
                    CourseLabel.Text = "Meters or meter conversions";
                    break;
                case "LO":
                    CourseLabel.Text = "Meters times only";
                    break;
                case "S":
                    CourseLabel.Text = "Short course meters or short course meter conversions";
                    break;
                case "SO":
                    CourseLabel.Text = "Short course meters times only";
                    break;
                case "LS":
                case "LY":
                    CourseLabel.Text = "Long course meter times preferred, all times accepted";
                    break;
                case "YL":
                case "YS":
                    CourseLabel.Text = "Yards times preferred, all times accepted";
                    break;
                case "SL":
                case "SY":
                    CourseLabel.Text = "Short course meter times preferred, all times accepted";
                    break;
            }

            Label MaxIndEntLabel = ((Label)this.DetailsView1.FindControl("MaxIndEntLabel"));
            if (int.Parse(MaxIndEntLabel.Text) < 1)
                MaxIndEntLabel.Text = "No Limit";
            Label MaxRelEntLabel = ((Label)this.DetailsView1.FindControl("MaxRelEntLabel"));
            if (int.Parse(MaxRelEntLabel.Text) < 1)
                MaxRelEntLabel.Text = "No Limit";
            Label MaxEntLabel = ((Label)this.DetailsView1.FindControl("MaxEntLabel"));
            if (int.Parse(MaxEntLabel.Text) < 1)
                MaxEntLabel.Text = "No Limit";
            Label OpenForCoachesLabel = ((Label)this.DetailsView1.FindControl("OpenForCoachesLabel"));
            if (OpenForCoachesLabel != null)
            {
                if (bool.Parse(OpenForCoachesLabel.Text))
                    OpenForCoachesLabel.Text = "Yes";
                else
                    OpenForCoachesLabel.Text = "No";
            }
        }
    }
    protected void MeetUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters.Remove("Course");
        e.InputParameters.Remove("MaxIndEnt");
        e.InputParameters.Remove("MaxRelEnt");
        e.InputParameters.Remove("MaxEnt");
    }
}