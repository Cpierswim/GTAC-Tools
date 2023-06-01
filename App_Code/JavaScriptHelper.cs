using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JavaScriptHelper
/// </summary>
public static class JavaScriptHelper
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