using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.IsInRole("Parent") ||
            !HttpContext.Current.User.Identity.IsAuthenticated )
            this.Menu1.Visible = false;
    }
}
