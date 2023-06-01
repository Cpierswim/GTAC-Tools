using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HyTekTime
/// </summary>
public class HyTekTime
{
    private TimeSpan _Time;
    private bool NoTime;
    private String _course;

    public int? Score
    {
        get
        {
            if (NoTime)
                return null;
            else
                return int.Parse((this._Time.TotalMilliseconds / 10).ToString());
            //if ((Time.Seconds <= 0) && (Time.Minutes <= 0) && (Time.Hours <= 0))
            //    return int.Parse((Time.Milliseconds / 10).ToString());
            //else if ((Time.Minutes <= 0) && (Time.Hours <= 0))
            //    return int.Parse((Time.Seconds * 100 + Time.Milliseconds / 10).ToString());
            //else if (Time.Hours <= 0)
            //    return int.Parse(((Time.Minutes * 60 + Time.Seconds) * 100 + Time.Milliseconds / 10).ToString());
            //else
            //    return int.Parse((((((Time.Hours * 60) + Time.Minutes) * 60) + Time.Seconds) * 100 + Time.Milliseconds / 10).ToString());
        }
    }
    public TimeSpan? Time
    {
        get
        {
            if (this._Time == null)
                return null;
            return this._Time;
        }
    }

    public String Course
    {
        get
        {
            if (!this.IsNoTime)
                return this._course;
            else
                return null;
        }
    }

    public HyTekTime(int? Score, String Course)
    {
        if (Score != null)
        {
            this._Time = new TimeSpan(long.Parse(Score.ToString() + "00000"));
            NoTime = false;
            this._course = Course;
        }
        else
            NoTime = true;

    }
    public HyTekTime(String Time)
    {
        if (String.IsNullOrWhiteSpace(Time))
            throw new InvalidTimeException();
        Time = Time.ToUpper();
        if (Time == "NT")
            this.NoTime = true;
        else
        {
            if (Time.Substring(Time.Length - 1) == "Y" || Time.Substring(Time.Length - 1) == "L" || Time.Substring(Time.Length - 1) == "S")
                this._course = Time.Substring(Time.Length - 1);
            else
                throw new InvalidTimeException();
            Time = Time.Substring(0, (Time.Length - 1));
            if (!HyTekTime.IsValidTime(Time))
                throw new InvalidTimeException();
            else
            {
                this._Time = GetFromTimeString(Time);
            }

        }
    }

    public bool IsNoTime
    {
        get
        {
            return this.NoTime;
        }
    }

    public override string ToString()
    {
        if (this.NoTime)
            return "NT";

        String Hundreths = "";
        if (this._Time.Milliseconds == 0)
            Hundreths = "00";
        else
        {
            int HundrethsINT = this._Time.Milliseconds / 10;
            if (HundrethsINT < 10)
                Hundreths = "0" + HundrethsINT;
            else
                Hundreths = HundrethsINT.ToString();
        }
        if (this._Time.Hours < 1 && this._Time.Minutes < 1)
        {
            return this._Time.Seconds + "." + Hundreths + this._course;
        }
        else if (this._Time.Hours < 1)
        {
            String Seconds = "";
            if (this._Time.Seconds < 10)
                Seconds = "0" + this._Time.Seconds;
            else
                Seconds = this._Time.Seconds.ToString();
            return this._Time.Minutes + ":" + Seconds + "." + Hundreths + this._course;
        }
        else
        {
            String Seconds = "";
            if (this._Time.Seconds < 10)
                Seconds = "0" + this._Time.Seconds;
            else
                Seconds = this._Time.Seconds.ToString();
            String Minutes = "";
            if (this._Time.Minutes < 10)
                Minutes = "0" + this._Time.Minutes;
            else
                Minutes = this._Time.Minutes.ToString();
            return this._Time.Hours + ":" + Minutes + ":" + Seconds + "." + Hundreths + this._course;
        }
    }
    public string ToStringNoCourse()
    {
        if (this.NoTime)
            return "NT";

        String Hundreths = "";
        if (this._Time.Milliseconds == 0)
            Hundreths = "00";
        else
        {
            int HundrethsINT = this._Time.Milliseconds / 10;
            if (HundrethsINT < 10)
                Hundreths = "0" + HundrethsINT;
            else
                Hundreths = HundrethsINT.ToString();
        }
        if (this._Time.Hours < 1 && this._Time.Minutes < 1)
        {
            return this._Time.Seconds + "." + Hundreths;
        }
        else if (this._Time.Hours < 1)
        {
            String Seconds = "";
            if (this._Time.Seconds < 10)
                Seconds = "0" + this._Time.Seconds;
            else
                Seconds = this._Time.Seconds.ToString();
            return this._Time.Minutes + ":" + Seconds + "." + Hundreths;
        }
        else
        {
            String Seconds = "";
            if (this._Time.Seconds < 10)
                Seconds = "0" + this._Time.Seconds;
            else
                Seconds = this._Time.Seconds.ToString();
            String Minutes = "";
            if (this._Time.Minutes < 10)
                Minutes = "0" + this._Time.Minutes;
            else
                Minutes = this._Time.Minutes.ToString();
            return this._Time.Hours + ":" + Minutes + ":" + Seconds + "." + Hundreths;
        }
    }

    public static bool operator ==(HyTekTime Time1, HyTekTime Time2)
    {
        return Time1.Time == Time2.Time;
    }
    public static bool operator <(HyTekTime Time1, HyTekTime Time2)
    {
        return Time1.Time < Time2.Time;
    }
    public static bool operator >(HyTekTime Time1, HyTekTime Time2)
    {
        return Time1.Time > Time2.Time;
    }
    public static bool operator <=(HyTekTime Time1, HyTekTime Time2)
    {
        return Time1.Time <= Time2.Time;
    }
    public static bool operator >=(HyTekTime Time1, HyTekTime Time2)
    {
        return Time1.Time >= Time2.Time;
    }
    public static bool operator !=(HyTekTime Time1, HyTekTime Time2)
    {
        if ((((Object)Time1) == null && ((Object)Time2) != null) ||
            (((Object)Time1) != null && ((Object)Time2) == null))
            return true;
        if(((Object)Time1) == null && ((Object)Time2) == null)
            return false;
        return Time1.Time != Time2.Time;
    }
    //public static HyTekTime operator +(HyTekTime Time1, HyTekTime Time2)
    //{
    //    if (Time1._course != Time2._course)
    //        throw new Exception("Course does not match");
    //    HyTekTime Temp = new HyTekTime(0, Time1._course);
    //    Temp.Time = Time1.Time + Time2.Time;
    //    return Temp;
    //}
    //public static HyTekTime operator -(HyTekTime Time1, HyTekTime Time2)
    //{
    //    if (Time1._course != Time2._course)
    //        throw new Exception("Course does not match");
    //    HyTekTime Temp = new HyTekTime(0, Time1._course);
    //    Temp.Time = Time1.Time - Time2.Time;
    //    return Temp;
    //}
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static HyTekTime NOTIME = new HyTekTime(null, "Y");

    public class InvalidTimeException : Exception { }

    private static bool IsValidTime(String Time)
    {
        Time = Time.ToUpper();
        if (Time == "NT")
            return true;
        if (Time.EndsWith("L") || Time.EndsWith("S") || Time.EndsWith("Y"))
            if (char.IsNumber(Time, (Time.Length - 2)))
                Time = Time.Substring(0, (Time.Length - 1));
        char[] seperator = new char[1];
        seperator[0] = '.';

        String[] SplitByPeriod = Time.Split(seperator);
        if (SplitByPeriod.Length > 2)
            return false;
        if (SplitByPeriod.Length == 1)
            if (SplitByPeriod[0].Length > 2)
            {
                SplitByPeriod[0] = SplitByPeriod[0].Substring(0, SplitByPeriod[0].Length - 2) + "." +
                    SplitByPeriod[0].Substring(SplitByPeriod[0].Length - 2);
                SplitByPeriod = SplitByPeriod[0].Split(seperator);
            }
        try
        {
            int hundredths = int.Parse(SplitByPeriod[SplitByPeriod.Length - 1]);
            if (hundredths > 99)
                return false;
        }
        catch (InvalidCastException)
        {
            return false;
        }
        catch (FormatException)
        {
            return false;
        }

        String TimeBeforePeriod = SplitByPeriod[0];
        if (string.IsNullOrEmpty(TimeBeforePeriod))
            return true;
        if (TimeBeforePeriod.Length > 2 && !TimeBeforePeriod.Contains(":"))
            TimeBeforePeriod = AddColons(TimeBeforePeriod);
        seperator[0] = ':';
        String[] SplitByColon = TimeBeforePeriod.Split(seperator);
        if (SplitByColon.Length > 3)
            return false;
        for (int i = 0; i < SplitByColon.Length; i++)
        {
            try
            {
                if (!(i == 0 && String.IsNullOrEmpty(SplitByColon[i])))
                    if (int.Parse(SplitByColon[i]) > 99)
                        return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        return true;
    }
    private static TimeSpan GetFromTimeString(String Time)
    {
        //if (string.IsNullOrEmpty(Time))
        //    return "NT"; // currently will never be reached, but possibly in the future

        char[] seperator = new char[1];
        seperator[0] = '.';

        String[] SplitByPeriod = Time.Split(seperator);
        int hundredths = 0, seconds = 0, minutes = 0, hours = 0;
        if (SplitByPeriod.Length == 1)
            if (SplitByPeriod[0].Length > 2)
            {
                SplitByPeriod[0] = SplitByPeriod[0].Substring(0, SplitByPeriod[0].Length - 2) + "." +
                    SplitByPeriod[0].Substring(SplitByPeriod[0].Length - 2);
                SplitByPeriod = SplitByPeriod[0].Split(seperator);
            }

        hundredths = int.Parse(SplitByPeriod[SplitByPeriod.Length - 1]);
        seperator[0] = ':';
        if (SplitByPeriod.Length > 1 && !SplitByPeriod[0].Contains(":"))
            SplitByPeriod[0] = AddColons(SplitByPeriod[0]);

        String[] BeforePeriod = SplitByPeriod[0].Split(seperator);
        if (BeforePeriod.Length == 1)
            seconds = int.Parse(BeforePeriod[0]);
        else if (BeforePeriod.Length == 2)
        {
            if (String.IsNullOrEmpty(BeforePeriod[1]))
                seconds = 0;
            else
                seconds = int.Parse(BeforePeriod[1]);
            if (String.IsNullOrEmpty(BeforePeriod[0]))
                minutes = 0;
            else
                minutes = int.Parse(BeforePeriod[0]);
        }
        else if (BeforePeriod.Length == 3)
        {
            seconds = int.Parse(BeforePeriod[2]);
            minutes = int.Parse(BeforePeriod[1]);
            hours = int.Parse(BeforePeriod[0]);
        }
        while (hundredths > 100)
        {
            hundredths = hundredths - 100;
            seconds++;
        }
        while (seconds > 60)
        {
            seconds = seconds - 60;
            minutes++;
        }
        while (minutes > 60)
        {
            minutes = minutes - 60;
            hours++;
        }




        return new TimeSpan(0, hours, minutes, seconds, (hundredths * 10));
    }

    private static String AddColons(String StringToAddColonsTo)
    {
        String ReturnString = "";
        while (StringToAddColonsTo.Length > 2)
        {
            ReturnString = ":" + StringToAddColonsTo.Substring(StringToAddColonsTo.Length - 2) + ReturnString;
            StringToAddColonsTo = StringToAddColonsTo.Substring(0, StringToAddColonsTo.Length - 2);
        }
        if (StringToAddColonsTo.Length <= 2)
            ReturnString = StringToAddColonsTo + ReturnString;
        if (ReturnString.StartsWith(":"))
            ReturnString = ReturnString.Substring(1);
        return ReturnString;
    }
}