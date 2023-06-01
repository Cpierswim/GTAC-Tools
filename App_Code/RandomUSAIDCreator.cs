using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RandomUSAIDCreator
/// </summary>
public static class RandomUSAIDCreator
{
    public static String GetRandomUSAID()
    {
        String USAID = "";
        Random rand = new Random();

        USAID = rand.Next(13, 99).ToString();
        USAID = USAID + rand.Next(32, 99).ToString();
        USAID = USAID + rand.Next(0, 99).ToString();

        List<String> Letters = new List<string>();

        Letters.Add("A");
        Letters.Add("B");
        Letters.Add("C");
        Letters.Add("D");
        Letters.Add("E");
        Letters.Add("F");
        Letters.Add("G");
        Letters.Add("H");
        Letters.Add("I");
        Letters.Add("J");
        Letters.Add("K");
        Letters.Add("L");
        Letters.Add("M");
        Letters.Add("N");
        Letters.Add("O");
        Letters.Add("P");
        Letters.Add("Q");
        Letters.Add("R");
        Letters.Add("S");
        Letters.Add("T");
        Letters.Add("U");
        Letters.Add("V");
        Letters.Add("W");
        Letters.Add("X");
        Letters.Add("Y");
        Letters.Add("Z");

        for (int i = 0; i < 8; i++)
            USAID = USAID + Letters[rand.Next(0, 25)];

        return USAID;
    }
    public static String GetRandomUSAID(int seed)
    {
        String USAID = "";
        Random rand = new Random(seed);

        USAID = rand.Next(13, 99).ToString();
        USAID = USAID + rand.Next(32, 99).ToString();
        USAID = USAID + rand.Next(0, 99).ToString();

        List<String> Letters = new List<string>();

        Letters.Add("A");
        Letters.Add("B");
        Letters.Add("C");
        Letters.Add("D");
        Letters.Add("E");
        Letters.Add("F");
        Letters.Add("G");
        Letters.Add("H");
        Letters.Add("I");
        Letters.Add("J");
        Letters.Add("K");
        Letters.Add("L");
        Letters.Add("M");
        Letters.Add("N");
        Letters.Add("O");
        Letters.Add("P");
        Letters.Add("Q");
        Letters.Add("R");
        Letters.Add("S");
        Letters.Add("T");
        Letters.Add("U");
        Letters.Add("V");
        Letters.Add("W");
        Letters.Add("X");
        Letters.Add("Y");
        Letters.Add("Z");

        for (int i = 0; i < 8; i++)
            USAID = USAID + Letters[rand.Next(0, 25)];

        return USAID;
    }
}