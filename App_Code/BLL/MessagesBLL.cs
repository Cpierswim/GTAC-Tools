using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SwimTeamDatabaseTableAdapters;

[System.ComponentModel.DataObject]
public class MessagesBLL
{
    private MessagesTableAdapter _messagesAdapter = null;
    protected MessagesTableAdapter Adapter
    {
        get
        {
            if (_messagesAdapter == null)
                _messagesAdapter = new MessagesTableAdapter();

            return _messagesAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MessagesDataTable GetAllMessages()
    {
        return Adapter.GetAllMessages();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MessagesDataTable GetMessagesNotSeenByOfficeManager()
    {
        return Adapter.GetMessagesByNotSeenByOfficeManager();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.MessagesDataTable GetMessagesByNotSeenByDatabaseManager()
    {
        return Adapter.GetMessagesByNotSeenByDatabaseManager();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public bool SetMessageAsSeenByDatabaseManager(int MessageID)
    {
        SwimTeamDatabase.MessagesDataTable Messages = Adapter.GetMessageByMessageID(MessageID);
        SwimTeamDatabase.MessagesRow message = Messages[0];

        message.SeenByDatabaseManager = true;


        if (!IsMessageSeenByBoth(message))
            return Adapter.Update(message) == 1;
        else
            return ActuallyDeleteMessage(message);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public bool SetMessageAsSeenByOfficeManager(int MessageID)
    {
        SwimTeamDatabase.MessagesDataTable Messages = Adapter.GetMessageByMessageID(MessageID);
        SwimTeamDatabase.MessagesRow message = Messages[0];

        message.SeenByOfficeManager = true;

        if (!IsMessageSeenByBoth(message))
            return Adapter.Update(message) == 1;
        else
            return ActuallyDeleteMessage(message);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool InsertMessage(String MessageText)
    {
        MessageText = MessageText.Trim();

        return Adapter.InsertMessage(MessageText, false, false) == 1;
    }

    private bool IsMessageSeenByBoth(SwimTeamDatabase.MessagesRow Message)
    {
        return (Message.SeenByOfficeManager && Message.SeenByDatabaseManager);
    }

    private bool ActuallyDeleteMessage(SwimTeamDatabase.MessagesRow Message)
    {
        return Adapter.DeleteMessage(Message.MessageID) == 1;
    }
}