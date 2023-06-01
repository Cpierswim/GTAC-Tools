function ToggleTimeControl(source) {
    var TextBox = $(source).parent().children(":nth-child(2)").get(0);
    var Validator = $(source).parent().children(":nth-child(3)").get(0);
    if (source.checked)
        TextBox.disabled = false;
    else {
        TextBox.className = "";
        TextBox.value = "";
        TextBox.disabled = true;
        Validator.isvalid = "";
    }
}

function CheckTime(source) {
    var trimmed = source.value.replace(/^\s+|\s+$/g, '');

    var DefaultCourse = $(source).parent().children(":nth-child(4)").get(0).value;

    if (trimmed.length == 0) {
        source.className = "Invalid";
        return false;
    }
    else if (trimmed.toUpperCase() == "NT") {
        source.value = trimmed.toUpperCase();
        source.className = "";
        return true;
    }
    else {
        try {
            var Course = GetCourse(trimmed, DefaultCourse);
            var Time = GetTime(trimmed);
            var TimeArray = GetTimeArray(Time);
            source.value = SetFromValues(TimeArray, Course);

            source.className = "";
            return true;
        }
        catch (Error) {
            source.className = "Invalid";
            return false;
        }
    }
}

function GetTime(Time) {
    if (AcceptableCourseValue(Time.charAt(Time.length - 1)))
        return Time.substr(0, (Time.length - 1));
    return Time;
}

function isNumeric(elem) {
    var numericExpression = /^[0-9]+$/;
    if (elem.match(numericExpression)) {
        return true;
    }
    else {
        return false;
    }
}

function GetCourse(Time, DefaultCourse) {
    if (isNumeric(Time.charAt(Time.length - 1)))
        return DefaultCourse;
    else {
        if (AcceptableCourseValue(Time.charAt(Time.length - 1)))
            return Time.charAt(Time.length - 1);
        else
            throw "Not acceptable course value";
    }
}

function AcceptableCourseValue(Char) {
    if ((Char == "Y") || (Char == "y"))
        return true;
    if ((Char == "S") || (Char == "s"))
        return true;
    if ((Char == "L") || (Char == "l"))
        return true;
    return false;
}



function GetTimeArray(Time) {
    var ReturnArray = new Array();
    if (Time.length == 0)
        throw "Malformed Time";
    var Hundredths = 0;

    if (Time.indexOf(".") == -1) {
        if (Time.length > 0 && Time.length <= 2) {
            if (!isNumeric(Time))
                throw "Malformed Time";

            Hundredths = Time;
            TimeArray[0] = 0;
            TimeArray[1] = 0;
            TimeArray[2] = 0;
            TimeArray[3] = Hundredths;
            return TimeArray;
        }
        else {
            Time = Time.substr(0, (Time.length - 2)) + "." + Time.substring(Time.length - 2);
        }
    }
    else {
        if (Time.indexOf(".") == (Time.length - 1))
            throw "Malformed Time";

        var TempTime = Time.substring((Time.indexOf(".") + 1));
        if (TempTime.indexOf(".") != -1)
            throw "Malformed Time";
        if (TempTime.length > 2)
            throw "Malformed Time";
    }

    Hundredths = Time.substring((Time.indexOf(".") + 1));
    if (!isNumeric(Hundredths))
        throw "Malformed Time";
    if (Hundredths.length == 1)
        Hundredths = Hundredths + 0;

    Time = Time.substr(0, Time.indexOf("."));

    if (Time.length > 2 && Time.indexOf(":") == -1)
        Time = InsertColons(Time);

    var TimeArray = ToStringArray(Time, ":");


    for (i = 0; i < TimeArray.length; i++)
        if (!isNumeric(TimeArray[i]))
            throw "Malformed Time";

var hours = 0;
var minutes = 0;
var seconds = 0;
if (TimeArray.length == 3) {
    hours = TimeArray[0];
    minutes = TimeArray[1];
    seconds = TimeArray[2];
}
else if (TimeArray.length == 2) {
    minutes = TimeArray[0];
    seconds = TimeArray[1];
}
else if (TimeArray.length == 1) {
    seconds = TimeArray[0];
} else
    throw "Malformed Time";



ReturnArray[0] = hours;
ReturnArray[1] = minutes;
ReturnArray[2] = seconds;
ReturnArray[3] = Hundredths;

var FirstNonZero = -1;
for (i = 0; i < ReturnArray.length; i++) {
    if (ReturnArray[i] != 0) {
        FirstNonZero = i;
        i = ReturnArray.length;
    }
}

if (FirstNonZero == -1)
    throw "Malformed Time";


for (i = (FirstNonZero + 1); i < ReturnArray.length; i++) {
    var testvalue = ReturnArray[i];
    var length = testvalue.toString().length
    if (length != 2)
        throw "Malformed Time"
}

while (ReturnArray[2] >= 60) {
    ReturnArray[1]++;
    ReturnArray[2] = ReturnArray[2] - 60;
    if (ReturnArray[2].toString().length == 1)
        ReturnArray[2] = "0" + ReturnArray[2];
}
while (ReturnArray[1] >= 60) {
    ReturnArray[0]++;
    ReturnArray[1] = ReturnArray[1] - 60;
    if (ReturnArray[1].toString().length == 1)
        ReturnArray[1] = "0" + ReturnArray[1];
}

return ReturnArray;
}


function ToStringArray(StringToChop, Splitter) {
    var ReturnArray = new Array();
    var index = 0;
    var WorkingOnString = "";
    for (i = 0; i < StringToChop.length; i++) {
        if (StringToChop.charAt(i) != Splitter)
            WorkingOnString = WorkingOnString + StringToChop.charAt(i);
        else {
            if (i != 0) {
                ReturnArray[index] = WorkingOnString;
                WorkingOnString = "";
                index++;
            }
        }
    }

    if (WorkingOnString.length > 0)
        ReturnArray[index] = WorkingOnString;


    return ReturnArray;
}

function SetFromValues(TimeArray, Course) {
    if (TimeArray[0] == 0 && TimeArray[1] != 0 && TimeArray[1].substr(0, 1) == "0")
        TimeArray[1] = TimeArray[1].substr(1, 1);
    var result = "";
    if (TimeArray[0] != 0)
        result = TimeArray[0] + ":";
    if (TimeArray[1] != "0") {
        result = result + TimeArray[1];
    }
    if (TimeArray[2] != "0") {
        if (TimeArray[2] < 10 && TimeArray[2].substr(0, 1) != "0")
            result = result + ":0" + TimeArray[2];
        else
            result = result + ":" + TimeArray[2];
    }
    result = result + "." + TimeArray[3] + Course.toUpperCase();

    return result;
}

function InsertColons(Time) {
    var ReturnString = "";
    while (Time.length > 1) {
        var test1 = Time.length - 2;
        var test2 = Time.length - 1;
        var TempString = Time.substr(test1);
        Time = Time.slice(0, (Time.length - 2));
        ReturnString = ":" + TempString + ReturnString;
    }

    if (Time.length == 1)
        ReturnString = Time + ReturnString;
    else
        ReturnString = ReturnString.substr(1);

    return ReturnString;
}

function ValidateTime(source, args) {
    var ValidatingControl = document.getElementById(source.controltovalidate.toString());
    ValidatingControl.value = ValidatingControl.value.replace(/^\s+|\s+$/g, '');

    if (!ValidatingControl.disabled) {
        var DefaultCourse = $(source).parent().children(":nth-child(4)").get(0).value;
        if (ValidatingControl.value.length == 0) {
            ValidatingControl.className = "Invalid";
            args.IsValid = false;
        }
        else if (ValidatingControl.value.toUpperCase() == "NT") {
            ValidatingControl.value = ValidatingControl.value.toUpperCase();
            ValidatingControl.className = "";
            args.IsValid = true;
        }
        else {
            try {
                var Course = GetCourse(ValidatingControl.value, DefaultCourse);
                var Time = GetTime(ValidatingControl.value);
                var TimeArray = GetTimeArray(Time);
                ValidatingControl.value = SetFromValues(TimeArray, Course);

                ValidatingControl.className = "";
                args.IsValid = true;
            }
            catch (Error) {
                ValidatingControl.className = "Invalid";
                args.IsValid = false;
            }
        }
    }
    else {
        args.IsValid = true;
    }
}

function IsBlank(str) {
    return (!str || /^\s*$/.test(str));
}


function SetAsBonus(source) {
    if (source.checked) {
        var $EntryCheckBox = $(source).parent().parent().children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(1)");
        var $TimeTextBox = $(source).parent().parent().children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(2)");
        if ($EntryCheckBox.get(0).checked) {
            if (!IsBlank($TimeTextBox.get(0).value)) {
                if (!CheckTime($TimeTextBox.get(0))) {
                    CopyBestTime($EntryCheckBox.get(0));
                }
            }
        }
        else
            source.checked = false;
    }
    else {
        var TimeTextBox = $(source).parent().parent().children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(2)").get(0);
        TimeTextBoxBlur(TimeTextBox);
//        if (CheckTime(TimeTextBox)) {
//            var Course = TimeTextBox.value.substr(TimeTextBox.value.length - 1);
//            //get cut time for course
//            //if time is over cut time, display over cut, otherwise do nothing
//            var $CutTimePanel = GetFastCutTimePanel(TimeTextBox);
//            var CutTime = GetBestTimeByCourse($CutTimePanel, Course);
//            if (CutTime != "NT") {
//                var TextBoxTimeSpan = GetTimeWithCourseAsTimeSpan(TimeTextBox.value);
//                var CutTimeSpan = GetTimeWithCourseAsTimeSpan(CutTime);
//                TextBoxTimeSpan.subtract(CutTimeSpan);
//                var MathResult = TextBoxTimeSpan.totalMilliseconds();
//                if (MathResult > 0) {
//                    TimeTextBox.value = "Over Cut";
//                    CheckTime(TimeTextBox);
//                }
//            }
//            
//        }
    }
}

function ExhibClicked(source) {
    var EntryCheckBox = $(source).parent().parent().children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(1)").get(0);
    if (!EntryCheckBox.checked && source.checked)
        source.checked = false;
}

function TimeTextBoxBlur(source) {
    var ValidTime = CheckTime(source);
    if (!ValidTime) {
        if (source.value.toString().toUpperCase() == "NT")
            source.value = "No NT's";
    }
    else {
        var Course = source.value.substr(source.value.length - 1);
        //get cut time for course
        //if time is over cut time, display over cut, otherwise do nothing
        var $FastCutTimePanel = GetFastCutTimePanel(source);
        var $SlowCutTimePanel = GetSlowCutTimePanel(source);
        var FastCutTime = GetBestTimeByCourse($FastCutTimePanel, Course);
        var SlowCutTime = GetBestTimeByCourse($SlowCutTimePanel, Course);
        var BonusCheckBox = GetBonusCheckBox(source);
        if (!BonusCheckBox.checked) {
            var TextBoxTimeSpan = GetTimeWithCourseAsTimeSpan(source.value);
            if (SlowCutTime == "NT") {
                if (FastCutTime != "NT") {
                    var FastCutTimeSpan = GetTimeWithCourseAsTimeSpan(FastCutTime);
                    TextBoxTimeSpan.subtract(FastCutTimeSpan);
                    var MathResult = TextBoxTimeSpan.totalMilliseconds();
                    if (MathResult > 0)
                        source.value = "Over Cut";
                }
            } else {
                if (FastCutTime == "NT") {
                    var SlowCutTimeSpan = GetTimeWithCourseAsTimeSpan(SlowCutTime);
                    TextBoxTimeSpan.subtract(SlowCutTimeSpan);
                    var MathResult = TextBoxTimeSpan.totalMilliseconds();
                    if (MathResult <= 0)
                        source.value = "Too Fast";
                }
                else {
                    var FastCutTimeSpan = GetTimeWithCourseAsTimeSpan(FastCutTime);
                    var SlowCutTimeSpan = GetTimeWithCourseAsTimeSpan(SlowCutTime);
                    var TimeSpanForMath = TextBoxTimeSpan;
                    //                    TimeSpanForMath.subtract(FastCutTimeSpan);
                    var MathResult = TimeSpanForMath.totalMilliseconds() - FastCutTimeSpan.totalMilliseconds();
                    if (MathResult > 0)
                        source.value = "Too Slow";
                    else {
                        var TimeSpanForMathAgain = TextBoxTimeSpan;
//                        TimeSpanForMathAgain.subtract(SlowCutTimeSpan);
                        var Test = TimeSpanForMathAgain.toString();
                        var MathTest = TimeSpanForMathAgain.totalSeconds();
                        var SecondMathResult = TimeSpanForMathAgain.totalMilliseconds() - SlowCutTimeSpan.totalMilliseconds();
                        if (SecondMathResult <= 0)
                            source.value = "Too Fast";
                    }
                }
            }
        }
    }
    CheckTime(source);
}

function ValidateSwimmer(source, args) {
    var ValidatingControl = document.getElementById(source.controltovalidate.toString());
    var CurrentEntriesInMeet = parseInt(GetCurrentEntriesForMeet(ValidatingControl));
    var MaxEntriesForMeet = GetMaxEntriesForMeet(ValidatingControl);
    var OverMaxMeetEntries = false;
    var OverMaxSessionEntries = false;
    var AllTimesValid = true;
    if (MaxEntriesForMeet.substr(0, 1) != "N")
        MaxEntriesForMeet = parseInt(MaxEntriesForMeet);
    else
        MaxEntriesForMeet = 99;
    if (CurrentEntriesInMeet > MaxEntriesForMeet)
        OverMaxMeetEntries = true;

    var $SwimmerDiv = $(ValidatingControl).parent().parent().parent().parent().parent().parent();
    var Children = $SwimmerDiv.children().length;
    for (var i = 3; i <= Children - 1; i = i + 1) {
        var $SessionTableBody = $SwimmerDiv.children(":nth-child(" + i + ")").children(":nth-child(1)");
        //get the session count and confirm all the boxes
        var Events = $SessionTableBody.children().length;
        for (var j = 3; j <= Events; j = j + 1) {
            var TimeTextBox = $SessionTableBody.children(":nth-child(" + j + ")").children(":nth-child(4)").children(":nth-child(1)").children(":nth-child(2)").get(0);
            if (j == 3) {
                var MaxSessionEntries = parseInt(GetMaxEntriesForSession(TimeTextBox));
                var CurrentSessionEntries = parseInt(GetCurrentEntriesForSession(TimeTextBox));
                if (MaxSessionEntries == 0)
                    MaxSessionEntries = 99;
                if (CurrentSessionEntries > MaxSessionEntries)
                    OverMaxSessionEntries = true;
            }
            if (!TimeTextBox.disabled) {
                if (!CheckTime(TimeTextBox))
                    AllTimesValid = false;
            }
        }
    }



    if (OverMaxMeetEntries || OverMaxSessionEntries || !AllTimesValid) {
        args.IsValid = false;

        //There are errors with the swimmer. Display the errors on the error label.
        var ErrorLabel = document.getElementById("ctl00_MainContent_ErrorLabel");
        var SwimmerName = $SwimmerDiv.prev().children(":nth-child(2)").get(0).innerHTML;
        var ErrorStringFirstPart = "Problem with " + SwimmerName + ": ";
        var ErrorStringSecondPart = "";
        if (OverMaxMeetEntries)
            "over meet entry limit";
        if (OverMaxSessionEntries) {
            if (ErrorStringSecondPart != "")
                ErrorStringSecondPart = ErrorStringSecondPart + ", ";
            ErrorStringSecondPart = ErrorStringSecondPart + "over session entry limit";
        }
        if (!AllTimesValid) {
            if (ErrorStringSecondPart != "")
                ErrorStringSecondPart = ErrorStringSecondPart + ", ";
            ErrorStringSecondPart = ErrorStringSecondPart + "at least 1 entry time is invalid";
        }
        ErrorStringFirstPart = ErrorStringFirstPart + ErrorStringSecondPart + ".";

        var InnerHTML = ErrorLabel.innerHTML;
        var InnerText = ErrorLabel.innerText;

        if (ErrorLabel.innerText == "")
            ErrorLabel.innerHTML = ErrorStringFirstPart;
        else
            ErrorLabel.innerHTML = ErrorLabel.innerHTML + "<br />" + ErrorStringFirstPart;

        if (ErrorLabel.innerHTML.indexOf("There were errors on the page") == -1)
            ErrorLabel.innerHTML = "There were errors on the page. NO entries were saved.<br /><br />" + ErrorLabel.innerHTML;

        SetSwimmerAsError($SwimmerDiv);
    }
    else
        SetSwimmerAsValid($SwimmerDiv);
}

function SetSwimmerAsError($SwimmerDiv) {
    InvalidSwimmers = InvalidSwimmers + 1;
    var $SwimmerHeader = $SwimmerDiv.prev();
    var color = $SwimmerHeader.css("background-color");
    if (color != 'rgb(255, 0, 0)') {
        $SwimmerHeader.toggleClass("testerrorpanel");
        $SwimmerHeader.animate({ backgroundColor: "red" }, 4500);
        $SwimmerDiv.prev().children(":nth-child(2)").toggleClass("errortext");
    }
}

function SetSwimmerAsValid($SwimmerDiv) {
    var $SwimmerHeader = $SwimmerDiv.prev();
    var color = $SwimmerHeader.css("background-color");
    if (color == 'rgb(255, 0, 0)') {
        $SwimmerHeader.animate({ backgroundColor: "#507CD1" }, 2500).toggleClass("testerrorpanel");
        $SwimmerDiv.prev().children(":nth-child(2)").toggleClass("errortext");
    }
}

function CheckAllTextBoxes() {
    var $TextBoxes = $('input[type=text]');
    for (var i = 0; i < $TextBoxes.length; i = i + 1) {
        var TextBox = $TextBoxes.get(i);
        if (!TextBox.disabled)
            CheckTime(TextBox);
    }
}

var InvalidSwimmers;
function SaveButtonClick(source) {
    ButtonClicked = "SaveButton";
    InvalidSwimmers = 0;
    $("#ctl00_MainContent_MainAccordion").accordion("activate", false);
    window.scrollTo(0, 0);
    var ErrorLabel = document.getElementById("ctl00_MainContent_ErrorLabel");
    ErrorLabel.innerText = "";
}

function SaveButtonClickTwo(sender) {
    $("#ctl00_MainContent_MainAccordion").accordion("activate", false);
    window.scrollTo(0, 0);
    var ErrorLabel = document.getElementById("ctl00_MainContent_ErrorLabel");
    ErrorLabel.innerText = "";
    var isValid = Page_ClientValidate("");
    if (isValid) {
        $("#ctl00_Body").block({
            message: '<h1>Saving Meet</h1>',
            css: { border: '3px solid #a00',
                    position: 'absolute',
                    left:'0px',
                    top: '0px'}
        });
    }
    return isValid;
}

function Unblock() {
    $("#dialog-modal").dialog("close");
}

function ShowSplashScreen(sender) {
    if (InvalidSwimmers == 0) {
        var Dialog = $("#DialogText").get(0);
        if (ButtonClicked == "LoadButton") {
            Dialog.innerHTML = "Loading...";
            Dialog.title = "Loading Meet";
        }
        else {
            Dialog.innerHTML = "Saving Meet...";
            Dialog.title = "Saving Meet";
        }
        $("#dialog-modal").dialog("open");
    }
}

var ButtonClicked = "";

function LoadMeetClicked(source) {
    InvalidSwimmers = 0;
    ButtonClicked = "LoadButton";
}