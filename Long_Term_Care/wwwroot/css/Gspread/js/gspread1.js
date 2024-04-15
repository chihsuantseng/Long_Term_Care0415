document.addEventListener('DOMContentLoaded', function () {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    document.getElementById('g_date').value = today;
});

window.onload = function () {

    if (OrAddtotal != 0) {
        var rangedSelect = document.getElementById('gsel_ran');
        rangedSelect.style.display = 'none';
        var cmsranged = document.getElementById('gcmsranged');
        cmsranged.style.display = 'block';
    }



};

let rows = document.getElementById('_table2').rows;
var Allprice = parseInt(rows[0].cells[5].innerText);
var OrAddtotal = parseInt(document.getElementById('gAddtotal').value);

if (OrAddtotal <= Allprice) {

    document.getElementById('g_usetotal').innerText = '前次累計使用額度：' + OrAddtotal;
} else {

    document.getElementById('g_usetotal').innerText = '前次累計使用額度：' + parseInt(rows[0].cells[5].innerText) + '(額度已滿)';
}

//設定資料庫的原始初始值
var OrBalnum = parseInt(document.getElementById('gBalnum').value);
var OrAllprice = parseInt(document.getElementById('gAllprice').innerText);  //CMS
//當月前次累計自費額
var OrOwnexp = parseInt(document.getElementById('grownexp').innerText);
console.log(OrAllprice);
console.log(OrAddtotal);
console.log(OrBalnum);
console.log(OrOwnexp);

function Gallowance(element) {
    let allp = element.parentElement.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling;
    allp.innerText = element.value;
    GcalculateTotal();

    //獲取選項的文字
    var selectedIndex = document.querySelector('.g_ranged').selectedIndex;
    var selectedOptionText = document.querySelector('.g_ranged').options[selectedIndex].text;
    console.log(selectedOptionText);
    document.getElementById('gcmsranged').value = selectedOptionText;


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
    rows[2].cells[5].innerText = Math.floor(y);
    GcalculateTotal();
}

function GcalculateTotal() {
    let rows = document.getElementById('_table2').rows;


    var a = parseFloat(rows[2].cells[2].innerText) * parseInt(rows[2].cells[4].firstElementChild.value);

    document.getElementById('gAddtotal').value = OrAddtotal + a;


    //第二次以後申請
    if (OrAddtotal != 0) {
        var rangedSelect = document.getElementById('gsel_ran');
        rangedSelect.style.display = 'none';
        var cmsranged = document.getElementById('gcmsranged');
        cmsranged.style.display = 'block';
        //在補助額度內
        if (a < OrBalnum) {


            document.getElementById('gBalnum').value = parseInt(rows[0].cells[5].innerText) - OrAddtotal - a;
            document.getElementById('gOwnexp').innerText = 0;
            document.getElementById('g_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(rows[2].cells[5].innerText));
        }
        //超出補助額度
        else {


            document.getElementById('gBalnum').value = 0;

            //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
            var b = parseInt(document.getElementById('gAddtotal').value);
            var y = b - parseInt(rows[0].cells[5].innerText);
            var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
            var lastown = parseInt(document.getElementById('grownexp').innerText);
            document.getElementById('gOwnexp').innerText = Math.floor(z) - parseInt(lastown);

            var own = document.getElementById('gOwnexp').innerText;
            document.getElementById('g_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(rows[2].cells[5].innerText) + parseInt(own));
        }

    }
    //首次申請
    else {
        var Balnum = parseInt(rows[0].cells[5].innerText);
        if (a <= Balnum) {


            document.getElementById('gBalnum').value = parseInt(rows[0].cells[5].innerText) - a;
            document.getElementById('gOwnexp').innerText = 0;
            document.getElementById('g_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(rows[2].cells[5].innerText));
        } else {


            document.getElementById('gBalnum').value = 0;

            //全部金額-補助額=全自費額   裡面有16%已經算了，剩下84%
            var b = parseInt(document.getElementById('gAddtotal').value);
            var y = b - parseInt(rows[0].cells[5].innerText);
            var z = y * (1 - parseFloat(rows[2].cells[3].innerText));
            var lastown = parseInt(document.getElementById('grownexp').innerText);
            document.getElementById('gOwnexp').innerText = Math.floor(z) - parseInt(lastown);

            var own = document.getElementById('gOwnexp').innerText;
            document.getElementById('g_totalpay').innerText = '總支付金額：$' + Math.floor(parseInt(rows[2].cells[5].innerText) + parseInt(own));
        }



    }


    var rownexp = parseInt(document.getElementById('grownexp').innerText);
    var ownexp = parseInt(document.getElementById('gOwnexp').innerText);
    document.getElementById('gowndata').value = rownexp + ownexp;

}




