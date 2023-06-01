using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MeetEventHelper
/// </summary>
public static class MeetEventHelper
{
    public static String GetAgeString(String AgeCode)
    {

        if (AgeCode == "99")
            return "Open";


        int AgeAsInt = int.Parse(AgeCode);

        if (AgeAsInt < 99)
            return AgeCode + " and Under";

        String FirstPart = "", SecondPart = "";
        if (AgeAsInt > 99 && AgeAsInt < 1000)
        {
            FirstPart = AgeCode.Substring(0, 1);
            SecondPart = AgeCode.Substring(1);
        }
        else
        {
            FirstPart = AgeCode.Substring(0, 2);
            SecondPart = AgeCode.Substring(2);
        }

        int MinAge = int.Parse(FirstPart);
        int MaxAge = int.Parse(SecondPart);

        if (MinAge == MaxAge)
            return MinAge.ToString() + " year old";
        else
        {
            if (MaxAge == 99)
                return MinAge.ToString() + " and Over";

            return MinAge.ToString() + "-" + MaxAge.ToString();
        }
    }

    public static int GetMinAgeFromAgeString(String AgeCode)
    {
        if (AgeCode == "99")
            return 0;


        int AgeAsInt = int.Parse(AgeCode);

        if (AgeAsInt < 99)
            return 0;

        String FirstPart = "";
        if (AgeAsInt > 99 && AgeAsInt < 1000)
            FirstPart = AgeCode.Substring(0, 1);
        else
            FirstPart = AgeCode.Substring(0, 2);

        return int.Parse(FirstPart);
    }

    public static int GetMaxAgeFromAgeString(String AgeCode)
    {
        if (AgeCode == "99")
            return 99;


        int AgeAsInt = int.Parse(AgeCode);

        if (AgeAsInt < 99)
            return AgeAsInt;

        String SecondPart = "";
        if (AgeAsInt > 99 && AgeAsInt < 1000)
            SecondPart = AgeCode.Substring(1);
        else
            SecondPart = AgeCode.Substring(2);

        return int.Parse(SecondPart);
    }

    public static String GetStrokeString(int StrokeCode, StrokeStringLength Length)
    {
        if (Length == StrokeStringLength.Middle)
            switch (StrokeCode)
            {
                case 1:
                    return "Free";
                case 2:
                    return "Back";
                case 3:
                    return "Breast";
                case 4:
                    return "Fly";
                case 5:
                    return "IM";
            }
        if (Length == StrokeStringLength.TwoLetters)
            switch (StrokeCode)
            {
                case 1:
                    return "FR";
                case 2:
                    return "BA";
                case 3:
                    return "BR";
                case 4:
                    return "FL";
                case 5:
                    return "IM";
            }
        if (Length == StrokeStringLength.Long)
            switch (StrokeCode)
            {
                case 1:
                    return "Freestyle";
                case 2:
                    return "Backstroke";
                case 3:
                    return "Breaststroke";
                case 4:
                    return "Butterfly";
                case 5:
                    return "Individual Medley";
            }

        return "";
    }
    public static String GetStrokeString(String StrokeCode, StrokeStringLength Length)
    {
        return MeetEventHelper.GetStrokeString(int.Parse(StrokeCode), Length);
    }

    public enum StrokeStringLength { TwoLetters, Middle, Long };

    public static int CurrentAge(DateTime Birthday)
    {
        int age = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year -
                    Birthday.Year;

        if (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").DayOfYear
                    < Birthday.DayOfYear)
            age--;

        return age;
    }

    public static int AgeOnDate(DateTime Date, DateTime Birthday)
    {
        int age = Date.Year - Birthday.Year;
        if (Date.DayOfYear < Birthday.DayOfYear)
            age--;
        return age;
    }

    public static String GetFullGender(String SexCode)
    {
        SexCode = SexCode.ToUpper();
        if (SexCode == "F")
            return "Girls";
        else if (SexCode == "M")
            return "Boys";
        else if (SexCode == "X")
            return "Mixed";
        throw new Exception("Invalid Sex Code.");
    }

    public static String GetCourse(String CourseCode, StrokeStringLength Length)
    {
        CourseCode.ToUpper();
        if (Length == StrokeStringLength.Long)
        {
            if (CourseCode == "Y")
                return "Yard";
            else if (CourseCode == "L" || CourseCode == "M")
                return "Meter";
            else if (CourseCode == "S")
                return "Short Course Meter";
        }
        else if (Length == StrokeStringLength.Middle)
        {
            if (CourseCode == "Y")
                return "Yard";
            else if (CourseCode == "L" || CourseCode == "M")
                return "Meter";
            else if (CourseCode == "S")
                return "SCM";
        }
        else if (Length == StrokeStringLength.TwoLetters)
        {
            if (CourseCode == "Y")
                return "Y";
            else if (CourseCode == "L" || CourseCode == "M")
                return "L";
            else if (CourseCode == "S")
                return "SCM";
        }
        return "";
    }

    public static String GetEventName(int Distance, int StrokeCode, String SexCode, String AgeCode, StrokeStringLength Length)
    {
        return GetAgeString(AgeCode) + " " + GetFullGender(SexCode) + " " + Distance + " " + GetStrokeString(StrokeCode, Length);
    }

    public static String GetEventName(int Distance, int StrokeCode, String Course, StrokeStringLength Length)
    {
        return Distance + " " + GetCourse(Course, Length) + " " + GetStrokeString(StrokeCode, Length);
    }
}