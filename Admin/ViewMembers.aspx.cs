using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_ViewMembers : System.Web.UI.Page
{
    private string UsernameToMatch
    {
        get
        {
            object o = ViewState["UsernameToMatch"];
            if (o == null)
                return string.Empty;
            else
                return (string)o;
        }
        set
        {
            ViewState["UsernameToMatch"] = value;
        }
    }

    private int PageIndex
    {
        get
        {
            object o = ViewState["PageIndex"];
            if (o == null)
                return 0;
            else
                return (int)o;
        }
        set
        {
            ViewState["PageIndex"] = value;
        }
    }

    private int PageSize
    {
        get
        {
            return 30;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindUserAccounts();
            BindFilteringUI();
            if (SelectedUsernameHiddenFIeld.Value != "")
                SetPanelInformationFromSelectedUser();
        }
        
    }

    private void BindUserAccounts()
    {
        int totalRecords;
        MembershipUserCollection Members = Membership.FindUsersByName(this.UsernameToMatch + "%", this.PageIndex, this.PageSize, out totalRecords);
        UserAccountsGridView.DataSource = Members;
        int test = 0;
        if (Members.Count < 10)
            test = Members.Count;
        UserAccountsGridView.DataBind();

        // Enable/disable the paging interface
        bool visitingFirstPage = (this.PageIndex == 0);
        lnkFirst.Enabled = !visitingFirstPage;
        lnkPrev.Enabled = !visitingFirstPage;

        int lastPageIndex = (totalRecords - 1) / this.PageSize;
        bool visitingLastPage = (this.PageIndex >= lastPageIndex);
        lnkNext.Enabled = !visitingLastPage;
        lnkLast.Enabled = !visitingLastPage;

    }

    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        this.PageIndex = 0;
        BindUserAccounts();
    }

    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        this.PageIndex -= 1;
        BindUserAccounts();
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        this.PageIndex += 1;
        BindUserAccounts();
    }

    protected void lnkLast_Click(object sender, EventArgs e)
    {
        // Determine the total number of records
        int totalRecords;
        Membership.FindUsersByName(this.UsernameToMatch + "%", this.PageIndex, this.PageSize, out totalRecords);
        // Navigate to the last page index
        this.PageIndex = (totalRecords - 1) / this.PageSize;
        BindUserAccounts();
    }

    private void BindFilteringUI()
    {
        string[] filterOptions = { "All", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        FilteringUI.DataSource = filterOptions;
        FilteringUI.DataBind();
    }

    protected void FilteringUI_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "All")
            this.UsernameToMatch = string.Empty;
        else
            this.UsernameToMatch = e.CommandName;
        this.PageIndex = 0;
        MakeEditorInvisible();
        BindUserAccounts();
    }
    protected void RowSelected(object sender, EventArgs e)
    {
        SelectedUsernameHiddenFIeld.Value = ((HiddenField)UserAccountsGridView.Rows[UserAccountsGridView.SelectedIndex]
            .FindControl("UserNameHiddenField")).Value;
        SetPanelInformationFromSelectedUser();
    }

    private void SetPanelInformationFromSelectedUser()
    {
        UserInfoPanel.Visible = true;
        MembershipUser SelectedUser = Membership.GetUser(SelectedUsernameHiddenFIeld.Value);
        UserNameTextBox.Text = SelectedUser.UserName;
        EmailTextBox.Text = SelectedUser.Email;
        ApprovedCheckBox.Checked = SelectedUser.IsApproved;
        if (SelectedUser.IsApproved)
            ApprovedCheckBox.Enabled = true;
        LockedOutCheckBox.Checked = SelectedUser.IsLockedOut;
        if (SelectedUser.IsLockedOut)
            LockedOutCheckBox.Enabled = true;
        if (SelectedUser.IsOnline)
            OnlineStatusLabel.Text = "Online";
        else
            OnlineStatusLabel.Text = "Offline";
        UserCommentsTextBox.Text = SelectedUser.Comment;
        CreatedDateLabel.Text = SelectedUser.CreationDate.ToString("G");
        LastActivityLabel.Text = SelectedUser.LastActivityDate.ToString("G");

        string[] roles = Roles.GetAllRoles();

        RolesCheckBoxList.Items.Clear();

        foreach (string RoleName in roles)
            RolesCheckBoxList.Items.Add(new ListItem(RoleName, RoleName));

        foreach (ListItem item in RolesCheckBoxList.Items)
            item.Selected = Roles.IsUserInRole(SelectedUser.UserName, item.Value);
    }
    protected void SaveUserButtonClicked(object sender, EventArgs e)
    {
        MembershipUser SelectedUser = Membership.GetUser(SelectedUsernameHiddenFIeld.Value);
        if (LockedOutCheckBox.Enabled && !LockedOutCheckBox.Checked)
            SelectedUser.UnlockUser();
        SelectedUser.Comment = UserCommentsTextBox.Text;
        SelectedUser.Email = EmailTextBox.Text;
        if (ApprovedCheckBox.Enabled && ApprovedCheckBox.Checked)
            SelectedUser.IsApproved = true;
        else
            SelectedUser.IsApproved = false;

        if (SelectedUser.UserName != "ApplicationAdmin")
        {
            foreach (ListItem item in RolesCheckBoxList.Items)
                if (item.Selected == true)
                {
                    if (!Roles.IsUserInRole(SelectedUser.UserName, item.Value))
                        Roles.AddUserToRole(SelectedUser.UserName, item.Value);
                }
                else
                    if (Roles.IsUserInRole(SelectedUser.UserName, item.Value))
                        Roles.RemoveUserFromRole(SelectedUser.UserName, item.Value);
        }

        if (Membership.FindUsersByEmail(SelectedUser.Email).Count == 0)
        {
            Membership.UpdateUser(SelectedUser);
            MakeEditorInvisible();

            BindUserAccounts();
        }
        else
        {
            ErrorLabel.Text = "E-mail address already used by a different user.<br /><br />";
            ErrorLabel.Visible = true;
        }
    }

    private void MakeEditorInvisible()
    {
        UserNameTextBox.Text = "";
        EmailTextBox.Text = "";
        OnlineStatusLabel.Text = "";
        CreatedDateLabel.Text = "";
        LastActivityLabel.Text = "";
        RolesCheckBoxList.Items.Clear();
        UserCommentsTextBox.Text = "";
        UserInfoPanel.Visible = false;
    }
    protected void DeleteUserButtonClicked(object sender, EventArgs e)
    {
        //THIS METHOD HAS BEEN CHANGED AS IS UNTESTED ALL THE WAY THROUGH - INCLUDING ALL
        //CALLS TO METHODS IN THE BLL
        if (!String.IsNullOrEmpty(SelectedUsernameHiddenFIeld.Value) && (SelectedUsernameHiddenFIeld.Value != "ApplicationAdmin")
            && (SelectedUsernameHiddenFIeld.Value != Membership.GetUser().UserName))
        {
            MembershipUser UserToDelete = Membership.GetUser(SelectedUsernameHiddenFIeld.Value);
            if (Roles.IsUserInRole(UserToDelete.UserName, "Parent"))
            {
                FamiliesBLL FamiliesAdapter = new FamiliesBLL();
                Guid DeleteUserId = (Guid)UserToDelete.ProviderUserKey;
                SwimTeamDatabase.FamiliesDataTable families = FamiliesAdapter.GetFamilyID(DeleteUserId);
                int FamilyID;
                if (families.Count != 0)
                {
                    FamilyID = families[0].FamilyID;
                    FamiliesAdapter.DeleteFamily(FamilyID);
                }
                /*
                FamiliesBLL FamiliesAdapter = new FamiliesBLL();
                Guid DeleteUserId = (Guid)UserToDelete.ProviderUserKey;
                SwimTeamDatabase.FamiliesDataTable families = FamiliesAdapter.GetFamilyID(DeleteUserId);
                int FamilyID;
                if (families.Count != 0)
                {
                    FamilyID = families[0].FamilyID;

                    SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                    SwimTeamDatabase.SwimmersDataTable swimmers = SwimmersAdapter.GetSwimmersByFamilyID(FamilyID);
                    if (swimmers.Count != 0)
                        foreach (SwimTeamDatabase.SwimmersRow swimmer in swimmers)
                            swimmer.Delete();

                    ParentsBLL ParentsAdapter = new ParentsBLL();
                    SwimTeamDatabase.ParentsDataTable parents = ParentsAdapter.GetParentsByFamilyID(FamilyID);
                    if (parents.Count != 0)
                        foreach (SwimTeamDatabase.ParentsRow parent in parents)
                            parent.Delete();

                    families[0].Delete();
                }*/
            }
            Membership.DeleteUser(UserToDelete.UserName, true);
            
            MakeEditorInvisible();
            BindUserAccounts();
        }
    }
}