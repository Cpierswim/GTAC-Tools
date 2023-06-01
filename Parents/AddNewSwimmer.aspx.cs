using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_AddNewSwimmer : System.Web.UI.Page
{
    const string StartTitle = "Start";
    const string Step1Title = "Swimmer Information Step 1";
    const string Step2Title = "Swimmer Information Step 2";
    const string CompleteTitle = "Complete";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Only do the following the first time the page is loaded and not on postbacks.
        if (!this.IsPostBack)
        {
            //Set the birthday drop down list to only contain the years for swimmers aged 3-21

            int CurrentYear = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year;
            for (int i = 3; i < 21; i++)
                BirthdayYearDropDown.Items.Add((CurrentYear - i).ToString());

            //Set the transfer drop down list of years to only contain the current year and the past 2 years.
            for (int i = 0; i < 3; i++)
                DateOfLastCompetitionYearDropDownList.Items.Add((CurrentYear - i).ToString());


            //set the Hidden Field to the FamilyID of the current swimmer 
            int FamilyID = -1;
            //int.TryParse(Session.Contents["FamilyID"].ToString(), out FamilyID);
            int.TryParse(Profile.FamilyID, out FamilyID);
            if (String.IsNullOrEmpty(Profile.FamilyID))
                int.TryParse(Session.Contents["FamilyID"].ToString(), out FamilyID);
            if (FamilyID == 0)
                throw new Exception("Family ID == 0");
            HiddenFamilyID.Value = FamilyID.ToString();


            //Get the last name of the Primary Parent for the family that this swimmer will be added to, and
            //set the last name text box to that last name.
            ParentsBLL ParentsAdapter = new ParentsBLL();
            SwimTeamDatabase.ParentsDataTable parents = ParentsAdapter.GetPrimaryContactParentByFamilyID(FamilyID);
            if (parents.Count == 0)
                Response.Redirect("~/Parents/AddParents.aspx?code=1");

            LastNameTextBox.Text = parents[0].LastName;

            //Get all the e-mails and phon numbers for the parents and add them to the hiddenfields
            //so that we can check to make sure that the athelte's contact info isn't actually the 
            //parents later.
            foreach (SwimTeamDatabase.ParentsRow parent in parents)
            {
                FamilyEmailsHiddenField.Value += parent.Email;
                FamilyPhoneNumbersHiddenField.Value += parent.CellPhone + parent.HomePhone +
                    parent.WorkPhone;
            }


            SetTransferFormHyperlinkFromSettings();



        }
        if (Wizard1.ActiveStep.Name == StartTitle)
            for (int i = 0; i < Wizard1.WizardSteps.Count; i++)
                if (Wizard1.WizardSteps[i].Name != StartTitle)
                    Wizard1.WizardSteps[i].StepType = WizardStepType.Complete;
        if (Wizard1.ActiveStep.Name == "Complete")
            HyperLink1.Visible = false;
        else
            HyperLink1.Visible = true;
    }
    protected void YearNotInList_Clicked(object sender, EventArgs e)
    {
        //The year the swimmer was born was not in the available list of years, so they need to enter a custom year
        CustomYearEntryTextBox.Visible = true;
        BirthdayYearDropDown.Visible = false;
        YearNotInListButton.Visible = false;
        CustomYearValidator.Enabled = true;
        CustomBirthdayYearRangeValidator.Enabled = true;
        int CurrentYear = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year;
        CustomBirthdayYearRangeValidator.MaximumValue = CurrentYear.ToString();
        CustomBirthdayYearRangeValidator.MinimumValue = (CurrentYear - 100).ToString();
    }


    protected void FINAOrginizationRadioButton_IndexChanged(object sender, EventArgs e)
    {
        //The index of the FINAOrginizationRadioButton has changed

        if (MemberOfOtherFINAOrginizationRadioButton.SelectedValue == "Yes")
        {
            //The user has specified that the swimmer is a member of another FINA orginization, 
            //so the controls that let them specify this information need to be updated.
            WhichFINAOrginizationLabel.Visible = true;
            OtherFINAOrginizationTextBox.Visible = true;
            OtherFINAOrginizationRequiredFieldValidator.Enabled = true;
        }
        else
        {
            //The user changed back to that the swimmer is not a member of another FINA
            //orginization, so we need to blank out in information and make the controls
            //invisible again.
            WhichFINAOrginizationLabel.Visible = false;
            OtherFINAOrginizationTextBox.Visible = false;
            OtherFINAOrginizationTextBox.Text = String.Empty;
            OtherFINAOrginizationRequiredFieldValidator.Enabled = false;
        }

    }

    private void SetTransferFormHyperlinkFromSettings()
    {
        //SettingsBLL SettingsAdapter = new SettingsBLL();
        //SwimTeamDatabase.SettingsDataTable settings = SettingsAdapter.GetTransferFormLocationSettings();
        //TransferFormHyperlink.NavigateUrl = settings[0].SettingValue;
    }
    protected void Wizard_ActiveStepChanged(object sender, EventArgs e)
    {
        if (Wizard1.ActiveStep.Name != StartTitle)
            for (int i = 0; i < Wizard1.WizardSteps.Count; i++)
            {
                if (Wizard1.WizardSteps[i].Name == StartTitle)
                    Wizard1.WizardSteps[i].StepType = WizardStepType.Start;
                else if (Wizard1.WizardSteps[i].Name == CompleteTitle)
                    Wizard1.WizardSteps[i].StepType = WizardStepType.Complete;
                else
                    Wizard1.WizardSteps[i].StepType = WizardStepType.Step;
            }

        if (Wizard1.ActiveStep.Title == CompleteTitle)
            CompleteWizard();
    }

    protected void ValidateBirthday(object source, ServerValidateEventArgs args)
    {
        int BirthdayYear = 0, BirthdayMonth = 0, BirthdayDay = 0;
        if (CustomYearEntryTextBox.Visible == true)
            int.TryParse(CustomYearEntryTextBox.Text, out BirthdayYear);
        else
            int.TryParse(BirthdayYearDropDown.SelectedValue, out BirthdayYear);

        int.TryParse(BirthdayMonthDropDownList.SelectedValue, out BirthdayMonth);
        int.TryParse(BirthdayDayDropDownList.SelectedValue, out BirthdayDay);

        try
        {
            DateTime Birthday = new DateTime(BirthdayYear, BirthdayMonth, BirthdayDay);
            args.IsValid = true;
        }
        catch (Exception)
        {
            args.IsValid = false;
        }
    }
    protected void NextButtonClicked(object sender, WizardNavigationEventArgs e)
    {
        if (!Page.IsValid)
            e.Cancel = true;
    }
    protected void ValidateDateofLastCompetition(object source, ServerValidateEventArgs args)
    {
        int Year = 0, Month = 0, Day = 0;

        int.TryParse(DateOfLastCompetitionYearDropDownList.SelectedValue, out Year);
        int.TryParse(DateOfLastCompetitionMonthDropDownList.SelectedValue, out Month);
        int.TryParse(DateOfLastCompetitionDayDropDownList.SelectedValue, out Day);

        try
        {
            DateTime LastCompetitionDate = new DateTime(Year, Month, Day);
            args.IsValid = true;
        }
        catch (Exception)
        {
            args.IsValid = false;
        }
    }

    private void CompleteWizard()
    {
        string LegalFirstName = LegalFirstNameTextBox.Text;
        string PreferredFirstName = PreferredNameTextBox.Text;
        string MiddleName = MiddleNameTextBox.Text;
        string LastName = LastNameTextBox.Text;


        int BirthdayYear = 0, BirthdayMonth = 0, BirthdayDay = 0;
        if (!String.IsNullOrEmpty(CustomYearEntryTextBox.Text))
            int.TryParse(CustomYearEntryTextBox.Text, out BirthdayYear);
        else
            int.TryParse(BirthdayYearDropDown.SelectedValue, out BirthdayYear);

        int.TryParse(BirthdayMonthDropDownList.SelectedValue, out BirthdayMonth);
        int.TryParse(BirthdayDayDropDownList.SelectedValue, out BirthdayDay);


        DateTime Birthday = new DateTime(BirthdayYear, BirthdayMonth, BirthdayDay);

        string Gender = GenderDropDownList.SelectedValue;
        string GroupID = GroupDropDownList.SelectedValue;

        bool USCitizen = true;
        if (USCitizenRadioButton.SelectedValue == "No")
            USCitizen = false;

        bool MemberOfAnotherFINAOrginization = false;
        if (MemberOfOtherFINAOrginizationRadioButton.SelectedValue == "Yes")
            MemberOfAnotherFINAOrginization = true;

        string OtherFINAOrginizationName = OtherFINAOrginizationTextBox.Text;
        string AthletePhone = AthletePhoneTextBox.Text;
        string AthleteEmail = AthleteEmailTextBox.Text;

        string DisabilityCodes = String.Empty;
        for (int i = 0; i < DisabilityCheckBoxList.Items.Count; i++)
            if (DisabilityCheckBoxList.Items[i].Selected)
                DisabilityCodes = DisabilityCodes + DisabilityCheckBoxList.Items[i].Value;

        string EthnicityCodes = String.Empty;
        for (int i = 0; i < EthnicityCheckBoxList.Items.Count; i++)
            if (EthnicityCheckBoxList.Items[i].Selected)
                EthnicityCodes = EthnicityCodes + EthnicityCheckBoxList.Items[i].Value;
        if (String.IsNullOrEmpty(EthnicityCodes))
            EthnicityCodes = "X";



        int Year = 0, Month = 0, Day = 0;

        int.TryParse(DateOfLastCompetitionYearDropDownList.SelectedValue, out Year);
        int.TryParse(DateOfLastCompetitionMonthDropDownList.SelectedValue, out Month);
        int.TryParse(DateOfLastCompetitionDayDropDownList.SelectedValue, out Day);
        DateTime LastCompetitionDate = new DateTime(Year, Month, Day);
        DateTime NextCompetitionDate = LastCompetitionDate.AddDays(120.0);

        string notes = SwimmerNotesTextBox.Text;

        string PreviousTeamName = PreviousTeamTextBox.Text;
        //append notes
        if (!String.IsNullOrEmpty(PreviousTeamName) || MemberOfAnotherFINAOrginization == true)
        {
            notes = notes + "SYSTEM GENERATED:";
            if (!String.IsNullOrEmpty(PreviousTeamName))
                notes = notes + " Previous Team: " + PreviousTeamName + " Last Date of Competition: " + LastCompetitionDate.ToShortDateString() +
                    " Next Date of Competition: " + NextCompetitionDate.ToShortDateString();
            if (MemberOfAnotherFINAOrginization == true)
                notes = notes + " Member of FINA Orginization: " + OtherFINAOrginizationName;
        }

        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        try
        {
            if (SwimmersAdapter.InsertSwimmer(int.Parse(HiddenFamilyID.Value),
                LastName, MiddleName, LegalFirstName, PreferredFirstName, Birthday, Gender, false, false, AthletePhone,
                AthleteEmail, notes, false, int.Parse(GroupID), EthnicityCodes, USCitizen, DisabilityCodes))
            {
                //swimmer added
                CompleteLabel.Text = "The Swimmer has been sucessfully added.";

                if (!String.IsNullOrEmpty(PreviousTeamName))
                {
                    //they have probably enterd a previous team

                    PreviousTeamIndicatedLabel.Visible = true;
                    TransferFormHyperlink.Visible = true;
                }

                //add school info
                int grade = int.Parse(GradeDropDownList.SelectedValue);
                String SchoolName = "";
                if (SchoolNameDropDownList.SelectedValue.Contains("Other"))
                    SchoolName = CustomSchoolNameTextBox.Text;
                else
                    SchoolName = SchoolNameDropDownList.SelectedValue;
                String USAID = SwimmersBLL.CreateUSAID(LastName, MiddleName, LegalFirstName, Birthday);
                SchoolInfoBLL SchoolInfoAdapter = new SchoolInfoBLL();
                SchoolInfoAdapter.AddSchoolInfo(USAID, SchoolName, grade);
            }
            else
            {
                //swimmer not added
                CompleteLabel.Text = "There were errors adding the swimmer. Please contact Chris Pierson at cpierson@sev.org.";
            }
        }
        catch (System.Data.SqlClient.SqlException e)
        {
            if (e.Message.Contains("Violation of PRIMARY KEY"))
            {
                ErrorLabel.Visible = true;
                Wizard1.Visible = false;
            }
            else
                throw e;
        }


    }
    protected void ValidatePreferredFirstName(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !PreferredNameTextBox.Text.Contains(LastNameTextBox.Text.Trim());
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
                if (NumberOfLetters == NumberOfCapitols && (NumberOfLetters == 1 || NumberOfLetters == 2))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    SourceCustomValidator.IsValid = false;
                    SourceCustomValidator.ErrorMessage = "All capitol letters are entered. Make sure you do not have your CAPS LOCK on.";
                }
            }
            else
                //Is valid if there is at least 1 capitol letter, and not all the letters are capitol
                SourceCustomValidator.IsValid = true;
        }

        //Is valid if there is no text
        SourceCustomValidator.IsValid = true;
    }
    protected void ValidateAthletePhone(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !FamilyPhoneNumbersHiddenField.Value.Contains(AthletePhoneTextBox.Text.Trim());
    }

    protected void ValidateAthleteEmail(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !FamilyEmailsHiddenField.Value.Contains(AthleteEmailTextBox.Text.Trim());
    }
    protected void ValidateThatGTACWasNotEnteredAsPreviousTeam(object source, ServerValidateEventArgs args)
    {
        String PreviousTeamLowerCase = PreviousTeamTextBox.Text.ToLower();
        if (PreviousTeamLowerCase.Contains("gtac") ||
            PreviousTeamLowerCase.Contains("greater toledo"))
            args.IsValid = false;
    }
    protected void SchoolNameDropDownListDataItemsAdded(object sender, EventArgs e)
    {
        bool OtherItemContained = false;
        for (int i = 0; i < SchoolNameDropDownList.Items.Count; i++)
            if (SchoolNameDropDownList.Items[i].Value.Contains("Other"))
                OtherItemContained = true;
        if (!OtherItemContained)
            SchoolNameDropDownList.Items.Add("Other...");
    }
    protected void SchoolNameDropDownListSelectedIndexChanged(object sender, EventArgs e)
    {
        if (SchoolNameDropDownList.SelectedValue.Contains("Other"))
        {
            SchoolNameDropDownList.Visible = false;
            CustomSchoolNameTextBox.Visible = true;
            SchoolNameRequiredFieldValidator.Enabled = true;
        }
    }
}