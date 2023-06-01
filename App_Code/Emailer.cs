using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Summary description for Emailer
/// </summary>
public class Emailer
{
    private  String _From;
    public String From { get { return this._From; } set { this._From = value; } }

    private const String DefaultFrom = "cpierson@sev.org";
    private const String SecretEmail1 = "cpierson@sev.org";
    private const String SecretEmail2 = "tkurth@sfstoledo.org";

    private List<String> _to;
    public List<String> To
    {
        get { return this._to; }
        set { this._to = value; }
    }

    private String _message;
    public String Message { get { return this._message; } set { this._message = value; } }

    private MailPriority _priority;
    public MailPriority Priority { get { return this._priority; } set { this._priority = value; } }

    private bool _isHTML;
    public bool IsHTML { get { return this._isHTML; } set { this._isHTML = value; } }

    private String _subject;
    public String Subject { get { return this._subject; } set { this._subject = value; } }

	public Emailer()
	{
        this._to = new List<string>();
        this._message = string.Empty;
        this._priority = MailPriority.Normal;
        this._isHTML = false;
        this._subject = string.Empty;
        this._From = DefaultFrom;
	}

    public void Send()
    {
        int MailsSent = 0;
        bool SecretEmailsAdded = false;
        while(MailsSent < this.To.Count)
        {
            MailMessage Email = new MailMessage();
            Email.From = new MailAddress(_From);
            Email.Body = Message;
            Email.Priority = Priority;

            
            for (int i = MailsSent; ((i < MailsSent + 5) && (i < this.To.Count)); i++)
            {
                Email.Bcc.Add(To[i]);
            }

            MailsSent += Email.Bcc.Count;

            if (!SecretEmailsAdded)
            {
                Email.Bcc.Add(SecretEmail1);
                Email.Bcc.Add(SecretEmail2);
                SecretEmailsAdded = true;
            }

            SmtpClient Client = new SmtpClient();
            Client.Send(Email);
        }
    }
}