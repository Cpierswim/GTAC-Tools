using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StringHelper
/// </summary>
public static class StringHelper
{
    public static bool IsSecondStringAlphabeticallyAfterFirstString(String FirstString, String SecondString)
    {
        char[] FirstStringArray = FirstString.Trim().ToUpper().ToCharArray();
        char[] SecondStringArray = SecondString.Trim().ToUpper().ToCharArray();

        int SmallerString = 0;
        if (FirstStringArray.Length < SecondStringArray.Length)
            SmallerString = FirstStringArray.Length;
        else
            SmallerString = SecondStringArray.Length;

        for (int i = 0; i < SmallerString; i++)
        {
            if (FirstStringArray[i] != SecondStringArray[i])
            {
                if (IsSecondCharAlphabeticallyAfterFirstString(FirstStringArray[i], SecondStringArray[i]))
                    return true;
                else
                    return false;
            }
        }

        if (SecondStringArray.Length >= FirstStringArray.Length)
            return true;
        else
            return false;
    }

    private static bool IsSecondCharAlphabeticallyAfterFirstString(char FirstChar, char SecondChar)
    {
        if (FirstChar >= SecondChar)
            return false;

        return true;
    }
}