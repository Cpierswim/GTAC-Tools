using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BirthdayHelper
/// </summary>
public class BirthdayHelper
{
    private DateTime _birthday;

    public int AgeNow
    {
        get
        {
            int UnadjustedAge = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year - _birthday.Year;

            bool BirthdayHasNotHappenedYet = true;
            if (_birthday.Month < TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Month)
                BirthdayHasNotHappenedYet = false;
            else
                if ((_birthday.Month == TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Month) && (_birthday.Day <= TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Day))
                    BirthdayHasNotHappenedYet = false;
            if (BirthdayHasNotHappenedYet)
                UnadjustedAge--;
            return UnadjustedAge;
        }
    }
	public BirthdayHelper(DateTime Birthday)
	{
        this._birthday = Birthday;
	}

    public int AgeOnDate(DateTime Date)
    {
        int UnadjustedAge = Date.Year - _birthday.Year;

        bool BirthdayHasNotHappenedYet = true;
        if (_birthday.Month < Date.Month)
            BirthdayHasNotHappenedYet = false;
        else
            if ((_birthday.Month == Date.Month) && (_birthday.Day <= Date.Day))
                BirthdayHasNotHappenedYet = false;
        if (BirthdayHasNotHappenedYet)
            UnadjustedAge--;
        return UnadjustedAge;
    }
}