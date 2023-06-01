using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_ManageCredits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridDatabound(object sender, EventArgs e)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        CreditsBLL CreditsAdapter = new CreditsBLL();
        ParentsBLL ParentsAdapter = new ParentsBLL();

        SwimTeamDatabase.SwimmersDataTable swimmers = SwimmersAdapter.GetSwimmers();
        SwimTeamDatabase.MeetCreditsDataTable credits = CreditsAdapter.GetAllCredits();
        SwimTeamDatabase.ParentsDataTable parents = ParentsAdapter.GetParents();

        GridView Grid = ((GridView)sender);
        for (int i = 0; i < Grid.Rows.Count; i++)
        {
            GridViewRow Row = Grid.Rows[i];
            if (Row.RowType == DataControlRowType.DataRow)
            {
                int FamilyID = int.Parse(((HiddenField)Row.FindControl("FamilyIDHiddenField")).Value);

                //get list of parents
                List<SwimTeamDatabase.ParentsRow> FamiliesParents = new List<SwimTeamDatabase.ParentsRow>();
                for (int j = 0; j < parents.Count; j++)
                    if (parents[j].FamilyID == FamilyID)
                        FamiliesParents.Add(parents[j]);

                //get List of Swimmers
                List<SwimTeamDatabase.SwimmersRow> FamiliesSwimmers = new List<SwimTeamDatabase.SwimmersRow>();
                for (int j = 0; j < swimmers.Count; j++)
                    if (swimmers[j].FamilyID == FamilyID)
                        FamiliesSwimmers.Add(swimmers[j]);

                //get Credit Account
                SwimTeamDatabase.MeetCreditsRow creditAccount = null;
                for (int j = 0; j < credits.Count; j++)
                {
                    if (credits[j].FamilyID == FamilyID)
                    {
                        creditAccount = credits[j];
                        j = credits.Count;
                    }
                }

                
                SwimTeamDatabase.ParentsRow PrimaryParent = null;
                SwimTeamDatabase.ParentsRow SecondaryParent = null;

                for (int j = 0; j < FamiliesParents.Count; j++)
                    if (FamiliesParents[j].PrimaryContact)
                        PrimaryParent = FamiliesParents[j];
                    else
                        SecondaryParent = FamiliesParents[j];

                Label InnerHtml = ((Label)Row.FindControl("InnerHtmlLabel"));
                if (PrimaryParent != null)
                    InnerHtml.Text = "Primary Parent: " + PrimaryParent.FirstName + " " + PrimaryParent.LastName;
                else
                    InnerHtml.Text = "No Primary Parent";
                if (SecondaryParent != null)
                    InnerHtml.Text += ", Secondary Parent: " + SecondaryParent.FirstName + " " + SecondaryParent.LastName;
                InnerHtml.Text += "<br />Swimmers: ";
                foreach (SwimTeamDatabase.SwimmersRow swimmer in FamiliesSwimmers)
                    InnerHtml.Text += "<b>" + swimmer.PreferredName + " " + swimmer.LastName + "</b>, ";
                InnerHtml.Text = InnerHtml.Text.Substring(0, (InnerHtml.Text.Length - 2));
                InnerHtml.Text += "<br />Available Credits: ";
                if (creditAccount != null)
                    InnerHtml.Text += creditAccount.NumberOfCredits;
                else
                    InnerHtml.Text += "??";
            }
        }
    }
    protected void ButtonClicked(object sender, GridViewCommandEventArgs e)
    {
        int RowIndex =int.Parse(e.CommandArgument.ToString());
        GridView sourceGrid = ((GridView)e.CommandSource);
        GridViewRow SourceRow = sourceGrid.Rows[RowIndex];

        int FamilyID = int.Parse(((HiddenField)SourceRow.FindControl("FamilyIDHiddenField")).Value);
        CreditsBLL CreditsAdapter = new CreditsBLL();
        if (e.CommandName == "Add")
            CreditsAdapter.AddCreditToFamily(FamilyID);
        else if (e.CommandName == "Subtract")
            CreditsAdapter.SubtractCreditFromFamily(FamilyID);

        sourceGrid.DataBind();
    }
}