using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TimeHelper
/// </summary>
public static class TimeHelper
{
    public static bool ParseTime(String TimeToParse, out int Hour, out int Minute)
    {
        TimeToParse = TimeToParse.ToUpper();
        Hour = -1;
        Minute = -1;

        char[] Splitter = new char[1];
        Splitter[0] = ':';

        String[] SplitStrings = TimeToParse.Split(Splitter);
        if (SplitStrings.Length == 2)
        {
            if (int.TryParse(SplitStrings[0], out Hour))
            {
                if (SplitStrings[1].Contains("PM"))
                    Hour = Hour + 12;
                else if (!SplitStrings[1].Contains("AM"))
                    return false;

                if (!int.TryParse(SplitStrings[1].Substring(0, 2), out Minute))
                    return false;
                
            }
            else
                return false;
        }
        else
            return false;
        if (Minute > 60 || Minute < 0)
        {
            Minute = -1;
            Hour = -1;
            return false;
        }
        if (Hour > 24 || Hour < 1)
        {
            Hour = -1;
            Minute = -1;
            return false;
        }

        return true;
    }
}