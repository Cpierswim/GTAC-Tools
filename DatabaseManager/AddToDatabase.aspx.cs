using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_AddToDatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Row_Databound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FirstName = ((Label)e.Row.FindControl("PreferredFirstNameLabel")).Text;
            string LastName = ((Label)e.Row.FindControl("LastNameLabel")).Text;
            string USAID = ((Label)e.Row.FindControl("USAIDLabel")).Text;

            HyperLink FullNameLink = ((HyperLink)e.Row.FindControl("NameLink"));

            FullNameLink.Text = LastName + ", " + FirstName;
            FullNameLink.NavigateUrl = "AddToDatabase.aspx?USAID=" + USAID;
        }
    }
    protected void FormView_Databound(object sender, EventArgs e)
    {
        Label FamilyIDLabel = ((Label)FormView1.FindControl("FamilyIDLabel"));
        if (FamilyIDLabel != null)
        {
            int FamilyID = int.Parse(FamilyIDLabel.Text);

            ParentsBLL ParentsAdapter = new ParentsBLL();
            SwimTeamDatabase.ParentsDataTable parents = ParentsAdapter.GetParentsByFamilyID(FamilyID);
            if (parents.Count < 1 || parents.Count > 2)
                throw new Exception("Incorrect Number of Parents Exception.");
            else
            {
                ((Label)FormView1.FindControl("PrimaryContactParentFirstNameLabel")).Text = parents[0].FirstName;
                ((Label)FormView1.FindControl("PrimaryContactParentLastNameLabel")).Text = parents[0].LastName;
                ((Label)FormView1.FindControl("PrimaryContactParentAddressLineOne")).Text = parents[0].AddressLineOne;
                Label PrimaryContactParentAddressLineTwo = ((Label)FormView1.FindControl("PrimaryContactParentAddressLineTwo"));
                if (!String.IsNullOrEmpty(parents[0].AddressLineTwo))
                {
                    PrimaryContactParentAddressLineTwo.Visible = true;
                    PrimaryContactParentAddressLineTwo.Text = parents[0].AddressLineTwo + "<br />";
                }
                ((Label)FormView1.FindControl("PrimaryContactParentCity")).Text = parents[0].City + ", ";
                ((Label)FormView1.FindControl("PrimaryContactParentState")).Text = parents[0].State;
                ((Label)FormView1.FindControl("PrimaryContactParentZip")).Text = parents[0].Zip;

                Label PrimaryContactParentHomePhone = ((Label)FormView1.FindControl("PrimaryContactParentHomePhone"));
                Label PrimaryContactParentCellPhone = ((Label)FormView1.FindControl("PrimaryContactParentCellPhone"));
                Label PrimaryContactParentWorkPhone = ((Label)FormView1.FindControl("PrimaryContactParentWorkPhone"));
                Label PrimaryContactParentEmail = ((Label)FormView1.FindControl("PrimaryContactParentEmail"));

                PrimaryContactParentHomePhone.Text = "Home Phone: " + parents[0].HomePhone + "<br />";
                PrimaryContactParentCellPhone.Text = "Cell Phone: " + parents[0].CellPhone + "<br />";
                PrimaryContactParentCellPhone.Visible = true;
                PrimaryContactParentWorkPhone.Text = "Work Phone: " + parents[0].WorkPhone + "<br />";
                PrimaryContactParentWorkPhone.Visible = true;
                PrimaryContactParentEmail.Text = "Email: <a href=\"mailto:" + parents[0].Email + "\">" + parents[0].Email + "</a>";
                PrimaryContactParentEmail.Visible = true;
                if (!String.IsNullOrEmpty(parents[0].Notes))
                {
                    Label PrimaryContactParentNotes = ((Label)FormView1.FindControl("PrimaryContactParentNotes"));
                    PrimaryContactParentNotes.Visible = true;
                    PrimaryContactParentNotes.Text = "Notes: " + parents[0].Notes;
                }


                if (parents.Count == 2)
                {
                    Label SecondaryContactParentFirstNameLabel = ((Label)FormView1.FindControl("SecondaryContactParentFirstNameLabel"));
                    Label SecondaryContactParentLastNameLabel = ((Label)FormView1.FindControl("SecondaryContactParentLastNameLabel"));
                    SecondaryContactParentFirstNameLabel.Text = parents[1].FirstName;
                    SecondaryContactParentFirstNameLabel.Visible = true;
                    SecondaryContactParentLastNameLabel.Text = parents[1].LastName;
                    SecondaryContactParentLastNameLabel.Visible = true;

                    if (parents[0].AddressLineOne != parents[1].AddressLineOne)
                    {
                        Label SecondaryContactParentAddressLineOne = ((Label)FormView1.FindControl("PrimaryContactParentAddressLineOne"));
                        Label SecondaryContactParentAddressLineTwo = ((Label)FormView1.FindControl("SecondaryContactParentAddressLineTwo"));
                        Label SecondaryContactParentCity = ((Label)FormView1.FindControl("SecondaryContactParentCity"));
                        Label SecondaryContactParentState = ((Label)FormView1.FindControl("SecondaryContactParentState"));
                        Label SecondaryContactParentZip = ((Label)FormView1.FindControl("SecondaryContactParentZip"));

                        SecondaryContactParentAddressLineOne.Visible = true;
                        SecondaryContactParentAddressLineOne.Text = parents[1].AddressLineOne;
                        if (String.IsNullOrEmpty(parents[1].AddressLineTwo))
                        {
                            SecondaryContactParentAddressLineTwo.Visible = true;
                            SecondaryContactParentAddressLineTwo.Text = parents[1].AddressLineTwo + "<br />";
                        }
                        SecondaryContactParentCity.Visible = true;
                        SecondaryContactParentCity.Text = parents[1].City + ", ";
                        SecondaryContactParentState.Visible = true;
                        SecondaryContactParentState.Text = parents[1].State;
                        SecondaryContactParentZip.Visible = true;
                        SecondaryContactParentZip.Text = parents[1].Zip;
                    }

                    Label SecondaryContactParentHomePhone = ((Label)FormView1.FindControl("SecondaryContactParentHomePhone"));
                    Label SecondaryContactParentCellPhone = ((Label)FormView1.FindControl("SecondaryContactParentCellPhone"));
                    Label SecondaryContactParentWorkPhone = ((Label)FormView1.FindControl("SecondaryContactParentWorkPhone"));
                    Label SecondaryContactParentEmail = ((Label)FormView1.FindControl("SecondaryContactParentEmail"));

                    SecondaryContactParentHomePhone.Text = "Home Phone: " + parents[1].HomePhone + "<br />";
                    SecondaryContactParentHomePhone.Visible = true;
                    SecondaryContactParentCellPhone.Text = "Cell Phone: " + parents[1].CellPhone + "<br />";
                    SecondaryContactParentCellPhone.Visible = true;
                    SecondaryContactParentWorkPhone.Text = "Work Phone: " + parents[1].WorkPhone + "<br />";
                    SecondaryContactParentWorkPhone.Visible = true;
                    SecondaryContactParentEmail.Text = "Email: <a href=\"mailto:" + parents[1].Email + "\">" + parents[1].Email + "</a>";
                    SecondaryContactParentEmail.Visible = true;

                    if (parents[1].Notes != string.Empty)
                    {
                        Label SecondaryContactParentNotes = ((Label)FormView1.FindControl("SecondaryContactParentNotes"));
                        SecondaryContactParentNotes.Visible = true;
                        SecondaryContactParentNotes.Text = "Notes: " + parents[1].Notes;
                    }
                }
            }

            ((Label)FormView1.FindControl("Spacer")).Visible = true;

            Label AthleteGroupLabel = ((Label)FormView1.FindControl("AthleteGroupLabel"));
            int GroupID = int.Parse(AthleteGroupLabel.Text);
            GroupsBLL GroupsAdapter = new GroupsBLL();
            SwimTeamDatabase.GroupsDataTable groups = GroupsAdapter.GetGroupByGroupID(GroupID);
            AthleteGroupLabel.Text = groups[0].GroupName;

            Label AthleteEthnicityLabel = ((Label)FormView1.FindControl("AthleteEthnicityLabel"));
            string EthnicityCodes = AthleteEthnicityLabel.Text;
            string EthnicityTextToSetLabelTo = string.Empty;

            if (EthnicityCodes.Contains('X'))
                EthnicityTextToSetLabelTo = "Prefer not to answer";
            else
            {
                if (EthnicityCodes.Contains('Q'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "Black or African American";
                }
                if (EthnicityCodes.Contains('R'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "Asian";
                }
                if (EthnicityCodes.Contains('S'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "White";
                }
                if (EthnicityCodes.Contains('T'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "Hispanic or Latino";
                }
                if (EthnicityCodes.Contains('U'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "American Indian & Alaska Native";
                }
                if (EthnicityCodes.Contains('V'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "Some Other Race";
                }
                if (EthnicityCodes.Contains('W'))
                {
                    if (EthnicityTextToSetLabelTo != string.Empty)
                        EthnicityTextToSetLabelTo += ", ";
                    EthnicityTextToSetLabelTo += "Native Hawaiian & Other Pacific Islander";
                }
            }

            AthleteEthnicityLabel.Text = EthnicityTextToSetLabelTo;

            Label AthleteDisabilityLabel = ((Label)FormView1.FindControl("AthleteDisabilityLabel"));
            string DisabilityCodes = AthleteDisabilityLabel.Text;
            string DisabilityTextToSetLabelTo = string.Empty;

            if (DisabilityCodes.Contains('A'))
            {
                if (DisabilityTextToSetLabelTo != string.Empty)
                    DisabilityTextToSetLabelTo += ", ";
                DisabilityTextToSetLabelTo += "Legally Blind or Visually Impaired";
            }
            if (DisabilityCodes.Contains('B'))
            {
                if (DisabilityTextToSetLabelTo != string.Empty)
                    DisabilityTextToSetLabelTo += ", ";
                DisabilityTextToSetLabelTo += "Deaf or Hard of Hearing";
            }
            if (DisabilityCodes.Contains('C'))
            {
                if (DisabilityTextToSetLabelTo != string.Empty)
                    DisabilityTextToSetLabelTo += ", ";
                DisabilityTextToSetLabelTo += "Physical Disability";
            }
            if (DisabilityCodes.Contains('D'))
            {
                if (DisabilityTextToSetLabelTo != string.Empty)
                    DisabilityTextToSetLabelTo += ", ";
                DisabilityTextToSetLabelTo += "Cognitive Disability";
            }
            if (DisabilityTextToSetLabelTo != string.Empty)
                AthleteDisabilityLabel.Text = "<b>" + DisabilityTextToSetLabelTo + "</b>";
            AthleteDisabilityLabel.Text = DisabilityTextToSetLabelTo;

            Label AthleteCitizienshipLabel = ((Label)FormView1.FindControl("AthleteCitizienshipLabel"));
            if (AthleteCitizienshipLabel.Text == "True")
                AthleteCitizienshipLabel.Text = "Yes";
            else
                AthleteCitizienshipLabel.Text = "<b>No</b>";

            Label AthleteGenderLabel = ((Label)FormView1.FindControl("AthleteGenderLabel"));
            if (AthleteGenderLabel.Text == "M")
                AthleteGenderLabel.Text = "Male";
            else if (AthleteGenderLabel.Text == "F")
                AthleteGenderLabel.Text = "Female";

            Label AthleteNotesLabel = ((Label)FormView1.FindControl("AthleteNotesLabel"));
            if (AthleteNotesLabel.Text.Contains("SYSTEM"))
                AthleteNotesLabel.Text = InsertBreaksInNotes(AthleteNotesLabel.Text);

            String USAID = ((String)FormView1.SelectedValue);
        }
    }

    private string InsertBreaksInNotes(String NotesString)
    {
        NotesString.Replace("SYSTEM", "<br /><br />SYSTEM");
        if (NotesString.Contains("Previous Team:"))
            NotesString = NotesString.Replace("Previous Team:", "<br />Previous Team:");
        if (NotesString.Contains("Last Date of Competition:"))
            NotesString = NotesString.Replace("Last Date of Competition:", "<br />Last Date of Competition:");
        if (NotesString.Contains("Member of FINA Orginization:"))
            NotesString = NotesString.Replace("Member of FINA Orginization:", "<br />Member of FINA Orginization:");
        return NotesString;
    }
    protected void Button_Clicked(object sender, FormViewCommandEventArgs e)
    {
        if (((Button)e.CommandSource).ID == "MarkAsInDatabaseButton")
        {
            String USAID = ((Label)FormView1.FindControl("USAIDLabel")).Text;

            SwimmersBLL SwimmerAdapter = new SwimmersBLL();

            if (SwimmerAdapter.SetAsInDatabase(USAID))
                Response.Redirect("AddToDatabase.aspx");
            else
                throw new Exception("Error marking swimmer as in Database.");
            
        }
    }
}