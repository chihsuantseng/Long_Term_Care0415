$('#xdd2').on('click', function () {
    $('#word2+div').remove()
    var passwordcheck = $('#word1').val() == $('#word2').val()
    let str = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,18}$/
    let sd = str.test(document.getElementById("word1").value)
    let regac = /^[a-zA-Z]\w*$/
    let ac = regac.test(document.getElementById("inputAccount1").value)
    let regemail = /^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4})*$/
    let email = regemail.test(document.getElementById("inputemail").value)
    let regphone = /^09\d{2}-?\d{3}-?\d{3}$/
    let phone = regphone.test(document.getElementById("inputphone").value)
    let regidnumber = /^[A-Za-z][1-2]\d{8}$/
    let idnumber = regidnumber.test(document.getElementById("inputidnumber").value)
    if (!passwordcheck) {
        $('#word2').after('<div>兩次密碼不相符</div>')
        $('#word2+div').css('color', 'red')
        return false
    }
    if (!sd) {
        alert("請輸入6 位數以上，並且至少包含 大寫字母、小寫字母、數字")
        return false
    }
    if (!ac) {
        alert(" 第一個字不為數字，只接受 大小寫字母、數字及底線 ")
        return false
    }
    if (!email) {
        alert("電子郵件格式不正確 ")
        return false
    }
    if (!phone) {
        alert("手機格式不正確 ")
        return false
    }
    if (!idnumber) {
        alert("身分證格式不正確 ")
        return false
    }
    else {
        $('#word2').after('<div>成功</div>')
        $('#word2+div').css('color', 'red')
        return true
    }
})