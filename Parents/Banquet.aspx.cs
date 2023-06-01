using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Banquet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int ErrorCode = int.Parse(Request.QueryString["Error"]);
            if (ErrorCode == 1)
                ErrorLabel.Visible = true;
        }
        catch (Exception) { }

        if (!Page.IsPostBack)
        {

            for (int i = 0; i < 21; i++)
            {
                AdultsDropDownList.Items.Add(i.ToString());
                ChildrenDropDownList.Items.Add(i.ToString());
            }

            int FamilyID = int.Parse(Profile.FamilyID);

            BanquetSignUpsBLL BanquetAdapter = new BanquetSignUpsBLL();
            SwimTeamDatabase.BanquentSignUpsDataTable BanquetTable = BanquetAdapter.GetBanquetSignUpByFamilyID(FamilyID);
            if (BanquetTable.Count > 0)
            {
                int adults = BanquetTable[0].Adults;
                AdultsDropDownList.Items[adults].Selected = true;
                int children = BanquetTable[0].Children;
                ChildrenDropDownList.Items[children].Selected = true;
                SignUpButton.Text = "Update Sign Up Info";
                DeleteSignUpButton.Visible = true;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            int adults = int.Parse(AdultsDropDownList.SelectedValue);
            int children = int.Parse(ChildrenDropDownList.SelectedValue);

            if ((adults == 0) && (children == 0))
            {
                Response.Redirect("~/Parents/Banquet.aspx?Error=1", false);
            }
            else
            {
                BanquetSignUpsBLL BanquetAdapter = new BanquetSignUpsBLL();
                bool sucess;
                if (!((Button)sender).Text.ToUpper().Contains("UPDATE"))
                    sucess = BanquetAdapter.SignUpFamilyForBanquet(int.Parse(Profile.FamilyID), adults, children);
                else
                    sucess = BanquetAdapter.UpdateFamilySignUp(int.Parse(Profile.FamilyID), adults, children);
                if (sucess)
                    Response.Redirect("~/Parents/FamilyView.aspx?BQM=1", false);
                else
                    Response.Redirect("~/Parents/FamilyView.aspx?BQM=2", false);
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Parents/FamilyView.aspx?BQM=2", true);
        }
    }
    protected void WithdrawFromBanquetClicked(object sender, EventArgs e)
    {
        BanquetSignUpsBLL BanquetAdapter = new BanquetSignUpsBLL();
        bool sucess = BanquetAdapter.DeleteFamilySignUp(int.Parse(Profile.FamilyID));
        if (sucess)
        {
            FamiliesBLL FamilyAdapter = new FamiliesBLL();
            String FamilyLastName = FamilyAdapter.GetFamilyLastName(int.Parse(Profile.FamilyID));
            MessagesBLL MessageAdapter = new MessagesBLL();
            String Message = "The " + FamilyLastName + " family has withdrawn from the banquet.";

            Response.Redirect("~/Parents/FamilyView.aspx?BQM=3", false);
        }
        else
            Response.Redirect("~/Parents/FamilyView.aspx?BQM=4", false);
    }
}