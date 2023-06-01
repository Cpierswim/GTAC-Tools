using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BestTimeRow
/// </summary>
public partial class SwimTeamDatabase
{
    public partial class BestTimesRow
    {
        public override string ToString()
        {
            String st = "";

            HyTekTime HT = new HyTekTime(this.Score, this.Course);


            return HT.ToString();
        }

        public HyTekTime Time
        {
            get
            {
                return new HyTekTime(this.Score, this.Course);
            }
        }
    }
}