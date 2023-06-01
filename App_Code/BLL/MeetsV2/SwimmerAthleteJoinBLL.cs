using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SwimTeamDatabaseTableAdapters;

/// <summary>
/// Summary description for SwimmerAthleteJoinBLL
/// </summary>
public class SwimmerAthleteJoinBLL
{
    private SwimmerAthleteJoinTableAdapter SwimmerAthleteJoinTableAdapter;
    protected SwimmerAthleteJoinTableAdapter Adapter
    {
        get
        {
            if (this.SwimmerAthleteJoinTableAdapter == null)
                this.SwimmerAthleteJoinTableAdapter = new SwimmerAthleteJoinTableAdapter();
            return this.SwimmerAthleteJoinTableAdapter;
        }
    }

    public SwimTeamDatabase.SwimmerAthleteJoinDataTable GetAllJoins()
    {
        return Adapter.GetAllJoins();
    }


    private Dictionary<int, String> _swimmerAthleteDictionary;
    public Dictionary<int, String> SwimmerAthleteDictionary
    {
        get
        {
            if (_swimmerAthleteDictionary == null)
                this.PopulateDictionaries();
            

            return _swimmerAthleteDictionary;
        }
    }

    private Dictionary<String, int> _AthelteSwimmerDictionary;
    public Dictionary<String, int> AthelteSwimmerDictionary
    {
        get
        {
            if (_AthelteSwimmerDictionary == null)
                this.PopulateDictionaries();

            return _AthelteSwimmerDictionary;
        }
    }

    private void PopulateDictionaries()
    {
        if ((_swimmerAthleteDictionary == null) || (_AthelteSwimmerDictionary == null))
        {
            SwimTeamDatabase.SwimmerAthleteJoinDataTable Joins = Adapter.GetAllJoins();
            if (_swimmerAthleteDictionary == null)
            {
                _swimmerAthleteDictionary = new Dictionary<int, string>();

                for (int i = 0; i < Joins.Count; i++)
                    _swimmerAthleteDictionary.Add(Joins[i].HyTekAthleteID, Joins[i].SwimmerID);
            }

            if (_AthelteSwimmerDictionary == null)
            {
                _AthelteSwimmerDictionary = new Dictionary<string, int>();
                for (int i = 0; i < Joins.Count; i++)
                    _AthelteSwimmerDictionary.Add(Joins[i].SwimmerID, Joins[i].HyTekAthleteID);
            }
        }
    }

    public int GetAtheleIDFromDatabase(String USAID)
    {
        return this.Adapter.GetSwimmerAthleteID(USAID) ?? -1;
    }
}