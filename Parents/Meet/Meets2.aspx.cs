using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Meet_Meets2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ErrorCode"] != null)
            if (Request.QueryString["ErrorCode"] == "1")
            {
                this.ErrorLabel.Text = "No Meet Picked. Please select a meet from below.<br /><br />";
                this.ErrorLabel.Visible = true;
            }
        if (Request.QueryString["ID"] == null)
            Response.Redirect("~/Parents/FamilyView.aspx?Error=9");
    }
    protected void RowDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label StartLabel = ((Label)e.Item.FindControl("StartLabel"));
        StartLabel.Text = DateTime.Parse(StartLabel.Text).ToString("dddd MMMM d, yyyy");

        Label EndDateLabel = ((Label)e.Item.FindControl("EndDateLabel"));
        EndDateLabel.Text = DateTime.Parse(EndDateLabel.Text).ToString("dddd MMMM d, yyyy");

        Label DeadlineLabel = ((Label)e.Item.FindControl("DeadlineLabel"));
        DeadlineLabel.Text = DateTime.Parse(DeadlineLabel.Text).ToString("MMMM d");
        if (DeadlineLabel.Text.EndsWith("1") || DeadlineLabel.Text.EndsWith("21") || DeadlineLabel.Text.EndsWith("31"))
            DeadlineLabel.Text += "<sup>st</sup>";
        else if (DeadlineLabel.Text.EndsWith("2") || DeadlineLabel.Text.EndsWith("22"))
            DeadlineLabel.Text += "<sup>nd</sup>";
        else if (DeadlineLabel.Text.EndsWith("3") || DeadlineLabel.Text.EndsWith("23"))
            DeadlineLabel.Text += "<sup>rd</sup>";
        else
            DeadlineLabel.Text += "<sup>th</sup>";

        Label MeetNameLabel = ((Label)e.Item.FindControl("MeetNameLabel"));

        Button EnterMeetButton = ((Button)e.Item.FindControl("EnterMeetButton"));

        EnterMeetButton.Text = EnterMeetButton.Text + MeetNameLabel.Text;

        Panel DisplayPanel = ((Panel)e.Item.FindControl("DisplayPanel"));
        DisplayPanel.Style.Add("border-bottom-style", "dashed");
        DisplayPanel.Style.Add("border-bottom-width", "1px");

        Label CourseLabel = ((Label)e.Item.FindControl("CourseLabel"));
        if (CourseLabel.Text.ToUpper().StartsWith("Y"))
            CourseLabel.Text = "Yards";
        else if (CourseLabel.Text.ToUpper().StartsWith("L"))
            CourseLabel.Text = "Meters";
        else if (CourseLabel.Text.ToUpper().StartsWith("S"))
            CourseLabel.Text = "Short Course Meters";
    }
    protected void RepeaterDataBound(object sender, EventArgs e)
    {
        Repeater r = ((Repeater)sender);
        if (r.Items.Count > 0)
        {
            RepeaterItem Item = r.Items[r.Items.Count - 1];

            Panel DisplayPanel = ((Panel)Item.FindControl("DisplayPanel"));

            DisplayPanel.Style.Remove("border-bottom-style");
            DisplayPanel.Style.Remove("border-bottom-width");
        }
        if (r.ID == "Repeater2")
        {
            if (r.Items.Count < 1)
            {
                r.Visible = false;
                this.Label2.Visible = false;
                this.LateEntriesOpenLabel.Visible = false;
            }
            else
            {
                r.Visible = true;
                this.Label2.Visible = true;
                this.Label2.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                this.LateEntriesOpenLabel.Visible = true;
                this.LateEntriesOpenLabel.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            }
        }
    }
    protected void ButtonClicked(object source, RepeaterCommandEventArgs e)
    {
        String MeetID = ((HiddenField)e.Item.FindControl("MeetIDHiddenField")).Value;

        String EncryptedUSAID = Request.QueryString["ID"];
        //USAIDEncryptor Encryptor = new USAIDEncryptor(USAID, USAIDEncryptor.EncryptionStatus.Unencrypted);
        //String EncryptedUSAID = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Encrypted);
        //if (Request.QueryString["ID"] != null)
        //    USAID = Request.QueryString["ID"];

        Response.Redirect("~/Parents/Meet/Sessions.aspx?MeetID=" + MeetID + "&ID=" + EncryptedUSAID);
    }
}