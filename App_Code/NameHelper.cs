using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NameHelper
/// </summary>
public static class NameHelper
{
    public static bool IsSecondNameAlphabeticallyAfterFirstName(
        string FirstLastName, string FirstFirstName, string FirstMiddleName,
        string SecondLastName, String SecondFirstName, string SecondMiddleName)
    {
        if (FirstLastName != SecondLastName)
            return StringHelper.IsSecondStringAlphabeticallyAfterFirstString(FirstLastName, SecondLastName);
        else
        {
            if (FirstFirstName != SecondFirstName)
                return StringHelper.IsSecondStringAlphabeticallyAfterFirstString(FirstFirstName, SecondFirstName);
            else
                return StringHelper.IsSecondStringAlphabeticallyAfterFirstString(FirstMiddleName, SecondMiddleName);
        }
    }

}