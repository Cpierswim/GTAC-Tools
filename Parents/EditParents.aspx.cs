using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_EditParents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            ParentsBLL ParentsAdapter = new ParentsBLL();
            if (ParentsAdapter.GetParentsByFamilyID(int.Parse(Session.Contents["FamilyID"].ToString())).Count < 2)
            {
                ParentSeclectorDropDownList.Items.RemoveAt(1);
                SwitchParentsButton.Visible = false;
            }
        }
    }

    protected void ParentUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters["PrimaryContact"] =
            ((HiddenField)DetailsView1.FindControl("PrimaryContactHiddenField")).Value;
    }

    protected void SelectedParent_Changed(object sender, EventArgs e)
    {
        if (ParentSeclectorDropDownList.SelectedValue == "Primary")
            ParentsDataSource.SelectMethod = "GetPrimaryContactParentByFamilyID";
        else
            ParentsDataSource.SelectMethod = "GetSecondaryContactParentByFamilyID";
        DetailsView1.DataBind();
    }
    protected void SwitchParentsClicked(object sender, EventArgs e)
    {
        ParentsBLL ParentsAdapter = new ParentsBLL();
        bool SucessfullyCommitted = ParentsAdapter.SwitchPrimaryAndSecondaryParent(
            int.Parse(((HiddenField)DetailsView1.FindControl("FamilyIDHiddenField")).Value));
        if (!SucessfullyCommitted)
        {
            SwitchingErrorLabel.Text = "Error Switching Primary and Secondary Parent. Are you sure there " +
                "are 2 parents?<br /><br />";
            SwitchingErrorLabel.Visible = true;
        }

        
        ParentSeclectorDropDownList.SelectedIndex = 0;
        ParentsDataSource.SelectMethod = "GetPrimaryContactParentByFamilyID";
        DetailsView1.DataBind();
    }
    protected void DetailsView_DataBound(object sender, EventArgs e)
    {
        Label ContactStatusLabel = ((Label)DetailsView1.FindControl("ContactStatusLabel"));
        if (ContactStatusLabel.Text == "True")
        {
            ContactStatusLabel.Text = "Primary Contact";
            this.SwitchParentsButton.Visible = false;
        }
        else
        {
            ContactStatusLabel.Text = "Secondary Contact";
            this.SwitchParentsButton.Visible = true;
        }

        LinkButton CancelButton = ((LinkButton)this.DetailsView1.FindControl("CancelButton"));
        CancelButton.PostBackUrl = "FamilyView.aspx";
    }
    protected void ParentUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        Response.Redirect("FamilyView.aspx", true);
    }
}