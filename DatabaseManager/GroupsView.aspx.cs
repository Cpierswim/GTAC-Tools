using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_GroupsView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RowEditingClicked(object sender, GridViewEditEventArgs e)
    {
        SetGroupsColumnVisibility(true);
    }
    protected void RowCanceledEdit(object sender, GridViewCancelEditEventArgs e)
    {
        SetGroupsColumnVisibility(false);
    }
    protected void SwimmerUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        e.InputParameters.Remove("Birthday");
        e.InputParameters.Remove("PreferredName");
        e.InputParameters.Remove("Gender");
    }
    protected void RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        SetGroupsColumnVisibility(false);

        //SettingsBLL SettingsAdapter = new SettingsBLL();
        //SettingsAdapter.SetGroupUpdateAsPending();

        GridView1.DataBind();
    }
    protected void RowDatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label AgeLabel = ((Label)e.Row.FindControl("AgeLabel"));
            DateTime Birthday = DateTime.Parse(((Label)e.Row.FindControl("BirthdayLabel")).Text);

            DateTime Current = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            
            int age = Current.Year - Birthday.Year;

            if (Current.Month < Birthday.Month)
                age--; //the birthday hasn't happened yet, so they are still the younger age
            else if ((Current.Month == Birthday.Month) && (Current.Day < Birthday.Day))
                age--; //the birthday is this month, but the birthday still hasn't happened yet.

            AgeLabel.Text = age.ToString();

            Label GenderLabel = ((Label)e.Row.FindControl("GenderLabel"));
            if (GenderLabel.Text == "M")
                GenderLabel.Text = "Male";
            else if (GenderLabel.Text == "F")
                GenderLabel.Text = "Female";
        }

    }
    protected void GroupSelectionDropDownList_Changed(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        SetGroupsColumnVisibility(false);
    }

    private void SetGroupsColumnVisibility(bool Visible)
    {
        for (int i = 0; i < GridView1.Columns.Count; i++)
            if (GridView1.Columns[i].HeaderText == "Group")
                GridView1.Columns[i].Visible = Visible;
    }
}