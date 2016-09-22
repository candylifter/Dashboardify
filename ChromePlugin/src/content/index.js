import getXPath from './getXPath'
var xpath = require('xpath-dom')
import unique from 'unique-selector'

const HIGHLIGHT_CLASSNAME = 'dashboardify-highlight';

// setTimeout(() => {
//   highlightDOMElements()
// }, 2000)

function handleElementClick(e) {
  e.stopPropagation()
  e.preventDefault()

  removeHighlight(this)

  var Item = {}

  var options = {
    selectorTypes : [ 'Class', 'Tag', 'NthChild', 'Attributes' ]
  }

  Item.Website = window.location.href
  Item.XPath = xpath.getXPath(this)
  // Item.XPath = getXPath(this)
  try {
    Item.CSS = unique(this, options)
  } catch (e) {
    alert('Failed to get CSS, will set it to null. Check console for more detailed info.')
    console.error(e)
    Item.CSS = null
  }
  console.log(Item.XPath)
  console.log(Item.CSS)

  chrome.runtime.sendMessage({action: "CREATE_ITEM", item: Item}, null)
}

function removeHighlight(el) {
  el.classList.remove(HIGHLIGHT_CLASSNAME)
  el.removeEventListener('click', handleElementClick)
  document.body.onmouseover = undefined
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
