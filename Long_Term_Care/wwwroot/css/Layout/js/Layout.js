// ※ 頁面在滾動時，top bar往上移動並fixed navbar

// 設置事件監聽，當頁面發生滾動時觸發
window.onscroll = function () { scroll_event() };

// 選擇id= "barfixed" 定義變數
var header = document.getElementById("barfixed");

// offsetTOP 獲取header與頁面頂端的距離，sticky會用於確定何時固定住navbar
var sticky = header.offsetTop;

// 定義上面的叫scroll_event函式的監聽
function scroll_event() {

  // 檢查頁面垂直滾動的Y軸偏移量是否大於 sticky 的值
  if (window.pageYOffset > sticky) {

    // 如果已經滾到大於 sticky 的值，就增加在css樣式表裡的.sticky樣式
    header.classList.add("sticky");
  } else {

    // 反之就移除.sticky樣式
    header.classList.remove("sticky");
  }
}

// ----------------------------------------------------------------------------------------------------//

// ※ Mobile&Pad 點擊Button展開list後，點空白處可以自動收回列表的功能

// 監聽文件載入
document.addEventListener("DOMContentLoaded", function () {
  // 定義navbar變數，從文件中選擇.js_navbar這個類別
  // 因為.js_navbar只下一個，所以這裡用querySelector方法只會選擇文件中第一個匹配到的元素
  var navbar = document.querySelector('.js_navbar');


  // 監聽點擊事件
  document.addEventListener('click', function (event) {
    // 檢查點擊事件是否發生在 navbar 以外的地方且 navbar 目前是展開的狀態
    var isOutsideNavbar = !navbar.contains(event.target) && navbar.classList.contains('show');

    if (isOutsideNavbar) {
      // 關閉 navbar
      var toggler = document.querySelector('.navbar-toggler');
      toggler.click(); // 模擬點擊 toggler 按鈕
    }
  });
});


// -----------------------------------------------------------------------------------------------------//

// ※ 回到頂部的button

// 宣告使用window物件上的requestAnimationFrame方法，或者如果這個方法在該瀏覽器中不存在，則會依次嘗試尋找其他瀏覽器的前綴版本。
let requestAnimationFrame = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame;

// 當HTML的DOM內容已經完全載入完成時，會觸發這個DOMContentLoaded事件
document.addEventListener('DOMContentLoaded', function () {

  // 選擇data-action="gotop"的元素，賦給goTopButton
  const goTopButton = document.querySelector('[data-action="gotop"]');

  // 將載入網頁視窗時的可視高度賦值給windowViewPortHeight
  const windowViewPortHeight = window.innerHeight; // browser viewport height

  // 宣告變數，並將初值設為false(用於追蹤requestAnimationFrame是否已經被請求)
  let isRequestingAnimationFrame = false;

  // 如果沒有找到goTopButton這個元素，則退出函式
  if (!goTopButton) {
    return;
  }

  // 給Button增加點擊事件的監聽，當按鈕點擊時，會執行後續的code，將頁滾回頂部
  goTopButton.addEventListener('click', function () {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'  // 平滑效果
    });
  });

  // 給視窗加一個滾動事件監聽，當用戶滾動頁面時，會執行後續的code
  window.addEventListener('scroll', function () {
    if (!isRequestingAnimationFrame) {

      // 請求瀏覽器在下一次畫面重繪時執行
      requestAnimationFrame(filterGoTopButtonVisibility);
      isRequestingAnimationFrame = true;
    }
  });

  // 定義函式，用在根據滾動的距離來顯示或隱藏回到頂部的按鈕
  function filterGoTopButtonVisibility(timestamp) {

    // 獲取當前視窗的Y軸滾動偏移量
    let windowPageYOffset = window.pageYOffset || document.documentElement.scrollTop;

    // 指定的滾動距離值為500px
    let scrollThreshold = 500;

    // 當滾距離超過以上設定的600px時，Button就會顯示出來，否則就隱藏
    if (windowPageYOffset > scrollThreshold) {
      goTopButton.classList.add('show');
      isRequestingAnimationFrame = false;
    } else {
      goTopButton.classList.remove('show');
      isRequestingAnimationFrame = false;
    }
  }
})


// Robot
// Robot CSS
const styleSet = window.WebChat.createStyleSet({
    bubbleBackground: 'rgba(0, 0, 255, .1)',
    bubbleFromUserBackground: 'rgba(0, 255, 0, .1)',
    rootHeight: '500px',
    rootWidth: '400px',
    backgroundColor: '#FAF8F3'
});


styleSet.textContent = {
    ...styleSet.textContent,
    fontFamily: "'Comic Sans MS', 'Arial', sans-serif",
    fontWeight: 'bold'
};


const avatarOptions = {
    botAvatarImage: 'https://bot-framework.azureedge.net/bot-icons-v1/chat-bot-dev-chuppy_FTwGTkGSq2FLD0B8bc5Zb6IBiW33R1Gq45t1G6EYRM14Lh.png',
    botAvatarInitials: '馨',
    userAvatarInitials: 'C'
};

window.WebChat.renderWebChat({
    directLine: window.WebChat.createDirectLine({
        token: '_65h1OlObdE.T5rXvnddR2tF4WgYJmxrnciXqnuSockxETKaHWr2h8o'
    }),
    styleSet,
    styleOptions: avatarOptions
}, document.getElementById('webchat'));

// Open / Close Robot
function openBot() {
    var button = document.querySelector('.open_button');
    var chatDiv = document.querySelector('.row_robot');

    if (chatDiv.style.display === 'none' || chatDiv.style.display === '') {
        chatDiv.style.display = 'block';
        button.innerHTML = '<i class="fa-solid fa-xmark"></i>';
    } else {
        chatDiv.style.display = 'none';
        button.innerHTML = '<i class="fa-solid fa-robot"></i>';
    }
}