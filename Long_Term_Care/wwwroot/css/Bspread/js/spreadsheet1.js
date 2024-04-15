
//document.addEventListener('DOMContentLoaded', function () {
window.onload = function () {
    addRow();
    //設定為今日日期
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mmo = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    var yyyy = today.getFullYear();
    console.log(mmo);

    var abc = yyyy + '-' + mmo + '-' + dd;
    document.getElementById('spr_date').value = abc;
    console.log(abc);

    if (OrAddtotal != 0) {
        var rangedSelect = document.getElementById('sel_ran');
        rangedSelect.style.display = 'none';
        var cmsranged = document.getElementById('cmsranged');
        cmsranged.style.display = 'block';
    }

   
    



};



let rows = document.getElementById('_table').rows;
var Allprice = parseInt(rows[0].cells[5].innerText);
var OrAddtotal = parseInt(document.getElementById('Addtotal').value);

if (OrAddtotal <= Allprice) {

    document.getElementById('_usetotal').innerText = '當月前次累計使用額度：' + OrAddtotal;
} else {

    document.getElementById('_usetotal').innerText = '當月前次累計使用額度：' + parseInt(rows[0].cells[5].innerText) + '(額度已滿)';
}

//設定資料庫的原始初始值
var OrBalnum = parseInt(document.getElementById('Balnum').value);
var OrAllprice = parseInt(document.getElementById('Allprice').innerText);  //CMS
//當月前次累計自費額
var OrOwnexp = parseInt(document.getElementById('rownexp').innerText);
console.log(OrAllprice);
console.log(OrAddtotal);
console.log(OrBalnum);
console.log(OrOwnexp);


function allowance(element) {
    let allprice = element.parentElement.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling;
    allprice.innerText = element.value;
    calculateTotal();

    //獲取選項的文字
    var selectedIndex = document.querySelector('._ranged').selectedIndex;
    var selectedOptionText = document.querySelector('._ranged').options[selectedIndex].text;
    console.log(selectedOptionText);
    document.getElementById('cmsranged').value = selectedOptionText;

}



function addRow() {
    event.preventDefault();
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
    event.preventDefault();//這句意思是我的按鈕可能被預設為submit，所以要阻止他的預設行為
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
    subtotal.innerText = Math.floor(element.value * price.innerText * discount.innerText);

    calculateTotal();
}





function calculateTotal() {
    let subtotals = Array.from(document.getElementsByClassName('_subtotal'));
    let total = 0;
    subtotals.map(subtotal => {
        total += Number(subtotal.innerText);
    });
    document.getElementById('_total').innerText = '小計合計：' + Math.floor(total);

    let rows = document.getElementById('_table').rows;

    for (var i = 2, a = 0; i < rows.length; i++) {
        a += parseFloat(rows[i].cells[2].innerText) * parseInt(rows[i].cells[4].firstElementChild.value);
    }
    document.getElementById('Addtotal').value = OrAddtotal + a;
    //document.getElementById('Balnum').value = parseInt(rows[0].cells[5].innerText);
    //var Balnum = parseInt(document.getElementById('Balnum').value);

    //第二次以後申請
    if (OrAddtotal != 0) {
        var rangedSelect = document.getElementById('sel_ran');
        rangedSelect.style.display = 'none';
        var cmsranged = document.getElementById('cmsranged');
        cmsranged.style.display = 'block';
        //在補助額度內
        if (a < OrBalnum) {

            document.getElementById('Balnum').value = parseInt(rows[0].cells[5].innerText) - OrAddtotal - a;
            document.getElementById('Ownexp').innerText = 0;
            document.getElementById('_totalpay').innerText = '總支付金額：$' + Math.floor(total);
        }
        //超出補助額度
        else {

            document.getElementById('Balnum').value = 0;

            //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
            var b = parseInt(document.getElementById('Addtotal').value);
            var y = b - parseInt(rows[0].cells[5].innerText);
            var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
            var lastown = parseInt(document.getElementById('rownexp').innerText);
            document.getElementById('Ownexp').innerText = Math.floor(z) - parseInt(lastown);

            var own = document.getElementById('Ownexp').innerText;
            document.getElementById('_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(total) + parseInt(own));
        }

    }
    //首次申請
    else {
        var Balnum = parseInt(rows[0].cells[5].innerText);
        if (a <= Balnum) {


            document.getElementById('Balnum').value = parseInt(rows[0].cells[5].innerText) - a;
            document.getElementById('Ownexp').innerText = 0;
            document.getElementById('_totalpay').innerText = '總支付金額：$' + Math.floor(total);
        } else {


            document.getElementById('Balnum').value = 0;

            //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
            var b = parseInt(document.getElementById('Addtotal').value);
            var y = b - parseInt(rows[0].cells[5].innerText);
            var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
            var lastown = parseInt(document.getElementById('rownexp').innerText);
            document.getElementById('Ownexp').innerText = Math.floor(z) - parseInt(lastown);

            var own = document.getElementById('Ownexp').innerText;
            document.getElementById('_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(total) + parseInt(own));
        }



    }


    var rownexp = parseInt(document.getElementById('rownexp').innerText);
    var ownexp = parseInt(document.getElementById('Ownexp').innerText);
    document.getElementById('owndata').value = rownexp + ownexp;

}



