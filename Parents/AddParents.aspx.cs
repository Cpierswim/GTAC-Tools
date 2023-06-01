using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Parents_AddParents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
                PrimaryEmail.Text = Membership.GetUser().Email;
            int ErrorCode = int.Parse(Request.QueryString["Code"]);

            if (ErrorCode == 1)
            {
                CodeLabel.Text = "No parents found on your account. This likely happened because of an" +
                    " error during the Registration process. You will add parents to your account now.";
                CodeLabel.Visible = true;
            }
        }
        catch (Exception)
        {
            //No codes or code not found. Page should be empty then.
        }
    }

    protected void AdvancingToNextStep(object sender, WizardNavigationEventArgs e)
    {
        if (!Page.IsValid)
            e.Cancel = true;
        else if (Wizard1.ActiveStep.ID == "PrimaryContactParent")
            if (RadioButtonList1.SelectedValue == "No")
                Wizard1.ActiveStepIndex = Wizard1.WizardSteps.Count - 1;
    }

    private int AddParents()
    {
        MembershipUser CurrentUser = Membership.GetUser();
        Guid CurrentUserId = (Guid)CurrentUser.ProviderUserKey;
        FamiliesBLL familyadapter = new FamiliesBLL();
        int CurrentFamilyID = familyadapter.GetNewFamilyID(CurrentUserId);


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
            PrimaryParentEmail, true, CurrentFamilyID))
            parentsadded++;

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
                SecondaryParentEmail, false, CurrentFamilyID))
                parentsadded++;
        }

        return parentsadded;
    }
    protected void ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStep.ID == "Complete")
        {
            int NumberOfParentsAdded = this.AddParents();
            if (NumberOfParentsAdded == 1)
                NumberOfParentsAddedTextBox.Text = NumberOfParentsAdded + " Parent has";
            else
                NumberOfParentsAddedTextBox.Text = NumberOfParentsAdded + " Parents have";
        }
        else if (Wizard1.ActiveStep.ID == "SecondaryContactParent")
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
    protected void CheckAddress(object source, ServerValidateEventArgs args)
    {
        if (Page.User.Identity.Name != "BenHe")
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
                catch (Exception)
                {
                    args.IsValid = false;
                    SourceCustomValidator.ErrorMessage = "There was an unknown error processing this address. Please retry entering the address.<br />" +
                        "If the error continues, please contact Chris Pierson at cpierson@sev.org.";
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
                    SourceCustomValidator.ErrorMessage = "There was an unknown error processing this address. Please retry entering the address.<br />" +
                        "If the error continues, please contact Chris Pierson at cpierson@sev.org.";
                }
            }
        }
    }

    protected void ValidateCapitolization(object source, ServerValidateEventArgs args)
    {
        CustomValidator SourceCustomValidator = ((CustomValidator)source);
        String Text = ((TextBox)this.Wizard1.FindControl(SourceCustomValidator.ControlToValidate)).Text;

        if (!String.IsNullOrEmpty(Text))
        {
            char[] TextArray = Text.ToCharArray();
            bool HasCapitol = false;
            int NumberOfLetters = 0, NumberOfCapitols = 0;
            for (int i = 0; i < TextArray.Length; i++)
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