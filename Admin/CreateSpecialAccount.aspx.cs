using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Admin_CreateSpecialAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void AddUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            MembershipCreateStatus status;
            MembershipUser NewUser = Membership.CreateUser(UsernameTextBox.Text,
                PasswordTextBox.Text, EmailTestBox.Text, null, null, true, out status);

            if (NewUser == null)
            {
                CreatedLabel.Visible = true;
                CreatedLabel.Text = GetErrorMessage(status);
            }
            else
            {
                CreatedLabel.Visible = true;
                CreatedLabel.Text = NewUser.UserName + " sucessfully created.";

                Roles.AddUserToRole(UsernameTextBox.Text, RoleDropDownList.SelectedValue);

                CreatedLabel.Text = CreatedLabel.Text + "<br /><br />" + UsernameTextBox.Text + "sucessfully added to Role: " + RoleDropDownList.SelectedValue;
            }
            
        }
        catch (Exception ex)
        {
            CreatedLabel.Visible = true;
            CreatedLabel.Text = CreatedLabel.Text + "<br /><br />ERROR<br /><br />" + ex.Message;
        }
    }

    private string GetErrorMessage(MembershipCreateStatus status)
    {
        switch (status)
        {
            case MembershipCreateStatus.DuplicateUserName:
                return "Unable to Create User. Username already Exists.";

            case MembershipCreateStatus.DuplicateEmail:
                return "A username for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }

}