document.addEventListener('DOMContentLoaded', function () {
  var selectAreaToggle = document.getElementById('select-area-toggle');

  selectAreaToggle.onclick = function () {
    this.classList.toggle('active');

    // chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
    //   chrome.tabs.sendMessage(tabs[0].id, {greeting: "hello"}, function(response) {
    //     console.log(response.farewell);
    //   });
    // });

    chrome.tabs.getSelected(null, function(tab) {
      chrome.tabs.executeScript({
        code: 'highlightDOMElements()'
      });
    });

  }
});
