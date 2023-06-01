using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DatabaseManager_Meet_AddPreEnterPickSwimmer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            if (Request.QueryString["MeetID"] == null)
                Response.Redirect("~/DatabaseManager/Meet/PreEntered.aspx");
            else
            {
                MeetsV2BLL MeetAdapter = new MeetsV2BLL();
                SwimTeamDatabase.MeetsV2Row Meet = MeetAdapter.GetMeetByMeetID(int.Parse(Request.QueryString["MeetID"].ToString()));
                this.Label1.Text = Meet.MeetName;
            }
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView View = e.Row.DataItem as DataRowView;


            Label NameLabel = e.Row.FindControl("NameLabel") as Label;

            NameLabel.Text = View["PreferredName"] + " " + View["LastName"];
        }
    }
    protected void EnterMeetClicked(object sender, GridViewCommandEventArgs e)
    {
        String USAID = ((HiddenField)this.GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("USAIDHiddenField")).Value;
        USAIDEncryptor Encryptor = new USAIDEncryptor(USAID, USAIDEncryptor.EncryptionStatus.Unencrypted);
        Response.Redirect("~/DatabaseManager/Meet/Sessions.aspx?MeetID=" +
            Request.QueryString["MeetID"] + "&ID=" + Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Encrypted));
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        //for (int i = 0; i < this.GridView1.Rows.Count; i++)
        //{
        //    if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
        //    {
        //        SwimTeamDatabase.SwimmersRow Swimmer = GridView1.Rows[i].DataItem as SwimTeamDatabase.SwimmersRow;
        //        Label NameLabel = GridView1.Rows[i].FindControl("NameLabel") as Label;

        //        NameLabel.Text = Swimmer.PreferredName + " " + Swimmer.LastName;
        //    }
        //}
    }
}