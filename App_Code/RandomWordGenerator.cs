using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

/// <summary>
/// Summary description for RandomWordGenerator
/// </summary>
public static class RandomWordGenerator
{
    private const String StringToLookFor = "<span id=\"tmpl_main_lblWord\" class=\"randomWord\">";
    private const String RandomWordGeneratorLocation = "http://www.watchout4snakes.com/CreativityTools/RandomWord/RandomWord.aspx";

    public static String GetRandomWord()
    {
        // used to build entire input
        StringBuilder sb = new StringBuilder();

        // used on each read operation
        byte[] buf = new byte[8192];

        // prepare the web page we will be asking for
        HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(RandomWordGeneratorLocation);


        HttpWebResponse response = (HttpWebResponse)((HttpWebRequest)WebRequest.Create("http://www.weather.com")).GetResponse();
        try
        {
            WebResponse respnser = request.GetResponse();

            // execute the request
            response = (HttpWebResponse)
                request.GetResponse();
        }
        catch(WebException e) 
        {
            Console.WriteLine("This program is expected to throw WebException on successful run."+
                        "\n\nException Message :" + e.Message);
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
            }
    }

        // we will read data via the response stream
        Stream resStream = response.GetResponseStream();

        string tempString = null;
        int count = 0;

        do
        {
            // fill the buffer with data
            count = resStream.Read(buf, 0, buf.Length);

            // make sure we read some data
            if (count != 0)
            {
                // translate from bytes to ASCII text
                tempString = Encoding.ASCII.GetString(buf, 0, count);

                // continue building the string
                sb.Append(tempString);
            }

            if (tempString.Contains(StringToLookFor))
                count = 0;
        }
        while (count > 0); // any more data to read?

        int startindex = tempString.IndexOf(StringToLookFor);
        if (startindex == -1)
            return RandomWordGenerator.TraditionalRandomWordGenerator();
        startindex = startindex + StringToLookFor.Length;

        String Partial = tempString.Substring(startindex);
        int LengthofWord = Partial.IndexOf("<");
        Partial = Partial.Substring(0, LengthofWord);



        return Partial;
    }

    private static String TraditionalRandomWordGenerator()
    {
        List<String> PasswordParts = new List<string>();
        PasswordParts.Add("Decorum");
        PasswordParts.Add("Orderly");
        PasswordParts.Add("Bricklayer");
        PasswordParts.Add("Android");
        PasswordParts.Add("Fledgling");
        PasswordParts.Add("Population");
        PasswordParts.Add("Pancreas");
        PasswordParts.Add("Gramophone");
        PasswordParts.Add("Garment");
        PasswordParts.Add("Healer");
        PasswordParts.Add("Sneer");
        PasswordParts.Add("Probation");
        PasswordParts.Add("Furrow");
        PasswordParts.Add("Nuance");
        PasswordParts.Add("Classmate");
        PasswordParts.Add("Bewilderment");
        PasswordParts.Add("Spotting");
        PasswordParts.Add("Seeing");
        PasswordParts.Add("Incline");
        PasswordParts.Add("Factor");
        PasswordParts.Add("Settle");
        PasswordParts.Add("Damage");
        PasswordParts.Add("Season");
        PasswordParts.Add("Here");
        PasswordParts.Add("Passing");
        PasswordParts.Add("Possible");
        PasswordParts.Add("Bite");
        PasswordParts.Add("Dying");
        PasswordParts.Add("Wire");
        PasswordParts.Add("Guy");
        PasswordParts.Add("Role");
        PasswordParts.Add("Worrying");
        PasswordParts.Add("Sentence");
        PasswordParts.Add("Hide");
        PasswordParts.Add("Selection");
        PasswordParts.Add("Structure");
        PasswordParts.Add("Button");
        PasswordParts.Add("Term");
        PasswordParts.Add("Incompatible");
        PasswordParts.Add("Content");
        PasswordParts.Add("Connected");
        PasswordParts.Add("Largest");
        PasswordParts.Add("Illegal");
        PasswordParts.Add("Many");
        PasswordParts.Add("Scientific");
        PasswordParts.Add("Raw");
        PasswordParts.Add("Basis");
        PasswordParts.Add("Tailors");
        PasswordParts.Add("Steady");
        PasswordParts.Add("Surface");
        PasswordParts.Add("Curse");
        PasswordParts.Add("Burns");
        PasswordParts.Add("How");
        PasswordParts.Add("Will");
        PasswordParts.Add("Infrequent");
        PasswordParts.Add("Convenience");
        PasswordParts.Add("Enter");
        PasswordParts.Add("Enabling");
        PasswordParts.Add("Aardvark");
        PasswordParts.Add("Will");
        PasswordParts.Add("Unknown");
        PasswordParts.Add("Problem");
        PasswordParts.Add("Display");
        PasswordParts.Add("Bomb");
        PasswordParts.Add("Hardship");
        PasswordParts.Add("Collapses");
        PasswordParts.Add("Ally");
        PasswordParts.Add("Motor");
        PasswordParts.Add("Wading");
        PasswordParts.Add("Police");
        PasswordParts.Add("Nuts");
        PasswordParts.Add("Cuckoo");
        PasswordParts.Add("Pardon");
        PasswordParts.Add("Damnation");
        PasswordParts.Add("Content");
        PasswordParts.Add("Inside");
        PasswordParts.Add("After");
        PasswordParts.Add("Round");
        PasswordParts.Add("Ripped");
        PasswordParts.Add("Adventure");
        PasswordParts.Add("Register");
        PasswordParts.Add("Arrive");
        PasswordParts.Add("Default");
        PasswordParts.Add("Sell");
        PasswordParts.Add("Joking");
        PasswordParts.Add("Expire");
        PasswordParts.Add("Scan");
        PasswordParts.Add("Golf");
        PasswordParts.Add("Believed");
        PasswordParts.Add("Stage");
        PasswordParts.Add("Learn");
        PasswordParts.Add("Typed");
        PasswordParts.Add("Collating");
        PasswordParts.Add("Duplicating");
        PasswordParts.Add("Deleted");
        PasswordParts.Add("Budget");
        PasswordParts.Add("Declined");
        PasswordParts.Add("Demolish");
        PasswordParts.Add("Surname");
        PasswordParts.Add("Joint");
        PasswordParts.Add("Dispute");
        PasswordParts.Add("Spare");
        PasswordParts.Add("Lined");
        PasswordParts.Add("Utter");
        PasswordParts.Add("Bring");
        PasswordParts.Add("Knight");
        PasswordParts.Add("Cupboard");
        PasswordParts.Add("Carrot");
        PasswordParts.Add("Assist");
        PasswordParts.Add("Gasoline");
        PasswordParts.Add("Hanging");
        PasswordParts.Add("Society");
        PasswordParts.Add("Smoke");
        PasswordParts.Add("Competence");
        PasswordParts.Add("Timer");
        PasswordParts.Add("Perceiving");
        PasswordParts.Add("Raving");
        PasswordParts.Add("Flat");
        PasswordParts.Add("Bell");
        PasswordParts.Add("Dependent");
        PasswordParts.Add("Pronoun");
        PasswordParts.Add("Rhythm");
        PasswordParts.Add("Horse");
        PasswordParts.Add("Shouting");
        PasswordParts.Add("Finance");
        PasswordParts.Add("Cabinet");
        PasswordParts.Add("Rhyme");
        PasswordParts.Add("Postulate");
        PasswordParts.Add("Horde");
        PasswordParts.Add("Photograph");
        PasswordParts.Add("Pressing");
        PasswordParts.Add("Calculator");
        PasswordParts.Add("Integral");
        PasswordParts.Add("Try");
        PasswordParts.Add("Spit");
        PasswordParts.Add("Deduction");
        PasswordParts.Add("Biologist");
        PasswordParts.Add("Lasting");
        PasswordParts.Add("Command");
        PasswordParts.Add("Knowing");
        PasswordParts.Add("Gas");
        PasswordParts.Add("Bake");
        PasswordParts.Add("Baking");
        PasswordParts.Add("Baked");
        PasswordParts.Add("Gassed");
        PasswordParts.Add("Finished");
        PasswordParts.Add("Transmitter");
        PasswordParts.Add("Sacrifice");
        PasswordParts.Add("Climbing");
        PasswordParts.Add("Hydrogen");

        Random rand = new Random();

        return PasswordParts[rand.Next(PasswordParts.Count)];
    }
}