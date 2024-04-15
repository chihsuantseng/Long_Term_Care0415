var savecalculate = document.getElementById('save_calculate');
savecalculate.addEventListener('click', function () {
    var rows = document.getElementById('_table').rows;
    var data = [];
    //登入儲存 設key=會員id
    var userid = "id";

    //儲存第0列的資料
    var firstRowData = {
        cmsLevel: document.querySelector('._ranged').selectedIndex,
        cmsLevelName: document.querySelector('._ranged').selectedOptions[0].text,
        householdType: document.querySelector('._typepay').selectedIndex,
        allprice: document.querySelector('._allprice').innerText
    };
    data.push(firstRowData);
    // 儲存B碼其他列的資料
    for (let i = 2; i < rows.length; i++) {
        let rowData = {
            product: rows[i].cells[1].querySelector('._product').selectedIndex,
            productName: rows[i].cells[1].querySelector('._product').selectedOptions[0].text,
            price: rows[i].cells[2].querySelector('._price').innerText,
            realpay: rows[i].cells[3].querySelector('._realpay').innerText,
            quantity: rows[i].cells[4].querySelector('._quantity').value,
            subtotal: rows[i].cells[5].querySelector('._subtotal').innerText
        };
        data.push(rowData);
    }


    console.log(data);

    localStorage.setItem(userid, JSON.stringify(data));


});


function importsp() {
    
    var userid = "id";
    var sadata = localStorage.getItem(userid);

    if (sadata) {
        var data = JSON.parse(sadata);
        // console.log(data);
        document.querySelector('._ranged').selectedIndex = data[0].cmsLevel;
        document.querySelector('._typepay').selectedIndex = data[0].householdType;
        document.querySelector('._allprice').innerText = data[0].allprice;
        var rows = document.getElementById('_table').rows;
        for (var i = 2; i < data.length + 1; i++) {
            rows[i].cells[1].querySelector('._product').selectedIndex = data[i - 1].product;
            rows[i].cells[2].querySelector('._price').innerText = data[i - 1].price;
            rows[i].cells[3].querySelector('._realpay').innerText = data[i - 1].realpay;
            rows[i].cells[4].querySelector('._quantity').value = data[i - 1].quantity;
            rows[i].cells[5].querySelector('._subtotal').innerText = data[i - 1].subtotal;
            if (i != data.length) {
                addRow();
            }
        }
        calculateTotal(); // 重新計算總金額

    }



}






function deleted() {
    var userid = "id";
    localStorage.removeItem(userid);

    window.location.reload();
}

//檢查鍵是否存在
function checked() {
    var userid = "id";
    var data = localStorage.getItem(userid);
    if (data === null) {
        alert("鍵 'id' 不存在");
    } else {
        alert("鍵 'id' 存在");
    }
}


