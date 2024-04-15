//使用localstorage儲存資料
var gsavecalculate = document.getElementById('g_save');
gsavecalculate.addEventListener('click', function () {
    // var rows = document.getElementById('_table2').rows;
    var data = [];
    //登入儲存 設key=g+會員id
    var userid = "gid";

    //儲存G碼的資料
    var gData = {
        gcms: document.querySelector('.g_ranged').selectedIndex,
        gcmsName: document.querySelector('.g_ranged').selectedOptions[0].text,
        ghousetype: document.querySelector('.g_typepay').value,
        gallprice: document.querySelector('.g_allprice').innerText,
        gquantity: document.querySelector('.g_quantity').value,
        gsubtotal: document.querySelector('.g_subtotal').innerText,
        gservice: document.getElementById('g_service').innerText,
        gseven: document.getElementById('g_seven').innerText
    };
    data.push(gData);

    console.log(data);

    localStorage.setItem(userid, JSON.stringify(data));

    
})

//function gimportsp() {
//    var userid = "gid";
//    var sadata = localStorage.getItem(userid);

//    if (sadata) {
//        var data = JSON.parse(sadata);
//        // console.log(data);

//        document.querySelector('.g_ranged').selectedIndex = data[0].gcms;
//        document.querySelector('.g_typepay').value = data[0].ghousetype;
//        document.querySelector('.g_allprice').innerText = data[0].gallprice;
//        document.querySelector('.g_realpay').innerText = data[0].ghousetype;
//        document.querySelector('.g_quantity').value = data[0].gquantity;
//        document.querySelector('.g_subtotal').innerText = data[0].gsubtotal;
//        GcalculateTotal()

       

       
//    }

    

//}






function gdeleted() {
    var userid = "gid";
    localStorage.removeItem(userid);
   

    window.location.reload();
}

//檢查鍵是否存在
function gchecked() {
    
    var userid = "gid";
    localStorage.removeItem(userid);
    if (data === null) {
        alert("鍵 'id' 不存在");
    } else {
        alert("鍵 'id' 存在");
    }
}
