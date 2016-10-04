import getXPath from './getXPath'
import cssSelectorGenerator from 'css-selector-generator'

const css = new cssSelectorGenerator
const HIGHLIGHT_CLASSNAME = 'dashboardify-highlight';

chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
      console.log(request);
      switch (request.action) {
        case 'HIGHLIGHT_ELEMENTS':
          console.log('Should highlight elements')
          highlightDOMElements()
          break;
        default:
          return null
      }
  }
)

function handleElementClick(e) {
  e.stopPropagation()
  e.preventDefault()

  removeHighlight(this)

  var Item = {}

  Item.Website = window.location.href
  Item.XPath = getXPath(this)
  try {
    Item.CSS = css.getSelector(this)
  } catch (e) {
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
