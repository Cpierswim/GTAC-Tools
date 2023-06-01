function CopyBestTime(source) {
    var TimeControlTextBox = GetTimeControlTextBox(source);
    var CurrentEntriesInMeet = parseInt(GetCurrentEntriesForMeet(source));
    var CurrentEntriesInSession = parseInt(GetCurrentEntriesForSession(source));
    var BonusChecked = IsBonusChecked(source);

    if (source.checked) {
        var CourseCode = GetMeetCourseCode(source);
        var $FastCutTimePanel = GetFastCutTimePanel(source);
        var $BestTimePanel = GetBestTimePanel(source);
        var $SlowCutTimePanel = GetSlowCutTimePanel(source);
        var MaxEntriesForSession = parseInt(GetMaxEntriesForSession(source));
        var MaxEntriesForMeet = GetMaxEntriesForMeet(source);
        if (MaxEntriesForMeet.toString().substr(0, 1) == "N")
            MaxEntriesForMeet = 99;
        else
            MaxEntriesForMeet = parseInt(MaxEntriesForMeet);


        if ((CurrentEntriesInSession <= MaxEntriesForSession) && (CurrentEntriesInMeet <= MaxEntriesForMeet)) {
            var Time = GetBestTime(CourseCode, $BestTimePanel, $FastCutTimePanel, $SlowCutTimePanel, BonusChecked);
            TimeControlTextBox.className = "";
            TimeControlTextBox.value = Time;
            if (TimeControlTextBox.value.toString().substr(0, 1) == "N")
                TimeControlTextBox.value = "No NT's";
            CheckTime(TimeControlTextBox);
        }
        else {
            source.checked = false;
            TimeControlTextBox.disabled = true;
        }

    }
    else {
        TimeControlTextBox.className = "aspNetDisabled";
        TimeControlTextBox.disabled = true;
        var BonusCheckBox = GetBonusCheckBox(source);
        BonusCheckBox.checked = false;
        var ExhibCheckBox = GetExhibCheckBox(source);
        ExhibCheckBox.checked = false;
    }
}

function GetBonusCheckBox(source) {
    return $(source).parent().parent().parent().children(":nth-child(8)").children(":nth-child(1)").get(0);
}

function GetExhibCheckBox(source) {
    return $(source).parent().parent().parent().children(":nth-child(9)").children(":nth-child(1)").get(0);
}

function GetMeetCourseCode(source) {
    return $(source).parent().parent().parent().parent().parent().parent().parent().parent().children(":nth-child(1)").get(0).value;
}

function GetFastCutTimePanel(source) {
    //if I change the tablecell order, change the "6" below
    return $(source).parent().parent().parent().children(":nth-child(6)").children(":nth-child(1)");
}

function GetSlowCutTimePanel(source) {
    //if I change the tablecell order, change the "6" below
    return $(source).parent().parent().parent().children(":nth-child(7)").children(":nth-child(1)");
}

function GetBestTimePanel(source) {
    //if I change the tablecell order, change the "5" below
    return $(source).parent().parent().parent().children(":nth-child(5)").children(":nth-child(1)");
}

function GetTimeControlTextBoxJQuery(source) {
    return $(source).parent().children(":nth-child(2)");
}

function GetTimeControlTextBox(source) {
    return $(source).parent().children(":nth-child(2)").get(0);
}

function GetMaxEntriesForMeet(source) {
    return $(source).parent().parent().parent().parent().parent().parent().children(":nth-child(1)").get(0).innerHTML;
}

function GetMaxEntriesForSession(source) {
    return $(source).parent().parent().parent().parent().children(":nth-child(1)").children(":nth-child(1)").children(":nth-child(3)").children(":nth-child(2)").get(0).innerHTML;
}

function GetCurrentEntriesForMeet(source) {
    var $SwimmerDiv = $(source).parent().parent().parent().parent().parent().parent();
    var ChildrenCount = $SwimmerDiv.children().length;
    var EntryCount = 0;
    for (i = 3; i <= ChildrenCount; i = i + 1) {
        var $SessionTable = $SwimmerDiv.children(":nth-child(" + i.toString() + ")").children(":nth-child(1)");
        var RowsCount = $SessionTable.children().length;
        for (j = 3; j <= RowsCount; j = j + 1) {
            var Test = $SessionTable.children(":nth-child(" + j.toString() + ")").children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(1)").get(0)
            if ($SessionTable.children(":nth-child(" + j.toString() + ")").children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(1)").get(0).checked)
                EntryCount = EntryCount + 1;
        }
    }
    return EntryCount;
}

function GetCurrentEntriesForSession(source) {
    var $TableBody = $(source).parent().parent().parent().parent();
    var ChildrenCount = $TableBody.children().length;
    var EntryCount = 0;
    for (i = 3; i <= ChildrenCount; i = i + 1) {
        if ($TableBody.children(":nth-child(" + i.toString() + ")").children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(1)").get(0).checked)
            EntryCount = EntryCount + 1;
    }

    return EntryCount
}

function IsBonusChecked(source) {
    return $(source).parent().parent().parent().children(":nth-child(8)").children(":nth-child(1)").get(0).checked;
}

function GetBestTime(CourseCode, $BestTimesPanel, $FastCutTimesPanel, $SlowCutTimesPanel, Bonus) {
    var YardBestTime = GetBestTimeByCourse($BestTimesPanel, "Y");
    var LCMBestTime = GetBestTimeByCourse($BestTimesPanel, "L");
    var SCMBestTime = GetBestTimeByCourse($BestTimesPanel, "S");

    var YardFastCutTime = GetCutTimeByCourse($FastCutTimesPanel, "Y");
    var LCMFastCutTime = GetCutTimeByCourse($FastCutTimesPanel, "L");
    var SCMFastCutTime = GetCutTimeByCourse($FastCutTimesPanel, "S");

    var YardSlowCutTime = GetCutTimeByCourse($SlowCutTimesPanel, "Y");
    var LCMSlowCutTime = GetCutTimeByCourse($SlowCutTimesPanel, "L");
    var SCMSlowCutTime = GetCutTimeByCourse($SlowCutTimesPanel, "S");

    var YardBestTimeSpan;
    var LCMBestTimeSpan;
    var SCMBestTimeSpan;
    if (YardBestTime != "NT")
        YardBestTimeSpan = GetTimeWithCourseAsTimeSpan(YardBestTime);
    if (LCMBestTime != "NT")
        LCMBestTimeSpan = GetTimeWithCourseAsTimeSpan(LCMBestTime);
    if (SCMBestTime != "NT")
        SCMBestTimeSpan = GetTimeWithCourseAsTimeSpan(SCMBestTime);

    var YardFastCutTimeSpan;
    var LCMFastCutTimeSpan;
    var SCMFastCutTimeSpan;
    if (YardFastCutTime != "NT")
        YardFastCutTimeSpan = GetTimeWithCourseAsTimeSpan(YardFastCutTime);
    if (LCMFastCutTime != "NT")
        LCMFastCutTimeSpan = GetTimeWithCourseAsTimeSpan(LCMFastCutTime);
    if (SCMFastCutTime != "NT")
        SCMFastCutTimeSpan = GetTimeWithCourseAsTimeSpan(SCMFastCutTime);

    var YardSlowCutTimeSpan;
    var LCMSLowCutTimeSpan;
    var SCMSlowCutTimeSpan;
    if(YardSlowCutTime != "NT")
        YardSlowCutTimeSpan = GetTimeWithCourseAsTimeSpan(YardSlowCutTime);
    if(LCMSlowCutTime != "NT")
        LCMSLowCutTimeSpan = GetTimeWithCourseAsTimeSpan(LCMSlowCutTime);
    if(SCMSlowCutTime != "NT")
        SCMSlowCutTimeSpan = GetTimeWithCourseAsTimeSpan(SCMFastCutTime);

    if (CourseCode == "YO" || CourseCode == "Y") {
        return GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
    }
    else if (CourseCode == "LO" || CourseCode == "L") {
        return GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
    }
    else if (CourseCode == "SO" || CourseCode == "S") {
        return GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
    }
    else {
        return GetTimeForMultipleCourse(YardBestTime, LCMBestTime, SCMBestTime, YardBestTimeSpan, LCMBestTimeSpan, SCMBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan,
        LCMFastCutTimeSpan, LCMSLowCutTimeSpan, SCMFastCutTimeSpan, SCMSlowCutTimeSpan, CourseCode, Bonus);
    }
}
function GetBestTimeByCourse($BestTimesPanel, Course) {
    for (i = 1; i <= $BestTimesPanel.children().length; i = i + 2) {
        var Element = $BestTimesPanel.children(":nth-child(" + i.toString() + ")").get(0);
        if (Element == undefined)
            return "NT";
        var Time = Element.innerHTML;
        if (Time.substr(Time.length - 1, 1) == Course)
            return Time;
    }

    return "NT";
}

function GetCutTimeByCourse($CutTimePanel, Course) {
    return GetBestTimeByCourse($CutTimePanel, Course);
}

function GetTimeWithCourseAsTimeSpan(Time) {
    var Hundredths = 0;
    var Seconds = 0;
    var Minutes = 0;
    var Hours = 0;
    Time = Time.substring(0, Time.length - 1);
    var SplitAtPeriod = Time.split(".");
    Hundredths = SplitAtPeriod[1];
    var SplitAtColon = SplitAtPeriod[0].split(":");
    if (SplitAtColon.length == 1)
        Seconds = SplitAtColon[0];
    else if (SplitAtColon.length == 2) {
        Minutes = SplitAtColon[0];
        Seconds = SplitAtColon[1];
    }
    else if (SplitAtColon.length == 3) {
        Hours = SplitAtColon[0];
        Minutes = SplitAtColon[1];
        Seconds = SplitAtColon[2];
    }
    var Milliseconds = Hundredths * 10;
    var ts = new TimeSpan(Milliseconds, Seconds, Minutes, Hours);
    return ts;
}

function GetTimeForOnlyCourse(BestTime, BestTimeSpan, FastCutTimeSpan, SlowCutTimeSpan, Bonus) {
    if (Bonus)
        return BestTime;

    if(SlowCutTimeSpan == undefined) {
        if (FastCutTimeSpan == undefined)
            return BestTime;
        if (FastCutTimeSpan != undefined && BestTime == "NT")
            return "No NT's";
        
        BestTimeSpan.subtract(FastCutTimeSpan);
        var MathResult = BestTimeSpan.totalMilliseconds();
        if (MathResult <= 0)
            return BestTime;
        else
            return "Over Cut";
     } else {
        if(FastCutTimeSpan == undefined){
             if(BestTime == "NT")
                return "No NT's";
             BestTimeSpan.subtract(SlowCutTimeSpan);
             var MathResult = BestTimeSpan.totalMilliseconds();
             if(MathResult > 0)
                return BestTime;
             else
                return "Too Fast";
         } else {
            if(BestTime == "NT")
                return "No NT's";
            var BestTimeForMath = BestTimeSpan;
            BestTimeForMath.subtract(FastCutTimeSpan);
            var MathResult = BestTimeForMath.totalMilliseconds();
            if(MathResult > 0)
                return "Too Slow";
            BestTimeForMath = BestTimeSpan;
            BestTimeForMath.subtract(SlowCutTimeSpan);
            var Testty = BestTimeSpan.totalMilliseconds();
            var Test = BestTimeForMath.totalMilliseconds();
            var SecondMathResult = BestTimeForMath.totalMilliseconds();
            if (SecondMathResult > 0)
                return "Too Fast";
            return BestTime;
         }
     }
}

function GetTimeForMultipleCourse(YardBestTime, LCMBestTime, SCMBestTime, YardBestTimeSpan, LCMBestTimeSpan, SCMBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, CourseCode, Bonus) {
    if (CourseCode == "LS") {
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        return LCMTime;
    } else if (CourseCode == "LY") {
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        return LCMTime;
    } else if (CourseCode == "YL") {
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        return YardTime;
    } else if (CourseCode == "YS") {
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        return YardTime;
    } else if (CourseCode == "SL") {
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        return SCMTime;
    } else if (CourseCode == "SY") {
        var SCMTime = GetTimeForOnlyCourse(SCMBestTime, SCMBestTimeSpan, SCMFastCutTimeSpan, SCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(SCMTime))
            return SCMTime;
        var YardTime = GetTimeForOnlyCourse(YardBestTime, YardBestTimeSpan, YardFastCutTimeSpan, YardSlowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(YardTime))
            return YardTime;
        var LCMTime = GetTimeForOnlyCourse(LCMBestTime, LCMBestTimeSpan, LCMFastCutTimeSpan, LCMSLowCutTimeSpan, Bonus);
        if (IsNotOverOrNT(LCMTime))
            return LCMTime;
        return SCMTime;
    }
}

function IsNotOverOrNT(TimeString) {
    if (TimeString.substr(0, 2) == "Ov")
        return false;
    else if (TimeString.substr(0, 2) == "No")
        return false;
    else if (TimeString.substr(0, 2) == "NT")
        return false;
    else if (TimeString.substr(0, 2) == "To")
        return false;
    return true;
}