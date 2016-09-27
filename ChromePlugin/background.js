chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
        console.log(request);
        switch (request.action) {
            case 'HIGHLIGHT_ITEMS':
                chrome.tabs.getSelected(null, function(tab) {
                    chrome.tabs.executeScript({
                        code: 'highlightDOMElements()'
                    });
                });
                break;
            case 'CREATE_ITEM':
                chrome.tabs.create({
                    url: 'views/createItem.html?website=' + request.item.Website + '&xpath=' + request.item.XPath + '&css=' + request.item.CSS
                });
            default:
                return null;
        }
    }
);
