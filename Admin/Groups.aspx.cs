using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Groups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetSelectedMethodFromDropDownList();
    }

    protected void ChangeTrueToYes(object sender, EventArgs e)
    {
        Label SendingLabel = ((Label)sender);
        if (SendingLabel.Text == "True")
            SendingLabel.Text = "Yes";
        else
            SendingLabel.Text = "No";
    }
    protected void RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String test = ((HiddenField)GridView1.Rows[e.RowIndex].FindControl("CreatedHiddenField")).Value;
        e.NewValues["Created"] = test;
        string testtest = e.NewValues["Created"].ToString();
    }
    protected void AddNewGroupButtonClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(NewGroupNameTextBox.Text))
        {
            GroupsBLL GroupsAdapter = new GroupsBLL();
            GroupsAdapter.InsertGroup(NewGroupNameTextBox.Text, 
               TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").ToString("G"),
               IsDefaultGroupCheckBox.Checked);
            GridView1.DataBind();
            NewGroupNameTextBox.Text = "";
            IsDefaultGroupCheckBox.Checked = false;
        }
    }
    protected void GroupSelectorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSelectedMethodFromDropDownList();
        GridView1.DataBind();
    }

    private void SetSelectedMethodFromDropDownList()
    {
        if (GroupSelectorDropDownList.SelectedValue == "Active")
        {
            GroupsDataSource.SelectMethod = "GetActiveGroups";
        }
        else if (GroupSelectorDropDownList.SelectedValue == "Inactive")
        {
            GroupsDataSource.SelectMethod = "GetInActiveGroups";
        }
        else if (GroupSelectorDropDownList.SelectedValue == "All")
        {
            GroupsDataSource.SelectMethod = "GetAllGroups";
        }
    }
}