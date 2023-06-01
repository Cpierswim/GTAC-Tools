<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        bool FileNotFound = false;
        String ErrorPage = string.Empty;
        try
        {

            ErrorPage = HttpContext.Current.Request.Url.ToString();
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError().GetBaseException();
            if (ErrorPage.ToString().Contains("tools/tools") && ex.Message.Contains("does not exist"))
            {
                String Page = ErrorPage.ToString();
                Page = Page.Replace("tools/tools", "tools");
                Response.Redirect(Page);
            }
            else
            {
                MembershipUser User = Membership.GetUser();
                if (ex.Message.Contains("does not exist"))
                    FileNotFound = true;
                System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage("website@gtacswim.com", "cpierson@sev.org");
                Email.Subject = "ERROR in GTAC Tools";
                Email.Body = "Error page: " + ErrorPage + "\n\n";
                Email.Body += "Type: " + ex.GetType() + "\n\n";
                Email.Body += "Message: " + ex.Message + "\n\n";
                if (User != null)
                    Email.Body += "User: " + User.UserName + " (email: ) " + User.Email + "\n\n";
                Email.Body += "Stack Trace: " + ex.StackTrace + "\n\n";
                Email.Body += "Soucre: " + ex.Source + "\n\n";
                Email.Body += "Target Site: " + ex.TargetSite;
                Email.Priority = System.Net.Mail.MailPriority.High;

                if (!Request.Url.GetLeftPart(UriPartial.Authority).Contains("localhost") &&
                    !ErrorPage.Contains("WebResource.axd"))
                {
                    System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();
                    Client.Send(Email);
                    Session.Add("Error", ex);
                }
                else
                {
                    System.Console.Write(Email.Body);
                    Session.Add("Error", ex);
                }
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            Server.ClearError();
            if (FileNotFound)
                Response.Redirect("~/404FileNotFound.aspx?Missing=" + ErrorPage.Remove(
                    ErrorPage.IndexOf("http://"), 7));
            else
            {
                Response.Redirect("~/Error.aspx");
            }
        }

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
