// step.0 個案姓名存到localStorage

var casename = "gname";
var jsonData = localStorage.getItem(casename);

if (jsonData) {
    var data = JSON.parse(jsonData);
    
    var caseInput = document.querySelector('input[name="CaseName"]');
    caseInput.value = data;
} else {
    console.log("LocalStorage 中沒有資料");
}


//step.1取得試算表的資料

// 使用 JavaScript 取得 localStorage 的資料
var userid = "gid";
var sadata = localStorage.getItem(userid);

if (sadata) {
    var data = JSON.parse(sadata);
    var sheetDataContainer = document.getElementById('sheetDataContainer');

    // 清空 sheetDataContainer 內容，以防止重複添加
    sheetDataContainer.innerHTML = '';

    if (data[0].gquantity !== "0") {
        var newDataButton = document.createElement('button');
        newDataButton.type = 'button';
        newDataButton.classList.add('btn', 'btn-outline-success');
        //// 將數據格式化為易讀格式
        var formattedData = "";
        if (data[0].gcmsName !== undefined) {
            formattedData = `服務項目:${data[0].gservice}、每月次數:${data[0].gquantity}、支付小計:${data[0].gsubtotal} \n\n`;
        }

        if (formattedData.trim() !== "") {
            newDataButton.innerText = `${formattedData}`;
            sheetDataContainer.appendChild(newDataButton);
        }
    }

} else {
    console.log("LocalStorage 中沒有資料");
}



//step.2 初始化fullcalender，取得用戶點擊的日期
var clickedDate;
document.addEventListener('DOMContentLoaded', function () {
    const today = new Date();
    const threeDaysLater = new Date(today);
    threeDaysLater.setDate(today.getDate() + 3); // 將日期設置為今天的三天後

    const calendarEl = document.getElementById('calendar')
    const calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'zh-tw',
        initialView: 'dayGridMonth',
        validRange: {
            start: threeDaysLater // 開始日期設置為今天的三天後
        },
        dateClick: function (info) {
            // 點擊的日期會在 'info' 物件的 'date' 屬性中
            clickedDate = info.date;
            combineDateTime(clickedDate);
            openModal(info.dateStr);
        }
    });

    calendar.render()
})

// 打開彈跳視窗的函數
function openModal(date) {
    var modalEl = document.getElementById('timeSlotModal');
    var modal = new bootstrap.Modal(modalEl);
    modalEl.querySelector('.modal-title').textContent = '您指定日期：' + date;
    modal.show();
}



//step.3 將用戶選中的試算表資料顯示在時段選擇區

//宣告變數存入用戶選中的資料
var ServiceId;

document.addEventListener('DOMContentLoaded', function () {
    // 取得需要操作的元素
    var sheetDataContainer = document.getElementById('sheetDataContainer'); //連結左下帶入的試算表資料
    var sheetdataFromcontainer1 = document.getElementById('sheetDataFromContainer1'); //連結step.3
    var lastClickedButton = null;

    // 在 sheetDataContainer 加上點擊事件
    sheetDataContainer.addEventListener('click', function (event) {

        // 確保點擊的是包含資料的 <div> 元素
        if (event.target.tagName === 'BUTTON') {
            // 清除上一個被點擊的按鈕的樣式
            if (lastClickedButton !== null) {
                lastClickedButton.classList.remove('clicked');
            }

            // 切換新點擊的按鈕的 CSS 類
            event.target.classList.toggle('clicked');

            // 更新上一個被點擊的按鈕為新點擊的按鈕
            lastClickedButton = event.target;

            // 獲取選中的資料字串
            var selectedData = event.target.innerText;

            // 使用正規表達式來提取selectedData字串中的資料
            var extractData = /服務項目:(.*?)、每月次數:(.*?)、支付小計:(.*?)\n\n/;
            var match = selectedData.match(extractData);

            // match[1] 為服務項目，match[2] 為每月次數，match[3] 為支付小計
            ServiceId = {
                productName: match[1],
                quantity: match[2],
                subtotal: match[3]
            };

            // 將選中的資料顯示在 sheetDataFromContainer1
            sheetdataFromcontainer1.innerText = selectedData;
        }
    });

});



//step.3-1 時段選擇器

// 獲取所有的 checkbox 元素
var checkboxes = document.querySelectorAll('input[type="checkbox"]');

// 創建一個空陣列來存放選中的時段
var selectedTimePeriods = [];

checkboxes.forEach(function (checkbox) {
    checkbox.addEventListener('click', function () {
        var maxAllowedQuantity = parseInt(ServiceId.quantity); // 將 quantity 轉換為整數
        var checkedCount = document.querySelectorAll('input[type="checkbox"]:checked').length;

        if (checkedCount > maxAllowedQuantity) {
            // 如果超過了允許的數量，取消選中該 checkbox
            checkbox.checked = false;
            alert(`您這個月最多只能選擇 ${maxAllowedQuantity} 次，若要選擇更多次數請回到上一頁更改`);
            return;
        }

        if (checkbox.checked) {
            // 將 checkbox.value 拆分為開始時間和結束時間
            var timePeriod = checkbox.value.split('~');
            // 將拆分後的時間添加到 selectedTimePeriods
            selectedTimePeriods.push({
                StartTime: timePeriod[0].trim(),
                EndTime: timePeriod[1].trim()
            });
        } else {
            // 移除取消勾選的時間段
            var index = selectedTimePeriods.findIndex(function (period) {
                return period.StartTime + '~' + period.EndTime === checkbox.value;
            });
            if (index !== -1) {
                selectedTimePeriods.splice(index, 1);
            }
        }
        combineDateTime();
    });
});



// 組合日期和時段的函數
var combinedDateTimeArray;
function combineDateTime(date) {
    // 創建一個空陣列來存放組合後的日期與時段
    combinedDateTimeArray = [];

    // 迭代每個 selectedTimePeriod
    selectedTimePeriods.forEach(function (timePeriod) {
        // 格式化日期與時段，並添加到組合後的陣列中
        var formattedDateTime = {
            ServiceId: ServiceId.productName,
            WorkingDate: formatDate(clickedDate),
            StartTime: timePeriod.StartTime,
            EndTime: timePeriod.EndTime
        };
        combinedDateTimeArray.push(formattedDateTime);
    });

}

combineDateTime();

// 格式化日期的輔助函數
function formatDate(date) {
    var day = date.getDate();
    var month = date.getMonth() + 1; // 月份從0開始，所以需要加1
    var year = date.getFullYear();
    var formattedDate = `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
    return formattedDate;
}



//step.4 組合所有用戶選擇的資料
var combineAllData = document.getElementById('CombineAllData');
var combinedDataContainer = document.getElementById('combinedDataContainer');
var allCombinedData = [];
var FinalData;
combineAllData.addEventListener('click', function () {
    // 確保用戶已選擇試算表資料和時段
    if (ServiceId && combinedDateTimeArray.length > 0) {
        allCombinedData.push(combinedDateTimeArray);

        // 清空舊的內容
        combinedDataContainer.innerHTML = "";

        // 用於儲存每個日期的數據
        var groupedData = {};

        // 將數據按照日期分組
        allCombinedData.forEach(function (data) {
            data.forEach(function (dateTime) {
                var WorkingDate = dateTime.WorkingDate;
                if (!groupedData[WorkingDate]) {
                    groupedData[WorkingDate] = [];
                }
                groupedData[WorkingDate].push(dateTime);
            });
        });

        // 顯示每個日期的數據
        Object.keys(groupedData).forEach(function (WorkingDate) {
            var dateDiv = document.createElement('div');
            dateDiv.classList.add('border', 'border-success', 'mt-2', 'mb-2', 'p-2');
            dateDiv.innerHTML += `<p>${WorkingDate}</p>`;

            groupedData[WorkingDate].forEach(function (dateTime) {
                dateDiv.innerHTML += `<p>${dateTime.ServiceId}， ${dateTime.StartTime} ~ ${dateTime.EndTime}</p>`;
            });

            // 添加刪除按鈕
            dateDiv.innerHTML += `<button class="btn btn-light" onclick="deleteCombinedData('${WorkingDate}')">刪除</button>`;

            // 將新創建的 div 元素添加到 combinedDataContainer
            combinedDataContainer.appendChild(dateDiv);

            FinalData = [].concat.apply([], allCombinedData);


        });
    } else {
        alert("請先選擇服務項目，再指定時段");
    }
    console.log(FinalData);
});

// 讓用戶手動刪除指定日期的所有資料
function deleteCombinedData(WorkingDateToDelete) {
    // 過濾掉指定日期的資料
    FinalData = FinalData.filter(function (dateTime) {
        return dateTime.WorkingDate !== WorkingDateToDelete;
    });

    // 從 allCombinedData 陣列中刪除相應的資料
    allCombinedData = allCombinedData.filter(function (data) {
        return data[0].WorkingDate !== WorkingDateToDelete;
    });

    // 清空 combinedDataContainer 内容
    combinedDataContainer.innerHTML = "";

    // 重新顯示所有combinedData物件
    renderFinalData();
    console.log(FinalData);
}


// 重新顯示所有 FinalData 物件
function renderFinalData() {
    combinedDataContainer.innerHTML = "";

    // 重新分組
    var groupedData = {};
    FinalData.forEach(function (dateTime) {
        var WorkingDate = dateTime.WorkingDate;
        if (!groupedData[WorkingDate]) {
            groupedData[WorkingDate] = [];
        }
        groupedData[WorkingDate].push(dateTime);
    });

    // 顯示每個日期的數據
    Object.keys(groupedData).forEach(function (WorkingDate) {
        var dateDiv = document.createElement('div');
        dateDiv.classList.add('border', 'border-success', 'mt-2', 'mb-2', 'p-2');
        dateDiv.innerHTML += `<p>${WorkingDate}</p>`;

        groupedData[WorkingDate].forEach(function (dateTime) {
            dateDiv.innerHTML += `<p>${dateTime.ServiceId}， ${dateTime.StartTime} ~ ${dateTime.EndTime}</p>`;
        });

        // 添加刪除按鈕
        dateDiv.innerHTML += `<button class="btn btn-light" onclick="deleteCombinedData('${WorkingDate}')">刪除</button>`;

        // 將新創建的 div 元素添加到 combinedDataContainer
        combinedDataContainer.appendChild(dateDiv);
    });
}