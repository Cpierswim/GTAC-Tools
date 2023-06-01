using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class UserControls_RecordsDisplay_RecordsDisplay : System.Web.UI.UserControl
{
    private static Color HeaderBackgroudColor = Color.FromArgb(43, 45, 137);

    private DateTime _highlightSince;
    public DateTime HighlightSince
    {
        get
        {
            if (this._highlightSince == null)
                return DateTime.MaxValue;
            else return this._highlightSince;
        }
        set { this._highlightSince = value; }
    }

    public UserControls_RecordsDisplay_RecordsDisplay()
    {
        this.Init += new EventHandler(Page_Init);
    }

    private void Page_Init(object sender, System.EventArgs e)
    {
        this.EnableViewState = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        bool PageDisplayable = true;

        String Sex = string.Empty, Course = string.Empty, Max = "x", Min = "x";
        Object o;
        o = Request.QueryString["S"];
        if (o != null)
            Sex = o.ToString();
        else
            PageDisplayable = false;
        o = Request.QueryString["C"];
        if (o != null)
            Course = o.ToString();
        else
            PageDisplayable = false;
        o = Request.QueryString["A"];
        if (o != null)
            Max = o.ToString();
        else
            PageDisplayable = false;
        o = Request.QueryString["I"];
        if (o != null)
            Min = o.ToString();
        else
            PageDisplayable = false;

        byte MaxAge = 0, MinAge = 0;

        if (!byte.TryParse(Max, out MaxAge))
            PageDisplayable = false;
        if (!byte.TryParse(Min, out MinAge))
            PageDisplayable = false;

        if (PageDisplayable)
        {
            bool LabelSet = SetPageLabel(MaxAge, MinAge, Course, Sex);
            if (LabelSet)
            {
                Image1.Visible = true;

                SettingsBLL SettingsAdapter = new SettingsBLL();
                this.HighlightSince = SettingsAdapter.GetRegistrationStartDate();

                RecordsBLL RecordsAdapter = new RecordsBLL();
                SwimTeamDatabase.RecordsDataTable AgeGroupRecords = RecordsAdapter.GetRecordsForRecordsPage(MinAge, MaxAge, Course, Sex);

                List<EventRecordList> Events = SplitResultsIntoEventRecordList(AgeGroupRecords, Course);
                DisplayEventsWithRecords(Events);
            }
            else
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "There was an error in the link to this page.";
            }
        }
        else
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "There was an error in the link to this page.";
        }
    }

    private List<EventRecordList> SplitResultsIntoEventRecordList(SwimTeamDatabase.RecordsDataTable AgeGroupRecords, String Course)
    {
        List<EventRecordList> Events = new List<EventRecordList>();
        if (Course == "y")
            Events.Add(new EventRecordList(25, 1));
        Events.Add(new EventRecordList(50, 1));
        Events.Add(new EventRecordList(100, 1));
        Events.Add(new EventRecordList(200, 1));
        if (Course == "y")
            Events.Add(new EventRecordList(500, 1));
        else
            Events.Add(new EventRecordList(400, 1));
        if (Course == "y")
            Events.Add(new EventRecordList(1000, 1));
        else
            Events.Add(new EventRecordList(800, 1));
        if (Course == "y")
            Events.Add(new EventRecordList(1650, 1));
        else
            Events.Add(new EventRecordList(1500, 1));
        if (Course == "y")
            Events.Add(new EventRecordList(25, 2));
        Events.Add(new EventRecordList(50, 2));
        Events.Add(new EventRecordList(100, 2));
        Events.Add(new EventRecordList(200, 2));
        if (Course == "y")
            Events.Add(new EventRecordList(25, 3));
        Events.Add(new EventRecordList(50, 3));
        Events.Add(new EventRecordList(100, 3));
        Events.Add(new EventRecordList(200, 3));
        if (Course == "y")
            Events.Add(new EventRecordList(25, 4));
        Events.Add(new EventRecordList(50, 4));
        Events.Add(new EventRecordList(100, 4));
        Events.Add(new EventRecordList(200, 4));
        if (Course == "y")
            Events.Add(new EventRecordList(100, 5));
        Events.Add(new EventRecordList(200, 5));
        Events.Add(new EventRecordList(400, 5));

        for (int i = 0; i < AgeGroupRecords.Count; i++)
        {
            for (int j = 0; j < Events.Count; j++)
            {
                if (Events[j].Stroke == AgeGroupRecords[i].Stroke &&
                    Events[j].Distance == AgeGroupRecords[i].Distance)
                {
                    Events[j].AddRecord(AgeGroupRecords[i].Last, AgeGroupRecords[i].First, AgeGroupRecords[i].Preferred,
                        AgeGroupRecords[i].Time.ToString(), AgeGroupRecords[i].Date, AgeGroupRecords[i].MeetName, HighlightSince,
                        AgeGroupRecords[i].RecordID);
                    j = Events.Count;
                }
            }
        }


        return Events;
    }

    private class EventRecordList
    {
        private int _distance;
        public int Distance { get { return this._distance; } }
        private int _stroke;
        public int Stroke { get { return this._stroke; } }

        private List<String> _nameList;
        public List<String> NameList { get { return this._nameList; } }

        private List<String> _timeList;
        public List<String> TimeList { get { return this._timeList; } }

        private List<String> _dateList;
        public List<String> DateList { get { return this._dateList; } }

        private List<String> _MeetNameList;
        public List<String> MeetNameList { get { return this._MeetNameList; } }

        private List<bool> _highlightRecordList;
        public List<bool> HighlightRecordList { get { return this._highlightRecordList; } }

        public EventRecordList(int Distance, int Stroke)
        {
            this._distance = Distance;
            this._stroke = Stroke;
            this._nameList = new List<string>();
            this._timeList = new List<string>();
            this._dateList = new List<string>();
            this._MeetNameList = new List<string>();
            this._highlightRecordList = new List<bool>();
        }

        public void AddRecord(String Last, String First, String Preferred, String Time, DateTime Date, String MeetName,
            DateTime HighlightSince, int RecordID)
        {
            if (First.Length == 1)
                First = First.ToUpper();
            if (Last == "Mccann")
                Last = "McCann";
            String Name = "";
            if (string.IsNullOrEmpty(Preferred))
            {
                if (First.Length != 1)
                    Name = First;
                else
                    if (First.Contains("."))
                        Name = First;
                    else
                        Name = First + ".";
            }
            else
            {
                if (First.Length != 1)
                    Name = Preferred;
                else
                    if (First.Contains("."))
                        Name = Preferred;
                    else
                        Name = Preferred + ".";
            }
            Name += " " + Last;

            this._nameList.Add(Name);

            if (Time.Length == 0)
                Time = "00";
            else if (Time.Length == 1)
                Time = "0" + Time;

            String hundreths = Time.Substring(Time.Length - 2, 2);
            String SecondsAsString = Time.Substring(0, Time.Length - 2);
            int seconds = 0;
            if (!String.IsNullOrEmpty(SecondsAsString))
                seconds = int.Parse(SecondsAsString);
            int minutes = seconds / 60;
            seconds = seconds % 60;
            int hours = minutes / 60;
            minutes = minutes % 60;

            String TimeAsString = "";
            if (hours > 0)
            {
                TimeAsString += hours + ":";
                if (minutes < 10)
                    TimeAsString += "0";
                TimeAsString += minutes + ":";
                if (seconds < 10)
                    TimeAsString += "0";
                TimeAsString += seconds + "." + hundreths;
            }
            else if (minutes > 0)
            {
                TimeAsString += minutes + ":";
                if (seconds < 10)
                    TimeAsString += "0";
                TimeAsString += seconds + "." + hundreths;
            }
            else
            {
                TimeAsString += seconds + "." + hundreths;
            }

            this._timeList.Add(TimeAsString);

            String DateAsString = Date.Month + "/" + Date.Year.ToString().Substring(2, 2);
            DateAsString = SetDateString(DateAsString, RecordID, TimeAsString, this.Stroke, this.Distance, Last);
            this._dateList.Add(DateAsString);

            this._MeetNameList.Add(MeetName);

            if (Date >= HighlightSince)
                this._highlightRecordList.Add(true);
            else
                this._highlightRecordList.Add(false);
        }

        private String SetDateString(String DateAsString, int RecordID,
            String TimeAsString, int stroke, int distance, String Last)
        {
            if (DateAsString != "1/90")
                return DateAsString;
            else
            {
                switch (RecordID)
                {
                    case 90656:
                    case 90657:
                        return "1/83";
                    case 90658:
                        return "2/83";
                    case 90631:
                        return "3/86";
                    case 90638:
                    case 90637:
                        return "2/89";
                    case 90671:
                    case 90676:
                    case 90674:
                    case 90672:
                    case 90673:
                    case 90675:
                        return "1/93";
                    case 90626:
                        return "2/93";
                    case 90597:
                    case 90601:
                    case 90598:
                    case 90599:
                    case 90602:
                        return "1/94";
                    case 90680:
                    case 90687:
                    case 90685:
                    case 90682:
                    case 90686:
                        return "3/96";
                    case 90695:
                        return "12/96";
                    case 90643:
                    case 90612:
                    case 90700:
                        return "1/97";
                    case 90693:
                    case 90692:
                    case 90609:
                    case 90606:
                        return "2/97";
                    case 90614:
                    case 90699:
                    case 90610:
                    case 90604:
                    case 90615:
                    case 90608:
                    case 90694:
                    case 90697:
                    case 90611:
                    case 90644:
                    case 90696:
                    case 90605:
                    case 90613:
                    case 90640:
                    case 90698:
                    case 90645:
                    case 90603:
                    case 90607:
                        return "3/97";
                    case 90630:
                        return "11/00";
                    case 90705:
                        return "2/06";
                    case 90670:
                        return "1/87";
                    case 90649:
                        return "11/96";
                    case 90663:
                    case 90664:
                    case 90661:
                    case 90665:
                    case 90662:
                    case 90666:
                        return "1/95";
                    case 90600:
                        return "12/93";
                    case 90635:
                    case 90632:
                    case 90633:
                    case 90636:
                        return "1/86";
                    case 90651:
                    case 90650:
                    case 90653:
                    case 90652:
                    case 90654:
                        return "3/97";
                    case 90681:
                    case 90688:
                        return "2/96";
                    case 90627:
                    case 90620:
                    case 90628:
                        return "8/93";
                    case 90703:
                    case 91736:
                        return "12/05";
                    case 90704:
                        return "11/05";
                    case 90634:
                        return "1986";
                    case 90793:
                        return "12/94";
                    case 90721:
                    case 90722:
                        return "2/89";
                    case 90773:
                        return "11/94";
                    case 90798:
                    case 90796:
                    case 90797:
                    case 90794:
                    case 90799:
                    case 90795:
                        return "1/95";
                    case 90767:
                    case 90769:
                    case 90765:
                    case 90771:
                    case 90768:
                    case 90770:
                    case 90766:
                    case 90772:
                        return "1991";
                    case 90762:
                    case 90763:
                        return "3/99";
                    case 90774:
                        return "3/94";
                    case 90745:
                    case 90744:
                        return "3/97";
                    case 90800:
                        return "11/95";
                    case 90807:
                        return "1/05";
                    case 90804:
                        return "11/05";
                    case 90805:
                        return "10/05";
                    case 90806:
                        return "11/08";
                    case 90709:
                    case 90719:
                    case 90756:
                    case 90750:
                    case 90752:
                    case 90748:
                    case 90753:
                    case 90718:
                        return "11/95";
                    case 90740:
                    case 90737:
                        return "3/95";
                    case 90712:
                    case 90717:
                        return "1/96";
                    case 90734:
                    case 90736:
                    case 90754:
                    case 90735:
                    case 90733:
                    case 90739:
                        return "1/95";
                    case 90786:
                        return "12/96";
                    case 90714:
                    case 90716:
                    case 90713:
                    case 90715:
                        return "12/95";
                    case 90729:
                    case 90730:
                        return "3/97";
                    case 90710:
                    case 90711:
                        return "1/96";
                    case 90757:
                    case 90759:
                        return "2/88";
                    case 90751:
                    case 90738:
                        return "1995";
                    case 90789:
                    case 90788:
                        return "1/97";
                    case 90725:
                    case 90707:
                        return "3/98";
                    case 90787:
                    case 91735:
                        return "11/96";
                    case 90758:
                        return "3/88";
                    case 90749:
                        return "11/94";
                    case 90706:
                        return "&nbsp;";
                    case 90755:
                        return "3/95";
                    case 91733:
                        return "11/08";
                    case 90882:
                    case 90870:
                    case 90875:
                    case 90886:
                    case 90889:
                    case 90873:
                    case 90819:
                    case 90883:
                    case 90876:
                    case 90887:
                    case 90874:
                    case 90890:
                    case 90878:
                    case 90820:
                    case 90884:
                    case 90891:
                    case 90885:
                        return "3/97";
                    case 90942:
                        return "11/93";
                    case 90899:
                    case 90896:
                    case 90901:
                    case 90850:
                        return "1/96";
                    case 90937:
                    case 90938:
                        return "3/98";
                    case 90915:
                        return "12/94";
                    case 90903:
                        return "1/94";
                    case 90871:
                    case 90888:
                    case 90877:
                    case 90881:
                        return "1/97";
                    case 90904:
                        return "12/93";
                    case 90898:
                    case 90853:
                    case 90846:
                        return "2/96";
                    case 90872:
                        return "2/97";
                    case 90879:
                        return "12/96";
                    case 90951:
                    case 90907:
                    case 90949:
                    case 90952:
                    case 90868:
                        return "12/00";
                    case 90941:
                        return "11/99";
                    case 90851:
                    case 90854:
                        return "11/95";
                    case 90880:
                    case 90821:
                    case 90892:
                        return "11/96";
                    case 90906:
                    case 90908:
                        return "10/00";
                    case 90847:
                        return "12/95";
                    case 90936:
                    case 90948:
                    case 90945:
                        return "11/01";
                    case 90856:
                    case 90855:
                    case 90860:
                    case 90857:
                    case 90861:
                    case 90864:
                    case 90858:
                    case 90865:
                    case 90859:
                        return "3/91";
                    case 90911:
                    case 90912:
                    case 90914:
                        return "11/93";
                    case 90913:
                        return "12/93";
                    case 90920:
                        return "11/96";
                    case 90862:
                    case 90863:
                        return "1991";
                    case 90956:
                        return "11/94";
                    case 90928:
                    case 90932:
                    case 90930:
                    case 90931:
                    case 90929:
                    case 90933:
                        return "2/91";
                    case 90953:
                    case 90954:
                        return "&nbsp;";
                    case 90921:
                        return "12/95";
                    case 90910:
                        return "1/94";
                    case 91732:
                        return "2/06";
                    case 90814:
                        return "3/98";
                    case 90812:
                    case 90811:
                    case 90840:
                    case 90917:
                        return "11/01";
                    case 90832:
                        return "2/96";
                    case 90835:
                        return "1/95";
                    case 90927:
                        return "2/91";
                    case 90830:
                        return "1/96";
                    case 91002:
                    case 91007:
                        return "1/88";
                    case 91012:
                    case 91013:
                    case 91009:
                    case 91015:
                    case 91017:
                    case 91016:
                    case 91008:
                        return "1992";
                    case 91067:
                    case 91070:
                    case 91068:
                    case 91075:
                        return "11/95";
                    case 90966:
                    case 90968:
                    case 90969:
                        return "3/98";
                    case 91082:
                        return "11/92";
                    case 91033:
                    case 91032:
                    case 91035:
                        return "1/95";
                    case 90987:
                    case 90992:
                    case 90988:
                    case 90990:
                    case 90984:
                    case 90985:
                    case 90986:
                        return "3/97";
                    case 90980:
                        return "11/97";
                    case 91056:
                    case 91064:
                        return "1/96";
                    case 91003:
                    case 91004:
                    case 91005:
                    case 91006:
                        return "3/88";
                    case 91010:
                    case 91011:
                        return "3/92";
                    case 91034:
                        return "1995";
                    case 91059:
                    case 91057:
                    case 91060:
                    case 91053:
                        return "12/95";
                    case 90981:
                    case 90983:
                        return "11/98";
                    case 91071:
                    case 91073:
                        return "11/95";
                    case 91080:
                        return "11/96";
                    case 90970:
                        return "3/98";
                    case 91014:
                        return "1/92";
                    case 91030:
                        return "1/95";
                    case 91078:
                    case 91079:
                        return "11/01";
                    case 91058:
                        return "1/96";
                    case 91086:
                    case 91087:
                        return "1995";
                    case 91025:
                    case 91088:
                    case 91092:
                    case 91090:
                    case 91105:
                    case 91091:
                    case 91094:
                    case 91095:
                    case 90994:
                    case 91100:
                    case 91001:
                    case 90995:
                        return "3/97";
                    case 91043:
                    case 91023:
                    case 91018:
                    case 91046:
                    case 91049:
                    case 90997:
                    case 91048:
                    case 91098:
                    case 91045:
                    case 91024:
                    case 91047:
                    case 91020:
                    case 91019:
                    case 91051:
                    case 91737:
                    case 90999:
                    case 90998:
                    case 91021:
                    case 91052:
                    case 91101:
                    case 91000:
                    case 91099:
                    case 91022:
                        return "3/98";
                    case 91039:
                    case 91041:
                    case 91040:
                    case 91042:
                        return "2/81";
                    case 91044:
                    case 91050:
                        return "11/97";
                    case 90974:
                    case 90972:
                    case 90975:
                    case 90973:
                    case 90976:
                    case 90977:
                        return "&nbsp;";
                    case 91104:
                    case 91103:
                        return "1/95";
                    case 91027:
                        return "3/95";
                    case 91734:
                        return "3/96";
                    case 90971:
                        return "11/92";
                    case 90996:
                        return "1/97";
                    case 91093:
                        return "2/97";
                    case 91028:
                        return "11/94";
                    case 91077:
                        return "1/96";
                    case 91029:
                        return "12/95";
                    case 91201:
                    case 91203:
                        return "2/97";
                    case 91738:
                    case 91169:
                    case 91176:
                    case 91122:
                        return "2/99";
                    case 91204:
                        return "3/09";
                    case 91180:
                    case 91181:
                        return "2/04";
                    case 91197:
                    case 91175:
                    case 91115:
                    case 91173:
                    case 91112:
                    case 91113:
                        return "3/97";
                    case 91182:
                    case 91191:
                        return "2/03";
                    case 91126:
                    case 91129:
                        return "3/00";
                    case 91148:
                    case 91151:
                    case 91149:
                    case 91160:
                    case 91159:
                    case 91187:
                    case 91152:
                    case 91107:
                    case 91155:
                    case 91161:
                    case 91146:
                    case 91190:
                    case 91141:
                    case 91142:
                        return "3/95";
                    case 91109:
                    case 91134:
                    case 91179:
                        return "2/00";
                    case 91188:
                    case 91139:
                    case 91154:
                    case 91189:
                        return "2/96";
                    case 91132:
                    case 91138:
                    case 91133:
                    case 91135:
                        return "&nbsp;";
                    case 91118:
                        return "2/05";
                    case 91198:
                    case 91166:
                    case 91165:
                    case 91117:
                    case 91167:
                        return "2/97";
                    case 91120:
                    case 91193:
                    case 91110:
                        return "2/02";
                    case 92237:
                    case 91131:
                    case 91137:
                        return "1992";
                    case 91128:
                    case 91170:
                    case 91123:
                        return "2/99";
                    case 91199:
                    case 91200:
                        return "11/96";
                    case 91116:
                        return "2000";
                    case 91130:
                        return "2/01";
                    case 91168:
                    case 91171:
                        return "3/96";
                    case 91150:
                    case 91140:
                        return "2/96";
                    case 91127:
                        return "3/00";
                    case 91184:
                        return "3/04";
                    case 91125:
                        return "3/01";
                    case 91136:
                        return "1990";
                    case 91206:
                    case 91207:
                    case 91232:
                    case 91230:
                    case 91233:
                    case 91231:
                    case 91208:
                        return "8/93";
                    case 91229:
                    case 91234:
                        return "6/93";
                    case 91255:
                    case 91261:
                    case 91244:
                    case 91260:
                    case 91243:
                    case 91256:
                    case 91241:
                    case 91259:
                    case 91262:
                    case 91257:
                    case 91263:
                        return "7/96";
                    case 91249:
                    case 91251:
                    case 91252:
                    case 91253:
                    case 91250:
                    case 91254:
                        return "6/95";
                    case 91222:
                        return "7/95";
                    case 91220:
                        return "7/97";
                    default:
                        return DateAsString;
                }
            }
        }
    }

    private void DisplayEventsWithRecords(List<EventRecordList> Events)
    {
        List<Table> DisplayTables = new List<Table>();
        for (int i = 0; i < Events.Count; i++)
            if (Events[i].TimeList.Count > 0)
            {
                Table RecordTable = CreateRecordTableForEvent(Events[i]);
                if (RecordTable != null)
                    DisplayTables.Add(RecordTable);
            }

        int RowWorkingOn = 0, CellWorkingOn = 0;
        for (int i = 0; i < DisplayTables.Count; i++)
        {
            Table1.Rows[RowWorkingOn].Cells[CellWorkingOn].Controls.Add(DisplayTables[i]);
            CellWorkingOn++;
            if (CellWorkingOn > 2)
            {
                RowWorkingOn++;
                CellWorkingOn = 0;
            }
        }

        MakeAllInnerTablesSameLenthAndFormat();
    }

    private Table CreateRecordTableForEvent(EventRecordList Event)
    {
        if (Event.TimeList.Count == 0)
            return null;

        TableRow HeaderRow = CreateHeader(Event.Distance, Event.Stroke);
        List<TableRow> RecordRows = CreateRecordRows(Event);

        Table ReturnTable = new Table();
        ReturnTable.ID = "RecordTable";
        ReturnTable.Rows.Add(HeaderRow);
        for (int i = 0; i < RecordRows.Count; i++)
            ReturnTable.Rows.Add(RecordRows[i]);

        ReturnTable.Style.Add("page-break-inside", "avoid");
        

        return ReturnTable;
    }
    private TableRow CreateHeader(int distance, int stroke)
    {
        String Text = distance.ToString() + " ";

        if (stroke == 1)
            Text += "Free";
        if (stroke == 2)
            Text += "Back";
        if (stroke == 3)
            Text += "Breast";
        if (stroke == 4)
            Text += "Fly";
        if (stroke == 5)
            Text += "IM";

        TableRow Row = new TableRow();
        TableCell HeaderNameCell = new TableCell();
        Label HeaderNameLabel = new Label();
        HeaderNameLabel.ID = "HeaderNameLabel";
        HeaderNameLabel.Text = Text;
        HeaderNameCell.ID = "HeaderNameCell";
        HeaderNameCell.Controls.Add(HeaderNameLabel);
        Row.ID = "HeaderRow";

        //HeaderNameCell.Style.Add("border-bottom-style
        //                                        ": solid; border-bottom-width:1px;" +
        //                                        "border-left-style: solid; border-left-width:1px;";

        Row.Cells.Add(HeaderNameCell);

        TableCell HeaderTimeCell = new TableCell();
        Label HeaderTimeLabel = new Label();
        HeaderTimeLabel.ID = "HeaderTimeLabel";
        HeaderTimeLabel.Text = "Time";
        HeaderTimeCell.ID = "HeaderTimeCell";
        HeaderTimeCell.Controls.Add(HeaderTimeLabel);
        Row.Cells.Add(HeaderTimeCell);

        TableCell HeaderDateCell = new TableCell();
        Label HeaderDateLabel = new Label();
        HeaderDateLabel.ID = "HeaderDateLabel";
        HeaderDateLabel.Text = "Year";
        HeaderDateCell.ID = "HeaderDateCell";
        HeaderDateCell.Controls.Add(HeaderDateLabel);
        Row.Cells.Add(HeaderDateCell);

        Row.BackColor = HeaderBackgroudColor;
        Row.ForeColor = Color.White;

        return Row;
    }
    private List<TableRow> CreateRecordRows(EventRecordList Event)
    {
        List<TableRow> Rows = new List<TableRow>();

        for (int i = 0; i < Event.TimeList.Count; i++)
        {
            TableRow Row = new TableRow();
            Row.ID = "RecordRow" + i;

            TableCell NameCell = new TableCell();
            NameCell.ID = "NameCell";
            Label NameLabel = new Label();
            NameLabel.ID = "NameLabel";
            NameLabel.Text = Event.NameList[i];
            NameCell.Controls.Add(NameLabel);



            TableCell TimeCell = new TableCell();
            TimeCell.ID = "TimeCell";
            Label TimeLabel = new Label();
            TimeLabel.ID = "TimeLabel";
            TimeLabel.Text = Event.TimeList[i];
            TimeCell.Controls.Add(TimeLabel);


            TableCell DateCell = new TableCell();
            DateCell.ID = "DateCell";
            Label DateLabel = new Label();
            DateLabel.ID = "DateLabel";
            DateLabel.Text = Event.DateList[i];
            DateCell.Controls.Add(DateLabel);


            Row.Cells.Add(NameCell);
            Row.Cells.Add(TimeCell);
            Row.Cells.Add(DateCell);

            if (Event.HighlightRecordList[i])
                Row.BackColor = Color.Yellow;

            Rows.Add(Row);
        }

        return Rows;
    }
    private Table FormatTableForDisplay(Table t)
    {
        t.CellPadding = 2;
        t.CellSpacing = 0;
        for (int i = 0; i < t.Rows.Count; i++)
        {
            TableCell CellA = t.Rows[i].Cells[0];
            TableCell CellB = t.Rows[i].Cells[1];
            TableCell CellC = t.Rows[i].Cells[2];

            if (i == 0)
            {
                //header row
                CellA.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                CellA.Style.Add(HtmlTextWriterStyle.FontFamily, "Comic Sans");
                CellA.Style.Add("border-top-style", "solid");
                CellA.Style.Add("border-top-width", "2px");
                CellA.Style.Add("border-left-style", "solid");
                CellA.Style.Add("border-left-width", "2px");
                CellA.Style.Add("border-bottom-style", "solid");
                CellA.Style.Add("border-bottom-width", "1px");
                CellA.Style.Add(HtmlTextWriterStyle.BorderColor, "Black");
                CellA.Style.Add(HtmlTextWriterStyle.Width, "152px");

                CellB.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                CellB.Style.Add(HtmlTextWriterStyle.FontFamily, "Comic Sans");
                CellB.Style.Add("border-top-style", "solid");
                CellB.Style.Add("border-top-width", "2px");
                CellB.Style.Add("border-bottom-style", "solid");
                CellB.Style.Add("border-bottom-width", "1px");
                CellB.Style.Add(HtmlTextWriterStyle.BorderColor, "Black");
                CellB.Style.Add(HtmlTextWriterStyle.Width, "94px");

                CellC.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                CellC.Style.Add(HtmlTextWriterStyle.FontFamily, "Comic Sans");
                CellC.Style.Add("border-top-style", "solid");
                CellC.Style.Add("border-top-width", "2px");
                CellC.Style.Add("border-right-style", "solid");
                CellC.Style.Add("border-right-width", "2px");
                CellC.Style.Add("border-bottom-style", "solid");
                CellC.Style.Add("border-bottom-width", "1px");
                CellC.Style.Add(HtmlTextWriterStyle.BorderColor, "Black");
                CellC.Style.Add(HtmlTextWriterStyle.Width, "55px");
            }
            else
            {
                //regular row
                CellA.Style.Add(HtmlTextWriterStyle.Color, "Black");
                CellA.Style.Add("border-left-style", "solid");
                CellA.Style.Add("border-left-width", "2px");
                CellA.Style.Add(HtmlTextWriterStyle.Width, "152px");

                CellB.Style.Add(HtmlTextWriterStyle.Color, "Black");
                CellB.Style.Add(HtmlTextWriterStyle.TextAlign, "Right");
                CellB.Style.Add(HtmlTextWriterStyle.FontFamily, "Courier");
                CellB.Style.Add("border-left-style", "solid");
                CellB.Style.Add("border-left-width", "1px");
                CellB.Style.Add(HtmlTextWriterStyle.Width, "94px");

                CellC.Style.Add(HtmlTextWriterStyle.Color, "Black");
                CellC.Style.Add(HtmlTextWriterStyle.TextAlign, "Center");
                CellC.Style.Add("border-left-style", "solid");
                CellC.Style.Add("border-left-width", "1px");
                CellC.Style.Add("border-right-style", "solid");
                CellC.Style.Add("border-right-width", "2px");
                CellC.Style.Add(HtmlTextWriterStyle.Width, "55px");

                if (i == t.Rows.Count - 1)
                {
                    CellA.Style.Add("border-bottom-style", "solid");
                    CellA.Style.Add("border-bottom-width", "2px");
                    CellB.Style.Add("border-bottom-style", "solid");
                    CellB.Style.Add("border-bottom-width", "2px");
                    CellC.Style.Add("border-bottom-style", "solid");
                    CellC.Style.Add("border-bottom-width", "2px");
                }
                else
                {
                    CellA.Style.Add("border-bottom-style", "solid");
                    CellA.Style.Add("border-bottom-width", "1px");
                    CellB.Style.Add("border-bottom-style", "solid");
                    CellB.Style.Add("border-bottom-width", "1px");
                    CellC.Style.Add("border-bottom-style", "solid");
                    CellC.Style.Add("border-bottom-width", "1px");
                }
            }
        }

        return t;
    }

    private void MakeAllInnerTablesSameLenthAndFormat()
    {
        for (int i = 0; i < Table1.Rows.Count; i++)
            FormatRow(Table1.Rows[i]);
    }
    private void FormatRow(TableRow Row)
    {
        int donormal = 0;
        if (donormal == 1)
        {
            for (int i = 0; i < Row.Cells.Count; i++)
                if (Row.Cells[i].Controls.Count > 0)
                    FormatTableForDisplay((Table)Row.Cells[i].Controls[0]);
        }
        else
        {

            Table TableA = null, TableB = null, TableC = null;
            if (Row.Cells[0].Controls.Count != 0)
                TableA = (Table)Row.Cells[0].Controls[0];
            if (Row.Cells[1].Controls.Count != 0)
                TableB = (Table)Row.Cells[1].Controls[0];
            if (Row.Cells[2].Controls.Count != 0)
                TableC = (Table)Row.Cells[2].Controls[0];

            int MaxRows = 10;
            if (TableA != null)
                if (TableA.Rows.Count - 1 > MaxRows)
                    MaxRows = TableA.Rows.Count - 1;
            if (TableB != null)
                if (TableB.Rows.Count - 1 > MaxRows)
                    MaxRows = TableB.Rows.Count - 1;
            if (TableC != null)
                if (TableC.Rows.Count - 1 > MaxRows)
                    MaxRows = TableC.Rows.Count - 1;

            //do this check just to get it to run faster
            if (MaxRows >= 0)
            {
                if (TableA != null)
                {
                    int InitialRowsA = TableA.Rows.Count - 1;
                    for (int j = 0; j < MaxRows - InitialRowsA; j++)
                    {
                        TableRow RowToAdd = new TableRow();
                        TableCell CellToAdd = new TableCell();
                        Label LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        TableA.Rows.Add(RowToAdd);
                    }
                }
                if (TableB != null)
                {
                    int InitialRowsB = TableB.Rows.Count - 1;
                    for (int j = 0; j < MaxRows - InitialRowsB; j++)
                    {
                        TableRow RowToAdd = new TableRow();
                        TableCell CellToAdd = new TableCell();
                        Label LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        TableB.Rows.Add(RowToAdd);
                    }
                }
                if (TableC != null)
                {
                    int InitialRowsC = TableC.Rows.Count - 1;
                    for (int j = 0; j < MaxRows - InitialRowsC; j++)
                    {
                        TableRow RowToAdd = new TableRow();
                        TableCell CellToAdd = new TableCell();
                        Label LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        CellToAdd = new TableCell();
                        LabelToAdd = new Label();
                        LabelToAdd.Text = "&nbsp;";
                        CellToAdd.Controls.Add(LabelToAdd);
                        RowToAdd.Cells.Add(CellToAdd);
                        TableC.Rows.Add(RowToAdd);
                    }
                }
            }


            for (int i = 0; i < Row.Cells.Count; i++)
                if (Row.Cells[i].Controls.Count > 0)
                    if (Row.Cells[i].Controls.Count > 0)
                        FormatTableForDisplay((Table)Row.Cells[i].Controls[0]);
        }
    }

    private bool SetPageLabel(int MaxAge, int MinAge, String Course, String Gender)
    {
        Label1.Text = "<br />";
        if (Course == "y")
            Label1.Text += "Short Course Team Records<br />";
        else if (Course == "l")
            Label1.Text += "Long Course Team Records<br />";
        else
            return false;

        if (MaxAge == 8 && MinAge < 3)
            Label1.Text += "8 & Under ";
        else if (MaxAge == 10 && MinAge == 9)
            Label1.Text += "9 - 10 ";
        else if (MaxAge == 12 && MinAge == 11)
            Label1.Text += "11 - 12 ";
        else if (MaxAge == 14 && MinAge == 13)
            Label1.Text += "13 - 14 ";
        else if (MaxAge > 17 && MinAge == 15)
            Label1.Text += "15 & Over ";
        else
            return false;

        if (Gender == "m")
            Label1.Text += "Men<br />";
        else if (Gender == "f")
            Label1.Text += "Women<br />";
        else
            return false;

        Label1.Visible = true;

        return true;
    }
}