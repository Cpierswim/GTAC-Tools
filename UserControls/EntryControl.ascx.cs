using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


public partial class EntryControl : System.Web.UI.UserControl
{
    bool AddJavascriptToPage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            this.EntryCheckBox.Attributes.Add("onclick", "ToggleTimeControl(this)");
            //this.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            if (this.AddJavascriptToPage)
                this.AddJavaScript();

            if (!String.IsNullOrWhiteSpace(this.CheckBoxClientEvent))
            {
                if (this.CheckBoxClientEvent.ToUpper() == "onclick".ToUpper())
                    this.EntryCheckBox.Attributes.Add("onclick", "ToggleTimeControl(this); " + this.CheckBoxClientScript);
                else
                    this.EntryCheckBox.Attributes.Add(this.CheckBoxClientEvent, this.CheckBoxClientScript);
            }

            //this.TimeControl1.DefaultCourse = this._DefaultCourse;
        }
        catch (Exception)
        {
        }
    }

    public EntryControl()
    {
        this.LoadFromPreviousValues = true;
    }

    public EntryControl(bool AddJavascriptToPage)
    {
        this.AddJavascriptToPage = AddJavascriptToPage;
        this.LoadFromPreviousValues = true;
    }

    private bool LoadFromPreviousValues;
    public EntryControl(bool AddJavascriptToPage, bool LoadFromPreviousValues)
    {
        this.AddJavascriptToPage = AddJavascriptToPage;
        this.LoadFromPreviousValues = LoadFromPreviousValues;
    }

    public bool IsEntered
    {
        get
        {
            return EntryCheckBox.Checked;
        }
    }

    //public HyTekTime EntryTime
    //{
    //    get { return this.TimeControl1.Time; }
    //    set
    //    {
    //        if (this.TimeControl1 != null)
    //            this.TimeControl1.Time = value;
    //        else
    //            this.WaitingTime = value;
    //    }
    //}
    

    //private CheckBox EntryCheckBox;
    //private TimeControl _TimeControl;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (EntryCheckBox == null)
        {
            EntryCheckBox = new CheckBox();
            EntryCheckBox.ID = "EntryCheckBox";
            EntryCheckBox.Checked = false;
            this.Controls.Add(EntryCheckBox);
        }

        //if (TimeControl1 == null)
        //{
            
        //    TimeControl1 = new ASP.usercontrols_timecontrol_timecontrol_ascx();
        //    TimeControl1.LoadControl("~/UserControls/TimeControl/TimeControl.ascx");
        //    TimeControl1 = ((ASP.usercontrols_timecontrol_timecontrol_ascx)LoadControl(typeof(ASP.usercontrols_timecontrol_timecontrol_ascx), new object[0]));
        //    this.TimeControl1.ID = "TimeControl1";
        //    this.TimeControl1.Enabled = false;
        //    TimeControl1.Time = WaitingTime;
        //    this.Controls.Add(TimeControl1);
        //}

        if (Request.Form[this.EntryCheckBox.UniqueID] != null)
            if (Request.Form[this.EntryCheckBox.UniqueID].ToUpper() == "ON")
                EntryCheckBox.Checked = true;
            else
                EntryCheckBox.Checked = false;
        //if (EntryCheckBox.Checked)
        //    TimeControl1.Enabled = true;
    }

    private void AddJavaScript()
    {
        String csName = "ToggleTime";
        Type csType = this.GetType();
        ClientScriptManager cs = Page.ClientScript;

        if (!cs.IsClientScriptBlockRegistered(csType, csName))
        {
            StringBuilder csText = new StringBuilder();
            csText.Append("\n<script type=\"text/javascript\">"); csText.Append("\n");
            csText.Append("function ToggleTimeControl(source) {"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var ID = source.id.substring(0, source.id.length - " + "EntryCheckBox".Length + ") + \"TimeControl1_TimeTextBox\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var VID = source.id.substring(0, source.id.length - " + "EntryCheckBox".Length + ") + \"TimeControl1_TimeValidator\";"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var TextBox = document.getElementById(ID);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("var Validator = document.getElementById(VID);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("if(source.checked)"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("TextBox.disabled = false;"); csText.Append("\n");
            csText.Append("\t"); csText.Append("else"); csText.Append("\n");
            csText.Append("\t"); csText.Append("{"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("TextBox.className  = \"\";"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("TextBox.value = \"\";"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("TextBox.disabled = true;"); csText.Append("\n");
            csText.Append("\t\t"); csText.Append("Validator.isvalid = \"\";"); csText.Append("\n");
            //csText.Append("\t\t"); csText.Append("var myVal = document.getElementById(ID, false);"); csText.Append("\n");
            csText.Append("\t"); csText.Append("}"); csText.Append("\n");
            csText.Append("}");
            csText.Append("\n</script>");
            cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
        }
    }

    public void AddClientEvent(InnerControl ControlToAddScriptTo, String Event, String ClientScript)
    {
        if (!ClientScript.EndsWith(";"))
            ClientScript = ClientScript + ";";
        if (ControlToAddScriptTo == InnerControl.CheckBox)
        {
            this.CheckBoxClientEvent = Event;
            this.CheckBoxClientScript = ClientScript;
        }
    }

    private String CheckBoxClientEvent = "";
    private String CheckBoxClientScript = "";

    public enum InnerControl { CheckBox, TextBox };

    //public String MeetCourseClientString
    //{
    //    get
    //    {
    //        return this.TimeControl1.MeetCourseClientString;
    //    }
    //    set
    //    {
    //        this.TimeControl1.MeetCourseClientString = value;
    //    }
    //}

    //private TimeControl.Course _DefaultCourse;
    //public TimeControl.Course DefaultCourse
    //{
    //    get { return this._DefaultCourse; }
    //    set { this._DefaultCourse = value; }
    //}

    //public String TimeText
    //{
    //    get
    //    {
    //        return this.TimeControl1.Time.ToString();
    //    }
    //    set
    //    {
    //        this.TimeControl1.Time = new HyTekTime(1999, "Y");
    //        String holder = value;
    //    }
    //}
}