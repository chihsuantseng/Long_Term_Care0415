//var now = new Date();
//var year = now.getFullYear();
//var month = now.getMonth() + 1; // getMonth() 從 0 開始，所以我們需要加 1
//var day = now.getDate();
//var hours = now.getHours();
//var minutes = now.getMinutes();

//// 確保月份和日期總是兩位數
//if (month < 10) month = '0' + month;
//if (day < 10) day = '0' + day;
//if (hours < 10) hours = '0' + hours;
//if (minutes < 10) minutes = '0' + minutes;

//document.getElementById('_date').value = year + '/' + month + '/' + day;
//document.getElementById('_time').value = hours + ':' + minutes;

function tempRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(35);
    var max = parseFloat(37.4);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function heartRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(60);
    var max = parseFloat(100);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function breaRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(12);
    var max = parseFloat(20);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function befRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(70);
    var max = parseFloat(100);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function aftRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(80);
    var max = parseFloat(140);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}

function sbpRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(90);
    var max = parseFloat(140);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}
function dbpRange(input) {
    var value = parseFloat(input.value);
    var min = parseFloat(60);
    var max = parseFloat(90);

    if (value < min || value > max) {
        input.style.color = "red";
    } else {
        input.style.color = ""; // Reset to default color
    }
}