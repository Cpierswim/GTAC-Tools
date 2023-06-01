using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tests_GeneralTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //EntryControl a = ((EntryControl)this.Panel1.FindControl("EntryControl1"));
            //EntryControl t = ((EntryControl)this.Panel1.FindControl("EntryControl2"));
            //HyTekTime Time = t.EntryTime;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    //private Control FindControl(String ID)
    //{
    //    try
    //    {
    //        for (int i = 0; i < this.Controls.Count; i++)
    //        {
    //            if (this.Controls[i].FindControl(ID) != null)
    //                return this.Controls[i].FindControl(ID);
    //        }
    //    }catch(Exception)
    //    {
    //    }
    //    return null;
    //}

    protected override void OnInit(EventArgs e)
    {
        //EntryControl EC = new EntryControl();
        //EC.LoadControl("~/UserControls/EntryControl.ascx");
        //EntryControl EC = ((EntryControl)LoadControl(typeof(EntryControl), new object[0]));
        //EC.ID = "EntryControl2";
        //this.Panel1.Controls.Add(EC);
        //Button b = new Button();
        //b.ID = "Button1";
        //b.Text = "Button1";
        //this.Panel1.Controls.Add(b);
        base.OnInit(e);
    }
}