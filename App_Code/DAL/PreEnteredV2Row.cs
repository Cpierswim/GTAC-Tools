using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PreEnteredV2Row
/// </summary>
public partial class SwimTeamDatabase
{
    public partial class PreEnteredV2Row
    {
        public bool IsPreEnteredInSession(int SessionNumber)
        {
            if(!this.PreEntered)
                return false;
            if (!this.IsIndividualSessionsDeclared())
                return false;

            String SessionsString = "";
            if (!this.IsSession1Null())
                if (this.Session1 == SessionNumber)
                    return true;
            if (!this.IsSession2Null())
                if (this.Session2 == SessionNumber)
                    return true;
            if (!this.IsSession3Null())
                if (this.Session3 == SessionNumber)
                    return true;
            if (!this.IsSession4Null())
                if (this.Session4 == SessionNumber)
                    return true;
            if (!this.IsSession5Null())
                if (this.Session5 == SessionNumber)
                    return true;
            if (!this.IsSession6Null())
                if (this.Session6 == SessionNumber)
                    return true;
            if (!this.IsSession7Null())
                if (this.Session7 == SessionNumber)
                    return true;
            if (!this.IsSession8Null())
                if (this.Session8 == SessionNumber)
                    return true;
            if (!this.IsSession9Null())
                if (this.Session9 == SessionNumber)
                    return true;
            if (!this.IsSession10Null())
                if (this.Session10 == SessionNumber)
                    return true;
            return false;
        }

        public bool IsIndividualSessionsDeclared()
        {
            return !(this.IsSession1Null() &&
                this.IsSession2Null() &&
                this.IsSession3Null() &&
                this.IsSession4Null() &&
                this.IsSession5Null() &&
                this.IsSession6Null() &&
                this.IsSession7Null() &&
                this.IsSession8Null() &&
                this.IsSession9Null() &&
                this.IsSession10Null());
        }

        public int? MaxSessionNumberPreEnteredIn
        {
            get
            {
                int i = -1;
                if (!this.IsSession1Null())
                    if (this.Session1 > i)
                        i = Session1;
                if (!this.IsSession2Null())
                    if (this.Session2 > i)
                        i = Session2;
                if (!this.IsSession3Null())
                    if (this.Session3 > i)
                        i = Session3;
                if (!this.IsSession4Null())
                    if (this.Session4 > i)
                        i = Session4;
                if (!this.IsSession5Null())
                    if (this.Session5 > i)
                        i = Session5;
                if (!this.IsSession6Null())
                    if (this.Session6 > i)
                        i = Session6;
                if (!this.IsSession7Null())
                    if (this.Session7 > i)
                        i = Session7;
                if (!this.IsSession8Null())
                    if (this.Session8 > i)
                        i = Session8;
                if (!this.IsSession9Null())
                    if (this.Session9 > i)
                        i = Session9;
                if (!this.IsSession10Null())
                    if (this.Session10 > i)
                        i = Session10;
                if (i != -1)
                    return i;
                else
                    return null;
            }
        }
    }
}