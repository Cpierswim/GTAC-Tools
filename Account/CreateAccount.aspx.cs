using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

public partial class Account_CreateAccount : System.Web.UI.Page
{
    private string _account_has_been_created = null;

    protected bool AccountCreated
    {
        get
        {
            if (this._account_has_been_created == null || this._account_has_been_created == "FALSE")
            {
                this._account_has_been_created = "FALSE";
                return false;
            }
            else
                return true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Roles.RoleExists("Parent"))
            Roles.CreateRole("Parent");
        if (!Page.IsPostBack)
        {
            SettingsBLL SettingsAdapter = new SettingsBLL();
            SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetApplicationStatusSetting();
            SwimTeamDatabase.SettingsRow ApplicationStatus = settings[0];

            if (ApplicationStatus.SettingValue != "Online")
            {
                ApplicationOfflineLablel.Visible = true;
                CreateUserWizard1.Visible = false;
            }
        }
    }
    protected void WizardCreatingUser(object sender, LoginCancelEventArgs e)
    {
    }
    protected void WizardCreatedUser(object sender, EventArgs e)
    {
        // Get the UserId of the just-added user
        MembershipUser newUser = Membership.GetUser(CreateUserWizard1.UserName);
        Guid newUserId = (Guid)newUser.ProviderUserKey;

        newUser.IsApproved = false;

        Roles.AddUserToRole(newUser.UserName, "Parent");

        FamiliesBLL familyadapter = new FamiliesBLL();

        bool result = familyadapter.CreateFamily(newUserId);
        if (result)
        {
            this._account_has_been_created = "TRUE";
        }
        else
        {
            this._account_has_been_created = "FALSE";
            throw new Exception("OMG - FAMILY NOT CREATED!");
        }
    }
    protected void WizardStepChanged(object sender, EventArgs e)
    {
        if (CreateUserWizard1.ActiveStep.Title == "Primary Contact Parent")
        {
            PrimaryEmail.Text = CreateUserWizard1.Email;
            //PrimaryFirstName.Focus();
        }
        else if (CreateUserWizard1.ActiveStep.Title == "Add Parents")
        {
            int NumberOfParentsAdded = this.AddParents();
            if (NumberOfParentsAdded == 1)
                NumberOfParentsAddedTextBox.Text = NumberOfParentsAdded + " Parent has";
            else
                NumberOfParentsAddedTextBox.Text = NumberOfParentsAdded + " Parents have";
        }
        else if (CreateUserWizard1.ActiveStep.Title == "Secondary Contact Parent") 
        {
            if (RadioButtonList1.SelectedValue == "No")
                CreateUserWizard1.ActiveStepIndex = CreateUserWizard1.ActiveStepIndex + 1;
            else
            {

                SecondaryAddressLineOne.Text = PrimaryAddressLineOne.Text;
                SecondaryAddressLineTwo.Text = PrimaryAddressLineTwo.Text;
                SecondaryLastName.Text = PrimaryLastName.Text;
                SecondaryCity.Text = PrimaryCity.Text;
                SecondaryState.SelectedIndex = PrimaryState.SelectedIndex;
                SecondaryZip.Text = PrimaryZip.Text;
                SecondaryHomePhone.Text = PrimaryHomePhone.Text;
                SecondaryFirstName.Focus();
            }
        }
    }

    private int AddParents()
    {
        MembershipUser newUser = Membership.GetUser(CreateUserWizard1.UserName);
        Guid newUserId = (Guid)newUser.ProviderUserKey;
        FamiliesBLL familyadapter = new FamiliesBLL();
        int NewFamilyID = familyadapter.GetNewFamilyID(newUserId);


        string PrimaryParentFirstName = "", PrimaryParentLastName = "",
            PrimaryParentAddressLineOne = "", PrimaryParentAddressLineTwo = "",
            PrimaryParentCity = "", PrimaryParentState = "", PrimaryParentZip = "",
            PrimaryParentHomePhone = "", PrimaryParentCellPhone = "",
            PrimaryParentWorkPhone = "", PrimaryParentEmail = "", PrimaryParentNotes = "";
        string SecondaryParentFirstName = "", SecondaryParentLastName = "",
            SecondaryParentAddressLineOne = "", SecondaryParentAddressLineTwo = "",
            SecondaryParentCity = "", SecondaryParentState = "", SecondaryParentZip = "",
            SecondaryParentHomePhone = "", SecondaryParentCellPhone = "",
            SecondaryParentWorkPhone = "", SecondaryParentEmail = "", SecondaryParentNotes = "";

        int parentsadded = 0;

        ParentsBLL ParentsAdapter = new ParentsBLL();

        PrimaryParentFirstName = PrimaryFirstName.Text;
        PrimaryParentLastName = PrimaryLastName.Text;
        PrimaryParentAddressLineOne = PrimaryAddressLineOne.Text;
        PrimaryParentAddressLineTwo = PrimaryAddressLineTwo.Text;
        PrimaryParentCity = PrimaryCity.Text;
        PrimaryParentState = PrimaryState.SelectedValue;
        PrimaryParentZip = PrimaryZip.Text;
        PrimaryParentHomePhone = PrimaryHomePhone.Text;
        PrimaryParentCellPhone = PrimaryCellPhone.Text;
        PrimaryParentWorkPhone = PrimaryWorkPhone.Text;
        PrimaryParentEmail = PrimaryEmail.Text;
        PrimaryParentNotes = PrimaryNotes.Text;

        if (ParentsAdapter.InsertParent(PrimaryParentFirstName, PrimaryParentLastName, PrimaryParentAddressLineOne,
            PrimaryParentAddressLineTwo, PrimaryParentCity, PrimaryParentState, PrimaryParentZip,
            PrimaryParentHomePhone, PrimaryParentCellPhone, PrimaryParentWorkPhone, PrimaryParentNotes,
            PrimaryParentEmail, true, NewFamilyID))
            parentsadded++;

        ProfileCommon p = Profile.GetProfile(newUser.UserName);

        if (string.IsNullOrEmpty(p.FamilyID))
        {
            p.FamilyID = NewFamilyID.ToString();
            p.Save();
        }

        if (RadioButtonList1.SelectedValue != "No")
        {
            SecondaryParentFirstName = SecondaryFirstName.Text;
            SecondaryParentLastName = SecondaryLastName.Text;
            SecondaryParentAddressLineOne = SecondaryAddressLineOne.Text;
            SecondaryParentAddressLineTwo = SecondaryAddressLineTwo.Text;
            SecondaryParentCity = SecondaryCity.Text;
            SecondaryParentState = SecondaryState.SelectedValue;
            SecondaryParentZip = SecondaryZip.Text;
            SecondaryParentHomePhone = SecondaryHomePhone.Text;
            SecondaryParentCellPhone = SecondaryCellPhone.Text;
            SecondaryParentWorkPhone = SecondaryWorkPhone.Text;
            SecondaryParentEmail = SecondaryEmail.Text;
            SecondaryParentNotes = SecondaryNotes.Text;

            if (ParentsAdapter.InsertParent(SecondaryParentFirstName, SecondaryParentLastName, SecondaryParentAddressLineOne,
                SecondaryParentAddressLineTwo, SecondaryParentCity, SecondaryParentState, SecondaryParentZip,
                SecondaryParentHomePhone, SecondaryParentCellPhone, SecondaryParentWorkPhone, SecondaryParentNotes,
                SecondaryParentEmail, false, NewFamilyID))
                parentsadded++;
        }

        return parentsadded;
    }
    protected void SendingActivationEmail(object sender, MailMessageEventArgs e)
    {
        e.Message.Bcc.Add("cpierson@sev.org");

        // Get the UserId of the just-added user
        MembershipUser newUser = Membership.GetUser(CreateUserWizard1.UserName);

        Guid newUserId = (Guid)newUser.ProviderUserKey;

        // Determine the full verification URL (i.e., http://localhost:6587/GTACTools/Account/Activate.aspx?ID=...)
        string urlBase = Request.Url.GetLeftPart(UriPartial.Authority) +
             Request.ApplicationPath;

        string verifyUrl = "/Activate.aspx?ID=" + newUserId.ToString();
        string fullUrl = urlBase + verifyUrl;

        // Replace <%VerificationUrl%> with the appropriate URL and querystring
        e.Message.Body = e.Message.Body.Replace("<%VerificationUrl%>", fullUrl);

    }
    protected void CheckAddress(object source, ServerValidateEventArgs args)
    {
        CustomValidator SourceCustomValidator = ((CustomValidator)source);

        if (SourceCustomValidator.ID.Contains("Primary"))
        {
            try
            {
                Address AddressToCheck = new Address(PrimaryAddressLineOne.Text, PrimaryAddressLineTwo.Text,
                    PrimaryCity.Text, PrimaryState.Text, PrimaryZip.Text);
                AddressToCheck.SetFromGoogle();
                if (AddressToCheck.Status == Address.StatusType.VALID)
                    args.IsValid = true;
                else
                {
                    args.IsValid = false;
                    SourceCustomValidator.IsValid = false;
                    
                    SourceCustomValidator.ErrorMessage = "The address supplied does not appear to be a Valid address.";
                }
            }
            catch (Exception e)
            {
                args.IsValid = false;
                SourceCustomValidator.IsValid = false;
                SourceCustomValidator.ErrorMessage = "There was an unknown error processing this address. Please retry entering the address.<br />" +
                    "If the error continues, please contact Chris Pierson at cpierson@sev.org.<br />Error Message: " + e.Message + "<br />" + 
                    e.TargetSite ;
            }
        }
        else if (SourceCustomValidator.ID.Contains("Secondary"))
        {
            try
            {
                Address AddressToCheck = new Address(SecondaryAddressLineOne.Text, SecondaryAddressLineTwo.Text,
                    SecondaryCity.Text, SecondaryState.Text, SecondaryZip.Text);
                AddressToCheck.SetFromGoogle();
                if (AddressToCheck.Status == Address.StatusType.VALID)
                    args.IsValid = true;
                else
                {
                    args.IsValid = false;
                    SourceCustomValidator.ErrorMessage = "The address supplied does not appear to be a Valid address.";
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
                SourceCustomValidator.IsValid = false;
                SourceCustomValidator.ErrorMessage = "There was an unknown error processing this address. Please retry entering the address.<br />" +
                    "If the error continues, please contact Chris Pierson at cpierson@sev.org.";
            }
        }
    }
    protected void NavigateButtonClicked(object sender, WizardNavigationEventArgs e)
    {
        if (!Page.IsValid)
            e.Cancel = true;
    }
    protected void ValidateCapitolization(object source, ServerValidateEventArgs args)
    {
        CustomValidator SourceCustomValidator = ((CustomValidator)source);
        String Text = ((TextBox)this.CreateUserWizard1.FindControl(SourceCustomValidator.ControlToValidate)).Text;

        if (!String.IsNullOrEmpty(Text))
        {
            char[] TextArray = Text.ToCharArray();
            bool HasCapitol = false;
            int NumberOfLetters = 0, NumberOfCapitols = 0;
            for(int i = 0; i < TextArray.Length; i++)
                if (char.IsLetter(TextArray[i]))
                {
                    NumberOfLetters++;
                    if (char.IsUpper(TextArray[i]))
                    {
                        HasCapitol = true;
                        NumberOfCapitols++;
                    }
                }
            if (!HasCapitol)
            {
                args.IsValid = false;
                SourceCustomValidator.IsValid = false;
                SourceCustomValidator.ErrorMessage = "There must be at least 1 capitol letter.";
            }
            else if (NumberOfLetters <= NumberOfCapitols)
            {
                args.IsValid = false;
                SourceCustomValidator.IsValid = false;
                SourceCustomValidator.ErrorMessage = "All capitol letters are entered. Make sure you do not have your CAPS LOCK on.";
            }
            else
                //Is valid if there is at least 1 capitol letter, and not all the letters are capitol
                SourceCustomValidator.IsValid = true;
        }

        //Is valid if there is no text
        SourceCustomValidator.IsValid = true;
    }
}