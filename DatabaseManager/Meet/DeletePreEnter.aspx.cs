using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_Meet_DeletePreEnter : System.Web.UI.Page
{
    private List<int> SessionsIn;
    private bool GeneralPreEnter;

    protected void Page_Load(object sender, EventArgs e)
    {
        //List<int> SessionsOffered = new List<int>();

        if (Request.QueryString["MeetID"] == null)
            Response.Redirect("~/DatabaseManager/Meet/PreEntered.aspx", true);
        if (Request.QueryString["USAID"] == null)
            Response.Redirect("~/DatabaseManager/Meet/PreEntered.aspx", true);

        if (!Page.IsPostBack)
        {
            String USAID = Request.QueryString["USAID"];

            SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmerByUSAID(USAID);
            this.NameLabel.Text = "<h1>" + Swimmers[0].PreferredName + " " + Swimmers[0].LastName + "</h1>";
            this.NameLabel.Style.Add("text-align", "left");

            this.HyperLink2.NavigateUrl = this.HyperLink2.NavigateUrl + "?MeetID=" + Request.QueryString["MeetID"];
        }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int MeetID = int.Parse(Request.QueryString["MeetID"]);
            String USAID = Request.QueryString["USAID"];
            
            PreEnteredV2BLL PreEnterAdapter = new PreEnteredV2BLL();
            SwimTeamDatabase.PreEnteredV2DataTable PreEntriesForSwimmer = PreEnterAdapter.GetPreEntriesByMeetAndSwimmer(USAID, MeetID);

            if (PreEntriesForSwimmer.Count > 0)
            {

                GeneralPreEnter = PreEntriesForSwimmer[0].PreEntered == true && !PreEntriesForSwimmer[0].IsIndividualSessionsDeclared();

                HiddenField AMHiddenField = ((HiddenField)e.Row.FindControl("AMHiddenField"));
                Label StartTimeLabel = ((Label)e.Row.FindControl("StartTimeLabel"));

                if (bool.Parse(AMHiddenField.Value))
                    StartTimeLabel.Text += " AM";
                else
                    StartTimeLabel.Text += " PM";



                Label SessionNumberLabel = ((Label)e.Row.FindControl("SessionNumberLabel"));
                CheckBox EnteredCheckBox = ((CheckBox)e.Row.FindControl("EnteredCheckBox"));
                EnteredCheckBox.Enabled = false;

                if (GeneralPreEnter)
                    EnteredCheckBox.Checked = true;
                else
                {
                    EnteredCheckBox.Checked = PreEntriesForSwimmer[0].IsPreEnteredInSession(int.Parse(SessionNumberLabel.Text));
                }
                //EnteredCheckBox.Checked = this.SessionsIn.Contains(int.Parse(SessionNumberLabel.Text));
                if (!EnteredCheckBox.Checked)
                    e.Row.Visible = false;
            }
        }
    }
    protected void RemovePreEnterClicked(object sender, GridViewCommandEventArgs e)
    {
        int SessionNumber = int.Parse(((Label)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("SessionNumberLabel")).Text);

        PreEnteredV2BLL PreEntryAdapter = new PreEnteredV2BLL();
        PreEntryAdapter.RemoveFromSession(Request.QueryString["USAID"], int.Parse(Request.QueryString["MeetID"]), SessionNumber,
            PreEnteredV2BLL.RemoveFromSessionOptions.DeleteBlankDeclaredEntry);
        GridView1.DataBind();
    }
    protected void GridViewDataBound(object sender, EventArgs e)
    {
        bool EntryFound = false;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                CheckBox EnteredCheckBox = ((CheckBox)GridView1.Rows[i].FindControl("EnteredCheckBox"));
                if (EnteredCheckBox.Checked)
                {
                    EntryFound = true;
                    break;
                }
            }
        }

        if (!EntryFound)
            for (int i = 0; i < GridView1.Rows.Count; i++)
                if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                    GridView1.Rows[i].Visible = false;
    }
}