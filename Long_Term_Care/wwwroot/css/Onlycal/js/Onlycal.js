function allowance(element) {
    let allprice = element.parentElement.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling;
    allprice.innerText = element.value;
    calculateTotal();

}

function addRow() {
    let table = document.getElementById('_table');
    let row = table.insertRow(-1);
    let cell0 = row.insertCell(0);
    let cell1 = row.insertCell(1);
    let cell2 = row.insertCell(2);
    let cell3 = row.insertCell(3);
    let cell4 = row.insertCell(4);
    let cell5 = row.insertCell(5);
    cell0.innerHTML = '<input type="checkbox">';
    cell1.innerHTML = '<select class="_product" onchange="updatePrice(this)"><option value="260">BA01基本身體清潔</option><option value="195">BA02基本日常照顧</option><option value="35">BA03測量生命徵象</option><option value="130">BA04協助進食或管灌</option><option value="310">BA05餐食照顧</option><option value="325">BA07協助沐浴及洗頭</option><option value="500">BA08足部照護</option><option value="155">BA10翻身拍背</option><option value="195">BA11肢體關節</option><option value="130">BA12協助上下樓梯</option><option value="195">BA13陪同外出</option><option value="685">BA14陪同就醫</option><option value="195">BA15家務協助</option><option value="130">BA16代購服務</option><option value="50">BA17c管路清潔</option><option value="50">BA17d1血糖機驗血糖</option><option value="50">BA17d2甘油球通便</option><option value="50">BA17e依指示置入藥盒</option><option value="200">BA18安全看視</option><option value="175">BA20陪伴服務</option><option value="130">BA22巡視服務</option><option value="200">BA23協助洗頭</option><option value="220">BA24協助排泄</option></select>';
    cell2.innerHTML = '<p class="_price">260</p>';
    let selectedTypePay = document.querySelector('._typepay').value;
    cell3.innerHTML = `<p class="_realpay">${selectedTypePay}</p>`;
    cell4.innerHTML = '<input class="_quantity" id="_quan" type="number" value="0" min="0" oninput="updateSubtotal(this)">';
    cell5.innerHTML = '<p class="_subtotal">0</p>';


    calculateTotal();
}


function deleteRow() {
    let table = document.getElementById('_table');
    let checkboxes = Array.from(table.getElementsByTagName('input'));
    let rowsToDelete = [];
    checkboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            rowsToDelete.push(checkbox.parentNode.parentNode.rowIndex);
        }
    });
    for (let i = rowsToDelete.length - 1; i >= 0; i--) {
        table.deleteRow(rowsToDelete[i]);
    }
    calculateTotal();
}


function updatePrice(element) {
    let price = element.parentElement.nextElementSibling.firstElementChild;
    price.innerText = element.value;

    let quantity = price.parentElement.nextElementSibling.nextElementSibling.firstElementChild;
    updateSubtotal(quantity);

}
function updatetwo(element) {
    let rows = document.getElementById('_table').rows;
    let selectedTypePay = element.value;
    for (let i = 2; i < rows.length; i++) {
        rows[i].cells[3].innerHTML = `<p class="_realpay">${selectedTypePay}</p>`;
        let quantity = rows[i].cells[4].firstElementChild;
        updateSubtotal(quantity);
    }

    calculateTotal();

}


function updateSubtotal(element) {
    let price = element.parentElement.previousElementSibling.previousElementSibling.firstElementChild;
    let discount = element.parentElement.previousElementSibling.firstElementChild;
    let subtotal = element.parentElement.nextElementSibling.firstElementChild;
    subtotal.innerText = (element.value * price.innerText * discount.innerText).toFixed(0);

    calculateTotal();
}

function calculateTotal() {
    let subtotals = Array.from(document.getElementsByClassName('_subtotal'));
    let total = 0;
    subtotals.map(subtotal => {
        total += Number(subtotal.innerText);
    });
    document.getElementById('_total').innerText = '小計合計：' + Math.floor(total).toLocaleString();

    let rows = document.getElementById('_table').rows;
    for (var i = 2, x = 0; i < rows.length; i++) {
        x += parseFloat(rows[i].cells[2].innerText) * parseInt(rows[i].cells[4].firstElementChild.value);
    }
    if (x <= rows[0].cells[5].innerText) {
        document.getElementById('_alltotal').innerText = '補助使用額度：' + Math.floor(x).toLocaleString();
        document.getElementById('_ownexp').innerText = '超出額度應自費額：' + 0;
        document.getElementById('_totalpay').innerText = '總支付金額：NT$' + Math.floor(total).toLocaleString();
    } else {
        document.getElementById('_alltotal').innerText = '補助使用額度：' + parseInt(rows[0].cells[5].innerText).toLocaleString() + '(**額度已滿**)';
        //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
        var y = x - parseInt(rows[0].cells[5].innerText);
        var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
        document.getElementById('_ownexp').innerText = '超出額度應自費額：' + Math.floor(z).toLocaleString();
        //補助額度*比率   + 全自費額
        var ztot = parseInt(rows[0].cells[5].innerText) * parseFloat(rows[2].cells[3].innerText) + y;
        document.getElementById('_totalpay').innerText = '總支付金額：NT$' + Math.floor(ztot).toLocaleString();

    }

}



//以下為G碼
function Gallowance(element) {
    let allp = element.parentElement.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling;
    allp.innerText = element.value;
    GcalculateTotal();
}
function Gupdatetwo(element) {
    let rows = document.getElementById('_table2').rows;
    rows[2].cells[3].innerText = element.value;
    let quantity = rows[2].cells[4].firstElementChild;
    GupdateSubtotal(quantity);
    GcalculateTotal();
}
function GupdateSubtotal(element) {
    let rows = document.getElementById('_table2').rows;
    let y = parseInt(rows[2].cells[2].innerText) * parseFloat(rows[2].cells[3].innerText) * rows[2].cells[4].firstElementChild.value;
    rows[2].cells[5].innerText = Math.floor(y).toLocaleString();
    GcalculateTotal();
}
function GcalculateTotal() {
    let rows = document.getElementById('_table2').rows;

    let x = parseFloat(rows[2].cells[2].innerText) * parseInt(rows[2].cells[4].firstElementChild.value);

    if (x <= rows[0].cells[5].innerText) {
        document.getElementById('g_alltotal').innerText = '補助使用額度：' + Math.floor(x).toLocaleString();
        document.getElementById('g_ownexp').innerText = '超出額度應自費額：' + 0;
        document.getElementById('g_totalpay').innerText = '總支付金額：NT$' + rows[2].cells[5].innerText;
    } else {
        document.getElementById('g_alltotal').innerText = '補助使用額度：' + parseInt(rows[0].cells[5].innerText).toLocaleString() + '(**額度已滿**)';
        //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
        var y = x - parseInt(rows[0].cells[5].innerText);
        var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
        document.getElementById('g_ownexp').innerText = '超出額度應自費額：' + Math.floor(z).toLocaleString();
        //補助額度*比率   + 全自費額
        var ztot = parseInt(rows[0].cells[5].innerText) * parseFloat(rows[2].cells[3].innerText) + y;
        document.getElementById('g_totalpay').innerText = '總支付金額：NT$' + Math.floor(ztot).toLocaleString();

    }
}