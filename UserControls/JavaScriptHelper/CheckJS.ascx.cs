using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Text;

public partial class CheckJS : System.Web.UI.UserControl
{
    protected static string JSQRYPARAM = "jse";
    protected static string JSENABLED = "1";
    protected static string JSDISABLED = "0";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        bool testJS = IsJSEnabled;
        if (Request.QueryString[JSQRYPARAM] != null)
        {
            IsJSEnabled = Request.QueryString[JSQRYPARAM] == JSENABLED;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string GetAppendedUrl(string newParam, string newParamValue)
    {
        string targeturl = string.Empty;
        Uri url = (string.IsNullOrEmpty(ResolveUrl(NonJSTargetURL))) ? new Uri(ResolveUrl(JSTargetURL)) : new Uri(ResolveUrl(NonJSTargetURL));
        if (url == null)
            url = Request.Url;

        string[] qry = url.Query.Replace("?","").Split('&');

        StringBuilder sb = new StringBuilder();
        foreach (string s in qry)
        {
            if (!s.ToLower().Contains(newParam.ToLower()))
            {
                sb.Append(s + "&");
            }
        }

        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
            targeturl = string.Format("{0}?{1}&{2}={3}", url.AbsolutePath, sb.ToString(), newParam, newParamValue);
        }
        else
        {
            targeturl = string.Format("{0}?{1}={2}", url.AbsolutePath, newParam, newParamValue);
        }
        return targeturl;
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (IsJSEnabled)
        {
            string targeturl = GetAppendedUrl(JSQRYPARAM, JSDISABLED);
            HtmlGenericControl ctrl = new HtmlGenericControl("NOSCRIPT");
            ctrl.InnerHtml = string.Format("<meta http-equiv=REFRESH content=0;URL={0}>", targeturl);
            Page.Header.Controls.Add(ctrl);
        }
        else
        {
            if (!string.IsNullOrEmpty(NonJSTargetURL))
                Response.Redirect(NonJSTargetURL);
            HtmlGenericControl ctrl = new HtmlGenericControl("NOSCRIPT");
            ctrl.InnerHtml = string.Empty;
            Page.Header.Controls.Add(ctrl);
        }
    }
    protected bool IsJSEnabled
    {
        get
        {
            if (Session["JS"] == null)
                Session["JS"] = true;

            return (bool)Session["JS"];
        }
        set
        {
            Session["JS"] = value;
        }
    }
    protected string JSTargetURL
    {
        get
        {
            return Request.Url.ToString();
        }
    }
    public string NonJSTargetURL
    {
        get
        {
            return (ViewState["NONJSURL"] != null) ? ViewState["NONJSURL"].ToString() : string.Empty;
        }
        set
        {
            try
            {
                ViewState["NONJSURL"] = ResolveServerUrl(value, false);
            }
            catch
            {
                throw new ApplicationException("Invalid URL. '" + value + "'");
            }
        }
    }
    public string ResolveServerUrl(string serverUrl, bool forceHttps)
    {
        if (serverUrl.IndexOf("://") > -1)

            return serverUrl;
        string newUrl = ResolveUrl(serverUrl);
        Uri originalUri = HttpContext.Current.Request.Url;

        newUrl = (forceHttps ? "https" : originalUri.Scheme) +

                 "://" + originalUri.Authority + newUrl;
        return newUrl;
    } 
}

public class CheckJavaScriptHelper
{
    public static bool IsJavascriptEnabled
    {
        get
        {
            if (HttpContext.Current.Session["JS"] == null)
                HttpContext.Current.Session["JS"] = true;

            return (bool)HttpContext.Current.Session["JS"];
        }
    }
}