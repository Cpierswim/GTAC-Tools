using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_OME_GroupEventPicker : System.Web.UI.UserControl
{
    private int _CurrentGroupID;
    public int CurrentGroupID { get { return this._CurrentGroupID; } }

    private SwimTeamDatabase.EntriesV2DataTable Entries;
    private SwimTeamDatabase.SwimmersDataTable Swimmers;
    private List<int> AthleteList;
    private SwimmerAthleteJoinBLL JoinAdapter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateMeetPicker();
            PopulateTables();
        }
        else
        {
            object o = ViewState["AthleteTable"];
            if (o != null)
                AthleteTable = (Table)o;
        }
    }

    private void PopulateTables()
    {
        try
        {
            this._CurrentGroupID = int.Parse(Request.QueryString["Group"]);
        }
        catch (Exception)
        {
            this._CurrentGroupID = -1;
        }


        EntriesV2BLL EntriesAdapter = new EntriesV2BLL();
        this.Entries = EntriesAdapter.GetAllEntries();
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        this.Swimmers = SwimmersAdapter.GetSwimmers();
        this.AthleteList = new List<int>();
        for (int i = 0; i < Entries.Count; i++)
            if (!AthleteList.Contains(Entries[i].AthleteID))
                AthleteList.Add(Entries[i].AthleteID);

        this.JoinAdapter = new SwimmerAthleteJoinBLL();


        for (int i = 0; i < AthleteList.Count; i++)
        {
            String USAID = this.JoinAdapter.SwimmerAthleteDictionary[AthleteList[i]];
            SwimTeamDatabase.SwimmersRow Swimmer = Swimmers.NewSwimmersRow();
            for (int j = 0; j < Swimmers.Count; j++)
                if (Swimmers[j].USAID == USAID)
                {
                    Swimmer = Swimmers[j];
                    break;
                }

            if (Swimmer.GroupID == this.CurrentGroupID)
            {
                SwimmerEventPickerTable SwimmerTable = new SwimmerEventPickerTable(Swimmer);
                int AthelteID = JoinAdapter.AthelteSwimmerDictionary[Swimmer.USAID];

                for (int j = 0; j < Entries.Count; j++)
                    if (Entries[j].AthleteID == AthelteID)
                        SwimmerTable.Entries.Add(Entries[j]);

                SwimmerTable.CreateTableFromEvents();

                TableCell TempCell = new TableCell();
                TempCell.Controls.Add(SwimmerTable);
                TableRow TempRow = new TableRow();
                TempRow.Cells.Add(TempCell);
                AthleteTable.Rows.Add(TempRow);


            }
        }

        ViewState.Add("AthleteTable", AthleteTable);
    }

    private void PopulateMeetPicker()
    {

    }

    private class SwimmerEventPickerTable : Table
    {
        SwimTeamDatabase.SwimmersRow _Swimmer;
        public SwimTeamDatabase.SwimmersRow Swimmer { get { return this._Swimmer; } }

        List<SwimTeamDatabase.EntriesV2Row> _Entries;
        public List<SwimTeamDatabase.EntriesV2Row> Entries
        {
            get
            {
                if (this._Entries == null)
                    this._Entries = new List<SwimTeamDatabase.EntriesV2Row>();

                return this._Entries;
            }
        }

        public SwimmerEventPickerTable(SwimTeamDatabase.SwimmersRow Swimmer)
        {
            this._Swimmer = Swimmer;
        }

        public void CreateTableFromEvents()
        {
            TableRow NameRow = new TableRow();
            Label NameLabel = new Label();
            BirthdayHelper Bdayhelper = new BirthdayHelper(Swimmer.Birthday);

            DateTime TEMPMEETDATE = new DateTime(2011, 6, 24);

            NameLabel.Text = Swimmer.PreferredName + " " + Swimmer.LastName + " Age at meet: " +
                Bdayhelper.AgeOnDate(TEMPMEETDATE);
            TableCell NameCell = new TableCell();
            NameCell.Controls.Add(NameLabel);
            NameRow.Cells.Add(NameCell);

            this.Rows.Add(NameRow);

        }

    }
}