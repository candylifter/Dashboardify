document.addEventListener('DOMContentLoaded', function() {
    var selectAreaToggle = document.getElementById('select-area-toggle');

    selectAreaToggle.onclick = function() {
        this.classList.toggle('active');

        // chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
        //   chrome.tabs.sendMessage(tabs[0].id, {greeting: "hello"}, function(response) {
        //     console.log(response.farewell);
        //   });
        // });

        chrome.runtime.sendMessage({
            action: "HIGHLIGHT_ITEMS"
        }, function(response) {});
    }
});

function postItem(item) {
    $.post(
            "http://23.251.133.254/app/api/Items/CreateItem", {
                Item: item
            }
        )
        .done(function(res) {
            console.log(res);
        })
        .fail(function() {});
}

chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {

        if (request.action == 'POST_ITEM') {
            $.post(
                    "http://23.251.133.254/app/api/Items/CreateItem", {
                        Item: request.item
                    }
                )
                .done(function(res) {
                    sendResponse({
                        data: res
                    });
                })
                .fail(function() {
                    sendResponse({
                        data: 'Error while posting'
                    });
                });
        }
    });


    var homeButton = document.getElementById('home-button');

    homeButton.onclick = function() {
      var newTab = {url : "http://23.251.133.254/app#/register?_k=0tskup"}
      chrome.tabs.create(newTab);
    }
