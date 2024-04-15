// 獲取體溫元素
var temp = document.querySelector('#_temp');

// 獲取體溫的值
var temperature = parseFloat(temp.innerText);

// 定義正常范围
var TemperatureMin = 35;
var TemperatureMax = 37.4;

// 检查體溫是否超出正常范围，如果是，添加红色樣式
if (temperature < TemperatureMin || temperature > TemperatureMax) {
    temp.style.color = 'red';
}

var heart = document.querySelector('#_heart');
var heartbeat = parseFloat(heart.innerText);
var heartMin = 60;
var heartMax = 100;

if (heartbeat < heartMin || heartbeat > heartMax) {
    heart.style.color = 'red';
}

var brea = document.querySelector('#_brea');
var breathe = parseFloat(brea.innerText);
var breaMin = 12;
var breaMax = 20;

if (breathe < breaMin || breathe > breaMax) {
    brea.style.color = 'red';
}

var befglu = document.querySelector('#_befglu');
var befglucose = parseFloat(befglu.innerText);
var befgluMin = 70;
var befgluMax = 100;

if (befglucose < befgluMin || befglucose > befgluMax) {
    befglu.style.color = 'red';
}

var aftglu = document.querySelector('#_aftglu');
var aftglucose = parseFloat(aftglu.innerText);
var aftgluMin = 80;
var aftgluMax = 140;

if (aftglucose < aftgluMin || aftglucose > aftgluMax) {
    aftglu.style.color = 'red';
}

var sbp = document.querySelector('#_sbp');
var sbpvalue = parseFloat(sbp.innerText);
var sbpMin = 90;
var sbpMax = 140;

if (sbpvalue < sbpMin || sbpvalue > sbpMax) {
    sbp.style.color = 'red';
}

var dbp = document.querySelector('#_dbp');
var dbpvalue = parseFloat(dbp.innerText);
var dbpMin = 60;
var dbpMax = 90;

if (dbpvalue < dbpMin || dbpvalue > dbpMax) {
    dbp.style.color = 'red';
}