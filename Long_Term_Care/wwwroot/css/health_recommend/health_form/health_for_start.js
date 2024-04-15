// 將程式碼模塊化以提高可讀性和可維護性
document.getElementById('startButton').addEventListener('click', handleStartButtonClick);

function handleStartButtonClick() {
    displayQuestion(); // 使用有意義的函數名稱提高可讀性
}

function displayQuestion() {
    document.getElementById('question').style.display = 'block';
}
