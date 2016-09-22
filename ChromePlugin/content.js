const HIGHLIGHT_CLASSNAME = 'dashboardify-highlight';

function handleElementClick(e) {
  e.stopPropagation();	//
  e.preventDefault();	//

  removeHighlight(this);

  var Item = {};

  Item.Website = window.location.href;
  Item.XPath = getPathTo(this);
  // Item.CSS = unique(this);

  // console.log(Item.CSS)

  chrome.runtime.sendMessage({action: "CREATE_ITEM", item: Item}, null);
}

function removeHighlight(el) {
  el.classList.remove(HIGHLIGHT_CLASSNAME);
  el.removeEventListener('click', handleElementClick);
  document.body.onmouseover = undefined;
}



function highlightDOMElements() {
  var prev;

  document.body.onmouseover = handler;

  function handler(event) {
    if (event.target === document.body || (prev && prev === event.target)) {
      return;
    }

    if (prev) {
      prev.removeEventListener('click', handleElementClick);

      prev.classList.toggle(HIGHLIGHT_CLASSNAME);
      prev = undefined;
    }

    if (event.target) {
      prev = event.target;
      prev.classList.toggle(HIGHLIGHT_CLASSNAME);
      prev.addEventListener('click', handleElementClick);
    }
  }
}

//Returns XPath to element
function getPathTo(element) {
    if (element.tagName == 'HTML')
        return '/HTML[1]';
    if (element===document.body)
        return '/HTML[1]/BODY[1]';

    var ix= 0;
    var siblings= element.parentNode.childNodes;
    for (var i= 0; i<siblings.length; i++) {
        var sibling= siblings[i];
        if (sibling===element)
            return (getPathTo(element.parentNode)+'/'+element.tagName+'['+(ix+1)+']').toLowerCase();
        if (sibling.nodeType===1 && sibling.tagName===element.tagName)
            ix++;
    }
}
