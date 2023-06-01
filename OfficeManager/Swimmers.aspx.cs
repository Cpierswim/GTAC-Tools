using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_Swimmers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SwimmersBLL Adapter = new SwimmersBLL();
        SwimTeamDatabase.SwimmersDataTable swimmers = null;
        if (DropDownList1.SelectedValue == "Active")
            swimmers = Adapter.GetSwimmerByInactive(SwimmersBLL.ReturnType.Active);
        else if (DropDownList1.SelectedValue == "Inactive")
            swimmers = Adapter.GetSwimmerByInactive(SwimmersBLL.ReturnType.Inactive);
        else if (DropDownList1.SelectedValue == "All")
            swimmers = Adapter.GetSwimmerByInactive(SwimmersBLL.ReturnType.Both);

        GridView1.DataSource = swimmers;
        GridView1.DataBind();

        try
        {
            int ErrorCode = int.Parse(Request.QueryString["Error"]);

            if (ErrorCode == 1)
            {
                ErrorLabel.Text = "Unable to Edit Swimmer. USA Swimming ID Number not found. " +
                    "Please try again.<br /><br />If the problem persists, please contact " +
                    "Chris Pierson at cpierson@sev.org.<br /><br />";
                ErrorLabel.Visible = true;
            }
            else if (ErrorCode == 2)
            {
                ErrorLabel.Text = "Error saving swimmer. Swimmer information was not updated. " +
                    "Please try again.<br /><br />If the problem persists, please contact " +
                    "Chris Pierson at cpierson@sev.org.<br /><br />";
                ErrorLabel.Visible = true;
            }
        }
        catch (Exception)
        {
        }
    }
    protected void SwimmersDataSource_DataBinding(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label GenderLabel = ((Label)e.Row.FindControl("GenderLabel"));

            if (GenderLabel.Text == "M")
                GenderLabel.Text = "Male";
            if (GenderLabel.Text == "F")
                GenderLabel.Text = "Female";

            bool ReadyToAddStatus, IsInDatabaseStatus;

            HiddenField ReadyToAddHiddenField = ((HiddenField)e.Row.FindControl("ReadyToAddHiddenField"));
            ReadyToAddStatus = bool.Parse(ReadyToAddHiddenField.Value);

            HiddenField IsInDatabaseHiddenField = ((HiddenField)e.Row.FindControl("IsInDatabaseHiddenField"));
            IsInDatabaseStatus = bool.Parse(IsInDatabaseHiddenField.Value);

            if (ReadyToAddStatus == false)
                ((Label)e.Row.FindControl("RegistrationProgressLabel")).Text = "Waiting to be approved";
            if((ReadyToAddStatus == true) && (IsInDatabaseStatus == false))
                ((Label)e.Row.FindControl("RegistrationProgressLabel")).Text = "Waiting to be added to databse";
            if((ReadyToAddStatus == true) && (IsInDatabaseStatus == true))
                ((Label)e.Row.FindControl("RegistrationProgressLabel")).Text = "Added to Database";

            Label ActiveStatusLabel = ((Label)e.Row.FindControl("ActiveStatusLabel"));
            if (ActiveStatusLabel.Text == "False")
                ActiveStatusLabel.Text = "Active";
            if (ActiveStatusLabel.Text == "True")
                ActiveStatusLabel.Text = "Inactive";

            Label GroupLabel = ((Label)e.Row.FindControl("GroupLabel"));
            int GroupID = int.Parse(GroupLabel.Text);

            GroupsBLL GroupsAdapter = new GroupsBLL();
            SwimTeamDatabase.GroupsDataTable groups = GroupsAdapter.GetGroupByGroupID(GroupID);
            GroupLabel.Text = groups[0].GroupName;

            String PreferredName = ((HiddenField)e.Row.FindControl("PreferredNameHiddenField")).Value;
            String LastName = ((HiddenField)e.Row.FindControl("LastNameHiddenField")).Value;
            String USAID = ((HiddenField)e.Row.FindControl("USAIDHiddenField1")).Value;

            HyperLink NameHyperLink = ((HyperLink)e.Row.FindControl("NameHyperLink"));
            NameHyperLink.Text = PreferredName + " " + LastName;
            NameHyperLink.NavigateUrl = "EditSwimmer.aspx?USAID=" + USAID;
        }
    }
}