using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Hyperlinks_MessagesHyperLink : System.Web.UI.UserControl
{
    public enum DisplayType { OfficeManager, DatabaseManager, Other }

    private DisplayType _displayType;

    public DisplayType DisplayAs
    {
        get { return this._displayType; }
        set
        {
            this._displayType = value;

            if (value == DisplayType.DatabaseManager)
            {
                this.Visible = true;
                this.MessagesHyperLink.NavigateUrl = "~/DatabaseManager/Messages.aspx";
                MessagesBLL MessagesAdapter = new MessagesBLL();
                int NumberOfMessages = MessagesAdapter.GetMessagesByNotSeenByDatabaseManager().Count;
                if (NumberOfMessages > 0)
                {
                    this.CountLabel.Visible = true;
                    this.CountLabel.Text = " - There are currently " + NumberOfMessages + " messages " +
                        "waiting to be viewed";
                    this.MessagesHyperLink.BackColor = System.Drawing.Color.Yellow;
                }
            }
            if (value == DisplayType.OfficeManager)
            {
                this.Visible = true;
                this.MessagesHyperLink.NavigateUrl = "~/OfficeManager/Messages.aspx";
                MessagesBLL MessagesAdapter = new MessagesBLL();
                int NumberOfMessages = MessagesAdapter.GetMessagesNotSeenByOfficeManager().Count;
                if (NumberOfMessages > 0)
                {
                    this.CountLabel.Visible = true;
                    this.CountLabel.Text = " - There are currently " + NumberOfMessages + " messages " +
                        "waiting to be viewed";
                    this.MessagesHyperLink.BackColor = System.Drawing.Color.Yellow;
                }
            }

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public UserControls_Hyperlinks_MessagesHyperLink()
        : base()
    {
        this._displayType = DisplayType.Other;
        this.Visible = false;
    }
}