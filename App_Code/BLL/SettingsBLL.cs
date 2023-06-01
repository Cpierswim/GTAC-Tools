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
public class SettingsBLL
{
    private SettingsTableAdapter _settingsAdapter = null;
    protected SettingsTableAdapter Adapter
    {
        get
        {
            if (_settingsAdapter == null)
                _settingsAdapter = new SettingsTableAdapter();

            return _settingsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SettingsDataTable GetAllSettings()
    {
        return Adapter.GetAllSettings();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SettingsDataTable GetTransferFormLocationSettings()
    {
        return Adapter.GetTransferFormLocation();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SettingsDataTable GetApplicationStatusSetting()
    {
        return Adapter.GetApplicationStatusSetting();
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetNewApplicationStatus(String NewApplicationStatus)
    {
        NewApplicationStatus = NewApplicationStatus.Trim();

        return (Adapter.SetNewApplicationStatus(NewApplicationStatus) == 1);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public DateTime GetRegistrationStartDate()
    {
        return DateTime.Parse(
            Adapter.GetRegStartDate()[0].SettingValue);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool SetNewRegistrationStartDate(DateTime NewRegistrationStartDate)
    {
        return Adapter.SetNewRegistrationStartDate(NewRegistrationStartDate.ToString("G")) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.SettingsDataTable GetNewSwimmerNotificationEmail()
    {
        return Adapter.GetNewSwimmerNotificationEmail();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateSwimmerAddedEmail(String NewEmail)
    {
        NewEmail = NewEmail.Trim();

        return Adapter.UpdateSwimmerAddedEmail(NewEmail) == 1;
    }

    public bool DisplayBanquetButton()
    {
        String SettingValue = Adapter.GetDisplayBanquetButtonDisplaySetting()[0].SettingValue.ToUpper();
        try
        {
            bool TFValue = bool.Parse(SettingValue);
            return TFValue;
        }
        catch (Exception)
        {
            return false;
        } 
    }

    public String GetBanquetButtonText()
    {
        return Adapter.GetBanquetButtonText()[0].SettingValue;
    }

    public bool SetDisplayBanquetButtonStatus(bool DisplayStatus)
    {
        return Adapter.SetBanquestButtonDisplayStatus(DisplayStatus.ToString().ToUpper()) == 1;
    }

    public bool SetBanquetButtonText(String BanquestButtonText)
    {
        return Adapter.SetBanquetButtonText(BanquestButtonText) == 1;
    }

    public bool SetGroupUpdateAsPending()
    {
        return Adapter.SetGroupStatusAsPendingUpdate() == 1;
    }

    public bool SetContactInfoUpdatedTime(DateTime ContactInfoUpdatedTime)
    {
        return Adapter.SetContactInfoUpdatedTime(ContactInfoUpdatedTime.ToString()) == 1;
    }

    public DateTime GetContactInfoUpdatedTime()
    {
        String SettingValue = Adapter.GetContactInfoUpdatedTime()[0].SettingValue;
        return DateTime.Parse(SettingValue);
    }
}