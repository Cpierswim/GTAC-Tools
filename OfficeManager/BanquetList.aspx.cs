using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeManager_BanquetList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangeFamilyIDtoFamilyName(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int FamilyID = int.Parse(((HiddenField)e.Row.FindControl("FamilyIDHiddenField")).Value);

            FamiliesBLL FamiliesAdapter = new FamiliesBLL();
            String FamilyLastName = FamiliesAdapter.GetFamilyLastName(FamilyID);

            ((Label)e.Row.FindControl("FamilyNameLabel")).Text = FamilyLastName;
        }
    }
    protected void GridView1DataBound(object sender, EventArgs e)
    {
        int totaladults = 0, totalchildren= 0;
        for(int i = 0; i < GridView1.Rows.Count; i++)
            if (GridView1.Rows[i].RowType == DataControlRowType.DataRow)
            {
                object o = GridView1.Rows[i].FindControl("AdultsTextBox");
                if (o != null)
                    totaladults += int.Parse(((TextBox)o).Text);
                else
                    totaladults += int.Parse(((Label)GridView1.Rows[i].FindControl("AdultsLabel")).Text);

                o = GridView1.Rows[i].FindControl("ChildrenTextBox");
                if (o != null)
                    totalchildren += int.Parse(((TextBox)o).Text);
                else
                    totalchildren += int.Parse(((Label)GridView1.Rows[i].FindControl("ChildrenLabel")).Text);
            }

        TotalLabel.Text = "Total Adults: " + totaladults + "<br />Total Children: " + totalchildren;
    }
    protected void RemoveFamilyClicked(object sender, GridViewCommandEventArgs e)
    {
        BanquetSignUpsBLL BanquetAdapter = new BanquetSignUpsBLL();

        int FamilyID = int.Parse(((HiddenField)GridView1.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("FamilyIDHiddenField")).Value);

        BanquetAdapter.DeleteFamilySignUpNoMessage(FamilyID);

        GridView1.DataBind();
    }
    protected void DeleteAllSignups(object sender, EventArgs e)
    {
        BanquetSignUpsBLL BanquetBLL = new BanquetSignUpsBLL();
        BanquetBLL.DeleteAllBanquetSignUps();
        GridView1.DataBind();
    }
}