$(document).ready(function () {
    //增加商品
    $(".NumberPlus").click(function () {
        var input = $(this).siblings("div").find("input[type='number']");
        var currentValue = parseInt(input.val());
        var maxValue = parseInt(input.attr("max"));
        if (currentValue < maxValue) {
            input.val(currentValue + 1);
        }
    });

    //減少商品
    $(".Numberminus").click(function () {
        var input = $(this).siblings("div").find("input[type='number']");
        var currentValue = parseInt(input.val());
        var minValue = parseInt(input.attr("min"));
        if (currentValue > minValue) {
            input.val(currentValue - 1);
        }
    });
});
function addToCar(itemId, itemName) {
    // 獲取對應商品的 input 元素的值
    var quantity = document.getElementById("quantity_" + itemId).value;
    // 構建 URL，包含 SupplementId 和 Quantity 參數
    var url = "/Shopping/AddCar?SupplementId=" + itemId + "&Quantity=" + quantity;
    // 重定向到該 URL
    window.location.href = url;
    var name = itemName;
    alert('已將★ ' + name + ' : ' + quantity + '件 ★加入購物車');
}

function addToCar_form(itemId, itemName) {
    // 獲取對應商品的 input 元素的值
    var quantity = document.getElementById("quantity_" + itemId).value;
    // 構建 URL，包含 SupplementId 和 Quantity 參數
    var url = "/Shopping/AddCar_form?SupplementId=" + itemId + "&Quantity=" + quantity;
    // 重定向到該 URL
    window.location.href = url;
    var name = itemName;
    alert('已將★ ' + name + ' : ' + quantity + '件 ★加入購物車');
}