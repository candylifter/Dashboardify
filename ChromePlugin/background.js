chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
        switch (request.action) {
            case 'HIGHLIGHT_ITEMS':
                chrome.tabs.query({active: true, currentWindow: true}, function(tabs) {
                  chrome.tabs.sendMessage(tabs[0].id, { action: 'HIGHLIGHT_ELEMENTS' }, null);
                });
                break;
            case 'CREATE_ITEM':
                chrome.tabs.create({
                    url: 'views/createItem.html?website=' + request.item.Website + '&xpath=' + request.item.XPath + '&css=' + request.item.CSS
                });
                break;
            default:
                return null;
        }
    }
);
