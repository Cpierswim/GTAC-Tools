using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DatabaseManager_SpecialDatabaseManagerPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        FamiliesBLL FamiliesAdapter = new FamiliesBLL();
        ParentsBLL ParentsAdapter = new ParentsBLL();

        int FamiliesWithNoSwimmers = FamiliesAdapter.GetFamiliesWithNoSwimmers().Count;
        int FamiliesWithNoParents = FamiliesAdapter.GetFamiliesWithNoParents().Count;
        int FamiliesWithTooManyParents = FamiliesAdapter.GetFamiliesWithTooManyParents().Count;
        int ParentsWithoutFamilies = ParentsAdapter.GetParentsWithNoFamilies().Count;
        int SwimmersWithoutFamilies = SwimmersAdapter.GetSwimmersWithNoFamily().Count;

        if (FamiliesWithNoSwimmers != 0)
        {
            FamiliesNoSwimmersHyperLink.Text += " (Count: " + FamiliesWithNoSwimmers + ")";
            FamiliesNoSwimmersHyperLink.BackColor = System.Drawing.Color.Yellow;
        }
        if (FamiliesWithNoParents != 0)
        {
            FamiliesNoParentsHyperLink.Text += " (Count: " + FamiliesWithNoParents + ")";
            FamiliesNoParentsHyperLink.BackColor = System.Drawing.Color.Yellow;
        }
        if (FamiliesWithTooManyParents != 0)
        {
            FamiliesTooManyParentsHyperLink.Text += " (Count: " + FamiliesWithTooManyParents + ")";
            FamiliesTooManyParentsHyperLink.BackColor = System.Drawing.Color.Yellow;
        }
        if (ParentsWithoutFamilies != 0)
        {
            ParentsNoFamiliesHyperLink.Text += " (Count: " + ParentsWithoutFamilies + ")";
            ParentsNoFamiliesHyperLink.BackColor = System.Drawing.Color.Yellow;
        }
        if (SwimmersWithoutFamilies != 0)
        {
            SwimmersNoFamiliesHyperLink.Text += " (Count: " + SwimmersWithoutFamilies + ")";
            SwimmersNoFamiliesHyperLink.BackColor = System.Drawing.Color.Yellow;
        }

    }
}