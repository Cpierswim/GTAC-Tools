using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class UserControls_Repeaters_CoachRepeater : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        FileStream fs = new FileStream(Server.MapPath("repeater_data.xml"),
                                FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(fs);
        ds.ReadXml(reader);
        fs.Close();
        DataView view = new DataView(ds.Tables[0]);
        MyRepeater.DataSource = view;
        MyRepeater.DataBind();

    }
}