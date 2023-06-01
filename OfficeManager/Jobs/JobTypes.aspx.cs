using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_Jobs_JobTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void SwitchPanels(object sender, EventArgs e)
    {
        FormView1.Visible = true;
        Panel2.Visible = true;
        AddJobButton.Visible = false;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
            this.GridView1.Rows[i].RowState = DataControlRowState.Normal;
    }
    protected void FormViewButtonClicked(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            FormView1.Visible = false;
            Panel2.Visible = false;
            AddJobButton.Visible = true;
        }
    }
    protected void FormViewInserting(object sender, FormViewInsertEventArgs e)
    {
        String ErrorDescription = "";
        if (e.Values["Description"].ToString().Length > 500)
        {
            e.Cancel = true;
            ErrorDescription += "<br />Description max length 500 characters.";
        }
        if (String.IsNullOrWhiteSpace(e.Values["Description"].ToString()))
        {
            e.Cancel = true;
            ErrorDescription += "<br />Description is required.";
        }
        if (e.Values["Name"].ToString().Length > 250)
        {
            e.Cancel = true;
            ErrorDescription += "<br />Name max length 250 characters.";
        }
        if (String.IsNullOrWhiteSpace(e.Values["Name"].ToString()))
        {
            e.Cancel = true;
            ErrorDescription += "<br />Name is required.";
        }
        if (e.Values["TimeToLearn"].ToString().Length > 50)
        {
            e.Cancel = true;
            ErrorDescription += "<br />Time To Learn max length 50 characters.";
        }
        if (e.Values["Length"].ToString().Length > 50)
        {
            e.Cancel = true;
            ErrorDescription += "<br />Length max length 50 characters.";
        }

        if (!String.IsNullOrWhiteSpace(ErrorDescription))
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "<br />" + ErrorDescription;
        }
    }
    protected void FormViewInserted(object sender, FormViewInsertedEventArgs e)
    {
        ErrorLabel.Visible = false;
        FormView1.Visible = false;
        Panel2.Visible = false;
        AddJobButton.Visible = true;
    }
}