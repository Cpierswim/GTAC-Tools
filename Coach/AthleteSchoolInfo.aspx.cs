using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Coach_AthleteSchoolInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label GradeLabel = ((Label)e.Row.FindControl("GradeLabel"));

            int Grade = int.Parse(GradeLabel.Text);

            if (Grade == 0)
                GradeLabel.Text = "Before Kindergarten";
            else if (Grade == 1)
                GradeLabel.Text = "Kindergarten";
            else if (Grade == 10)
                GradeLabel.Text = "Freshman";
            else if (Grade == 11)
                GradeLabel.Text = "Sophomore";
            else if (Grade == 12)
                GradeLabel.Text = "Junior";
            else if (Grade == 13)
                GradeLabel.Text = "Senior";
            else if (Grade == 14)
                GradeLabel.Text = "College Freshman";
            else if (Grade == 15)
                GradeLabel.Text = "College Sophomore";
            else if (Grade == 16)
                GradeLabel.Text = "College Junior";
            else if (Grade == 17)
                GradeLabel.Text = "College Senior";
            else if (Grade == 18)
                GradeLabel.Text = "College Post-Senior";
            else if (Grade == 19)
                GradeLabel.Text = "Post Grad";
            else if (Grade == 20)
                GradeLabel.Text = "None";
            else
            {
                Grade--;
                if (Grade == 1)
                    GradeLabel.Text = "1st Grade";
                else if (Grade == 2)
                    GradeLabel.Text = "2nd Grade";
                else if (Grade == 3)
                    GradeLabel.Text = "3rd Grade";
                else
                    GradeLabel.Text = Grade + "th Grade";
            }
        }
    }
    protected void SchoolsDropDownListDataBound(object sender, EventArgs e)
    {
        DropDownList SchoolsDropDownList = ((DropDownList)sender);

        ListItemCollection PreviousItems = new ListItemCollection();
        for (int i = 0; i < SchoolsDropDownList.Items.Count; i++)
            PreviousItems.Add(SchoolsDropDownList.Items[i]);
        SchoolsDropDownList.Items.Clear();
        SchoolsDropDownList.Items.Add(new ListItem("All Schools", "All Schools", true));
        for (int i = 0; i < PreviousItems.Count; i++)
            SchoolsDropDownList.Items.Add(PreviousItems[i]);
    }
    protected void GroupsDropDownListDataBound(object sender, EventArgs e)
    {
        DropDownList GroupsDropDownList = ((DropDownList)sender);
        ListItemCollection PreviousItems = new ListItemCollection();
        for (int i = 0; i < GroupsDropDownList.Items.Count; i++)
            PreviousItems.Add(GroupsDropDownList.Items[i]);
        GroupsDropDownList.Items.Clear();
        GroupsDropDownList.Items.Add(new ListItem("All Groups", "All Groups", true));
        for (int i = 0; i < PreviousItems.Count; i++)
            GroupsDropDownList.Items.Add(PreviousItems[i]);
    }
    protected void GridDataBound(object sender, EventArgs e)
    {
        GridView View = ((GridView)sender);

        int rows = 0;

        for (int i = 0; i < View.Rows.Count; i++)
            if (View.Rows[i].RowType == DataControlRowType.DataRow)
                rows++;

        this.CountLabel.Text = rows.ToString();
    }
}