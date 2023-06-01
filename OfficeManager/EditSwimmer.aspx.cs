using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Edit_Swimmer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * The code below makes sure that the EditSwimmer value is set in the session, and if it is not, then it sends
         * the page back to the FamilyView page with the error code that that page knows is an error that the swimmer
         * can't be found
         * 
         * This needs to be uncommented out when the site goes live because right now if it can't find the value - it 
         * uses a default value for quicker testing.
         */ 
        try
        {
            USAIDHiddenField.Value = Request.QueryString["USAID"].ToString();
        }
        catch (Exception)
        {
            Response.Redirect("Swimmers.aspx?Error=1", true);
        }
         
    }

    protected void SwimmerUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        int BirthdayMonth = int.Parse(((DropDownList)DetailsView1.FindControl("BirthdayMonthDropDownList")).SelectedValue);
        int BirthdayDay = int.Parse(((DropDownList)DetailsView1.FindControl("BirthdayDayDropDownList")).SelectedValue);
        int BirthdayYear = 0;
        DropDownList BirthdayYearDropDownList = ((DropDownList)DetailsView1.FindControl("BirthdayYearDropDownList"));
        if (BirthdayYearDropDownList.Visible == true)
            BirthdayYear = int.Parse(BirthdayYearDropDownList.SelectedValue);
        else
            BirthdayYear = int.Parse(((TextBox)DetailsView1.FindControl("CustomYearEntryTextBox")).Text);
        DateTime Birthday = new DateTime(BirthdayYear, BirthdayMonth, BirthdayDay);

        e.InputParameters["Birthday"] = Birthday;

        String Notes = ((TextBox)DetailsView1.FindControl("NotesTextBox")).Text;
        String SystemGenderatedNotes = ((HiddenField)DetailsView1.FindControl("SystemGenderatedHiddenField")).Value;

        e.InputParameters["Notes"] = Notes + SystemGenderatedNotes;

        String EthnicityCodes = "";
        CheckBoxList EthnicityCheckBoxList = ((CheckBoxList)DetailsView1.FindControl("EthnicityCheckBoxList"));
        for (int i = 0; i < EthnicityCheckBoxList.Items.Count; i++)
            if (EthnicityCheckBoxList.Items[i].Selected == true)
                EthnicityCodes += EthnicityCheckBoxList.Items[i].Value;

        if (String.IsNullOrEmpty(EthnicityCodes))
            EthnicityCodes = "X";

        e.InputParameters["Ethnicity"] = EthnicityCodes;

        String DisabilityCodes = "";
        CheckBoxList DisabilityCheckBoxList = ((CheckBoxList)DetailsView1.FindControl("DisabilityCheckBoxList"));
        for (int i = 0; i < DisabilityCheckBoxList.Items.Count; i++)
            if (DisabilityCheckBoxList.Items[i].Selected == true)
                DisabilityCodes += DisabilityCheckBoxList.Items[i].Value;

        e.InputParameters["Disability"] = DisabilityCodes;
    }
    protected void YearNotInList_Clicked(object sender, EventArgs e)
    {
        TextBox CustomYearEntryTextBox = ((TextBox)DetailsView1.FindControl("CustomYearEntryTextBox"));
        CustomYearEntryTextBox.Visible = true;
        DetailsView1.FindControl("BirthdayYearDropDownList").Visible = false;
        DetailsView1.FindControl("YearNotInListButton").Visible = false;
        ((RequiredFieldValidator)DetailsView1.FindControl("YearValidator")).Enabled = true;
        String fulldate = ((HiddenField)DetailsView1.FindControl("BirthdayHiddenField")).Value;
        DateTime Birthday = DateTime.Parse(fulldate);
        CustomYearEntryTextBox.Text = Birthday.Year.ToString();
        RangeValidator CustomBirthdayYearRangeValidator = ((RangeValidator)DetailsView1.FindControl("CustomBirthdayYearRangeValidator"));
        CustomBirthdayYearRangeValidator.Enabled = true;
        CustomBirthdayYearRangeValidator.MaximumValue = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year.ToString();
        CustomBirthdayYearRangeValidator.MinimumValue = (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year - 100).ToString();
    }
    protected void CancelButton_Clicked(object sender, EventArgs e)
    {
        Response.Redirect("Swimmers.aspx");
    }
    protected void DetailsView_DataBound(object sender, EventArgs e)
    {
        if(DetailsView1.PageCount == 0)
            Response.Redirect("Swimmers.aspx?Error=1", true);

        HiddenField test = ((HiddenField)DetailsView1.FindControl("BirthdayHiddenField"));
        if (test != null)
        {
            DateTime Birthday = DateTime.Parse(((HiddenField)DetailsView1.FindControl("BirthdayHiddenField")).Value);

            ((DropDownList)DetailsView1.FindControl("BirthdayMonthDropDownList")).SelectedValue = Birthday.Month.ToString();
            ((DropDownList)DetailsView1.FindControl("BirthdayDayDropDownList")).SelectedValue = Birthday.Day.ToString();
            DropDownList BirthdayYearDropDownList = ((DropDownList)DetailsView1.FindControl("BirthdayYearDropDownList"));

            for (int i = (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year - 3); i >= (Birthday.Year - 5); i--)
                BirthdayYearDropDownList.Items.Add(i.ToString());


            BirthdayYearDropDownList.SelectedValue = Birthday.Year.ToString();

            HiddenField NotesHiddenField = ((HiddenField)DetailsView1.FindControl("NotesHiddenField"));
            HiddenField SystemGenderatedHiddenField = ((HiddenField)DetailsView1.FindControl("SystemGenderatedHiddenField"));

            String notes = NotesHiddenField.Value;

            TextBox NotesTextBox = ((TextBox)DetailsView1.FindControl("NotesTextBox"));
            NotesTextBox.Text = notes;

            if (notes.Contains("SYSTEM GENERATED"))
            {
                String BeforeSystemGenerated = "";
                String AfterSystemGenerated = "";

                int index = notes.IndexOf("SYSTEM GENERATED");
                BeforeSystemGenerated = notes.Substring(0, index);
                AfterSystemGenerated = notes.Substring(index);
                SystemGenderatedHiddenField.Value = AfterSystemGenerated;
                ((TextBox)DetailsView1.FindControl("NotesTextBox")).Text = BeforeSystemGenerated;
            }
            else
                SystemGenderatedHiddenField.Value = "";

            String EthnicityCodes = ((HiddenField)DetailsView1.FindControl("OriginalEthnicityCodes")).Value;
            CheckBoxList EthnicityCheckBoxList = ((CheckBoxList)DetailsView1.FindControl("EthnicityCheckBoxList"));
            for (int i = 0; i < EthnicityCheckBoxList.Items.Count; i++)
                if (EthnicityCodes.Contains(EthnicityCheckBoxList.Items[i].Value))
                    EthnicityCheckBoxList.Items[i].Selected = true;

            String DisabilityCodes = ((HiddenField)DetailsView1.FindControl("OriginalDisabilityCodesHiddenField")).Value;
            CheckBoxList DisabilityCheckBoxList = ((CheckBoxList)DetailsView1.FindControl("DisabilityCheckBoxList"));
            for (int i = 0; i < DisabilityCheckBoxList.Items.Count; i++)
                if (DisabilityCodes.Contains(DisabilityCheckBoxList.Items[i].Value))
                    DisabilityCheckBoxList.Items[i].Selected = true;

            EditParentsButton.PostBackUrl = "EditParents.aspx?FamilyID=" + ((HiddenField)DetailsView1.FindControl("FamilyIDHiddenField")).Value +
                "&USAID=" + this.USAIDHiddenField.Value;
        }
    }
    protected void ValidateBirthday(object source, ServerValidateEventArgs args)
    {
        int BirthdayYear = 0, BirthdayMonth = 0, BirthdayDay = 0;
        TextBox CustomYearEntryTextBox = ((TextBox)DetailsView1.FindControl("CustomYearEntryTextBox"));
        DropDownList BirthdayMonthDropDownList = ((DropDownList)DetailsView1.FindControl("BirthdayMonthDropDownList"));
        DropDownList BirthdayDayDropDownList = ((DropDownList)DetailsView1.FindControl("BirthdayDayDropDownList"));
        DropDownList BirthdayYearDropDownList = ((DropDownList)DetailsView1.FindControl("BirthdayYearDropDownList"));
        if (CustomYearEntryTextBox.Visible == true)
            int.TryParse(CustomYearEntryTextBox.Text, out BirthdayYear);
        else
            int.TryParse(BirthdayYearDropDownList.SelectedValue, out BirthdayYear);

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
    protected void SwimmerUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        if (e.Exception == null)
            Response.Redirect("Swimmers.aspx");
        else
            Response.Redirect("Swimmers.aspx?Error=2");
    }
}