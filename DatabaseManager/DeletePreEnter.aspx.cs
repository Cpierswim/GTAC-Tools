using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_DeletePreEnter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Object o = Request.QueryString["ID"];
            if (o != null)
            {
                USAIDEncryptor Encryptor = new USAIDEncryptor(o.ToString(), USAIDEncryptor.EncryptionStatus.Encrypted);
                HiddenField1.Value = Encryptor.GetUSAID(USAIDEncryptor.EncryptionStatus.Unencrypted);
            }

            o = Request.QueryString["MID"];
            if (o != null)
                HyperLink2.NavigateUrl += "?SEL=" + o.ToString();
        }
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        Object o = Request.QueryString["MID"];
        if (o != null)
        {
            int MeetID = int.Parse(o.ToString());

            SessionsBLL SessionAdapter = new SessionsBLL();
            SwimTeamDatabase.SessionsDataTable Sessions = SessionAdapter.GetSessionsByMeetID(MeetID);

            for (int i = 0; i < GridView1.Rows.Count; i++)
                if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    Label SessionIDLabel = ((Label)GridView1.Rows[i].FindControl("SessionIDLabel"));
                    int SessionID = int.Parse(SessionIDLabel.Text);

                    SwimTeamDatabase.SessionsRow Session = null;
                    for (int j = 0; j < Sessions.Count; j++)
                        if (SessionID == Sessions[j].SessionID)
                            Session = Sessions[j];

                    String NewLabelText = Session.WarmUpTime.ToLongDateString() + " " + Session.WarmUpTime.ToShortTimeString();

                    SessionIDLabel.Text = NewLabelText;
                }
        }
    }
    protected void AddMeetCreditClicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(HiddenField1.Value))
            if (new CreditsBLL().AddCreditForSwimmersFamily(HiddenField1.Value))
                Button1.Enabled = false;
    }
    protected void RowDeletingTest(object sender, GridViewDeleteEventArgs e)
    {
        new EntryBLL().DeleteEntry(int.Parse(e.Values[0].ToString()));
        ((GridView)sender).DataBind();
    }
}