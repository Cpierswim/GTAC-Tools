using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_ApproveSwimmers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RowDatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FirstName = ((Label)e.Row.FindControl("FirstNameLabel")).Text;
            string LastName = ((Label)e.Row.FindControl("LastNameLabel")).Text;
            string USAID = ((Label)e.Row.FindControl("USAIDLabel")).Text;
            string FamilyID = ((Label)e.Row.FindControl("FamilyIDLabel")).Text;

            HyperLink FullNameLink = ((HyperLink)e.Row.FindControl("FullNameHyperlink"));

            FullNameLink.Text = LastName + ", " + FirstName;
            FullNameLink.NavigateUrl = "~/OfficeManager/ApproveSwimmers.aspx?USAID=" + USAID + "&FamilyID=" + FamilyID;

            Label GroupLabel = ((Label)e.Row.FindControl("GroupLabel"));
            GroupsBLL GroupsAdapter = new GroupsBLL();
            SwimTeamDatabase.GroupsDataTable groups = GroupsAdapter.GetGroupByGroupID(int.Parse(GroupLabel.Text));
            if (groups.Count != 1)
                GroupLabel.Text = "Error";
            else
                GroupLabel.Text = groups[0].GroupName;
        }
    }

    protected void FirstNameBinding(object sender, EventArgs e)
    {
    }
    protected void ReadyToAddButton_Clicked(object sender, GridViewCommandEventArgs e)
    {
        string USAID = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();

        SwimmersBLL SwimmerAdapter = new SwimmersBLL();
        SwimmerAdapter.SetAsReadyToAdd(USAID);

        GridView1.DataBind();
    }
    protected void FormView_Databound(object sender, EventArgs e)
    {
        Label GenderLabel = ((Label)FormView1.FindControl("GenderLabel"));
        Label GroupLabel = ((Label)FormView1.FindControl("GroupLabel"));
        if (GenderLabel != null)
        {
            String Gender = GenderLabel.Text;

            if (Gender == "M")
                GenderLabel.Text = "Male";
            else if (Gender == "F")
                GenderLabel.Text = "Female";
            else
                GenderLabel.Text = "Error";


            GroupsBLL GroupsAdapter = new GroupsBLL();
            SwimTeamDatabase.GroupsDataTable groups = GroupsAdapter.GetGroupByGroupID(int.Parse(GroupLabel.Text));
            if (groups.Count != 1)
                GroupLabel.Text = "Error";
            else
                GroupLabel.Text = groups[0].GroupName;

        }
    }
    protected void ParentDataBound(object sender, EventArgs e)
    {
        FormView SendingForm = ((FormView)sender);
        int ControlCount = SendingForm.Controls[0].Controls.Count;

        if (ControlCount != 0)
        {
            //There are no controls, so there is no parent, so none of these controls exist.
            Label NotesLabel = ((Label)SendingForm.FindControl("NotesLabel"));
            if (!string.IsNullOrEmpty(NotesLabel.Text))
                NotesLabel.Text = "Notes: " + NotesLabel.Text;

            HiddenField AddressLineOneHiddenField = ((HiddenField)SendingForm.FindControl("AddressLineOneHiddenField"));
            HiddenField AddressLineTwoHiddenField = ((HiddenField)SendingForm.FindControl("AddressLineTwoHiddenField"));
            HiddenField CityHiddenField = ((HiddenField)SendingForm.FindControl("CityHiddenField"));
            HiddenField StateHiddenField = ((HiddenField)SendingForm.FindControl("StateHiddenField"));
            HiddenField ZipHiddenField = ((HiddenField)SendingForm.FindControl("ZipHiddenField"));
            Label AddressLabel = ((Label)SendingForm.FindControl("AddressLabel"));
            String Address = string.Empty;

            Address += AddressLineOneHiddenField.Value + "<br />";
            if (!string.IsNullOrEmpty(AddressLineTwoHiddenField.Value))
                Address += AddressLineTwoHiddenField.Value + "<br />";
            Address += CityHiddenField.Value + ", " + StateHiddenField.Value + " " +
                ZipHiddenField.Value;

            AddressLabel.Text = Address;

            Label EmailLabel = ((Label)SendingForm.FindControl("EmailLabel"));
            if (EmailLabel.Text.Contains("@"))
                EmailLabel.Text = "<a href=\"mailto:" + EmailLabel.Text + "\">" + EmailLabel.Text + "</a>";
        }
    }
}