using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class UserControls_Attendance_AttendanceControl : System.Web.UI.UserControl
{

    /*
     *  When trying to reset the information in things that need to maintain values
     *  ASP.NET automatically applies names to the beginning of these things making it
     *  difficult to determine the name to search for. This internal setting lets us
     *  determine what the first part of the names of all the controls used in this
     *  control is. It is only run once and then saved during the construction of each
     *  page.
     */
    private String _dynamicNamingContainer;
    private String DynamicNamingContainer
    {
        get
        {
            if (String.IsNullOrEmpty(_dynamicNamingContainer))
            {
                for (int i = 0; i < Request.Form.Keys.Count; i++)
                    if (Request.Form.Keys[i].Contains("GroupsDropDownList"))
                    {
                        String Key = Request.Form.Keys[i];
                        int index = Key.IndexOf("GroupsDropDownList");
                        _dynamicNamingContainer = Key.Substring(0, index);
                    }
            }

            return _dynamicNamingContainer;
        }
    }

    private int? _GroupPicking;
    private int? GroupPicked
    {
        get
        {
            if (this._GroupPicking == null)
                if (ViewState["GroupPicking"] != null)
                    if (!string.IsNullOrEmpty(ViewState["GroupPicking"].ToString()))
                        this._GroupPicking = int.Parse(ViewState["GroupPicking"].ToString());

            return this._GroupPicking;
        }
        set
        {
            this._GroupPicking = value;
            ViewState["GroupPicking"] = this._GroupPicking;
        }
    }

    private List<String> _GroupsNamesList;
    private List<String> GroupsNamesList
    {
        get
        {
            if (this._GroupsNamesList == null)
                if (ViewState["GroupsNamesList"] != null)
                    this._GroupsNamesList = ((List<String>)ViewState["GroupsNamesList"]);
                else
                    this.GetGroupsFromDatabase();
            return this._GroupsNamesList;
        }
        set
        {
            this._GroupsNamesList = value;
            ViewState["GroupsNamesList"] = this._GroupsNamesList;
        }
    }
    private List<int> _GroupsIDValuesList;
    private List<int> GroupsIDValuesList
    {
        get
        {
            if (this._GroupsIDValuesList == null)
                if (ViewState["GroupsIDValuesList"] != null)
                    this._GroupsIDValuesList = ((List<int>)ViewState["GroupsIDValuesList"]);
                else
                    this.GetGroupsFromDatabase();
            return this._GroupsIDValuesList;
        }
        set
        {
            this._GroupsIDValuesList = value;
            ViewState["GroupsIDValuesList"] = this._GroupsIDValuesList;
        }
    }

    private void GetGroupsFromDatabase()
    {
        if (this._GroupsNamesList == null)
        {
            //The Groups have not been previously set, so we need to get them
            GroupsBLL GroupsAdapter = new GroupsBLL();
            SwimTeamDatabase.GroupsDataTable ActiveGroups = GroupsAdapter.GetActiveGroups();

            List<String> GroupNames = new List<string>();
            List<int> GroupIDs = new List<int>();
            foreach (SwimTeamDatabase.GroupsRow Group in ActiveGroups)
            {
                GroupNames.Add(Group.GroupName);
                GroupIDs.Add(Group.GroupID);
            }
            this._GroupsNamesList = GroupNames;
            this._GroupsIDValuesList = GroupIDs;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        this.AddPracticeErrorLabel.Visible = false;
        this.AddJavaScript();
    }

    //private DateTime StartTime;
    protected void Page_Init(object sender, EventArgs e)
    {
        //this.StartTime = DateTime.Now;
        Page.LoadComplete += this.Page_LoadComplete;

        this.DateControl1.SelectedDateChanged += this.DateControlDateChanged;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.PopulateGroupDropDownList();
            this.SetGroupDropDownList();
            this.LoadSwimmersDataTable();
            this.LoadLastTimeLaneDictionary();
            if (this.DateControl1.Date != null)
                this.LoadAttendaceTableForDate(this.DateControl1.Date);
            this.MatchSwimmersAndAttendanceTableOrder();
            this.CreateDisplayTable();
        }
        else
        {

            //All Possible Postback situations should handle creating the table themselves
            //In the mean time, since these have not been handled, we are just going to do
            //the same thing every time.
            //this.PopulateGroupDropDownList();
            this.SetGroupDropDownList();
            if (this.GetValuesFromStatus == GetValuesFrom.Database)
            {
                if (this.FormValues != null)
                    this.FormValues.Clear();
                this.LoadSwimmersDataTable();
                this.LoadLastTimeLaneDictionary();
                if (this.DateControl1.Date != null)
                    this.LoadAttendaceTableForDate(this.DateControl1.Date);

            }
            this.MatchSwimmersAndAttendanceTableOrder();
            this.CreateDisplayTable();
            if (this.GetValuesFromStatus == GetValuesFrom.Form)
                this.LoadFromSavedFormValues();
            if (this.UpdateYardages)
                this.ReassignYardagesToAllFromPreviousPage();
            if (this.PracticeSaved == true)
                this.AttendanceSavedLabel.Visible = true;
            else
                this.AttendanceSavedLabel.Visible = false;
        }

        //TimeSpan PreparationTime = this.StartTime - DateTime.Now;
        //this.Tester.Text = PreparationTime.ToString();
        //Tester.Visible = true;
    }




    private void DateControlDateChanged(object sender, EventArgs e)
    {
        int i = 0;
        i++;
        this.GetValuesFromStatus = GetValuesFrom.Database;
    }


    private void PopulateGroupDropDownList()
    {

        if (this.GroupsDropDownList.Items.Count > 0)
            this.GroupsDropDownList.Items.Clear();

        for (int i = 0; i < this.GroupsNamesList.Count; i++)
        {
            GroupsDropDownList.Items.Add(new ListItem(
                this.GroupsNamesList[i],
                this.GroupsIDValuesList[i].ToString()));
        }
    }

    private void SetGroupDropDownList()
    {
        if (GroupsDropDownList.Items.Count > 0)
        {
            if (this.GroupPicked != null)
                this.GroupsDropDownList.SelectedValue = this.GroupPicked.ToString();
            else
            {

                if (String.IsNullOrEmpty(Profile.GroupID))
                    this.ChangeDefaultGroupButtonClicked(this.ChangeDefaultGroupButton, new EventArgs());
                else
                {
                    try
                    {
                        this.GroupsDropDownList.SelectedValue = Profile.GroupID;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        //the default group is not an active group, so pick a new one
                        this.ChangeDefaultGroupButtonClicked(this.ChangeDefaultGroupButton, new EventArgs());
                    }
                }
            }

            if (!String.IsNullOrEmpty(GroupsDropDownList.SelectedValue))
                this.GroupPicked = int.Parse(GroupsDropDownList.SelectedValue);
        }
    }


    private void GetAttendanceForDay()
    {
    }

    private void GetListOfAthletesForGroup()
    {
    }
    protected void DefaultGroupChanged(object sender, EventArgs e)
    {
        this.AddSwimmerPanel.Visible = false;
        this.RegularPanel.Visible = true;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;



        Profile.GroupID = DefaultListDropDownBox.SelectedValue;
        this.GroupPicked = int.Parse(Profile.GroupID);

        //this.PopulateGroupDropDownList();
        this.SetGroupDropDownList();

        this.GetValuesFromStatus = GetValuesFrom.Form;

        this.GetValuesFromStatus = GetValuesFrom.Database;

    }
    protected void ChangeDefaultGroupButtonClicked(object sender, EventArgs e)
    {
        this.SaveFormValues();
        this.AddSwimmerPanel.Visible = false;
        this.RegularPanel.Visible = false;
        this.ChangeDefaultGroupPanel.Visible = true;
        this.LanePanel.Visible = false;

        if (this.DefaultListDropDownBox.Items.Count > 0)
            this.DefaultListDropDownBox.Items.Clear();

        for (int i = 0; i < this.GroupsNamesList.Count; i++)
            this.DefaultListDropDownBox.Items.Add(new ListItem(
                this.GroupsNamesList[i],
                this.GroupsIDValuesList[i].ToString()));

        if (!string.IsNullOrEmpty(Profile.GroupID))
        {
            try
            {
                this.DefaultListDropDownBox.SelectedValue = Profile.GroupID;
            }
            catch (ArgumentOutOfRangeException)
            {
                //If the previous default group is not in the list, do nothing
            }
        }

        this.GetValuesFromStatus = GetValuesFrom.Neither;
    }
    protected void KeepThisGroupButtonClicked(object sender, EventArgs e)
    {
        this.AddSwimmerPanel.Visible = false;
        this.RegularPanel.Visible = true;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;

        this.SetGroupDropDownList();
        //this.ReloadFromPreviousFormValues = true;

        this.GetValuesFromStatus = GetValuesFrom.Form;
    }
    protected void ChangeSelectedGroup(object sender, EventArgs e)
    {
        this.GroupPicked = int.Parse(Request.Form[this.DynamicNamingContainer + "GroupsDropDownList"].ToString());
        this.SetGroupDropDownList();

        this.GetValuesFromStatus = GetValuesFrom.Database;
    }

    private static DataColumn Swimmers_USAIDColumn
    {
        get
        {
            return new DataColumn("USAID", System.Type.GetType("System.String"));
        }
    }
    private static DataColumn Swimmers_LastNameColumn
    {
        get
        {
            return new DataColumn("LastName", System.Type.GetType("System.String"));
        }
    }
    private static DataColumn Swimmers_PreferredNameColumn
    {
        get
        {
            return new DataColumn("PreferredName", System.Type.GetType("System.String"));
        }
    }
    private DataTable _swimmers;
    private DataTable Swimmers
    {
        get
        {
            if (_swimmers == null)
                _swimmers = ((DataTable)ViewState["SwimmersDataTable"]);
            return _swimmers;
        }
        set
        {
            this._swimmers = value;
            if (this._swimmers.Columns.Count < 3)
                throw new Exception("Error - Swimmers Data Table malformated");
            else
            {
                for (int i = 0; i < 3; i++)
                    if (this._swimmers.Columns[i].DataType != System.Type.GetType("System.String"))
                        throw new Exception("Error - Swimmers Data Table malformated");

            }
            ViewState.Add("SwimmersDataTable", this._swimmers);
        }
    }

    private DataTable _ExtraSwimmersDataTable;
    private DataTable ExtraSwimmersTable
    {
        get
        {
            if (this._ExtraSwimmersDataTable == null)
                if (ViewState["ExtraSwimmersDataTable"] != null)
                    this._ExtraSwimmersDataTable = ((DataTable)ViewState["ExtraSwimmersDataTable"]);
            return this._ExtraSwimmersDataTable;
        }
        set
        {
            this._ExtraSwimmersDataTable = value;
            if (ViewState["ExtraSwimmersDataTable"] == null)
                ViewState.Add("ExtraSwimmersDataTable", value);
            else
                ViewState["ExtraSwimmersDataTable"] = value;
        }
    }
    private void LoadSwimmersDataTable()
    {
        SwimmersBLL SwimmersAdapter = new SwimmersBLL();
        if (this.GroupPicked != null)
        {
            SwimTeamDatabase.SwimmersDataTable SwimmersTable =
                SwimmersAdapter.GetSwimmersByGroupID(int.Parse(this.GroupPicked.ToString()));

            //DataTable SwimmersDataTable = new DataTable("SwimmersDataTable");

            //DataColumn Swimmers_USAIDColumn = UserControls_Attendance_AttendanceControl.Swimmers_USAIDColumn;
            //DataColumn Swimmers_LastNameColumn = UserControls_Attendance_AttendanceControl.Swimmers_LastNameColumn;
            //DataColumn Swimmers_PreferredNameColumn = UserControls_Attendance_AttendanceControl.Swimmers_PreferredNameColumn;

            //SwimmersDataTable.Columns.Add(Swimmers_USAIDColumn);
            //SwimmersDataTable.Columns.Add(Swimmers_LastNameColumn);
            //SwimmersDataTable.Columns.Add(Swimmers_PreferredNameColumn);

            //DataColumn[] keys = new DataColumn[1];
            //keys[0] = Swimmers_USAIDColumn;
            //SwimmersDataTable.PrimaryKey = keys;

            DataTable SwimmersDataTable = this.EmptySwimmerTable();

            for (int i = 0; i < SwimmersTable.Count; i++)
            {
                DataRow NewRow = SwimmersDataTable.NewRow();
                NewRow.SetField(Swimmers_USAIDColumn.ColumnName, SwimmersTable[i].USAID);
                NewRow.SetField(Swimmers_LastNameColumn.ColumnName, SwimmersTable[i].LastName);
                NewRow.SetField(Swimmers_PreferredNameColumn.ColumnName, SwimmersTable[i].PreferredName);

                SwimmersDataTable.Rows.Add(NewRow);
            }

            AttendanceBLL AttendanceAdapter = new AttendanceBLL();
            this.ExtraSwimmersTable = AttendanceAdapter.DistinctListOfAdditionalSwimmersForGroup(
                int.Parse(this.GroupPicked.ToString()));

            if (ExtraSwimmersTable != null)
            {
                for (int i = 0; i < ExtraSwimmersTable.Rows.Count; i++)
                {
                    DataRow NewRow = SwimmersDataTable.NewRow();
                    NewRow[Swimmers_USAIDColumn.ColumnName] = this.ExtraSwimmersTable.Rows[i][AttendanceBLL.USAIDColumn.ColumnName];
                    NewRow[Swimmers_LastNameColumn.ColumnName] = this.ExtraSwimmersTable.Rows[i][AttendanceBLL.LastNameColumn.ColumnName];
                    NewRow[Swimmers_PreferredNameColumn.ColumnName] = this.ExtraSwimmersTable.Rows[i][AttendanceBLL.PreferredNameColumn.ColumnName];

                    SwimmersDataTable.Rows.Add(NewRow);

                    if (this.NewSwimmerUSAIDs == null)
                        this.NewSwimmerUSAIDs = new List<string>();

                    if (!this.NewSwimmerUSAIDs.Contains(this.ExtraSwimmersTable.Rows[i][AttendanceBLL.USAIDColumn.ColumnName].ToString()))
                        this.NewSwimmerUSAIDs.Add(this.ExtraSwimmersTable.Rows[i][AttendanceBLL.USAIDColumn.ColumnName].ToString());
                }
            }

            this.NewSwimmerUSAIDs = this.NewSwimmerUSAIDs;
            //if (this.NewSwimmerUSAIDs != null)
            //{
            //    for (int i = 0; i < this.NewSwimmerUSAIDs.Count; i++)
            //    {
            //        DataRow NewRow = SwimmersDataTable.NewRow();
            //        NewRow[Swimmers_USAIDColumn.ColumnName] = this.NewSwimmerUSAIDs[i];
            //        NewRow[Swimmers_LastNameColumn.ColumnName] = this.NewSwimmerLastName[i];
            //        NewRow[Swimmers_PreferredNameColumn.ColumnName] = this.NewSwimmerFirstName[i];

            //        SwimmersDataTable.Rows.Add(NewRow);
            //    }
            //}

            this.Swimmers = SwimmersDataTable;
        }
    }





    private Dictionary<String, int> _LastTimeLane;
    private Dictionary<String, int> LastTimeLane
    {
        get
        {
            if (this._LastTimeLane == null)
                if (ViewState["LastTimeLaneDictionary"] != null)
                    LastTimeLane = ((Dictionary<String, int>)ViewState["LastTimeLaneDictionary"]);
            return this._LastTimeLane;
        }
        set
        {
            this._LastTimeLane = value;
            ViewState.Add("LastTimeLaneDictionary", this._LastTimeLane);
        }
    }
    private DateTime _FurthestDateOfMostRecentAttendance;
    private DateTime FurthestDateOfMostRecentAttendance
    {
        get
        {
            if (this._FurthestDateOfMostRecentAttendance == null)
                if (ViewState["FurthestDateOfMostRecentAttendance"] != null)
                    this._FurthestDateOfMostRecentAttendance = ((DateTime)ViewState["FurthestDateOfMostRecentAttendance"]);
            return this._FurthestDateOfMostRecentAttendance;
        }
        set
        {
            this._FurthestDateOfMostRecentAttendance = value;
            ViewState.Add("FurthestDateOfMostRecentAttendance", value);
        }
    }

    private SwimTeamDatabase.AttendanceDataTable _AttendanceTable;
    private SwimTeamDatabase.AttendanceDataTable AttendanceTable
    {
        get
        {
            if (this._AttendanceTable == null)
                if (ViewState["AttendanceTable"] != null)
                    this._AttendanceTable = ((SwimTeamDatabase.AttendanceDataTable)ViewState["AttendanceTable"]);
            return this._AttendanceTable;
        }
        set
        {
            this._AttendanceTable = value;
            ViewState.Add("AttendanceTable", this._AttendanceTable);
        }
    }

    private int? _practiceOfTheDay;
    private int? PracticeOfTheDaySelection
    {
        get
        {
            if (this._practiceOfTheDay == null)
                if (ViewState["PracticeOfTheDay"] != null)
                    this._practiceOfTheDay = int.Parse(ViewState["PracticeOfTheDay"].ToString());
            return this._practiceOfTheDay;
        }
        set
        {
            this._practiceOfTheDay = value;
            ViewState.Add("PracticeOfTheDay", value);
        }
    }

    private void LoadAttendaceTableForDate(DateTime AttendanceDate)
    {
        if (this.PracticeOfTheDaySelection == null)
            this.PracticeOfTheDaySelection = 1;
        List<int> PracticesList = new List<int>();
        if (this.GroupPicked != null)
        {
            AttendanceBLL AttendanceAdapter = new AttendanceBLL();
            SwimTeamDatabase.AttendanceDataTable TempAttendanceTable = AttendanceAdapter.GetAttendancesForCertainGroupForCertainDate(
                int.Parse(this.GroupPicked.ToString()), AttendanceDate);

            if (TempAttendanceTable.Count == 0)
                this.AttendanceTable = TempAttendanceTable;
            else
            {
                SwimTeamDatabase.AttendanceDataTable SetAttendanceTable = new SwimTeamDatabase.AttendanceDataTable();
                for (int i = 0; i < TempAttendanceTable.Count; i++)
                {
                    if (TempAttendanceTable[i].PracticeoftheDay == this.PracticeOfTheDaySelection)
                    {
                        SwimTeamDatabase.AttendanceRow TempRow = SetAttendanceTable.NewAttendanceRow();
                        TempRow = AttendanceBLL.CopyRowToRow(TempAttendanceTable[i], TempRow);
                        SetAttendanceTable.AddAttendanceRow(TempRow);
                    }
                    if (!PracticesList.Contains(TempAttendanceTable[i].PracticeoftheDay))
                        PracticesList.Add(TempAttendanceTable[i].PracticeoftheDay);
                }
                this.AttendanceTable = SetAttendanceTable;
            }
        }
        if (this.PracticeOfTheDaySelection != null)
        {
            if (PracticesList.Count == 0)
                PracticesList.Add(1);
            if (!PracticesList.Contains(int.Parse(this.PracticeOfTheDaySelection.ToString())) && this.AddingPractice)
            {
                int MaxPracticeSoFar = 0;
                for (int i = 0; i < PracticesList.Count; i++)
                    if (MaxPracticeSoFar < PracticesList[i])
                        MaxPracticeSoFar = PracticesList[i];
                for (int i = MaxPracticeSoFar + 1; i <= this.PracticeOfTheDaySelection; i++)
                    PracticesList.Add(i);
            }
            else if (!this.AddingPractice)
            {
                int MaxPracticeSoFar = 0;
                for (int i = 0; i < PracticesList.Count; i++)
                    if (MaxPracticeSoFar < PracticesList[i])
                        MaxPracticeSoFar = PracticesList[i];
                if (this.PracticeOfTheDaySelection > MaxPracticeSoFar)
                    this.PracticeOfTheDaySelection = MaxPracticeSoFar;
            }
        }

        this.LoadPracticeOfTheDayDropDownList(PracticesList);
    }

    private void LoadPracticeOfTheDayDropDownList(List<int> PracticesList)
    {
        PracticeOfTheDayDropDownList.Items.Clear();
        int i = 1;
        do
        {
            ListItem TempItem = new ListItem(i.ToString(), i.ToString());
            PracticeOfTheDayDropDownList.Items.Add(TempItem);
            i++;
        } while (PracticesList.Contains(i));
        if (this.PracticeOfTheDaySelection != null)
            PracticeOfTheDayDropDownList.SelectedValue = this.PracticeOfTheDaySelection.ToString();
        else
            PracticeOfTheDayDropDownList.SelectedIndex = 0;

    }

    private void LoadLastTimeLaneDictionary()
    {
        if (this.GroupPicked != null)
        {
            AttendanceBLL AttendanceAdapter = new AttendanceBLL();
            SwimTeamDatabase.AttendanceDataTable Attendances =
                AttendanceAdapter.GetLastAttendanceForSwimmerInGroup(this.GroupPicked ?? -1);

            this.LastTimeLane = new Dictionary<string, int>();
            for (int i = 0; i < Attendances.Count; i++)
                if (!this.LastTimeLane.ContainsKey(Attendances[i].USAID))
                    if (!Attendances[i].IsLaneNull())
                        this.LastTimeLane.Add(Attendances[i].USAID, Attendances[i].Lane);
        }
    }


    private void CreateDisplayTable()
    {

        DataColumn USAIDColumn = this.Swimmers.Columns[UserControls_Attendance_AttendanceControl.Swimmers_USAIDColumn.ColumnName];
        DataColumn LastNameColumn = this.Swimmers.Columns[UserControls_Attendance_AttendanceControl.Swimmers_LastNameColumn.ColumnName];
        DataColumn PreferredNameColumn = this.Swimmers.Columns[UserControls_Attendance_AttendanceControl.Swimmers_PreferredNameColumn.ColumnName];

        if (this.Swimmers.Rows.Count != this.AttendanceTable.Count)
            throw new Exception("Tables do not match");

        this.Table1.Rows.Add(this.CreateHeaderRow());

        for (int i = 0; i < this.Swimmers.Rows.Count; i++)
        {
            String SwimmersUSAID = this.Swimmers.Rows[i].Field<String>(USAIDColumn);
            String SwimmersLastName = this.Swimmers.Rows[i].Field<String>(LastNameColumn);
            String SwimmersPreferredName = this.Swimmers.Rows[i].Field<String>(PreferredNameColumn);


            TableRow Row = new TableRow();
            Row.ID = SwimmersUSAID + "Row";

            TableCell NameCell = new TableCell();
            NameCell.ID = "Row" + i + "NameColumnCell";

            HiddenField USAIDHiddenField = new HiddenField();
            USAIDHiddenField.ID = SwimmersUSAID + "HiddenField";
            USAIDHiddenField.Value = SwimmersUSAID;
            NameCell.Controls.Add(USAIDHiddenField);

            Label NameLabel = new Label();
            NameLabel.ID = SwimmersUSAID + "NameLabel";
            NameLabel.Text = SwimmersPreferredName + " " + SwimmersLastName;
            NameCell.Controls.Add(NameLabel);

            Row.Cells.Add(NameCell);

            TableCell PresentCell = new TableCell();
            PresentCell.ID = SwimmersUSAID + "PresentCell";
            CheckBox PresentCheckBox = new CheckBox();
            PresentCheckBox.ID = SwimmersUSAID + "CheckBox";
            if (this.AttendanceTable[i].AttendanceType != "A" && this.AttendanceTable[i].AttendanceType != "$")
                PresentCheckBox.Checked = true;
            else
                PresentCheckBox.Checked = false;
            PresentCheckBox.Attributes.Add("onclick", "EnableRow(this)");
            PresentCell.Controls.Add(PresentCheckBox);
            Row.Cells.Add(PresentCell);

            TableCell LaneCell = new TableCell();
            LaneCell.ID = SwimmersUSAID + NameCell;
            TextBox LaneTextBox = new TextBox();
            LaneTextBox.ID = SwimmersUSAID + "LaneTextBox";
            LaneTextBox.Columns = 1;
            if (this.AttendanceTable[i].IsLaneNull())
            {
                if (this.LastTimeLane.ContainsKey(SwimmersUSAID))
                    LaneTextBox.Text = this.LastTimeLane[SwimmersUSAID].ToString();
            }
            else
            {
                LaneTextBox.Text = this.AttendanceTable[i].Lane.ToString();
            }
            LaneCell.Controls.Add(LaneTextBox);
            Row.Cells.Add(LaneCell);

            TableCell AttendanceTypeCell = new TableCell();
            AttendanceTypeCell.ID = SwimmersUSAID + "TypeCell";
            DropDownList TypeList = new DropDownList();
            TypeList.ID = SwimmersUSAID + "TypeDropDownList";
            //ListItem AbsentListItem = new ListItem("Absent", "A");
            //TypeList.Items.Add(AbsentListItem);
            ListItem PresentListItem = new ListItem("Present", "X");
            TypeList.Items.Add(PresentListItem);
            ListItem LateListItem = new ListItem("Late", "L");
            TypeList.Items.Add(LateListItem);
            ListItem VeryLateListItem = new ListItem("Very Late", "VL");
            TypeList.Items.Add(VeryLateListItem);
            ListItem LeftEarlyListItem = new ListItem("Left Early", "LE");
            TypeList.Items.Add(LeftEarlyListItem);
            ListItem SatOutALotListItem = new ListItem("Sat out a lot", "SO");
            TypeList.Items.Add(SatOutALotListItem);
            ListItem ExcusedAbsenceListItem = new ListItem("Excused Abscence", "EA");
            TypeList.Items.Add(ExcusedAbsenceListItem);
            ListItem BonusPracticeListItem = new ListItem("Bonus Attendance", "BA");
            TypeList.Items.Add(BonusPracticeListItem);

            switch (this.AttendanceTable[i].AttendanceType)
            {
                case (null):
                    TypeList.Enabled = false;
                    break;
                case "A":
                    TypeList.Enabled = false;
                    break;
                case "X":
                    TypeList.SelectedValue = PresentListItem.Value;
                    break;
                case "L":
                    TypeList.SelectedValue = LateListItem.Value;
                    break;
                case "VL":
                    TypeList.SelectedValue = VeryLateListItem.Value;
                    break;
                case "LE":
                    TypeList.SelectedValue = LeftEarlyListItem.Value;
                    break;
                case "SO":
                    TypeList.SelectedValue = SatOutALotListItem.Value;
                    break;
                case "EA":
                    TypeList.SelectedValue = ExcusedAbsenceListItem.Value;
                    break;
                case "BA":
                    TypeList.SelectedValue = BonusPracticeListItem.Value;
                    break;
                default:
                    TypeList.Enabled = false;
                    break;
            }
            AttendanceTypeCell.Controls.Add(TypeList);
            Row.Cells.Add(AttendanceTypeCell);

            TableCell YardageCell = new TableCell();
            YardageCell.ID = SwimmersUSAID + "YardageCell";
            TextBox YardageTextBox = new TextBox();
            YardageTextBox.ID = SwimmersUSAID + "YardageTextBox";
            YardageTextBox.Columns = 5;
            DropDownList YMDropDownList = new DropDownList();
            YMDropDownList.ID = SwimmersUSAID + "YMDropDownList";
            ListItem NothingListItem = new ListItem("", "B");
            YMDropDownList.Items.Add(NothingListItem);
            ListItem YardsListItem = new ListItem("Yards", "Y");
            YMDropDownList.Items.Add(YardsListItem);
            ListItem MetersListItem = new ListItem("Meters", "M");
            YMDropDownList.Items.Add(MetersListItem);
            if (!this.AttendanceTable[i].IsYardsNull())
            {
                YardageTextBox.Text = this.AttendanceTable[i].Yards.ToString();
                YMDropDownList.SelectedValue = YardsListItem.Value;
            }
            else if (!this.AttendanceTable[i].IsMetersNull())
            {
                YardageTextBox.Text = this.AttendanceTable[i].Meters.ToString();
                YMDropDownList.SelectedValue = MetersListItem.Value;
            }
            else
            {
                YardageTextBox.Text = "";
                YMDropDownList.SelectedValue = NothingListItem.Value;
            }
            YardageCell.Controls.Add(YardageTextBox);
            YardageCell.Controls.Add(YMDropDownList);
            Row.Cells.Add(YardageCell);

            TableCell NotesCell = new TableCell();
            NotesCell.ID = SwimmersUSAID + "NotesCell";
            TextBox NotesTextBox = new TextBox();
            NotesTextBox.ID = SwimmersUSAID + "NotesTextBox";
            NotesTextBox.MaxLength = 100;
            NotesTextBox.Width = new Unit(340.0, UnitType.Pixel);
            if (!this.AttendanceTable[i].IsNoteNull())
            {
                String Notes = this.AttendanceTable[i].Note;
                if (Notes.Contains("SYSTEM"))
                    Notes = Notes.Substring(0, Notes.IndexOf("SYSTEM"));
                NotesTextBox.Text = Notes;
            }
            NotesCell.Controls.Add(NotesTextBox);
            Row.Cells.Add(NotesCell);

            switch (this.AttendanceTable[i].AttendanceType)
            {
                case "X":
                case "L":
                case "VL":
                case "LE":
                case "SO":
                case "EA":
                case "BA":
                    LaneTextBox.Enabled = true;
                    YardageTextBox.Enabled = true;
                    YMDropDownList.Enabled = true;
                    NotesTextBox.Enabled = true;
                    break;
                default:
                    LaneTextBox.Enabled = false;
                    YardageTextBox.Enabled = false;
                    YMDropDownList.Enabled = false;
                    NotesTextBox.Enabled = false;
                    break;
            }

            this.Table1.Rows.Add(Row);

            //Don't know why, but the YMDropDownList looses its selected value when the row is added to the table
            DropDownList Temp = ((DropDownList)this.Table1.FindControl(SwimmersUSAID + "YMDropDownList"));
            if (!this.AttendanceTable[i].IsYardsNull())
            {
                Temp.SelectedValue = YardsListItem.Value;
            }
            else if (!this.AttendanceTable[i].IsMetersNull())
            {
                Temp.SelectedValue = MetersListItem.Value;
            }
            else
            {
                Temp.SelectedValue = NothingListItem.Value;
            }

            //So is the attendance Type drop down list
            DropDownList AnotherTemp = ((DropDownList)this.Table1.FindControl(SwimmersUSAID + "TypeDropDownList"));
            switch (this.AttendanceTable[i].AttendanceType)
            {
                case (null):
                    AnotherTemp.Enabled = false;
                    break;
                case "A":
                    AnotherTemp.Enabled = false;
                    break;
                case "X":
                    AnotherTemp.SelectedValue = PresentListItem.Value;
                    break;
                case "L":
                    AnotherTemp.SelectedValue = LateListItem.Value;
                    break;
                case "VL":
                    AnotherTemp.SelectedValue = VeryLateListItem.Value;
                    break;
                case "LE":
                    AnotherTemp.SelectedValue = LeftEarlyListItem.Value;
                    break;
                case "SO":
                    AnotherTemp.SelectedValue = SatOutALotListItem.Value;
                    break;
                case "EA":
                    AnotherTemp.SelectedValue = ExcusedAbsenceListItem.Value;
                    break;
                case "BA":
                    AnotherTemp.SelectedValue = BonusPracticeListItem.Value;
                    break;
                default:
                    AnotherTemp.Enabled = false;
                    break;
            }
        }

        this.Table1.Rows.Add(this.CreateFooterRow());
    }

    private TableRow CreateHeaderRow()
    {
        TableRow HeaderRow = new TableRow();
        HeaderRow.ID = "HeaderRow";

        TableCell HeaderRowNameCell = new TableCell();
        HeaderRowNameCell.ID = "HeaderRowNameCell";
        Label HeaderRowNameLabel = new Label();
        HeaderRowNameLabel.ID = "HeaderRowNameLabel";
        HeaderRowNameLabel.Text = "Name";
        HeaderRowNameCell.Controls.Add(HeaderRowNameLabel);
        HeaderRow.Cells.Add(HeaderRowNameCell);

        TableCell HeaderRowPresentCell = new TableCell();
        HeaderRowPresentCell.ID = "HeaderRowPresentCell";
        Label HeaderRowPresentLabel = new Label();
        HeaderRowPresentLabel.Text = "Present";
        HeaderRowPresentCell.Controls.Add(HeaderRowPresentLabel);
        HeaderRow.Cells.Add(HeaderRowPresentCell);

        TableCell HeaderRowLaneCell = new TableCell();
        HeaderRowLaneCell.ID = "HeaderRowLaneCell";
        Label HeaderRowLaneLabel = new Label();
        HeaderRowLaneLabel.Text = "Lane";
        HeaderRowLaneCell.Controls.Add(HeaderRowLaneLabel);
        HeaderRow.Cells.Add(HeaderRowLaneCell);

        TableCell HeaderRowTypeCell = new TableCell();
        HeaderRowTypeCell.ID = "HeaderRowTypeCell";
        Label HeaderRowTypeLabel = new Label();
        HeaderRowTypeLabel.ID = "HeaderRowTypeLabel";
        HeaderRowTypeLabel.Text = "Attendance Type";
        HeaderRowTypeCell.Controls.Add(HeaderRowTypeLabel);
        HeaderRow.Cells.Add(HeaderRowTypeCell);

        TableCell HeaderRowYardageCell = new TableCell();
        HeaderRowYardageCell.ID = "HeaderRowYardageCell";
        Label HeaderRowYardageLabel = new Label();
        HeaderRowYardageLabel.ID = "HeaderRowYardageLabel";
        HeaderRowYardageLabel.Text = "Yardage";
        HeaderRowYardageCell.Controls.Add(HeaderRowYardageLabel);
        HeaderRow.Cells.Add(HeaderRowYardageCell);

        TableCell HeaderRowNotesCell = new TableCell();
        HeaderRowNotesCell.ID = "HeaderRowNotesCell";
        Label HeaderRowNotesLabel = new Label();
        HeaderRowNotesLabel.ID = "HeaderRowNotesLabel";
        HeaderRowNotesLabel.Text = "Notes";
        HeaderRowNotesCell.Controls.Add(HeaderRowNotesLabel);
        HeaderRow.Cells.Add(HeaderRowNotesCell);

        return HeaderRow;
    }
    private TableRow CreateFooterRow()
    {
        TableRow Row = new TableRow();
        Row.ID = "FooterRow";

        TableCell FooterCell = new TableCell();
        FooterCell.ID = "FooterCell";
        FooterCell.ColumnSpan = 6;

        Label CounterLabel = new Label();
        CounterLabel.ID = "CounterLabel";
        CounterLabel.Text = "Total: ";
        FooterCell.Controls.Add(CounterLabel);

        Label Counter = new Label();
        Counter.ID = "Counter";
        FooterCell.Controls.Add(Counter);

        Row.Cells.Add(FooterCell);

        return Row;
    }

    private void MatchSwimmersAndAttendanceTableOrder()
    {
        if (this.AttendanceTable != null)
        {

            if (this.Swimmers.Rows.Count < this.AttendanceTable.Count)
                throw new Exception("We should never reach this.");
            else
            {
                SwimTeamDatabase.AttendanceDataTable TempAttendanceTable = new SwimTeamDatabase.AttendanceDataTable();
                int NewPrimaryKey = -1;
                for (int i = 0; i < this.Swimmers.Rows.Count; i++)
                {
                    bool SwimmerFound = false;
                    String USAID = this.Swimmers.Rows[i].Field<String>(0);

                    //Starting at the top of the swimmer table, go through and find a match in the attendance table
                    for (int j = 0; j < this.AttendanceTable.Count; j++)
                    {
                        if (USAID ==
                                this.AttendanceTable[j].USAID)
                        {
                            SwimTeamDatabase.AttendanceRow temprow = TempAttendanceTable.NewAttendanceRow();
                            temprow = AttendanceBLL.CopyRowToRow(this.AttendanceTable[j], temprow);
                            TempAttendanceTable.AddAttendanceRow(temprow);
                            SwimmerFound = true;
                            break;
                        }
                    }

                    //If we get here, the swimmer has no attendance record for the day.
                    if (!SwimmerFound)
                    {
                        SwimTeamDatabase.AttendanceRow temprow = TempAttendanceTable.NewAttendanceRow();
                        bool Errors = false;


                        temprow.USAID = USAID;
                        temprow.Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
                        temprow.PracticeoftheDay = -1;
                        temprow.GroupID = -1;
                        temprow.AttendanceType = "$";
                        do
                        {
                            try
                            {
                                temprow.AttendanceID = NewPrimaryKey;
                                TempAttendanceTable.AddAttendanceRow(temprow);
                                Errors = false;
                                NewPrimaryKey--;
                            }
                            catch (ConstraintException)
                            {
                                Errors = true;
                                NewPrimaryKey--;
                            }
                        } while (Errors);
                    }
                }

                this.AttendanceTable = TempAttendanceTable;
                this.AttendanceTable.AcceptChanges();
            }
        }
    }

    private DataTable EmptySwimmerTable()
    {
        DataTable SwimmersDataTable = new DataTable("SwimmersDataTable");

        DataColumn Swimmers_USAIDColumn = UserControls_Attendance_AttendanceControl.Swimmers_USAIDColumn;
        DataColumn Swimmers_LastNameColumn = UserControls_Attendance_AttendanceControl.Swimmers_LastNameColumn;
        DataColumn Swimmers_PreferredNameColumn = UserControls_Attendance_AttendanceControl.Swimmers_PreferredNameColumn;

        SwimmersDataTable.Columns.Add(Swimmers_USAIDColumn);
        SwimmersDataTable.Columns.Add(Swimmers_LastNameColumn);
        SwimmersDataTable.Columns.Add(Swimmers_PreferredNameColumn);

        DataColumn[] keys = new DataColumn[1];
        keys[0] = Swimmers_USAIDColumn;
        SwimmersDataTable.PrimaryKey = keys;

        return SwimmersDataTable;
    }


    protected void PracticeOfTheDayDropDownListSelectionChanged(object sender, EventArgs e)
    {
        this.PracticeOfTheDaySelection = int.Parse(this.PracticeOfTheDayDropDownList.SelectedValue);
    }


    bool AddingPractice = false;
    protected void AddPracticeButtonClicked(object sender, EventArgs e)
    {
        if (this.AttendanceTable != null)
        {
            int NonAbsent = 0;
            for (int i = 0; i < this.AttendanceTable.Count; i++)
                if (this.AttendanceTable[i].AttendanceType != "A" && this.AttendanceTable[i].AttendanceType != "$")
                {
                    NonAbsent++;
                    i = this.AttendanceTable.Count;
                }
            if (NonAbsent > 0)
            {
                int PracticeNumberToAdd = int.Parse(PracticeOfTheDayDropDownList.Items[PracticeOfTheDayDropDownList.Items.Count - 1].Value) + 1;
                PracticeOfTheDayDropDownList.Items.Add(new ListItem(PracticeNumberToAdd.ToString(), PracticeNumberToAdd.ToString()));
                PracticeOfTheDayDropDownList.SelectedValue = PracticeNumberToAdd.ToString();
                this.PracticeOfTheDaySelection = PracticeNumberToAdd;
                AddingPractice = true;
            }
            else
            {
                this.SaveFormValues();
                this.AddPracticeErrorLabel.Text = "&nbsp;Cannot add practice. No practice saved for the current displayed practice." +
                    "You must first save the practice below before you can add another.";
                this.AddPracticeErrorLabel.Visible = true;
                //this.ReloadFromPreviousFormValues = true;
            }
        }

        this.GetValuesFromStatus = GetValuesFrom.Database;
    }

    private void AddJavaScript()
    {
        HtmlGenericControl body = (HtmlGenericControl)Page.Master.FindControl("Body");
        body.Attributes.Add("onload", "CountSwimmersHere()");

        String csName = "CheckBoxControlClickScript";
        Type csType = this.GetType();
        ClientScriptManager cs = Page.ClientScript;

        if (!cs.IsClientScriptBlockRegistered(csName))
        {
            StringBuilder csText = new StringBuilder();
            csText.Append("<script type=\"text/javascript\">"); csText.Append("\n");
            csText.Append("function EnableRow(sender) {"); csText.Append("\n");
            csText.Append("\tvar SenderID = sender.id.toString();"); csText.Append("\n");
            csText.Append("\tvar index = SenderID.indexOf(\"CheckBox\");"); csText.Append("\n");
            csText.Append("\tvar NamingContainer = SenderID.substr(0, index).toString();"); csText.Append("\n");
            csText.Append("\tvar IDToLookFor = NamingContainer.concat(\"TypeDropDownList\");"); csText.Append("\n");
            csText.Append("\tvar AttendanceTypeDropDownList = document.getElementById(IDToLookFor);"); csText.Append("\n");
            csText.Append("\tIDToLookFor = NamingContainer.concat(\"LaneTextBox\");"); csText.Append("\n");
            csText.Append("\tvar LaneTextBox = document.getElementById(IDToLookFor);"); csText.Append("\n");
            csText.Append("\tIDToLookFor = NamingContainer.concat(\"YardageTextBox\");"); csText.Append("\n");
            csText.Append("\tvar YardageTextBox = document.getElementById(IDToLookFor);"); csText.Append("\n");
            csText.Append("\tIDToLookFor = NamingContainer.concat(\"YMDropDownList\");"); csText.Append("\n");
            csText.Append("\tvar YMDropDownList = document.getElementById(IDToLookFor);"); csText.Append("\n");
            csText.Append("\tIDToLookFor = NamingContainer.concat(\"NotesTextBox\");"); csText.Append("\n");
            csText.Append("\tvar NotesTextBox = document.getElementById(IDToLookFor);"); csText.Append("\n");
            csText.Append("\n");
            csText.Append("\tif (sender.checked == true) {"); csText.Append("\n");
            {
                csText.Append("\n");
                csText.Append("\t\tAttendanceTypeDropDownList.disabled = false;"); csText.Append("\n");
                csText.Append("\t\tLaneTextBox.disabled = false;"); csText.Append("\n");
                csText.Append("\t\tYardageTextBox.disabled = false;"); csText.Append("\n");
                csText.Append("\t\tYMDropDownList.disabled = false;"); csText.Append("\n");
                csText.Append("\t\tNotesTextBox.disabled = false;"); csText.Append("\n");

            } csText.Append("\t}"); csText.Append(" else {"); csText.Append("\n");
            {
                csText.Append("\t\tAttendanceTypeDropDownList.disabled = true;"); csText.Append("\n");
                csText.Append("\t\tLaneTextBox.disabled = true;"); csText.Append("\n");
                csText.Append("\t\tYardageTextBox.disabled = true;"); csText.Append("\n");
                csText.Append("\t\tYMDropDownList.disabled = true;"); csText.Append("\n");
                csText.Append("\t\tNotesTextBox.disabled = true;"); csText.Append("\n");
            } csText.Append("\t}"); csText.Append("\n\n");
            csText.Append("\tCountSwimmersHere();\n");

            //csText.Append("var TestBox = document.getElementById(\"ctl00_MainContent_AttendanceControl1_Tester\");"); csText.Append("\n");
            //csText.Append("TestBox.value = YardageTextBox.disabled.toString();"); csText.Append("\n");
            csText.Append("}"); csText.Append("\n");
            csText.Append("\n\n");
            csText.Append("function CountSwimmersHere()\n");
            csText.Append("{\n");
            csText.Append("\tvar Inputs = document.getElementsByTagName(\"input\");\n");
            csText.Append("\tvar Count = 0;\n");
            csText.Append("\tfor(i = 0; i < Inputs.length; i++)\n");
            csText.Append("\t{\n");
            csText.Append("\t\tif(Inputs[i].name.toString().indexOf(\"CheckBox\") != -1)\n");
            csText.Append("\t\t\tif(Inputs[i].checked)\n");
            csText.Append("\t\t\t\tCount++;\n");
            csText.Append("\t}\n");
            csText.Append("\tvar Counter = document.getElementById(\"ctl00_MainContent_AttendanceControl1_Counter\");\n");
            csText.Append("\tCounter.innerHTML = Count.toString();\n");
            csText.Append("}\n");
            csText.Append("</script>");
            cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
        }
    }
    protected void AssignYardageToLanesButtonClicked(object sender, EventArgs e)
    {
        List<int> LaneList = GetListOfLanesToSetValuesFor();
        if (LaneList.Count > 0)
        {
            this.SaveFormValues();
            this.CreateTableForLanes(LaneList);

            this.AddSwimmerPanel.Visible = false;
            this.ChangeDefaultGroupPanel.Visible = false;
            this.LanePanel.Visible = true;
            this.RegularPanel.Visible = false;
        }

        this.GetValuesFromStatus = GetValuesFrom.Neither;
    }

    private List<int> GetListOfLanesToSetValuesFor()
    {
        List<int> LaneList = new List<int>();
        for (int i = 0; i < Request.Form.Keys.Count; i++)
        {
            String Key = Request.Form.AllKeys.ElementAt<String>(i);
            if (Key.Contains("LaneTextBox"))
            {
                int lane;
                if (int.TryParse(Request.Form[Key], out lane))
                    if (!LaneList.Contains(lane))
                        LaneList.Add(lane);
            }
        }
        List<int> TempLaneList = LaneList;
        LaneList = new List<int>();
        while (TempLaneList.Count > 0)
        {
            int MinNumber = TempLaneList[0];
            if (TempLaneList.Count > 1)
            {
                for (int i = 0; i < TempLaneList.Count; i++)
                    if (MinNumber > TempLaneList[i])
                        MinNumber = TempLaneList[i];

            }
            LaneList.Add(MinNumber);
            TempLaneList.Remove(MinNumber);
        }

        return LaneList;
    }
    private void CreateTableForLanes(List<int> LaneList)
    {
        TableRow HeaderRow = new TableRow();
        HeaderRow.ID = "LaneHeaderRow";
        TableCell HeaderLaneLabelCell = new TableCell();
        HeaderLaneLabelCell.ID = "HeaderLaneLabelCell";
        Label HeaderLaneLabel = new Label();
        HeaderLaneLabel.ID = "HeaderLaneLabel";
        HeaderLaneLabel.Text = "Lane";
        HeaderLaneLabelCell.Controls.Add(HeaderLaneLabel);
        HeaderRow.Cells.Add(HeaderLaneLabelCell);

        TableCell HeaderYardageCell = new TableCell();
        HeaderYardageCell.ID = "HeaderYardageCell";
        Label HeaderYardageLabel = new Label();
        HeaderYardageCell.ID = "HeaderYardageLabel";
        HeaderYardageLabel.Text = "Value";
        HeaderYardageCell.Controls.Add(HeaderYardageLabel);
        HeaderRow.Cells.Add(HeaderYardageCell);
        this.LaneTable.Rows.Add(HeaderRow);


        for (int i = 0; i < LaneList.Count; i++)
        {
            TableRow Row = new TableRow();
            Row.ID = "Row_" + i;

            TableCell LaneLabelCell = new TableCell();
            LaneLabelCell.ID = "LaneLabelCell_" + i;
            Label LaneLabel = new Label();
            LaneLabel.ID = "LaneLabel_" + i;
            LaneLabel.Text = LaneList[i].ToString();
            LaneLabelCell.Controls.Add(LaneLabel);

            HiddenField LaneLabelHiddenField = new HiddenField();
            LaneLabelHiddenField.ID = "LaneLabelHiddenField_" + i;
            LaneLabelHiddenField.Value = LaneList[i].ToString();
            LaneLabelCell.Controls.Add(LaneLabelHiddenField);
            Row.Cells.Add(LaneLabelCell);

            TableCell YardageCell = new TableCell();
            YardageCell.ID = "YardageCell_" + i;
            TextBox YardageTextBox = new TextBox();
            YardageTextBox.ID = "YardageTextBox_" + i;
            YardageCell.Controls.Add(YardageTextBox);
            Row.Cells.Add(YardageCell);

            this.LaneTable.Rows.Add(Row);
        }
    }

    private bool ReloadFromPreviousFormValues = false;
    private bool UpdateYardages = false;
    private Dictionary<int, String> Lane_YardageDictionary;
    protected void AssignYardagesButtonclicked(object sender, EventArgs e)
    {
        this.ReloadFromPreviousFormValues = true;
        this.UpdateYardages = true;

        this.Lane_YardageDictionary = new Dictionary<int, string>();

        for (int i = 0; i < Request.Form.Keys.Count; i++)
        {
            if (Request.Form.Keys[i].Contains("LaneLabelHiddenField_"))
            {
                int lane = int.Parse(Request.Form[Request.Form.Keys[i]]);
                i++;
                String yardage = Request.Form[Request.Form.Keys[i]];
                yardage = yardage + YardsMetersDropDownList.SelectedValue;
                yardage = yardage.Replace(",", "");
                this.Lane_YardageDictionary.Add(lane, yardage);
            }
        }

        this.AddSwimmerPanel.Visible = false;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;
        this.RegularPanel.Visible = true;

        this.GetValuesFromStatus = GetValuesFrom.Form;
    }

    private void ReassignYardagesToAllFromPreviousPage()
    {
        for (int i = 1; i < this.Table1.Rows.Count - 1; i++)
        {
            TextBox LaneTextBox = ((TextBox)this.Table1.Rows[i].Cells[2].Controls[0]);

            if (((CheckBox)this.Table1.Rows[i].Cells[1].Controls[0]).Checked)
            {
                if (!String.IsNullOrEmpty(LaneTextBox.Text))
                {
                    TextBox YardageTextBox = ((TextBox)this.Table1.Rows[i].Cells[4].Controls[0]);
                    DropDownList YMDropDownList = ((DropDownList)this.Table1.Rows[i].Cells[4].Controls[1]);

                    int Lane;
                    if (int.TryParse(LaneTextBox.Text, out Lane))
                    {
                        if (this.Lane_YardageDictionary.ContainsKey(Lane))
                        {
                            String Yardage = this.Lane_YardageDictionary[Lane];
                            if (Yardage.Contains("Y"))
                            {
                                String PreviousYMValue = YMDropDownList.SelectedValue;
                                YMDropDownList.SelectedValue = "Y";
                                Yardage = Yardage.Replace("Y", "");
                                int Yards;
                                if (int.TryParse(Yardage, out Yards))
                                {
                                    if (YMDropDownList.SelectedValue == PreviousYMValue)
                                    {
                                        int PreviousYards;
                                        if (int.TryParse(YardageTextBox.Text, out PreviousYards))
                                        {
                                            if (PreviousYards < 0)
                                                Yards = PreviousYards + Yards;
                                            YardageTextBox.Text = Yards.ToString();
                                        }
                                        else
                                            YardageTextBox.Text = Yards.ToString();
                                    }
                                    else
                                        YardageTextBox.Text = Yards.ToString();
                                }
                            }
                            else if (Yardage.Contains("M"))
                            {
                                String PreviousYMValue = YMDropDownList.SelectedValue;
                                YMDropDownList.SelectedValue = "M";
                                Yardage = Yardage.Replace("M", "");
                                int Meters;
                                if (int.TryParse(Yardage, out Meters))
                                {
                                    if (YMDropDownList.SelectedValue == PreviousYMValue)
                                    {
                                        int PreviousMeters;
                                        if (int.TryParse(YardageTextBox.Text, out PreviousMeters))
                                        {
                                            if (PreviousMeters < 0)
                                                Meters = PreviousMeters + Meters;
                                            YardageTextBox.Text = Meters.ToString();
                                        }
                                        else
                                            YardageTextBox.Text = Meters.ToString();
                                    }
                                    else
                                        YardageTextBox.Text = Meters.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private Dictionary<String, String> _FormValues;
    private Dictionary<String, String> FormValues
    {
        get
        {
            if (this._FormValues == null)
                if (ViewState["FormValues"] != null)
                    this._FormValues = ((Dictionary<String, String>)ViewState["FormValues"]);
            return this._FormValues;
        }
        set
        {
            this._FormValues = value;
            if (ViewState["FormValues"] == null)
                ViewState.Add("FormValues", value);
            else
                ViewState["FormValues"] = value;
        }
    }
    private void SaveFormValues()
    {
        if (this.FormValues != null)
            this.FormValues.Clear();

        this.SavedDynamicNamingContainer = this.DynamicNamingContainer;
        if (this.FormValues == null)
            FormValues = new Dictionary<string, string>();
        for (int i = 0; i < Request.Form.Keys.Count; i++)
        {
            String Key = Request.Form.Keys[i];
            if (Key.Contains(this.DynamicNamingContainer))
            {
                //if(!String.IsNullOrEmpty(Request.Form[Key]))
                this.FormValues.Add(Key, Request.Form[Key]);
            }
        }
    }

    //private bool ReloadFromPreviousFormValues = false;
    private String _SavedDynamicNamingContainer;
    private String SavedDynamicNamingContainer
    {
        get
        {
            if (this._SavedDynamicNamingContainer == null)
                if (ViewState["SavedDynamicNamingContainer"] != null)
                    this._SavedDynamicNamingContainer = ((String)ViewState["SavedDynamicNamingContainer"]);
            return this._SavedDynamicNamingContainer;
        }
        set
        {
            this._SavedDynamicNamingContainer = value;
            if (ViewState["SavedDynamicNamingContainer"] == null)
                ViewState.Add("SavedDynamicNamingContainer", value);
            else
                ViewState["SavedDynamicNamingContainer"] = value;
        }
    }
    private void LoadFromSavedFormValues()
    {
        for (int i = 1; i < Table1.Rows.Count - 1; i++)
        {
            CheckBox PresentCheckBox = ((CheckBox)Table1.Rows[i].Cells[1].Controls[0]);
            PresentCheckBox.Checked = this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + PresentCheckBox.ID);

            DropDownList TypeDropDownList = ((DropDownList)Table1.Rows[i].Cells[3].Controls[0]);
            if (this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + TypeDropDownList.ID))
                TypeDropDownList.SelectedValue = this.FormValues[this.SavedDynamicNamingContainer + TypeDropDownList.ID];


            TextBox LaneTextBox = ((TextBox)Table1.Rows[i].Cells[2].Controls[0]);
            if (this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + LaneTextBox.ID))
                LaneTextBox.Text = this.FormValues[this.SavedDynamicNamingContainer + LaneTextBox.ID];

            TextBox YardageTextBox = ((TextBox)Table1.Rows[i].Cells[4].Controls[0]);
            if (this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + YardageTextBox.ID))
                YardageTextBox.Text = this.FormValues[this.SavedDynamicNamingContainer + YardageTextBox.ID];

            DropDownList YMDropDownList = ((DropDownList)Table1.Rows[i].Cells[4].Controls[1]);
            if (this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + YMDropDownList.ID))
                YMDropDownList.SelectedValue = this.FormValues[this.SavedDynamicNamingContainer + YMDropDownList.ID];

            TextBox NotesTextBox = ((TextBox)Table1.Rows[i].Cells[5].Controls[0]);
            if (this.FormValues.ContainsKey(this.SavedDynamicNamingContainer + NotesTextBox.ID))
                NotesTextBox.Text = this.FormValues[this.SavedDynamicNamingContainer + NotesTextBox.ID];

            if (PresentCheckBox.Checked)
            {
                TypeDropDownList.Enabled = true;
                LaneTextBox.Enabled = true;
                YardageTextBox.Enabled = true;
                YMDropDownList.Enabled = true;
                NotesTextBox.Enabled = true;
            }


        }

        this.FormValues.Clear();
    }
    protected void SwitchToAddNewSwimmerPanel(object sender, EventArgs e)
    {
        this.AddSwimmerPanel.Visible = true;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;
        this.RegularPanel.Visible = false;
        this.NewSwimmerFirstNameTextBox.Text = "";
        this.NewSwimmerLastNameTextBox.Text = "";

        this.SaveFormValues();

        this.GetValuesFromStatus = GetValuesFrom.Neither;
    }


    private List<String> _NewSwimmerUSAIDs;
    private List<String> NewSwimmerUSAIDs
    {
        get
        {
            if (this._NewSwimmerUSAIDs == null)
                if (ViewState["NewSwimmerUSAIDs"] != null)
                    this._NewSwimmerUSAIDs = ((List<String>)ViewState["NewSwimmerUSAIDs"]);
            return this._NewSwimmerUSAIDs;
        }
        set
        {
            this._NewSwimmerUSAIDs = value;
            ViewState["NewSwimmerUSAIDs"] = value;
        }
    }
    private List<String> _NewSwimmerFirstName;
    private List<String> NewSwimmerFirstName
    {
        get
        {
            if (this._NewSwimmerFirstName == null)
                if (ViewState["NewSwimmerFirstName"] != null)
                    this._NewSwimmerFirstName = ((List<String>)ViewState["NewSwimmerFirstName"]);
            return this._NewSwimmerFirstName;
        }
        set
        {
            this._NewSwimmerFirstName = value;
            if (ViewState["NewSwimmerFirstName"] == null)
                ViewState.Add("NewSwimmerFirstName", value);
            else
                ViewState["NewSwimmerFirstName"] = value;
        }
    }
    private List<String> _NewSwimmerLastName;
    private List<String> NewSwimmerLastName
    {
        get
        {
            if (this._NewSwimmerLastName == null)
                if (ViewState["NewSwimmerLastName"] != null)
                    this._NewSwimmerLastName = ((List<String>)ViewState["NewSwimmerLastName"]);
            return this._NewSwimmerLastName;
        }
        set
        {
            this._NewSwimmerLastName = value;
            if (ViewState["NewSwimmerLastName"] == null)
                ViewState.Add("NewSwimmerLastName", value);
            else
                ViewState["NewSwimmerLastName"] = value;
        }
    }

    protected void AddSwimmerButtonClicked(object sender, EventArgs e)
    {
        String RandomUSAID = RandomUSAIDCreator.GetRandomUSAID();

        if (this.NewSwimmerUSAIDs == null)
            this.NewSwimmerUSAIDs = new List<string>();
        if (this.NewSwimmerFirstName == null)
            this.NewSwimmerFirstName = new List<string>();
        if (this.NewSwimmerLastName == null)
            this.NewSwimmerLastName = new List<string>();

        NewSwimmerUSAIDs.Add(RandomUSAID);
        NewSwimmerFirstName.Add(NewSwimmerFirstNameTextBox.Text);
        NewSwimmerLastName.Add(NewSwimmerLastNameTextBox.Text);



        if (this.NewSwimmerUSAIDs != null)
        {
            for (int i = 0; i < this.NewSwimmerUSAIDs.Count; i++)
            {
                DataRow NewRow = this.Swimmers.NewRow();
                NewRow[Swimmers_USAIDColumn.ColumnName] = this.NewSwimmerUSAIDs[i];
                NewRow[Swimmers_LastNameColumn.ColumnName] = this.NewSwimmerLastName[i];
                NewRow[Swimmers_PreferredNameColumn.ColumnName] = this.NewSwimmerFirstName[i];

                this.Swimmers.Rows.Add(NewRow);
            }
        }

        //this.ReloadFromPreviousFormValues = true;

        this.AddSwimmerPanel.Visible = false;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;
        this.RegularPanel.Visible = true;

        this.GetValuesFromStatus = GetValuesFrom.Form;
    }
    protected void CancelAddSwimmerButtonClicked(object sender, EventArgs e)
    {
        //this.ReloadFromPreviousFormValues = true;

        this.AddSwimmerPanel.Visible = false;
        this.ChangeDefaultGroupPanel.Visible = false;
        this.LanePanel.Visible = false;
        this.RegularPanel.Visible = true;

        this.GetValuesFromStatus = GetValuesFrom.Form;
    }
    protected void SavePracticeButtonClicked(object sender, EventArgs e)
    {
        //this.SaveFormValues();
        AttendanceBLL AttendanceAdapter = new AttendanceBLL();
        if (this.Swimmers.Rows.Count > 0)
            AttendanceAdapter.BeginBatchInsert();

        List<int> DeleteInsertIndexes = new List<int>();
        List<int> DeleteIndexes = new List<int>();
        for (int i = 0; i < this.Swimmers.Rows.Count; i++)
        {
            String USAID = this.Swimmers.Rows[i].Field<String>(Swimmers_USAIDColumn.ColumnName);
            String FirstName = this.Swimmers.Rows[i].Field<String>(Swimmers_PreferredNameColumn.ColumnName);
            String LastName = this.Swimmers.Rows[i].Field<String>(Swimmers_LastNameColumn.ColumnName);

            SwimTeamDatabase.AttendanceRow MatchingRow = this.AttendanceTable.NewAttendanceRow();
            for (int j = 0; j < this.AttendanceTable.Count; j++)
                if (this.AttendanceTable[j].USAID == USAID)
                {
                    MatchingRow = this.AttendanceTable[j];
                    break;
                }
            bool NotInSwimmerDatabaseOrAttendanceDatabase = false;
            bool NotInSwimmerDatabase = false;
            if (this.NewSwimmerUSAIDs != null)
                NotInSwimmerDatabaseOrAttendanceDatabase = this.NewSwimmerUSAIDs.Contains(USAID);
            if (this.ExtraSwimmersTable == null)

                for (int j = 0; j < this.ExtraSwimmersTable.Rows.Count; j++)
                {
                    if (this.ExtraSwimmersTable.Rows[j].Field<String>(AttendanceBLL.USAIDColumn.ColumnName) == USAID)
                    {
                        NotInSwimmerDatabase = true;
                        break;
                    }
                }

            bool NoChange = true;
            bool Update = false;
            bool Insert = false;
            bool Delete = false;

            String NewAttendanceType = "";
            String NewLane = "";
            String NewYardage = "";
            String NewMeterage = "";
            String NewNotes = "";

            if (MatchingRow.AttendanceType == "$")
            {
                //If we reach here, the attendance row we are checking was never in the database

                if (Request.Form[this.DynamicNamingContainer + USAID + "CheckBox"] == "on")
                {
                    //If we reach here, they are checked as attended, so we need to insert an attendance
                    //This is the block of code that will be reached the vast majority of the time.
                    NoChange = false;
                    Insert = true;

                    NewAttendanceType = Request.Form[this.DynamicNamingContainer + USAID + "TypeDropDownList"];
                    NewLane = Request.Form[this.DynamicNamingContainer + USAID + "LaneTextBox"];
                    String TempYardage = Request.Form[this.DynamicNamingContainer + USAID + "YardageTextBox"];
                    if (!String.IsNullOrEmpty(TempYardage))
                    {
                        String YMValue = Request.Form[this.DynamicNamingContainer + USAID + "YMDropDownList"];
                        if (YMValue == "Y")
                            NewYardage = TempYardage;
                        else if (YMValue == "M")
                            NewMeterage = TempYardage;
                    }
                    NewNotes = Request.Form[this.DynamicNamingContainer + USAID + "NotesTextBox"];

                    if (NotInSwimmerDatabase || NotInSwimmerDatabaseOrAttendanceDatabase)
                    {
                        NewNotes = "SYSTEMGENERATED-NAME: " + FirstName + "$" + LastName;
                    }


                }
            }
            else
            {
                //if we reach here, there was some previous attendance value

                if (MatchingRow.AttendanceType == "A")
                {
                    //if we reach here, they were previously recorded as being absent. Probably due to an update
                    if (Request.Form[this.DynamicNamingContainer + USAID + "CheckBox"] == "on")
                    {
                        //if we reach here, they are now recorded as being present
                        //this is an update
                        //if there were any previous values, we are just going to overwrite them
                        NoChange = false;
                        Update = true;

                        NewAttendanceType = Request.Form[this.DynamicNamingContainer + USAID + "TypeDropDownList"];
                        NewLane = Request.Form[this.DynamicNamingContainer + USAID + "LaneTextBox"];
                        String TempYardage = Request.Form[this.DynamicNamingContainer + USAID + "YardageTextBox"];
                        if (!String.IsNullOrEmpty(TempYardage))
                        {
                            String YMValue = Request.Form[this.DynamicNamingContainer + USAID + "YMDropDownList"];
                            if (YMValue == "Y")
                                NewYardage = TempYardage;
                            else if (YMValue == "M")
                                NewMeterage = TempYardage;
                        }
                        NewNotes = Request.Form[this.DynamicNamingContainer + USAID + "NotesTextBox"];

                        if (NotInSwimmerDatabase || NotInSwimmerDatabaseOrAttendanceDatabase)
                        {
                            NewNotes = "SYSTEMGENERATED-NAME: " + FirstName + "$" + LastName;
                        }
                    }
                }
                else
                {
                    //if we reach here, they already have a attendance record as present

                    //If they have been switched to having no attendance, they is going to be no record in the Request.Form
                    if (!Request.Form.AllKeys.Contains(this.DynamicNamingContainer + USAID + "CheckBox"))
                    {
                        //if we reach here, they were switched from having been present to being absent
                        //this is a delete
                        NoChange = false;
                        Delete = true;
                        //no need to save any information since it is going to be deleted from the database
                    }

                    else
                    {
                        //if we reach here, they were already present and they are still present
                        //we need to check to see if any of the values changed. If they have, it is an udpate
                        //if they are all the same, then we don't need to do anything since it is already in the database

                        NewAttendanceType = Request.Form[this.DynamicNamingContainer + USAID + "TypeDropDownList"];
                        NewLane = Request.Form[this.DynamicNamingContainer + USAID + "LaneTextBox"];
                        String TempYardage = Request.Form[this.DynamicNamingContainer + USAID + "YardageTextBox"];
                        if (!String.IsNullOrEmpty(TempYardage))
                        {
                            String YMValue = Request.Form[this.DynamicNamingContainer + USAID + "YMDropDownList"];
                            if (YMValue == "Y")
                                NewYardage = TempYardage;
                            else if (YMValue == "M")
                                NewMeterage = TempYardage;
                        }
                        NewNotes = Request.Form[this.DynamicNamingContainer + USAID + "NotesTextBox"];

                        if (NotInSwimmerDatabase || NotInSwimmerDatabaseOrAttendanceDatabase)
                        {
                            NewNotes = "SYSTEMGENERATED-NAME: " + FirstName + "$" + LastName;
                        }

                        if (NewAttendanceType != MatchingRow.AttendanceType)
                        {
                            NoChange = false;
                            Update = true;
                        }
                        if (MatchingRow.IsLaneNull())
                        {
                            if (!string.IsNullOrEmpty(NewLane))
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        else
                        {
                            if (NewLane != MatchingRow.Lane.ToString())
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        if (MatchingRow.IsMetersNull())
                        {
                            if (!String.IsNullOrEmpty(NewMeterage))
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        else
                        {
                            if (NewMeterage != MatchingRow.Meters.ToString())
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        if (MatchingRow.IsYardsNull())
                        {
                            if (!String.IsNullOrEmpty(NewYardage))
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        else
                        {
                            if (NewYardage != MatchingRow.Yards.ToString())
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        if (MatchingRow.IsNoteNull())
                        {
                            if (!String.IsNullOrEmpty(NewNotes))
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                        else
                        {
                            if (NewNotes != MatchingRow.Note)
                            {
                                NoChange = false;
                                Update = true;
                            }
                        }
                    }
                }
            }




            //Time to actually make updates to the database
            if (!NoChange)
            {
                if (Insert)
                {
                    MatchingRow.AttendanceType = NewAttendanceType;
                    MatchingRow.Date = this.DateControl1.Date;
                    if (String.IsNullOrEmpty(NewLane))
                        MatchingRow.SetLaneNull();
                    else
                        MatchingRow.Lane = int.Parse(NewLane);
                    if (String.IsNullOrEmpty(NewMeterage))
                        MatchingRow.SetMetersNull();
                    else
                        MatchingRow.Meters = int.Parse(NewMeterage.Replace(",", ""));
                    if (string.IsNullOrEmpty(NewNotes))
                        MatchingRow.SetNoteNull();
                    else
                        MatchingRow.Note = NewNotes;
                    MatchingRow.PracticeoftheDay = int.Parse(this.PracticeOfTheDayDropDownList.SelectedValue);
                    if (String.IsNullOrEmpty(NewYardage))
                        MatchingRow.SetYardsNull();
                    else
                        MatchingRow.Yards = int.Parse(NewYardage.Replace(",", ""));
                    int? PassInYardageValue, PassInMeterageValue, PassInLaneValue;
                    String PassInNotesValue;
                    if (MatchingRow.IsYardsNull())
                        PassInYardageValue = null;
                    else
                        PassInYardageValue = MatchingRow.Yards;
                    if (MatchingRow.IsMetersNull())
                        PassInMeterageValue = null;
                    else
                        PassInMeterageValue = MatchingRow.Meters;
                    if (MatchingRow.IsLaneNull())
                        PassInLaneValue = null;
                    else
                        PassInLaneValue = MatchingRow.Lane;
                    if (MatchingRow.IsNoteNull())
                        PassInNotesValue = null;
                    else
                        PassInNotesValue = MatchingRow.Note;
                    MatchingRow.GroupID = int.Parse(GroupPicked.Value.ToString());

                    AttendanceAdapter.BatchInsert(MatchingRow.USAID, MatchingRow.Date, MatchingRow.PracticeoftheDay,
                        MatchingRow.GroupID, MatchingRow.AttendanceType, PassInNotesValue, PassInLaneValue,
                        PassInYardageValue, PassInMeterageValue);

                    //MatchingRow.Delete();
                    DeleteInsertIndexes.Add(i);
                }
                else if (Update)
                {
                    MatchingRow.AttendanceType = NewAttendanceType;
                    MatchingRow.Date = new DateTime(this.DateControl1.Date.Year,
                        this.DateControl1.Date.Month, this.DateControl1.Date.Day);
                    if (String.IsNullOrEmpty(NewLane))
                        MatchingRow.SetLaneNull();
                    else
                        MatchingRow.Lane = int.Parse(NewLane);
                    if (String.IsNullOrEmpty(NewMeterage))
                        MatchingRow.SetMetersNull();
                    else
                        MatchingRow.Meters = int.Parse(NewMeterage.Replace(",", ""));
                    if (string.IsNullOrEmpty(NewNotes))
                        MatchingRow.SetNoteNull();
                    else
                        MatchingRow.Note = NewNotes;
                    MatchingRow.PracticeoftheDay = int.Parse(this.PracticeOfTheDayDropDownList.SelectedValue);
                    if (String.IsNullOrEmpty(NewYardage))
                        MatchingRow.SetYardsNull();
                    else
                        MatchingRow.Yards = int.Parse(NewYardage.Replace(",", ""));
                    int? PassInYardageValue, PassInMeterageValue, PassInLaneValue;
                    String PassInNotesValue;
                    if (MatchingRow.IsYardsNull())
                        PassInYardageValue = null;
                    else
                        PassInYardageValue = MatchingRow.Yards;
                    if (MatchingRow.IsMetersNull())
                        PassInMeterageValue = null;
                    else
                        PassInMeterageValue = MatchingRow.Meters;
                    if (MatchingRow.IsLaneNull())
                        PassInLaneValue = null;
                    else
                        PassInLaneValue = MatchingRow.Lane;
                    if (MatchingRow.IsNoteNull())
                        PassInNotesValue = null;
                    else
                        PassInNotesValue = MatchingRow.Note;
                }
                else if (Delete)
                {
                    //MatchingRow.Delete();
                    DeleteIndexes.Add(i);

                }
                else
                {
                    throw new Exception("Error. Was neither an Insert, Update, or a Delete");
                }
            }
        }
        if (DeleteInsertIndexes.Count > 0)
            for (int i = 0; i < DeleteInsertIndexes.Count; i++)
            {
                this.AttendanceTable[i].AcceptChanges();
                //this.AttendanceTable[i].Delete();
            }
        if (DeleteIndexes.Count > 0)
            for (int i = 0; i < DeleteIndexes.Count; i++)
            {
                this.AttendanceTable[DeleteIndexes[i]].Delete();
                for (int j = i + 1; j < DeleteIndexes.Count; j++)
                    DeleteIndexes[i]--;
            }

        AttendanceAdapter.CommitBatchInsert();
        AttendanceAdapter.EndBatchInsert();
        AttendanceAdapter.BatchUpdateIgnoreDBConcurrency(this.AttendanceTable);

        //this.ReloadFromPreviousFormValues = true;
        this.PracticeSaved = true;
        this.GetValuesFromStatus = GetValuesFrom.Database;
    }
    private bool PracticeSaved = false;

    private enum GetValuesFrom { Database, Form, Neither }

    private GetValuesFrom GetValuesFromStatus;
}