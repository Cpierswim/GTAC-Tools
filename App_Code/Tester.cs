using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tester
/// </summary>
public class Tester
{
    private DateTime _setupTime;

    public DateTime SetupTime
    {
        get
        {
            if (_setupTime == null)
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            return this._setupTime;
        }
        set { this._setupTime = value; }
    }

    public Tester()
    {

    }

    public void RunTest()
    {
        GetNumberOfWeeksInMonth(new DateTime(2015, 2, 1));
        
        //bool breaker = true;
        //DateTime NextMonthWith4Weeks = DateTime.Now;
        //for(int i = 2016; breaker; i++)
        //    for (int j = 1; j <= 12; j++)
        //    {
        //        if (i == 2015 && j == 2)
        //            breaker = false;
        //        if (GetNumberOfWeeksInMonth(new DateTime(i, j, 1)) == 4)
        //        {
        //            NextMonthWith4Weeks = new DateTime(i, j, 1);
        //            breaker = false;
        //            j = 13;
        //        }
        //    }

    }

    private int GetNumberOfWeeksInMonth(DateTime Month)
    {
        DateTime StartDate = new DateTime(Month.Year, Month.Month, 1);
        int DaysInMonth = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);


        int WorkingOnDay = 1;

        switch (StartDate.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                WorkingOnDay -= 6;
                break;
            case DayOfWeek.Friday:
                WorkingOnDay -= 5;
                break;
            case DayOfWeek.Thursday:
                WorkingOnDay -= 4;
                break;
            case DayOfWeek.Wednesday:
                WorkingOnDay -= 3;
                break;
            case DayOfWeek.Tuesday:
                WorkingOnDay -= 2;
                break;
            case DayOfWeek.Monday:
                WorkingOnDay -= 1;
                break;
        }

        int NumberOfWeeks = 0;
        bool continueloop = true;
        while (continueloop)
        {
            int SundayofWeek = WorkingOnDay;
            int SaturdayofWeek = SundayofWeek + 6;
            if (SundayofWeek > DaysInMonth)
                continueloop = false;
            else
                NumberOfWeeks++;
            WorkingOnDay += 7;
        }

        return NumberOfWeeks;
    }
}