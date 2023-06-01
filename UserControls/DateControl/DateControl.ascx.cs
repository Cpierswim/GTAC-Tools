using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_DateControl_DateControl : System.Web.UI.UserControl
{
    private DateTime _Date;
    private static String DateViewStateName = "l9kalksdjf002kkjs";
    public DateTime Date
    {
        get
        {
            if (_Date == null)
            {
                try
                {
                    _Date = ((DateTime)ViewState[UserControls_DateControl_DateControl.DateViewStateName]);
                }
                catch (Exception)
                {
                    _Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
                }

                if (_Date == null)
                    _Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            }
            if (_Date.Year < 1000)
            {
                try
                {
                    _Date = ((DateTime)ViewState[UserControls_DateControl_DateControl.DateViewStateName]);
                }
                catch (Exception)
                {
                    _Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
                }

                if (_Date == null)
                    _Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
            }
            return _Date;
        }

        set
        {
            _Date = value;
            ViewState.Add(UserControls_DateControl_DateControl.DateViewStateName, _Date);
        }
    }

    private int _yearsBeforeCurrent;
    private static String YearsBeforeCurrentViewStateName = "32939skksdfh";
    public int YearsBeforeCurrent
    {
        get
        {

            if (_yearsBeforeCurrent == 0)
            {
                try
                {
                    _yearsBeforeCurrent = ((int)ViewState[UserControls_DateControl_DateControl.YearsBeforeCurrentViewStateName]);
                }
                catch (Exception)
                {
                    _yearsBeforeCurrent = 2;
                }

            }

            if (_yearsBeforeCurrent == -1)
                return 0;
            return _yearsBeforeCurrent;
        }
        set
        {
            if (value <= 0)
                _yearsBeforeCurrent = -1;
            else
                _yearsBeforeCurrent = value;
            ViewState.Add(UserControls_DateControl_DateControl.YearsBeforeCurrentViewStateName, _yearsBeforeCurrent);
        }
    }

    private bool? _autoChangeDate;
    private static String AutoChangeDateViewStateName = "e29923l234ns";
    public bool AutoChangeDate
    {
        get
        {
            if (this._autoChangeDate == null)
            {
                try
                {
                    this._autoChangeDate = ((bool)ViewState[AutoChangeDateViewStateName]);
                }
                catch (Exception)
                {
                    this._autoChangeDate = true;
                }
            }
             

            if (this._autoChangeDate == true)
                return true;
            else
                return false;
        }
        set
        {
            this._autoChangeDate = value;

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //    CreateDropDownListsFromDate();


    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Page.LoadComplete += this.Page_LoadComplete;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //if (!this.DateJustChanged)
        //{
            CreateDropDownListsFromDate();
        //}
    }

    private void CreateDropDownListsFromDate()
    {
        this.AddDaysForMonth();
        this.AddYears();
        this.HighlightCurrent();

        if (this.AutoChangeDate)
        {
            this.MonthDropDownList.AutoPostBack = true;
            this.DayDropDownList.AutoPostBack = true;
            this.YearDropDownList.AutoPostBack = true;

            this.ChangeDateButton.Visible = false;
        }
        else
        {
            this.MonthDropDownList.AutoPostBack = false;
            this.DayDropDownList.AutoPostBack = false;
            this.YearDropDownList.AutoPostBack = false;

            this.ChangeDateButton.Visible = true;
        }
    }
    private void AddDaysForMonth()
    {
        this.DayDropDownList.Items.Clear();
        for (int i = 1; i <= DateTime.DaysInMonth(Date.Year, Date.Month); i++)
            this.DayDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
    }
    private void AddYears()
    {
        this.YearDropDownList.Items.Clear();
        int StartYearValue = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time").Year;
        for (int i = StartYearValue; i >= (StartYearValue - this.YearsBeforeCurrent); i--)
            this.YearDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
    }
    private void HighlightCurrent()
    {
        MonthDropDownList.ClearSelection();
        DayDropDownList.ClearSelection();
        YearDropDownList.ClearSelection();
        MonthDropDownList.Items.FindByValue(this.Date.Month.ToString()).Selected = true;
        DayDropDownList.Items.FindByValue(this.Date.Day.ToString()).Selected = true;
        YearDropDownList.Items.FindByValue(this.Date.Year.ToString()).Selected = true;
    }
    protected void DropDownListPostedBacked(object sender, EventArgs e)
    {
        if (this.AutoChangeDate)
            ChangeDate();
    }

    private bool DateJustChanged = false;
    private void ChangeDate()
    {
        //change the date to whatever is displayed
        int Year = int.Parse(YearDropDownList.SelectedValue);
        int Month = int.Parse(MonthDropDownList.SelectedValue);
        int Day = int.Parse(DayDropDownList.SelectedValue);
        int MaxDaysInMonth = DateTime.DaysInMonth(Year, Month);

        if (MaxDaysInMonth < Day)
            Day = MaxDaysInMonth;
        Date = new DateTime(Year, Month, Day);

        //this.CreateDropDownListsFromDate();

        if (this.SelectedDateChanged != null)
        {
            EventArgs test = new EventArgs();
            
            this.SelectedDateChanged(this, new EventArgs());
        }

        DateJustChanged = true;
    }


    protected void ChangeDateButtonClicked(object sender, EventArgs e)
    {
        this.ChangeDate();
    }

    public event System.EventHandler SelectedDateChanged;

    protected void ChangeToTodayButtonClicked(object sender, EventArgs e)
    {
        //change the date to today

        Date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Eastern Standard Time");
        Date = new DateTime(Date.Year, Date.Month, Date.Day);

        //this.CreateDropDownListsFromDate();

        if (this.SelectedDateChanged != null)
        {
            EventArgs test = new EventArgs();

            this.SelectedDateChanged(this, new EventArgs());
        }

        DateJustChanged = true;
    }
}