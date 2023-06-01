using System;
using System.Data;
using System.Configuration;
using SwimTeamDatabaseTableAdapters;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Summary description for AttendanceBLL
/// </summary>
[System.ComponentModel.DataObject]
public class AttendanceBLL
{
    private AttendanceTableAdapter _attendanceAdapter = null;
    protected AttendanceTableAdapter Adapter
    {
        get
        {
            if (this._attendanceAdapter == null)
                this._attendanceAdapter = new AttendanceTableAdapter();
            return this._attendanceAdapter;
        }
    }
    private PracticeTableAdapter _practiceAdapter = null;
    protected PracticeTableAdapter PracticeAdapter
    {
        get
        {
            if (this._practiceAdapter == null)
                this._practiceAdapter = new PracticeTableAdapter();
            return this._practiceAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAllAttendances()
    {
        return this.Adapter.GetAllAttendance();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAttendancesForCertainGroupForCertainDate(int GroupID, DateTime Date)
    {
        DateTime ActualDate = new DateTime(Date.Year, Date.Month, Date.Day);
        return this.Adapter.GetAttendancesForCertainGrouponCertainDate(GroupID, ActualDate);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAttendancesSinceCertainDateForCertainGroup(int GroupID, DateTime SinceDate)
    {
        DateTime ActualDate = new DateTime(SinceDate.Year, SinceDate.Month, SinceDate.Day);

        return this.Adapter.GetAttendancesSinceDateForGroup(ActualDate, GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAttendancesByGroupID(int GroupID)
    {
        return this.Adapter.GetAttendancesByGroupID(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAttendancesThreeDaysBeforeDateForCertainGroup(int GroupID, DateTime Date)
    {
        Date = new DateTime(Date.Year, Date.Month, Date.Day);
        Date = Date.Subtract(new TimeSpan(3, 0, 0, 0));

        return this.GetAttendancesSinceCertainDateForCertainGroup(GroupID, Date);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAttendancesByUSAID(String USAID)
    {
        return this.Adapter.GetAttendancesByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.AttendanceDataTable GetAllAttendancesForSwimmersNotInDatabase()
    {
        return this.Adapter.GetAllAttendancesForSwimmersNotInDatabase();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    public int DeleteByUSAID(String USAID)
    {
        return this.Adapter.DeleteByUSAID(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool ChangeUSAID(String New_USAID, String Old_USAID)
    {
        return this.Adapter.ChangeUSAID(New_USAID, Old_USAID) == 1;
    }


    public int PracticesForCertainGroupOnCertainDay(int GroupID, DateTime Date)
    {
        Date = new DateTime(Date.Year, Date.Month, Date.Day);
        return int.Parse(this.Adapter.PracticesForCertainGroupOnCertainDay(GroupID, Date).ToString());
    }

    public int TotalPracticesForCertainGroup(int GroupID)
    {
        return int.Parse(this.Adapter.TotalPracticesForGroup(GroupID).ToString());
    }

    public static DataColumn USAIDColumn
    {
        get
        {
            return new DataColumn("USAID", System.Type.GetType("System.String"));
        }
    }
    public static DataColumn LastNameColumn
    {
        get { return new DataColumn("LastName", System.Type.GetType("System.String")); }
    }
    public static DataColumn PreferredNameColumn
    {
        get
        {
            return new DataColumn("PreferredName", System.Type.GetType("System.String"));
        }
    }

    public DataTable DistinctListOfAdditionalSwimmersForGroup(int GroupID)
    {
        AttendanceAdditionalSwimmersHelperTableAdapter AttendanceHelperAdapter =
            new AttendanceAdditionalSwimmersHelperTableAdapter();
        SwimTeamDatabase.AttendanceAdditionalSwimmersHelperDataTable HelperTable =
            AttendanceHelperAdapter.GetDistinctSwimmersNotRegistedWithAttendancesForGroup(GroupID);

        DataTable ReturnTable = new DataTable("DistinctAdditionalSwimmersForGroup");
        DataColumn USAIDColumn = AttendanceBLL.USAIDColumn;
        DataColumn LastNameColumn = AttendanceBLL.LastNameColumn;
        DataColumn PreferredNameColumn = AttendanceBLL.PreferredNameColumn;

        ReturnTable.Columns.Add(USAIDColumn);
        ReturnTable.Columns.Add(LastNameColumn);
        ReturnTable.Columns.Add(PreferredNameColumn);

        if (HelperTable == null)
            return ReturnTable;
        if (HelperTable.Count == 0)
            return ReturnTable;





        for (int i = 0; i < HelperTable.Count; i++)
        {
            DataRow row = ReturnTable.NewRow();

            row[USAIDColumn] = HelperTable[i].USAID;
            String Notes = HelperTable[i].Note;
            int index = Notes.IndexOf("SYSTEMGENERATED-NAME:");
            Notes = Notes.Substring(index + "SYSTEMGENERATED-NAME:".Length);
            char[] seperators = new char[1];
            seperators[0] = '$';
            String[] PossibleNames = Notes.Split(seperators);
            for (int j = 0; j < PossibleNames.Length; j++)
                PossibleNames[j] = PossibleNames[j].Trim();
            row[PreferredNameColumn] = PossibleNames[0];
            if (PossibleNames[1].Contains(" "))
            {
                index = PossibleNames[1].IndexOf(" ");
                PossibleNames[1] = PossibleNames[1].Substring(0, index);
            }
            row[LastNameColumn] = PossibleNames[1];

            ReturnTable.Rows.Add(row);
        }

        return ReturnTable;
    }

    public DateTime LastDateOfAttendanceForLastSwimmerForGroup(int GroupID)
    {
        DateTime? NullableReturnDate = this.Adapter.LastDateOfAttendanceForLastSwimmerForGroup(GroupID);
        SettingsBLL SettingsAdapter = new SettingsBLL();
        DateTime NonNullableReturnDate = SettingsAdapter.GetRegistrationStartDate();
        if (NullableReturnDate != null)
            NonNullableReturnDate = DateTime.Parse(NullableReturnDate.ToString());

        return NonNullableReturnDate;
    }

    public static SwimTeamDatabase.AttendanceRow CopyRowToRow(SwimTeamDatabase.AttendanceRow SeedRow,
        SwimTeamDatabase.AttendanceRow RowToCopyTo)
    {
        RowToCopyTo.AttendanceID = SeedRow.AttendanceID;
        if (SeedRow.IsAttendanceTypeNull())
            RowToCopyTo.SetAttendanceTypeNull();
        else
            RowToCopyTo.AttendanceType = SeedRow.AttendanceType;
        RowToCopyTo.Date = SeedRow.Date;
        RowToCopyTo.GroupID = SeedRow.GroupID;
        if (SeedRow.IsLaneNull())
            RowToCopyTo.SetLaneNull();
        else
            RowToCopyTo.Lane = SeedRow.Lane;
        if (SeedRow.IsMetersNull())
            RowToCopyTo.SetMetersNull();
        else
            RowToCopyTo.Meters = SeedRow.Meters;
        if (SeedRow.IsNoteNull())
            RowToCopyTo.SetNoteNull();
        else
            RowToCopyTo.Note = SeedRow.Note;
        RowToCopyTo.PracticeoftheDay = SeedRow.PracticeoftheDay;
        RowToCopyTo.USAID = SeedRow.USAID;
        if (SeedRow.IsYardsNull())
            RowToCopyTo.SetYardsNull();
        else
            RowToCopyTo.Yards = SeedRow.Yards;

        return RowToCopyTo;
    }

    public void BeginBatchInsert()
    {
        this.Adapter.BeginBatchInsert();
    }

    public void BatchInsert(String USAID, DateTime Date, int PracticeoftheDay, int GroupID, String AttendanceType,
            String Note, int? Lane, int? Yards, int? Meters)
    {
        DateTime ActualDateOnly = new DateTime(Date.Year, Date.Month, Date.Day);
        this.Adapter.BatchInsert(USAID, ActualDateOnly, PracticeoftheDay, GroupID, AttendanceType, Note, Lane, Yards, Meters);
    }

    public void CommitBatchInsert()
    {
        this.Adapter.CommitBatchInsert();
    }

    public void EndBatchInsert()
    {
        this.Adapter.EndBatchInsert();
    }

    public void ClearBatchInserts()
    {
        this.Adapter.ClearBatchInserts();
    }

    public int BatchUpdate(SwimTeamDatabase.AttendanceDataTable Table)
    {
        for (int i = 0; i < Table.Count; i++)
            if (Table[i].RowState != DataRowState.Deleted)
                Table[i].Date = new DateTime(Table[i].Date.Year, Table[i].Date.Month, Table[i].Date.Day);
        return this.Adapter.BatchUpdate(Table, Table.Count);
    }

    public int BatchUpdateOnlyDeletes(SwimTeamDatabase.AttendanceDataTable Table)
    {
        return this.Adapter.BatchUpdate(Table, Table.Count);
    }

    public int BatchUpdateIgnoreDBConcurrency(SwimTeamDatabase.AttendanceDataTable Table)
    {
        try
        {
            for (int i = 0; i < Table.Count; i++)
                if (Table[i].RowState != DataRowState.Deleted)
                    Table[i].Date = new DateTime(Table[i].Date.Year, Table[i].Date.Month, Table[i].Date.Day);

            return this.Adapter.BatchUpdate(Table, Table.Count);
        }
        catch (DBConcurrencyException)
        {
            return -1;
        }
    }

    public int TestBatchUpdate(SwimTeamDatabase.AttendanceDataTable Table)
    {
        return this.Adapter.Update(Table);
    }

    public int UpdateUSAID(String old_USAID, String new_USAID)
    {
        return this.Adapter.UpdateUSAID(new_USAID, old_USAID);
    }

    public int GetTotalPractiesForCertainGroup(int GroupID)
    {
        return int.Parse(this.Adapter.TotalPracticesForGroup(GroupID).ToString());
    }

    public SwimTeamDatabase.AttendanceDataTable GetUnregisteredAttendances()
    {
        return this.Adapter.GetUnregisteredAttendances();
    }

    public SwimTeamDatabase.AttendanceDataTable GetLastAttendanceForSwimmerInGroup(int GroupID)
    {
        return this.Adapter.GetLastPracticeForAllSwimmersInGroup(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public SwimTeamDatabase.AttendanceDataTable GetBetweenTwoDatesByGroupID(int GroupID, DateTime StartDate, DateTime EndDate)
    {
        return this.Adapter.GetByBetweenTwoDatesAndGroup(GroupID, StartDate, EndDate);
    }

    public Dictionary<PracticeInfo, SwimTeamDatabase.AttendanceRow> GetListForSwimmer(String USAID, DateTime StartDate, DateTime EndDate, int GroupID)
    {
        SwimTeamDatabase.AttendanceDataTable Attendances = this.Adapter.GetByBetweenTwoDatesAndGroup(GroupID, StartDate, EndDate);

        if (Attendances.Count == 0)
            return null;
        else
        {

            DateTime WorkingOnDate = Attendances[0].Date;

            return null;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PracticeDataTable GetPracticesForGroup(int GroupID)
    {
        return PracticeAdapter.GetPracticesForGroup(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.PracticeDataTable GetPracticesSwimmerAttended(String USAID)
    {
        return PracticeAdapter.GetPracticesSwimmerAttended(USAID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public CalculatedAttendanceDataTable GetAttendancesTableForGroup(int GroupID)
    {
        SwimTeamDatabase.PracticeDataTable AllPracticesOffered = this.GetPracticesForGroup(GroupID);
        SwimTeamDatabase.AttendanceDataTable AllAttendences = this.GetAttendancesByGroupID(GroupID);

        List<List<SwimTeamDatabase.AttendanceRow>> AttendancesListList = new List<List<SwimTeamDatabase.AttendanceRow>>();
        List<SwimTeamDatabase.AttendanceRow> TempRow = new List<SwimTeamDatabase.AttendanceRow>();
        if (AllAttendences.Count > 0)
        {
            TempRow.Add(AllAttendences[0]);
            AttendancesListList.Add(TempRow);
            if (AllAttendences.Count > 0)
            {
                for (int i = 1; i < AllAttendences.Count; i++)
                {
                    SwimTeamDatabase.AttendanceRow Attendance = AllAttendences[i];
                    bool FoundList = false;
                    foreach (List<SwimTeamDatabase.AttendanceRow> AttendanceList in AttendancesListList)
                    {
                        if (AttendanceList[0].USAID == Attendance.USAID)
                        {
                            AttendanceList.Add(Attendance);
                            FoundList = true;
                            break;
                        }
                    }
                    if (!FoundList)
                    {
                        List<SwimTeamDatabase.AttendanceRow> NewTempRow = new List<SwimTeamDatabase.AttendanceRow>();
                        NewTempRow.Add(Attendance);
                        AttendancesListList.Add(NewTempRow);
                    }
                }


                CalculatedAttendanceDataTable ReturnTable = new CalculatedAttendanceDataTable();

                SwimmersBLL SwimmersAdapter = new SwimmersBLL();
                SwimTeamDatabase.SwimmersDataTable Swimmers = SwimmersAdapter.GetSwimmersByGroupID(GroupID);

                for (int i = 0; i < AttendancesListList.Count; i++)
                {
                    string USAID = AttendancesListList[i][0].USAID;
                    bool SwimmerFound = false;
                    for (int j = 0; j < Swimmers.Count; j++)
                        if (Swimmers[j].USAID == USAID)
                        { SwimmerFound = true; break; }
                    if (!SwimmerFound)
                    {
                        int stop = 0;
                        stop++;
                    }
                }

                foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
                {
                    List<SwimTeamDatabase.AttendanceRow> SwimmerAttendanceList = new List<SwimTeamDatabase.AttendanceRow>();
                    foreach (List<SwimTeamDatabase.AttendanceRow> TestingAttendanceList in AttendancesListList)
                    {
                        if (TestingAttendanceList.Count > 0)
                        {
                            if (TestingAttendanceList[0].USAID == Swimmer.USAID)
                            {
                                SwimmerAttendanceList = TestingAttendanceList;
                                break;
                            }
                        }
                    }


                    int PracticesOffered = AllPracticesOffered.Count;

                    CalculatedAttendanceRow NewRow = ReturnTable.NewCalculatedAttendanceRow();

                    if (SwimmerAttendanceList.Count == 0)
                    {
                        NewRow.USAID = Swimmer.USAID;
                        NewRow.Attended = 0;
                        NewRow.Offered = PracticesOffered;
                        NewRow.LastName = Swimmer.LastName;
                        NewRow.FirstName = Swimmer.PreferredName;
                        NewRow.GroupID = Swimmer.GroupID;
                    }
                    else
                    {
                        CultureInfo myCI = new CultureInfo("en-US");
                        System.Globalization.Calendar cal = myCI.Calendar;
                        int WeekOfFirstPracticeOffered = -1;
                        int WeekOfFirstPracticeAttended = -1;
                        int ReducePracticesOfferedBy = 0;

                        WeekOfFirstPracticeOffered = cal.GetWeekOfYear(AllPracticesOffered[0].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                        for (int i = 0; i < AllPracticesOffered.Count; i++)
                            if (AllPracticesOffered[i].Date.DayOfYear == SwimmerAttendanceList[SwimmerAttendanceList.Count - 1].Date.DayOfYear &&
                                AllPracticesOffered[i].PracticeoftheDay == SwimmerAttendanceList[SwimmerAttendanceList.Count - 1].PracticeoftheDay)
                                break;
                            else
                                ReducePracticesOfferedBy++;
                        WeekOfFirstPracticeAttended = cal.GetWeekOfYear(SwimmerAttendanceList[SwimmerAttendanceList.Count - 1].Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                        WeekOfFirstPracticeAttended = WeekOfFirstPracticeAttended + ((SwimmerAttendanceList[SwimmerAttendanceList.Count - 1].Date.Year - AllPracticesOffered[0].Date.Year) * 52);

                        if (WeekOfFirstPracticeAttended - WeekOfFirstPracticeOffered > 2)
                            PracticesOffered = PracticesOffered - ReducePracticesOfferedBy;

                        int yardage = 0, meterage = 0, Late = 0, VeryLate = 0, LeftEarly = 0, Bonus = 0, SatOutALot = 0, ExcusedAbscence = 0;
                        for (int i = 0; i < SwimmerAttendanceList.Count; i++)
                        {
                            if (!SwimmerAttendanceList[i].IsYardsNull())
                                yardage += SwimmerAttendanceList[i].Yards;
                            if (!SwimmerAttendanceList[i].IsMetersNull())
                                meterage += SwimmerAttendanceList[i].Meters;
                            String AttendanceType = SwimmerAttendanceList[i].AttendanceType.ToUpper();
                            if (AttendanceType == "L")
                                Late++;
                            else if (AttendanceType == "VL")
                                VeryLate++;
                            else if (AttendanceType == "LE")
                                LeftEarly++;
                            else if (AttendanceType == "SO")
                                SatOutALot++;
                            else if (AttendanceType == "BA")
                                Bonus++;
                            else if (AttendanceType == "EA")
                                ExcusedAbscence++;
                        }
                        if (ExcusedAbscence != 0)
                            PracticesOffered = PracticesOffered - ExcusedAbscence;

                        NewRow.USAID = Swimmer.USAID;
                        NewRow.Attended = SwimmerAttendanceList.Count - ExcusedAbscence;
                        NewRow.Offered = PracticesOffered;
                        if (yardage > 0)
                            NewRow.Yardage = yardage;
                        if (meterage > 0)
                            NewRow.Meterage = meterage;
                        if (Late != 0 || VeryLate != 0 || LeftEarly != 0 || Bonus != 0 || SatOutALot != 0 || ExcusedAbscence != 0)
                        {
                            String Notes = "";
                            if (Late > 0)
                                Notes += "Late: " + Late;
                            if (VeryLate > 0)
                            {
                                if (Notes != String.Empty)
                                    Notes += ", ";
                                Notes += "Very Late: " + VeryLate;
                            }
                            if (LeftEarly > 0)
                            {
                                if (Notes != String.Empty)
                                    Notes += ", ";
                                Notes += "Left Early: " + LeftEarly;
                            }
                            if (SatOutALot > 0)
                            {
                                if (Notes != String.Empty)
                                    Notes += ", ";
                                Notes += "Sat out a Lot: " + SatOutALot;
                            }
                            if (Bonus > 0)
                            {
                                if (Notes != String.Empty)
                                    Notes += ", ";
                                Notes += "Bonus: " + Bonus;
                            }
                            if (ExcusedAbscence > 0)
                            {
                                if (Notes != String.Empty)
                                    Notes += ", ";
                                Notes += "Excused Abscence: " + ExcusedAbscence;
                            }

                            NewRow.Notes = Notes;
                        }
                        NewRow.LastName = Swimmer.LastName;
                        NewRow.FirstName = Swimmer.PreferredName;
                        NewRow.GroupID = Swimmer.GroupID;
                    }


                    ReturnTable.AddCalculatedAttendanceRow(NewRow);
                }

                return ReturnTable;
            }

            return null;
        }
        return null;
    }

    public class PracticeInfo
    {
        private int _PracticeOfTheDay;
        public int PracticeOfTheDay
        {
            get { return this._PracticeOfTheDay; }
            set { this._PracticeOfTheDay = value; }
        }
        private DateTime _Date;
        public DateTime Date
        {
            get { return this._Date; }
            set { this._Date = value; }
        }
        public PracticeInfo(int PracticeOfTheDay, DateTime Date)
        {
            this._PracticeOfTheDay = PracticeOfTheDay;
            this._Date = Date;
        }
        public PracticeInfo(int PracticeOfTheDay, int Month, int Day, int Year)
        {
            DateTime Date = new DateTime(Year, Month, Day, 0, 0, 0);
            this._PracticeOfTheDay = PracticeOfTheDay;
        }
    }

    public class CalculatedAttendanceDataTable : global::System.Data.TypedTableBase<CalculatedAttendanceRow>
    {
        private DataColumn _USAIDColumn;
        private DataColumn _AttendedColumn;
        private DataColumn _OfferedColumn;
        private DataColumn _PercentageColumn;
        private DataColumn _YardageColumn;
        private DataColumn _MeterageColumn;
        private DataColumn _NotesColumn;
        private DataColumn _LastNameColumn;
        private DataColumn _FirstNameColumn;
        private DataColumn _GroupIDColumn;

        public CalculatedAttendanceDataTable()
        {
            this.TableName = "CalculatedAttendance";
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        internal CalculatedAttendanceDataTable(global::System.Data.DataTable table)
        {
            this.TableName = table.TableName;
            if ((table.CaseSensitive != table.DataSet.CaseSensitive))
                this.CaseSensitive = table.CaseSensitive;
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
                this.Locale = table.Locale;
            if ((table.Namespace != table.DataSet.Namespace))
                this.Namespace = table.Namespace;
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
        }
        protected CalculatedAttendanceDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) :
            base(info, context)
        {
            this.InitVars();
        }

        public global::System.Data.DataColumn USAIDColum
        {
            get { return this._USAIDColumn; }
        }
        public global::System.Data.DataColumn AttendedColumn
        {
            get { return this._AttendedColumn; }
        }
        public global::System.Data.DataColumn OfferedColumn
        {
            get { return this._OfferedColumn; }
        }
        public global::System.Data.DataColumn PercentageColumn
        {
            get { return this._PercentageColumn; }
        }
        public global::System.Data.DataColumn YardageColumn
        {
            get { return this._YardageColumn; }
        }
        public global::System.Data.DataColumn MeterageColumn
        {
            get
            {
                return this._MeterageColumn;
            }
        }
        public global::System.Data.DataColumn NotesColumn
        {
            get { return this._NotesColumn; }
        }
        public global::System.Data.DataColumn LastNameColumn
        {
            get { return this._LastNameColumn; }
        }
        public global::System.Data.DataColumn FirstNameColumn
        {
            get { return this._FirstNameColumn; }
        }
        public global::System.Data.DataColumn GroupIDColumn
        {
            get { return this._GroupIDColumn; }
        }

        public int Count
        {
            get { return this.Rows.Count; }
        }

        public void AddCalculatedAttendanceRow(CalculatedAttendanceRow row)
        {
            this.Rows.Add(row);
        }

        public CalculatedAttendanceRow AddCalculatedAttendanceRow(String USAID,
            int PracticesAttended, int PracticesOffered, int Yardage, int Meterage, String Notes, String LastName, String FirstName, int GroupID)
        {
            double AttendancePercentage = Convert.ToDouble(PracticesAttended) / Convert.ToDouble(PracticesOffered);
            AttendancePercentage = Math.Round(AttendancePercentage, 1);

            CalculatedAttendanceRow rowCalculatedAttendanceRow = ((CalculatedAttendanceRow)(this.NewRow()));
            object[] columnValuesArray = new object[]{
                USAID, PracticesAttended, PracticesOffered, AttendancePercentage, Yardage, Meterage, Notes, LastName, FirstName, GroupID};
            rowCalculatedAttendanceRow.ItemArray = columnValuesArray;
            this.Rows.Add(rowCalculatedAttendanceRow);
            return rowCalculatedAttendanceRow;
        }

        public override DataTable Clone()
        {
            CalculatedAttendanceDataTable cln = ((CalculatedAttendanceDataTable)base.Clone());
            cln.InitVars();
            return cln;
        }

        protected override DataTable CreateInstance()
        {
            return new CalculatedAttendanceDataTable();
        }

        private void InitClass()
        {
            this._USAIDColumn = new global::System.Data.DataColumn("USAID", typeof(String), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._USAIDColumn);
            this._AttendedColumn = new global::System.Data.DataColumn("Attended", typeof(int), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._AttendedColumn);
            this._OfferedColumn = new global::System.Data.DataColumn("Offered", typeof(int), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._OfferedColumn);
            this._PercentageColumn = new global::System.Data.DataColumn("Percentage", typeof(double), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._PercentageColumn);
            this._YardageColumn = new global::System.Data.DataColumn("Yardage", typeof(int), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._YardageColumn);
            this._MeterageColumn = new global::System.Data.DataColumn("Meterage", typeof(int), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._MeterageColumn);
            this._NotesColumn = new global::System.Data.DataColumn("Notes", typeof(String), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._NotesColumn);
            this._LastNameColumn = new global::System.Data.DataColumn("LastName", typeof(String), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._LastNameColumn);
            this._FirstNameColumn = new global::System.Data.DataColumn("FirstName", typeof(String), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._FirstNameColumn);
            this._GroupIDColumn = new global::System.Data.DataColumn("GroupID", typeof(int), null, global::System.Data.MappingType.Element);
            base.Columns.Add(this._GroupIDColumn);

            this.Constraints.Add(new global::System.Data.UniqueConstraint("Contraint1", new global::System.Data.DataColumn[] { this._USAIDColumn }, true));

            this._AttendedColumn.AllowDBNull = true;
            this._OfferedColumn.AllowDBNull = true;
            this._PercentageColumn.AllowDBNull = true;
            this._YardageColumn.AllowDBNull = true;
            this._MeterageColumn.AllowDBNull = true;
            this._NotesColumn.AllowDBNull = true;

            this._USAIDColumn.Unique = true;
            this._USAIDColumn.MaxLength = 14;
        }

        public CalculatedAttendanceRow NewCalculatedAttendanceRow()
        {
            return ((CalculatedAttendanceRow)(this.NewRow()));
        }
        protected override DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder)
        {
            return new CalculatedAttendanceRow(builder);
        }
        protected override Type GetRowType()
        {
            return typeof(CalculatedAttendanceRow);
        }
        public void RemoveCalculatedAttendanceRow(CalculatedAttendanceRow row)
        {
            this.Rows.Remove(row);
        }
        private void InitVars()
        {
            this._USAIDColumn = base.Columns["USAID"];
            this._AttendedColumn = base.Columns["Attended"];
            this._OfferedColumn = base.Columns["Offered"];
            this._PercentageColumn = base.Columns["Percentage"];
            this._YardageColumn = base.Columns["Yardage"];
            this._MeterageColumn = base.Columns["Meterage"];
            this._NotesColumn = base.Columns["Notes"];
            this._LastNameColumn = base.Columns["LastName"];
            this._FirstNameColumn = base.Columns["FirstName"];
            this._GroupIDColumn = base.Columns["GroupID"];
        }
    }

    public class CalculatedAttendanceRow : DataRow
    {
        private CalculatedAttendanceDataTable tableCalculatedAttendance;

        internal CalculatedAttendanceRow(global::System.Data.DataRowBuilder rb)
            : base(rb)
        {
            this.tableCalculatedAttendance = ((CalculatedAttendanceDataTable)this.Table);
        }

        public string USAID
        {
            get
            {
                try
                {
                    return ((string)(this[this.tableCalculatedAttendance.USAIDColum]));
                }
                catch (InvalidCastException e)
                {
                    throw new StrongTypingException("The value for \'USAID\' in table \'CalculatedAttendance\' is DBNull.", e);
                }
            }
            set
            {
                this[this.tableCalculatedAttendance.USAIDColum] = value;
            }
        }

        public int Attended
        {
            get
            {
                return (int)this[this.tableCalculatedAttendance.AttendedColumn];
            }
            set
            {
                this[this.tableCalculatedAttendance.AttendedColumn] = value;
                this.SetPercentage();
            }
        }

        public int Offered
        {
            get
            {
                return (int)this[this.tableCalculatedAttendance.OfferedColumn];
            }
            set
            {
                this[this.tableCalculatedAttendance.OfferedColumn] = value;
                this.SetPercentage();
            }
        }

        public int Yardage
        {
            get { return (int)this[this.tableCalculatedAttendance.YardageColumn]; }
            set { this[this.tableCalculatedAttendance.YardageColumn] = value; }
        }
        public int Meterage
        {
            get { return (int)this[this.tableCalculatedAttendance.MeterageColumn]; }
            set { this[this.tableCalculatedAttendance.MeterageColumn] = value; }
        }
        public String Notes
        {
            get { return (String)this[this.tableCalculatedAttendance.NotesColumn]; }
            set { this[this.tableCalculatedAttendance.NotesColumn] = value; }
        }
        public String LastName
        {
            get { return (String)this[this.tableCalculatedAttendance.LastNameColumn]; }
            set { this[this.tableCalculatedAttendance.LastNameColumn] = value; }
        }
        public String FirstName
        {
            get { return (String)this[this.tableCalculatedAttendance.FirstNameColumn]; }
            set { this[this.tableCalculatedAttendance.FirstNameColumn] = value; }
        }
        public int GroupID
        {
            get { return (int)this[this.tableCalculatedAttendance.GroupIDColumn]; }
            set { this[this.tableCalculatedAttendance.GroupIDColumn] = value; }
        }

        public double Percentage
        {
            get
            {
                return (double)this[this.tableCalculatedAttendance.PercentageColumn];
            }
        }

        public bool IsAttendanceNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.AttendedColumn);
        }
        public void SetAttendanceNull()
        {
            this[this.tableCalculatedAttendance.AttendedColumn] = Convert.DBNull;
        }
        public bool IsOfferedNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.OfferedColumn);
        }
        public void SetOfferedNull()
        {
            this[this.tableCalculatedAttendance.OfferedColumn] = Convert.DBNull;
        }
        public bool IsPercentageNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.PercentageColumn);
        }
        public void SetPercentageNull()
        {
            this[this.tableCalculatedAttendance.PercentageColumn] = Convert.DBNull;
        }
        public bool IsYardageNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.YardageColumn);
        }
        public void SetYardageNull()
        {
            this[this.tableCalculatedAttendance.YardageColumn] = Convert.DBNull;
        }
        public bool IsMeterageNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.MeterageColumn);
        }
        public void SetMeterageNull()
        {
            this[this.tableCalculatedAttendance.MeterageColumn] = Convert.DBNull;
        }
        public bool IsNotesNull()
        {
            return this.IsNull(this.tableCalculatedAttendance.NotesColumn);
        }
        public void SetNotesNull()
        {
            this[this.tableCalculatedAttendance.NotesColumn] = Convert.DBNull;
        }

        private void SetPercentage()
        {
            if (!IsAttendanceNull() && !IsOfferedNull())
            {
                //double Attendance = (Convert.ToDouble(this.Attended) / Convert.ToDouble(Offered)) * 100.0;
                //Attendance = Math.Round(Attendance, 1);
                //this[this.tableCalculatedAttendance.PercentageColumn] = Attendance;
                this[this.tableCalculatedAttendance.PercentageColumn] = Convert.ToDouble(this.Attended) / Convert.ToDouble(Offered);
            }
        }
    }

    public int DeleteDuplicateAttendances()
    {
        SwimTeamDatabase.AttendanceDataTable Attendances = this.Adapter.GetAllAttendance();

        List<int> DeleteIndexes = new List<int>();

        if (Attendances.Count > 0)
        {
            String LastUSAID = Attendances[0].USAID;
            DateTime LastDate = Attendances[0].Date;
            int LastPracticeNumber = Attendances[0].PracticeoftheDay;


            for (int i = 1; i < Attendances.Count; i++)
            {
                if (Attendances[i].USAID == LastUSAID)
                    if (Attendances[i].Date == LastDate)
                        if (Attendances[i].PracticeoftheDay == LastPracticeNumber)
                            DeleteIndexes.Add(i);
                        else
                            LastPracticeNumber = Attendances[i].PracticeoftheDay;
                    else
                        LastDate = Attendances[i].Date;
                else
                    LastUSAID = Attendances[i].USAID;
            }
        }

        for (int i = 0; i < DeleteIndexes.Count; i++)
            Attendances[DeleteIndexes[i]].Delete();

        this.BatchUpdateOnlyDeletes(Attendances);

        return DeleteIndexes.Count;
    }
}