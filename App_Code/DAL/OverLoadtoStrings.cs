using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class SwimTeamDatabase
{
    public partial class MeetEventsRow
    {
        public override string ToString()
        {
            int MinAge = MeetEventHelper.GetMinAgeFromAgeString(this.AgeCode.ToString());
            int MaxAge = MeetEventHelper.GetMaxAgeFromAgeString(this.AgeCode.ToString());

            String st = "";

            if (MinAge == 0 && MaxAge == 99)
                st = "Open";
            else if (MinAge == 0 && MaxAge > 0)
                st = MaxAge + " and Under";
            else if (MaxAge == 99 && MinAge < 99)
                st = MinAge + " and Over";
            else
                st = MinAge + "-" + MaxAge;

            if (this.SexCode.ToUpper() == "F")
                st += " Girls ";
            else
                st += " Boys ";

            st += this.Distance;
            if (this.Course.ToUpper() == "Y")
                st += " Yard";
            else if (this.Course.ToUpper() == "L")
                st += " Meter";
            else
                st += " SCM";

            switch (this.StrokeCode)
            {
                case 1:
                    st += " Free";
                    break;
                case 2:
                    st += " Back";
                    break;
                case 3:
                    st += " Breast";
                    break;
                case 4:
                    st += " Fly";
                    break;
                case 5:
                    st += " IM";
                    break;
            }

            return st;
        }
    }
    
}