var now = new Date();
var year = now.getFullYear();
var month = now.getMonth() + 1; // getMonth() 從 0 開始，所以我們需要加 1
var day = now.getDate();
var hours = now.getHours();
var minutes = now.getMinutes();

// 確保月份和日期總是兩位數
if (month < 10) month = '0' + month;
if (day < 10) day = '0' + day;
if (hours < 10) hours = '0' + hours;
if (minutes < 10) minutes = '0' + minutes;

document.getElementById('_date').value = year + '/' + month + '/' + day;
document.getElementById('_time').value = hours + ':' + minutes;

function checkRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(input.min);
    var max = parseFloat(input.max);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function saveData() {
    var name = document.getElementById("_name").value;
    var date = document.getElementById("_date").value;
    var time = document.getElementById("_time").value;

    var doview = {
        name: name,
        date: date,
        time: time,
        path: "recordForm.html"
    }
    var oldData = JSON.parse(localStorage.getItem("allview")) || [];
    oldData.push(doview);
    localStorage.setItem("allview", JSON.stringify(oldData));

    //key設為name+date+time
    var casekey = "皮卡丘";
    // var casekey = "小火龍";
    var data = {
        name: document.getElementById("_name").value,
        date: document.getElementById("_date").value,
        time: document.getElementById("_time").value,
        temperature: document.getElementById("_temperature").value,
        heart: document.getElementById("heart_beat").value,
        breathe: document.getElementById("_breathe").value,
        befmeal: document.getElementById("bef_meal").value,
        aftmeal: document.getElementById("aft_meal").value,
        systolic: document.getElementById("_systolic").value,
        diastolic: document.getElementById("_diastolic").value,
        breakfast: document.getElementById("_breakfast").value,
        lunch: document.getElementById("_lunch").value,
        dinner: document.getElementById("_dinner").value,
        diaper: document.getElementById("_diaper").value,
        other: document.getElementById("_others").value
    }


    localStorage.setItem(casekey, JSON.stringify(data));

    //清空輸入欄位
    document.getElementById("_name").value = "";
    document.getElementById("_temperature").value = "";
    document.getElementById("heart_beat").value = "";
    document.getElementById("_breathe").value = "";
    document.getElementById("bef_meal").value = "";
    document.getElementById("aft_meal").value = "";
    document.getElementById("_systolic").value = "";
    document.getElementById("_diastolic").value = "";
    document.getElementById("_breakfast").value = "";
    document.getElementById("_lunch").value = "";
    document.getElementById("_dinner").value = "";
    document.getElementById("_diaper").value = "";
    document.getElementById("_others").value = "";


    //顯示資料
    displayData();
    // displayData1();
}


function deleteData() {
    var casekey = "皮卡丘";
    // var casekey = "小火龍";
    localStorage.removeItem(casekey);
    localStorage.removeItem("allview");
}

//檢查鍵是否存在
function checkKey() {
    var casekey = "皮卡丘";
    // var casekey = "overview"
    // var casekey= "小火龍";
    var data = localStorage.getItem(casekey);
    if (data === null) {
        alert("鍵 'casekey' 不存在");
    } else {
        alert("鍵 'casekey' 存在");
    }
}


